/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: SopInstanceReferenceMacro.cs
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

namespace UIH.RT.TMS.Dicom.Iod.Macros
{
	/// <summary>
	/// SopInstanceReference Macro
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section 10.8 (Table 10-11)</remarks>
	public interface ISopInstanceReferenceMacro : IIodMacro
	{
		/// <summary>
		/// Gets or sets the value of ReferencedSopClassUid in the underlying collection. Type 1.
		/// </summary>
		string ReferencedSopClassUid { get; set; }

		/// <summary>
		/// Gets or sets the value of ReferencedSopInstanceUid in the underlying collection. Type 1.
		/// </summary>
		string ReferencedSopInstanceUid { get; set; }
	}

	/// <summary>
	/// SopInstanceReference Macro Base Implementation
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section 10.8 (Table 10-11)</remarks>
	internal class SopInstanceReferenceMacro : SequenceIodBase, ISopInstanceReferenceMacro
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SopInstanceReferenceMacro"/> class.
		/// </summary>
		public SopInstanceReferenceMacro() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="SopInstanceReferenceMacro"/> class.
		/// </summary>
		/// <param name="dicomSequenceItem">The dicom sequence item.</param>
		public SopInstanceReferenceMacro(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) {}

		/// <summary>
		/// Initializes the underlying collection to implement the module using default values.
		/// </summary>
		public virtual void InitializeAttributes()
		{
			this.ReferencedSopClassUid = "1";
			this.ReferencedSopInstanceUid = "1";
		}

		/// <summary>
		/// Gets or sets the value of ReferencedSopClassUid in the underlying collection. Type 1.
		/// </summary>
		public string ReferencedSopClassUid
		{
			get { return base.DicomElementProvider[DicomTags.ReferencedSopClassUid].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("value", "ReferencedSopClassUid is Type 1 Required.");
				base.DicomElementProvider[DicomTags.ReferencedSopClassUid].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ReferencedSopInstanceUid in the underlying collection. Type 1.
		/// </summary>
		public string ReferencedSopInstanceUid
		{
			get { return base.DicomElementProvider[DicomTags.ReferencedSopInstanceUid].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("value", "ReferencedSopInstanceUid is Type 1 Required.");
				base.DicomElementProvider[DicomTags.ReferencedSopInstanceUid].SetString(0, value);
			}
		}
	}
}
