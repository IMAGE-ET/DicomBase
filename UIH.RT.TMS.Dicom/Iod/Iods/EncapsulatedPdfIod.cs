/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: EncapsulatedPdfIod.cs
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

using UIH.RT.TMS.Dicom.Iod.Modules;

namespace UIH.RT.TMS.Dicom.Iod.Iods
{
	/// <summary>
	/// EncapsulatedPdf IOD
	/// </summary>
	/// <remarks>
	/// <para>As defined in the DICOM Standard 2009, Part 3, Section A.45.1 (Table A.45.1-1)</para>
	/// </remarks>
	public class EncapsulatedPdfIod
	{
		private readonly IDicomElementProvider _dicomElementProvider;
		private readonly PatientModuleIod _patientModule;
		private readonly ClinicalTrialSubjectModuleIod _clinicalTrialSubjectModule;
		private readonly GeneralStudyModuleIod _generalStudyModule;
		private readonly PatientStudyModuleIod _patientStudyModule;
		private readonly ClinicalTrialStudyModuleIod _clinicalTrialStudyModule;
		private readonly EncapsulatedDocumentSeriesModuleIod _encapsulatedDocumentSeriesModule;
		private readonly ClinicalTrialSeriesModuleIod _clinicalTrialSeriesModule;
		private readonly GeneralEquipmentModuleIod _generalEquipmentModule;
		private readonly ScEquipmentModuleIod _scEquipmentModule;
		private readonly EncapsulatedDocumentModuleIod _encapsulatedDocumentModule;
		private readonly SopCommonModuleIod _sopCommonModule;

		/// <summary>
		/// Initializes a new instance of the <see cref="EncapsulatedPdfIod"/> class.
		/// </summary>	
		public EncapsulatedPdfIod()
			: this(new DicomDataset()) {}

		/// <summary>
		/// Initializes a new instance of the <see cref="EncapsulatedPdfIod"/> class.
		/// </summary>
		/// <param name="dicomElementProvider">The DICOM attribute provider.</param>
		public EncapsulatedPdfIod(IDicomElementProvider dicomElementProvider)
		{
			_dicomElementProvider = dicomElementProvider;

			_patientModule = new PatientModuleIod(_dicomElementProvider);
			_clinicalTrialSubjectModule = new ClinicalTrialSubjectModuleIod(_dicomElementProvider);
			_generalStudyModule = new GeneralStudyModuleIod(_dicomElementProvider);
			_patientStudyModule = new PatientStudyModuleIod(_dicomElementProvider);
			_clinicalTrialStudyModule = new ClinicalTrialStudyModuleIod(_dicomElementProvider);
			_encapsulatedDocumentSeriesModule = new EncapsulatedDocumentSeriesModuleIod(_dicomElementProvider);
			_clinicalTrialSeriesModule = new ClinicalTrialSeriesModuleIod(_dicomElementProvider);
			_generalEquipmentModule = new GeneralEquipmentModuleIod(_dicomElementProvider);
			_scEquipmentModule = new ScEquipmentModuleIod(_dicomElementProvider);
			_encapsulatedDocumentModule = new EncapsulatedDocumentModuleIod(_dicomElementProvider);
			_sopCommonModule = new SopCommonModuleIod(_dicomElementProvider);
		}

		public PatientModuleIod Patient
		{
			get { return _patientModule; }
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

		public EncapsulatedDocumentSeriesModuleIod EncapsulatedDocumentSeries
		{
			get { return _encapsulatedDocumentSeriesModule; }
		}

		public ClinicalTrialSeriesModuleIod ClinicalTrialSeries
		{
			get { return _clinicalTrialSeriesModule; }
		}

		public GeneralEquipmentModuleIod GeneralEquipment
		{
			get { return _generalEquipmentModule; }
		}

		public ScEquipmentModuleIod ScEquipment
		{
			get { return _scEquipmentModule; }
		}

		public EncapsulatedDocumentModuleIod EncapsulatedDocument
		{
			get { return _encapsulatedDocumentModule; }
		}

		public SopCommonModuleIod SopCommon
		{
			get { return _sopCommonModule; }
		}

		public bool HasClinicalTrialSubjectModule { get; set; }

		public bool HasPatientStudyModule { get; set; }

		public bool HasClinicalTrialStudyModule { get; set; }

		public bool HasClinicalTrialSeriesModule { get; set; }
	}
}
