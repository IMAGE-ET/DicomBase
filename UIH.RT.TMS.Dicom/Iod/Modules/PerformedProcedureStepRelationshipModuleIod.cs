/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: PerformedProcedureStepRelationshipModuleIod.cs
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
using UIH.RT.TMS.Dicom.Iod.Sequences;
using UIH.RT.TMS.Dicom.Utilities;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
    /// <summary>
    /// Patient Identification Module, as per Part 3, C.4.13
    /// </summary>
    public class PerformedProcedureStepRelationshipModuleIod : IodBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PerformedProcedureStepRelationshipModuleIod"/> class.
        /// </summary>
        public PerformedProcedureStepRelationshipModuleIod()
            :base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PerformedProcedureStepRelationshipModuleIod"/> class.
        /// </summary>
		public PerformedProcedureStepRelationshipModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider)
        {
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the name of the patients.
        /// </summary>
        /// <value>The name of the patients.</value>
        public PersonName PatientsName
        {
            get { return new PersonName(base.DicomElementProvider[DicomTags.PatientsName].GetString(0, String.Empty)); }
            set { base.DicomElementProvider[DicomTags.PatientsName].SetString(0, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the patient id.
        /// </summary>
        /// <value>The patient id.</value>
        public string PatientId
        {
            get { return base.DicomElementProvider[DicomTags.PatientId].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.PatientId].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the issuer of patient id.
        /// </summary>
        /// <value>The issuer of patient id.</value>
        public string IssuerOfPatientId
        {
            get { return base.DicomElementProvider[DicomTags.IssuerOfPatientId].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.IssuerOfPatientId].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the patients birth date (only, no time).
        /// </summary>
        /// <value>The patients birth date.</value>
        public DateTime? PatientsBirthDate
        {
        	get { return DateTimeParser.ParseDateAndTime(base.DicomElementProvider, 0, DicomTags.PatientsBirthDate, 0);  }
        
            set { DateTimeParser.SetDateTimeAttributeValues(value, base.DicomElementProvider, 0, DicomTags.PatientsBirthDate, 0); }
        }

        /// <summary>
        /// Gets or sets the patients sex.
        /// </summary>
        /// <value>The patients sex.</value>
        public PatientsSex PatientsSex
        {
            get { return IodBase.ParseEnum<PatientsSex>(base.DicomElementProvider[DicomTags.PatientsSex].GetString(0, String.Empty), PatientsSex.None); }
            set { IodBase.SetAttributeFromEnum(base.DicomElementProvider[DicomTags.PatientsSex], value); }
        }

        /// <summary>
        /// Gets the referenced patient sequence list.
        /// </summary>
        /// <value>The referenced patient sequence list.</value>
        public SequenceIodList<ReferencedInstanceSequenceIod> ReferencedPatientSequenceList
        {
            get
            {
                return new SequenceIodList<ReferencedInstanceSequenceIod>(base.DicomElementProvider[DicomTags.ReferencedPatientSequence] as DicomElementSq);
            }
        }

        /// <summary>
        /// Gets the scheduled step attributes sequence list.
        /// </summary>
        /// <value>The scheduled step attributes sequence list.</value>
        public SequenceIodList<ScheduledStepAttributesSequenceIod> ScheduledStepAttributesSequenceList
        {
            get
            {
                return new SequenceIodList<ScheduledStepAttributesSequenceIod>(base.DicomElementProvider[DicomTags.ScheduledStepAttributesSequence] as DicomElementSq);
            }
        }

        #endregion

    }
    
}
