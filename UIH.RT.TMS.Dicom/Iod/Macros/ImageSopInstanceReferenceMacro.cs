/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ImageSopInstanceReferenceMacro.cs
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

namespace UIH.RT.TMS.Dicom.Iod.Macros
{
	/// <summary>
	/// Image SOP Instance Reference Macro
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section 10.3 (Table 10-3)</remarks>
	public class ImageSopInstanceReferenceMacro : SequenceIodBase
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ImageSopInstanceReferenceMacro"/> class.
		/// </summary>
		public ImageSopInstanceReferenceMacro() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="ImageSopInstanceReferenceMacro"/> class.
		/// </summary>
		/// <param name="dicomSequenceItem">The dicom sequence item.</param>
		public ImageSopInstanceReferenceMacro(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) {}

		#endregion

		#region Public Properties

		/// <summary>
		/// Uniquely identifies the referenced SOP Class
		/// </summary>
		/// <value>The referenced sop class uid.</value>
		public string ReferencedSopClassUid
		{
			get { return base.DicomElementProvider[DicomTags.ReferencedSopClassUid].GetString(0, String.Empty); }
			set { base.DicomElementProvider[DicomTags.ReferencedSopClassUid].SetString(0, value); }
		}

		/// <summary>
		/// Uniquely identifies the referenced SOP Instance.
		/// </summary>
		/// <value>The referenced sop instance uid.</value>
		public string ReferencedSopInstanceUid
		{
			get { return base.DicomElementProvider[DicomTags.ReferencedSopInstanceUid].GetString(0, String.Empty); }
			set { base.DicomElementProvider[DicomTags.ReferencedSopInstanceUid].SetString(0, value); }
		}

		/// <summary>
		/// Identifies the frame numbers within the Referenced SOP Instance to which the 
		/// reference applies. The first frame shall be denoted as frame number 1. 
		/// <para>Note: This Attribute may be multi-valued. </para> 
		/// <para>
		/// Required if the Referenced SOP Instance is a multi-frame image and the reference 
		/// does not apply to all frames, and Referenced Segment Number (0062,000B) is not present.
		/// </para> 
		/// </summary>
		/// <value>The referenced frame number.</value>
		public DicomElementIs ReferencedFrameNumber
		{
			get { return base.DicomElementProvider[DicomTags.ReferencedFrameNumber] as DicomElementIs; }
		}

		/// <summary>
		/// Identifies the Segment Number to which the reference applies. Required if the Referenced
		///  SOP Instance is a Segmentation and the reference does not apply to all segments and
		///  Referenced Frame Number (0008,1160) is not present.
		/// </summary>
		/// <value>The referenced segment number.</value>
		public DicomElementUs ReferencedSegmentNumber
		{
			get { return base.DicomElementProvider[DicomTags.ReferencedSegmentNumber] as DicomElementUs; }
		}

		#endregion
	}
}
