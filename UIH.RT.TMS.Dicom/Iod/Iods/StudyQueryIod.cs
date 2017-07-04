/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: StudyQueryIod.cs
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
using UIH.RT.TMS.Dicom.Utilities;
using UIH.RT.TMS.Common;

namespace UIH.RT.TMS.Dicom.Iod.Iods
{
    /// <summary>
    /// IOD for common Query Retrieve items.  This is a replacement for the <see cref="UIH.RT.TMS.Dicom.QueryResult"/>
    /// </summary>
    public class StudyQueryIod : QueryIodBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="StudyQueryIod"/> class.
        /// </summary>
        public StudyQueryIod()
            :base()
        {
            SetAttributeFromEnum(DicomElementProvider[DicomTags.QueryRetrieveLevel], QueryRetrieveLevel.Study);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StudyQueryIod"/> class.
        /// </summary>
		public StudyQueryIod(IDicomElementProvider dicomElementProvider)
			: base(dicomElementProvider)
        {
            SetAttributeFromEnum(DicomElementProvider[DicomTags.QueryRetrieveLevel], QueryRetrieveLevel.Study);
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the study instance uid.
        /// </summary>
        /// <value>The study instance uid.</value>
        public string StudyInstanceUid
        {
            get { return DicomElementProvider[DicomTags.StudyInstanceUid].GetString(0, String.Empty); }
            set { DicomElementProvider[DicomTags.StudyInstanceUid].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the patient id.
        /// </summary>
        /// <value>The patient id.</value>
        public string PatientId
        {
            get { return DicomElementProvider[DicomTags.PatientId].GetString(0, String.Empty); }
            set { DicomElementProvider[DicomTags.PatientId].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the name of the patient.
        /// </summary>
        /// <value>The name of the patients.</value>
        public PersonName PatientsName
        {
            get { return new PersonName(DicomElementProvider[DicomTags.PatientsName].GetString(0, String.Empty)); }
            set { DicomElementProvider[DicomTags.PatientsName].SetString(0, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the patients birth date.
        /// </summary>
        /// <value>The patients birth date.</value>
        public DateTime PatientsBirthDate
        {
            get { return DicomElementProvider[DicomTags.PatientsBirthDate].GetDateTime(0, DateTime.MinValue); }
            set { DicomElementProvider[DicomTags.PatientsBirthDate].SetDateTime(0, value); }
        }

        /// <summary>
        /// Gets or sets the patients sex.
        /// </summary>
        /// <value>The patients sex.</value>
        public string PatientsSex
        {
            get { return DicomElementProvider[DicomTags.PatientsSex].GetString(0, String.Empty); }
            set { DicomElementProvider[DicomTags.PatientsSex].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the modalities in study.
        /// </summary>
        /// <value>The modalities in study.</value>
        public string ModalitiesInStudy
        {
            get { return DicomElementProvider[DicomTags.ModalitiesInStudy].GetString(0, String.Empty); }
            set { DicomElementProvider[DicomTags.ModalitiesInStudy].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the study description.
        /// </summary>
        /// <value>The study description.</value>
        public string StudyDescription
        {
            get { return DicomElementProvider[DicomTags.StudyDescription].GetString(0, String.Empty); }
            set { DicomElementProvider[DicomTags.StudyDescription].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the study id.
        /// </summary>
        /// <value>The study id.</value>
        public string StudyId
        {
            get { return DicomElementProvider[DicomTags.StudyId].GetString(0, String.Empty); }
            set { DicomElementProvider[DicomTags.StudyId].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the study date.
        /// </summary>
        /// <value>The study date.</value>
        public DateTime? StudyDate
        {
            get { return DateTimeParser.ParseDateAndTime(String.Empty, 
                    DicomElementProvider[DicomTags.StudyDate].GetString(0, String.Empty), 
                    DicomElementProvider[DicomTags.StudyTime].GetString(0, String.Empty)); }

            set { DateTimeParser.SetDateTimeAttributeValues(value, DicomElementProvider[DicomTags.StudyDate], DicomElementProvider[DicomTags.StudyTime]); }
        }

        /// <summary>
        /// Gets or sets the accession number.
        /// </summary>
        /// <value>The accession number.</value>
        public string AccessionNumber
        {
            get { return DicomElementProvider[DicomTags.AccessionNumber].GetString(0, String.Empty); }
            set { DicomElementProvider[DicomTags.AccessionNumber].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the number of study related instances.
        /// </summary>
        /// <value>The number of study related instances.</value>
        public uint NumberOfStudyRelatedInstances
        {
            get { return DicomElementProvider[DicomTags.NumberOfStudyRelatedInstances].GetUInt32(0, 0); }
            set { DicomElementProvider[DicomTags.NumberOfStudyRelatedInstances].SetUInt32(0, value); }
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the common tags for a query retrieve request.
        /// </summary>
        public void SetCommonTags()
        {
            SetCommonTags(DicomElementProvider);
        }

        public static void SetCommonTags(IDicomElementProvider dicomElementProvider)
        {
			Platform.CheckForNullReference(dicomElementProvider, "dicomElementProvider");

			PatientQueryIod.SetCommonTags(dicomElementProvider);

			SetAttributeFromEnum(dicomElementProvider[DicomTags.QueryRetrieveLevel], QueryRetrieveLevel.Study);

			dicomElementProvider[DicomTags.StudyInstanceUid].SetNullValue();
			dicomElementProvider[DicomTags.StudyId].SetNullValue();
			dicomElementProvider[DicomTags.StudyDate].SetNullValue();
			dicomElementProvider[DicomTags.StudyTime].SetNullValue();
			dicomElementProvider[DicomTags.StudyDescription].SetNullValue();
			dicomElementProvider[DicomTags.AccessionNumber].SetNullValue();
			dicomElementProvider[DicomTags.NumberOfStudyRelatedInstances].SetNullValue();
			dicomElementProvider[DicomTags.NumberOfStudyRelatedSeries].SetNullValue();
			dicomElementProvider[DicomTags.ModalitiesInStudy].SetNullValue();
			dicomElementProvider[DicomTags.RequestingPhysician].SetNullValue();
			dicomElementProvider[DicomTags.ReferringPhysiciansName].SetNullValue();
        }
        #endregion
    }

}
