/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: DicomElementComparisonResult.cs
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
using System.Xml.Serialization;

namespace UIH.RT.TMS.Dicom
{
    /// <summary>
    /// Represents the result of the comparison when two sets of attributes are compared using <see cref="DicomDataset.Equals()"/>.
    /// </summary>
    public class DicomElementComparisonResult
    {
        #region Public Overrides
		public override string  ToString()
		{
			return Details;
		}
    	#endregion

        #region Public Properties

    	/// <summary>
    	/// Type of differences.
    	/// </summary>
    	[XmlAttribute]
    	public ComparisonResultType ResultType { get; set; }

    	/// <summary>
    	/// The name of the offending tag. This can be null if the difference is not tag specific.
    	/// </summary>
    	[XmlAttribute]
    	public String TagName { get; set; }

    	/// <summary>
    	/// Detailed text describing the problem.
    	/// </summary>
    	public string Details { get; set; }

    	#endregion

    }
}
