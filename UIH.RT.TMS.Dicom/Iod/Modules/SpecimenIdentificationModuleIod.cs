/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: SpecimenIdentificationModuleIod.cs
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
using UIH.RT.TMS.Dicom.Iod.Sequences;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// SpecimenIdentification Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.7.1.2 (Table C.7-2a)</remarks>
	public class SpecimenIdentificationModuleIod : IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SpecimenIdentificationModuleIod"/> class.
		/// </summary>	
		public SpecimenIdentificationModuleIod() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="SpecimenIdentificationModuleIod"/> class.
		/// </summary>
		public SpecimenIdentificationModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider) {}

		/// <summary>
		/// Initializes the underlying collection to implement the module or sequence using default values.
		/// </summary>
		public void InitializeAttributes()
		{
			this.SpecimenAccessionNumber = "1";
			this.SpecimenSequence = null;
		}

		/// <summary>
		/// Checks if this module appears to be non-empty.
		/// </summary>
		/// <returns>True if the module appears to be non-empty; False otherwise.</returns>
		public bool HasValues()
		{
			if (this.SpecimenSequence == null && string.IsNullOrEmpty(this.SpecimenAccessionNumber))
				return false;
			return true;
		}

		/// <summary>
		/// Gets or sets the value of SpecimenAccessionNumber in the underlying collection. Type 1.
		/// </summary>
		public string SpecimenAccessionNumber
		{
			get { return base.DicomElementProvider[DicomTags.SpecimenAccessionNumberRetired].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("value", "SpecimenAccessionNumber is Type 1 Required.");
				base.DicomElementProvider[DicomTags.SpecimenAccessionNumberRetired].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of SpecimenSequence in the underlying collection. Type 2.
		/// </summary>
		public SpecimenSequence[] SpecimenSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.SpecimenSequenceRetired];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}

				SpecimenSequence[] result = new SpecimenSequence[dicomElement.Count];
				DicomSequenceItem[] items = (DicomSequenceItem[]) dicomElement.Values;
				for (int n = 0; n < items.Length; n++)
					result[n] = new SpecimenSequence(items[n]);

				return result;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.DicomElementProvider[DicomTags.SpecimenSequenceRetired].SetNullValue();
					return;
				}

				DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
				for (int n = 0; n < value.Length; n++)
					result[n] = value[n].DicomSequenceItem;

				base.DicomElementProvider[DicomTags.SpecimenSequenceRetired].Values = result;
			}
		}
	}
}
