/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: IDicomServerHandler.cs
////
//// Summary:
////
////
//// Date: 2014/08/19
//////////////////////////////////////////////////////////////////////////
#region License

// Copyright (c) 2011 - 2013, United-Imaging Inc.
// All rights reserved.
// http://www.united-imaging.com

#endregion

using System;

namespace UIH.RT.TMS.Dicom.Network
{
    public interface IDicomServerHandler
    {
        void OnReceiveAssociateRequest(DicomServer server, ServerAssociationParameters association);
        void OnReceiveRequestMessage(DicomServer server, ServerAssociationParameters association, byte presentationID, DicomMessage message);
        void OnReceiveResponseMessage(DicomServer server, ServerAssociationParameters association, byte presentationID, DicomMessage message);
        void OnReceiveReleaseRequest(DicomServer server, ServerAssociationParameters association);
        
        void OnReceiveAbort(DicomServer server, ServerAssociationParameters association, DicomAbortSource source, DicomAbortReason reason);
        void OnNetworkError(DicomServer server, ServerAssociationParameters association, Exception e);
        void OnDimseTimeout(DicomServer server, ServerAssociationParameters association);
        
    }
}
