/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: BasicPixelSpacingCalibrationMacro.cs
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

namespace UIH.RT.TMS.Dicom.Iod.Macros
{
	/// <summary>
	/// Basic Pixel Spacing Calibration Macro
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2009, Part 3, Section 10.7 (Table 10-10)</remarks>
	public class BasicPixelSpacingCalibrationMacro : IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BasicPixelSpacingCalibrationMacro"/> class.
		/// </summary>
		public BasicPixelSpacingCalibrationMacro() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="BasicPixelSpacingCalibrationMacro"/> class.
		/// </summary>
		/// <param name="dicomElementProvider">The <see cref="IDicomElementProvider"/>.</param>
		public BasicPixelSpacingCalibrationMacro(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider) {}

		/// <summary>
		/// Gets or sets the value of PixelSpacing in the underlying collection. Type 1C.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Physical distance in the patient between the center of each pixel, specified by a numeric
		/// pair - adjacent row spacing (delimiter) adjacent column spacing in mm. See 10.7.1.1 and 10.7.1.3.
		/// Required if the image has been calibrated. May be present otherwise.
		/// </para>
		/// </remarks>
		public double[] PixelSpacing
		{
			get
			{
				double[] result = new double[2];
				if (DicomElementProvider[DicomTags.PixelSpacing].TryGetFloat64(0, out result[0]))
					if (DicomElementProvider[DicomTags.PixelSpacing].TryGetFloat64(1, out result[1]))
						return result;
				return null;
			}
			set
			{
				if (value == null || value.Length != 2)
				{
					DicomElementProvider[DicomTags.PixelSpacing] = null;
					return;
				}
				DicomElementProvider[DicomTags.PixelSpacing].SetFloat64(0, value[0]);
				DicomElementProvider[DicomTags.PixelSpacing].SetFloat64(1, value[1]);
			}
		}

		/// <summary>
		/// Gets or sets the value of PixelSpacingCalibrationType in the underlying collection. Type 3.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The type of correction for the effect of geometric magnification or calibration
		/// against an object of known size, if any. See 10.7.1.2.
		/// </para>
		/// </remarks>
		public PixelSpacingCalibrationType PixelSpacingCalibrationType
		{
			get { return ParseEnum(DicomElementProvider[DicomTags.PixelSpacingCalibrationType].GetString(0, string.Empty), PixelSpacingCalibrationType.None); }
			set
			{
				if (value == PixelSpacingCalibrationType.None)
				{
					DicomElementProvider[DicomTags.PixelSpacingCalibrationType] = null;
					return;
				}
				SetAttributeFromEnum(DicomElementProvider[DicomTags.PixelSpacingCalibrationType], value);
			}
		}

		/// <summary>
		/// Gets or sets the value of PixelSpacingCalibrationDescription in the underlying collection. Type 1C.
		/// </summary>
		/// <remarks>
		/// <para>A free text description of the type of correction or calibration performed.</para>
		/// <list type="number">
		/// <listheader>Notes</listheader>
		/// <item>In the case of correction, the text might include description of the
		/// assumptions made about the body part and geometry and depth within the patient.</item>
		/// <item>in the case of calibration, the text might include a description of the
		/// fiducial and where it is located (e.g., &quot;XYZ device applied to the skin over the greater trochanter&quot;).
		/// </item>
		/// <item>Though it is not required, the Device Module may be used to
		/// describe the specific characteristics and size of the calibration device.</item>
		/// </list>
		/// <para>Required if <see cref="PixelSpacingCalibrationType">Pixel Spacing Calibration Type (0028,0A02)</see> is present.</para>
		/// </remarks>
		public string PixelSpacingCalibrationDescription
		{
			get { return DicomElementProvider[DicomTags.PixelSpacingCalibrationDescription].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.PixelSpacingCalibrationDescription] = null;
					return;
				}
				DicomElementProvider[DicomTags.PixelSpacingCalibrationDescription].SetString(0, value);
			}
		}
	}

	/// <summary>
	/// Enumerated values for the <see cref="DicomTags.PixelSpacingCalibrationType"/> attribute describing the type
	/// of correction for the effect of geometric magnification or calibration against an object of known size, if any.
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2009, Part 3, Section 10.7.1.2</remarks>
	public enum PixelSpacingCalibrationType
	{
		/// <summary>
		/// Represents the null value.
		/// </summary>
		None,

		/// <summary>
		/// Indicates that the Pixel Spacing (0028,0030) values account for assumed or known geometric
		/// magnification effects and correspond to some unspecified depth within the patient; the
		/// Pixel Spacing (0028,0030) values may thus be used for measurement sof objects
		/// located close to the central ray and at the same depth.
		/// </summary>
		Geometry,

		/// <summary>
		/// Indicates that the Pixel Spacing (0028,0030) values have been calibrated by the operator or image
		/// processing software by measurement of an object (fiducial) that is visible in the pixel
		/// data and is of known size and is located close to the central ray; the Pixel Spacing
		/// (0028,0030) values may thus be used for measurements of objects located close to the
		/// central ray and located at the same depth within the patient as the fiducial.
		/// </summary>
		Fiducial
	}
}
