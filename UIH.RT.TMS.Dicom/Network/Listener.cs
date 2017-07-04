/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: Listener.cs
////
//// Summary:
////
////
//// Date: 2014/08/19
//////////////////////////////////////////////////////////////////////////
#region License

// Copyright (c) 2011 - 2013, United-Imaging Inc.
// All rights reserved.
// http://www.united-imaging.com

#endregion

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UIH.RT.Framework.Utility;
using UIH.RT.TMS.Common;

namespace UIH.RT.TMS.Dicom.Network
{
    internal struct ListenerInfo
    {
        public StartAssociation StartDelegate;
        public ServerAssociationParameters Parameters;
    }

    /// <summary>
    /// Class used to create background listen threads for incoming DICOM associations.
    /// </summary>
    internal class Listener : IDisposable
    {
        #region Members

        static private readonly Dictionary<IPEndPoint, Listener> _listeners = new Dictionary<IPEndPoint, Listener>();
        private readonly IPEndPoint _ipEndPoint = null;
        private readonly Dictionary<String, ListenerInfo> _applications = new Dictionary<String, ListenerInfo>();
        private TcpListener _tcpListener = null;
        private Thread _theThread = null;
        private volatile bool _stop = false;
		private static readonly object _syncLock = new object();
        #endregion

        #region Public Static Methods

        public static bool Listen(ServerAssociationParameters parameters, StartAssociation acceptor)
        {
			lock (_syncLock)
			{
				Listener theListener;
				if (_listeners.TryGetValue(parameters.LocalEndPoint, out theListener))
				{

					ListenerInfo info = new ListenerInfo();

					info.StartDelegate = acceptor;
					info.Parameters = parameters;

					if (theListener._applications.ContainsKey(parameters.CalledAE))
					{
                        LogAdapter.Logger.ErrorWithFormat("Already listening with AE {0} on {1}", parameters.CalledAE,
						             parameters.LocalEndPoint.ToString());
						return false;
					}

					theListener._applications.Add(parameters.CalledAE, info);
                    LogAdapter.Logger.InfoWithFormat("Starting to listen with AE {0} on existing port {1}", parameters.CalledAE,
					             parameters.LocalEndPoint.ToString());
				}
				else
				{
					theListener = new Listener(parameters, acceptor);
					if (!theListener.StartListening())
					{
                        LogAdapter.Logger.ErrorWithFormat("Unexpected error starting to listen on {0}", parameters.LocalEndPoint.ToString());
						return false;
					}

					_listeners[parameters.LocalEndPoint] = theListener;
					theListener.StartThread();

                    LogAdapter.Logger.InfoWithFormat("Starting to listen with AE {0} on port {1}", parameters.CalledAE,
					             parameters.LocalEndPoint.ToString());
				}

				return true;
			}
        }

		public bool StartListening()
		{
			_tcpListener = new TcpListener(_ipEndPoint);
			try
			{
				_tcpListener.Start(50);
			}
			catch (SocketException e)
			{
                LogAdapter.Logger.TraceException(e);
                LogAdapter.Logger.ErrorWithFormat("Shutting down listener on {0}", _ipEndPoint.ToString());
				_tcpListener = null;
				return false;
			}
			return true;
		}

        public static bool StopListening(ServerAssociationParameters parameters)
        {
			lock (_syncLock)
			{
				Listener theListener;

				if (_listeners.TryGetValue(parameters.LocalEndPoint, out theListener))
				{
					if (theListener._applications.ContainsKey(parameters.CalledAE))
					{
						theListener._applications.Remove(parameters.CalledAE);

						if (theListener._applications.Count == 0)
						{
							// Cleanup the listener
							_listeners.Remove(parameters.LocalEndPoint);
							theListener.StopThread();
							theListener.Dispose();
						}
                        LogAdapter.Logger.InfoWithFormat("Stopping listening with AE {0} on {1}", parameters.CalledAE,
						             parameters.LocalEndPoint.ToString());
					}
					else
					{
                        LogAdapter.Logger.ErrorWithFormat("Unable to stop listening on AE {0}, assembly was not listening with this AE.",
						             parameters.CalledAE);
						return false;
					}
				}
				else
				{
                    LogAdapter.Logger.ErrorWithFormat("Unable to stop listening, assembly was not listening on end point {0}.",
					             parameters.LocalEndPoint.ToString());
					return false;
				}

				return true;
			}
        }
        #endregion

        #region Constructors
        internal Listener(ServerAssociationParameters parameters, StartAssociation acceptor)
        {
            ListenerInfo info = new ListenerInfo();

            info.Parameters = parameters;
            info.StartDelegate = acceptor;

            _applications.Add(parameters.CalledAE, info);

            _ipEndPoint = parameters.LocalEndPoint;
        }
        #endregion

        private void StartThread()
        {
            
            _theThread = new Thread(Listen);
            _theThread.Name = "Association Listener on port " + _ipEndPoint.Port;

            _theThread.Start();
        }

        private void StopThread()
        {
            _stop = true;

            _theThread.Join();
        }

        public void Listen()
        {
            while (_stop == false)
            {
                // Tried Async i/o here, but had some weird problems with connections not getting
                // through.
                if (_tcpListener.Pending())
                {
                    Socket theSocket = _tcpListener.AcceptSocket();

					// The DicomServer will automatically start working in the background
                    new DicomServer(theSocket, _applications);
                    continue;
                }
                Thread.Sleep(10);               
            }
            try
            {
                _tcpListener.Stop();
            }
            catch (SocketException e)
            {
				LogAdapter.Logger.TraceException(e);
            }
        }

        #region IDisposable Implementation
        public void Dispose()
        {
            StopThread();
            if (_tcpListener != null)
            {
                _tcpListener.Stop();
                _tcpListener = null;
            }
        }
        #endregion
    }
}
