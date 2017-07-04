/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ImageAcquisitionResultsModuleIod.cs
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
using UIH.RT.TMS.Dicom.Iod.Macros;
using UIH.RT.TMS.Dicom.Iod.Sequences;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// As per Dicom DOC 3 C.4.15 (pg 256)
	/// </summary>
	public class ImageAcquisitionResultsModuleIod : IodBase
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ImageAcquisitionResultsModuleIod"/> class.
		/// </summary>
		public ImageAcquisitionResultsModuleIod() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="ImageAcquisitionResultsModuleIod"/> class.
		/// </summary>
		public ImageAcquisitionResultsModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider) { }

		#endregion

		#region Public Properties

		public Modality Modality
		{
			get { return ParseEnum<Modality>(base.DicomElementProvider[DicomTags.Modality].GetString(0, String.Empty), Modality.None); }
			set { SetAttributeFromEnum(base.DicomElementProvider[DicomTags.Modality], value); }
		}

		public string StudyId
		{
			get { return base.DicomElementProvider[DicomTags.StudyId].GetString(0, String.Empty); }
			set { base.DicomElementProvider[DicomTags.StudyId].SetString(0, value); }
		}

		/// <summary>
		/// Gets the performed protocol code sequence list.
		/// Sequence describing the Protocol performed for this Procedure Step. This sequence 
		/// may have zero or more Items.
		/// </summary>
		/// <value>The performed protocol code sequence list.</value>
		public SequenceIodList<CodeSequenceMacro> PerformedProtocolCodeSequenceList
		{
			get { return new SequenceIodList<CodeSequenceMacro>(base.DicomElementProvider[DicomTags.PerformedProtocolCodeSequence] as DicomElementSq); }
		}

		/// <summary>
		/// Gets the protocol context sequence list.
		/// Sequence that specifies the context for the Performed Protocol Code Sequence Item. 
		/// One or more items may be included in this sequence. See Section C.4.10.1.
		/// </summary>
		/// <value>The protocol context sequence list.</value>
		public SequenceIodList<ContentItemMacro> ProtocolContextSequenceList
		{
			get { return new SequenceIodList<ContentItemMacro>(base.DicomElementProvider[DicomTags.ProtocolContextSequence] as DicomElementSq); }
		}

		/// <summary>
		/// Sequence that specifies modifiers for a Protocol Context Content Item. One or 
		/// more items may be included in this sequence. See Section C.4.10.1.
		/// </summary>
		/// <value>The content item modifier sequence list.</value>
		public SequenceIodList<ContentItemMacro> ContentItemModifierSequenceList
		{
			get { return new SequenceIodList<ContentItemMacro>(base.DicomElementProvider[DicomTags.ContentItemModifierSequence] as DicomElementSq); }
		}

		public SequenceIodList<PerformedSeriesSequenceIod> PerformedSeriesSequenceList
		{
			get { return new SequenceIodList<PerformedSeriesSequenceIod>(base.DicomElementProvider[DicomTags.PerformedSeriesSequence] as DicomElementSq); }
		}

		#endregion
	}
}
