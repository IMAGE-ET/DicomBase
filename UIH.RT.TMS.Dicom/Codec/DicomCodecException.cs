/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: DicomCodecException.cs
////
//// Summary:
////
////
//// Date: 2014/08/18
//////////////////////////////////////////////////////////////////////////
#region License

// Copyright (c) 2011 - 2013, United-Imaging Inc.
// All rights reserved.
// http://www.united-imaging.com

#endregion

using System;

namespace UIH.RT.TMS.Dicom.Codec
{
    /// <summary>
    /// A codec specific exception.
    /// </summary>
    [Serializable]
    public class DicomCodecException : DicomException
    {
        public DicomCodecException()
        {
        }
        public DicomCodecException(string desc, Exception e) : base(desc, e)
        {
        }

        public DicomCodecException(string desc) : base(desc)
        {
        }
    }
}
