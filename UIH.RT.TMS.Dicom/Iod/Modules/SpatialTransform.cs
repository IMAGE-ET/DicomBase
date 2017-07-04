/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: SpatialTransform.cs
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
using UIH.RT.TMS.Dicom.Iod.Macros;
using UIH.RT.TMS.Dicom.Iod.Sequences;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// SpatialTransform Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.10.6 (Table C.10-6)</remarks>
	public class SpatialTransformModuleIod : IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SpatialTransformModuleIod"/> class.
		/// </summary>	
		public SpatialTransformModuleIod() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="SpatialTransformModuleIod"/> class.
		/// </summary>
		public SpatialTransformModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider) { }

		/// <summary>
		/// Gets or sets the value of ImageRotation in the underlying collection. Type 1.
		/// </summary>
		public int ImageRotation
		{
			get { return base.DicomElementProvider[DicomTags.ImageRotation].GetInt32(0, 0); }
			set
			{
				if (value % 90 != 0)
					throw new ArgumentOutOfRangeException("value", "ImageRotation must be one of 0, 90, 180 or 270.");
				base.DicomElementProvider[DicomTags.ImageRotation].SetInt32(0, ((value % 360) + 360) % 360); // this ensures that the value stored is positive and < 360
			}
		}

		/// <summary>
		/// Gets or sets the value of ImageHorizontalFlip in the underlying collection. Type 1.
		/// </summary>
		public ImageHorizontalFlip ImageHorizontalFlip
		{
			get { return ParseEnum(base.DicomElementProvider[DicomTags.ImageHorizontalFlip].GetString(0, string.Empty), ImageHorizontalFlip.None); }
			set
			{
				if (value == ImageHorizontalFlip.None)
					throw new ArgumentOutOfRangeException("value", "ImageHorizontalFlip is Type 1 Required.");
				SetAttributeFromEnum(base.DicomElementProvider[DicomTags.ImageHorizontalFlip], value);
			}
		}

		/// <summary>
		/// Gets an enumeration of <see cref="DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags {
			get {
				yield return DicomTags.ImageHorizontalFlip;
				yield return DicomTags.ImageRotation;
			}
		}
	}

	/// <summary>
	/// Enumerated values for the <see cref="DicomTags.ImageHorizontalFlip"/> attribute describing whether or not to flip the image horizontally.
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.10.6 (Table C.10-6)</remarks>
	public enum ImageHorizontalFlip {
		Y,
		N,

		/// <summary>
		/// Represents the null value, which is equivalent to the unknown status.
		/// </summary>
		None
	}
}
