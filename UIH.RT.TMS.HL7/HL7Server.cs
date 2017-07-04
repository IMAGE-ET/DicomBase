#region License

// Copyright (c) 2011 - 2013, United-Imaging Inc.
// All rights reserved.
// http://www.united-imaging.com

#endregion

using UIH.RT.Framework.Utility;
using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UIH.RT.TMS.HL7
{
    public class HL7Server
    {
        private static X509Certificate _serverCertificate = null;

        #region Private fields

        private readonly string _certFilename;

        private Socket _listenSocket;

        private Thread _svcThread;

        private bool _stop = false;

        public bool UseTls { get; private set; }

        #endregion

        public HL7Server()
        {
            UseTls = false;
        }

        public HL7Server(string certFilename)
        {
            _certFilename = certFilename;
            UseTls = true;
        }

        public delegate byte[] OnHL7Message(byte[] request);

        public OnHL7Message OnMessage { get; set; }

        public void Start(IPEndPoint endPoint)
        {
            if (UseTls)
            {
                _serverCertificate = X509Certificate.CreateFromCertFile(_certFilename);
            }

            _svcThread = new Thread(
                () =>
                    {
                        this._listenSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                        this._listenSocket.Bind(endPoint);

                        LogAdapter.Logger.Info(string.Format("Start HL7 Server on {0}", endPoint));
                        //Platform.Log(LogLevel.Info, "Start HL7 Server on {0}", endPoint.ToString());
                        this._listenSocket.Listen(100);

                        while (!_stop)
                        {
                            Socket client = this._listenSocket.Accept();
                            LogAdapter.Logger.Info(string.Format("Accept connection from {0}", client.RemoteEndPoint));
                            //Platform.Log(LogLevel.Debug, "Accept connection from {0}", client.RemoteEndPoint.ToString());

                            if (UseTls)
                            {
                                Task.Factory.StartNew(this.TlsClientHandle, client);
                            }
                            else
                            {
                                Task.Factory.StartNew(this.ClientHandle, client);
                            }
                        }
                    });
            _svcThread.Start();
        }

        public void Stop()
        {
            _stop = true;
        }

        private void TlsClientHandle(object obj)
        {
            Socket socket = obj as Socket;
            if (socket != null)
            {
                var stream = new SslMllpNetworkSream(new NetworkStream(socket), false);
                try
                {
                    stream.AuthenticateAsServer(_serverCertificate, false, SslProtocols.Default, true);
                }
                catch (AuthenticationException e)
                {
                    LogAdapter.Logger.TraceException(e);
                    //Platform.Log(LogLevel.Error, e);
                    stream.Close();
                    return;
                }
                catch (Exception e)
                {
                    LogAdapter.Logger.TraceException(e);
                    //Platform.Log(LogLevel.Error, e);
                    stream.Close();
                    return;
                }

                try
                {
                    while (true)
                    {
                        byte[] request = stream.ReadMessage();
                        if (HL7Setting.Default.LogRequestMessage)
                        {
                            LogAdapter.Logger.Info(string.Format("Receive an HL7 request message from {0} \n {1}",
                                socket.RemoteEndPoint,
                                Encoding.UTF8.GetString(request)));
                            //Platform.Log(
                            //    LogLevel.Info,
                            //    "Receive an HL7 request message from {0} \n {1}",
                            //    socket.RemoteEndPoint.ToString(),
                            //    Encoding.UTF8.GetString(request));
                        }

                        byte[] response = OnMessage(request);
                        if (HL7Setting.Default.LogResponseMessage)
                        {
                            LogAdapter.Logger.Info(string.Format("Sending HL7 Response message to {0} \n {1}",
                                socket.RemoteEndPoint,
                                Encoding.UTF8.GetString(response)));
                            //Platform.Log(
                            //    LogLevel.Info,
                            //    "Sending HL7 Response message to {0} \n {1}",
                            //    socket.RemoteEndPoint.ToString(),
                            //    Encoding.UTF8.GetString(response));
                        }

                        stream.WriteMessage(response, 0, response.Length);
                    }
                }
                catch (Exception ex)
                {
                    LogAdapter.Logger.TraceException(ex);
                    //Platform.Log(LogLevel.Warn, ex);
                }
                finally
                {
                    stream.Close();
                }
            }
        }

        private void ClientHandle(object obj)
        {
            Socket socket = obj as Socket;
            if (socket != null)
            {
                var stream = new MllpNetworkStream(socket, true) { ReadTimeout = 20000, WriteTimeout = 20000 };
                
                try
                {
                    while (true)
                    {
                        byte[] request = stream.ReadMessage();
                        if (HL7Setting.Default.LogRequestMessage)
                        {
                            LogAdapter.Logger.Info(string.Format("Receive an HL7 request message from {0} \n {1}",
                                socket.RemoteEndPoint,
                                Encoding.UTF8.GetString(request)));
                            //Platform.Log(
                            //    LogLevel.Info,
                            //    "Receive an HL7 request message from {0} \n {1}",
                            //    socket.RemoteEndPoint.ToString(),
                            //    Encoding.UTF8.GetString(request));
                        }

                        byte[] response = OnMessage(request);
                        if (HL7Setting.Default.LogResponseMessage)
                        {
                            LogAdapter.Logger.Info(string.Format("Sending HL7 Response message to {0} \n {1}",
                                socket.RemoteEndPoint,
                                Encoding.UTF8.GetString(response)));
                            //Platform.Log(
                            //    LogLevel.Info,
                            //    "Sending HL7 Response message to {0} \n {1}",
                            //    socket.RemoteEndPoint.ToString(),
                            //    Encoding.UTF8.GetString(response));
                        }

                        stream.WriteMessage(response, 0, response.Length);
                    }
                }
                catch (Exception ex)
                {
                    LogAdapter.Logger.TraceException(ex);
                    //Platform.Log(LogLevel.Warn, ex);
                }
                finally
                {
                    stream.Close();
                }
            }
        }
    }
}
