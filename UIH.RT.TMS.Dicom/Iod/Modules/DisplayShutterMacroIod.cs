/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: DisplayShutterMacroIod.cs
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
using UIH.RT.TMS.Dicom.Utilities;
using System.Drawing;
using System.Text;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// DisplayShutter Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.7.6.11 (Table ?)</remarks>
	public class DisplayShutterModuleIod : DisplayShutterMacroIod
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DisplayShutterModuleIod"/> class.
		/// </summary>	
		public DisplayShutterModuleIod() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="DisplayShutterModuleIod"/> class.
		/// </summary>
		public DisplayShutterModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider) { }
	}

	/// <summary>
	/// DisplayShutter Macro
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.7.6.11 (Table ?)</remarks>
	public class DisplayShutterMacroIod : IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DisplayShutterModuleIod"/> class.
		/// </summary>	
		public DisplayShutterMacroIod() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="DisplayShutterModuleIod"/> class.
		/// </summary>
		public DisplayShutterMacroIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider) { }

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
					throw new ArgumentException("BITMAP is not supported in this module.", "value");
				}
				else
				{
					List<string> shapes = new List<string>(3);
					if ((value & ShutterShape.Circular) == ShutterShape.Circular)
						shapes.Add(ShutterShape.Circular.ToString().ToUpperInvariant());
					if ((value & ShutterShape.Polygonal) == ShutterShape.Polygonal)
						shapes.Add(ShutterShape.Polygonal.ToString().ToUpperInvariant());
					if ((value & ShutterShape.Rectangular) == ShutterShape.Rectangular)
						shapes.Add(ShutterShape.Rectangular.ToString().ToUpperInvariant());
					base.DicomElementProvider[DicomTags.ShutterShape].SetStringValue(string.Join("\\", shapes.ToArray()));
				}
			}
		}

		/// <summary>
		/// Gets or sets the left vertical edge of a rectangular shutter.  Type 1C.
		/// </summary>
		public int? ShutterLeftVerticalEdge
		{
			get
			{
				DicomElement element;
				if (base.DicomElementProvider.TryGetAttribute(DicomTags.ShutterLeftVerticalEdge, out element))
					return element.GetInt32(0, 0);
				else
					return null;
			}
			set
			{
				if (!value.HasValue)
					base.DicomElementProvider[DicomTags.ShutterLeftVerticalEdge] = null;
				else
					base.DicomElementProvider[DicomTags.ShutterLeftVerticalEdge].SetInt32(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the right vertical edge of a rectangular shutter.  Type 1C.
		/// </summary>
		public int? ShutterRightVerticalEdge
		{
			get
			{
				DicomElement element;
				if (base.DicomElementProvider.TryGetAttribute(DicomTags.ShutterRightVerticalEdge, out element))
					return element.GetInt32(0, 0);
				else
					return null;
			}
			set
			{
				if (!value.HasValue)
					base.DicomElementProvider[DicomTags.ShutterRightVerticalEdge] = null;
				else
					base.DicomElementProvider[DicomTags.ShutterRightVerticalEdge].SetInt32(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the upper horizontal edge of a rectangular shutter.  Type 1C.
		/// </summary>
		public int? ShutterUpperHorizontalEdge
		{
			get
			{
				DicomElement element;
				if (base.DicomElementProvider.TryGetAttribute(DicomTags.ShutterUpperHorizontalEdge, out element))
					return element.GetInt32(0, 0);
				else
					return null;
			}
			set
			{
				if (!value.HasValue)
					base.DicomElementProvider[DicomTags.ShutterUpperHorizontalEdge] = null;
				else
					base.DicomElementProvider[DicomTags.ShutterUpperHorizontalEdge].SetInt32(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the lower horizontal edge of a rectangular shutter.  Type 1C.
		/// </summary>
		public int? ShutterLowerHorizontalEdge
		{
			get
			{
				DicomElement element;
				if (base.DicomElementProvider.TryGetAttribute(DicomTags.ShutterLowerHorizontalEdge, out element))
					return element.GetInt32(0, 0);
				else
					return null;
			}
			set
			{
				if (!value.HasValue)
					base.DicomElementProvider[DicomTags.ShutterLowerHorizontalEdge] = null;
				else
					base.DicomElementProvider[DicomTags.ShutterLowerHorizontalEdge].SetInt32(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the center of a circular shutter.  Type 1C.
		/// </summary>
		public Point? CenterOfCircularShutter
		{
			get
			{
				DicomElement element;
				if (base.DicomElementProvider.TryGetAttribute(DicomTags.CenterOfCircularShutter, out element))
				{
					if (element.Count >= 2)
						return new Point(element.GetInt32(1, 0), element.GetInt32(0, 0));
				}

				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					base.DicomElementProvider[DicomTags.CenterOfCircularShutter] = null;
				}
				else
				{
					string val = String.Format("{0}\\{1}", value.Value.Y, value.Value.X);
					base.DicomElementProvider[DicomTags.CenterOfCircularShutter].SetStringValue(val);
				}
			}
		}

		/// <summary>
		/// Gets or sets the radius of a circular shutter.  Type 1C.
		/// </summary>
		public int? RadiusOfCircularShutter
		{
			get
			{
				DicomElement element;
				if (base.DicomElementProvider.TryGetAttribute(DicomTags.RadiusOfCircularShutter, out element))
					return element.GetInt32(0, 0);
				else
					return null;
			}
			set
			{
				if (!value.HasValue)
					base.DicomElementProvider[DicomTags.RadiusOfCircularShutter] = null;
				else
					base.DicomElementProvider[DicomTags.RadiusOfCircularShutter].SetInt32(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the vertices of a polygonal shutter.  Type 1C.
		/// </summary>
		public Point[] VerticesOfThePolygonalShutter
		{
			get
			{
				int[] values;
				DicomElement element = base.DicomElementProvider[DicomTags.VerticesOfThePolygonalShutter];
				if (DicomStringHelper.TryGetIntArray(element, out values))
				{
					long count = element.Count & 0x7ffffffffffffffe; // rounds down to nearest multiple of 2
					Point[] points = new Point[count / 2];
					
					int j = 0;
					for (int i = 0; i < count; i+= 2)
					{
						int row = element.GetInt32(i, 0);
						int column = element.GetInt32(i + 1, 0);
						points[j++] = new Point(column, row);
					}

					return points;
				}
				else
				{
					return new Point[0];
				}
			}
			set
			{
				if (value == null)
				{
					base.DicomElementProvider[DicomTags.VerticesOfThePolygonalShutter] = null;
				}
				else  if (value.Length == 0)
				{
					base.DicomElementProvider[DicomTags.VerticesOfThePolygonalShutter].SetNullValue();
				}
				else
				{
					string[] points = new string[value.Length];
					for (int n = 0; n < points.Length; n++)
						points[n] = string.Format("{0}\\{1}", value[n].Y, value[n].X);
					base.DicomElementProvider[DicomTags.VerticesOfThePolygonalShutter].SetStringValue(string.Join("\\", points));
				}
			}
		}

		/// <summary>
		/// Gets or sets the shutter presentation value.  Type 3.
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
				yield return DicomTags.ShutterLeftVerticalEdge;
				yield return DicomTags.ShutterRightVerticalEdge;
				yield return DicomTags.ShutterLowerHorizontalEdge;
				yield return DicomTags.ShutterUpperHorizontalEdge;
				yield return DicomTags.CenterOfCircularShutter;
				yield return DicomTags.RadiusOfCircularShutter;
				yield return DicomTags.VerticesOfThePolygonalShutter;
				yield return DicomTags.ShutterPresentationValue;
				yield return DicomTags.ShutterPresentationColorCielabValue;
				yield break;
			}
		}
	}
}
