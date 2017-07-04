/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ModalityPerformedProcedureStepIod.cs
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
    /// Modality Performed Procedure Step Iod
    /// </summary>
    /// <remarks>As per Dicom Doc 3, B.17.2-1 (pg 237)</remarks>
    public class ModalityPerformedProcedureStepIod : IodBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ModalityPerformedProcedureStepIod"/> class.
        /// </summary>
        public ModalityPerformedProcedureStepIod()
            :base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModalityPerformedProcedureStepIod"/> class.
        /// </summary>
		public ModalityPerformedProcedureStepIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider)
        {
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Contains SOP common information.
        /// </summary>
        /// <value>The sop common.</value>
        public SopCommonModuleIod SopCommon
        {
            get { return base.GetModuleIod<SopCommonModuleIod>(); }
        }

        /// <summary>
        /// References the related SOPs and IEs.
        /// </summary>
        /// <value>The performed procedure step relationship.</value>
        public PerformedProcedureStepRelationshipModuleIod PerformedProcedureStepRelationship
        {
            get { return base.GetModuleIod<PerformedProcedureStepRelationshipModuleIod>(); }
        }

        /// <summary>
        /// Includes identifying and status information as well as place and time
        /// </summary>
        /// <value>The performed procedure step information.</value>
        public PerformedProcedureStepInformationModuleIod PerformedProcedureStepInformation
        {
            get { return base.GetModuleIod<PerformedProcedureStepInformationModuleIod>(); }
        }

        /// <summary>
        /// Identifies Series and Images related to this PPS and specific image acquisition conditions.
        /// </summary>
        /// <value>The image acquisition results.</value>
        public ImageAcquisitionResultsModuleIod ImageAcquisitionResults
        {
            get { return base.GetModuleIod<ImageAcquisitionResultsModuleIod>(); }
        }

        /// <summary>
        /// Contains radiation dose information related to this Performed Procedure Step.
        /// </summary>
        /// <value>The radiation dose.</value>
        public RadiationDoseModuleIod RadiationDose
        {
            get { return base.GetModuleIod<RadiationDoseModuleIod>(); }
        }

        /// <summary>
        /// Contains codes for billing and material management.
        /// </summary>
        /// <value>The billing and material management codes.</value>
        public BillingAndMaterialManagementCodesModuleIod BillingAndMaterialManagementCodes
        {
            get { return base.GetModuleIod<BillingAndMaterialManagementCodesModuleIod>(); }
        }
        
       #endregion

        #region Public Methods
        /// <summary>
        /// Sets the common tags for a typical request.
        /// </summary>
        public void SetCommonTags()
        {
            SetCommonTags(base.DicomElementProvider);
        }
        #endregion

        #region Public Static Methods
        /// <summary>
        /// Sets the common tags for a typical request.
        /// </summary>
        public static void SetCommonTags(IDicomElementProvider dicomElementProvider)
        {
            //dicomElementProvider[DicomTags.PatientsName].SetString(0, "*");
            //dicomElementProvider[DicomTags.PatientId].SetNullValue();
            //dicomElementProvider[DicomTags.PatientsBirthDate].SetNullValue();
            //dicomElementProvider[DicomTags.PatientsBirthTime].SetNullValue();
            //dicomElementProvider[DicomTags.PatientsWeight].SetNullValue();

            //dicomElementProvider[DicomTags.RequestedProcedureId].SetNullValue();
            //dicomElementProvider[DicomTags.RequestedProcedureDescription].SetNullValue();
            //dicomElementProvider[DicomTags.StudyInstanceUid].SetNullValue();
            //dicomElementProvider[DicomTags.ReasonForTheRequestedProcedure].SetNullValue();
            //dicomElementProvider[DicomTags.RequestedProcedureComments].SetNullValue();
            //dicomElementProvider[DicomTags.RequestedProcedurePriority].SetNullValue();
            //dicomElementProvider[DicomTags.ImagingServiceRequestComments].SetNullValue();
            //dicomElementProvider[DicomTags.RequestingPhysician].SetNullValue();
            //dicomElementProvider[DicomTags.ReferringPhysiciansName].SetNullValue();
            //dicomElementProvider[DicomTags.RequestedProcedureLocation].SetNullValue();
            //dicomElementProvider[DicomTags.AccessionNumber].SetNullValue();

            //// TODO: this better and easier...
            //DicomElementSq DicomElementSq = dicomElementProvider[DicomTags.ScheduledProcedureStepSequence] as DicomElementSq;
            //DicomSequenceItem dicomSequenceItem = new DicomSequenceItem();
            //DicomElementSq.Values = dicomSequenceItem;

            //dicomSequenceItem[DicomTags.Modality].SetNullValue();
            //dicomSequenceItem[DicomTags.ScheduledProcedureStepId].SetNullValue();
            //dicomSequenceItem[DicomTags.ScheduledProcedureStepDescription].SetNullValue();
            //dicomSequenceItem[DicomTags.ScheduledStationAeTitle].SetNullValue();
            //dicomSequenceItem[DicomTags.ScheduledProcedureStepStartDate].SetNullValue();
            //dicomSequenceItem[DicomTags.ScheduledProcedureStepStartTime].SetNullValue();
            //dicomSequenceItem[DicomTags.ScheduledPerformingPhysiciansName].SetNullValue();
            //dicomSequenceItem[DicomTags.ScheduledProcedureStepLocation].SetNullValue();
            //dicomSequenceItem[DicomTags.ScheduledProcedureStepStatus].SetNullValue();
            //dicomSequenceItem[DicomTags.CommentsOnTheScheduledProcedureStep].SetNullValue();

        }
        #endregion
    }
}
