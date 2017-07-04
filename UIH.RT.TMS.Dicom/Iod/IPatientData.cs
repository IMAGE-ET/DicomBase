/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: IPatientData.cs
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

namespace UIH.RT.TMS.Dicom.Iod
{
	//internal for now b/c the patient root query stuff is
    public interface IPatientRootData : IPatientData
	{
		[DicomField(DicomTags.NumberOfPatientRelatedStudies)]
		int? NumberOfPatientRelatedStudies { get; }

		[DicomField(DicomTags.NumberOfPatientRelatedSeries)]
		int? NumberOfPatientRelatedSeries { get; }

		[DicomField(DicomTags.NumberOfPatientRelatedInstances)]
		int? NumberOfPatientRelatedInstances { get; }
	}

	public interface IPatientData
	{
		[DicomField(DicomTags.PatientId)]
		string PatientId { get; }

		[DicomField(DicomTags.PatientsName)]
		string PatientsName { get; }

		[DicomField(DicomTags.PatientsBirthDate)]
		string PatientsBirthDate { get; }

		[DicomField(DicomTags.PatientsBirthTime)]
		string PatientsBirthTime { get; }

		[DicomField(DicomTags.PatientsSex)]
		string PatientsSex { get; }

		#region Species

		[DicomField(DicomTags.PatientSpeciesDescription)]
		string PatientSpeciesDescription { get; }

		[DicomField(DicomTags.CodingSchemeDesignator, DicomTags.PatientSpeciesCodeSequence)]
		string PatientSpeciesCodeSequenceCodingSchemeDesignator { get; }

		[DicomField(DicomTags.CodeValue, DicomTags.PatientSpeciesCodeSequence)]
		string PatientSpeciesCodeSequenceCodeValue { get; }

		[DicomField(DicomTags.CodeMeaning, DicomTags.PatientSpeciesCodeSequence)]
		string PatientSpeciesCodeSequenceCodeMeaning { get; }

		#endregion

		#region Breed

		[DicomField(DicomTags.PatientBreedDescription)]
		string PatientBreedDescription { get; }

		[DicomField(DicomTags.CodingSchemeDesignator, DicomTags.PatientBreedCodeSequence)]
		string PatientBreedCodeSequenceCodingSchemeDesignator { get; }

		[DicomField(DicomTags.CodeValue, DicomTags.PatientBreedCodeSequence)]
		string PatientBreedCodeSequenceCodeValue { get; }

		[DicomField(DicomTags.CodeMeaning, DicomTags.PatientBreedCodeSequence)]
		string PatientBreedCodeSequenceCodeMeaning { get; }

		#endregion

		#region Responsible Person/Organization

		[DicomField(DicomTags.ResponsiblePerson)]
		string ResponsiblePerson { get; }

		[DicomField(DicomTags.ResponsiblePersonRole)]
		string ResponsiblePersonRole { get; }

		[DicomField(DicomTags.ResponsibleOrganization)]
		string ResponsibleOrganization { get; }

		#endregion
	}
}
