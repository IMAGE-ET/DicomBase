/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ScEquipmentModuleIod.cs
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

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// ScEquipment Module
	/// </summary>
	/// <remarks>
	/// <para>As defined in the DICOM Standard 2009, Part 3, Section C.8.6.1 (Table C.8-24)</para>
	/// </remarks>
	public class ScEquipmentModuleIod
		: IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ScEquipmentModuleIod"/> class.
		/// </summary>	
		public ScEquipmentModuleIod() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="ScEquipmentModuleIod"/> class.
		/// </summary>
		/// <param name="dicomElementProvider">The DICOM attribute provider.</param>
		public ScEquipmentModuleIod(IDicomElementProvider dicomElementProvider)
			: base(dicomElementProvider) {}

		/// <summary>
		/// Gets or sets the value of ConversionType in the underlying collection. Type 1.
		/// </summary>
		public string ConversionType
		{
			get { return DicomElementProvider[DicomTags.ConversionType].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("value", "ConversionType is Type 1 Required.");
				DicomElementProvider[DicomTags.ConversionType].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of Modality in the underlying collection. Type 3.
		/// </summary>
		public string Modality
		{
			get { return DicomElementProvider[DicomTags.Modality].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.Modality] = null;
					return;
				}
				DicomElementProvider[DicomTags.Modality].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of SecondaryCaptureDeviceId in the underlying collection. Type 3.
		/// </summary>
		public string SecondaryCaptureDeviceId
		{
			get { return DicomElementProvider[DicomTags.SecondaryCaptureDeviceId].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.SecondaryCaptureDeviceId] = null;
					return;
				}
				DicomElementProvider[DicomTags.SecondaryCaptureDeviceId].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of SecondaryCaptureDeviceManufacturer in the underlying collection. Type 3.
		/// </summary>
		public string SecondaryCaptureDeviceManufacturer
		{
			get { return DicomElementProvider[DicomTags.SecondaryCaptureDeviceManufacturer].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.SecondaryCaptureDeviceManufacturer] = null;
					return;
				}
				DicomElementProvider[DicomTags.SecondaryCaptureDeviceManufacturer].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of SecondaryCaptureDeviceManufacturersModelName in the underlying collection. Type 3.
		/// </summary>
		public string SecondaryCaptureDeviceManufacturersModelName
		{
			get { return DicomElementProvider[DicomTags.SecondaryCaptureDeviceManufacturersModelName].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.SecondaryCaptureDeviceManufacturersModelName] = null;
					return;
				}
				DicomElementProvider[DicomTags.SecondaryCaptureDeviceManufacturersModelName].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of SecondaryCaptureDeviceSoftwareVersions in the underlying collection. Type 3.
		/// </summary>
		public string[] SecondaryCaptureDeviceSoftwareVersions
		{
			get
			{
				var dicomAttribute = DicomElementProvider[DicomTags.SecondaryCaptureDeviceSoftwareVersions];
				if (dicomAttribute.IsNull || dicomAttribute.IsEmpty)
					return null;

				var result = new string[dicomAttribute.Count];
				for (var n = 0; n < result.Length; n++)
					result[n] = dicomAttribute.GetString(n, string.Empty);
				return result;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					DicomElementProvider[DicomTags.SecondaryCaptureDeviceSoftwareVersions] = null;
					return;
				}

				var dicomAttribute = DicomElementProvider[DicomTags.SecondaryCaptureDeviceSoftwareVersions];
				for (var n = 0; n < value.Length; n++)
					dicomAttribute.SetString(n, value[n]);
			}
		}

		/// <summary>
		/// Gets or sets the value of VideoImageFormatAcquired in the underlying collection. Type 3.
		/// </summary>
		public string VideoImageFormatAcquired
		{
			get { return DicomElementProvider[DicomTags.VideoImageFormatAcquired].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.VideoImageFormatAcquired] = null;
					return;
				}
				DicomElementProvider[DicomTags.VideoImageFormatAcquired].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of DigitalImageFormatAcquired in the underlying collection. Type 3.
		/// </summary>
		public string DigitalImageFormatAcquired
		{
			get { return DicomElementProvider[DicomTags.DigitalImageFormatAcquired].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.DigitalImageFormatAcquired] = null;
					return;
				}
				DicomElementProvider[DicomTags.DigitalImageFormatAcquired].SetStringValue(value);
			}
		}

		/// <summary>
		/// Initializes the attributes in this module to their default values.
		/// </summary>
		public void InitializeAttributes()
		{
			ConversionType = ' '.ToString();
		}

		/// <summary>
		/// Gets an enumeration of <see cref="UIH.RT.TMS.Dicom.DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags
		{
			get
			{
				yield return DicomTags.ConversionType;
				yield return DicomTags.Modality;
				yield return DicomTags.SecondaryCaptureDeviceId;
				yield return DicomTags.SecondaryCaptureDeviceManufacturer;
				yield return DicomTags.SecondaryCaptureDeviceManufacturersModelName;
				yield return DicomTags.SecondaryCaptureDeviceSoftwareVersions;
				yield return DicomTags.VideoImageFormatAcquired;
				yield return DicomTags.DigitalImageFormatAcquired;
			}
		}
	}
}
