/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ContainerMacro.cs
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
using UIH.RT.TMS.Dicom.Iod.Sequences;

namespace UIH.RT.TMS.Dicom.Iod.Macros
{
	/// <summary>
	/// Container Macro
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.18.8 (Table C.18.8-1)</remarks>
	public interface IContainerMacro : IIodMacro
	{
		/// <summary>
		/// Gets or sets the value of ContinuityOfContent in the underlying collection. Type 1.
		/// </summary>
		ContinuityOfContent ContinuityOfContent { get; set; }

		/// <summary>
		/// Gets or sets the value of ContentTemplateSequence in the underlying collection. Type 1C.
		/// </summary>
		ContentTemplateSequence ContentTemplateSequence { get; set; }

		/// <summary>
		/// Creates the value of ContentTemplateSequence in the underlying collection. Type 1C.
		/// </summary>
		ContentTemplateSequence CreateContentTemplateSequence();
	}

	/// <summary>
	/// Container Macro Base Implementation
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.18.8 (Table C.18.8-1)</remarks>
	internal class ContainerMacro : SequenceIodBase, IContainerMacro
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerMacro"/> class.
		/// </summary>
		public ContainerMacro() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerMacro"/> class.
		/// </summary>
		/// <param name="dicomSequenceItem">The dicom sequence item.</param>
		public ContainerMacro(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) {}

		/// <summary>
		/// Initializes the underlying collection to implement the module or sequence using default values.
		/// </summary>
		public void InitializeAttributes()
		{
			this.ContinuityOfContent = ContinuityOfContent.Separate;
			this.ContentTemplateSequence = null;
		}

		/// <summary>
		/// Gets or sets the value of ContinuityOfContent in the underlying collection. Type 1.
		/// </summary>
		public ContinuityOfContent ContinuityOfContent
		{
			get { return ParseEnum(base.DicomElementProvider[DicomTags.ContinuityOfContent].GetString(0, string.Empty), ContinuityOfContent.Unknown); }
			set
			{
				if (value == ContinuityOfContent.Unknown)
					throw new ArgumentNullException("value", "Continuity of Content is Type 1 Required.");
				SetAttributeFromEnum(base.DicomElementProvider[DicomTags.ContinuityOfContent], value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ContentTemplateSequence in the underlying collection. Type 1C.
		/// </summary>
		public ContentTemplateSequence ContentTemplateSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.ContentTemplateSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}
				return new ContentTemplateSequence(((DicomSequenceItem[]) dicomElement.Values)[0]);
			}
			set
			{
				if (value == null)
				{
					base.DicomElementProvider[DicomTags.ContentTemplateSequence] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.ContentTemplateSequence].Values = new DicomSequenceItem[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Creates the value of ContentTemplateSequence in the underlying collection. Type 1C.
		/// </summary>
		public ContentTemplateSequence CreateContentTemplateSequence()
		{
			DicomElement dicomElement = base.DicomElementProvider[DicomTags.ContentTemplateSequence];
			if (dicomElement.IsNull || dicomElement.Count == 0)
			{
				DicomSequenceItem dicomSequenceItem = new DicomSequenceItem();
				dicomElement.Values = new DicomSequenceItem[] {dicomSequenceItem};
				ContentTemplateSequence iodBase = new ContentTemplateSequence(dicomSequenceItem);
				iodBase.InitializeAttributes();
				return iodBase;
			}
			return new ContentTemplateSequence(((DicomSequenceItem[]) dicomElement.Values)[0]);
		}
	}
}
