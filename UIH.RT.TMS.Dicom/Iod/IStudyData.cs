/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: IStudyData.cs
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
	public interface IStudyRootData : IStudyData, IPatientData
	{}

	public interface IStudyData
	{
		/// <summary>
		/// Gets the Study Instance UID of the identified study.
		/// </summary>
		[DicomField(DicomTags.StudyInstanceUid)]
		string StudyInstanceUid { get; }

        /// <summary>
        /// Gets the sop classes in the identified study.
        /// </summary>
        [DicomField(DicomTags.SopClassesInStudy)]
        string[] SopClassesInStudy { get; }
        
        /// <summary>
		/// Gets the modalities in the identified study.
		/// </summary>
		[DicomField(DicomTags.ModalitiesInStudy)]
		string[] ModalitiesInStudy { get; }

		/// <summary>
		/// Gets the study description of the identified study.
		/// </summary>
		[DicomField(DicomTags.StudyDescription)]
		string StudyDescription { get; }

		/// <summary>
		/// Gets the study ID of the identified study.
		/// </summary>
		[DicomField(DicomTags.StudyId)]
		string StudyId { get; }

		/// <summary>
		/// Gets the study date of the identified study.
		/// </summary>
		[DicomField(DicomTags.StudyDate)]
		string StudyDate { get; }

		/// <summary>
		/// Gets the study time of the identified study.
		/// </summary>
		[DicomField(DicomTags.StudyTime)]
		string StudyTime { get; }

		/// <summary>
		/// Gets the accession number of the identified study.
		/// </summary>
		[DicomField(DicomTags.AccessionNumber)]
		string AccessionNumber { get; }

		[DicomField(DicomTags.ReferringPhysiciansName)]
		string ReferringPhysiciansName { get; }

		/// <summary>
		/// Gets the number of series belonging to the identified study.
		/// </summary>
		[DicomField(DicomTags.NumberOfStudyRelatedSeries)]
		int? NumberOfStudyRelatedSeries { get; }

		/// <summary>
		/// Gets the number of composite object instances belonging to the identified study.
		/// </summary>
		[DicomField(DicomTags.NumberOfStudyRelatedInstances)]
		int? NumberOfStudyRelatedInstances { get; }
	}
}
