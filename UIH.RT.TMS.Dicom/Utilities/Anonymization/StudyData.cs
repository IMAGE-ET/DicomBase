/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: StudyData.cs
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

using System;
using UIH.RT.TMS.Common.Utilities;
using UIH.RT.TMS.Dicom.Iod;

namespace UIH.RT.TMS.Dicom.Utilities.Anonymization
{
	/// <summary>
	/// A class containing commonly anonymized dicom study attributes.
	/// </summary>
	[Cloneable(true)]
	public class StudyData
	{
		private string _patientId = "";
		private string _patientsNameRaw = "";
		private string _patientsBirthDateRaw = "";
		private string _patientsSex = "";
		private string _accessionNumber = "";
		private string _studyInstanceUid = "";
		private string _studyDescription = "";
		private string _studyId = "";
		private string _studyDateRaw = "";

		/// <summary>
		/// Constructor.
		/// </summary>
		public StudyData()
		{
		}

		/// <summary>
		/// Gets or sets the patient's name.
		/// </summary>
		public PersonName PatientsName
		{
			get { return new PersonName(PatientsNameRaw); }	
			set
			{
				string p = null;
				if (value != null)
					p = value.ToString();

				PatientsNameRaw = p ?? "";
			}
		}

		/// <summary>
		/// Gets or sets the study date.
		/// </summary>
		public DateTime? StudyDate
		{
			get
			{
				return DateParser.Parse(StudyDateRaw);
			}
			set
			{
				if (value == null)
					StudyDateRaw = "";
				else
					StudyDateRaw = value.Value.ToString(DateParser.DicomDateFormat) ?? "";
			}
		}

		/// <summary>
		/// Gets or sets the patient's birth date.
		/// </summary>
		public DateTime? PatientsBirthDate
		{
			get
			{
				return DateParser.Parse(PatientsBirthDateRaw);
			}
			set
			{
				if (value == null)
					PatientsBirthDateRaw = "";
				else
					PatientsBirthDateRaw = value.Value.ToString(DateParser.DicomDateFormat) ?? "";
			}
		}

		/// <summary>
		/// Gets or sets the patient id.
		/// </summary>
		[DicomField(DicomTags.PatientId)]
		public string PatientId
		{
			get { return _patientId; }
			set { _patientId = value ?? ""; }
		}

		/// <summary>
		/// Gets or sets the patients name, as a raw string.
		/// </summary>
		[DicomField(DicomTags.PatientsName)]
		public string PatientsNameRaw
		{
			get { return _patientsNameRaw; }
			set { _patientsNameRaw = value ?? ""; }
		}

		/// <summary>
		/// Gets or sets the patient's birth date, as a raw string.
		/// </summary>
		[DicomField(DicomTags.PatientsBirthDate)]
		public string PatientsBirthDateRaw
		{
			get { return _patientsBirthDateRaw; }
			set { _patientsBirthDateRaw = value ?? ""; }
		}

		/// <summary>
		/// Gets or sets the patient's sex.
		/// </summary>
		[DicomField(DicomTags.PatientsSex)]
		public string PatientsSex
		{
			get { return _patientsSex; }
			set { _patientsSex = value ?? ""; }
		}

		/// <summary>
		/// Gets or sets the accession number.
		/// </summary>
		[DicomField(DicomTags.AccessionNumber)]
		public string AccessionNumber
		{
			get { return _accessionNumber; }
			set { _accessionNumber = value ?? ""; }
		}

		/// <summary>
		/// Gets or sets the study description.
		/// </summary>
		[DicomField(DicomTags.StudyDescription)]
		public string StudyDescription
		{
			get { return _studyDescription; }
			set { _studyDescription = value ?? ""; }
		}

		/// <summary>
		/// Gets or sets the study id.
		/// </summary>
		[DicomField(DicomTags.StudyId)]
		public string StudyId
		{
			get { return _studyId; }
			set { _studyId = value ?? ""; }
		}

		/// <summary>
		/// Gets or sets the study date, as a raw string.
		/// </summary>
		[DicomField(DicomTags.StudyDate)]
		public string StudyDateRaw
		{
			get { return _studyDateRaw; }
			set { _studyDateRaw = value ?? ""; }
		}

		internal string StudyInstanceUid
		{
			get { return _studyInstanceUid; }
			set { _studyInstanceUid = value ?? ""; }
		}

		internal void LoadFrom(DicomFile file)
		{
			file.DataSet.LoadDicomFields(this);
			this.StudyInstanceUid = file.DataSet[DicomTags.StudyInstanceUid];
		}

		internal void SaveTo(DicomFile file)
		{
			file.DataSet.SaveDicomFields(this);
			file.DataSet[DicomTags.StudyInstanceUid].SetStringValue(this.StudyInstanceUid);
		}

		/// <summary>
		/// Creates a deep clone of this instance.
		/// </summary>
		public StudyData Clone()
		{
			return CloneBuilder.Clone(this) as StudyData;
		}
	}
}
