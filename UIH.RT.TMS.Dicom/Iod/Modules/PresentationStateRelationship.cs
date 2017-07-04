/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: PresentationStateRelationship.cs
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
using UIH.RT.TMS.Dicom.Iod.Macros.PresentationStateRelationship;
using UIH.RT.TMS.Dicom.Iod.Sequences;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// PresentationStateRelationship Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.11.11 (Table C.11.11-1)</remarks>
	public class PresentationStateRelationshipModuleIod : IodBase, IPresentationStateRelationshipMacro
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PresentationStateRelationshipModuleIod"/> class.
		/// </summary>	
		public PresentationStateRelationshipModuleIod() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="PresentationStateRelationshipModuleIod"/> class.
		/// </summary>
		public PresentationStateRelationshipModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider) { }

		DicomSequenceItem IIodMacro.DicomSequenceItem
		{
			get { return base.DicomElementProvider as DicomSequenceItem; }
			set { base.DicomElementProvider = value; }
		}

		/// <summary>
		/// Initializes the underlying collection to implement the module or sequence using default values.
		/// </summary>
		public virtual void InitializeAttributes() { }

		/// <summary>
		/// Gets or sets the value of ReferencedSeriesSequence in the underlying collection. Type 1.
		/// </summary>
		public IReferencedSeriesSequence[] ReferencedSeriesSequence {
			get {
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.ReferencedSeriesSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
					return null;

				IReferencedSeriesSequence[] result = new IReferencedSeriesSequence[dicomElement.Count];
				DicomSequenceItem[] items = (DicomSequenceItem[])dicomElement.Values;
				for (int n = 0; n < items.Length; n++)
					result[n] = new PresentationStateRelationshipMacro.ReferencedSeriesSequenceItem(items[n]);

				return result;
			}
			set {
				if (value == null || value.Length == 0)
					throw new ArgumentNullException("value", "ReferencedSeriesSequence is Type 1 Required.");

				DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
				for (int n = 0; n < value.Length; n++)
					result[n] = value[n].DicomSequenceItem;

				base.DicomElementProvider[DicomTags.ReferencedSeriesSequence].Values = result;
			}
		}

		/// <summary>
		/// Creates a single instance of a ReferencedSeriesSequence item. Does not modify the ReferencedSeriesSequence in the underlying collection.
		/// </summary>
		public IReferencedSeriesSequence CreateReferencedSeriesSequence() {
			IReferencedSeriesSequence iodBase = new PresentationStateRelationshipMacro.ReferencedSeriesSequenceItem(new DicomSequenceItem());
			iodBase.InitializeAttributes();
			return iodBase;
		}

		/// <summary>
		/// Gets an enumeration of <see cref="DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags {
			get {
				yield return DicomTags.ReferencedSeriesSequence;
			}
		}
	}
}
