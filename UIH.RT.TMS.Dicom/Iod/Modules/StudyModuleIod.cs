/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: StudyModuleIod.cs
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
    /// Study Module IOD (in progress)
    /// </summary>
    public class StudyModuleIod : IodBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the PatientModule class.
        /// </summary>
        public StudyModuleIod()
            :base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Iod class.
        /// </summary>
        /// <param name="_dicomAttributeCollection"></param>
		public StudyModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider)
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
        #endregion
    }
}
