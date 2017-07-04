/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: AdminServerService.cs
////
//// Summary: AdminServerService
//// 
//// Date: 10/31/2015 11:08:02 AM
//////////////////////////////////////////////////////////////////////////

using System.ServiceModel;
using System.ServiceModel.Activation;

namespace UIH.RT.TMS.AdminServer
{
    [ServiceContract]
    public interface IAdminServer
    {
        [OperationContract]
        bool RestartServerStoreScp();
    }

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class AdminServerService : IAdminServer
    {
        public bool RestartServerStoreScp()
        {
            return ServerStoreScp.ReStartStoreScpService();
        }
    }
}
