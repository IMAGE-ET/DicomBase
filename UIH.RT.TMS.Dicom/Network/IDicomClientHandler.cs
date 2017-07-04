/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: IDicomClientHandler.cs
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
    public interface IDicomClientHandler
    {
        void OnReceiveAssociateAccept(DicomClient client, ClientAssociationParameters association);
        void OnReceiveAssociateReject(DicomClient client, ClientAssociationParameters association, DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason);
        void OnReceiveRequestMessage(DicomClient client, ClientAssociationParameters association, byte presentationID, DicomMessage message);
        void OnReceiveResponseMessage(DicomClient client, ClientAssociationParameters association, byte presentationID, DicomMessage message);
        void OnReceiveReleaseResponse(DicomClient client, ClientAssociationParameters association);
        void OnReceiveAbort(DicomClient client, ClientAssociationParameters association, DicomAbortSource source, DicomAbortReason reason);
        void OnNetworkError(DicomClient client, ClientAssociationParameters association, Exception e);
        void OnDimseTimeout(DicomClient server, ClientAssociationParameters association);
    }
}
