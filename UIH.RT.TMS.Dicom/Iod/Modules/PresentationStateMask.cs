/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: PresentationStateMask.cs
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

using System.Collections.Generic;
using UIH.RT.TMS.Dicom.Iod.Sequences;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// PresentationStateMask Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2011, Part 3, Section C.11.13 (Table C.11.13-1)</remarks>
	public class PresentationStateMaskModuleIod : IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PresentationStateMaskModuleIod"/> class.
		/// </summary>	
		public PresentationStateMaskModuleIod() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="PresentationStateMaskModuleIod"/> class.
		/// </summary>
		public PresentationStateMaskModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider) {}

		/// <summary>
		/// Gets or sets the value of MaskSubtractionSequence in the underlying collection. Type 1C.
		/// </summary>
		public MaskSubtractionSequenceIod MaskSubtractionSequence
		{
			get
			{
				var dicomAttribute = DicomElementProvider[DicomTags.MaskSubtractionSequence];
				if (dicomAttribute.IsNull || dicomAttribute.IsEmpty)
					return null;
				return new MaskSubtractionSequenceIod(((DicomSequenceItem[]) dicomAttribute.Values)[0]);
			}
			set
			{
				if (value == null)
				{
					DicomElementProvider[DicomTags.MaskSubtractionSequence] = null;
					return;
				}

				var dicomAttribute = DicomElementProvider[DicomTags.MaskSubtractionSequence];
				dicomAttribute.Values = new[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Creates the MaskSubtractionSequence in the underlying collection. Type 1C.
		/// </summary>
		public MaskSubtractionSequenceIod CreateMaskSubtractionSequence()
		{
			var dicomAttribute = DicomElementProvider[DicomTags.MaskSubtractionSequence];
			if (dicomAttribute.IsNull || dicomAttribute.IsEmpty)
			{
				var dicomSequenceItem = new DicomSequenceItem();
				dicomAttribute.Values = new[] {dicomSequenceItem};
				var sequenceType = new MaskSubtractionSequenceIod(dicomSequenceItem);
				sequenceType.InitializeAttributes();
				return sequenceType;
			}
			return new MaskSubtractionSequenceIod(((DicomSequenceItem[]) dicomAttribute.Values)[0]);
		}

		/// <summary>
		/// Gets or sets the value of RecommendedViewingMode in the underlying collection. Type 1C.
		/// </summary>
		public RecommendedViewingMode RecommendedViewingMode
		{
			get { return ParseEnum(DicomElementProvider[DicomTags.RecommendedViewingMode].GetString(0, string.Empty), RecommendedViewingMode.None); }
			set
			{
				if (value == RecommendedViewingMode.None)
				{
					DicomElementProvider[DicomTags.RecommendedViewingMode] = null;
					return;
				}
				SetAttributeFromEnum(DicomElementProvider[DicomTags.RecommendedViewingMode], value);
			}
		}

		public void InitializeAttributes()
		{
			MaskSubtractionSequence = null;
			RecommendedViewingMode = RecommendedViewingMode.None;
		}

		/// <summary>
		/// Gets an enumeration of <see cref="DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags
		{
			get
			{
				yield return DicomTags.MaskSubtractionSequence;
				yield return DicomTags.RecommendedViewingMode;
			}
		}
	}
}
