/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: BitmapDisplayShutter.cs
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
using UIH.RT.TMS.Dicom;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// BitmapDisplayShutter Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.7.6.15 (Table ?)</remarks>
	public class BitmapDisplayShutterModuleIod : IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BitmapDisplayShutterModuleIod"/> class.
		/// </summary>	
		public BitmapDisplayShutterModuleIod() : base() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="BitmapDisplayShutterModuleIod"/> class.
		/// </summary>
		public BitmapDisplayShutterModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider) { }

		/// <summary>
		/// Gets or sets the shutter shape.  Type 1.
		/// </summary>
		public ShutterShape ShutterShape
		{
			get
			{
				ShutterShape returnValue = ShutterShape.None;
				string[] values = base.DicomElementProvider[DicomTags.ShutterShape].Values as string[];
				if (values != null && values.Length > 0)
				{
					foreach (string value in values)
					{
						string upperValue = value.ToUpperInvariant();
						if (upperValue == "CIRCULAR")
							returnValue |= Iod.ShutterShape.Circular;
						else if (upperValue == "RECTANGULAR")
							returnValue |= Iod.ShutterShape.Rectangular;
						else if (upperValue == "POLYGONAL")
							returnValue |= Iod.ShutterShape.Polygonal;
						else if (upperValue == "BITMAP")
							returnValue |= Iod.ShutterShape.Bitmap;
					}
				}

				return returnValue;
			}
			set
			{
				if (value == ShutterShape.None)
				{
					base.DicomElementProvider[DicomTags.ShutterShape] = null;
				}
				else if ((value & ShutterShape.Bitmap) == ShutterShape.Bitmap)
				{
					base.DicomElementProvider[DicomTags.ShutterShape].SetString(0, ShutterShape.Bitmap.ToString().ToUpperInvariant());
				}
				else
				{
					throw new ArgumentException("Only BITMAP is supported in this module.", "value");
				}
			}
		}

		/// <summary>
		/// Gets or sets the zero-based index of the overlay to use as a bitmap display shutter (0-15).
		/// </summary>
		/// <remarks>
		/// Setting this value will automatically update the <see cref="ShutterOverlayGroup"/> tag.
		/// </remarks>
		/// <seealso cref="ShutterOverlayGroup"/>
		/// <seealso cref="ShutterOverlayGroupTagOffset"/>
		public int ShutterOverlayGroupIndex
		{
			get { return (this.ShutterOverlayGroup - 0x6000)/2; }
			set
			{
				if (value < 0 || value > 15)
					throw new ArgumentOutOfRangeException("value", "Value must be between 0 and 15 inclusive.");
				this.ShutterOverlayGroup = (ushort) (value*2 + 0x6000);
			}
		}

		/// <summary>
		/// Gets or sets the DICOM tag value offset from the defined base tags (such as <see cref="DicomTags.OverlayBitPosition"/>).
		/// </summary>
		/// <remarks>
		/// Setting this value will automatically update the <see cref="ShutterOverlayGroup"/> tag.
		/// </remarks>
		/// <seealso cref="ShutterOverlayGroup"/>
		/// <seealso cref="ShutterOverlayGroupIndex"/>
		public uint ShutterOverlayGroupTagOffset
		{
			get { return (uint) ((this.ShutterOverlayGroup - 0x6000) << 16); }
			set { this.ShutterOverlayGroup = (ushort) ((value >> 16) + 0x6000); }
		}

		/// <summary>
		/// Gets or sets the DICOM group number of the overlay to use as a bitmap display shutter. Type 1.
		/// </summary>
		/// <seealso cref="ShutterOverlayGroupTagOffset"/>
		/// <seealso cref="ShutterOverlayGroupIndex"/>
		public ushort ShutterOverlayGroup
		{
			get
			{
				ushort group = base.DicomElementProvider[DicomTags.ShutterOverlayGroup].GetUInt16(0, 0);
				if ((group & 0xFFE1) != 0x6000)
					return 0x0000;
				return group;
			}
			set
			{
				if ((value & 0xFFE1) != 0x6000)
					throw new ArgumentOutOfRangeException("value", "Overlay group must be an even value between 0x6000 and 0x601E inclusive.");
				base.DicomElementProvider[DicomTags.ShutterOverlayGroup].SetUInt16(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the shutter presentation value.  Type 1.
		/// </summary>
		public ushort? ShutterPresentationValue
		{
			get
			{
				DicomElement element;
				if (base.DicomElementProvider.TryGetAttribute(DicomTags.ShutterPresentationValue, out element))
					return element.GetUInt16(0, 0);
				else
					return null;
			}
			set
			{
				if (!value.HasValue)
					base.DicomElementProvider[DicomTags.ShutterPresentationValue] = null;
				else
					base.DicomElementProvider[DicomTags.ShutterPresentationValue].SetUInt16(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the shutter presentation color value.  Type 3.
		/// </summary>
		public CIELabColor? ShutterPresentationColorCielabValue
		{
			get
			{
				DicomElement element = base.DicomElementProvider[DicomTags.ShutterPresentationColorCielabValue];
				if (element.IsEmpty || element.IsNull)
					return null;

				ushort[] values = element.Values as ushort[];
				if (values != null && values.Length >= 3)
					return new CIELabColor(values[0], values[1], values[2]);
				else
					return null;
			}
			set
			{
				if (!value.HasValue)
					base.DicomElementProvider[DicomTags.ShutterPresentationColorCielabValue] = null;
				else
					base.DicomElementProvider[DicomTags.ShutterPresentationColorCielabValue].Values = value.Value.ToArray();
			}
		}

		/// <summary>
		/// Gets an enumeration of <see cref="DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags
		{
			get
			{
				yield return DicomTags.ShutterShape;
				yield return DicomTags.ShutterOverlayGroup;
				yield return DicomTags.ShutterPresentationValue;
				yield return DicomTags.ShutterPresentationColorCielabValue;
				yield break;
			}
		}
	}
}
