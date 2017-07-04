/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: MaskSubtractionSequenceIod.cs
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

namespace UIH.RT.TMS.Dicom.Iod.Sequences
{
	/// <summary>
	/// Mask Subtraction Sequence Item
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2011, Part 3, Section C.7.6.10 (Table C.7-16)</remarks>
	public class MaskSubtractionSequenceIod : SequenceIodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MaskModuleIod"/> class.
		/// </summary>	
		public MaskSubtractionSequenceIod() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="MaskModuleIod"/> class.
		/// </summary>
		public MaskSubtractionSequenceIod(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) {}

		/// <summary>
		/// Gets or sets the value of MaskOperation in the underlying collection. Type 1.
		/// </summary>
		public MaskOperation MaskOperation
		{
			get { return ParseEnum(DicomElementProvider[DicomTags.MaskOperation].GetString(0, string.Empty), MaskOperation.None); }
			set
			{
				if (value == MaskOperation.None)
					throw new ArgumentOutOfRangeException("value", "MaskOperation is Type 1 Required.");
				SetAttributeFromEnum(DicomElementProvider[DicomTags.MaskOperation], value);
			}
		}

		/// <summary>
		/// Gets or sets the value of SubtractionItemId in the underlying collection. Type 1C.
		/// </summary>
		public ushort? SubtractionItemId
		{
			get
			{
				ushort result;
				if (DicomElementProvider[DicomTags.SubtractionItemId].TryGetUInt16(0, out result))
					return result;
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					DicomElementProvider[DicomTags.SubtractionItemId] = null;
					return;
				}
				DicomElementProvider[DicomTags.SubtractionItemId].SetUInt16(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ApplicableFrameRange in the underlying collection. Type 1C.
		/// </summary>
		public int[] ApplicableFrameRange
		{
			get
			{
				var dicomAttribute = DicomElementProvider[DicomTags.ApplicableFrameRange];
				if (dicomAttribute.IsNull || dicomAttribute.IsEmpty)
					return null;

				var result = new int[dicomAttribute.Count];
				for (var n = 0; n < result.Length; n++)
					result[n] = dicomAttribute.GetInt32(n, 0);
				return result;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					DicomElementProvider[DicomTags.ApplicableFrameRange] = null;
					return;
				}

				var dicomAttribute = DicomElementProvider[DicomTags.ApplicableFrameRange];
				for (var n = 0; n < value.Length; n++)
					dicomAttribute.SetInt32(n, value[n]);
			}
		}

		/// <summary>
		/// Gets or sets the value of MaskFrameNumbers in the underlying collection. Type 1C.
		/// </summary>
		public int[] MaskFrameNumbers
		{
			get
			{
				var dicomAttribute = DicomElementProvider[DicomTags.MaskFrameNumbers];
				if (dicomAttribute.IsNull || dicomAttribute.IsEmpty)
					return null;

				var result = new int[dicomAttribute.Count];
				for (var n = 0; n < result.Length; n++)
					result[n] = dicomAttribute.GetInt32(n, 0);
				return result;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					DicomElementProvider[DicomTags.MaskFrameNumbers] = null;
					return;
				}

				var dicomAttribute = DicomElementProvider[DicomTags.MaskFrameNumbers];
				for (var n = 0; n < value.Length; n++)
					dicomAttribute.SetInt32(n, value[n]);
			}
		}

