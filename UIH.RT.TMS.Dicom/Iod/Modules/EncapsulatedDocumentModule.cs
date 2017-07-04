/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: EncapsulatedDocumentModule.cs
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
using UIH.RT.TMS.Dicom.Iod.Macros;
using UIH.RT.TMS.Dicom.Utilities;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// EncapsulatedDocument Module
	/// </summary>
	/// <remarks>
	/// <para>As defined in the DICOM Standard 2009, Part 3, Section C.24.2 (Table C.24-2)</para>
	/// </remarks>
	public class EncapsulatedDocumentModuleIod
		: IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EncapsulatedDocumentModuleIod"/> class.
		/// </summary>	
		public EncapsulatedDocumentModuleIod() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="EncapsulatedDocumentModuleIod"/> class.
		/// </summary>
		/// <param name="dicomElementProvider">The DICOM attribute provider.</param>
		public EncapsulatedDocumentModuleIod(IDicomElementProvider dicomElementProvider)
			: base(dicomElementProvider) {}

		/// <summary>
		/// Gets or sets the value of InstanceNumber in the underlying collection. Type 1.
		/// </summary>
		public int InstanceNumber
		{
			get { return DicomElementProvider[DicomTags.InstanceNumber].GetInt32(0, 0); }
			set { DicomElementProvider[DicomTags.InstanceNumber].SetInt32(0, value); }
		}

		/// <summary>
		/// Gets or sets the value of ContentDate and ContentTime in the underlying collection.  Type 2.
		/// </summary>
		public DateTime? ContentDateTime
		{
			get
			{
				var date = DicomElementProvider[DicomTags.ContentDate].GetString(0, string.Empty);
				var time = DicomElementProvider[DicomTags.ContentTime].GetString(0, string.Empty);
				return DateTimeParser.ParseDateAndTime(string.Empty, date, time);
			}
			set
			{
				if (!value.HasValue)
				{
					DicomElementProvider[DicomTags.ContentDate].SetNullValue();
					DicomElementProvider[DicomTags.ContentTime].SetNullValue();
					return;
				}
				var date = DicomElementProvider[DicomTags.ContentDate];
				var time = DicomElementProvider[DicomTags.ContentTime];
				DateTimeParser.SetDateTimeAttributeValues(value, date, time);
			}
		}

		/// <summary>
		/// Gets or sets the value of AcquisitionDatetime in the underlying collection. Type 2.
		/// </summary>
		public DateTime? AcquisitionDateTime
		{
			get { return DateTimeParser.Parse(DicomElementProvider[DicomTags.AcquisitionDatetime].ToString()); }
			set
			{
				if (!value.HasValue)
				{
					DicomElementProvider[DicomTags.AcquisitionDatetime].SetNullValue();
					return;
				}
				DicomElementProvider[DicomTags.AcquisitionDatetime].SetStringValue(DateTimeParser.ToDicomString(value.Value, false));
			}
		}

		/// <summary>
		/// Gets or sets the value of BurnedInAnnotation in the underlying collection. Type 1.
		/// </summary>
		public bool BurnedInAnnotation
		{
			get { return !string.Equals(@"NO", DicomElementProvider[DicomTags.BurnedInAnnotation].GetString(0, string.Empty), StringComparison.InvariantCultureIgnoreCase); }
			set { DicomElementProvider[DicomTags.BurnedInAnnotation].SetString(0, value ? @"YES" : @"NO"); }
		}

		/// <summary>
		/// Gets or sets the value of SourceInstanceSequence in the underlying collection. Type 1C.
		/// </summary>
		public ISopInstanceReferenceMacro[] SourceInstanceSequence
		{
			get
			{
				var dicomAttribute = DicomElementProvider[DicomTags.SourceInstanceSequence];
				if (dicomAttribute.IsNull || dicomAttribute.IsEmpty)
					return null;

				var result = new ISopInstanceReferenceMacro[dicomAttribute.Count];
				var items = (DicomSequenceItem[]) dicomAttribute.Values;
				for (var n = 0; n < items.Length; n++)
					result[n] = new SopInstanceReferenceMacro(items[n]);

				return result;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					DicomElementProvider[DicomTags.SourceInstanceSequence] = null;
					return;
				}

				var result = new DicomSequenceItem[value.Length];
				for (var n = 0; n < value.Length; n++)
					result[n] = value[n].DicomSequenceItem;

				DicomElementProvider[DicomTags.SourceInstanceSequence].Values = result;
			}
		}

		/// <summary>
		/// Creates a single instance of a SourceInstanceSequence item. Does not modify the SourceInstanceSequence in the underlying collection.
		/// </summary>
		public ISopInstanceReferenceMacro CreateSourceInstanceSequence()
		{
			var iodBase = new SopInstanceReferenceMacro(new DicomSequenceItem());
			iodBase.InitializeAttributes();
			return iodBase;
		}

		/// <summary>
		/// Gets or sets the value of DocumentTitle in the underlying collection. Type 2.
		/// </summary>
		public string DocumentTitle
		{
			get { return DicomElementProvider[DicomTags.DocumentTitle].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.DocumentTitle].SetNullValue();
					return;
				}
				DicomElementProvider[DicomTags.DocumentTitle].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ConceptNameCodeSequence in the underlying collection. Type 2.
		/// </summary>
		public CodeSequenceMacro ConceptNameCodeSequence
		{
			get
			{
				var dicomAttribute = DicomElementProvider[DicomTags.ConceptNameCodeSequence];
				if (dicomAttribute.IsNull || dicomAttribute.IsEmpty)
				{
					return null;
				}
				return new CodeSequenceMacro(((DicomSequenceItem[]) dicomAttribute.Values)[0]);
			}
			set
			{
				var dicomAttribute = DicomElementProvider[DicomTags.ConceptNameCodeSequence];
				if (value == null)
				{
					dicomAttribute.SetNullValue();
					return;
				}
				dicomAttribute.Values = new[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Creates the ConceptNameCodeSequence in the underlying collection. Type 2.
		/// </summary>
		public CodeSequenceMacro CreateConceptNameCodeSequence()
		{
			var dicomAttribute = DicomElementProvider[DicomTags.ConceptNameCodeSequence];
			if (dicomAttribute.IsNull || dicomAttribute.IsEmpty)
			{
				var dicomSequenceItem = new DicomSequenceItem();
				dicomAttribute.Values = new[] {dicomSequenceItem};
				var sequenceType = new CodeSequenceMacro(dicomSequenceItem);
				return sequenceType;
			}
			return new CodeSequenceMacro(((DicomSequenceItem[]) dicomAttribute.Values)[0]);
		}

		/// <summary>
		/// Gets or sets the value of VerificationFlag in the underlying collection. Type 3.
		/// </summary>
		public VerificationFlag VerificationFlag
		{
			get { return ParseEnum(DicomElementProvider[DicomTags.VerificationFlag].GetString(0, string.Empty), VerificationFlag.None); }
			set
			{
				if (value == VerificationFlag.None)
				{
					DicomElementProvider[DicomTags.VerificationFlag] = null;
					return;
				}
				SetAttributeFromEnum(DicomElementProvider[DicomTags.VerificationFlag], value);
			}
		}

		/// <summary>
		/// Gets or sets the value of Hl7InstanceIdentifier in the underlying collection. Type 1C.
		/// </summary>
		public string Hl7InstanceIdentifier
		{
			get { return DicomElementProvider[DicomTags.Hl7InstanceIdentifier].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.Hl7InstanceIdentifier] = null;
					return;
				}
				DicomElementProvider[DicomTags.Hl7InstanceIdentifier].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of MimeTypeOfEncapsulatedDocument in the underlying collection. Type 1.
		/// </summary>
		public string MimeTypeOfEncapsulatedDocument
		{
			get { return DicomElementProvider[DicomTags.MimeTypeOfEncapsulatedDocument].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("value", "MimeTypeOfEncapsulatedDocument is Type 1 Required.");
				DicomElementProvider[DicomTags.MimeTypeOfEncapsulatedDocument].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ListOfMimeTypes in the underlying collection. Type 1C.
		/// </summary>
		public string[] ListOfMimeTypes
		{
			get
			{
				var dicomAttribute = DicomElementProvider[DicomTags.ListOfMimeTypes];
				if (dicomAttribute.IsNull || dicomAttribute.IsEmpty)
					return null;

				var result = new string[dicomAttribute.Count];
				for (var n = 0; n < result.Length; n++)
					result[n] = dicomAttribute.GetString(n, string.Empty);
				return result;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					DicomElementProvider[DicomTags.ListOfMimeTypes] = null;
					return;
				}

				var dicomAttribute = DicomElementProvider[DicomTags.ListOfMimeTypes];
				for (var n = 0; n < value.Length; n++)
					dicomAttribute.SetString(n, value[n]);
			}
		}

		/// <summary>
		/// Gets or sets the value of EncapsulatedDocument in the underlying collection. Type 1.
		/// </summary>
		public byte[] EncapsulatedDocument
		{
			get
			{
				var attribute = DicomElementProvider[DicomTags.EncapsulatedDocument];
				if (attribute.IsNull || attribute.IsEmpty)
					return null;
				return (byte[]) attribute.Values;
			}
			set
			{
				if (value == null)
					throw new ArgumentOutOfRangeException("value", "EncapsulatedDocument is Type 1 Required.");
				DicomElementProvider[DicomTags.EncapsulatedDocument].Values = value;
			}
		}

		/// <summary>
		/// Initializes the attributes in this module to their default values.
		/// </summary>
		public void InitializeAttributes()
		{
			InstanceNumber = 0;
			ContentDateTime = null;
			AcquisitionDateTime = null;
			BurnedInAnnotation = false;
			SourceInstanceSequence = null;
			DocumentTitle = string.Empty;
			ConceptNameCodeSequence = null;
			Hl7InstanceIdentifier = null;
			MimeTypeOfEncapsulatedDocument = @"application/octet-stream";
			ListOfMimeTypes = null;
			EncapsulatedDocument = new byte[0];
		}

		/// <summary>
		/// Gets an enumeration of <see cref="UIH.RT.TMS.Dicom.DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags
		{
			get
			{
				yield return DicomTags.InstanceNumber;
				yield return DicomTags.ContentDate;
				yield return DicomTags.ContentTime;
				yield return DicomTags.AcquisitionDatetime;
				yield return DicomTags.BurnedInAnnotation;
				yield return DicomTags.SourceInstanceSequence;
				yield return DicomTags.DocumentTitle;
				yield return DicomTags.ConceptNameCodeSequence;
				yield return DicomTags.VerificationFlag;
				yield return DicomTags.Hl7InstanceIdentifier;
				yield return DicomTags.MimeTypeOfEncapsulatedDocument;
				yield return DicomTags.ListOfMimeTypes;
				yield return DicomTags.EncapsulatedDocument;
			}
		}
	}
}
