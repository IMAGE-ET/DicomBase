/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ModalityLutMacro.cs
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

using UIH.RT.TMS.Dicom.Iod.Macros.ModalityLut;

namespace UIH.RT.TMS.Dicom.Iod.Macros
{
	/// <summary>
	/// ModalityLut Macro
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.11.1 (Table C.11-1b)</remarks>
	public interface IModalityLutMacro : IIodMacro
	{
		/// <summary>
		/// Gets or sets the value of ModalityLutSequence in the underlying collection. Type 1C.
		/// </summary>
		ModalityLutSequenceItem ModalityLutSequence { get; set; }

		/// <summary>
		/// Gets or sets the value of RescaleIntercept in the underlying collection. Type 1C.
		/// </summary>
		double? RescaleIntercept { get; set; }

		/// <summary>
		/// Gets or sets the value of RescaleSlope in the underlying collection. Type 1C.
		/// </summary>
		double? RescaleSlope { get; set; }

		/// <summary>
		/// Gets or sets the value of RescaleType in the underlying collection. Type 1C.
		/// </summary>
		string RescaleType { get; set; }
	}

	/// <summary>
	/// ModalityLut Macro
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.11.1 (Table C.11-1b)</remarks>
	internal class ModalityLutMacro : SequenceIodBase, IModalityLutMacro
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ModalityLutMacro"/> class.
		/// </summary>
		public ModalityLutMacro() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="ModalityLutMacro"/> class.
		/// </summary>
		/// <param name="dicomSequenceItem">The dicom sequence item.</param>
		public ModalityLutMacro(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) {}

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
	}

	namespace ModalityLut
	{
		/// <summary>
		/// ModalityLut Sequence
		/// </summary>
		/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.11.1 (Table C.11-1b)</remarks>
		public class ModalityLutSequenceItem : SequenceIodBase
		{
			/// <summary>
			/// Initializes a new instance of the <see cref="ModalityLutSequenceItem"/> class.
			/// </summary>
			public ModalityLutSequenceItem() : base() {}

			/// <summary>
			/// Initializes a new instance of the <see cref="ModalityLutSequenceItem"/> class.
			/// </summary>
			/// <param name="dicomSequenceItem">The dicom sequence item.</param>
			public ModalityLutSequenceItem(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) {}

			/// <summary>
			/// Gets or sets the value of LutDescriptor in the underlying collection. Type 1C.
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
					{
						base.DicomElementProvider[DicomTags.LutDescriptor] = null;
						return;
					}
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
			/// Gets or sets the value of ModalityLutType in the underlying collection. Type 1C.
			/// </summary>
			public string ModalityLutType
			{
				get { return base.DicomElementProvider[DicomTags.ModalityLutType].GetString(0, string.Empty); }
				set
				{
					if (string.IsNullOrEmpty(value))
					{
						base.DicomElementProvider[DicomTags.ModalityLutType] = null;
						return;
					}
					base.DicomElementProvider[DicomTags.ModalityLutType].SetString(0, value);
				}
			}

			/// <summary>
			/// Gets or sets the value of LutData in the underlying collection. Type 1C.
			/// </summary>
			public byte[] LutData
			{
				get
				{
					DicomElement element = base.DicomElementProvider[DicomTags.LutData];
					if (element.IsNull || element.IsEmpty || element.Count == 0)
						return null;
					return (byte[]) element.Values;
				}
				set
				{
					if (value == null)
					{
						base.DicomElementProvider[DicomTags.LutData] = null;
						return;
					}
					base.DicomElementProvider[DicomTags.LutData].Values = value;
				}
			}
		}
	}
}
