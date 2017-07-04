/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: IccProfileModule.cs
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
using System.Collections.Generic;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// IccProfile Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.11.15 (Table C.11.15-1)</remarks>
	public class IccProfileModuleIod : IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="IccProfileModuleIod"/> class.
		/// </summary>	
		public IccProfileModuleIod() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="IccProfileModuleIod"/> class.
		/// </summary>
		/// <param name="IDicomAttributeProvider">The dicom attribute provider.</param>
		public IccProfileModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider) {}

		/// <summary>
		/// NOT IMPLEMENTED. Gets or sets the value of IccProfile in the underlying collection. Type 1.
		/// </summary> 		
		public object IccProfile
		{
			// TODO - Implement this.
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		/// <summary>
		/// Gets an enumeration of <see cref="DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags
		{
			get { yield return DicomTags.IccProfile; }
		}
	}
}
