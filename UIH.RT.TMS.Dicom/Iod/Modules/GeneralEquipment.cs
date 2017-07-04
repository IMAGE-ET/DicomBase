/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: GeneralEquipment.cs
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

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// GeneralEquipment Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2009, Part 3, Section C.7.5.1 (Table C.7-8)</remarks>
	public class GeneralEquipmentModuleIod : IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GeneralEquipmentModuleIod"/> class.
		/// </summary>	
		public GeneralEquipmentModuleIod() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="GeneralEquipmentModuleIod"/> class.
		/// </summary>
		public GeneralEquipmentModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider) {}

		/// <summary>
		/// Gets or sets the value of Manufacturer in the underlying collection. Type 2.
		/// </summary>
		public string Manufacturer
		{
			get { return DicomElementProvider[DicomTags.Manufacturer].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.Manufacturer].SetNullValue();
					return;
				}
				DicomElementProvider[DicomTags.Manufacturer].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the values of InstitutionName, InstitutionAddress and InstitutionalDepartmentName in the underlying collection. Type 3.
		/// </summary>
		public Institution Institution
		{
			get { return new Institution(InstitutionName, InstitutionAddress, InstitutionalDepartmentName); }
			set
			{
				InstitutionName = value.Name;
				InstitutionAddress = value.Address;
				InstitutionalDepartmentName = value.DepartmentName;
			}
		}

		/// <summary>
		/// Gets or sets the value of InstitutionName in the underlying collection. Type 3.
		/// </summary>
		public string InstitutionName
		{
			get { return DicomElementProvider[DicomTags.InstitutionName].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.InstitutionName] = null;
					return;
				}
				DicomElementProvider[DicomTags.InstitutionName].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of InstitutionAddress in the underlying collection. Type 3.
		/// </summary>
		public string InstitutionAddress
		{
			get { return DicomElementProvider[DicomTags.InstitutionAddress].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.InstitutionAddress] = null;
					return;
				}
				DicomElementProvider[DicomTags.InstitutionAddress].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of StationName in the underlying collection. Type 3.
		/// </summary>
		public string StationName
		{
			get { return DicomElementProvider[DicomTags.StationName].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.StationName] = null;
					return;
				}
				DicomElementProvider[DicomTags.StationName].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of InstitutionalDepartmentName in the underlying collection. Type 3.
		/// </summary>
		public string InstitutionalDepartmentName
		{
			get { return DicomElementProvider[DicomTags.InstitutionalDepartmentName].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.InstitutionalDepartmentName] = null;
					return;
				}
				DicomElementProvider[DicomTags.InstitutionalDepartmentName].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ManufacturersModelName in the underlying collection. Type 3.
		/// </summary>
		public string ManufacturersModelName
		{
			get { return DicomElementProvider[DicomTags.ManufacturersModelName].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.ManufacturersModelName] = null;
					return;
				}
				DicomElementProvider[DicomTags.ManufacturersModelName].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of DeviceSerialNumber in the underlying collection. Type 3.
		/// </summary>
		public string DeviceSerialNumber
		{
			get { return DicomElementProvider[DicomTags.DeviceSerialNumber].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.DeviceSerialNumber] = null;
					return;
				}
				DicomElementProvider[DicomTags.DeviceSerialNumber].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of SoftwareVersions in the underlying collection. Type 3.
		/// </summary>
		public string SoftwareVersions
		{
			get { return DicomElementProvider[DicomTags.SoftwareVersions].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.SoftwareVersions] = null;
					return;
				}
				DicomElementProvider[DicomTags.SoftwareVersions].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of GantryId in the underlying collection. Type 3.
		/// </summary>
		public string GantryId
		{
			get { return DicomElementProvider[DicomTags.GantryId].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.GantryId] = null;
					return;
				}
				DicomElementProvider[DicomTags.GantryId].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of SpatialResolution in the underlying collection. Type 3.
		/// </summary>
		public double? SpatialResolution
		{
			get
			{
				double result;
				if (DicomElementProvider[DicomTags.SpatialResolution].TryGetFloat64(0, out result))
					return result;
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					DicomElementProvider[DicomTags.SpatialResolution] = null;
					return;
				}
				DicomElementProvider[DicomTags.SpatialResolution].SetFloat64(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the value of DateOfLastCalibration and TimeOfLastCalibration in the underlying collection. Type 3.
		/// </summary>
		public DateTime? DateTimeOfLastCalibration
		{
			get
			{
				var date = DicomElementProvider[DicomTags.DateOfLastCalibration].GetString(0, string.Empty);
				var time = DicomElementProvider[DicomTags.TimeOfLastCalibration].GetString(0, string.Empty);
				return DateTimeParser.ParseDateAndTime(string.Empty, date, time);
			}
			set
			{
				if (!value.HasValue)
				{
					DicomElementProvider[DicomTags.DateOfLastCalibration] = null;
					DicomElementProvider[DicomTags.TimeOfLastCalibration] = null;
					return;
				}
				var date = DicomElementProvider[DicomTags.DateOfLastCalibration];
				var time = DicomElementProvider[DicomTags.TimeOfLastCalibration];
				DateTimeParser.SetDateTimeAttributeValues(value, date, time);
			}
		}

		/// <summary>
		/// Gets or sets the value of PixelPaddingValue in the underlying collection. Type 3.
		/// </summary>
		public int? PixelPaddingValue
		{
			get
			{
				int result;
				if (DicomElementProvider[DicomTags.PixelPaddingValue].TryGetInt32(0, out result))
					return result;
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					DicomElementProvider[DicomTags.PixelPaddingValue] = null;
					return;
				}
				DicomElementProvider[DicomTags.PixelPaddingValue].SetInt32(0, value.Value);
			}
		}

		/// <summary>
		/// Initializes the attributes of the module to their default values.
		/// </summary>
		public void InitializeAttributes()
		{
			Manufacturer = string.Empty;
		}

		/// <summary>
		/// Gets an enumeration of <see cref="DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags
		{
			get
			{
				yield return DicomTags.DateOfLastCalibration;
				yield return DicomTags.TimeOfLastCalibration;
				yield return DicomTags.DeviceSerialNumber;
				yield return DicomTags.GantryId;
				yield return DicomTags.InstitutionAddress;
				yield return DicomTags.InstitutionalDepartmentName;
				yield return DicomTags.InstitutionName;
				yield return DicomTags.Manufacturer;
				yield return DicomTags.ManufacturersModelName;
				yield return DicomTags.PixelPaddingValue;
				yield return DicomTags.SoftwareVersions;
				yield return DicomTags.SpatialResolution;
				yield return DicomTags.StationName;
			}
		}
	}
}
