/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ReferencedSopInstanceMacSequence.cs
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

namespace UIH.RT.TMS.Dicom.Iod.Sequences
{
	/// <summary>
	/// ReferencedSopInstanceMac Sequence
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.17.2.1 (Table C.17-3a)</remarks>
	public class ReferencedSopInstanceMacSequence : SequenceIodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ReferencedSopInstanceMacSequence"/> class.
		/// </summary>
		public ReferencedSopInstanceMacSequence() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="ReferencedSopInstanceMacSequence"/> class.
		/// </summary>
		/// <param name="dicomSequenceItem">The dicom sequence item.</param>
		public ReferencedSopInstanceMacSequence(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) {}

		/// <summary>
		/// Gets or sets the value of MacCalculationTransferSyntaxUid in the underlying collection. Type 1.
		/// </summary>
		public string MacCalculationTransferSyntaxUid
		{
			get { return base.DicomElementProvider[DicomTags.MacCalculationTransferSyntaxUid].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("value", "MacCalculationTransferSyntaxUid is Type 1 Required.");
				base.DicomElementProvider[DicomTags.MacCalculationTransferSyntaxUid].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of MacAlgorithm in the underlying collection. Type 1.
		/// </summary>
		public MacAlgorithm MacAlgorithm
		{
			get { return ParseEnum(base.DicomElementProvider[DicomTags.MacAlgorithm].GetString(0, string.Empty), MacAlgorithm.Unknown); }
			set
			{
				if (value == MacAlgorithm.Unknown)
					throw new ArgumentOutOfRangeException("value", "MacAlgorithm is Type 1 Required.");
				SetAttributeFromEnum(base.DicomElementProvider[DicomTags.MacAlgorithm], value);
			}
		}

		/// <summary>
		/// Gets or sets the value of DataElementsSigned in the underlying collection. Type 1.
		/// </summary>
		public uint[] DataElementsSigned
		{
			get { return (uint[]) base.DicomElementProvider[DicomTags.DataElementsSigned].Values; }
			set
			{
				if (value == null || value.Length == 0)
					throw new ArgumentNullException("value", "DataElementsSigned is Type 1 Required.");
				base.DicomElementProvider[DicomTags.DataElementsSigned].Values = value;
			}
		}

		/// <summary>
		/// Gets or sets the value of Mac in the underlying collection. Type 1.
		/// </summary>
		public byte[] Mac
		{
			get { return (byte[]) base.DicomElementProvider[DicomTags.Mac].Values; }
			set
			{
				if (value == null || value.Length == 0)
					throw new ArgumentNullException("value", "Mac is Type 1 Required.");
				base.DicomElementProvider[DicomTags.Mac].Values = value;
			}
		}
	}
}
