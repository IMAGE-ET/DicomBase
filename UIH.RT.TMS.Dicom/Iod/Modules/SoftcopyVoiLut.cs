/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: SoftcopyVoiLut.cs
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
using UIH.RT.TMS.Dicom.Iod.Macros.VoiLut;
using UIH.RT.TMS.Dicom.Iod.Sequences;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// SoftcopyVoiLut Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.11.8 (Table C.11.8-1)</remarks>
	public class SoftcopyVoiLutModuleIod : IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SoftcopyVoiLutModuleIod"/> class.
		/// </summary>	
		public SoftcopyVoiLutModuleIod() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="SoftcopyVoiLutModuleIod"/> class.
		/// </summary>
		public SoftcopyVoiLutModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider) { }

		/// <summary>
		/// Gets or sets the value of SoftcopyVoiLutSequence in the underlying collection. Type 1.
		/// </summary>
		public SoftcopyVoiLutSequenceItem[] SoftcopyVoiLutSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.SoftcopyVoiLutSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
					return null;

				SoftcopyVoiLutSequenceItem[] result = new SoftcopyVoiLutSequenceItem[dicomElement.Count];
				DicomSequenceItem[] items = (DicomSequenceItem[]) dicomElement.Values;
				for (int n = 0; n < items.Length; n++)
					result[n] = new SoftcopyVoiLutSequenceItem(items[n]);

				return result;
			}
			set
			{
				if (value == null || value.Length == 0)
					throw new ArgumentNullException("value", "SoftcopyVoiLutSequence is Type 1 Required.");

				DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
				for (int n = 0; n < value.Length; n++)
					result[n] = value[n].DicomSequenceItem;

				base.DicomElementProvider[DicomTags.SoftcopyVoiLutSequence].Values = result;
			}
		}

		/// <summary>
		/// SoftcopyVoiLut Sequence
		/// </summary>
		/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.11.8 (Table C.11-8)</remarks>
		public class SoftcopyVoiLutSequenceItem : SequenceIodBase, IVoiLutMacro {
			/// <summary>
			/// Initializes a new instance of the <see cref="SoftcopyVoiLutSequenceItem"/> class.
			/// </summary>
			public SoftcopyVoiLutSequenceItem() : base() { }

			/// <summary>
			/// Initializes a new instance of the <see cref="SoftcopyVoiLutSequenceItem"/> class.
			/// </summary>
			/// <param name="dicomSequenceItem">The dicom sequence item.</param>
			public SoftcopyVoiLutSequenceItem(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) { }

			public void InitializeAttributes() { }

			/// <summary>
			/// Gets or sets the value of ReferencedImageSequence in the underlying collection. Type 1C.
			/// </summary>
			public ImageSopInstanceReferenceMacro[] ReferencedImageSequence
			{
				get
				{
					DicomElement dicomElement = base.DicomElementProvider[DicomTags.ReferencedImageSequence];
					if (dicomElement.IsNull || dicomElement.Count == 0)
					{
						return null;
					}

					ImageSopInstanceReferenceMacro[] result = new ImageSopInstanceReferenceMacro[dicomElement.Count];
					DicomSequenceItem[] items = (DicomSequenceItem[]) dicomElement.Values;
					for (int n = 0; n < items.Length; n++)
						result[n] = new ImageSopInstanceReferenceMacro(items[n]);

					return result;
				}
				set
				{
					if (value == null || value.Length == 0)
					{
						base.DicomElementProvider[DicomTags.ReferencedImageSequence] = null;
						return;
					}

					DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
					for (int n = 0; n < value.Length; n++)
						result[n] = value[n].DicomSequenceItem;

					base.DicomElementProvider[DicomTags.ReferencedImageSequence].Values = result;
				}
			}

			/// <summary>
			/// Gets or sets the value of VoiLutSequence in the underlying collection. Type 1C.
			/// </summary>
			public VoiLutSequenceItem[] VoiLutSequence {
				get
				{
					DicomElement dicomElement = base.DicomElementProvider[DicomTags.VoiLutSequence];
					if (dicomElement.IsNull || dicomElement.Count == 0)
					{
						return null;
					}

					VoiLutSequenceItem[] result = new VoiLutSequenceItem[dicomElement.Count];
					DicomSequenceItem[] items = (DicomSequenceItem[]) dicomElement.Values;
					for (int n = 0; n < items.Length; n++)
						result[n] = new VoiLutSequenceItem(items[n]);

					return result;
				}
				set
				{
					if (value == null || value.Length == 0)
					{
						base.DicomElementProvider[DicomTags.VoiLutSequence] = null;
						return;
					}

					DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
					for (int n = 0; n < value.Length; n++)
						result[n] = value[n].DicomSequenceItem;

					base.DicomElementProvider[DicomTags.VoiLutSequence].Values = result;
				}
			}

			/// <summary>
			/// Gets or sets the value of WindowCenter in the underlying collection. Type 1C.
			/// </summary>
			public double[] WindowCenter
			{
				get
				{
					DicomElement element = base.DicomElementProvider[DicomTags.WindowCenter];
					if (element.IsNull || element.IsEmpty || element.Count == 0)
						return null;

					double[] values = new double[element.Count];
					for (int n = 0; n < element.Count; n++)
						values[n] = element.GetFloat64(n, 0);
					return values;
				}
				set
				{
					if (value == null || value.Length == 0)
					{
						base.DicomElementProvider[DicomTags.WindowCenter] = null;
						return;
					}

					DicomElement element = base.DicomElementProvider[DicomTags.WindowCenter];
					for (int n = value.Length - 1; n >= 0; n--)
						element.SetFloat64(n, value[n]);
				}
			}

			/// <summary>
			/// Gets or sets the value of WindowWidth in the underlying collection. Type 1C.
			/// </summary>
			public double[] WindowWidth
			{
				get
				{
					DicomElement element = base.DicomElementProvider[DicomTags.WindowWidth];
					if (element.IsNull || element.IsEmpty || element.Count == 0)
						return null;

					double[] values = new double[element.Count];
					for (int n = 0; n < element.Count; n++)
						values[n] = element.GetFloat64(n, 0);
					return values;
				}
				set
				{
					if (value == null || value.Length == 0)
					{
						base.DicomElementProvider[DicomTags.WindowWidth] = null;
						return;
					}

					DicomElement element = base.DicomElementProvider[DicomTags.WindowWidth];
					for (int n = value.Length - 1; n >= 0; n--)
						element.SetFloat64(n, value[n]);
				}
			}

			/// <summary>
			/// Gets or sets the value of WindowCenterWidthExplanation in the underlying collection. Type 3.
			/// </summary>
			public string[] WindowCenterWidthExplanation
			{
				get
				{
					DicomElement element = base.DicomElementProvider[DicomTags.WindowCenterWidthExplanation];
					if (element.IsNull || element.IsEmpty || element.Count == 0)
						return null;
					return (string[]) element.Values;
				}
				set
				{
					if (value == null || value.Length == 0)
					{
						base.DicomElementProvider[DicomTags.WindowCenterWidthExplanation] = null;
						return;
					}
					base.DicomElementProvider[DicomTags.WindowCenterWidthExplanation].Values = value;
				}
			}

			/// <summary>
			/// Gets or sets the value of VoiLutFunction in the underlying collection. Type 3.
			/// </summary>
			public VoiLutFunction VoiLutFunction
			{
				get { return ParseEnum(base.DicomElementProvider[DicomTags.VoiLutFunction].GetString(0, string.Empty), VoiLutFunction.None); }
				set
				{
					if (value == VoiLutFunction.None)
					{
						base.DicomElementProvider[DicomTags.VoiLutFunction] = null;
						return;
					}
					SetAttributeFromEnum(base.DicomElementProvider[DicomTags.VoiLutFunction], value);
				}
			}

			/// <summary>
			/// Gets the number of VOI Data LUTs included in this sequence.
			/// </summary>
			public long CountDataLuts
			{
				get
				{
					DicomElement element = base.DicomElementProvider[DicomTags.VoiLutSequence];
					if (element.IsNull || element.IsEmpty)
						return 0;
					return element.Count;
				}
			}

			/// <summary>
			/// Gets the number of VOI Windows included in this sequence.
			/// </summary>
			public long CountWindows
			{
				get
				{
					DicomElement element = base.DicomElementProvider[DicomTags.WindowCenter];
					if (element.IsNull || element.IsEmpty)
						return 0;
					return element.Count;
				}
			}
		}

		/// <summary>
		/// Gets an enumeration of <see cref="DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags {
			get {
				yield return DicomTags.SoftcopyVoiLutSequence;
			}
		}
	}
}
