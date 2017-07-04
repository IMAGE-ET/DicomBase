/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: VoiLut.cs
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
using UIH.RT.TMS.Dicom;
using UIH.RT.TMS.Dicom.Iod.Macros.VoiLut;

namespace UIH.RT.TMS.Dicom.Iod.Macros
{
	/// <summary>
	/// VoiLut Macro
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.11.2 (Table C.11-2b)</remarks>
	public interface IVoiLutMacro : IIodMacro
	{
		/// <summary>
		/// Gets or sets the value of VoiLutSequence in the underlying collection. Type 1C.
		/// </summary>
		VoiLutSequenceItem[] VoiLutSequence { get; set; }

		/// <summary>
		/// Gets or sets the value of WindowCenter in the underlying collection. Type 1C.
		/// </summary>
		double[] WindowCenter { get; set; }

		/// <summary>
		/// Gets or sets the value of WindowWidth in the underlying collection. Type 1C.
		/// </summary>
		double[] WindowWidth { get; set; }

		/// <summary>
		/// Gets or sets the value of WindowCenterWidthExplanation in the underlying collection. Type 3.
		/// </summary>
		string[] WindowCenterWidthExplanation { get; set; }

		/// <summary>
		/// Gets or sets the value of VoiLutFunction in the underlying collection. Type 3.
		/// </summary>
		VoiLutFunction VoiLutFunction { get; set; }

		/// <summary>
		/// Gets the number of VOI Data LUTs included in this sequence.
		/// </summary>
		long CountDataLuts { get; }

		/// <summary>
		/// Gets the number of VOI Windows included in this sequence.
		/// </summary>
		long CountWindows { get; }
	}

	/// <summary>
	/// VoiLut Macro
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.11.2 (Table C.11-2b)</remarks>
	internal class VoiLutMacro : SequenceIodBase, IVoiLutMacro
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="VoiLutMacro"/> class.
		/// </summary>
		public VoiLutMacro() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="VoiLutMacro"/> class.
		/// </summary>
		/// <param name="dicomSequenceItem">The dicom sequence item.</param>
		public VoiLutMacro(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) {}

		public void InitializeAttributes() {}

		/// <summary>
		/// Gets or sets the value of VoiLutSequence in the underlying collection. Type 1C.
		/// </summary>
		public VoiLutSequenceItem[] VoiLutSequence
		{
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

	namespace VoiLut
	{
		/// <summary>
		/// VoiLut Sequence
		/// </summary>
		/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.11.2 (Table C.11-2b)</remarks>
		public class VoiLutSequenceItem : SequenceIodBase
		{
			/// <summary>
			/// Initializes a new instance of the <see cref="VoiLutSequenceItem"/> class.
			/// </summary>
			public VoiLutSequenceItem() : base() {}

			/// <summary>
			/// Initializes a new instance of the <see cref="VoiLutSequenceItem"/> class.
			/// </summary>
			/// <param name="dicomSequenceItem">The dicom sequence item.</param>
			public VoiLutSequenceItem(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) {}

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
			/// Gets or sets the value of LutData in the underlying collection. Type 1C.
			/// </summary>
			public ushort[] LutData
			{
				get
				{
					DicomElement element = base.DicomElementProvider[DicomTags.LutData];
					if (element.IsNull || element.IsEmpty || element.Count == 0)
						return null;
					return (ushort[])element.Values;
				}
				set
				{
					if (value == null || value.Length == 0)
					{
						base.DicomElementProvider[DicomTags.LutData] = null;
						return;
					}
					base.DicomElementProvider[DicomTags.LutData].Values = value;
				}
			}
		}

		/// <summary>
		/// Enumerated values for the <see cref="DicomTags.VoiLutFunction"/> attribute describing
		/// a VOI LUT function to apply to the <see cref="IVoiLutMacro.WindowCenter"/> and <see cref="IVoiLutMacro.WindowWidth"/>.
		/// </summary>
		/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.11.2 (Table C.11-2b)</remarks>
		public enum VoiLutFunction {
			Linear,
			Sigmoid,

			/// <summary>
			/// Represents the null value, which is equivalent to the unknown status.
			/// </summary>
			None
		}
	}
}
