/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: DeviceManager.cs
////
//// Summary:
////
////
//// Date: 2014/08/19
//////////////////////////////////////////////////////////////////////////
#region License

// Copyright (c) 2011 - 2014, **** Inc.
// All rights reserved.
// http://www.****.com

#endregion

using System;
using System.Net;
using UIH.RT.TMS.Dicom.Network;
using UIH.RT.TMS.DicomService.Model;

namespace UIH.RT.TMS.DicomService
{
    internal class DeviceManager
    {
        public static Device LookupDevice(AssociationParameters association, out bool isNew)
        {
            /*
             * Read From Config to Check if the AE is supported!
             */
            isNew = true;
            return new Device
                       {
                           Enabled = true
                       };
        }
    }
}
