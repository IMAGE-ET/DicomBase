using System;
using System.ServiceModel;
using System.Net;
using UIH.RT.TMS.HL7Server;
using UIH.RT.TMS.ServerBase;
using System.Net.Sockets;

namespace UIH.RT.TMS.AdminServer
{
    public partial class AdminServer : System.ServiceProcess.ServiceBase
    {
        private ServiceHost _adminServerServiceHost = null;

        public AdminServer()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ServerStoreScp.StartStoreSCPService();
            StartAdminServerService();
            StartHL7Server();
        }

        protected override void OnStop()
        {
            ServerStoreScp.StopStoreSCPService();
            StopAdminServerService();
            StopHL7Server();
        }

        private void StartAdminServerService()
        {
            if (_adminServerServiceHost != null)
            {
                _adminServerServiceHost.Close();
            }

            // Create a ServiceHost for the CalculatorService type and 
            // provide the base address.
            _adminServerServiceHost = new ServiceHost(typeof(AdminServerService));

            // Open the ServiceHostBase to create listeners and start 
            // listening for messages.
            _adminServerServiceHost.Open();
        }

        private void StopAdminServerService()
        {
            if (_adminServerServiceHost != null)
            {
                _adminServerServiceHost.Close();
                _adminServerServiceHost = null;
            }
        }

        private const string address = "127.0.0.1";
        private HL7Handler hl7Handle;
        private UIH.RT.TMS.HL7.HL7Server hl7Server;
        private void StartHL7Server()
        {
            hl7Handle = new HL7Handler();
            hl7Server = new UIH.RT.TMS.HL7.HL7Server();
            var ipEndPonint = new IPEndPoint(IPAddress.Parse(address), 8080);
            hl7Server.OnMessage += hl7Handle.ProcessMessage;
            hl7Server.Start(ipEndPonint);
        }

        private void StopHL7Server()
        {
            if (hl7Server != null)
            {
                hl7Server.Stop();
            }
        }
    }
}
