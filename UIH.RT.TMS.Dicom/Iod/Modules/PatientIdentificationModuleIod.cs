/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: PatientIdentificationModuleIod.cs
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
    /// Patient Identification Module, as per Part 3, C.2.2
    /// </summary>
    public class PatientIdentificationModuleIod : IodBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PatientIdentificationModuleIod"/> class.
        /// </summary>
        public PatientIdentificationModuleIod()
            :base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientIdentificationModuleIod"/> class.
        /// </summary>
		public PatientIdentificationModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider)
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the name of the patient.
        /// </summary>
        /// <value>The name of the patient.</value>
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

        //TODO: Other Patient IDs
        //TODO: Other Patient IDs Sequence
        //TODO: Other Patient Names 

        /// <summary>
        /// Gets or sets the name of the patients birth.
        /// </summary>
        /// <value>The name of the patients birth.</value>
        public PersonName PatientsBirthName
        {
            get { return new PersonName(base.DicomElementProvider[DicomTags.PatientsBirthName].GetString(0, String.Empty)); }
            set { base.DicomElementProvider[DicomTags.PatientsBirthName].SetString(0, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the birth name of the patient's mother.
        /// </summary>
        /// <value>The birth name of the patient's mother.</value>
        public PersonName PatientsMothersBirthName
        {
            get { return new PersonName(base.DicomElementProvider[DicomTags.PatientsMothersBirthName].GetString(0, String.Empty)); }
            set { base.DicomElementProvider[DicomTags.PatientsMothersBirthName].SetString(0, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the medical record locator.
        /// </summary>
        /// <value>The medical record locator.</value>
        public string MedicalRecordLocator
        {
            get { return base.DicomElementProvider[DicomTags.MedicalRecordLocator].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.MedicalRecordLocator].SetString(0, value); }
        }

        public string PatientAge
        {
            get { return base.DicomElementProvider[DicomTags.PatientsAge].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.PatientsAge].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the occupation.
        /// </summary>
        /// <value>The occupation.</value>
        public string Occupation
        {
            get { return base.DicomElementProvider[DicomTags.Occupation].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.Occupation].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the confidentiality constraint on patient data description.
        /// </summary>
        /// <value>The confidentiality constraint on patient data description.</value>
        public string ConfidentialityConstraintOnPatientDataDescription
        {
            get { return base.DicomElementProvider[DicomTags.ConfidentialityConstraintOnPatientDataDescription].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.ConfidentialityConstraintOnPatientDataDescription].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the patients birth date.
        /// </summary>
        /// <value>The patients birth date.</value>
        public DateTime? PatientsBirthDate
        {
        	get { return DateTimeParser.ParseDateAndTime(String.Empty, 
        					base.DicomElementProvider[DicomTags.PatientsBirthDate].GetString(0, String.Empty), 
                  base.DicomElementProvider[DicomTags.PatientsBirthTime].GetString(0, String.Empty)); }

            set { DateTimeParser.SetDateTimeAttributeValues(value, base.DicomElementProvider[DicomTags.PatientsBirthDate], base.DicomElementProvider[DicomTags.PatientsBirthTime]); }
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
        
        //TODO: Patient's Insurance Plan Code Sequence
        //TODO: Patient�s Primary Language Code Sequence
        //TODO: Patient�s Primary Language Code Modifier Sequence

        /// <summary>
        /// Gets or sets the size of the patients (in meters)
        /// </summary>
        /// <value>The size of the patients.</value>
        public float PatientsSize
        {
            get { return base.DicomElementProvider[DicomTags.PatientsSize].GetFloat32(0, 0.0F); }
            set { base.DicomElementProvider[DicomTags.PatientsSize].SetFloat32(0, value); }
        }

        /// <summary>
        /// Gets or sets the patients weight (in KG)
        /// </summary>
        /// <value>The patients weight.</value>
        public float PatientsWeight
        {
            get { return base.DicomElementProvider[DicomTags.PatientsWeight].GetFloat32(0, 0.0F); }
            set { base.DicomElementProvider[DicomTags.PatientsWeight].SetFloat32(0, value); }
        }

        /// <summary>
        /// Gets or sets the patients address.
        /// </summary>
        /// <value>The patients address.</value>
        public string PatientsAddress
        {
            get { return base.DicomElementProvider[DicomTags.PatientsAddress].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.PatientsAddress].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the military rank.
        /// </summary>
        /// <value>The military rank.</value>
        public string MilitaryRank
        {
            get { return base.DicomElementProvider[DicomTags.MilitaryRank].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.MilitaryRank].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the branch of service.
        /// </summary>
        /// <value>The branch of service.</value>
        public string BranchOfService
        {
            get { return base.DicomElementProvider[DicomTags.BranchOfService].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.BranchOfService].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the country of residence.
        /// </summary>
        /// <value>The country of residence.</value>
        public string CountryOfResidence
        {
            get { return base.DicomElementProvider[DicomTags.CountryOfResidence].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.CountryOfResidence].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the region of residence.
        /// </summary>
        /// <value>The region of residence.</value>
        public string RegionOfResidence
        {
            get { return base.DicomElementProvider[DicomTags.RegionOfResidence].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.RegionOfResidence].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the patients telephone number. TODO: Way to specify more than 1...
        /// </summary>
        /// <value>The patients telephone numbers.</value>
        public string PatientsTelephoneNumbers
        {
            get { return base.DicomElementProvider[DicomTags.PatientsTelephoneNumbers].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.PatientsTelephoneNumbers].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the ethnic group.
        /// </summary>
        /// <value>The ethnic group.</value>
        public string EthnicGroup
        {
            get { return base.DicomElementProvider[DicomTags.EthnicGroup].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.EthnicGroup].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the patients religious preference.
        /// </summary>
        /// <value>The patients religious preference.</value>
        public string PatientsReligiousPreference
        {
            get { return base.DicomElementProvider[DicomTags.PatientsReligiousPreference].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.PatientsReligiousPreference].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the patient comments.
        /// </summary>
        /// <value>The patient comments.</value>
        public string PatientComments
        {
            get { return base.DicomElementProvider[DicomTags.PatientComments].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.PatientComments].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the responsible person.
        /// </summary>
        /// <value>The responsible person.</value>
        public PersonName ResponsiblePerson
        {
            get { return new PersonName(base.DicomElementProvider[DicomTags.ResponsiblePerson].GetString(0, String.Empty)); }
            set { base.DicomElementProvider[DicomTags.ResponsiblePerson].SetString(0, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the responsible person role.
        /// </summary>
        /// <value>The responsible person role.</value>
        public ResponsiblePersonRole ResponsiblePersonRole
        {
            get { return IodBase.ParseEnum<ResponsiblePersonRole>(base.DicomElementProvider[DicomTags.ResponsiblePersonRole].GetString(0, String.Empty), ResponsiblePersonRole.None); }
            set { IodBase.SetAttributeFromEnum(base.DicomElementProvider[DicomTags.ResponsiblePersonRole], value); }
        }

        /// <summary>
        /// Gets or sets the responsible organization.
        /// </summary>
        /// <value>The responsible organization.</value>
        public string ResponsibleOrganization
        {
            get { return base.DicomElementProvider[DicomTags.ResponsibleOrganization].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.ResponsibleOrganization].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the patient species description.
        /// </summary>
        /// <value>The patient species description.</value>
        public string PatientSpeciesDescription
        {
            get { return base.DicomElementProvider[DicomTags.PatientSpeciesDescription].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.PatientSpeciesDescription].SetString(0, value); }
        }

        //TODO: Patient Species Code Sequence

        /// <summary>
        /// Gets or sets the patient breed description.
        /// </summary>
        /// <value>The patient breed description.</value>
        public string PatientBreedDescription
        {
            get { return base.DicomElementProvider[DicomTags.PatientBreedDescription].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.PatientBreedDescription].SetString(0, value); }
        }
        
        //TODO: Patient Breed Code Sequence

        //TODO: Patient Breed Registration Sequence
        #endregion

    }    
}
