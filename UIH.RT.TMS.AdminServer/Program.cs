/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: chengcheng.tao@united-imaging.com
////
//// File: Program.cs
////
//// Summary: 
//// 
//// Date  2015/08/11
//////////////////////////////////////////////////////////////////////////

using UIH.RT.Framework.Utility;
using UIH.RT.TMS.ServerBase;
using System.IO;
using System;

namespace UIH.RT.TMS.AdminServer
{
    static class Program
    {
        private const string _logSource = "TmsAdminServer";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            InitLog();
            System.ServiceProcess.ServiceBase[] ServicesToRun;
            ServicesToRun = new System.ServiceProcess.ServiceBase[] 
            { 
                new AdminServer() 
            };
            System.ServiceProcess.ServiceBase.Run(ServicesToRun);
        }

        private static void InitLog()
        {
            string configfile = System.Reflection.Assembly.GetExecutingAssembly().Location + ".config";
            LogAdapter.Logger.Regist(new Log4netLogHandler(configfile));
            LogAdapter.Logger.Regist(new McsfLogHandler(McsfLogHandler.DefaultServerLogConfigFile, _logSource));
        }
    }
}
