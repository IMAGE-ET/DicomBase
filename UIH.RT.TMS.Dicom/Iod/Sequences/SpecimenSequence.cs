/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: SpecimenSequence.cs
////
//// Summary:
////
////
//// Date: 2014/08/19
//////////////////////////////////////////////////////////////////////////
#region License

// Copyright (c) 2011 - 2013, United-Imaging Inc.
// All rights reserved.
// http://www.united-imaging.com

#endregion

namespace UIH.RT.TMS.Dicom.Iod.Sequences
{
	/// <summary>
	/// Specimen Sequence
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.7.1.2 (Table C.7-2a)</remarks>
	public class SpecimenSequence : SequenceIodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SpecimenSequence"/> class.
		/// </summary>
		public SpecimenSequence() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="SpecimenSequence"/> class.
		/// </summary>
		/// <param name="dicomSequenceItem">The dicom sequence item.</param>
		public SpecimenSequence(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) {}

		/// <summary>
		/// Gets or sets the value of SpecimenIdentifier in the underlying collection. Type 2.
		/// </summary>
		public string SpecimenIdentifier
		{
			get { return base.DicomElementProvider[DicomTags.SpecimenIdentifier].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.SpecimenIdentifier].SetNullValue();
					return;
				}
				base.DicomElementProvider[DicomTags.SpecimenIdentifier].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of SpecimenTypeCodeSequence in the underlying collection. Type 2C.
		/// </summary>
		public SpecimenTypeCodeSequence SpecimenTypeCodeSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.SpecimenTypeCodeSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}
				return new SpecimenTypeCodeSequence(((DicomSequenceItem[]) dicomElement.Values)[0]);
			}
			set
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.SpecimenTypeCodeSequence];
				if (value == null)
				{
					base.DicomElementProvider[DicomTags.SpecimenTypeCodeSequence] = null;
					return;
				}
				dicomElement.Values = new DicomSequenceItem[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Gets or sets the value of SlideIdentifier in the underlying collection. Type 2C.
		/// </summary>
		public string SlideIdentifier
		{
			get { return base.DicomElementProvider[DicomTags.SlideIdentifierRetired].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.SlideIdentifierRetired] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.SlideIdentifierRetired].SetString(0, value);
			}
		}
	}
}
