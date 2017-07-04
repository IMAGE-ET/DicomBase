/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: KeyObjectDocumentSeries.cs
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
using UIH.RT.TMS.Dicom.Iod.Macros;
using UIH.RT.TMS.Dicom.Utilities;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// KeyObjectDocumentSeries Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.17.6.1 (Table C.17.6-1)</remarks>
	public class KeyObjectDocumentSeriesModuleIod : IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="KeyObjectDocumentSeriesModuleIod"/> class.
		/// </summary>
		public KeyObjectDocumentSeriesModuleIod() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="KeyObjectDocumentSeriesModuleIod"/> class.
		/// </summary>
		public KeyObjectDocumentSeriesModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider) { }

		/// <summary>
		/// Initializes the underlying collection to implement the module using default values.
		/// </summary>
		public void InitializeAttributes()
		{
			this.Modality = Modality.KO;
			this.SeriesInstanceUid = "1";
			this.SeriesNumber = 0;
			this.SeriesDateTime = null;
			this.SeriesDescription = null;
			this.ReferencedPerformedProcedureStepSequence = null;
		}

		/// <summary>
		/// Gets or sets the value of Modality in the underlying collection. Type 1.
		/// </summary>
		public Modality Modality
		{
			get { return ParseEnum(base.DicomElementProvider[DicomTags.Modality].GetString(0, string.Empty), Modality.None); }
			set
			{
				if (value != Modality.KO)
					throw new ArgumentOutOfRangeException("value", "Modality must be KO.");
				SetAttributeFromEnum(base.DicomElementProvider[DicomTags.Modality], value);
			}
		}

		/// <summary>
		/// Gets or sets the value of SeriesInstanceUid in the underlying collection. Type 1.
		/// </summary>
		public string SeriesInstanceUid
		{
			get { return base.DicomElementProvider[DicomTags.SeriesInstanceUid].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("value", "SeriesInstanceUid is Type 1 Required.");
				base.DicomElementProvider[DicomTags.SeriesInstanceUid].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of SeriesNumber in the underlying collection. Type 1.
		/// </summary>
		public int SeriesNumber
		{
			get { return base.DicomElementProvider[DicomTags.SeriesNumber].GetInt32(0, 0); }
			set { base.DicomElementProvider[DicomTags.SeriesNumber].SetInt32(0, value); }
		}

		/// <summary>
		/// Gets or sets the value of SeriesDate and SeriesTime in the underlying collection. Type 3.
		/// </summary>
		public DateTime? SeriesDateTime
		{
			get
			{
				string date = base.DicomElementProvider[DicomTags.SeriesDate].GetString(0, string.Empty);
				string time = base.DicomElementProvider[DicomTags.SeriesTime].GetString(0, string.Empty);
				return DateTimeParser.ParseDateAndTime(string.Empty, date, time);
			}
			set
			{
				DicomElement date = base.DicomElementProvider[DicomTags.SeriesDate];
				DicomElement time = base.DicomElementProvider[DicomTags.SeriesTime];
				DateTimeParser.SetDateTimeAttributeValues(value, date, time);
			}
		}

		/// <summary>
		/// Gets or sets the value of SeriesDescription in the underlying collection. Type 3.
		/// </summary>
		public string SeriesDescription
		{
			// Type 3
			get { return base.DicomElementProvider[DicomTags.SeriesDescription].GetString(0, string.Empty); }
			set { base.DicomElementProvider[DicomTags.SeriesDescription].SetString(0, value); }
		}

		/// <summary>
		/// Gets or sets the value of ReferencedPerformedProcedureStepSequence in the underlying collection. Type 2.
		/// </summary>
		public ISopInstanceReferenceMacro ReferencedPerformedProcedureStepSequence
		{
			// Type 2
			get
			{
				DicomElement referencedPerformedProcedureStepSequence = base.DicomElementProvider[DicomTags.ReferencedPerformedProcedureStepSequence];
				if (referencedPerformedProcedureStepSequence.IsNull || referencedPerformedProcedureStepSequence.Count == 0)
				{
					return null;
				}
				return new SopInstanceReferenceMacro(((DicomSequenceItem[]) referencedPerformedProcedureStepSequence.Values)[0]);
			}
			set
			{
				DicomElement referencedPerformedProcedureStepSequence = base.DicomElementProvider[DicomTags.ReferencedPerformedProcedureStepSequence];
				if (value == null)
				{
					referencedPerformedProcedureStepSequence.SetNullValue();
					return;
				}
				referencedPerformedProcedureStepSequence.Values = new DicomSequenceItem[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Creates the ReferencedPerformedProcedureStepSequence in the underlying collection.
		/// </summary>
		public ISopInstanceReferenceMacro CreateReferencedPerformedProcedureStepSequence()
		{
			DicomElement referencedPerformedProcedureStepSequence = base.DicomElementProvider[DicomTags.ReferencedPerformedProcedureStepSequence];
			if (referencedPerformedProcedureStepSequence.IsNull || referencedPerformedProcedureStepSequence.Count == 0)
			{
				DicomSequenceItem dicomSequenceItem = new DicomSequenceItem();
				referencedPerformedProcedureStepSequence.Values = new DicomSequenceItem[] {dicomSequenceItem};
				SopInstanceReferenceMacro sopInstanceReference = new SopInstanceReferenceMacro(dicomSequenceItem);
				sopInstanceReference.InitializeAttributes();
				return sopInstanceReference;
			}
			return new SopInstanceReferenceMacro(((DicomSequenceItem[]) referencedPerformedProcedureStepSequence.Values)[0]);
		}
	}
}
