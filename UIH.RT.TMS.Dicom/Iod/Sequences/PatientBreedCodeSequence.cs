/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: PatientBreedCodeSequence.cs
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
	/// PatientBreed Code Sequence
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.7.1.1 (Table C.7-1)</remarks>
	[Obsolete("Use ContextGroups.Breed instead.")]
	public class PatientBreedCodeSequence : CodeSequenceMacro
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PatientBreedCodeSequence"/> class.
		/// </summary>
		public PatientBreedCodeSequence() : base()
		{
			base.ContextIdentifier = "7480";
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PatientBreedCodeSequence"/> class.
		/// </summary>
		/// <param name="dicomSequenceItem">The dicom sequence item.</param>
		public PatientBreedCodeSequence(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem)
		{
			base.ContextIdentifier = "7480";
		}

		/// <summary>
		/// Converts a <see cref="PatientBreedCodeSequence"/> to a <see cref="Breed"/>.
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		public static implicit operator Breed(PatientBreedCodeSequence code)
		{
			return new Breed(code.CodingSchemeDesignator, code.CodingSchemeVersion, code.CodeValue, code.CodeMeaning);
		}

		/// <summary>
		/// Converts a <see cref="Breed"/> to a <see cref="PatientBreedCodeSequence"/>.
		/// </summary>
		/// <param name="breed"></param>
		/// <returns></returns>
		public static implicit operator PatientBreedCodeSequence(Breed breed)
		{
			var codeSequence = new PatientBreedCodeSequence();
			breed.WriteToCodeSequence(codeSequence);
			return codeSequence;
		}
	}
}
