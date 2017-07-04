/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: FileReference.cs
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

namespace UIH.RT.TMS.Dicom
{
	internal class FileReference
	{
		#region Private Members

		private readonly string _filename;
		private readonly long _offset;
		private readonly long _length;
		private readonly Endian _endian;
		private readonly DicomVr _vr;

		#endregion

		#region Public Properties

		internal string Filename
		{
			get { return _filename; }
		}

		internal long Offset
		{
			get { return _offset; }
		}

		internal Endian Endian
		{
			get { return _endian; }
		}

		internal DicomVr Vr
		{
			get { return _vr; }
		}

		public uint Length
		{
			get { return (uint) _length; }
		}

		#endregion

		#region Constructors

		internal FileReference(string file, long offset, long length, Endian endian, DicomVr vr)
		{
			_filename = file;
			_offset = offset;
			_length = length;
			_endian = endian;
			_vr = vr;
		}

		#endregion
	}
}
