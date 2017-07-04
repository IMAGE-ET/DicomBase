/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: AssociationVerifier.cs
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

using UIH.RT.Framework.Utility;
using UIH.RT.TMS.Common;
using UIH.RT.TMS.Dicom.Network;
using UIH.RT.TMS.DicomService.Model;

namespace UIH.RT.TMS.DicomService
{
    /// <summary>
    /// Static class used to verify if an association should be accepted.
    /// </summary>
    public static class AssociationVerifier
    {
        /// <summary>
        /// Do the actual verification if an association is acceptable.
        /// </summary>
        /// <remarks>
        /// This method primarily checks the remote AE title to see if it is a valid device that can 
        /// connect to the partition.
        /// </remarks>
        /// <param name="context">Generic parameter passed in, is a DicomScpParameters instance.</param>
        /// <param name="assocParms">The association parameters.</param>
        /// <param name="result">Output parameter with the DicomRejectResult for rejecting the association.</param>
        /// <param name="reason">Output parameter with the DicomRejectReason for rejecting the association.</param>
        /// <returns>true if the association should be accepted, false if it should be rejected.</returns>
        public static bool Verify(DicomScpContext context, ServerAssociationParameters assocParms,
                                  out DicomRejectResult result, out DicomRejectReason reason)
        {
            bool isNew;
            Device device = DeviceManager.LookupDevice( assocParms, out isNew);

            if (device == null)
            {
                reason = DicomRejectReason.CallingAENotRecognized;
                result = DicomRejectResult.Permanent;
                return false;
            }

            if (device.Enabled == false)
            {
                LogAdapter.Logger.ErrorWithFormat("Rejecting association from {0} to {1}.  Device is disabled.",
                    assocParms.CallingAE, assocParms.CalledAE);

                reason = DicomRejectReason.CallingAENotRecognized;
                result = DicomRejectResult.Permanent;
                return false;
            }

            reason = DicomRejectReason.NoReasonGiven;
            result = DicomRejectResult.Permanent;

            return true;
        }
    }
}
