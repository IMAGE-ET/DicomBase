/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: EnhancedMammographySeriesModuleIod.cs
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

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// Enhanced Mammography Series Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2011, Part 3, Section C.8.11.10 (Table C.8-76b)</remarks>
	public class EnhancedMammographySeriesModuleIod : IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EnhancedMammographySeriesModuleIod"/> class.
		/// </summary>	
		public EnhancedMammographySeriesModuleIod() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="EnhancedMammographySeriesModuleIod"/> class.
		/// </summary>
		/// <param name="dicomElementProvider">The DICOM attribute collection.</param>
		public EnhancedMammographySeriesModuleIod(IDicomElementProvider dicomElementProvider)
			: base(dicomElementProvider) {}

		/// <summary>
		/// Gets an enumeration of <see cref="DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags
		{
			get
			{
				yield return DicomTags.Modality;
				yield return DicomTags.ReferencedPerformedProcedureStepSequence;
				yield return DicomTags.RequestAttributesSequence;
			}
		}

		/// <summary>
		/// Initializes the underlying collection to implement the module or sequence using default values.
		/// </summary>
		public void InitializeAttributes()
		{
			Modality = "MG";
			ReferencedPerformedProcedureStepSequence = null;
			RequestAttributesSequence = null;
		}

		/// <summary>
		/// Checks if this module appears to be non-empty.
		/// </summary>
		/// <returns>True if the module appears to be non-empty; False otherwise.</returns>
		public bool HasValues()
		{
			return !(IsNullOrEmpty(Modality)
			         && IsNullOrEmpty(ReferencedPerformedProcedureStepSequence)
			         && IsNullOrEmpty(RequestAttributesSequence));
		}

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
		/// Gets or sets the value of ReferencedPerformedProcedureStepSequence in the underlying collection. Type 1C.
		/// </summary>
		public ISopInstanceReferenceMacro ReferencedPerformedProcedureStepSequence
		{
			get
			{
				var dicomAttribute = DicomElementProvider[DicomTags.ReferencedPerformedProcedureStepSequence];
				if (dicomAttribute.IsNull || dicomAttribute.IsEmpty)
				{
					return null;
				}
				return new SopInstanceReferenceMacro(((DicomSequenceItem[]) dicomAttribute.Values)[0]);
			}
			set
			{
				var dicomAttribute = DicomElementProvider[DicomTags.ReferencedPerformedProcedureStepSequence];
				if (value == null)
				{
					DicomElementProvider[DicomTags.ReferencedPerformedProcedureStepSequence] = null;
					return;
				}
				dicomAttribute.Values = new[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Creates the ReferencedPerformedProcedureStepSequence in the underlying collection. Type 1C.
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
		/// Gets or sets the value of RequestAttributesSequence in the underlying collection. Type 3.
		/// </summary>
		public RequestAttributesSequenceIod[] RequestAttributesSequence
		{
			get
			{
				var dicomAttribute = DicomElementProvider[DicomTags.RequestAttributesSequence];
				if (dicomAttribute.IsNull || dicomAttribute.IsEmpty)
				{
					return null;
				}

				var result = new RequestAttributesSequenceIod[dicomAttribute.Count];
				var items = (DicomSequenceItem[]) dicomAttribute.Values;
				for (int n = 0; n < items.Length; n++)
					result[n] = new RequestAttributesSequenceIod(items[n]);

				return result;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					DicomElementProvider[DicomTags.RequestAttributesSequence] = null;
					return;
				}

				var result = new DicomSequenceItem[value.Length];
				for (int n = 0; n < value.Length; n++)
					result[n] = value[n].DicomSequenceItem;

				DicomElementProvider[DicomTags.RequestAttributesSequence].Values = result;
			}
		}

		/// <summary>
		/// Creates a single instance of a RequestAttributesSequence item. Does not modify the RequestAttributesSequence in the underlying collection.
		/// </summary>
		public RequestAttributesSequenceIod CreateRequestAttributesSequence()
		{
			var iodBase = new RequestAttributesSequenceIod(new DicomSequenceItem());
			return iodBase;
		}
	}
}
