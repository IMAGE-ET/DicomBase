/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: PatientsSexNeutered.cs
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
	/// Enumerated values for the <see cref="DicomTags.PatientsSexNeutered"/> attribute describing whether or not a procedure has been performed in an effort to render the patient sterile.
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.7.2.2 (Table C.7-4a)</remarks>
	public enum PatientsSexNeutered
	{
		/// <summary>
		/// Altered/Neutered.
		/// </summary>
		Altered,

		/// <summary>
		/// Unaltered/Intact.
		/// </summary>
		Unaltered,

		/// <summary>
		/// Represents the unknown status, which is equivalent to the null value.
		/// </summary>
		Unknown
	}
}
