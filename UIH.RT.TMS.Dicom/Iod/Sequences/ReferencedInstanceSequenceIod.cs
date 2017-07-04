/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ReferencedInstanceSequenceIod.cs
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
    /// Referenced Sop Class and Instance Sequence, consisting of Referenced SOP Class UID (0008,1150)
    /// and Referenced SOP Instance UID (0008,1155), and optionally .
    /// <para>This is mainly for the different sequences in the the Basic Film Box Relationship
    /// Module (Part 3, Table C 13.4, pg 867) such as Referenced Film Session Sequence, Referenced Image Box Sequence, Referenced Basic Annotation Box Sequence,
    /// etc., but there may be other uses for it.</para>
    /// </summary>
    /// <remarks>As per Part 3, Table C 13.4, pg 867</remarks>
    public class ReferencedInstanceSequenceIod : SequenceIodBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferencedInstanceSequenceIod"/> class.
        /// </summary>
        public ReferencedInstanceSequenceIod()
            :base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferencedInstanceSequenceIod"/> class.
        /// </summary>
        /// <param name="dicomSequenceItem">The dicom sequence item.</param>
        public ReferencedInstanceSequenceIod(DicomSequenceItem dicomSequenceItem)
            : base(dicomSequenceItem)
        {
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Uniquely identifies the referenced SOP Class. (0008,1150)
        /// </summary>
        /// <value>The referenced sop class uid.</value>
        public string ReferencedSopClassUid
        {
            get { return base.DicomElementProvider[DicomTags.ReferencedSopClassUid].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.ReferencedSopClassUid].SetString(0, value); }
        }

        /// <summary>
        /// Uniquely identifies the referenced SOP Instance. (0008,1155)
        /// </summary>
        /// <value>The referenced sop instance uid.</value>
        public string ReferencedSopInstanceUid
        {
            get { return base.DicomElementProvider[DicomTags.ReferencedSopInstanceUid].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.ReferencedSopInstanceUid].SetString(0, value); }
        }
        
       #endregion
    }

}
