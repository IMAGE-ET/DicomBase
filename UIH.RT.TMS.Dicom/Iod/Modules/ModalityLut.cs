/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ModalityLut.cs
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
using UIH.RT.TMS.Dicom.Iod.Macros.ModalityLut;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// ModalityLut Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.11.1 (Table C.11-1)</remarks>
	public class ModalityLutModuleIod : IodBase, IModalityLutMacro
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ModalityLutModuleIod"/> class.
		/// </summary>	
		public ModalityLutModuleIod() : base() {}

		//TODO: Modality lut module should take ???

		/// <summary>
		/// Initializes a new instance of the <see cref="ModalityLutModuleIod"/> class.
		/// </summary>
		public ModalityLutModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider) { }

		DicomSequenceItem IIodMacro.DicomSequenceItem
		{
			get { return base.DicomElementProvider as DicomSequenceItem; }
			set { base.DicomElementProvider = value; }
		}

		/// <summary>
		/// Initializes the underlying collection to implement the module or sequence using default values.
		/// </summary>
		public void InitializeAttributes() {}

		/// <summary>
		/// Gets or sets the value of ModalityLutSequence in the underlying collection. Type 1C.
		/// </summary>
		public ModalityLutSequenceItem ModalityLutSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.ModalityLutSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}
				return new ModalityLutSequenceItem(((DicomSequenceItem[]) dicomElement.Values)[0]);
			}
			set
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.ModalityLutSequence];
				if (value == null)
				{
					base.DicomElementProvider[DicomTags.ModalityLutSequence] = null;
					return;
				}
				dicomElement.Values = new DicomSequenceItem[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Gets or sets the value of RescaleIntercept in the underlying collection. Type 1C.
		/// </summary>
		public double? RescaleIntercept
		{
			get
			{
				double result;
				if (base.DicomElementProvider[DicomTags.RescaleIntercept].TryGetFloat64(0, out result))
					return result;
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					base.DicomElementProvider[DicomTags.RescaleIntercept] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.RescaleIntercept].SetFloat64(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the value of RescaleSlope in the underlying collection. Type 1C.
		/// </summary>
		public double? RescaleSlope
		{
			get
			{
				double result;
				if (base.DicomElementProvider[DicomTags.RescaleSlope].TryGetFloat64(0, out result))
					return result;
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					base.DicomElementProvider[DicomTags.RescaleSlope] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.RescaleSlope].SetFloat64(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the value of RescaleType in the underlying collection. Type 1C.
		/// </summary>
		public string RescaleType
		{
			get { return base.DicomElementProvider[DicomTags.RescaleType].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.RescaleType] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.RescaleType].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets an enumeration of <see cref="DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags {
			get {
				yield return DicomTags.ModalityLutSequence;
				yield return DicomTags.RescaleIntercept;
				yield return DicomTags.RescaleSlope;
				yield return DicomTags.RescaleType;
			}
		}
	}
}
