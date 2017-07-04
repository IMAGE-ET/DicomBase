/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ScheduledProcedureStepModuleIod.cs
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

using UIH.RT.TMS.Dicom.Iod.Sequences;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
    /// <summary>
    /// Scheduled Procedure Step Modole
    /// </summary>
    /// <remarks>As per Dicom Doc 3, Table C.4-10 (pg 246)</remarks>
    public class ScheduledProcedureStepModuleIod : IodBase
    {
        #region Constructors
        /// <summary>
		/// Constructor.
		/// </summary>
        public ScheduledProcedureStepModuleIod()
            :base()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
		public ScheduledProcedureStepModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider)
        {
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the scheduled procedure step sequence list.
        /// </summary>
        /// <value>The scheduled procedure step sequence list.</value>
        public SequenceIodList<ScheduledProcedureStepSequenceIod> ScheduledProcedureStepSequenceList
        {
            get 
            {
                return new SequenceIodList<ScheduledProcedureStepSequenceIod>(base.DicomElementProvider[DicomTags.ScheduledProcedureStepSequence] as DicomElementSq); 
            }
        }

       #endregion

    }

}
