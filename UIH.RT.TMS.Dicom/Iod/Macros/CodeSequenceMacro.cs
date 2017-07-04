/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: CodeSequenceMacro.cs
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
using UIH.RT.TMS.Dicom.Utilities;

namespace UIH.RT.TMS.Dicom.Iod.Macros
{
	/// <summary>
	/// Code Sequence Attributes Macro
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section 8.8 (Table 8.8-1)</remarks>
	public class CodeSequenceMacro : SequenceIodBase
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeSequenceMacro"/> class.
		/// </summary>
		public CodeSequenceMacro()
		{}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeSequenceMacro"/> class.
		/// </summary>
		/// <param name="dicomSequenceItem">The dicom sequence item.</param>
		public CodeSequenceMacro(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) {}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the code value.
		/// </summary>
		/// <value>The code value.</value>
		public string CodeValue
		{
			get { return DicomElementProvider[DicomTags.CodeValue].GetString(0, String.Empty); }
			set { DicomElementProvider[DicomTags.CodeValue].SetString(0, value); }
		}

		/// <summary>
		/// Gets or sets the coding scheme designator.
		/// </summary>
		/// <value>The coding scheme designator.</value>
		public string CodingSchemeDesignator
		{
			get { return DicomElementProvider[DicomTags.CodingSchemeDesignator].GetString(0, String.Empty); }
			set { DicomElementProvider[DicomTags.CodingSchemeDesignator].SetString(0, value); }
		}

		/// <summary>
		/// Gets or sets the coding scheme version.
		/// </summary>
		/// <value>The coding scheme version.</value>
		public string CodingSchemeVersion
		{
			get { return DicomElementProvider[DicomTags.CodingSchemeVersion].GetString(0, String.Empty); }
			set { DicomElementProvider[DicomTags.CodingSchemeVersion].SetString(0, value); }
		}

		/// <summary>
		/// Gets or sets the code meaning.
		/// </summary>
		/// <value>The code meaning.</value>
		public string CodeMeaning
		{
			get { return DicomElementProvider[DicomTags.CodeMeaning].GetString(0, String.Empty); }
			set { DicomElementProvider[DicomTags.CodeMeaning].SetString(0, value); }
		}

		/// <summary>
		/// Enhanced Encoding Mode: Gets or sets the context identifier.
		/// </summary>
		/// <value>The context identifier.</value>
		public string ContextIdentifier
		{
			get { return DicomElementProvider[DicomTags.ContextIdentifier].GetString(0, String.Empty); }
			set { DicomElementProvider[DicomTags.ContextIdentifier].SetString(0, value); }
		}

		/// <summary>
		/// Enhanced Encoding Mode: Gets or sets the mapping resource.
		/// </summary>
		/// <value>The mapping resource.</value>
		public string MappingResource
		{
			get { return DicomElementProvider[DicomTags.MappingResource].GetString(0, String.Empty); }
			set { DicomElementProvider[DicomTags.MappingResource].SetString(0, value); }
		}

		/// <summary>
		/// Enhanced Encoding Mode: Gets or sets the context group version.
		/// </summary>
		/// <value>The context group version.</value>
		public DateTime? ContextGroupVersion
		{
			get { return DateTimeParser.ParseDateAndTime(DicomElementProvider, DicomTags.ContextGroupVersion, 0, 0); }

			set { DateTimeParser.SetDateTimeAttributeValues(value, DicomElementProvider, DicomTags.ContextGroupVersion, 0, 0); }
		}

		/// <summary>
		/// Enhanced Encoding Mode: Gets or sets the context group extension flag.  Y or N
		/// </summary>
		/// <value>The context group extension flag.</value>
		public string ContextGroupExtensionFlag
		{
			get { return DicomElementProvider[DicomTags.ContextGroupExtensionFlag].GetString(0, String.Empty); }
			set { DicomElementProvider[DicomTags.ContextGroupExtensionFlag].SetString(0, value); }
		}

		/// <summary>
		/// Enhanced Encoding Mode: Gets or sets the context group local version.
		/// </summary>
		/// <value>The context group local version.</value>
		public DateTime? ContextGroupLocalVersion
		{
			get { return DateTimeParser.ParseDateAndTime(DicomElementProvider, DicomTags.ContextGroupLocalVersion, 0, 0); }

			set { DateTimeParser.SetDateTimeAttributeValues(value, DicomElementProvider, DicomTags.ContextGroupLocalVersion, 0, 0); }
		}

		/// <summary>
		/// Enhanced Encoding Mode: Gets or sets the context group extension creator uid.
		/// </summary>
		/// <value>The context group extension creator uid.</value>
		public string ContextGroupExtensionCreatorUid
		{
			get { return DicomElementProvider[DicomTags.ContextGroupExtensionCreatorUid].GetString(0, String.Empty); }
			set { DicomElementProvider[DicomTags.ContextGroupExtensionCreatorUid].SetString(0, value); }
		}

		#endregion
	}
}
