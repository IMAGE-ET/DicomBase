/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: PatientModuleIod.cs
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
using UIH.RT.TMS.Dicom.Iod.Sequences;
using UIH.RT.TMS.Dicom.Utilities;
using UIH.RT.TMS.Dicom.Iod.ContextGroups;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// Patient Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.7.1.1 (Table C.7-1)</remarks>
	public class PatientModuleIod : IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PatientModuleIod"/> class.
		/// </summary>	
		public PatientModuleIod() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="PatientModuleIod"/> class.
		/// </summary>
		public PatientModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider) {}

		/// <summary>
		/// Initializes the underlying collection to implement the module or sequence using default values.
		/// </summary>
		public void InitializeAttributes()
		{
			this.BreedRegistrationSequence = null;
			this.DeIdentificationMethod = null;
			this.DeIdentificationMethodCodeSequence = null;
			this.EthnicGroup = null;
			this.IssuerOfPatientId = null;
			this.OtherPatientIds = null;
			this.OtherPatientIdsSequence = null;
			this.OtherPatientNames = null;
			this.PatientBreedCodeSequence = null;
			this.PatientBreedDescription = null;
			this.PatientComments = null;
			this.PatientId = null;
			this.PatientIdentityRemoved = PatientIdentityRemoved.Unknown;
			this.PatientsBirthDateTime = null;
			this.PatientsName = null;
			this.PatientSpeciesCodeSequence = null;
			this.PatientSpeciesDescription = null;
			this.PatientsSex = PatientsSex.None;
			this.ReferencedPatientSequence = null;
			this.ResponsibleOrganization = null;
			this.ResponsiblePerson = null;
			this.ResponsiblePersonRole = ResponsiblePersonRole.None;
		}

		/// <summary>
		/// Checks if this module appears to be non-empty.
		/// </summary>
		/// <returns>True if the module appears to be non-empty; False otherwise.</returns>
		public bool HasValues()
		{
			if (this.BreedRegistrationSequence == null
			    && string.IsNullOrEmpty(this.DeIdentificationMethod)
			    && this.DeIdentificationMethodCodeSequence == null
			    && string.IsNullOrEmpty(this.EthnicGroup)
			    && string.IsNullOrEmpty(this.IssuerOfPatientId)
			    && string.IsNullOrEmpty(this.OtherPatientIds)
			    && this.OtherPatientIdsSequence == null
			    && string.IsNullOrEmpty(this.OtherPatientNames)
			    && this.PatientBreedCodeSequence == null
			    && string.IsNullOrEmpty(this.PatientBreedDescription)
			    && string.IsNullOrEmpty(this.PatientComments)
			    && string.IsNullOrEmpty(this.PatientId)
			    && this.PatientIdentityRemoved == PatientIdentityRemoved.Unknown
			    && !this.PatientsBirthDateTime.HasValue
			    && string.IsNullOrEmpty(this.PatientsName)
			    && this.PatientSpeciesCodeSequence == null
			    && string.IsNullOrEmpty(this.PatientSpeciesDescription)
			    && this.PatientsSex == PatientsSex.None
			    && this.ReferencedPatientSequence == null
			    && string.IsNullOrEmpty(this.ResponsibleOrganization)
			    && string.IsNullOrEmpty(this.ResponsiblePerson)
			    && this.ResponsiblePersonRole == ResponsiblePersonRole.None)
				return false;
			return true;
		}

		/// <summary>
		/// Gets or sets the value of PatientsName in the underlying collection. Type 2.
		/// </summary>
		public string PatientsName
		{
			get { return base.DicomElementProvider[DicomTags.PatientsName].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.PatientsName].SetNullValue();
					return;
				}
				base.DicomElementProvider[DicomTags.PatientsName].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of PatientId in the underlying collection. Type 2.
		/// </summary>
		public string PatientId
		{
			get { return base.DicomElementProvider[DicomTags.PatientId].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.PatientId].SetNullValue();
					return;
				}
				base.DicomElementProvider[DicomTags.PatientId].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of IssuerOfPatientId in the underlying collection. Type 3.
		/// </summary>
		public string IssuerOfPatientId
		{
			get { return base.DicomElementProvider[DicomTags.IssuerOfPatientId].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.IssuerOfPatientId] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.IssuerOfPatientId].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of PatientsBirthDate and PatientsBirthTime in the underlying collection.  Type 2.
		/// </summary>
		public DateTime? PatientsBirthDateTime
		{
			get
			{
				string date = base.DicomElementProvider[DicomTags.PatientsBirthDate].GetString(0, string.Empty);
				string time = base.DicomElementProvider[DicomTags.PatientsBirthTime].GetString(0, string.Empty);
				return DateTimeParser.ParseDateAndTime(string.Empty, date, time);
			}
			set
			{
				if (!value.HasValue)
				{
					base.DicomElementProvider[DicomTags.PatientsBirthDate].SetNullValue();
					base.DicomElementProvider[DicomTags.PatientsBirthTime].SetNullValue();
					return;
				}
				DicomElement date = base.DicomElementProvider[DicomTags.PatientsBirthDate];
				DicomElement time = base.DicomElementProvider[DicomTags.PatientsBirthTime];
				DateTimeParser.SetDateTimeAttributeValues(value, date, time);
			}
		}

		/// <summary>
		/// Gets or sets the value of PatientsSex in the underlying collection. Type 2.
		/// </summary>
		public PatientsSex PatientsSex
		{
			get { return ParseEnum(base.DicomElementProvider[DicomTags.PatientsSex].GetString(0, string.Empty), PatientsSex.None); }
			set
			{
				if (value == PatientsSex.None)
				{
					base.DicomElementProvider[DicomTags.PatientsSex].SetNullValue();
					return;
				}
				SetAttributeFromEnum(base.DicomElementProvider[DicomTags.PatientsSex], value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ReferencedPatientSequence in the underlying collection. Type 3.
		/// </summary>
		public ISopInstanceReferenceMacro ReferencedPatientSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.ReferencedPatientSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}
				return new SopInstanceReferenceMacro(((DicomSequenceItem[]) dicomElement.Values)[0]);
			}
			set
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.ReferencedPatientSequence];
				if (value == null)
				{
					base.DicomElementProvider[DicomTags.ReferencedPatientSequence] = null;
					return;
				}
				dicomElement.Values = new DicomSequenceItem[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Creates the ReferencedPatientSequence in the underlying collection. Type 3.
		/// </summary>
		public ISopInstanceReferenceMacro CreateReferencedPatientSequence()
		{
			DicomElement dicomElement = base.DicomElementProvider[DicomTags.ReferencedPatientSequence];
			if (dicomElement.IsNull || dicomElement.Count == 0)
			{
				DicomSequenceItem dicomSequenceItem = new DicomSequenceItem();
				dicomElement.Values = new DicomSequenceItem[] {dicomSequenceItem};
				ISopInstanceReferenceMacro sequenceType = new SopInstanceReferenceMacro(dicomSequenceItem);
				sequenceType.InitializeAttributes();
				return sequenceType;
			}
			return new SopInstanceReferenceMacro(((DicomSequenceItem[]) dicomElement.Values)[0]);
		}

		/// <summary>
		/// Gets or sets the value of OtherPatientIds in the underlying collection. Type 3.
		/// </summary>
		public string OtherPatientIds
		{
			get { return base.DicomElementProvider[DicomTags.OtherPatientIds].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.OtherPatientIds] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.OtherPatientIds].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of OtherPatientIdsSequence in the underlying collection. Type 3.
		/// </summary>
		public OtherPatientIdsSequence[] OtherPatientIdsSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.OtherPatientIdsSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}

				OtherPatientIdsSequence[] result = new OtherPatientIdsSequence[dicomElement.Count];
				DicomSequenceItem[] items = (DicomSequenceItem[]) dicomElement.Values;
				for (int n = 0; n < items.Length; n++)
					result[n] = new OtherPatientIdsSequence(items[n]);

				return result;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.DicomElementProvider[DicomTags.OtherPatientIdsSequence] = null;
					return;
				}

				DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
				for (int n = 0; n < value.Length; n++)
					result[n] = value[n].DicomSequenceItem;

				base.DicomElementProvider[DicomTags.OtherPatientIdsSequence].Values = result;
			}
		}

		/// <summary>
		/// Gets or sets the value of OtherPatientNames in the underlying collection. Type 3.
		/// </summary>
		public string OtherPatientNames
		{
			get { return base.DicomElementProvider[DicomTags.OtherPatientNames].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.OtherPatientNames] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.OtherPatientNames].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of EthnicGroup in the underlying collection. Type 3.
		/// </summary>
		public string EthnicGroup
		{
			get { return base.DicomElementProvider[DicomTags.EthnicGroup].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.EthnicGroup] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.EthnicGroup].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of PatientComments in the underlying collection. Type 3.
		/// </summary>
		public string PatientComments
		{
			get { return base.DicomElementProvider[DicomTags.PatientComments].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.PatientComments] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.PatientComments].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of PatientSpeciesDescription in the underlying collection. Type 1C.
		/// </summary>
		public string PatientSpeciesDescription
		{
			get { return base.DicomElementProvider[DicomTags.PatientSpeciesDescription].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.PatientSpeciesDescription] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.PatientSpeciesDescription].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of PatientSpeciesCodeSequence in the underlying collection. Type 1C.
		/// </summary>
		public Species PatientSpeciesCodeSequence
		{
			get
			{

				DicomElement dicomElement = base.DicomElementProvider[DicomTags.PatientSpeciesCodeSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}

				var dicomSequenceItem = ((DicomSequenceItem[])dicomElement.Values)[0];
				return new Species(new CodeSequenceMacro(dicomSequenceItem));
			}
			set
			{
				if (value == null)
				{
					base.DicomElementProvider[DicomTags.PatientSpeciesCodeSequence] = null;
					return;
				}

				var dicomAttribute = base.DicomElementProvider[DicomTags.PatientSpeciesCodeSequence];

				var sequenceItem = new CodeSequenceMacro();
				value.WriteToCodeSequence(sequenceItem);

				dicomAttribute.Values = new DicomSequenceItem[] { sequenceItem.DicomSequenceItem };
			}
		}

		/// <summary>
		/// Gets or sets the value of PatientBreedDescription in the underlying collection. Type 2C.
		/// </summary>
		public string PatientBreedDescription
		{
			get { return base.DicomElementProvider[DicomTags.PatientBreedDescription].GetString(0, string.Empty); }
			set
			{
				if (value == null)
				{
					base.DicomElementProvider[DicomTags.PatientBreedDescription].SetNullValue();
					return;
				}
				base.DicomElementProvider[DicomTags.PatientBreedDescription].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of PatientBreedCodeSequence in the underlying collection. Type 3.
		/// </summary>
		public Breed[] PatientBreedCodeSequence
		{
			get
			{

				DicomElement dicomElement = base.DicomElementProvider[DicomTags.PatientBreedCodeSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}

				var results = new Breed[dicomElement.Count];
				var sequenceItems = (DicomSequenceItem[])dicomElement.Values;
				for (var n = 0; n < sequenceItems.Length; n++)
				{
					results[n] = new Breed(new CodeSequenceMacro(sequenceItems[n]));
				}

				return results;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.DicomElementProvider[DicomTags.PatientBreedCodeSequence] = null;
					return;
				}

				var result = new DicomSequenceItem[value.Length];
				for (var n = 0; n < value.Length; n++)
				{
					var codeSequence = new CodeSequenceMacro();
					value[n].WriteToCodeSequence(codeSequence);
					result[n] = codeSequence.DicomSequenceItem;
				}

				base.DicomElementProvider[DicomTags.PatientBreedCodeSequence].Values = result;
			}
		}

		/// <summary>
		/// Gets or sets the value of BreedRegistrationSequence in the underlying collection. Type 3.
		/// </summary>
		public BreedRegistrationSequence[] BreedRegistrationSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.BreedRegistrationSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}

				BreedRegistrationSequence[] result = new BreedRegistrationSequence[dicomElement.Count];
				DicomSequenceItem[] items = (DicomSequenceItem[]) dicomElement.Values;
				for (int n = 0; n < items.Length; n++)
					result[n] = new BreedRegistrationSequence(items[n]);

				return result;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.DicomElementProvider[DicomTags.BreedRegistrationSequence] = null;
					return;
				}

				DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
				for (int n = 0; n < value.Length; n++)
					result[n] = value[n].DicomSequenceItem;

				base.DicomElementProvider[DicomTags.BreedRegistrationSequence].Values = result;
			}
		}

		/// <summary>
		/// Gets or sets the value of ResponsiblePerson in the underlying collection. Type 2C.
		/// </summary>
		public string ResponsiblePerson
		{
			get { return base.DicomElementProvider[DicomTags.ResponsiblePerson].GetString(0, string.Empty); }
			set
			{
				if (value == null)
				{
					base.DicomElementProvider[DicomTags.ResponsiblePerson] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.ResponsiblePerson].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ResponsiblePersonRole in the underlying collection. Type 1C.
		/// </summary>
		public ResponsiblePersonRole ResponsiblePersonRole
		{
			get { return ParseEnum(base.DicomElementProvider[DicomTags.ResponsiblePersonRole].GetString(0, string.Empty), ResponsiblePersonRole.None); }
			set
			{
				if (value == ResponsiblePersonRole.None)
				{
					base.DicomElementProvider[DicomTags.ResponsiblePersonRole] = null;
					return;
				}
				SetAttributeFromEnum(base.DicomElementProvider[DicomTags.ResponsiblePersonRole], value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ResponsibleOrganization in the underlying collection. Type 2C.
		/// </summary>
		public string ResponsibleOrganization
		{
			get { return base.DicomElementProvider[DicomTags.ResponsibleOrganization].GetString(0, string.Empty); }
			set
			{
				if (value == null)
				{
					base.DicomElementProvider[DicomTags.ResponsibleOrganization] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.ResponsibleOrganization].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of PatientIdentityRemoved in the underlying collection. Type 3.
		/// </summary>
		public PatientIdentityRemoved PatientIdentityRemoved
		{
			get { return ParseEnum(base.DicomElementProvider[DicomTags.PatientIdentityRemoved].GetString(0, string.Empty), PatientIdentityRemoved.Unknown); }
			set
			{
				if (value == PatientIdentityRemoved.Unknown)
				{
					base.DicomElementProvider[DicomTags.PatientIdentityRemoved] = null;
					return;
				}
				SetAttributeFromEnum(base.DicomElementProvider[DicomTags.PatientIdentityRemoved], value);
			}
		}

		/// <summary>
		/// Gets or sets the value of DeIdentificationMethod in the underlying collection. Type 1C.
		/// </summary>
		public string DeIdentificationMethod
		{
			get { return base.DicomElementProvider[DicomTags.DeIdentificationMethod].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.DeIdentificationMethod] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.DeIdentificationMethod].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of DeIdentificationMethodCodeSequence in the underlying collection. Type 1C.
		/// </summary>
		public DeIdentificationMethodCodeSequence[] DeIdentificationMethodCodeSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.DeIdentificationMethodCodeSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}

				DeIdentificationMethodCodeSequence[] result = new DeIdentificationMethodCodeSequence[dicomElement.Count];
				DicomSequenceItem[] items = (DicomSequenceItem[]) dicomElement.Values;
				for (int n = 0; n < items.Length; n++)
					result[n] = new DeIdentificationMethodCodeSequence(items[n]);

				return result;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.DicomElementProvider[DicomTags.DeIdentificationMethodCodeSequence] = null;
					return;
				}

				DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
				for (int n = 0; n < value.Length; n++)
					result[n] = value[n].DicomSequenceItem;

				base.DicomElementProvider[DicomTags.DeIdentificationMethodCodeSequence].Values = result;
			}
		}

		/// <summary>
		/// Gets an enumeration of <see cref="DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags {
			get {
				yield return DicomTags.BreedRegistrationSequence;
				yield return DicomTags.DeIdentificationMethod;
				yield return DicomTags.DeIdentificationMethodCodeSequence;
				yield return DicomTags.EthnicGroup;
				yield return DicomTags.IssuerOfPatientId;
				yield return DicomTags.OtherPatientIds;
				yield return DicomTags.OtherPatientIdsSequence;
				yield return DicomTags.OtherPatientNames;
				yield return DicomTags.PatientBreedCodeSequence;
				yield return DicomTags.PatientBreedDescription;
				yield return DicomTags.PatientComments;
				yield return DicomTags.PatientId;
				yield return DicomTags.PatientIdentityRemoved;
				yield return DicomTags.PatientsBirthDate;
				yield return DicomTags.PatientsBirthTime;
				yield return DicomTags.PatientsName;
				yield return DicomTags.PatientSpeciesCodeSequence;
				yield return DicomTags.PatientSpeciesDescription;
				yield return DicomTags.PatientsSex;
				yield return DicomTags.ReferencedPatientSequence;
				yield return DicomTags.ResponsibleOrganization;
				yield return DicomTags.ResponsiblePerson;
				yield return DicomTags.ResponsiblePersonRole;
			}
		}
	}
}
