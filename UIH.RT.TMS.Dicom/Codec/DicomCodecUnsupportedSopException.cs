/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: DicomCodecUnsupportedSopException.cs
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
    [Serializable]
    public class DicomCodecUnsupportedSopException : DicomCodecException
	{
        public DicomCodecUnsupportedSopException()
        {
        }
		public DicomCodecUnsupportedSopException(string desc, Exception e) : base(desc, e)
        {
        }

		public DicomCodecUnsupportedSopException(string desc)
			: base(desc)
        {
        }
	}
}
