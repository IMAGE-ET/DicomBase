/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: PatientQueryIod.cs
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

namespace UIH.RT.TMS.Dicom.Iod.Iods
{
    /// <summary>
    /// IOD for common Patient Query Retrieve items.
    /// </summary>
    public class PatientQueryIod : QueryIodBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PatientQueryIod"/> class.
        /// </summary>
        public PatientQueryIod()
        {
            SetAttributeFromEnum(DicomElementProvider[DicomTags.QueryRetrieveLevel], QueryRetrieveLevel.Patient);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientQueryIod"/> class.
        /// </summary>
		public PatientQueryIod(IDicomElementProvider dicomElementProvider)
            :base(dicomElementProvider)
        {
            SetAttributeFromEnum(DicomElementProvider[DicomTags.QueryRetrieveLevel], QueryRetrieveLevel.Patient);
        }
        #endregion

        #region Public Properties
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
		/// Gets or sets the number of patient related instances.
		/// </summary>
		/// <value>The number of patient related instances.</value>
		public uint NumberOfPatientRelatedInstances
		{
			get { return DicomElementProvider[DicomTags.NumberOfPatientRelatedInstances].GetUInt32(0, 0); }
			set { DicomElementProvider[DicomTags.NumberOfPatientRelatedInstances].SetUInt32(0, value); }
		}

		/// <summary>
		/// Gets or sets the number of patient related series.
		/// </summary>
		/// <value>The number of patient related series.</value>
		public uint NumberOfPatientRelatedSeries
		{
			get { return DicomElementProvider[DicomTags.NumberOfPatientRelatedSeries].GetUInt32(0, 0); }
			set { DicomElementProvider[DicomTags.NumberOfPatientRelatedSeries].SetUInt32(0, value); }
		}

		/// <summary>
		/// Gets or sets the number of patient related studies.
		/// </summary>
		/// <value>The number of patient related studies.</value>
		public uint NumberOfPatientRelatedStudies
		{
			get { return DicomElementProvider[DicomTags.NumberOfPatientRelatedStudies].GetUInt32(0, 0); }
			set { DicomElementProvider[DicomTags.NumberOfPatientRelatedStudies].SetUInt32(0, value); }
		}

        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the common tags for a patient query retrieve request.
        /// </summary>
         public void SetCommonTags()
        {
            SetCommonTags(DicomElementProvider);
        }

        /// <summary>
        /// Sets the common tags for a patient query retrieve request.
        /// </summary>
        public static void SetCommonTags(IDicomElementProvider dicomElementProvider)
        {
			SetAttributeFromEnum(dicomElementProvider[DicomTags.QueryRetrieveLevel], QueryRetrieveLevel.Patient);

			// Always set the Patient 
			dicomElementProvider[DicomTags.PatientsName].SetString(0, "*");
			dicomElementProvider[DicomTags.PatientId].SetNullValue();
			dicomElementProvider[DicomTags.PatientsBirthDate].SetNullValue();
			dicomElementProvider[DicomTags.PatientsBirthTime].SetNullValue();
			dicomElementProvider[DicomTags.PatientsSex].SetNullValue();
			dicomElementProvider[DicomTags.NumberOfPatientRelatedStudies].SetNullValue();
			dicomElementProvider[DicomTags.NumberOfPatientRelatedSeries].SetNullValue();
			dicomElementProvider[DicomTags.NumberOfPatientRelatedInstances].SetNullValue();
		}
        #endregion
    }

}
