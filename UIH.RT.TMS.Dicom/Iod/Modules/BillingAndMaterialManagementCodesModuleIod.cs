/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: BillingAndMaterialManagementCodesModuleIod.cs
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

using UIH.RT.TMS.Dicom.Iod.Macros;
using UIH.RT.TMS.Dicom.Iod.Sequences;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
    /// <summary>
    /// As per Dicom DOC 3 Table C.4-17
    /// </summary>
    public class BillingAndMaterialManagementCodesModuleIod : IodBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BillingAndMaterialManagementCodesModuleIod"/> class.
        /// </summary>
        public BillingAndMaterialManagementCodesModuleIod()
            :base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BillingAndMaterialManagementCodesModuleIod"/> class.
        /// </summary>
		public BillingAndMaterialManagementCodesModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider)
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Contains billing codes for the Procedure Type performed within the Procedure Step. The sequence may have zero or more Items.
        /// </summary>
        /// <value>The billing procedure step sequence list.</value>
        public SequenceIodList<CodeSequenceMacro> BillingProcedureStepSequenceList
        {
            get
            {
                return new SequenceIodList<CodeSequenceMacro>(base.DicomElementProvider[DicomTags.BillingProcedureStepSequence] as DicomElementSq);
            }
        }

        /// <summary>
        /// Information about the film consumption for this Performed Procedure Step. The sequence may have zero or more Items.
        /// </summary>
        /// <value>The film consumption sequence list.</value>
        public SequenceIodList<FilmConsumptionSequenceIod> FilmConsumptionSequenceList
        {
            get
            {
                return new SequenceIodList<FilmConsumptionSequenceIod>(base.DicomElementProvider[DicomTags.FilmConsumptionSequence] as DicomElementSq);
            }
        }

        /// <summary>
        /// Chemicals, supplies and devices for billing used in the Performed Procedure Step. The sequence may have one or more Items.
        /// </summary>
        /// <value>The billing supplies and devices sequence list.</value>
        public SequenceIodList<BillingSuppliesAndDevicesSequenceIod> BillingSuppliesAndDevicesSequenceList
        {
            get
            {
                return new SequenceIodList<BillingSuppliesAndDevicesSequenceIod>(base.DicomElementProvider[DicomTags.BillingSuppliesAndDevicesSequence] as DicomElementSq);
            }
        }

        #endregion

    }
}
