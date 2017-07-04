/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: GrayscaleSoftcopyPresentationStateIod.cs
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
	public class GrayscaleSoftcopyPresentationStateIod
	{
		private readonly IDicomElementProvider _dicomElementProvider;

		public GrayscaleSoftcopyPresentationStateIod() : this(new DicomDataset()) {}

		public GrayscaleSoftcopyPresentationStateIod(IDicomElementProvider provider)
		{
			_dicomElementProvider = provider;

			this.Patient = new PatientModuleIod(_dicomElementProvider);
			this.ClinicalTrialSubject = new ClinicalTrialSubjectModuleIod(_dicomElementProvider);

			this.GeneralStudy = new GeneralStudyModuleIod(_dicomElementProvider);
			this.PatientStudy = new PatientStudyModuleIod(_dicomElementProvider);
			this.ClinicalTrialStudy = new ClinicalTrialStudyModuleIod(_dicomElementProvider);

			this.GeneralSeries = new GeneralSeriesModuleIod(_dicomElementProvider);
			this.ClinicalTrialSeries = new ClinicalTrialSeriesModuleIod(_dicomElementProvider);
			this.PresentationSeries = new PresentationSeriesModuleIod(_dicomElementProvider);

			this.GeneralEquipment = new GeneralEquipmentModuleIod(_dicomElementProvider);

			this.PresentationStateIdentification = new PresentationStateIdentificationModuleIod(_dicomElementProvider);
			this.PresentationStateRelationship = new PresentationStateRelationshipModuleIod(_dicomElementProvider);
			this.PresentationStateShutter = new PresentationStateShutterModuleIod(_dicomElementProvider);
			this.PresentationStateMask = new PresentationStateMaskModuleIod(_dicomElementProvider);
			this.Mask = new MaskModuleIod(_dicomElementProvider);
			this.DisplayShutter = new DisplayShutterModuleIod(_dicomElementProvider);
			this.BitmapDisplayShutter = new BitmapDisplayShutterModuleIod(_dicomElementProvider);
			this.OverlayPlane = new OverlayPlaneModuleIod(_dicomElementProvider);
			this.OverlayActivation = new OverlayActivationModuleIod(_dicomElementProvider);
			this.DisplayedArea = new DisplayedAreaModuleIod(_dicomElementProvider);
			this.GraphicAnnotation = new GraphicAnnotationModuleIod(_dicomElementProvider);
			this.SpatialTransform = new SpatialTransformModuleIod(_dicomElementProvider);
			this.GraphicLayer = new GraphicLayerModuleIod(_dicomElementProvider);
			this.ModalityLut = new ModalityLutModuleIod(_dicomElementProvider);
			this.SoftcopyVoiLut = new SoftcopyVoiLutModuleIod(_dicomElementProvider);
			this.SoftcopyPresentationLut = new SoftcopyPresentationLutModuleIod(_dicomElementProvider);
			this.SopCommon = new SopCommonModuleIod(_dicomElementProvider);
		}

		public IDicomElementProvider DicomElementProvider
		{
			get { return _dicomElementProvider; }
		}

		#region Patient IE

		/// <summary>
		/// Gets the Patient module (required usage).
		/// </summary>
		public readonly PatientModuleIod Patient;

		/// <summary>
		/// Gets the Clinical Trial Subject module (optional usage).
		/// </summary>
		public readonly ClinicalTrialSubjectModuleIod ClinicalTrialSubject;

		#endregion

		#region Study IE

		/// <summary>
		/// Gets the General Study module (required usage).
		/// </summary>
		public readonly GeneralStudyModuleIod GeneralStudy;

		/// <summary>
		/// Gets the Patient Study module (optional usage).
		/// </summary>
		public readonly PatientStudyModuleIod PatientStudy;

		/// <summary>
		/// Gets the Clinical Trial Study module (optional usage).
		/// </summary>
		public readonly ClinicalTrialStudyModuleIod ClinicalTrialStudy;

		#endregion

		#region Series IE

		/// <summary>
		/// Gets the General Series module (required usage).
		/// </summary>
		public readonly GeneralSeriesModuleIod GeneralSeries;

		/// <summary>
		/// Gets the Clinical Trial Series module (optional usage).
		/// </summary>
		public readonly ClinicalTrialSeriesModuleIod ClinicalTrialSeries;

		/// <summary>
		/// Gets the Presentation Series module (required usage).
		/// </summary>
		public readonly PresentationSeriesModuleIod PresentationSeries;

		#endregion

		#region Equipment IE

		/// <summary>
		/// Gets the General Equipment module (required usage).
		/// </summary>
		public readonly GeneralEquipmentModuleIod GeneralEquipment;

		#endregion

		#region Presentation State IE

		/// <summary>
		/// Gets the Presentation State Identification module (required usage).
		/// </summary>
		public readonly PresentationStateIdentificationModuleIod PresentationStateIdentification;

		/// <summary>
		/// Gets the Presentation State Relationship module (required usage).
		/// </summary>
		public readonly PresentationStateRelationshipModuleIod PresentationStateRelationship;

		/// <summary>
		/// Gets the Presentation State Shutter module (required usage).
		/// </summary>
		public readonly PresentationStateShutterModuleIod PresentationStateShutter;

		/// <summary>
		/// Gets the Presentation State Mask module (required usage).
		/// </summary>
		public readonly PresentationStateMaskModuleIod PresentationStateMask;

		/// <summary>
		/// Gets the Mask module (required if the referenced images are multiframe and are to be subtracted).
		/// </summary>
		public readonly MaskModuleIod Mask;

		/// <summary>
		/// Gets the Display Shutter module (conditionally required usage).
		/// </summary>
		public readonly DisplayShutterModuleIod DisplayShutter;

		/// <summary>
		/// Gets the Bitmap Display Shutter module (conditionally required usage).
		/// </summary>
		public readonly BitmapDisplayShutterModuleIod BitmapDisplayShutter;

		/// <summary>
		/// Gets the Overlay Plane module (conditionally required usage).
		/// </summary>
		public readonly OverlayPlaneModuleIod OverlayPlane;

		/// <summary>
		/// Gets the Overlay Activation module (conditionally required usage).
		/// </summary>
		public readonly OverlayActivationModuleIod OverlayActivation;

		/// <summary>
		/// Gets the Displayed Area module (required usage).
		/// </summary>
		public readonly DisplayedAreaModuleIod DisplayedArea;

		/// <summary>
		/// Gets the Graphic Annotation module (conditionally required usage).
		/// </summary>
		public readonly GraphicAnnotationModuleIod GraphicAnnotation;

		/// <summary>
		/// Gets the Spatial Transform module (conditionally required usage).
		/// </summary>
		public readonly SpatialTransformModuleIod SpatialTransform;

		/// <summary>
		/// Gets the Graphic Layer module (conditionally required usage).
		/// </summary>
		public readonly GraphicLayerModuleIod GraphicLayer;

		/// <summary>
		/// Gets the Modality LUT module (conditionally required usage).
		/// </summary>
		public readonly ModalityLutModuleIod ModalityLut;

		/// <summary>
		/// Gets the Softcopy VOI LUT module (conditionally required usage).
		/// </summary>
		public readonly SoftcopyVoiLutModuleIod SoftcopyVoiLut;

		/// <summary>
		/// Gets the Softcopy Presentation LUT module (required usage).
		/// </summary>
		public readonly SoftcopyPresentationLutModuleIod SoftcopyPresentationLut;

		/// <summary>
		/// Gets the SOP Common module (required usage).
		/// </summary>
		public readonly SopCommonModuleIod SopCommon;

		#endregion
	}
}
