/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ComparisonResultType.cs
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

namespace UIH.RT.TMS.Dicom
{
    /// <summary>
    /// Types of differences when two sets of attributes are compared using <see cref="DicomDataset.Equals()"/>.
    /// </summary>
    public enum ComparisonResultType
    {
        /// <summary>
        /// Cannot be compared with the target because of its type.
        /// </summary>
        InvalidType,

        /// <summary>
        /// Source and target does not have the same set of attributes.
        /// </summary>
        DifferentAttributeSet,

        /// <summary>
        /// Attributes in the source and target have different values.
        /// </summary>
        DifferentValues
    }
}
