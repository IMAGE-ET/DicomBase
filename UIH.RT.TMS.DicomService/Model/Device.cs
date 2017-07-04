/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: Device.cs
////
//// Summary:
////
////
//// Date: 2014/08/19
//////////////////////////////////////////////////////////////////////////
using System;

namespace UIH.RT.TMS.DicomService.Model
{
    public class Device
    {
        public string AeTitle { get; set; }

        public string Hostname { get; set; }

        public int Port { get; set; }

        public bool Enabled { get; set; }

        public string Description { get; set; }

        public bool Dhcp { get; set; }

        public bool AllowStorage { get; set; }

        public bool AllowRetrieve { get; set; }

        public bool AllowQuery { get; set; }

        public bool AllowMPPS { get; set; }

        public bool AllowWorkList { get; set; }

        public DateTime LastAccessTime { get; set; }
    }
}
