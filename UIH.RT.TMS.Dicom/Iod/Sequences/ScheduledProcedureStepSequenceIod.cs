/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ScheduledProcedureStepSequenceIod.cs
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
using UIH.RT.TMS.Dicom.Iod.Macros;

namespace UIH.RT.TMS.Dicom.Iod.Sequences
{
    /// <summary>
    /// Scheduled Procedure Step Sequence (0040,0100)
    /// </summary>
    /// <remarks>As per Dicom Doc 3, C.4-10 (pg 249)</remarks>
    public class ScheduledProcedureStepSequenceIod : SequenceIodBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduledProcedureStepSequenceIod"/> class.
        /// </summary>
        public ScheduledProcedureStepSequenceIod()
            :base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduledProcedureStepSequenceIod"/> class.
        /// </summary>
        /// <param name="dicomSequenceItem">The dicom sequence item.</param>
        public ScheduledProcedureStepSequenceIod(DicomSequenceItem dicomSequenceItem)
            : base(dicomSequenceItem)
        {
        }
        #endregion

        #region Public Properties
        public string ScheduledStationAeTitle
        {
            get { return base.DicomElementProvider[DicomTags.ScheduledStationAeTitle].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.ScheduledStationAeTitle].SetString(0, value); }
        }

        public string ScheduledStationName
        {
            get { return base.DicomElementProvider[DicomTags.ScheduledStationName].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.ScheduledStationName].SetString(0, value); }
        }

        public string ScheduledProcedureStepLocation
        {
            get { return base.DicomElementProvider[DicomTags.ScheduledProcedureStepLocation].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.ScheduledProcedureStepLocation].SetString(0, value); }
        }

        public DateTime ScheduledProcedureStepStartDate
        {
            get { return base.DicomElementProvider[DicomTags.ScheduledProcedureStepStartDate].GetDateTime(0, DateTime.MinValue); }
            set { base.DicomElementProvider[DicomTags.ScheduledProcedureStepStartDate].SetDateTime(0, value); }
        }

        public DateTime ScheduledProcedureStepEndDate
        {
            get { return base.DicomElementProvider[DicomTags.ScheduledProcedureStepEndDate].GetDateTime(0, DateTime.MinValue); }
            set { base.DicomElementProvider[DicomTags.ScheduledProcedureStepEndDate].SetDateTime(0, value); }
        }

        public PersonName ScheduledPerformingPhysiciansName
        {
            get { return new PersonName(base.DicomElementProvider[DicomTags.ScheduledPerformingPhysiciansName].GetString(0, String.Empty)); }
            set { base.DicomElementProvider[DicomTags.ScheduledPerformingPhysiciansName].SetString(0, value.ToString()); }
        }

        public string ScheduledProcedureStepDescription
        {
            get { return base.DicomElementProvider[DicomTags.ScheduledProcedureStepDescription].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.ScheduledProcedureStepDescription].SetString(0, value); }
        }

        public string ScheduledProcedureStepId
        {
            get { return base.DicomElementProvider[DicomTags.ScheduledProcedureStepId].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.ScheduledProcedureStepId].SetString(0, value); }
        }

        public ScheduledProcedureStepStatus ScheduledProcedureStepStatus
        {
            get { return IodBase.ParseEnum<ScheduledProcedureStepStatus>(base.DicomElementProvider[DicomTags.ScheduledProcedureStepStatus].GetString(0, String.Empty), ScheduledProcedureStepStatus.None); }
            set { IodBase.SetAttributeFromEnum(base.DicomElementProvider[DicomTags.ScheduledProcedureStepStatus], value, false); }
        }

        public string CommentsOnTheScheduledProcedureStep
        {
            get { return base.DicomElementProvider[DicomTags.CommentsOnTheScheduledProcedureStep].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.CommentsOnTheScheduledProcedureStep].SetString(0, value); }
        }

        public string Modality
        {
            get { return base.DicomElementProvider[DicomTags.Modality].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.Modality].SetString(0, value); }
        }

        public string RequestedContrastAgent
        {
            get { return base.DicomElementProvider[DicomTags.RequestedContrastAgent].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.RequestedContrastAgent].SetString(0, value); }
        }

        public string PreMedication
        {
            get { return base.DicomElementProvider[DicomTags.PreMedication].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.PreMedication].SetString(0, value); }
        }

        public SequenceIodList<CodeSequenceMacro> ScheduledProtocolCodeSequenceList
        {
            get { return new SequenceIodList<CodeSequenceMacro>(DicomElementProvider[DicomTags.ScheduledProtocolCodeSequence] as DicomElementSq); }
        }
       #endregion

        #region Public Methods
        /// <summary>
        /// Sets the common tags for a typical Modality Worklist Request.
        /// </summary>
        public void SetCommonTags()
        {
            SetCommonTags(this);
        }
        #endregion

        #region Public Static Methods

        /// <summary>
        /// Sets the common tags for a typical Modality Worklist Request.
        /// </summary>
		/// <param name="scheduledProcedureStepSequenceIod">The scheduled step attributes sequence iod.</param>
        public static void SetCommonTags(ScheduledProcedureStepSequenceIod scheduledProcedureStepSequenceIod)
        {
            scheduledProcedureStepSequenceIod.SetAttributeNull(DicomTags.Modality);
            scheduledProcedureStepSequenceIod.SetAttributeNull(DicomTags.ScheduledProcedureStepId);
            scheduledProcedureStepSequenceIod.SetAttributeNull(DicomTags.ScheduledProcedureStepDescription);
            scheduledProcedureStepSequenceIod.SetAttributeNull(DicomTags.ScheduledStationAeTitle);
            scheduledProcedureStepSequenceIod.SetAttributeNull(DicomTags.ScheduledProcedureStepStartDate);
            scheduledProcedureStepSequenceIod.SetAttributeNull(DicomTags.ScheduledProcedureStepStartTime);
            scheduledProcedureStepSequenceIod.SetAttributeNull(DicomTags.ScheduledPerformingPhysiciansName);
            scheduledProcedureStepSequenceIod.SetAttributeNull(DicomTags.ScheduledProcedureStepLocation);
            scheduledProcedureStepSequenceIod.SetAttributeNull(DicomTags.ScheduledProcedureStepStatus);
            scheduledProcedureStepSequenceIod.SetAttributeNull(DicomTags.CommentsOnTheScheduledProcedureStep);
        }
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ScheduledProcedureStepStatus
    {
        /// <summary>
        /// No status, or empty value
        /// </summary>
        None,
        /// <summary>
        /// Procedure Step scheduled
        /// </summary>
        Scheduled,
        /// <summary>
        /// patient is available for the Scheduled Procedure Step
        /// </summary>
        Arrived,
        /// <summary>
        /// all patient and other necessary preparation for this step has been completed
        /// </summary>
        Ready,
        /// <summary>
        /// at least one Performed Procedure Step has been created that references this Scheduled Procedure Step
        /// </summary>
        Started
    }
}
