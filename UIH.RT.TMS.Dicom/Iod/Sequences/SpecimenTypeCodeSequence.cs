/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: SpecimenTypeCodeSequence.cs
////
//// Summary:
////
////
//// Date: 2014/08/19
//////////////////////////////////////////////////////////////////////////
#region License

// Copyright (c) 2011 - 2013, United-Imaging Inc.
// All rights reserved.
// http://www.united-imaging.com

#endregion

using UIH.RT.TMS.Dicom.Iod.Macros;

namespace UIH.RT.TMS.Dicom.Iod.Sequences
{
	/// <summary>
	/// SpecimenType Code Sequence
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.7.1.2 (Table C.7-2a)</remarks>
	public class SpecimenTypeCodeSequence : CodeSequenceMacro
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SpecimenTypeCodeSequence"/> class.
		/// </summary>
		public SpecimenTypeCodeSequence() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="SpecimenTypeCodeSequence"/> class.
		/// </summary>
		/// <param name="dicomSequenceItem">The dicom sequence item.</param>
		public SpecimenTypeCodeSequence(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) {}
	}
}
