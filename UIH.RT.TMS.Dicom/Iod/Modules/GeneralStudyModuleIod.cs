/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: GeneralStudyModuleIod.cs
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
	/// GeneralStudy Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.7.2.1 (Table C.7-3)</remarks>
	public class GeneralStudyModuleIod : IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GeneralStudyModuleIod"/> class.
		/// </summary>	
		public GeneralStudyModuleIod() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="GeneralStudyModuleIod"/> class.
		/// </summary>
		public GeneralStudyModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider) { }

		/// <summary>
		/// Gets or sets the value of StudyInstanceUid in the underlying collection.
		/// </summary>
		public string StudyInstanceUid
		{
			get { return base.DicomElementProvider[DicomTags.StudyInstanceUid].GetString(0, string.Empty); }
			set { base.DicomElementProvider[DicomTags.StudyInstanceUid].SetString(0, value); }
		}

		/// <summary>
		/// Gets or sets the value of StudyDate and StudyTime in the underlying collection.
		/// </summary>
		public DateTime? StudyDateTime
		{
			get
			{
				string date = base.DicomElementProvider[DicomTags.StudyDate].GetString(0, string.Empty);
				string time = base.DicomElementProvider[DicomTags.StudyTime].GetString(0, string.Empty);
				return DateTimeParser.ParseDateAndTime(string.Empty, date, time);
			}
			set
			{
				DicomElement date = base.DicomElementProvider[DicomTags.StudyDate];
				DicomElement time = base.DicomElementProvider[DicomTags.StudyTime];
				DateTimeParser.SetDateTimeAttributeValues(value, date, time);
			}
		}

		/// <summary>
		/// Gets or sets the value of ReferringPhysiciansName in the underlying collection.
		/// </summary>
		public string ReferringPhysiciansName
		{
			get { return base.DicomElementProvider[DicomTags.ReferringPhysiciansName].GetString(0, string.Empty); }
			set { base.DicomElementProvider[DicomTags.ReferringPhysiciansName].SetString(0, value); }
		}

		/// <summary>
		/// Gets or sets the value of ReferringPhysicianIdentificationSequence in the underlying collection.
		/// </summary>
		public PersonIdentificationMacro ReferringPhysicianIdentificationSequence
		{
			get
			{
				var dicomAttribute = base.DicomElementProvider[DicomTags.ReferringPhysicianIdentificationSequence];
				if (dicomAttribute.IsNull || dicomAttribute.Count == 0)
				{
					return null;
				}
				return new PersonIdentificationMacro(((DicomSequenceItem[]) dicomAttribute.Values)[0]);
			}
			set
			{
				if (value == null)
				{
					base.DicomElementProvider[DicomTags.ReferringPhysicianIdentificationSequence] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.ReferringPhysicianIdentificationSequence].Values = new DicomSequenceItem[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Gets or sets the value of StudyId in the underlying collection.
		/// </summary>
		public string StudyId
		{
			get { return base.DicomElementProvider[DicomTags.StudyId].GetString(0, string.Empty); }
			set { base.DicomElementProvider[DicomTags.StudyId].SetString(0, value); }
		}

		/// <summary>
		/// Gets or sets the value of AccessionNumber in the underlying collection.
		/// </summary>
		public string AccessionNumber
		{
			get { return base.DicomElementProvider[DicomTags.AccessionNumber].GetString(0, string.Empty); }
			set { base.DicomElementProvider[DicomTags.AccessionNumber].SetString(0, value); }
		}

		/// <summary>
		/// Gets or sets the value of StudyDescription in the underlying collection.
		/// </summary>
		public string StudyDescription
		{
			get { return base.DicomElementProvider[DicomTags.StudyDescription].GetString(0, string.Empty); }
			set { base.DicomElementProvider[DicomTags.StudyDescription].SetString(0, value); }
		}

		/// <summary>
		/// Gets or sets the value of PhysiciansOfRecord in the underlying collection.
		/// </summary>
		public string PhysiciansOfRecord
		{
			get { return base.DicomElementProvider[DicomTags.PhysiciansOfRecord].ToString(); }
			set { base.DicomElementProvider[DicomTags.PhysiciansOfRecord].SetStringValue(value); }
		}

		/// <summary>
		/// Gets or sets the value of PhysiciansOfRecordIdentificationSequence in the underlying collection.
		/// </summary>
		public PersonIdentificationMacro PhysiciansOfRecordIdentificationSequence
		{
			get
			{
				var dicomAttribute = base.DicomElementProvider[DicomTags.PhysiciansOfRecordIdentificationSequence];
				if (dicomAttribute.IsNull || dicomAttribute.Count == 0)
				{
					return null;
				}
				return new PersonIdentificationMacro(((DicomSequenceItem[]) dicomAttribute.Values)[0]);
			}
			set
			{
				if (value == null)
				{
					base.DicomElementProvider[DicomTags.PhysiciansOfRecordIdentificationSequence] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.PhysiciansOfRecordIdentificationSequence].Values = new DicomSequenceItem[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Gets or sets the value of NameOfPhysiciansReadingStudy in the underlying collection.
		/// </summary>
		public string NameOfPhysiciansReadingStudy
		{
			get { return base.DicomElementProvider[DicomTags.NameOfPhysiciansReadingStudy].ToString(); }
			set { base.DicomElementProvider[DicomTags.NameOfPhysiciansReadingStudy].SetStringValue(value); }
		}

		/// <summary>
		/// Gets or sets the value of PhysiciansReadingStudyIdentificationSequence in the underlying collection.
		/// </summary>
		public PersonIdentificationMacro PhysiciansReadingStudyIdentificationSequence
		{
			get
			{
				var dicomAttribute = base.DicomElementProvider[DicomTags.PhysiciansReadingStudyIdentificationSequence];
				if (dicomAttribute.IsNull || dicomAttribute.Count == 0)
				{
					return null;
				}
				return new PersonIdentificationMacro(((DicomSequenceItem[]) dicomAttribute.Values)[0]);
			}
			set
			{
				if (value == null)
				{
					base.DicomElementProvider[DicomTags.PhysiciansReadingStudyIdentificationSequence] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.PhysiciansReadingStudyIdentificationSequence].Values = new DicomSequenceItem[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Gets or sets the value of ReferencedStudySequence in the underlying collection.
		/// </summary>
		public ISopInstanceReferenceMacro ReferencedStudySequence
		{
			get
			{
				var dicomAttribute = base.DicomElementProvider[DicomTags.ReferencedStudySequence];
				if (dicomAttribute.IsNull || dicomAttribute.Count == 0)
				{
					return null;
				}
				return new SopInstanceReferenceMacro(((DicomSequenceItem[]) dicomAttribute.Values)[0]);
			}
			set
			{
				if (value == null)
				{
					base.DicomElementProvider[DicomTags.ReferencedStudySequence] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.ReferencedStudySequence].Values = new DicomSequenceItem[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Gets or sets the value of ProcedureCodeSequence in the underlying collection.
		/// </summary>
		public CodeSequenceMacro ProcedureCodeSequence
		{
			get
			{
				var dicomAttribute = DicomElementProvider[DicomTags.ProcedureCodeSequence];
				if (dicomAttribute.IsNull || dicomAttribute.Count == 0)
				{
					return null;
				}
				return new CodeSequenceMacro(((DicomSequenceItem[]) dicomAttribute.Values)[0]);
			}
			set
			{
				if (value == null)
				{
					base.DicomElementProvider[DicomTags.ProcedureCodeSequence] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.ProcedureCodeSequence].Values = new DicomSequenceItem[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Checks if this module appears to be non-empty.
		/// </summary>
		/// <returns>True if the module appears to be non-empty; False otherwise.</returns>
		public bool HasValues()
		{
			return !string.IsNullOrEmpty(AccessionNumber)
			       || !string.IsNullOrEmpty(NameOfPhysiciansReadingStudy)
			       || !string.IsNullOrEmpty(PhysiciansOfRecord)
			       || PhysiciansOfRecordIdentificationSequence != null
			       || PhysiciansReadingStudyIdentificationSequence != null
			       || ProcedureCodeSequence != null
			       || ReferencedStudySequence != null
			       || ReferringPhysicianIdentificationSequence != null
			       || !string.IsNullOrEmpty(ReferringPhysiciansName)
			       || StudyDateTime.HasValue
			       || !string.IsNullOrEmpty(StudyDescription)
			       || !string.IsNullOrEmpty(StudyId)
			       || !string.IsNullOrEmpty(StudyInstanceUid);
		}

		/// <summary>
		/// Gets an enumeration of <see cref="DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags {
			get {
				yield return DicomTags.AccessionNumber;
				yield return DicomTags.NameOfPhysiciansReadingStudy;
				yield return DicomTags.PhysiciansOfRecord;
				yield return DicomTags.PhysiciansOfRecordIdentificationSequence;
				yield return DicomTags.PhysiciansReadingStudyIdentificationSequence;
				yield return DicomTags.ProcedureCodeSequence;
				yield return DicomTags.ReferencedStudySequence;
				yield return DicomTags.ReferringPhysicianIdentificationSequence;
				yield return DicomTags.ReferringPhysiciansName;
				yield return DicomTags.StudyDate;
				yield return DicomTags.StudyTime;
				yield return DicomTags.StudyDescription;
				yield return DicomTags.StudyId;
				yield return DicomTags.StudyInstanceUid;
			}
		}
	}
}
