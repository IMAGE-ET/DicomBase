/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: KeyObjectDocument.cs
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
using UIH.RT.TMS.Dicom.Iod.Macros.HierarchicalSeriesInstanceReference;
using UIH.RT.TMS.Dicom.Iod.Sequences;
using UIH.RT.TMS.Dicom.Utilities;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// KeyObjectDocument Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.17.6.2 (Table C.17.6-2)</remarks>
	public class KeyObjectDocumentModuleIod : IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="KeyObjectDocumentModuleIod"/> class.
		/// </summary>	
		public KeyObjectDocumentModuleIod() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="KeyObjectDocumentModuleIod"/> class.
		/// </summary>
		public KeyObjectDocumentModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider) {}

		/// <summary>
		/// Initializes the underlying collection to implement the module using default values.
		/// </summary>
		public void InitializeAttributes()
		{
			this.InstanceNumber = 1;
			this.ContentDateTime = DateTime.Now;
			this.ReferencedRequestSequence = null;
			this.CreateCurrentRequestedProcedureEvidenceSequence();
			this.IdenticalDocumentsSequence = null;
		}

		/// <summary>
		/// Gets or sets the value of InstanceNumber in the underlying collection. Type 1.
		/// </summary>
		public int InstanceNumber
		{
			get { return base.DicomElementProvider[DicomTags.InstanceNumber].GetInt32(0, 0); }
			set { base.DicomElementProvider[DicomTags.InstanceNumber].SetInt32(0, value); }
		}

		/// <summary>
		/// Gets or sets the value of ContentDate and ContentTime in the underlying collection.  Type 1.
		/// </summary>
		public DateTime? ContentDateTime
		{
			get
			{
				string date = base.DicomElementProvider[DicomTags.ContentDate].GetString(0, string.Empty);
				string time = base.DicomElementProvider[DicomTags.ContentTime].GetString(0, string.Empty);
				return DateTimeParser.ParseDateAndTime(string.Empty, date, time);
			}
			set
			{
				if (!value.HasValue)
					throw new ArgumentNullException("value", "Content is Type 1 Required.");
				DicomElement date = base.DicomElementProvider[DicomTags.ContentDate];
				DicomElement time = base.DicomElementProvider[DicomTags.ContentTime];
				DateTimeParser.SetDateTimeAttributeValues(value, date, time);
			}
		}

		/// <summary>
		/// Gets or sets the value of ReferencedRequestSequence in the underlying collection. Type 1C.
		/// </summary>
		public ReferencedRequestSequence[] ReferencedRequestSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.ReferencedRequestSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}

				ReferencedRequestSequence[] result = new ReferencedRequestSequence[dicomElement.Count];
				DicomSequenceItem[] items = (DicomSequenceItem[]) dicomElement.Values;
				for (int n = 0; n < items.Length; n++)
					result[n] = new ReferencedRequestSequence(items[n]);

				return result;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.DicomElementProvider[DicomTags.ReferencedRequestSequence] = null;
					return;
				}

				DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
				for (int n = 0; n < value.Length; n++)
					result[n] = value[n].DicomSequenceItem;

				base.DicomElementProvider[DicomTags.ReferencedRequestSequence].Values = result;
			}
		}

		/// <summary>
		/// Gets or sets the value of CurrentRequestedProcedureEvidenceSequence in the underlying collection. Type 1.
		/// </summary>
		/// <remarks>
		/// The helper class <see cref="HierarchicalSopInstanceReferenceDictionary"/> can be used to assist in creating
		/// an evidence sequence with minimal repetition.
		/// </remarks>
		public IHierarchicalSopInstanceReferenceMacro[] CurrentRequestedProcedureEvidenceSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.CurrentRequestedProcedureEvidenceSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
					return null;

				IHierarchicalSopInstanceReferenceMacro[] result = new IHierarchicalSopInstanceReferenceMacro[dicomElement.Count];
				DicomSequenceItem[] items = (DicomSequenceItem[]) dicomElement.Values;
				for (int n = 0; n < items.Length; n++)
					result[n] = new HierarchicalSopInstanceReferenceMacro(items[n]);

				return result;
			}
			set
			{
				if (value == null || value.Length == 0)
					throw new ArgumentNullException("value", "CurrentRequestedProcedureEvidenceSequence is Type 1 Required.");

				DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
				for (int n = 0; n < value.Length; n++)
					result[n] = value[n].DicomSequenceItem;

				base.DicomElementProvider[DicomTags.CurrentRequestedProcedureEvidenceSequence].Values = result;
			}
		}

		/// <summary>
		/// Creates a single instance of a CurrentRequestedProcedureEvidenceSequence item. Does not modify the CurrentRequestedProcedureEvidenceSequence in the underlying collection.
		/// </summary>
		public IHierarchicalSopInstanceReferenceMacro CreateCurrentRequestedProcedureEvidenceSequence()
		{
			IHierarchicalSopInstanceReferenceMacro iodBase = new HierarchicalSopInstanceReferenceMacro(new DicomSequenceItem());
			iodBase.InitializeAttributes();
			return iodBase;
		}

		/// <summary>
		/// Gets or sets the value of IdenticalDocumentsSequence in the underlying collection. Type 1C.
		/// </summary>
		public IHierarchicalSopInstanceReferenceMacro[] IdenticalDocumentsSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.IdenticalDocumentsSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}

				IHierarchicalSopInstanceReferenceMacro[] result = new IHierarchicalSopInstanceReferenceMacro[dicomElement.Count];
				DicomSequenceItem[] items = (DicomSequenceItem[]) dicomElement.Values;
				for (int n = 0; n < items.Length; n++)
					result[n] = new HierarchicalSopInstanceReferenceMacro(items[n]);

				return result;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.DicomElementProvider[DicomTags.IdenticalDocumentsSequence] = null;
					return;
				}

				DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
				for (int n = 0; n < value.Length; n++)
					result[n] = value[n].DicomSequenceItem;

				base.DicomElementProvider[DicomTags.IdenticalDocumentsSequence].Values = result;
			}
		}

		/// <summary>
		/// Creates a single instance of a IdenticalDocumentsSequence item. Does not modify the IdenticalDocumentsSequence in the underlying collection.
		/// </summary>
		public IHierarchicalSopInstanceReferenceMacro CreateIdenticalDocumentsSequence()
		{
			IHierarchicalSopInstanceReferenceMacro iodBase = new HierarchicalSopInstanceReferenceMacro(new DicomSequenceItem());
			iodBase.InitializeAttributes();
			return iodBase;
		}

		/// <summary>
		/// Creates a single instance of a IdenticalDocumentsSequence item. Does not modify the IdenticalDocumentsSequence in the underlying collection.
		/// </summary>
		public IHierarchicalSopInstanceReferenceMacro CreateIdenticalDocumentsSequence(string studyInstanceUid, string seriesInstanceUid, string sopClassUid, string sopInstanceUid)
		{
			IHierarchicalSopInstanceReferenceMacro identicalDocument;
			IHierarchicalSeriesInstanceReferenceMacro seriesReference;
			IReferencedSopSequence sopReference;

			identicalDocument = this.CreateIdenticalDocumentsSequence();
			identicalDocument.InitializeAttributes();
			identicalDocument.StudyInstanceUid = studyInstanceUid;
			identicalDocument.ReferencedSeriesSequence = new IHierarchicalSeriesInstanceReferenceMacro[] {seriesReference = identicalDocument.CreateReferencedSeriesSequence()};

			seriesReference.InitializeAttributes();
			seriesReference.SeriesInstanceUid = seriesInstanceUid;
			seriesReference.ReferencedSopSequence = new IReferencedSopSequence[] {sopReference = seriesReference.CreateReferencedSopSequence()};

			sopReference.InitializeAttributes();
			sopReference.ReferencedSopClassUid = sopClassUid;
			sopReference.ReferencedSopInstanceUid = sopInstanceUid;

			return identicalDocument;
		}
	}
}
