/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: SopCommonModuleIod.cs
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
using UIH.RT.TMS.Dicom.Iod.Sequences;
using UIH.RT.TMS.Dicom.Utilities;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// SopCommon Module
	/// </summary>
	/// <remarks>
	/// <para>As defined in the DICOM Standard 2009, Part 3, Section C.12.1 (Table C.12-1)</para>
	/// </remarks>
	public class SopCommonModuleIod
		: IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SopCommonModuleIod"/> class.
		/// </summary>	
		public SopCommonModuleIod() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="SopCommonModuleIod"/> class.
		/// </summary>
		/// <param name="dicomElementProvider">The DICOM attribute provider.</param>
		public SopCommonModuleIod(IDicomElementProvider dicomElementProvider)
			: base(dicomElementProvider) {}

		/// <summary>
		/// Gets or sets the value of SopClassUid in the underlying collection. Type 1.
		/// </summary>
		public string SopClassUid
		{
			get { return DicomElementProvider[DicomTags.SopClassUid].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("value", "SopClassUid is Type 1 Required.");
				DicomElementProvider[DicomTags.SopClassUid].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of SopClassUid in the underlying collection. Type 1.
		/// </summary>
		public SopClass SopClass
		{
			get { return SopClass.GetSopClass(SopClassUid); }
			set { SopClassUid = value != null ? value.Uid : string.Empty; }
		}

		/// <summary>
		/// Gets or sets the value of SopInstanceUid in the underlying collection. Type 1.
		/// </summary>
		public string SopInstanceUid
		{
			get { return DicomElementProvider[DicomTags.SopInstanceUid].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("value", "SopInstanceUid is Type 1 Required.");
				DicomElementProvider[DicomTags.SopInstanceUid].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of SpecificCharacterSet in the underlying collection. Type 1C.
		/// </summary>
		public string SpecificCharacterSet
		{
			get { return DicomElementProvider[DicomTags.SpecificCharacterSet].ToString(); }
			set
			{
				if (value == null) // an empty string has a special meaning for Specific Character Set
				{
					DicomElementProvider[DicomTags.SpecificCharacterSet] = null;
					return;
				}
				DicomElementProvider[DicomTags.SpecificCharacterSet].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of InstanceCreationDate and InstanceCreationTime in the underlying collection. Type 3.
		/// </summary>
		public DateTime? InstanceCreationDateTime
		{
			get
			{
				var date = DicomElementProvider[DicomTags.InstanceCreationDate].GetString(0, string.Empty);
				var time = DicomElementProvider[DicomTags.InstanceCreationTime].GetString(0, string.Empty);
				return DateTimeParser.ParseDateAndTime(string.Empty, date, time);
			}
			set
			{
				if (!value.HasValue)
				{
					DicomElementProvider[DicomTags.InstanceCreationDate] = null;
					DicomElementProvider[DicomTags.InstanceCreationTime] = null;
					return;
				}
				var date = DicomElementProvider[DicomTags.InstanceCreationDate];
				var time = DicomElementProvider[DicomTags.InstanceCreationTime];
				DateTimeParser.SetDateTimeAttributeValues(value, date, time);
			}
		}

		/// <summary>
		/// Gets or sets the value of InstanceCreatorUid in the underlying collection. Type 3.
		/// </summary>
		public string InstanceCreatorUid
		{
			get { return DicomElementProvider[DicomTags.InstanceCreatorUid].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.InstanceCreatorUid] = null;
					return;
				}
				DicomElementProvider[DicomTags.InstanceCreatorUid].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of RelatedGeneralSopClassUid in the underlying collection. Type 3.
		/// </summary>
		public string RelatedGeneralSopClassUid
		{
			get { return DicomElementProvider[DicomTags.RelatedGeneralSopClassUid].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.RelatedGeneralSopClassUid] = null;
					return;
				}
				DicomElementProvider[DicomTags.RelatedGeneralSopClassUid].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of OriginalSpecializedSopClassUid in the underlying collection. Type 3.
		/// </summary>
		public string OriginalSpecializedSopClassUid
		{
			get { return DicomElementProvider[DicomTags.OriginalSpecializedSopClassUid].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.OriginalSpecializedSopClassUid] = null;
					return;
				}
				DicomElementProvider[DicomTags.OriginalSpecializedSopClassUid].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of CodingSchemeIdentificationSequence in the underlying collection. Type 3.
		/// </summary>
		public CodingSchemeIdentificationSequenceItem[] CodingSchemeIdentificationSequence
		{
			get
			{
				var dicomAttribute = DicomElementProvider[DicomTags.CodingSchemeIdentificationSequence];
				if (dicomAttribute.IsNull || dicomAttribute.IsEmpty)
					return null;

				var result = new CodingSchemeIdentificationSequenceItem[dicomAttribute.Count];
				var items = (DicomSequenceItem[]) dicomAttribute.Values;
				for (var n = 0; n < items.Length; n++)
					result[n] = new CodingSchemeIdentificationSequenceItem(items[n]);

				return result;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					DicomElementProvider[DicomTags.CodingSchemeIdentificationSequence] = null;
					return;
				}

				var result = new DicomSequenceItem[value.Length];
				for (var n = 0; n < value.Length; n++)
					result[n] = value[n].DicomSequenceItem;

				DicomElementProvider[DicomTags.CodingSchemeIdentificationSequence].Values = result;
			}
		}

		/// <summary>
		/// Creates a single instance of a CodingSchemeIdentificationSequence item. Does not modify the CodingSchemeIdentificationSequence in the underlying collection.
		/// </summary>
		public CodingSchemeIdentificationSequenceItem CreateCodingSchemeIdentificationSequence()
		{
			var iodBase = new CodingSchemeIdentificationSequenceItem(new DicomSequenceItem());
			return iodBase;
		}

		/// <summary>
		/// Gets or sets the value of TimezoneOffsetFromUtc in the underlying collection. Type 3.
		/// </summary>
		public string TimezoneOffsetFromUtc
		{
			get { return DicomElementProvider[DicomTags.TimezoneOffsetFromUtc].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.TimezoneOffsetFromUtc] = null;
					return;
				}
				DicomElementProvider[DicomTags.TimezoneOffsetFromUtc].SetStringValue(value);
			}
		}

		// TODO: Implement Contributing Equipment Sequence

		/// <summary>
		/// Gets or sets the value of InstanceNumber in the underlying collection. Type 3.
		/// </summary>
		public int? InstanceNumber
		{
			get
			{
				int result;
				if (DicomElementProvider[DicomTags.InstanceNumber].TryGetInt32(0, out result))
					return result;
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					DicomElementProvider[DicomTags.InstanceNumber] = null;
					return;
				}
				DicomElementProvider[DicomTags.InstanceNumber].SetInt32(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the value of SopInstanceStatus in the underlying collection. Type 3.
		/// </summary>
		public string SopInstanceStatus
		{
			get { return DicomElementProvider[DicomTags.SopInstanceStatus].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.SopInstanceStatus] = null;
					return;
				}
				DicomElementProvider[DicomTags.SopInstanceStatus].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of SopAuthorizationDatetime in the underlying collection. Type 3.
		/// </summary>
		public DateTime? SopAuthorizationDatetime
		{
			get { return DateTimeParser.Parse(DicomElementProvider[DicomTags.SopAuthorizationDatetime].ToString()); }
			set
			{
				if (!value.HasValue)
				{
					DicomElementProvider[DicomTags.SopAuthorizationDatetime] = null;
					return;
				}
				DicomElementProvider[DicomTags.SopAuthorizationDatetime].SetStringValue(DateTimeParser.ToDicomString(value.Value, false));
			}
		}

		/// <summary>
		/// Gets or sets the value of SopAuthorizationComment in the underlying collection. Type 3.
		/// </summary>
		public string SopAuthorizationComment
		{
			get { return DicomElementProvider[DicomTags.SopAuthorizationComment].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.SopAuthorizationComment] = null;
					return;
				}
				DicomElementProvider[DicomTags.SopAuthorizationComment].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of AuthorizationEquipmentCertificationNumber in the underlying collection. Type 3.
		/// </summary>
		public string AuthorizationEquipmentCertificationNumber
		{
			get { return DicomElementProvider[DicomTags.AuthorizationEquipmentCertificationNumber].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.AuthorizationEquipmentCertificationNumber] = null;
					return;
				}
				DicomElementProvider[DicomTags.AuthorizationEquipmentCertificationNumber].SetStringValue(value);
			}
		}

		// TODO: Include Digital Signatures Macro
		// TODO: Implement Encrypted Attributes Sequence
		// TODO: Implement Original Attributes Sequence
		// TODO: Implement HL7 Structured Document Reference Sequence

		/// <summary>
		/// Initializes the attributes in this module to their default values.
		/// </summary>
		public void InitializeAttributes()
		{
			// nothing to initialize since the only required attributes will likely be overridden by client code anyway
		}

		/// <summary>
		/// Gets an enumeration of <see cref="DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags
		{
			get
			{
				yield return DicomTags.SopClassUid;
				yield return DicomTags.SopInstanceUid;
				yield return DicomTags.SpecificCharacterSet;
				yield return DicomTags.InstanceCreationDate;
				yield return DicomTags.InstanceCreationTime;
				yield return DicomTags.InstanceCreatorUid;
				yield return DicomTags.RelatedGeneralSopClassUid;
				yield return DicomTags.OriginalSpecializedSopClassUid;
				yield return DicomTags.CodingSchemeIdentificationSequence;
				yield return DicomTags.TimezoneOffsetFromUtc;
				yield return DicomTags.ContributingEquipmentSequence;
				yield return DicomTags.InstanceNumber;
				yield return DicomTags.SopInstanceStatus;
				yield return DicomTags.SopAuthorizationDatetime;
				yield return DicomTags.SopAuthorizationComment;
				yield return DicomTags.AuthorizationEquipmentCertificationNumber;
				yield return DicomTags.EncryptedAttributesSequence;
				yield return DicomTags.OriginalAttributesSequence;
				yield return DicomTags.Hl7StructuredDocumentReferenceSequence;
			}
		}
	}
}
