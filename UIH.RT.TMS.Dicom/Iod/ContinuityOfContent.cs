/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ContinuityOfContent.cs
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
	/// Enumerated values for the <see cref="DicomTags.ContinuityOfContent"/> attribute specifying for a CONTAINER whether or not
	/// its contained Content Items are logically linked in a
	/// continuous textual flow, or are separate items.
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.18.8 (Table C.18.8-1)</remarks>
	public enum ContinuityOfContent
	{
		/// <summary>
		/// SEPARATE
		/// </summary>
		Separate,

		/// <summary>
		/// CONTINUOUS
		/// </summary>
		Continuous,

		/// <summary>
		/// Represents the unknown status, which is equivalent to the null value.
		/// </summary>
		Unknown
	}
}
