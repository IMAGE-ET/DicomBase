#region License

// Copyright (c) 2011 - 2013, United-Imaging Inc.
// All rights reserved.
// http://www.united-imaging.com

#endregion

using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using UIH.RT.Framework.Utility;

namespace UIH.RT.TMS.HL7
{
    public class HL7Client
    {
        private readonly TcpClient _client; 

        private readonly string _hostName;

        private readonly int _port;

        public HL7Client(string hostname, int port)
        {
            _hostName = hostname;
            _port = port;
            _client = new TcpClient();
        }

        public byte[] Send(byte[] request)
        {
            if (!_client.Connected)
            {
                _client.Connect(_hostName, _port);
            }

            MllpNetworkStream stream = new MllpNetworkStream(_client.Client) { ReadTimeout = 20000, WriteTimeout = 20000 };
            stream.WriteMessage(request, 0, request.Length);
            return stream.ReadMessage();
        }

        public byte[] SendTls(byte[] request, string serverName)
        {
            if (!_client.Connected)
            {
                _client.Connect(_hostName, _port);
            }

            SslMllpNetworkSream stream = new SslMllpNetworkSream(
                new NetworkStream(_client.Client),
                false,
                new RemoteCertificateValidationCallback(ValidateServerCertificate),
                null);

            try
            {
               stream.AuthenticateAsClient(serverName);
            }
            catch (AuthenticationException e)
            {
                //Platform.Log(LogLevel.Error, e);
                LogAdapter.Logger.TraceException(e);
                if (e.InnerException != null)
                {
                    LogAdapter.Logger.TraceException(e.InnerException);
                    //Platform.Log(LogLevel.Error, e.InnerException);
                }

                LogAdapter.Logger.Error("Authentication failed - closing the connection");
                //Platform.Log(LogLevel.Error, "Authentication failed - closing the connection");
                _client.Close();
                throw;
            }

            stream.WriteMessage(request, 0, request.Length);
            return stream.ReadMessage();
        }

        public static bool ValidateServerCertificate(
              object sender,
              X509Certificate certificate,
              X509Chain chain,
              SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}