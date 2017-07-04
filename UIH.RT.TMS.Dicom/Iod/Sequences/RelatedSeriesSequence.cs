/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: RelatedSeriesSequence.cs
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

namespace UIH.RT.TMS.Dicom.Iod.Sequences
{
	/// <summary>
	/// RelatedSeries Sequence
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.7.3.1 (Table C.7-5a)</remarks>
	public class RelatedSeriesSequence : SequenceIodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RelatedSeriesSequence"/> class.
		/// </summary>
		public RelatedSeriesSequence() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="RelatedSeriesSequence"/> class.
		/// </summary>
		/// <param name="dicomSequenceItem">The dicom sequence item.</param>
		public RelatedSeriesSequence(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) {}

		/// <summary>
		/// Gets or sets the value of StudyInstanceUid in the underlying collection. Type 1.
		/// </summary>
		public string StudyInstanceUid
		{
			get { return base.DicomElementProvider[DicomTags.StudyInstanceUid].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("value", "StudyInstanceUid is Type 1 Required.");
				base.DicomElementProvider[DicomTags.StudyInstanceUid].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of SeriesInstanceUid in the underlying collection. Type 1.
		/// </summary>
		public string SeriesInstanceUid
		{
			get { return base.DicomElementProvider[DicomTags.SeriesInstanceUid].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("value", "SeriesInstanceUid is Type 1 Required.");
				base.DicomElementProvider[DicomTags.SeriesInstanceUid].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of PurposeOfReferenceCodeSequence in the underlying collection. Type 2.
		/// </summary>
		public CodeSequenceMacro[] PurposeOfReferenceCodeSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.PurposeOfReferenceCodeSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}

				CodeSequenceMacro[] result = new CodeSequenceMacro[dicomElement.Count];
				DicomSequenceItem[] items = (DicomSequenceItem[]) dicomElement.Values;
				for (int n = 0; n < items.Length; n++)
					result[n] = new CodeSequenceMacro(items[n]);

				return result;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.DicomElementProvider[DicomTags.PurposeOfReferenceCodeSequence].SetNullValue();
					return;
				}

				DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
				for (int n = 0; n < value.Length; n++)
					result[n] = value[n].DicomSequenceItem;

				base.DicomElementProvider[DicomTags.PurposeOfReferenceCodeSequence].Values = result;
			}
		}
	}
}
