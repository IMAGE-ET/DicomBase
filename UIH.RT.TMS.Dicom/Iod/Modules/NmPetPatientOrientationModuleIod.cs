/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: NmPetPatientOrientationModuleIod.cs
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
using UIH.RT.TMS.Dicom.Iod.Macros;
using UIH.RT.TMS.Dicom.Iod.Sequences;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// NM/PET Patient Orientation Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2011, Part 3, Section C.8.4.6 (Table C.8-5)</remarks>
	public class NmPetPatientOrientationModuleIod : IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NmPetPatientOrientationModuleIod"/> class.
		/// </summary>	
		public NmPetPatientOrientationModuleIod() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="NmPetPatientOrientationModuleIod"/> class.
		/// </summary>
		/// <param name="dicomElementProvider">The DICOM attribute collection.</param>
		public NmPetPatientOrientationModuleIod(IDicomElementProvider dicomElementProvider)
			: base(dicomElementProvider) {}

		/// <summary>
		/// Gets an enumeration of <see cref="DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags
		{
			get
			{
				yield return DicomTags.PatientOrientationCodeSequence;
				yield return DicomTags.PatientGantryRelationshipCodeSequence;
			}
		}

		/// <summary>
		/// Initializes the underlying collection to implement the module or sequence using default values.
		/// </summary>
		public void InitializeAttributes()
		{
			PatientOrientationCodeSequence = null;
			PatientGantryRelationshipCodeSequence = null;
		}

		/// <summary>
		/// Checks if this module appears to be non-empty.
		/// </summary>
		/// <returns>True if the module appears to be non-empty; False otherwise.</returns>
		public bool HasValues()
		{
			if (PatientOrientationCodeSequence == null
			    && PatientGantryRelationshipCodeSequence == null)
				return false;
			return true;
		}

		/// <summary>
		/// Gets or sets the value of PatientOrientationCodeSequence in the underlying collection. Type 2.
		/// </summary>
		public PatientOrientationCodeSequence PatientOrientationCodeSequence
		{
			get
			{
				var dicomAttribute = DicomElementProvider[DicomTags.PatientOrientationCodeSequence];
				if (dicomAttribute.IsNull || dicomAttribute.IsEmpty)
				{
					return null;
				}
				return new PatientOrientationCodeSequence(((DicomSequenceItem[]) dicomAttribute.Values)[0]);
			}
			set
			{
				var dicomAttribute = DicomElementProvider[DicomTags.PatientOrientationCodeSequence];
				if (value == null)
				{
					dicomAttribute.SetNullValue();
					return;
				}
				dicomAttribute.Values = new[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Creates the PatientOrientationCodeSequence in the underlying collection. Type 2.
		/// </summary>
		public PatientOrientationCodeSequence CreatePatientOrientationCodeSequence()
		{
			var dicomAttribute = DicomElementProvider[DicomTags.PatientOrientationCodeSequence];
			if (dicomAttribute.IsNull || dicomAttribute.IsEmpty)
			{
				var dicomSequenceItem = new DicomSequenceItem();
				dicomAttribute.Values = new[] {dicomSequenceItem};
				var sequenceType = new PatientOrientationCodeSequence(dicomSequenceItem);
				return sequenceType;
			}
			return new PatientOrientationCodeSequence(((DicomSequenceItem[]) dicomAttribute.Values)[0]);
		}

		/// <summary>
		/// Gets or sets the value of PatientGantryRelationshipCodeSequence in the underlying collection. Type 2.
		/// </summary>
		public CodeSequenceMacro PatientGantryRelationshipCodeSequence
		{
			get
			{
				var dicomAttribute = DicomElementProvider[DicomTags.PatientGantryRelationshipCodeSequence];
				if (dicomAttribute.IsNull || dicomAttribute.IsEmpty)
				{
					return null;
				}
				return new CodeSequenceMacro(((DicomSequenceItem[]) dicomAttribute.Values)[0]);
			}
			set
			{
				var dicomAttribute = DicomElementProvider[DicomTags.PatientGantryRelationshipCodeSequence];
				if (value == null)
				{
					dicomAttribute.SetNullValue();
					return;
				}
				dicomAttribute.Values = new[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Creates the PatientGantryRelationshipCodeSequence in the underlying collection. Type 2.
		/// </summary>
		public CodeSequenceMacro CreatePatientGantryRelationshipCodeSequence()
		{
			var dicomAttribute = DicomElementProvider[DicomTags.PatientGantryRelationshipCodeSequence];
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
