/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: SoftcopyPresentationLut.cs
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
	/// SoftcopyPresentationLut Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.11.6 (Table C.11.6-1)</remarks>
	public class SoftcopyPresentationLutModuleIod : IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SoftcopyPresentationLutModuleIod"/> class.
		/// </summary>	
		public SoftcopyPresentationLutModuleIod() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="SoftcopyPresentationLutModuleIod"/> class.
		/// </summary>
		public SoftcopyPresentationLutModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider) { }

		/// <summary>
		/// Initializes the underlying collection to implement the module or sequence using default values.
		/// </summary>
		public void InitializeAttributes()
		{
			this.PresentationLutSequence = null;
			this.PresentationLutShape = PresentationLutShape.None;
		}

		/// <summary>
		/// Gets or sets the value of PresentationLutSequence in the underlying collection. Type 1C.
		/// </summary>
		public PresentationLutSequenceItem PresentationLutSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.PresentationLutSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}
				return new PresentationLutSequenceItem(((DicomSequenceItem[]) dicomElement.Values)[0]);
			}
			set
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.PresentationLutSequence];
				if (value == null)
				{
					base.DicomElementProvider[DicomTags.PresentationLutSequence] = null;
					return;
				}
				dicomElement.Values = new DicomSequenceItem[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Gets or sets the value of PresentationLutShape in the underlying collection. Type 1C.
		/// </summary>
		public PresentationLutShape PresentationLutShape
		{
			get { return ParseEnum(base.DicomElementProvider[DicomTags.PresentationLutShape].GetString(0, string.Empty), PresentationLutShape.None); }
			set
			{
				if (value == PresentationLutShape.None)
				{
					base.DicomElementProvider[DicomTags.PresentationLutShape] = null;
					return;
				}
				SetAttributeFromEnum(base.DicomElementProvider[DicomTags.PresentationLutShape], value);
			}
		}

		/// <summary>
		/// PresentationLut Sequence
		/// </summary>
		/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.11.6 (Table C.11.6-1)</remarks>
		public class PresentationLutSequenceItem : SequenceIodBase
		{
			/// <summary>
			/// Initializes a new instance of the <see cref="PresentationLutSequenceItem"/> class.
			/// </summary>
			public PresentationLutSequenceItem() : base() {}

			/// <summary>
			/// Initializes a new instance of the <see cref="PresentationLutSequenceItem"/> class.
			/// </summary>
			/// <param name="dicomSequenceItem">The dicom sequence item.</param>
			public PresentationLutSequenceItem(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) {}

			/// <summary>
			/// Gets or sets the value of LutDescriptor in the underlying collection. Type 1.
			/// </summary>
			public int[] LutDescriptor
			{
				get
				{
					int[] result = new int[3];
					if (base.DicomElementProvider[DicomTags.LutDescriptor].TryGetInt32(0, out result[0]))
						if (base.DicomElementProvider[DicomTags.LutDescriptor].TryGetInt32(1, out result[1]))
							if (base.DicomElementProvider[DicomTags.LutDescriptor].TryGetInt32(2, out result[2]))
								return result;
					return null;
				}
				set
				{
					if (value == null || value.Length != 3)
						throw new ArgumentNullException("value", "LutDescriptor is Type 1 Required.");
					base.DicomElementProvider[DicomTags.LutDescriptor].SetInt32(0, value[0]);
					base.DicomElementProvider[DicomTags.LutDescriptor].SetInt32(1, value[1]);
					base.DicomElementProvider[DicomTags.LutDescriptor].SetInt32(2, value[2]);
				}
			}

			/// <summary>
			/// Gets or sets the value of LutExplanation in the underlying collection. Type 3.
			/// </summary>
			public string LutExplanation
			{
				get { return base.DicomElementProvider[DicomTags.LutExplanation].GetString(0, string.Empty); }
				set
				{
					if (string.IsNullOrEmpty(value))
					{
						base.DicomElementProvider[DicomTags.LutExplanation] = null;
						return;
					}
					base.DicomElementProvider[DicomTags.LutExplanation].SetString(0, value);
				}
			}

			/// <summary>
			/// Gets or sets the value of LutData in the underlying collection. Type 1.
			/// </summary>
			public byte[] LutData
			{
				get
				{
					DicomElement element = base.DicomElementProvider[DicomTags.LutData];
					if (element.IsNull || element.IsEmpty)
						return null;
					return (byte[]) element.Values;
				}
				set
				{
					if (value == null)
						throw new ArgumentOutOfRangeException("value", "LutData is Type 1 Required.");
					base.DicomElementProvider[DicomTags.LutData].Values = value;
				}
			}
		}

		/// <summary>
		/// Gets an enumeration of <see cref="DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags {
			get {
				yield return DicomTags.PresentationLutSequence;
				yield return DicomTags.PresentationLutShape;
			}
		}
	}
}
