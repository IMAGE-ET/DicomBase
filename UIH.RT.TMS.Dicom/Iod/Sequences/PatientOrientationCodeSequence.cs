/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: PatientOrientationCodeSequence.cs
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

using UIH.RT.TMS.Dicom.Iod.Macros;

namespace UIH.RT.TMS.Dicom.Iod.Sequences
{
	/// <summary>
	/// PatientOrientation Code Sequence
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2011, Part 3, Section C.8.4.6 (Table C.8-5)</remarks>
	public class PatientOrientationCodeSequence : CodeSequenceMacro
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PatientOrientationCodeSequence"/> class.
		/// </summary>
		public PatientOrientationCodeSequence() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="PatientOrientationCodeSequence"/> class.
		/// </summary>
		/// <param name="dicomSequenceItem">The DICOM sequence item.</param>
		public PatientOrientationCodeSequence(DicomSequenceItem dicomSequenceItem)
			: base(dicomSequenceItem) {}

		/// <summary>
		/// Gets or sets the value of PatientOrientationModifierCodeSequence in the underlying collection. Type 2C.
		/// </summary>
		public CodeSequenceMacro PatientOrientationModifierCodeSequence
		{
			get
			{
				var dicomAttribute = DicomElementProvider[DicomTags.PatientOrientationModifierCodeSequence];
				if (dicomAttribute.IsNull || dicomAttribute.IsEmpty)
				{
					return null;
				}
				return new CodeSequenceMacro(((DicomSequenceItem[]) dicomAttribute.Values)[0]);
			}
			set
			{
				var dicomAttribute = DicomElementProvider[DicomTags.PatientOrientationModifierCodeSequence];
				if (value == null)
				{
					DicomElementProvider[DicomTags.PatientOrientationModifierCodeSequence] = null;
					return;
				}
				dicomAttribute.Values = new[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Creates the PatientOrientationModifierCodeSequence in the underlying collection. Type 2C.
		/// </summary>
		public CodeSequenceMacro CreatePatientOrientationModifierCodeSequence()
		{
			var dicomAttribute = DicomElementProvider[DicomTags.PatientOrientationModifierCodeSequence];
			if (dicomAttribute.IsNull || dicomAttribute.IsEmpty)
			{
				var dicomSequenceItem = new DicomSequenceItem();
				dicomAttribute.Values = new[] {dicomSequenceItem};
				var sequenceType = new CodeSequenceMacro(dicomSequenceItem);
				return sequenceType;
			}
			return new CodeSequenceMacro(((DicomSequenceItem[]) dicomAttribute.Values)[0]);
		}
	}
}
