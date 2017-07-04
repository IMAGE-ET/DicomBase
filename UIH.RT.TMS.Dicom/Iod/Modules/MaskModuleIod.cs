/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: MaskModuleIod.cs
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
using System.Collections.Generic;
using UIH.RT.TMS.Dicom.Iod.Sequences;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// Mask Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2011, Part 3, Section C.7.6.10 (Table C.7-16)</remarks>
	public class MaskModuleIod : IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MaskModuleIod"/> class.
		/// </summary>	
		public MaskModuleIod() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="MaskModuleIod"/> class.
		/// </summary>
		public MaskModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider) {}

		/// <summary>
		/// Gets or sets the value of MaskSubtractionSequence in the underlying collection. Type 1.
		/// </summary>
		public MaskSubtractionSequenceIod[] MaskSubtractionSequence
		{
			get
			{
				var dicomAttribute = DicomElementProvider[DicomTags.MaskSubtractionSequence];
				if (dicomAttribute.IsNull || dicomAttribute.IsEmpty)
					return null;

				var result = new MaskSubtractionSequenceIod[dicomAttribute.Count];
				var items = (DicomSequenceItem[]) dicomAttribute.Values;
				for (int n = 0; n < items.Length; n++)
					result[n] = new MaskSubtractionSequenceIod(items[n]);

				return result;
			}
			set
			{
				if (value == null || value.Length == 0)
					throw new ArgumentNullException("value", "MaskSubtractionSequence is Type 1 Required.");

				var result = new DicomSequenceItem[value.Length];
				for (int n = 0; n < value.Length; n++)
					result[n] = value[n].DicomSequenceItem;

				DicomElementProvider[DicomTags.MaskSubtractionSequence].Values = result;
			}
		}

		/// <summary>
		/// Creates a single instance of a MaskSubtractionSequence item. Does not modify the MaskSubtractionSequence in the underlying collection.
		/// </summary>
		public MaskSubtractionSequenceIod CreateMaskSubtractionSequence()
		{
			var iodBase = new MaskSubtractionSequenceIod(new DicomSequenceItem());
			iodBase.InitializeAttributes();
			return iodBase;
		}

		/// <summary>
		/// Gets or sets the value of RecommendedViewingMode in the underlying collection. Type 2.
		/// </summary>
		public RecommendedViewingMode RecommendedViewingMode
		{
			get { return ParseEnum(DicomElementProvider[DicomTags.RecommendedViewingMode].GetString(0, string.Empty), RecommendedViewingMode.None); }
			set
			{
				if (value == RecommendedViewingMode.None)
				{
					DicomElementProvider[DicomTags.RecommendedViewingMode].SetNullValue();
					return;
				}
				SetAttributeFromEnum(DicomElementProvider[DicomTags.RecommendedViewingMode], value);
			}
		}

		public void InitializeAttributes()
		{
			MaskSubtractionSequence = new[] {CreateMaskSubtractionSequence()};
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

	/// <summary>
	/// Enumerated values for the <see cref="DicomTags.RecommendedViewingMode"/> attribute specifying the recommended viewing protocol(s).
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2011, Part 3, Section C.7.6.10 (Table C.7-16)</remarks>
	public enum RecommendedViewingMode
	{
		/// <summary>
		/// Represents the empty viewing mode, which is equivalent to the null value.
		/// </summary>
		None,

		/// <summary>
		/// Represents viewing subtraction of image with mask.
		/// </summary>
		Sub,

		/// <summary>
		/// Represents native viewing of image as sent.
		/// </summary>
		Nat
	}
}
