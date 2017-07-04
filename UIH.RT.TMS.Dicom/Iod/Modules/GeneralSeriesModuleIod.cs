/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: GeneralSeriesModuleIod.cs
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
using UIH.RT.TMS.Dicom.Iod.Macros.PerformedProcedureStepSummary.PerformedProtocolCodeSequence;
using UIH.RT.TMS.Dicom.Iod.Sequences;
using UIH.RT.TMS.Dicom.Utilities;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// GeneralSeries Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.7.3.1 (Table C.7-5a)</remarks>
	public class GeneralSeriesModuleIod : IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GeneralSeriesModuleIod"/> class.
		/// </summary>	
		public GeneralSeriesModuleIod() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="GeneralSeriesModuleIod"/> class.
		/// </summary>
		public GeneralSeriesModuleIod(IDicomElementProvider provider) : base(provider) { }

		/// <summary>
		/// Initializes the underlying collection to implement the module or sequence using default values.
		/// </summary>
		public void InitializeAttributes()
		{
			this.BodyPartExamined = null;
			this.CommentsOnThePerformedProcedureStep = null;
			this.LargestPixelValueInSeries = null;
			this.Laterality = null;
			//this.Modality = Modality.None;
			this.OperatorIdentificationSequence =null;
			this.OperatorsName = null;
			this.PatientPosition = null;
			this.PerformedProcedureStepDescription = null;
			this.PerformedProcedureStepId = null;
			this.PerformedProcedureStepStartDateTime = null;
			this.PerformedProtocolCodeSequence = null;
			this.PerformingPhysicianIdentificationSequence = null;
			this.PerformingPhysiciansName = null;
			this.ProtocolName = null;
			this.ReferencedPerformedProcedureStepSequence = null;
			this.RelatedSeriesSequence = null;
			this.RequestAttributesSequence = null;
			this.SeriesDateTime = null;
			this.SeriesDescription = null;
			this.SeriesInstanceUid  = "1";
			this.SeriesNumber = null;
			this.SmallestPixelValueInSeries = null;
			this.AnatomicalOrientationType = AnatomicalOrientationType.None;
		}

		/// <summary>
		/// Gets or sets the value of Modality in the underlying collection. Type 1.
		/// </summary>
		public Modality Modality
		{
			get { return ParseEnum(base.DicomElementProvider[DicomTags.Modality].GetString(0, string.Empty), Modality.None); }
			set
			{
				if (value == Modality.None)
					throw new ArgumentOutOfRangeException("value", "Modality is Type 1 Required.");
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
		/// Gets or sets the value of SeriesNumber in the underlying collection. Type 2.
		/// </summary>
		public int? SeriesNumber
		{
			get
			{
				int result;
				if (base.DicomElementProvider[DicomTags.SeriesNumber].TryGetInt32(0, out result))
					return result;
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					base.DicomElementProvider[DicomTags.SeriesNumber].SetNullValue();
					return;
				}
				base.DicomElementProvider[DicomTags.SeriesNumber].SetInt32(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the value of Laterality in the underlying collection. Type 2C.
		/// </summary>
		public string Laterality
		{
			get { return base.DicomElementProvider[DicomTags.Laterality].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.Laterality] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.Laterality].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of SeriesDate and SeriesTime in the underlying collection.  Type 3.
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
				if (!value.HasValue)
				{
					base.DicomElementProvider[DicomTags.SeriesDate] = null;
					base.DicomElementProvider[DicomTags.SeriesTime] = null;
					return;
				}
				DicomElement date = base.DicomElementProvider[DicomTags.SeriesDate];
				DicomElement time = base.DicomElementProvider[DicomTags.SeriesTime];
				DateTimeParser.SetDateTimeAttributeValues(value, date, time);
			}
		}

		/// <summary>
		/// Gets or sets the value of PerformingPhysiciansName in the underlying collection. Type 3.
		/// </summary>
		public string PerformingPhysiciansName
		{
			get { return base.DicomElementProvider[DicomTags.PerformingPhysiciansName].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.PerformingPhysiciansName] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.PerformingPhysiciansName].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of PerformingPhysicianIdentificationSequence in the underlying collection. Type 3.
		/// </summary>
		public PersonIdentificationMacro[] PerformingPhysicianIdentificationSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.PerformingPhysicianIdentificationSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}

				PersonIdentificationMacro[] result = new PersonIdentificationMacro[dicomElement.Count];
				DicomSequenceItem[] items = (DicomSequenceItem[]) dicomElement.Values;
				for (int n = 0; n < items.Length; n++)
					result[n] = new PersonIdentificationMacro(items[n]);

				return result;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.DicomElementProvider[DicomTags.PerformingPhysicianIdentificationSequence] = null;
					return;
				}

				DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
				for (int n = 0; n < value.Length; n++)
					result[n] = value[n].DicomSequenceItem;

				base.DicomElementProvider[DicomTags.PerformingPhysicianIdentificationSequence].Values = result;
			}
		}

		/// <summary>
		/// Gets or sets the value of ProtocolName in the underlying collection. Type 3.
		/// </summary>
		public string ProtocolName
		{
			get { return base.DicomElementProvider[DicomTags.ProtocolName].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.ProtocolName] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.ProtocolName].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of SeriesDescription in the underlying collection. Type 3.
		/// </summary>
		public string SeriesDescription
		{
			get { return base.DicomElementProvider[DicomTags.SeriesDescription].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.SeriesDescription] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.SeriesDescription].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of OperatorsName in the underlying collection. Type 3.
		/// </summary>
		public string OperatorsName
		{
			get { return base.DicomElementProvider[DicomTags.OperatorsName].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.OperatorsName] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.OperatorsName].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of OperatorIdentificationSequence in the underlying collection. Type 3.
		/// </summary>
		public PersonIdentificationMacro[] OperatorIdentificationSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.OperatorIdentificationSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}

				PersonIdentificationMacro[] result = new PersonIdentificationMacro[dicomElement.Count];
				DicomSequenceItem[] items = (DicomSequenceItem[]) dicomElement.Values;
				for (int n = 0; n < items.Length; n++)
					result[n] = new PersonIdentificationMacro(items[n]);

				return result;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.DicomElementProvider[DicomTags.OperatorIdentificationSequence] = null;
					return;
				}

				DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
				for (int n = 0; n < value.Length; n++)
					result[n] = value[n].DicomSequenceItem;

				base.DicomElementProvider[DicomTags.OperatorIdentificationSequence].Values = result;
			}
		}

		/// <summary>
		/// Gets or sets the value of ReferencedPerformedProcedureStepSequence in the underlying collection. Type 3.
		/// </summary>
		public ISopInstanceReferenceMacro ReferencedPerformedProcedureStepSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.ReferencedPerformedProcedureStepSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}
				return new SopInstanceReferenceMacro(((DicomSequenceItem[]) dicomElement.Values)[0]);
			}
			set
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.ReferencedPerformedProcedureStepSequence];
				if (value == null)
				{
					base.DicomElementProvider[DicomTags.ReferencedPerformedProcedureStepSequence] = null;
					return;
				}
				dicomElement.Values = new DicomSequenceItem[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Creates the ReferencedPerformedProcedureStepSequence in the underlying collection. Type 3.
		/// </summary>
		public ISopInstanceReferenceMacro CreateReferencedPerformedProcedureStepSequence()
		{
			DicomElement dicomElement = base.DicomElementProvider[DicomTags.ReferencedPerformedProcedureStepSequence];
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
		/// Gets or sets the value of RelatedSeriesSequence in the underlying collection. Type 3.
		/// </summary>
		public RelatedSeriesSequence[] RelatedSeriesSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.RelatedSeriesSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}

				RelatedSeriesSequence[] result = new RelatedSeriesSequence[dicomElement.Count];
				DicomSequenceItem[] items = (DicomSequenceItem[]) dicomElement.Values;
				for (int n = 0; n < items.Length; n++)
					result[n] = new RelatedSeriesSequence(items[n]);

				return result;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.DicomElementProvider[DicomTags.RelatedSeriesSequence] = null;
					return;
				}

				DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
				for (int n = 0; n < value.Length; n++)
					result[n] = value[n].DicomSequenceItem;

				base.DicomElementProvider[DicomTags.RelatedSeriesSequence].Values = result;
			}
		}

		/// <summary>
		/// Gets or sets the value of BodyPartExamined in the underlying collection. Type 3.
		/// </summary>
		public string BodyPartExamined
		{
			get { return base.DicomElementProvider[DicomTags.BodyPartExamined].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.BodyPartExamined] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.BodyPartExamined].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of PatientPosition in the underlying collection. Type 2C.
		/// </summary>
		public string PatientPosition
		{
			get { return base.DicomElementProvider[DicomTags.PatientPosition].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.PatientPosition] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.PatientPosition].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of SmallestPixelValueInSeries in the underlying collection. Type 3.
		/// </summary>
		public int? SmallestPixelValueInSeries
		{
			get
			{
				int result;
				if (base.DicomElementProvider[DicomTags.SmallestPixelValueInSeries].TryGetInt32(0, out result))
					return result;
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					base.DicomElementProvider[DicomTags.SmallestPixelValueInSeries] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.SmallestPixelValueInSeries].SetInt32(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the value of LargestPixelValueInSeries in the underlying collection. Type 3.
		/// </summary>
		public int? LargestPixelValueInSeries
		{
			get
			{
				int result;
				if (base.DicomElementProvider[DicomTags.LargestPixelValueInSeries].TryGetInt32(0, out result))
					return result;
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					base.DicomElementProvider[DicomTags.LargestPixelValueInSeries] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.LargestPixelValueInSeries].SetInt32(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the value of RequestAttributesSequence in the underlying collection. Type 3.
		/// </summary>
		public IRequestAttributesMacro[] RequestAttributesSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.RequestAttributesSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}

				IRequestAttributesMacro[] result = new IRequestAttributesMacro[dicomElement.Count];
				DicomSequenceItem[] items = (DicomSequenceItem[]) dicomElement.Values;
				for (int n = 0; n < items.Length; n++)
					result[n] = new RequestAttributesMacro(items[n]);

				return result;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.DicomElementProvider[DicomTags.RequestAttributesSequence] = null;
					return;
				}

				DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
				for (int n = 0; n < value.Length; n++)
					result[n] = value[n].DicomSequenceItem;

				base.DicomElementProvider[DicomTags.RequestAttributesSequence].Values = result;
			}
		}

		/// <summary>
		/// Creates a single instance of a RequestAttributesSequence item. Does not modify the RequestAttributesSequence in the underlying collection.
		/// </summary>
		public IRequestAttributesMacro CreateRequestAttributesSequence()
		{
			IRequestAttributesMacro iodBase = new RequestAttributesMacro(new DicomSequenceItem());
			iodBase.InitializeAttributes();
			return iodBase;
		}

		/// <summary>
		/// Gets or sets the value of PerformedProcedureStepId in the underlying collection. Type 3.
		/// </summary>
		public string PerformedProcedureStepId
		{
			get { return base.DicomElementProvider[DicomTags.PerformedProcedureStepId].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.PerformedProcedureStepId] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.PerformedProcedureStepId].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of PerformedProcedureStepStartDate and PerformedProcedureStepStartTime in the underlying collection.  Type 3.
		/// </summary>
		public DateTime? PerformedProcedureStepStartDateTime
		{
			get
			{
				string date = base.DicomElementProvider[DicomTags.PerformedProcedureStepStartDate].GetString(0, string.Empty);
				string time = base.DicomElementProvider[DicomTags.PerformedProcedureStepStartTime].GetString(0, string.Empty);
				return DateTimeParser.ParseDateAndTime(string.Empty, date, time);
			}
			set
			{
				if (!value.HasValue)
				{
					base.DicomElementProvider[DicomTags.PerformedProcedureStepStartDate] = null;
					base.DicomElementProvider[DicomTags.PerformedProcedureStepStartTime] = null;
					return;
				}
				DicomElement date = base.DicomElementProvider[DicomTags.PerformedProcedureStepStartDate];
				DicomElement time = base.DicomElementProvider[DicomTags.PerformedProcedureStepStartTime];
				DateTimeParser.SetDateTimeAttributeValues(value, date, time);
			}
		}

		/// <summary>
		/// Gets or sets the value of PerformedProcedureStepDescription in the underlying collection. Type 3.
		/// </summary>
		public string PerformedProcedureStepDescription
		{
			get { return base.DicomElementProvider[DicomTags.PerformedProcedureStepDescription].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.PerformedProcedureStepDescription] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.PerformedProcedureStepDescription].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of PerformedProtocolCodeSequence in the underlying collection. Type 3.
		/// </summary>
		public IPerformedProtocolCodeSequence[] PerformedProtocolCodeSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.PerformedProtocolCodeSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}

				IPerformedProtocolCodeSequence[] result = new IPerformedProtocolCodeSequence[dicomElement.Count];
				DicomSequenceItem[] items = (DicomSequenceItem[]) dicomElement.Values;
				for (int n = 0; n < items.Length; n++)
					result[n] = new PerformedProtocolCodeSequenceClass(items[n]);

				return result;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.DicomElementProvider[DicomTags.PerformedProtocolCodeSequence] = null;
					return;
				}

				DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
				for (int n = 0; n < value.Length; n++)
					result[n] = value[n].DicomSequenceItem;

				base.DicomElementProvider[DicomTags.PerformedProtocolCodeSequence].Values = result;
			}
		}

		/// <summary>
		/// Creates a single instance of a PerformedProtocolCodeSequence item. Does not modify the PerformedProtocolCodeSequence in the underlying collection.
		/// </summary>
		public IPerformedProtocolCodeSequence CreatePerformedProtocolCodeSequence()
		{
			IPerformedProtocolCodeSequence iodBase = new PerformedProtocolCodeSequenceClass(new DicomSequenceItem());
			iodBase.InitializeAttributes();
			return iodBase;
		}

		/// <summary>
		/// Gets or sets the value of CommentsOnThePerformedProcedureStep in the underlying collection. Type 3.
		/// </summary>
		public string CommentsOnThePerformedProcedureStep
		{
			get { return base.DicomElementProvider[DicomTags.CommentsOnThePerformedProcedureStep].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.CommentsOnThePerformedProcedureStep] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.CommentsOnThePerformedProcedureStep].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the anatomical orientation type.  Type 1C
		/// </summary>
		public AnatomicalOrientationType AnatomicalOrientationType
		{
			get { return ParseEnum(base.DicomElementProvider[DicomTags.AnatomicalOrientationType].GetString(0, String.Empty), AnatomicalOrientationType.None); }
			set
			{
				if (value == AnatomicalOrientationType.None)
				{
					base.DicomElementProvider[DicomTags.AnatomicalOrientationType].SetNullValue();
					return;
				}

				SetAttributeFromEnum(base.DicomElementProvider[DicomTags.AnatomicalOrientationType], value);
			}
		}

		/// <summary>
		/// PerformedProtocol Code Sequence
		/// </summary>
		/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section 10.13 (Table 10-16)</remarks>
		internal class PerformedProtocolCodeSequenceClass : CodeSequenceMacro, IPerformedProtocolCodeSequence
		{
			/// <summary>
			/// Initializes a new instance of the <see cref="PerformedProtocolCodeSequenceClass"/> class.
			/// </summary>
			public PerformedProtocolCodeSequenceClass() : base() {}

			/// <summary>
			/// Initializes a new instance of the <see cref="PerformedProtocolCodeSequenceClass"/> class.
			/// </summary>
			/// <param name="dicomSequenceItem">The dicom sequence item.</param>
			public PerformedProtocolCodeSequenceClass(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) {}

			/// <summary>
			/// Initializes the underlying collection to implement the module or sequence using default values.
			/// </summary>
			public virtual void InitializeAttributes() {}

			/// <summary>
			/// Gets or sets the value of ProtocolContextSequence in the underlying collection. Type 3.
			/// </summary>
			public IProtocolContextSequence[] ProtocolContextSequence
			{
				get
				{
					DicomElement dicomElement = base.DicomElementProvider[DicomTags.ProtocolContextSequence];
					if (dicomElement.IsNull || dicomElement.Count == 0)
					{
						return null;
					}

					ProtocolContextSequenceClass[] result = new ProtocolContextSequenceClass[dicomElement.Count];
					DicomSequenceItem[] items = (DicomSequenceItem[]) dicomElement.Values;
					for (int n = 0; n < items.Length; n++)
						result[n] = new ProtocolContextSequenceClass(items[n]);

					return result;
				}
				set
				{
					if (value == null || value.Length == 0)
					{
						base.DicomElementProvider[DicomTags.ProtocolContextSequence] = null;
						return;
					}

					DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
					for (int n = 0; n < value.Length; n++)
						result[n] = value[n].DicomSequenceItem;

					base.DicomElementProvider[DicomTags.ProtocolContextSequence].Values = result;
				}
			}

			/// <summary>
			/// Creates a single instance of a ProtocolContextSequence item. Does not modify the tag in the underlying collection.
			/// </summary>
			public IProtocolContextSequence CreateProtocolContextSequence()
			{
				IProtocolContextSequence iodBase = new ProtocolContextSequenceClass(new DicomSequenceItem());
				iodBase.InitializeAttributes();
				return iodBase;
			}

			/// <summary>
			/// ProtocolContext Sequence
			/// </summary>
			/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section 10.13 (Table 10-16)</remarks>
			internal class ProtocolContextSequenceClass : ContentItemMacro, IProtocolContextSequence
			{
				/// <summary>
				/// Initializes a new instance of the <see cref="ProtocolContextSequenceClass"/> class.
				/// </summary>
				public ProtocolContextSequenceClass() : base() {}

				/// <summary>
				/// Initializes a new instance of the <see cref="ProtocolContextSequenceClass"/> class.
				/// </summary>
				/// <param name="dicomSequenceItem">The dicom sequence item.</param>
				public ProtocolContextSequenceClass(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) {}

				/// <summary>
				/// Initializes the underlying collection to implement the module or sequence using default values.
				/// </summary>
				public virtual void InitializeAttributes() {}

				/// <summary>
				/// Gets or sets the value of ContentItemModifierSequence in the underlying collection. Type 3.
				/// </summary>
				public ContentItemMacro[] ContentItemModifierSequence
				{
					get
					{
						DicomElement dicomElement = base.DicomElementProvider[DicomTags.ContentItemModifierSequence];
						if (dicomElement.IsNull || dicomElement.Count == 0)
						{
							return null;
						}

						ContentItemMacro[] result = new ContentItemMacro[dicomElement.Count];
						DicomSequenceItem[] items = (DicomSequenceItem[]) dicomElement.Values;
						for (int n = 0; n < items.Length; n++)
							result[n] = new ContentItemMacro(items[n]);

						return result;
					}
					set
					{
						if (value == null || value.Length == 0)
						{
							base.DicomElementProvider[DicomTags.ContentItemModifierSequence] = null;
							return;
						}

						DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
						for (int n = 0; n < value.Length; n++)
							result[n] = value[n].DicomSequenceItem;

						base.DicomElementProvider[DicomTags.ContentItemModifierSequence].Values = result;
					}
				}
			}
		}

		/// <summary>
		/// Gets an enumeration of <see cref="DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags {
			get {
				yield return DicomTags.BodyPartExamined;
				yield return DicomTags.CommentsOnThePerformedProcedureStep;
				yield return DicomTags.LargestPixelValueInSeries;
				yield return DicomTags.Laterality;
				yield return DicomTags.Modality;
				yield return DicomTags.OperatorIdentificationSequence;
				yield return DicomTags.OperatorsName;
				yield return DicomTags.PatientPosition;
				yield return DicomTags.PerformedProcedureStepDescription;
				yield return DicomTags.PerformedProcedureStepId;
				yield return DicomTags.PerformedProcedureStepStartDate;
				yield return DicomTags.PerformedProcedureStepStartTime;
				yield return DicomTags.PerformedProtocolCodeSequence;
				yield return DicomTags.PerformingPhysicianIdentificationSequence;
				yield return DicomTags.PerformingPhysiciansName;
				yield return DicomTags.ProtocolName;
				yield return DicomTags.ReferencedPerformedProcedureStepSequence;
				yield return DicomTags.RelatedSeriesSequence;
				yield return DicomTags.RequestAttributesSequence;
				yield return DicomTags.SeriesDate;
				yield return DicomTags.SeriesTime;
				yield return DicomTags.SeriesDescription;
				yield return DicomTags.SeriesInstanceUid;
				yield return DicomTags.SeriesNumber;
				yield return DicomTags.SmallestPixelValueInSeries;
				yield return DicomTags.AnatomicalOrientationType;
			}
		}
	}
}
