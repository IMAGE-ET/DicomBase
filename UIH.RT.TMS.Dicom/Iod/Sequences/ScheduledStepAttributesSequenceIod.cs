/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ScheduledStepAttributesSequenceIod.cs
////
//// Summary:
////
////
//// Date: 2014/08/19
//////////////////////////////////////////////////////////////////////////
#region License

// Copyright (c) 2011 - 2013, United-Imaging Inc.
// All rights reserved.
// http://www.united-imaging.com

#endregion

using System;

namespace UIH.RT.TMS.Dicom.Iod.Sequences
{
    /// <summary>
    /// Scheduled Step Attributes Sequence (0040,0270)
    /// </summary>
    /// <remarks>As per Dicom Doc 3, C.4-13 (pg 253)</remarks>
    public class ScheduledStepAttributesSequenceIod : SequenceIodBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduledStepAttributesSequuenceIod"/> class.
        /// </summary>
        public ScheduledStepAttributesSequenceIod()
            :base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduledStepAttributesSequuenceIod"/> class.
        /// </summary>
        /// <param name="dicomSequenceItem">The dicom sequence item.</param>
        public ScheduledStepAttributesSequenceIod(DicomSequenceItem dicomSequenceItem)
            : base(dicomSequenceItem)
        {
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the study instance uid.
        /// </summary>
        /// <value>The study instance uid.</value>
        public string StudyInstanceUid
        {
            get { return base.DicomElementProvider[DicomTags.StudyInstanceUid].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.StudyInstanceUid].SetString(0, value); }
        }

        //TODO: Referenced Study Sequence

        /// <summary>
        /// Gets or sets the accession number.
        /// </summary>
        /// <value>The accession number.</value>
        public string AccessionNumber
        {
            get { return base.DicomElementProvider[DicomTags.AccessionNumber].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.AccessionNumber].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the placer order number imaging service request.
        /// </summary>
        /// <value>The placer order number imaging service request.</value>
        public string PlacerOrderNumberImagingServiceRequest
        {
            get { return base.DicomElementProvider[DicomTags.PlacerOrderNumberImagingServiceRequest].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.PlacerOrderNumberImagingServiceRequest].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the filler order number imaging service request.
        /// </summary>
        /// <value>The filler order number imaging service request.</value>
        public string FillerOrderNumberImagingServiceRequest
        {
            get { return base.DicomElementProvider[DicomTags.FillerOrderNumberImagingServiceRequest].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.FillerOrderNumberImagingServiceRequest].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the requested procedure id.
        /// </summary>
        /// <value>The requested procedure id.</value>
        public string RequestedProcedureId
        {
            get { return base.DicomElementProvider[DicomTags.RequestedProcedureId].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.RequestedProcedureId].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the requested procedure description.
        /// </summary>
        /// <value>The requested procedure description.</value>
        public string RequestedProcedureDescription
        {
            get { return base.DicomElementProvider[DicomTags.RequestedProcedureDescription].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.RequestedProcedureDescription].SetString(0, value); }
        }

        // TODO: Requested Procedure Code Sequence

        /// <summary>
        /// Gets or sets the scheduled procedure step id.
        /// </summary>
        /// <value>The scheduled procedure step id.</value>
        public string ScheduledProcedureStepId
        {
            get { return base.DicomElementProvider[DicomTags.ScheduledProcedureStepId].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.ScheduledProcedureStepId].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the scheduled procedure step description.
        /// </summary>
        /// <value>The scheduled procedure step description.</value>
        public string ScheduledProcedureStepDescription
        {
            get { return base.DicomElementProvider[DicomTags.ScheduledProcedureStepDescription].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.ScheduledProcedureStepDescription].SetString(0, value); }
        }

        //TODO: >Scheduled Protocol Code Sequence
        
        #endregion

    }

}
