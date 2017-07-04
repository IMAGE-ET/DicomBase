/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ImageIod.cs
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
    /// Generic Image IOD.  Note, in progress.
    /// </summary>
    public class ImageIod : IodBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageIod"/> class.
        /// </summary>
        public ImageIod()
            :base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageIod"/> class.
        /// </summary>
        public ImageIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider)
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the patient module.
        /// </summary>
        /// <value>The patient module.</value>
        public PatientIdentificationModuleIod PatientIdentificationModule
        {
            get { return base.GetModuleIod<PatientIdentificationModuleIod>(); }
        }

        /// <summary>
        /// Gets the study module.
        /// </summary>
        /// <value>The study module.</value>
        public StudyModuleIod StudyModule
        {
            get { return base.GetModuleIod<StudyModuleIod>(); }
        }
        #endregion

    }
}
