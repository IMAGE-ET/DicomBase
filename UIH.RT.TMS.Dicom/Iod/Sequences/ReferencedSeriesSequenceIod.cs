/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ReferencedSeriesSequenceIod.cs
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

namespace UIH.RT.TMS.Dicom.Iod.Sequences
{
    /// <summary>
    /// Referenced Series Sequence.  
    /// </summary>
    /// <remarks>As per Part 3, Table 10.4, pg 78</remarks>
    public class ReferencedSeriesSequenceIod : SequenceIodBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferencedSeriesSequenceIod"/> class.
        /// </summary>
        public ReferencedSeriesSequenceIod()
            :base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferencedSeriesSequenceIod"/> class.
        /// </summary>
        /// <param name="dicomSequenceItem">The dicom sequence item.</param>
        public ReferencedSeriesSequenceIod(DicomSequenceItem dicomSequenceItem)
            : base(dicomSequenceItem)
        {
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Unique identifier of the Series containing the referenced Instances.
        /// </summary>
        /// <value>The series instance uid.</value>
        public string SeriesInstanceUid
        {
            get { return base.DicomElementProvider[DicomTags.SeriesInstanceUid].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.SeriesInstanceUid].SetString(0, value); }
        }

        /// <summary>
        /// Sequence of Items each providing a reference to an Instance that is part of the
        /// Series defined by Series Instance UID (0020,000E) in the enclosing Item. 
        /// One or more Items shall be present.
        /// </summary>
        /// <value>The referenced film session sequence list.</value>
        public SequenceIodList<ReferencedInstanceSequenceIod> ReferencedFilmSessionSequenceList
        {
            get
            {
                return new SequenceIodList<ReferencedInstanceSequenceIod>(base.DicomElementProvider[DicomTags.ReferencedInstanceSequence] as DicomElementSq);
            }
        }        
       #endregion
    }

}
