/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: OtherPatientIdsSequence.cs
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
	/// OtherPatientIds Sequence
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.7.1.1 (Table C.7-1)</remarks>
	public class OtherPatientIdsSequence : SequenceIodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="OtherPatientIdsSequence"/> class.
		/// </summary>
		public OtherPatientIdsSequence() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="OtherPatientIdsSequence"/> class.
		/// </summary>
		/// <param name="dicomSequenceItem">The dicom sequence item.</param>
		public OtherPatientIdsSequence(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) { }

		/// <summary>
		/// Initializes the underlying collection to implement the module or sequence using default values.
		/// </summary>
		public virtual void InitializeAttributes() {
			this.PatientId = " ";
			this.IssuerOfPatientId = " ";
			this.TypeOfPatientId = TypeOfPatientId.Text;
		}

		/// <summary>
		/// Gets or sets the value of PatientId in the underlying collection. Type 1.
		/// </summary>
		public string PatientId
		{
			get { return base.DicomElementProvider[DicomTags.PatientId].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("value", "PatientId is Type 1 Required.");
				base.DicomElementProvider[DicomTags.PatientId].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of IssuerOfPatientId in the underlying collection. Type 1.
		/// </summary>
		public string IssuerOfPatientId
		{
			get { return base.DicomElementProvider[DicomTags.IssuerOfPatientId].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("value", "IssuerOfPatientId is Type 1 Required.");
				base.DicomElementProvider[DicomTags.IssuerOfPatientId].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of TypeOfPatientId in the underlying collection. Type 1.
		/// </summary>
		public TypeOfPatientId TypeOfPatientId
		{
			get { return ParseEnum(base.DicomElementProvider[DicomTags.TypeOfPatientId].GetString(0, string.Empty), TypeOfPatientId.Unknown); }
			set
			{
				if (value == TypeOfPatientId.Unknown)
					throw new ArgumentOutOfRangeException("value", "TypeOfPatientId is Type 1 Required.");
				SetAttributeFromEnum(base.DicomElementProvider[DicomTags.TypeOfPatientId], value);
			}
		}
	}
}
