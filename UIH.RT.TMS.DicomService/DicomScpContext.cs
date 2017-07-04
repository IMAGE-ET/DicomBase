/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: DicomScpContext.cs
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
using UIH.RT.TMS.DicomService.Model;

namespace UIH.RT.TMS.DicomService
{
    public class DicomScpContext
    {
        #region Constructors

        public DicomScpContext(ServerPartition partition)
        {
            Partition = partition;
        }

        #endregion

        #region Properties

        public ServerPartition Partition { get; set; }

        #endregion
    }
}
