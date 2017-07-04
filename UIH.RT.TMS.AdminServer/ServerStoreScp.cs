/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ServerStoreScp.cs
////
//// Summary: ServerStoreScp
//// 
//// Date: 10/31/2015 11:09:49 AM
//////////////////////////////////////////////////////////////////////////
using System;
using UIH.RT.TMS.ServerStoreScp;
using UIH.RT.Framework.Utility;

namespace UIH.RT.TMS.AdminServer
{
    public static class ServerStoreScp
    {
        public static void StartStoreSCPService()
        {
            try
            {
                TMSServerService.Start();
            }
            catch (Exception ex)
            {
                LogAdapter.Logger.TraceException(ex);
            }
        }

        public static void StopStoreSCPService()
        {
            try
            {
                TMSServerService.Stop();
            }
            catch (Exception ex)
            {
                LogAdapter.Logger.TraceException(ex);
            }
        }

        public static bool ReStartStoreScpService()
        {
            try
            {
                TMSServerService.Stop();
                TMSServerService.Start();
                return true;
            }
            catch (Exception ex)
            {
                LogAdapter.Logger.TraceException(ex);
                return false;
            }
        }
    }
}
