/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: EncapsulatedDocumentSeriesModuleIod.cs
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
using UIH.RT.TMS.Dicom.Iod.Macros.PerformedProcedureStepSummary;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// EncapsulatedDocumentSeries Module
	/// </summary>
	/// <remarks>
	/// <para>As defined in the DICOM Standard 2009, Part 3, Section C.24.1 (Table C.24-1)</para>
	/// </remarks>
	public class EncapsulatedDocumentSeriesModuleIod
		: IodBase, IPerformedProcedureStepSummaryMacro
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EncapsulatedDocumentSeriesModuleIod"/> class.
		/// </summary>
		public EncapsulatedDocumentSeriesModuleIod() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="EncapsulatedDocumentSeriesModuleIod"/> class.
		/// </summary>
		/// <param name="dicomElementProvider">The DICOM attribute provider.</param>
		public EncapsulatedDocumentSeriesModuleIod(IDicomElementProvider dicomElementProvider)
			: base(dicomElementProvider) {}

		/// <summary>
		/// Gets or sets the value of Modality in the underlying collection. Type 1.
		/// </summary>
		public string Modality
		{
			get { return DicomElementProvider[DicomTags.Modality].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("value", "Modality is Type 1 Required.");
				DicomElementProvider[DicomTags.Modality].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of SeriesInstanceUid in the underlying collection. Type 1.
		/// </summary>
		public string SeriesInstanceUid
		{
			get { return DicomElementProvider[DicomTags.SeriesInstanceUid].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("value", "SeriesInstanceUid is Type 1 Required.");
				DicomElementProvider[DicomTags.SeriesInstanceUid].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of SeriesNumber in the underlying collection. Type 1.
		/// </summary>
		public int SeriesNumber
		{
			get { return DicomElementProvider[DicomTags.SeriesNumber].GetInt32(0, 0); }
			set { DicomElementProvider[DicomTags.SeriesNumber].SetInt32(0, value); }
		}

		/// <summary>
		/// Gets or sets the value of ReferencedPerformedProcedureStepSequence in the underlying collection. Type 3.
		/// </summary>
		public ISopInstanceReferenceMacro ReferencedPerformedProcedureStepSequence
		{
			get
			{
				var dicomAttribute = DicomElementProvider[DicomTags.ReferencedPerformedProcedureStepSequence];
				if (dicomAttribute.IsNull || dicomAttribute.IsEmpty)
					return null;
				return new SopInstanceReferenceMacro(((DicomSequenceItem[]) dicomAttribute.Values)[0]);
			}
			set
			{
				if (value == null)
				{
					DicomElementProvider[DicomTags.ReferencedPerformedProcedureStepSequence] = null;
					return;
				}

				var dicomAttribute = DicomElementProvider[DicomTags.ReferencedPerformedProcedureStepSequence];
				dicomAttribute.Values = new[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Creates the ReferencedPerformedProcedureStepSequence in the underlying collection. Type 3.
		/// </summary>
		public ISopInstanceReferenceMacro CreateReferencedPerformedProcedureStepSequence()
		{
			var dicomAttribute = DicomElementProvider[DicomTags.ReferencedPerformedProcedureStepSequence];
			if (dicomAttribute.IsNull || dicomAttribute.IsEmpty)
			{
				var dicomSequenceItem = new DicomSequenceItem();
				dicomAttribute.Values = new[] {dicomSequenceItem};
				var sequenceType = new SopInstanceReferenceMacro(dicomSequenceItem);
				sequenceType.InitializeAttributes();
				return sequenceType;
			}
			return new SopInstanceReferenceMacro(((DicomSequenceItem[]) dicomAttribute.Values)[0]);
		}

		/// <summary>
		/// Gets or sets the value of SeriesDescription in the underlying collection. Type 3.
		/// </summary>
		public string SeriesDescription
		{
			get { return DicomElementProvider[DicomTags.SeriesDescription].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.SeriesDescription] = null;
					return;
				}
				DicomElementProvider[DicomTags.SeriesDescription].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of SeriesDescriptionCodeSequence in the underlying collection. Type 3.
		/// </summary>
		public CodeSequenceMacro SeriesDescriptionCodeSequence
		{
			get
			{
				var dicomAttribute = DicomElementProvider[DicomTags.SeriesDescriptionCodeSequence];
				if (dicomAttribute.IsNull || dicomAttribute.IsEmpty)
					return null;
				return new CodeSequenceMacro(((DicomSequenceItem[]) dicomAttribute.Values)[0]);
			}
			set
			{
				if (value == null)
				{
					DicomElementProvider[DicomTags.SeriesDescriptionCodeSequence] = null;
					return;
				}

				var dicomAttribute = DicomElementProvider[DicomTags.SeriesDescriptionCodeSequence];
				dicomAttribute.Values = new[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Creates the SeriesDescriptionCodeSequence in the underlying collection. Type 3.
		/// </summary>
		public CodeSequenceMacro CreateSeriesDescriptionCodeSequence()
		{
			var dicomAttribute = DicomElementProvider[DicomTags.SeriesDescriptionCodeSequence];
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
		/// Gets or sets the value of RequestAttributesSequence in the underlying collection. Type 3.
		/// </summary>
		public IRequestAttributesMacro RequestAttributesSequence
		{
			get
			{
				var dicomAttribute = DicomElementProvider[DicomTags.RequestAttributesSequence];
				if (dicomAttribute.IsNull || dicomAttribute.IsEmpty)
					return null;
				return new RequestAttributesMacro(((DicomSequenceItem[]) dicomAttribute.Values)[0]);
			}
			set
			{
				if (value == null)
				{
					DicomElementProvider[DicomTags.RequestAttributesSequence] = null;
					return;
				}

				var dicomAttribute = DicomElementProvider[DicomTags.RequestAttributesSequence];
				dicomAttribute.Values = new[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Creates the RequestAttributesSequence in the underlying collection. Type 3.
		/// </summary>
		public IRequestAttributesMacro CreateRequestAttributesSequence()
		{
			var dicomAttribute = DicomElementProvider[DicomTags.RequestAttributesSequence];
			if (dicomAttribute.IsNull || dicomAttribute.IsEmpty)
			{
				var dicomSequenceItem = new DicomSequenceItem();
				dicomAttribute.Values = new[] {dicomSequenceItem};
				var sequenceType = new RequestAttributesMacro(dicomSequenceItem);
				sequenceType.InitializeAttributes();
				return sequenceType;
			}
			return new RequestAttributesMacro(((DicomSequenceItem[]) dicomAttribute.Values)[0]);
		}

		/// <summary>
		/// Initializes the attributes in this module to their default values.
		/// </summary>
		public void InitializeAttributes()
		{
			Modality = @"DOC";
			SeriesInstanceUid = DicomUid.GenerateUid().UID;
			SeriesNumber = 0;
			PerformedProcedureStepSummaryMacro.InitializeAttributes();
		}

		/// <summary>
		/// Gets an enumeration of <see cref="UIH.RT.TMS.Dicom.DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags
		{
			get
			{
				yield return DicomTags.Modality;
				yield return DicomTags.SeriesInstanceUid;
				yield return DicomTags.SeriesNumber;
				yield return DicomTags.ReferencedPerformedProcedureStepSequence;
				yield return DicomTags.SeriesDescription;
				yield return DicomTags.SeriesDescriptionCodeSequence;
				yield return DicomTags.RequestAttributesSequence;

				foreach (var tag in PerformedProcedureStepSummaryMacro.DefinedTags)
					yield return tag;
			}
		}

		#region IPerformedProcedureStepSummaryMacro Members

		private PerformedProcedureStepSummaryMacro PerformedProcedureStepSummaryMacro
		{
			get { return new PerformedProcedureStepSummaryMacro(DicomElementProvider); }
		}

		/// <summary>
		/// Not all macros are sequence items.
		/// </summary>
		DicomSequenceItem IIodMacro.DicomSequenceItem
		{
			get { return DicomElementProvider as DicomSequenceItem; }
			set { DicomElementProvider = value; }
		}

		public string PerformedProcedureStepId
		{
			get { return PerformedProcedureStepSummaryMacro.PerformedProcedureStepId; }
			set { PerformedProcedureStepSummaryMacro.PerformedProcedureStepId = value; }
		}

		public DateTime? PerformedProcedureStepStartDateTime
		{
			get { return PerformedProcedureStepSummaryMacro.PerformedProcedureStepStartDateTime; }
			set { PerformedProcedureStepSummaryMacro.PerformedProcedureStepStartDateTime = value; }
		}

		public string PerformedProcedureStepDescription
		{
			get { return PerformedProcedureStepSummaryMacro.PerformedProcedureStepDescription; }
			set { PerformedProcedureStepSummaryMacro.PerformedProcedureStepDescription = value; }
		}

		public IPerformedProtocolCodeSequence[] PerformedProtocolCodeSequence
		{
			get { return PerformedProcedureStepSummaryMacro.PerformedProtocolCodeSequence; }
			set { PerformedProcedureStepSummaryMacro.PerformedProtocolCodeSequence = value; }
		}

		public string CommentsOnThePerformedProcedureStep
		{
			get { return PerformedProcedureStepSummaryMacro.CommentsOnThePerformedProcedureStep; }
			set { PerformedProcedureStepSummaryMacro.CommentsOnThePerformedProcedureStep = value; }
		}

		public IPerformedProtocolCodeSequence CreatePerformedProtocolCodeSequence()
		{
			return PerformedProcedureStepSummaryMacro.CreatePerformedProtocolCodeSequence();
		}

		#endregion
	}
}
