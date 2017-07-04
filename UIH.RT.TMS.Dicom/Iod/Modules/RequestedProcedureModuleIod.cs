/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: RequestedProcedureModuleIod.cs
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
    /// As per Dicom Doc 3, Table C.4-11 (pg 248)
    /// </summary>
    public class RequestedProcedureModuleIod : IodBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the PatientModule class.
        /// </summary>
        public RequestedProcedureModuleIod()
            :base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Iod class.
        /// </summary>
        /// <param name="_dicomAttributeCollection"></param>
		public RequestedProcedureModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider)
        {
        }
        #endregion

        #region Public Properties
        public string RequestedProcedureId
        {
            get { return base.DicomElementProvider[DicomTags.RequestedProcedureId].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.RequestedProcedureId].SetString(0, value); }
        }
        public string ReasonForTheRequestedProcedure
        {
            get { return base.DicomElementProvider[DicomTags.ReasonForTheRequestedProcedure].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.ReasonForTheRequestedProcedure].SetString(0, value); }
        }

        public string RequestedProcedureComments
        {
            get { return base.DicomElementProvider[DicomTags.RequestedProcedureComments].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.RequestedProcedureComments].SetString(0, value); }
        }

        public string StudyInstanceUid
        {
            get { return base.DicomElementProvider[DicomTags.StudyInstanceUid].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.StudyInstanceUid].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the study date.
        /// </summary>
        /// <value>The study date.</value>
        public DateTime? StudyDate
        {
            get
            {
                return DateTimeParser.ParseDateAndTime(String.Empty,
                  base.DicomElementProvider[DicomTags.StudyDate].GetString(0, String.Empty),
                  base.DicomElementProvider[DicomTags.StudyTime].GetString(0, String.Empty));
            }

            set { DateTimeParser.SetDateTimeAttributeValues(value, base.DicomElementProvider[DicomTags.StudyDate], base.DicomElementProvider[DicomTags.StudyTime]); }
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

        // TODO: make one with the RequestedProcedurePriority enum
        public RequestedProcedurePriority RequestedProcedurePriority
        {
            get { return IodBase.ParseEnum<RequestedProcedurePriority>(base.DicomElementProvider[DicomTags.RequestedProcedurePriority].GetString(0, String.Empty), RequestedProcedurePriority.None); }
            set 
            {
                string stringValue = value == RequestedProcedurePriority.None ? String.Empty : value.ToString().ToUpperInvariant();
                base.DicomElementProvider[DicomTags.RequestedProcedurePriority].SetString(0, stringValue); 
            }
        }
        
        #endregion

    }

    /// <summary>
    /// 
    /// </summary>
    public enum RequestedProcedurePriority
    {
        /// <summary>
        /// 
        /// </summary>
        None,
        /// <summary>
        /// 
        /// </summary>
        Stat,
        /// <summary>
        /// 
        /// </summary>
        High,
        /// <summary>
        /// 
        /// </summary>
        Routine,
        /// <summary>
        /// 
        /// </summary>
        Medium,
        /// <summary>
        /// 
        /// </summary>
        Low
    }
}
