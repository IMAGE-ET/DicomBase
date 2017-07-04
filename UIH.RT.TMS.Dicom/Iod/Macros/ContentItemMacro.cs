/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ContentItemMacro.cs
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
	/// Content Item Macro
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section 10.2 (Table 10-2)</remarks>
	public class ContentItemMacro : SequenceIodBase
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ContentItemMacro"/> class.
		/// </summary>
		public ContentItemMacro() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="ContentItemMacro"/> class.
		/// </summary>
		/// <param name="dicomSequenceItem">The dicom sequence item.</param>
		public ContentItemMacro(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) {}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the type of the value.
		/// </summary>
		/// <value>The type of the value.</value>
		public ContentItemValueType ValueType
		{
			get { return ParseEnum<ContentItemValueType>(base.DicomElementProvider[DicomTags.ValueType].GetString(0, String.Empty), ContentItemValueType.None); }
			set { SetAttributeFromEnum(base.DicomElementProvider[DicomTags.ValueType], value); }
		}

		public SequenceIodList<CodeSequenceMacro> ConceptNameCodeSequenceList
		{
			get { return new SequenceIodList<CodeSequenceMacro>(base.DicomElementProvider[DicomTags.ConceptNameCodeSequence] as DicomElementSq); }
		}

		/// <summary>
		/// Datetime value for this name-value Item. Required if Value Type (0040,A040) is DATETIME.
		/// </summary>
		/// <value>The datetime.</value>
		public DateTime? Datetime
		{
			get { return base.DicomElementProvider[DicomTags.Datetime].GetDateTime(0); }

			set
			{
				if (value.HasValue)
					base.DicomElementProvider[DicomTags.Datetime].SetDateTime(0, value.Value);
				else
					base.DicomElementProvider[DicomTags.Datetime].SetNullValue();
			}
		}

		/// <summary>
		/// Date value for this name-value Item. Required if Value Type (0040,A040) is DATE.
		/// </summary>
		/// <value>The date.</value>
		public DateTime? Date
		{
			get { return base.DicomElementProvider[DicomTags.Date].GetDateTime(0); }

			set { base.DicomElementProvider[DicomTags.Date].SetDateTime(0, value); }
		}

		/// <summary>
		/// Time value for this name-value Item.  Required if Value Type (0040,A040) is TIME.
		/// </summary>
		/// <value>The time.</value>
		public DateTime? Time
		{
			get { return base.DicomElementProvider[DicomTags.Time].GetDateTime(0); }

			set { base.DicomElementProvider[DicomTags.Time].SetDateTime(0, value); }
		}

		/// <summary>
		/// Person name value for this name-value Item.  Required if Value Type (0040,A040) is PNAME.
		/// </summary>
		/// <value>The name of the person.</value>
		public PersonName PersonName
		{
			get { return new PersonName(base.DicomElementProvider[DicomTags.PersonName].GetString(0, String.Empty)); }
			set { base.DicomElementProvider[DicomTags.PersonName].SetString(0, value.ToString()); }
		}

		/// <summary>
		/// UID value for this name-value Item.  Required if Value Type (0040,A040) is UIDREF.
		/// </summary>
		/// <value>The uid.</value>
		public string Uid
		{
			get { return base.DicomElementProvider[DicomTags.Uid].GetString(0, String.Empty); }
			set { base.DicomElementProvider[DicomTags.Uid].SetString(0, value); }
		}

		/// <summary>
		/// Text value for this name-value Item.  Required if Value Type (0040,A040) is TEXT.
		/// </summary>
		/// <value>The text value.</value>
		public string TextValue
		{
			get { return base.DicomElementProvider[DicomTags.TextValue].GetString(0, String.Empty); }
			set { base.DicomElementProvider[DicomTags.TextValue].SetString(0, value); }
		}

		/// <summary>
		/// Coded concept value of this name-value Item.  Required if Value Type (0040,A040) is CODE.
		/// </summary>
		/// <value>The concept code sequence list.</value>
		public SequenceIodList<CodeSequenceMacro> ConceptCodeSequenceList
		{
			get { return new SequenceIodList<CodeSequenceMacro>(base.DicomElementProvider[DicomTags.ConceptCodeSequence] as DicomElementSq); }
		}

		/// <summary>
		/// Numeric value for this name-value Item. Required if Value Type (0040,A040) is NUMERIC.
		/// </summary>
		/// <value>The numeric value.</value>
		public float NumericValue
		{
			get { return base.DicomElementProvider[DicomTags.NumericValue].GetFloat32(0, 0.0F); }
			set { base.DicomElementProvider[DicomTags.NumericValue].SetFloat32(0, value); }
		}

		/// <summary>
		/// Units of measurement for a numeric value in this namevalue Item.  Required if Value Type (0040,A040) is NUMERIC.
		/// </summary>
		/// <value>The measurement units code sequence list.</value>
		public SequenceIodList<CodeSequenceMacro> MeasurementUnitsCodeSequenceList
		{
			get { return new SequenceIodList<CodeSequenceMacro>(base.DicomElementProvider[DicomTags.MeasurementUnitsCodeSequence] as DicomElementSq); }
		}

		#endregion
	}

	#region ContentItemValueType Enum

	/// <summary>
	/// 
	/// </summary>
	public enum ContentItemValueType
	{
		/// <summary>
		/// 
		/// </summary>
		None,
		/// <summary>
		/// 
		/// </summary>
		DateTime,
		/// <summary>
		/// 
		/// </summary>
		Date,
		/// <summary>
		/// 
		/// </summary>
		Time,
		/// <summary>
		/// 
		/// </summary>
		PName,
		/// <summary>
		/// 
		/// </summary>
		UidRef,
		/// <summary>
		/// 
		/// </summary>
		Text,
		/// <summary>
		/// 
		/// </summary>
		Code,
		/// <summary>
		/// 
		/// </summary>
		Numeric
	}

	#endregion
}
