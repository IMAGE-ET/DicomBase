/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: PresentationStateShutter.cs
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

using System.Collections.Generic;
using UIH.RT.TMS.Dicom.Iod.Macros;
using UIH.RT.TMS.Dicom.Iod.Sequences;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// PresentationStateShutter Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.11.12 (Table C.11.12-1)</remarks>
	public class PresentationStateShutterModuleIod : IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PresentationStateShutterModuleIod"/> class.
		/// </summary>	
		public PresentationStateShutterModuleIod() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="PresentationStateShutterModuleIod"/> class.
		/// </summary>
		public PresentationStateShutterModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider) { }

		/// <summary>
		/// Initializes the underlying collection to implement the module or sequence using default values.
		/// </summary>
		public void InitializeAttributes()
		{
			this.ShutterPresentationColorCielabValue = null;
			this.ShutterPresentationValue = null;
		}

		/// <summary>
		/// Gets or sets the shutter presentation value.  Type 1C.
		/// </summary>
		public int? ShutterPresentationValue
		{
			get
			{
				DicomElement element;
				if (base.DicomElementProvider.TryGetAttribute(DicomTags.ShutterPresentationValue, out element))
					return element.GetInt32(0, 0);
				else
					return null;
			}
			set
			{
				if (!value.HasValue)
					base.DicomElementProvider[DicomTags.ShutterPresentationValue] = null;
				else
					base.DicomElementProvider[DicomTags.ShutterPresentationValue].SetInt32(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the shutter presentation color value.  Type 1C.
		/// </summary>
		public CIELabColor? ShutterPresentationColorCielabValue
		{
			get
			{
				DicomElement element = base.DicomElementProvider[DicomTags.ShutterPresentationColorCielabValue];
				if (element.IsEmpty || element.IsNull)
					return null;

				ushort[] values = element.Values as ushort[];
				if (values != null && values.Length >= 3)
					return new CIELabColor(values[0], values[1], values[2]);
				else
					return null;
			}
			set
			{
				if (!value.HasValue)
					base.DicomElementProvider[DicomTags.ShutterPresentationColorCielabValue] = null;
				else
					base.DicomElementProvider[DicomTags.ShutterPresentationColorCielabValue].Values = value.Value.ToArray();
			}
		}

		/// <summary>
		/// Gets an enumeration of <see cref="DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags {
			get {
				yield return DicomTags.ShutterPresentationColorCielabValue;
				yield return DicomTags.ShutterPresentationValue;
			}
		}
	}
}
