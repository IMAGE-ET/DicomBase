/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: IDicomElementProvider.cs
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

using UIH.RT.TMS.Dicom;

namespace UIH.RT.TMS.Dicom
{
	public delegate DicomElement DicomElementGetter(uint tag);
	public delegate void DicomElementSetter(uint tag, DicomElement value);
	
	/// <summary>
	/// Interface for classes that provide <see cref="DicomElement"/>s.
	/// </summary>
	public interface IDicomElementProvider
	{
		/// <summary>
		/// Gets or sets the <see cref="DicomElement"/> for the given tag.
		/// </summary>
		DicomElement this[DicomTag tag] { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="DicomElement"/> for the given tag.
		/// </summary>
		DicomElement this[uint tag] { get; set; }

		/// <summary>
		/// Attempts to get the element specified by <paramref name="tag"/>.
		/// </summary>
		bool TryGetAttribute(uint tag, out DicomElement element);

		/// <summary>
		/// Attempts to get the element specified by <paramref name="tag"/>.
		/// </summary>
		bool TryGetAttribute(DicomTag tag, out DicomElement element);
	}
}
