/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: BaseScp.cs
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
using UIH.RT.Framework.Utility;
using UIH.RT.TMS.Common;
using UIH.RT.TMS.Dicom;
using UIH.RT.TMS.Dicom.Network;
using UIH.RT.TMS.Dicom.Network.Scp;
using UIH.RT.TMS.DicomService.Model;

namespace UIH.RT.TMS.DicomService
{
    public abstract class BaseScp : IDicomScp<DicomScpContext>
    {
        #region Protected Members

        private DicomScpContext _context;

        #endregion

        #region Properties
        
        #endregion

        public DicomPresContextResult VerifyAssociation(AssociationParameters association, byte pcid)
        {
            var result = OnVerifyAssociation(association, pcid);
            if (result != DicomPresContextResult.Accept)
            {
                LogAdapter.Logger.WarnWithFormat(
                    "Rejecting Presentation Context {0}:{1} in association between {2} and {3}.", pcid,
                    association.GetAbstractSyntax(pcid).Description,
                    association.CallingAE, association.CalledAE);
            }

            return result;
        }

        protected abstract DicomPresContextResult OnVerifyAssociation(AssociationParameters association, byte pcid);
        public abstract bool OnReceiveRequest(DicomServer server, ServerAssociationParameters association, byte presentationID, DicomMessage message);
        public abstract IList<SupportedSop> GetSupportedSopClasses();

        public void SetContext(DicomScpContext context)
        {
            _context = context;
        }

        public virtual void Cleanup() { }
        public virtual void AssociationRelease(DicomServer server, AssociationParameters assoc) { }
        public virtual void AssociationAbort(DicomServer server, AssociationParameters assoc) { }
        public virtual void OnNetworkError(DicomServer server, AssociationParameters assoc) { }
    }
}
