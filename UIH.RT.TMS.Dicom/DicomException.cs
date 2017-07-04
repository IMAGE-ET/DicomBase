/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: DicomException.cs
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
using System.Runtime.Serialization;

namespace UIH.RT.TMS.Dicom
{
    [Serializable]
    public class DicomException : Exception
    {
        public DicomException(){}

        public DicomException(String desc)
            : base(desc)
        {
        }
        public DicomException(String desc, Exception e)
            : base(desc,e)
        {
        }
        protected DicomException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
