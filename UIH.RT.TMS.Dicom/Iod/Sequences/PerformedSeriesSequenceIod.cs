/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: PerformedSeriesSequenceIod.cs
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
    /// Performed Series Sequence.  
    /// </summary>
    /// <remarks>As per Part 3, Table C4.15, pg 256</remarks>
    public class PerformedSeriesSequenceIod : SequenceIodBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PerformedSeriesSequenceIod"/> class.
        /// </summary>
        public PerformedSeriesSequenceIod()
            :base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PerformedSeriesSequenceIod"/> class.
        /// </summary>
        /// <param name="dicomSequenceItem">The dicom sequence item.</param>
        public PerformedSeriesSequenceIod(DicomSequenceItem dicomSequenceItem)
            : base(dicomSequenceItem)
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Name of the physician(s) administering this Series.
        /// </summary>
        /// <value>The name of the performing physicians.</value>
        public PersonName PerformingPhysiciansName
        {
            get { return new PersonName(base.DicomElementProvider[DicomTags.PerformingPhysiciansName].GetString(0, String.Empty)); }
            set { base.DicomElementProvider[DicomTags.PerformingPhysiciansName].SetString(0, value.ToString()); }
        }

        /// <summary>
        /// Identification of the physician(s) administering the Series. One or more items 
        /// shall be included in this sequence. If more than one Item, the number and
        /// order shall correspond to the value of Performing Physician�s Name (0008,1050), if present.
        /// </summary>
        /// <value>The performing physician identification sequence list.</value>
        public SequenceIodList<PersonIdentificationMacro> PerformingPhysicianIdentificationSequenceList
        {
            get
            {
                return new SequenceIodList<PersonIdentificationMacro>(base.DicomElementProvider[DicomTags.PerformingPhysicianIdentificationSequence] as DicomElementSq);
            }
        }

        /// <summary>
        /// Gets or sets the name of the operators.
        /// </summary>
        /// <value>The name of the operators.</value>
        public PersonName OperatorsName
        {
            get { return new PersonName(base.DicomElementProvider[DicomTags.OperatorsName].GetString(0, String.Empty)); }
            set { base.DicomElementProvider[DicomTags.OperatorsName].SetString(0, value.ToString()); }
        }

        /// <summary>
        /// Identification of the operator(s) supporting the Series. One or more items shall be 
        /// included in this sequence. If more than one Item, the number and
        /// order shall correspond to the value of Operators� Name (0008,1070), if present.
        /// </summary>
        /// <value>The operator identification sequence list.</value>
        public SequenceIodList<PersonIdentificationMacro> OperatorIdentificationSequenceList
        {
            get
            {
                return new SequenceIodList<PersonIdentificationMacro>(base.DicomElementProvider[DicomTags.OperatorIdentificationSequence] as DicomElementSq);
            }
        }

        /// <summary>
        /// User-defined description of the conditions under which the Series was performed. 
        /// <para>Note: This element conveys series-specific protocol identification and may or may not be identical 
        /// to the one presented in the Performed Protocol Code Sequence (0040,0260).</para>
        /// </summary>
        /// <value>The name of the protocol.</value>
        public string ProtocolName
        {
            get { return base.DicomElementProvider[DicomTags.ProtocolName].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.ProtocolName].SetString(0, value); }
        }

        public string SeriesInstanceUid
        {
            get { return base.DicomElementProvider[DicomTags.SeriesInstanceUid].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.SeriesInstanceUid].SetString(0, value); }
        }

        public string SeriesDescription
        {
            get { return base.DicomElementProvider[DicomTags.SeriesDescription].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.SeriesDescription].SetString(0, value); }
        }

        /// <summary>
        /// Title of the DICOM Application Entity where the Images and other Composite SOP 
        /// Instances in this Series may be retrieved on the network.
        /// <para>Note: The duration for which this location remains valid is unspecified.</para>
        /// </summary>
        /// <value>The retrieve ae title.</value>
        public string RetrieveAeTitle
        {
            get { return base.DicomElementProvider[DicomTags.RetrieveAeTitle].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.RetrieveAeTitle].SetString(0, value); }
        }

        /// <summary>
        /// A Sequence that provides reference to one or more sets of Image SOP Class/SOP 
        /// Instance pairs created during the acquisition of the procedure step.
        /// The sequence may have zero or more Items.
        /// </summary>
        /// <value>The referenced image sequence list.</value>
        public SequenceIodList<ReferencedInstanceSequenceIod> ReferencedImageSequenceList
        {
            get
            {
                return new SequenceIodList<ReferencedInstanceSequenceIod>(base.DicomElementProvider[DicomTags.ReferencedImageSequence] as DicomElementSq);
            }
        }

        /// <summary>
        /// Uniquely identifies instances, other than images, of any SOP Class that conforms to the DICOM 
        /// Composite IOD Information Model, such as Waveforms, Presentation States or Structured 
        /// Reports, created during the acquisition of the procedure step. The sequence may have zero or
        /// more Items.
        /// </summary>
        /// <value>The referenced non image composite sop instance sequence list.</value>
        public SequenceIodList<ReferencedInstanceSequenceIod> ReferencedNonImageCompositeSopInstanceSequenceList
        {
            get
            {
                return new SequenceIodList<ReferencedInstanceSequenceIod>(base.DicomElementProvider[DicomTags.ReferencedNonImageCompositeSopInstanceSequence] as DicomElementSq);
            }
        }
        
       #endregion
    }

}
