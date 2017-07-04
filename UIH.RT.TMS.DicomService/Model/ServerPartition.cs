/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ServerPartition.cs
////
//// Summary:
////
////
//// Date: 2014/08/19
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIH.RT.TMS.DicomService.Model
{
    public class ServerPartition
    {
        public int Id { get; set; }

        public int FileSystemFk { get; set; }

        public string AeTitle { get; set; }

        public bool Enable { get; set; }

        public string Hostname { get; set; }

        public int Port { get; set; }

        public string Description { get; set; }

        public string StationName { get; set; }

        public string Institution { get; set; }

        public string Department { get; set; }

        public string WadoUrl { get; set; }

        public string PatitionFolder { get; set; }

        public bool AcceptAnyDevice { get; set; }

        public bool AutoInsertDevice { get; set; }
    }
}
