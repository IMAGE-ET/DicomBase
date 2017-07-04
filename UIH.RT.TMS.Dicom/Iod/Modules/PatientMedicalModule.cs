/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: PatientMedicalModule.cs
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

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
    /// <summary>
    /// Patient Medical Module, as per Part 3, C.2.4
    /// </summary>
    public class PatientMedicalModule : IodBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PatientMedicalModule"/> class.
        /// </summary>
        public PatientMedicalModule()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientMedicalModule"/> class.
        /// </summary>
        /// <param name="dicomDataset">The dicom attribute collection.</param>
        public PatientMedicalModule(DicomDataset dicomDataset)
            : base(dicomDataset)
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the medical alerts.
        /// </summary>
        /// <value>The medical alerts.</value>
        public string MedicalAlerts
        {
            get { return base.DicomElementProvider[DicomTags.MedicalAlerts].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.MedicalAlerts].SetString(0, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the patient allergies
        /// </summary>
        /// <value>The allergies.</value>
        public string Allergies
        {
            get { return base.DicomElementProvider[DicomTags.Allergies].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.Allergies].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the smoking status.
        /// </summary>
        /// <value>The smoking status.</value>
        public SmokingStatus SmokingStatus
        {
            get { return IodBase.ParseEnum<SmokingStatus>(base.DicomElementProvider[DicomTags.SmokingStatus].GetString(0, String.Empty), SmokingStatus.Unknown); }
            set { IodBase.SetAttributeFromEnum(base.DicomElementProvider[DicomTags.SmokingStatus], value); }
        }

        /// <summary>
        /// Gets or sets the additional patient history.
        /// </summary>
        /// <value>The additional payment history.</value>
        public string AdditionalPatientHistory
        {
            get { return base.DicomElementProvider[DicomTags.AdditionalPatientHistory].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.AdditionalPatientHistory].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the responsible person role.
        /// </summary>
        /// <value>The responsible person role.</value>
        public PregnancyStatus PregnancyStatus
        {
            get { return IodBase.ParseEnum<PregnancyStatus>(base.DicomElementProvider[DicomTags.PregnancyStatus].GetString(0, String.Empty), PregnancyStatus.Unknown); }
            set { IodBase.SetAttributeFromEnum(base.DicomElementProvider[DicomTags.PregnancyStatus], value); }
        }

        /// <summary>
        /// Gets or sets the patients last Menstrual date (if applicable).
        /// </summary>
        /// <value>The patients last Menstrual date.</value>
        public DateTime? LastMenstrualDate
        {
            get
            {
                return DateTimeParser.ParseDateAndTime(String.Empty,
                          base.DicomElementProvider[DicomTags.LastMenstrualDate].GetString(0, String.Empty),
                base.DicomElementProvider[DicomTags.LastMenstrualDate].GetString(0, String.Empty));
            }

            set { DateTimeParser.SetDateTimeAttributeValues(value, base.DicomElementProvider[DicomTags.LastMenstrualDate], base.DicomElementProvider[DicomTags.LastMenstrualDate]); }
        }

        /// <summary>
        /// Gets or sets the Patient's Sex Neutered value.
        /// </summary>
        /// <value>The Patient's Sex Neutered value</value>
        public PatientsSexNeutered PatientsSexNeutered
        {
            get { return IodBase.ParseEnum<PatientsSexNeutered>(base.DicomElementProvider[DicomTags.PatientsSexNeutered].GetString(0, String.Empty), PatientsSexNeutered.Unaltered); }
            set { IodBase.SetAttributeFromEnum(base.DicomElementProvider[DicomTags.PatientsSexNeutered], value); }
        }

        /// <summary>
        /// Gets or sets the special needs field.
        /// </summary>
        /// <value>Special Needs.</value>
        public string SpecialNeeds
        {
            get { return base.DicomElementProvider[DicomTags.SpecialNeeds].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.SpecialNeeds].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the patient's state.
        /// </summary>
        /// <value>Patient's State.</value>
        public string PatientState
        {
            get { return base.DicomElementProvider[DicomTags.PatientState].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.PatientState].SetString(0, value); }
        }
 
        //TODO: Patient's Pertinent Documents Sequence
        //TODO: Purposes of Code Reference Sequence

        /// <summary>
        /// Gets or sets the Document Title.
        /// </summary>
        /// <value>Title of Referenced Document.</value>
        public string DocumentTitle
        {
            get { return base.DicomElementProvider[DicomTags.DocumentTitle].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.DocumentTitle].SetString(0, value); }
        }

        //TODO: Patient Clinical Trials Participation Sequence.

        /// <summary>
        /// Gets or sets the Clinical Trial Sponsor Name.
        /// </summary>
        /// <value>The name of the clinical trial sponsor.</value>
        public string ClinicalTrialSponsorName
        {
            get { return base.DicomElementProvider[DicomTags.ClinicalTrialSponsorName].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.ClinicalTrialSponsorName].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the Document Title.
        /// </summary>
        /// <value>Identifier for the Noted Protocol.</value>
        public string ClinicalTrialProtocolId
        {
            get { return base.DicomElementProvider[DicomTags.ClinicalTrialProtocolId].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.ClinicalTrialProtocolId].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the Clinical Trial Protocol Name.
        /// </summary>
        /// <value>The name or title of the clinical trial protocol.</value>
        public string ClinicalTrialProtocolName
        {
            get { return base.DicomElementProvider[DicomTags.ClinicalTrialProtocolName].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.ClinicalTrialProtocolName].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the Clinical Trial Site Id.
        /// </summary>
        /// <value>Identifier of the clinical trial site</value>
        public string ClinicalTrialSiteId
        {
            get { return base.DicomElementProvider[DicomTags.ClinicalTrialSiteId].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.ClinicalTrialSiteId].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the Clinical Trial Site Name.
        /// </summary>
        /// <value>Clinical Trial Site Name.</value>
        public string ClinicalTrialSiteName
        {
            get { return base.DicomElementProvider[DicomTags.ClinicalTrialSiteName].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.ClinicalTrialSiteName].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the Clinical Trial Subject Id.
        /// </summary>
        /// <value>Clinical Trial Subject Id.</value>
        public string ClinicalTrialSubjectId
        {
            get { return base.DicomElementProvider[DicomTags.ClinicalTrialSubjectId].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.ClinicalTrialSubjectId].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the Clinical Trial Subject Reading Id.
        /// </summary>
        /// <value>Clinical Trial Subject Reading Id.</value>
        public string ClinicalTrialSubjectReadingId
        {
            get { return base.DicomElementProvider[DicomTags.ClinicalTrialSubjectReadingId].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.ClinicalTrialSubjectReadingId].SetString(0, value); }
        }

        #endregion

    }

    #region SmokingStatus Enum
    /// <summary>
    /// SmokingStatus Enumeration
    /// </summary>
    public enum SmokingStatus
    {
        /// <summary>
        /// Yes
        /// </summary>
        Yes,
        /// <summary>
        /// No
        /// </summary>
        No,
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown
        
    }
    #endregion

    #region PregnancyStatus Enum
    /// <summary>
    /// PregnancyStatus Enumeration
    /// </summary>
    public enum PregnancyStatus
    {
        /// <summary>
        /// Not Pregnant
        /// </summary>
        NotPregnant,
        /// <summary>
        /// Possibly Pregnant
        /// </summary>
        PossiblyPregnant,
        /// <summary>
        /// Definitely Pregnant
        /// </summary>
        DefinitelyPregnant,
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown

    }
    #endregion

}

