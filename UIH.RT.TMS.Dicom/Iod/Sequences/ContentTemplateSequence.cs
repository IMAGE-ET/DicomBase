/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ContentTemplateSequence.cs
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

namespace UIH.RT.TMS.Dicom.Iod.Sequences
{
	/// <summary>
	/// Content Template Sequence
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.18.8 (Table C.18.8-1)</remarks>
	public class ContentTemplateSequence : SequenceIodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ContentTemplateSequence"/> class.
		/// </summary>
		public ContentTemplateSequence() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="ContentTemplateSequence"/> class.
		/// </summary>
		/// <param name="dicomSequenceItem">The dicom sequence item.</param>
		public ContentTemplateSequence(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) {}

		public void InitializeAttributes()
		{
			this.MappingResource = "DCMR";
			this.TemplateIdentifier = "1";
		}

		/// <summary>
		/// Gets or sets the value of MappingResource in the underlying collection. Type 1.
		/// </summary>
		public string MappingResource
		{
			get { return base.DicomElementProvider[DicomTags.MappingResource].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("value", "MappingResource is Type 1 Required.");
				base.DicomElementProvider[DicomTags.MappingResource].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of TemplateIdentifier in the underlying collection. Type 1.
		/// </summary>
		public string TemplateIdentifier
		{
			get { return base.DicomElementProvider[DicomTags.TemplateIdentifier].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("value", "Template Identifier is Type 1 Required.");
				base.DicomElementProvider[DicomTags.TemplateIdentifier].SetString(0, value);
			}
		}
	}
}
