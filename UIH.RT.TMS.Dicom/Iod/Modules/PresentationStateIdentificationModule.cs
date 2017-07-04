/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: PresentationStateIdentificationModule.cs
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

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// PresentationStateIdentification Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.11.10 (Table C.11.10-1)</remarks>
	public class PresentationStateIdentificationModuleIod : IodBase, IContentIdentificationMacro
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PresentationStateIdentificationModuleIod"/> class.
		/// </summary>	
		public PresentationStateIdentificationModuleIod() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="PresentationStateIdentificationModuleIod"/> class.
		/// </summary>
		public PresentationStateIdentificationModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider) { }

		/// <summary>
		/// Gets the dicom attribute collection as a dicom sequence item.
		/// </summary>
		/// <value>The dicom sequence item.</value>
		DicomSequenceItem IIodMacro.DicomSequenceItem
		{
			get { return base.DicomElementProvider as DicomSequenceItem; }
			set { base.DicomElementProvider = value; }
		}

		/// <summary>
		/// Initializes the underlying collection to implement the module or sequence using default values.
		/// </summary>
		public virtual void InitializeAttributes()
		{
			this.PresentationCreationDateTime = DateTime.Now;
			this.InstanceNumber = 1;
			this.ContentLabel = " ";
			this.ContentDescription = null;
			this.ContentCreatorsName = null;
			this.ContentCreatorsIdentificationCodeSequence = null;
		}

		/// <summary>
		/// Gets or sets the value of PresentationCreationDate and PresentationCreationTime in the underlying collection.  Type 1.
		/// </summary>
		public DateTime? PresentationCreationDateTime
		{
			get
			{
				string date = base.DicomElementProvider[DicomTags.PresentationCreationDate].GetString(0, string.Empty);
				string time = base.DicomElementProvider[DicomTags.PresentationCreationTime].GetString(0, string.Empty);
				return DateTimeParser.ParseDateAndTime(string.Empty, date, time);
			}
			set
			{
				if (!value.HasValue)
					throw new ArgumentNullException("value", "PresentationCreation is Type 1 Required.");
				DicomElement date = base.DicomElementProvider[DicomTags.PresentationCreationDate];
				DicomElement time = base.DicomElementProvider[DicomTags.PresentationCreationTime];
				DateTimeParser.SetDateTimeAttributeValues(value, date, time);
			}
		}

		/// <summary>
		/// Gets or sets the value of InstanceNumber in the underlying collection. Type 1.
		/// </summary>
		public int InstanceNumber {
			get { return base.DicomElementProvider[DicomTags.InstanceNumber].GetInt32(0, 0); }
			set { base.DicomElementProvider[DicomTags.InstanceNumber].SetInt32(0, value); }
		}

		/// <summary>
		/// Gets or sets the value of ContentLabel in the underlying collection. Type 1.
		/// </summary>
		public string ContentLabel {
			get { return base.DicomElementProvider[DicomTags.ContentLabel].GetString(0, string.Empty); }
			set {
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("value", "ContentLabel is Type 1 Required.");
				base.DicomElementProvider[DicomTags.ContentLabel].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ContentDescription in the underlying collection. Type 2.
		/// </summary>
		public string ContentDescription {
			get { return base.DicomElementProvider[DicomTags.ContentDescription].GetString(0, string.Empty); }
			set {
				if (string.IsNullOrEmpty(value)) {
					base.DicomElementProvider[DicomTags.ContentDescription].SetNullValue();
					return;
				}
				base.DicomElementProvider[DicomTags.ContentDescription].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ContentCreatorsName in the underlying collection. Type 2.
		/// </summary>
		public string ContentCreatorsName {
			get { return base.DicomElementProvider[DicomTags.ContentCreatorsName].GetString(0, string.Empty); }
			set {
				if (string.IsNullOrEmpty(value)) {
					base.DicomElementProvider[DicomTags.ContentCreatorsName].SetNullValue();
					return;
				}
				base.DicomElementProvider[DicomTags.ContentCreatorsName].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ContentCreatorsIdentificationCodeSequence in the underlying collection. Type 3.
		/// </summary>
		public PersonIdentificationMacro ContentCreatorsIdentificationCodeSequence {
			get {
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.ContentCreatorsIdentificationCodeSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0) {
					return null;
				}
				return new PersonIdentificationMacro(((DicomSequenceItem[])dicomElement.Values)[0]);
			}
			set {
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.ContentCreatorsIdentificationCodeSequence];
				if (value == null) {
					base.DicomElementProvider[DicomTags.ContentCreatorsIdentificationCodeSequence] = null;
					return;
				}
				dicomElement.Values = new DicomSequenceItem[] { value.DicomSequenceItem };
			}
		}

		/// <summary>
		/// Gets an enumeration of <see cref="DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags {
			get {
				yield return DicomTags.ContentCreatorsIdentificationCodeSequence;
				yield return DicomTags.ContentCreatorsName;
				yield return DicomTags.ContentDescription;
				yield return DicomTags.ContentLabel;
				yield return DicomTags.InstanceNumber;
				yield return DicomTags.PresentationCreationDate;
				yield return DicomTags.PresentationCreationTime;
			}
		}
	}
}
