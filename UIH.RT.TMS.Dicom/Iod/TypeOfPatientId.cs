/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: TypeOfPatientId.cs
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

namespace UIH.RT.TMS.Dicom.Iod
{
	/// <summary>
	/// Enumerated values for the <see cref="DicomTags.TypeOfPatientId"/> attribute indicating the type of identifier in an OtherPatientIds Sequence Item.
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.7.1.1 (Table C.7-1)</remarks>
	public enum TypeOfPatientId {
		/// <summary>
		/// TEXT
		/// </summary>
		Text,

		/// <summary>
		/// RFID
		/// </summary>
		Rfid,

		/// <summary>
		/// BARCODE
		/// </summary>
		Barcode,

		/// <summary>
		/// Represents the unknown status, which is equivalent to the null value.
		/// </summary>
		Unknown
	}
}
