/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: GraphicAnnotation.cs
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

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// GraphicAnnotation Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.10.5 (Table C.10-5)</remarks>
	public class GraphicAnnotationModuleIod : IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GraphicAnnotationModuleIod"/> class.
		/// </summary>	
		public GraphicAnnotationModuleIod() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="GraphicAnnotationModuleIod"/> class.
		/// </summary>
		public GraphicAnnotationModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider) {}

		/// <summary>
		/// Gets or sets the value of GraphicAnnotationSequence in the underlying collection. Type 1.
		/// </summary>
		public GraphicAnnotationSequenceItem[] GraphicAnnotationSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.GraphicAnnotationSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
					return null;

				GraphicAnnotationSequenceItem[] result = new GraphicAnnotationSequenceItem[dicomElement.Count];
				DicomSequenceItem[] items = (DicomSequenceItem[]) dicomElement.Values;
				for (int n = 0; n < items.Length; n++)
					result[n] = new GraphicAnnotationSequenceItem(items[n]);

				return result;
			}
			set
			{
				if (value == null || value.Length == 0)
					throw new ArgumentNullException("value", "GraphicAnnotationSequence is Type 1 Required.");

				DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
				for (int n = 0; n < value.Length; n++)
					result[n] = value[n].DicomSequenceItem;

				base.DicomElementProvider[DicomTags.GraphicAnnotationSequence].Values = result;
			}
		}

		/// <summary>
		/// Gets an enumeration of <see cref="DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags {
			get {
				yield return DicomTags.GraphicAnnotationSequence;
			}
		}
	}
}
