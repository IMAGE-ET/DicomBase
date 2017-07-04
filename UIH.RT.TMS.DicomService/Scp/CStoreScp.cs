/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: CStoreScp.cs
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
using System.Collections.Generic;
using System.ComponentModel.Composition;
using UIH.RT.Framework.Utility;
using UIH.RT.TMS.Common;
using UIH.RT.TMS.Dicom;
using UIH.RT.TMS.Dicom.Network;
using UIH.RT.TMS.Dicom.Network.Scp;
using UIH.RT.TMS.DicomService.Model;

namespace UIH.RT.TMS.DicomService
{
    [Export(typeof(IDicomScp<DicomScpContext>))]
    public class CStoreScp : BaseScp
    {
        #region Private Member

        private IList<SupportedSop> _list;

        private static readonly List<TransferSyntax> TransferSyntaxUIDList =
            new List<TransferSyntax>
                {
                    TransferSyntax.ExplicitVrLittleEndian,
                    TransferSyntax.ImplicitVrLittleEndian,
                };

        #endregion

        public override IList<SupportedSop> GetSupportedSopClasses()
        {
            if (_list == null)
            {
                _list = new List<SupportedSop>();

                var storageAbstractSyntaxList = new List<SopClass>();

                storageAbstractSyntaxList.Add(SopClass.MrImageStorage);
                storageAbstractSyntaxList.Add(SopClass.CtImageStorage);
                storageAbstractSyntaxList.Add(SopClass.SecondaryCaptureImageStorage);
                // Add SupportedSopCless here
                
                foreach (var abstractSyntax in storageAbstractSyntaxList)
                {
                    var supportedSop = new SupportedSop {SopClass = abstractSyntax};
                    supportedSop.AddSyntax(TransferSyntax.ExplicitVrLittleEndian);
                    supportedSop.AddSyntax(TransferSyntax.ImplicitVrLittleEndian);
                    _list.Add(supportedSop);
                }
            }

            return _list;
        }

        public override bool OnReceiveRequest(DicomServer server,
                                              ServerAssociationParameters association,
                                              byte presentationID, DicomMessage message)
        {
            try
            {
                server.SendCStoreResponse(presentationID, message.MessageId,
                    message.AffectedSopInstanceUid, DicomStatuses.Success);
            }
            catch (DicomDataException ex)
            {
                LogAdapter.Logger.TraceException(ex);
                return false; // caller will abort the association
            }
            catch (Exception ex)
            {
                LogAdapter.Logger.TraceException(ex);
                return false; // caller will abort the association
            }

            return true;
        }

        protected override DicomPresContextResult OnVerifyAssociation(AssociationParameters association, byte pcid)
        {
            // Add Verify Logic here to control who has privilege Store 

            //bool isNew;
            //Device device = DeviceManager.LookupDevice(association, out isNew);
            //if (!device.AllowStorage)
            //    return DicomPresContextResult.RejectUser;

            return DicomPresContextResult.Accept;
        }
    }
}