		/// <summary>
		/// Gets or sets the value of ContrastFrameAveraging in the underlying collection. Type 3.
		/// </summary>
		public int? ContrastFrameAveraging
		{
			get
			{
				int result;
				if (DicomElementProvider[DicomTags.ContrastFrameAveraging].TryGetInt32(0, out result))
					return result;
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					DicomElementProvider[DicomTags.ContrastFrameAveraging] = null;
					return;
				}
				DicomElementProvider[DicomTags.ContrastFrameAveraging].SetInt32(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the value of MaskSubPixelShift in the underlying collection. Type 3.
		/// </summary>
		public double[] MaskSubPixelShift
		{
			get
			{
				var result = new double[2];
				if (DicomElementProvider[DicomTags.MaskSubPixelShift].TryGetFloat64(0, out result[0])
				    && DicomElementProvider[DicomTags.MaskSubPixelShift].TryGetFloat64(1, out result[1]))
					return result;
				return null;
			}
			set
			{
				if (value == null || value.Length != 2)
				{
					DicomElementProvider[DicomTags.MaskSubPixelShift] = null;
					return;
				}
				DicomElementProvider[DicomTags.MaskSubPixelShift].SetFloat64(0, value[0]);
				DicomElementProvider[DicomTags.MaskSubPixelShift].SetFloat64(1, value[1]);
			}
		}

		/// <summary>
		/// Gets or sets the value of TidOffset in the underlying collection. Type 2C.
		/// </summary>
		public int? TidOffset
		{
			get
			{
				int result;
				if (DicomElementProvider[DicomTags.TidOffset].TryGetInt32(0, out result))
					return result;
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					DicomElementProvider[DicomTags.TidOffset] = null;
					return;
				}
				DicomElementProvider[DicomTags.TidOffset].SetInt32(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the value of MaskOperationExplanation in the underlying collection. Type 3.
		/// </summary>
		public string MaskOperationExplanation
		{
			get { return DicomElementProvider[DicomTags.MaskOperationExplanation].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.MaskOperationExplanation] = null;
					return;
				}
				DicomElementProvider[DicomTags.MaskOperationExplanation].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of MaskSelectionMode in the underlying collection. Type 3.
		/// </summary>
		public MaskSelectionMode MaskSelectionMode
		{
			get { return ParseEnum(DicomElementProvider[DicomTags.MaskSelectionMode].GetString(0, string.Empty), MaskSelectionMode.None); }
			set
			{
				if (value == MaskSelectionMode.None)
				{
					DicomElementProvider[DicomTags.MaskSelectionMode] = null;
					return;
				}
				SetAttributeFromEnum(DicomElementProvider[DicomTags.MaskSelectionMode], value);
			}
		}

		public void InitializeAttributes()
		{
			MaskOperation = MaskOperation.None;
			SubtractionItemId = null;
			ApplicableFrameRange = new int[0];
			MaskFrameNumbers = new int[0];
			ContrastFrameAveraging = null;
			MaskSubPixelShift = new double[0];
			TidOffset = null;
			MaskOperationExplanation = null;
			MaskSelectionMode = MaskSelectionMode.None;
		}

		/// <summary>
		/// Gets an enumeration of <see cref="DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags
		{
			get
			{
				yield return DicomTags.MaskOperation;
				yield return DicomTags.SubtractionItemId;
				yield return DicomTags.ApplicableFrameRange;
				yield return DicomTags.MaskFrameNumbers;
				yield return DicomTags.ContrastFrameAveraging;
				yield return DicomTags.MaskSubPixelShift;
				yield return DicomTags.TidOffset;
				yield return DicomTags.MaskOperationExplanation;
				yield return DicomTags.MaskSelectionMode;
			}
		}
	}

	/// <summary>
	/// Enumerated values for the <see cref="DicomTags.MaskOperation"/> attribute identifying the type of mask operation to be performed.
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2011, Part 3, Section C.7.6.10.1</remarks>
	public enum MaskOperation
	{
		/// <summary>
		/// Represents the null value.
		/// </summary>
		None,

		// ReSharper disable InconsistentNaming

		/// <summary>
		/// AVG_SUB
		/// </summary>
		Avg_Sub,

		/// <summary>
		/// TID
		/// </summary>
		Tid,

		/// <summary>
		/// REV_TID
		/// </summary>
		Rev_Tid

		// ReSharper restore InconsistentNaming
	}

	/// <summary>
	/// Enumerated values for the <see cref="UIH.RT.TMS.Dicom.DicomTags.MaskSelectionMode"/> attribute specifying the method of selection of the mask operations of this item.
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2011, Part 3, Section 10.7.6.10 (Table C.7-16)</remarks>
	public enum MaskSelectionMode
	{
		/// <summary>
		/// Represents the null value.
		/// </summary>
		None,

		/// <summary>
		/// SYSTEM
		/// </summary>
		System,

		/// <summary>
		/// USER
		/// </summary>
		User
	}
}
