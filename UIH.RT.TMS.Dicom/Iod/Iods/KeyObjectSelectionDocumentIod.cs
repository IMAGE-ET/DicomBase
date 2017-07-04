/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: KeyObjectSelectionDocumentIod.cs
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

using UIH.RT.TMS.Dicom;
using UIH.RT.TMS.Dicom.Iod.Modules;

namespace UIH.RT.TMS.Dicom.Iod.Iods
{
	public class KeyObjectSelectionDocumentIod
	{
		private readonly IDicomElementProvider _dicomElementProvider;
		private readonly PatientModuleIod _patientModule;
		private readonly SpecimenIdentificationModuleIod _specimenIdentificationModule;
		private readonly ClinicalTrialSubjectModuleIod _clinicalTrialSubjectModule;
		private readonly GeneralStudyModuleIod _generalStudyModule;
		private readonly PatientStudyModuleIod _patientStudyModule;
		private readonly ClinicalTrialStudyModuleIod _clinicalTrialStudyModule;
		private readonly KeyObjectDocumentSeriesModuleIod _keyObjectDocumentSeriesModule;
		private readonly ClinicalTrialSeriesModuleIod _clinicalTrialSeriesModule;
		private readonly GeneralEquipmentModuleIod _generalEquipmentModule;
		private readonly KeyObjectDocumentModuleIod _keyObjectDocumentModule;
		private readonly SrDocumentContentModuleIod _srDocumentContentModule;
		private readonly SopCommonModuleIod _sopCommonModule;

		private bool _hasSpecimenIdentificationModule = false;
		private bool _hasClinicalTrialSubjectModule = false;
		private bool _hasPatientStudyModule = false;
		private bool _hasClinicalTrialStudyModule = false;
		private bool _hasClinicalTrialSeriesModule = false;

		public KeyObjectSelectionDocumentIod() : this(new DicomDataset()) {}

		public KeyObjectSelectionDocumentIod(IDicomElementProvider dicomElementProvider)
		{
			_dicomElementProvider = dicomElementProvider;
			_patientModule = new PatientModuleIod(_dicomElementProvider);
			_specimenIdentificationModule = new SpecimenIdentificationModuleIod(_dicomElementProvider);
			_clinicalTrialSubjectModule = new ClinicalTrialSubjectModuleIod(_dicomElementProvider);
			_generalStudyModule = new GeneralStudyModuleIod(_dicomElementProvider);
			_patientStudyModule = new PatientStudyModuleIod(_dicomElementProvider);
			_clinicalTrialStudyModule = new ClinicalTrialStudyModuleIod(_dicomElementProvider);
			_keyObjectDocumentSeriesModule = new KeyObjectDocumentSeriesModuleIod(_dicomElementProvider);
			_clinicalTrialSeriesModule = new ClinicalTrialSeriesModuleIod(_dicomElementProvider);
			_generalEquipmentModule = new GeneralEquipmentModuleIod(_dicomElementProvider);
			_keyObjectDocumentModule = new KeyObjectDocumentModuleIod(_dicomElementProvider);
			_srDocumentContentModule = new SrDocumentContentModuleIod(_dicomElementProvider);
			_sopCommonModule = new SopCommonModuleIod(_dicomElementProvider);
		}

		public PatientModuleIod Patient
		{
			get { return _patientModule; }
		}

		public SpecimenIdentificationModuleIod SpecimenIdentification
		{
			get { return _specimenIdentificationModule; }
		}

		public ClinicalTrialSubjectModuleIod ClinicalTrialSubject
		{
			get { return _clinicalTrialSubjectModule; }
		}

		public GeneralStudyModuleIod GeneralStudy
		{
			get { return _generalStudyModule; }
		}

		public PatientStudyModuleIod PatientStudy
		{
			get { return _patientStudyModule; }
		}

		public ClinicalTrialStudyModuleIod ClinicalTrialStudy
		{
			get { return _clinicalTrialStudyModule; }
		}

		public KeyObjectDocumentSeriesModuleIod KeyObjectDocumentSeries
		{
			get { return _keyObjectDocumentSeriesModule; }
		}

		public ClinicalTrialSeriesModuleIod ClinicalTrialSeries
		{
			get { return _clinicalTrialSeriesModule; }
		}

		public GeneralEquipmentModuleIod GeneralEquipment
		{
			get { return _generalEquipmentModule; }
		}

		public KeyObjectDocumentModuleIod KeyObjectDocument
		{
			get { return _keyObjectDocumentModule; }
		}

		public SrDocumentContentModuleIod SrDocumentContent
		{
			get { return _srDocumentContentModule; }
		}

		public SopCommonModuleIod SopCommon
		{
			get { return _sopCommonModule; }
		}

		public bool HasSpecimenIdentificationModule
		{
			get { return _hasSpecimenIdentificationModule; }
			set { _hasSpecimenIdentificationModule = value; }
		}

		public bool HasClinicalTrialSubjectModule
		{
			get { return _hasClinicalTrialSubjectModule; }
			set { _hasClinicalTrialSubjectModule = value; }
		}

		public bool HasPatientStudyModule
		{
			get { return _hasPatientStudyModule; }
			set { _hasPatientStudyModule = value; }
		}

		public bool HasClinicalTrialStudyModule
		{
			get { return _hasClinicalTrialStudyModule; }
			set { _hasClinicalTrialStudyModule = value; }
		}

		public bool HasClinicalTrialSeriesModule
		{
			get { return _hasClinicalTrialSeriesModule; }
			set { _hasClinicalTrialSeriesModule = value; }
		}
	}
}
