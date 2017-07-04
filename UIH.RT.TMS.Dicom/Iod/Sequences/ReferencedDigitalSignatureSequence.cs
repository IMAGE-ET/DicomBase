/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ReferencedDigitalSignatureSequence.cs
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
	/// ReferencedDigitalSignature Sequence
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.17.2.1 (Table C.17-3a)</remarks>
	public class ReferencedDigitalSignatureSequence : SequenceIodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ReferencedDigitalSignatureSequence"/> class.
		/// </summary>
		public ReferencedDigitalSignatureSequence() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="ReferencedDigitalSignatureSequence"/> class.
		/// </summary>
		/// <param name="dicomSequenceItem">The dicom sequence item.</param>
		public ReferencedDigitalSignatureSequence(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) {}

		/// <summary>
		/// Gets or sets the value of DigitalSignatureUid in the underlying collection. Type 1.
		/// </summary>
		public string DigitalSignatureUid
		{
			get { return base.DicomElementProvider[DicomTags.DigitalSignatureUid].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("value", "DigitalSignatureUid is Type 1 Required.");
				base.DicomElementProvider[DicomTags.DigitalSignatureUid].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of Signature in the underlying collection. Type 1.
		/// </summary>
		public byte[] Signature
		{
			get { return (byte[]) base.DicomElementProvider[DicomTags.Signature].Values; }
			set
			{
				if (value == null || value.Length == 0)
					throw new ArgumentNullException("value", "Signature is Type 1 Required.");
				base.DicomElementProvider[DicomTags.Signature].Values = value;
			}
		}
	}
}
