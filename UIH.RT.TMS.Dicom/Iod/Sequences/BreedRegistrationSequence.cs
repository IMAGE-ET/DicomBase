/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: BreedRegistrationSequence.cs
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
using UIH.RT.TMS.Dicom.Iod.ContextGroups;
using UIH.RT.TMS.Dicom.Iod.Macros;

namespace UIH.RT.TMS.Dicom.Iod.Sequences
{
	/// <summary>
	/// BreedRegistration Sequence
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.7.1.1 (Table C.7-1)</remarks>
	public class BreedRegistrationSequence : SequenceIodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BreedRegistrationSequence"/> class.
		/// </summary>
		public BreedRegistrationSequence() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="BreedRegistrationSequence"/> class.
		/// </summary>
		/// <param name="dicomSequenceItem">The dicom sequence item.</param>
		public BreedRegistrationSequence(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) { }

		/// <summary>
		/// Initializes the underlying collection to implement the module or sequence using default values.
		/// </summary>
		public void InitializeAttributes()
		{
			this.BreedRegistrationNumber = " ";
		}

		/// <summary>
		/// Gets or sets the value of BreedRegistrationNumber in the underlying collection. Type 1.
		/// </summary>
		public string BreedRegistrationNumber
		{
			get { return base.DicomElementProvider[DicomTags.BreedRegistrationNumber].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("value", "BreedRegistrationNumber is Type 1 Required.");
				base.DicomElementProvider[DicomTags.BreedRegistrationNumber].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of BreedRegistryCodeSequence in the underlying collection. Type 1.
		/// Only one item shall be present.
		/// </summary>
		public BreedRegistry BreedRegistryCodeSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.BreedRegistryCodeSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
					return null;

				var dicomSequenceItem = ((DicomSequenceItem[])dicomElement.Values)[0];
				return new BreedRegistry(new CodeSequenceMacro(dicomSequenceItem));
			}
			set
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.BreedRegistryCodeSequence];
				if (value == null)
					throw new ArgumentNullException("value", "BreedRegistryCodeSequence is Type 1 Required.");

				var sequenceItem = new CodeSequenceMacro();
				value.WriteToCodeSequence(sequenceItem);

				dicomElement.Values = new DicomSequenceItem[] { sequenceItem.DicomSequenceItem };
			}
		}
	}
}
