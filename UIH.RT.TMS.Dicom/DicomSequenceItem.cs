/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: DicomSequenceItem.cs
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

using UIH.RT.TMS.Dicom;

namespace UIH.RT.TMS.Dicom
{
    /// <summary>
    /// A class representing a DICOM Sequence Item.
    /// </summary>
    public class DicomSequenceItem : DicomDataset
    {
        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public DicomSequenceItem() : base(0x00000000,0xFFFFFFFF)
        {
        }

        /// <summary>
        /// Internal constructor used when making a copy of a <see cref="DicomDataset"/>.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="copyBinary"></param>
        /// <param name="copyPrivate"></param>
        /// <param name="copyUnknown"></param>
        internal DicomSequenceItem(DicomDataset source, bool copyBinary, bool copyPrivate, bool copyUnknown)
            : base(source, copyBinary, copyPrivate, copyUnknown)
        {
        }
        #endregion

        #region Public Overridden Methods
        /// <summary>
        /// Create a copy of this DicomSequenceItem.
        /// </summary>
        /// <returns>The copied DicomSequenceItem.</returns>
        public override DicomDataset Copy()
        {
        	return Copy(true, true, true);
        }

    	/// <summary>
    	/// Creates a copy of this DicomSequenceItem.
    	/// </summary>
    	/// <param name="copyBinary">When set to false, the copy will not include <see cref="DicomElement"/>
    	/// instances that are of type <see cref="DicomElementOb"/>, <see cref="DicomElementOw"/>,
    	/// or <see cref="DicomElementOf"/>.</param>
    	/// <param name="copyPrivate">When set to false, the copy will not include Private tags</param>
    	/// <param name="copyUnknown">When set to false, the copy will not include UN VR tags</param>
    	/// <returns>The copied DicomSequenceItem.</returns>
    	public override DicomDataset Copy(bool copyBinary, bool copyPrivate, bool copyUnknown)
        {
            return new DicomSequenceItem(this,copyBinary,copyPrivate,copyUnknown);
        }
        #endregion
    }
}
