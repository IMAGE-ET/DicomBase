/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: PrinterModuleIod.cs
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
using UIH.RT.TMS.Dicom.Utilities;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
    /// <summary>
    /// Printer Module as per Part 3 Table C.13-9, page 872
    /// </summary>
    public class PrinterModuleIod : IodBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PrinterModuleIod"/> class.
        /// </summary>
        public PrinterModuleIod()
            :base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrinterModuleIod"/> class.
        /// </summary>
		public PrinterModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider)
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the printer status.
        /// </summary>
        /// <value>The printer status.</value>
        public PrinterStatus PrinterStatus
        {
            get { return IodBase.ParseEnum<PrinterStatus>(base.DicomElementProvider[DicomTags.PrinterStatus].GetString(0, String.Empty), PrinterStatus.None); }
            set { IodBase.SetAttributeFromEnum(base.DicomElementProvider[DicomTags.PrinterStatus], value, false); }
        }

        /// <summary>
        /// Gets or sets the printer status info.
        /// </summary>
        /// <value>The printer status info.</value>
        public string PrinterStatusInfo
        {
            get { return base.DicomElementProvider[DicomTags.PrinterStatusInfo].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.PrinterStatusInfo].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the name of the printer.
        /// </summary>
        /// <value>The name of the printer.</value>
        public string PrinterName
        {
            get { return base.DicomElementProvider[DicomTags.PrinterName].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.PrinterName].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the manufacturer.
        /// </summary>
        /// <value>The manufacturer.</value>
        public string Manufacturer
        {
            get { return base.DicomElementProvider[DicomTags.Manufacturer].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.Manufacturer].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the name of the manufacturers model.
        /// </summary>
        /// <value>The name of the manufacturers model.</value>
        public string ManufacturersModelName
        {
            get { return base.DicomElementProvider[DicomTags.ManufacturersModelName].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.ManufacturersModelName].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the device serial number.
        /// </summary>
        /// <value>The device serial number.</value>
        public string DeviceSerialNumber
        {
            get { return base.DicomElementProvider[DicomTags.DeviceSerialNumber].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.DeviceSerialNumber].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the software versions.
        /// </summary>
        /// <value>The software versions.</value>
        public string SoftwareVersions
        {
            get { return base.DicomElementProvider[DicomTags.SoftwareVersions].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.SoftwareVersions].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the date of last calibration.
        /// </summary>
        /// <value>The date of last calibration.</value>
        public DateTime? DateOfLastCalibration
        {
        	get { return DateTimeParser.ParseDateAndTime(String.Empty, 
        					base.DicomElementProvider[DicomTags.DateOfLastCalibration].GetString(0, String.Empty), 
                  base.DicomElementProvider[DicomTags.TimeOfLastCalibration].GetString(0, String.Empty)); }

                  set { DateTimeParser.SetDateTimeAttributeValues(value, base.DicomElementProvider[DicomTags.DateOfLastCalibration], base.DicomElementProvider[DicomTags.TimeOfLastCalibration]); }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the commonly used tags in the base dicom attribute collection.
        /// </summary>
        public void SetCommonTags()
        {
            SetCommonTags(base.DicomElementProvider);
        }
        #endregion

        #region Public Static Methods
        /// <summary>
        /// Sets the commonly used tags in the specified dicom attribute collection.
        /// </summary>
        public static void SetCommonTags(IDicomElementProvider dicomElementProvider)
        {
            if (dicomElementProvider == null)
				throw new ArgumentNullException("dicomElementProvider");

            dicomElementProvider[DicomTags.PrinterStatus].SetNullValue();
            dicomElementProvider[DicomTags.PrinterStatusInfo].SetNullValue();
            dicomElementProvider[DicomTags.PrinterName].SetNullValue();
            dicomElementProvider[DicomTags.Manufacturer].SetNullValue();
            dicomElementProvider[DicomTags.ManufacturersModelName].SetNullValue();
            dicomElementProvider[DicomTags.DeviceSerialNumber].SetNullValue();
            dicomElementProvider[DicomTags.SoftwareVersions].SetNullValue();
            dicomElementProvider[DicomTags.DateOfLastCalibration].SetNullValue();
            dicomElementProvider[DicomTags.TimeOfLastCalibration].SetNullValue();

        }
        #endregion
    }

    #region PrinterStatus Enum
    /// <summary>
    /// Enumeration for Printer Status
    /// </summary>
    public enum PrinterStatus
    {
        /// <summary>
        /// 
        /// </summary>
        None,
        /// <summary>
        /// 
        /// </summary>
        Normal,
        /// <summary>
        /// 
        /// </summary>
        Warning,
        /// <summary>
        /// 
        /// </summary>
        Failure
    }
    #endregion
}
