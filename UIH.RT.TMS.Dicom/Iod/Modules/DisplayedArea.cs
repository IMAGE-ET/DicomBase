/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: DisplayedArea.cs
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
using System.Drawing;
using UIH.RT.TMS.Dicom.Iod.Macros;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// DisplayedArea Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.10.4 (Table C.10-4)</remarks>
	public class DisplayedAreaModuleIod : IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DisplayedAreaModuleIod"/> class.
		/// </summary>	
		public DisplayedAreaModuleIod() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="DisplayedAreaModuleIod"/> class.
		/// </summary>
		public DisplayedAreaModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider) { }

		/// <summary>
		/// Initializes the underlying collection to implement the module or sequence using default values.
		/// </summary>
		public void InitializeAttributes() {}

		/// <summary>
		/// Gets or sets the value of DisplayedAreaSelectionSequence in the underlying collection. Type 1.
		/// </summary>
		public DisplayedAreaSelectionSequenceItem[] DisplayedAreaSelectionSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.DisplayedAreaSelectionSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
					return null;

				DisplayedAreaSelectionSequenceItem[] result = new DisplayedAreaSelectionSequenceItem[dicomElement.Count];
				DicomSequenceItem[] items = (DicomSequenceItem[]) dicomElement.Values;
				for (int n = 0; n < items.Length; n++)
					result[n] = new DisplayedAreaSelectionSequenceItem(items[n]);

				return result;
			}
			set
			{
				if (value == null || value.Length == 0)
					throw new ArgumentNullException("value", "DisplayedAreaSelectionSequence is Type 1 Required.");

				DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
				for (int n = 0; n < value.Length; n++)
					result[n] = value[n].DicomSequenceItem;

				base.DicomElementProvider[DicomTags.DisplayedAreaSelectionSequence].Values = result;
			}
		}

		/// <summary>
		/// DisplayedAreaSelection Sequence
		/// </summary>
		/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.10.4 (Table C.10-4)</remarks>
		public class DisplayedAreaSelectionSequenceItem : SequenceIodBase
		{
			/// <summary>
			/// Initializes a new instance of the <see cref="DisplayedAreaSelectionSequenceItem"/> class.
			/// </summary>
			public DisplayedAreaSelectionSequenceItem() : base() {}

			/// <summary>
			/// Initializes a new instance of the <see cref="DisplayedAreaSelectionSequenceItem"/> class.
			/// </summary>
			/// <param name="dicomSequenceItem">The dicom sequence item.</param>
			public DisplayedAreaSelectionSequenceItem(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) {}

			/// <summary>
			/// Initializes the underlying collection to implement the module or sequence using default values.
			/// </summary>
			public void InitializeAttributes()
			{
				this.ReferencedImageSequence = null;
				this.PresentationPixelSpacing = null;
				this.PresentationPixelAspectRatio = null;
				this.PresentationPixelMagnificationRatio = null;
			}

			/// <summary>
			/// Gets or sets the value of ReferencedImageSequence in the underlying collection. Type 1C.
			/// </summary>
			public ImageSopInstanceReferenceMacro[] ReferencedImageSequence
			{
				get
				{
					DicomElement dicomElement = base.DicomElementProvider[DicomTags.ReferencedImageSequence];
					if (dicomElement.IsNull || dicomElement.Count == 0)
						return null;

					ImageSopInstanceReferenceMacro[] result = new ImageSopInstanceReferenceMacro[dicomElement.Count];
					DicomSequenceItem[] items = (DicomSequenceItem[]) dicomElement.Values;
					for (int n = 0; n < items.Length; n++)
						result[n] = new ImageSopInstanceReferenceMacro(items[n]);

					return result;
				}
				set
				{
					if (value == null || value.Length == 0)
					{
						base.DicomElementProvider[DicomTags.ReferencedImageSequence] = null;
						return;
					}

					DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
					for (int n = 0; n < value.Length; n++)
						result[n] = value[n].DicomSequenceItem;

					base.DicomElementProvider[DicomTags.ReferencedImageSequence].Values = result;
				}
			}

			/// <summary>
			/// Gets or sets the value of DisplayedAreaTopLeftHandCorner in the underlying collection. Type 1.
			/// </summary>
			public Point DisplayedAreaTopLeftHandCorner
			{
				get
				{
					int x, y;
					if (base.DicomElementProvider[DicomTags.DisplayedAreaTopLeftHandCorner].TryGetInt32(0, out x))
						if (base.DicomElementProvider[DicomTags.DisplayedAreaTopLeftHandCorner].TryGetInt32(1, out y))
							return new Point(x, y);
					return Point.Empty;
				}
				set
				{
					base.DicomElementProvider[DicomTags.DisplayedAreaTopLeftHandCorner].SetInt32(0, value.X);
					base.DicomElementProvider[DicomTags.DisplayedAreaTopLeftHandCorner].SetInt32(1, value.Y);
				}
			}

			/// <summary>
			/// Gets or sets the value of DisplayedAreaBottomRightHandCorner in the underlying collection. Type 1.
			/// </summary>
			public Point DisplayedAreaBottomRightHandCorner
			{
				get
				{
					int x, y;
					if (base.DicomElementProvider[DicomTags.DisplayedAreaBottomRightHandCorner].TryGetInt32(0, out x))
						if (base.DicomElementProvider[DicomTags.DisplayedAreaBottomRightHandCorner].TryGetInt32(1, out y))
							return new Point(x, y);
					return Point.Empty;
				}
				set
				{
					base.DicomElementProvider[DicomTags.DisplayedAreaBottomRightHandCorner].SetInt32(0, value.X);
					base.DicomElementProvider[DicomTags.DisplayedAreaBottomRightHandCorner].SetInt32(1, value.Y);
				}
			}

			/// <summary>
			/// Gets or sets the value of PresentationSizeMode in the underlying collection. Type 1.
			/// </summary>
			public PresentationSizeMode PresentationSizeMode
			{
				get { return ParseEnum(base.DicomElementProvider[DicomTags.PresentationSizeMode].GetString(0, string.Empty), PresentationSizeMode.None); }
				set
				{
					if (value == PresentationSizeMode.None)
						throw new ArgumentOutOfRangeException("value", "PresentationSizeMode is Type 1 Required.");
					SetAttributeFromEnum(base.DicomElementProvider[DicomTags.PresentationSizeMode], value, true);
				}
			}

			/// <summary>
			/// Gets or sets the value of PresentationPixelSpacing in the underlying collection. Type 1C.
			/// </summary>
			public PixelSpacing PresentationPixelSpacing
			{
				get
				{
					DicomElement element = base.DicomElementProvider[DicomTags.PresentationPixelSpacing];
					if (element.IsEmpty || element.IsNull)
						return null;
					return PixelSpacing.FromString(element.ToString());
				}
				set
				{
					if (value == null || value.IsNull)
					{
						base.DicomElementProvider[DicomTags.PresentationPixelSpacing] = null;
						return;
					}
					base.DicomElementProvider[DicomTags.PresentationPixelSpacing].SetStringValue(value.ToString());
				}
			}

			/// <summary>
			/// Gets or sets the value of PresentationPixelAspectRatio in the underlying collection. Type 1C.
			/// </summary>
			public PixelAspectRatio PresentationPixelAspectRatio
			{
				get
				{
					DicomElement element = base.DicomElementProvider[DicomTags.PresentationPixelAspectRatio];
					if (element.IsEmpty || element.IsNull)
						return null;
					return PixelAspectRatio.FromString(element.ToString());
				}
				set
				{
					if (value == null || value.IsNull)
					{
						base.DicomElementProvider[DicomTags.PresentationPixelAspectRatio] = null;
						return;
					}
					base.DicomElementProvider[DicomTags.PresentationPixelAspectRatio].SetStringValue(value.ToString());
				}
			}

			/// <summary>
			/// Gets or sets the value of PresentationPixelMagnificationRatio in the underlying collection. Type 1C.
			/// </summary>
			public double? PresentationPixelMagnificationRatio
			{
				get
				{
					double result;
					if (base.DicomElementProvider[DicomTags.PresentationPixelMagnificationRatio].TryGetFloat64(0, out result))
						return result;
					return null;
				}
				set
				{
					if (!value.HasValue)
					{
						base.DicomElementProvider[DicomTags.PresentationPixelMagnificationRatio] = null;
						return;
					}
					base.DicomElementProvider[DicomTags.PresentationPixelMagnificationRatio].SetFloat64(0, value.Value);
				}
			}
		}

		/// <summary>
		/// Gets an enumeration of <see cref="DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags {
			get {
				yield return DicomTags.DisplayedAreaSelectionSequence;
			}
		}

		/// <summary>
		/// Enumerated values for the <see cref="DicomTags.PresentationSizeMode"/> attribute .
		/// </summary>
		/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.10.4 (Table C.10-4)</remarks>
		public enum PresentationSizeMode
		{
			ScaleToFit,
			TrueSize,
			Magnify,

			/// <summary>
			/// Represents the null value, which is equivalent to the unknown status.
			/// </summary>
			None
		}
	}
}
