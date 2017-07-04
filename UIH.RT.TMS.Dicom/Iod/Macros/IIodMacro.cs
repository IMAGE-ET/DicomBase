/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: IIodMacro.cs
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

namespace UIH.RT.TMS.Dicom.Iod.Macros
{
	public interface IIodMacro
	{
		/// <summary>
		/// Initializes the underlying collection to implement the module or sequence using default values.
		/// </summary>
		void InitializeAttributes();

		/// <summary>
		/// Gets or sets the underlying DICOM sequence item.
		/// </summary>
		/// <remarks>
		/// This property may return NULL for macros implemented at the module level rather than on a sequence item.
		/// </remarks>
		/// <value>The DICOM sequence item.</value>
		DicomSequenceItem DicomSequenceItem { get; set; }
	}
}
