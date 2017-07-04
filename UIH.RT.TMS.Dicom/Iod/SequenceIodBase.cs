/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: SequenceIodBase.cs
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

namespace UIH.RT.TMS.Dicom.Iod
{
    /// <summary>
    /// Sequence IOD, subclasses <see cref="Iod"/> to take a <see cref="DicomSequenceItem"/> instead of a <see cref="DicomDataset"/>.
    /// </summary>
    public abstract class SequenceIodBase : IodBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceIodBase"/> class.
        /// </summary>
        protected SequenceIodBase() : base(new DicomSequenceItem())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceIodBase"/> class.
        /// </summary>
        /// <param name="dicomSequenceItem">The dicom sequence item.</param>
        protected SequenceIodBase(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem)
        {
        }

		#endregion

        #region Public Properties
        /// <summary>
        /// Gets the dicom attribute collection as a dicom sequence item.
        /// </summary>
        /// <value>The dicom sequence item.</value>
        public DicomSequenceItem DicomSequenceItem
        {
            get { return base.DicomElementProvider as DicomSequenceItem; }
            set { base.DicomElementProvider = value; }
        }
        #endregion
    }
}
