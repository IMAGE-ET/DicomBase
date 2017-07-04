/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: PrintJobModuleIod.cs
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
    /// Print Job Module as per Part 3 Table C.13-8 page 873
    /// </summary>
    public class PrintJobModuleIod : IodBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PrintJobModuleIod"/> class.
        /// </summary>
        public PrintJobModuleIod()
            :base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrintJobModuleIod"/> class.
        /// </summary>
		public PrintJobModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider)
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the execution status of print job.
        /// </summary>
        /// <value>The execution status.</value>
        public ExecutionStatus ExecutionStatus
        {
            get { return IodBase.ParseEnum<ExecutionStatus>(base.DicomElementProvider[DicomTags.ExecutionStatus].GetString(0, String.Empty), ExecutionStatus.None); }
            set { IodBase.SetAttributeFromEnum(base.DicomElementProvider[DicomTags.ExecutionStatus], value, false); }
        }

        /// <summary>
        /// Gets or sets the execution status info.
        /// <para> Additional information about <see cref="ExecutionStatus"/> (2100,0020). </para>
        /// <para>Defined Terms when the Execution Status is DONE or PRINTING: NORMAL</para>
        /// <para>Defined Terms when the Execution Status is FAILURE: </para>
        /// <para>INVALID PAGE DES = The specified page layout cannot be printed or other page description errors have been detected.</para>
        /// <para>INSUFFIC MEMORY = There is not enough memory available to complete this job.</para>
		/// <para>See Section C.13.9.1 for additional Defined Terms when the Execution Status is PENDING or FAILURE.</para>
        /// </summary>
        /// <value>The execution status info.</value>
        public string ExecutionStatusInfo
        {
            get { return base.DicomElementProvider[DicomTags.ExecutionStatusInfo].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.ExecutionStatusInfo].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the date of print job creation.
        /// </summary>
        /// <value>The creation date.</value>
        public DateTime? CreationDate
        {
            get { return DateTimeParser.ParseDateAndTime(base.DicomElementProvider, 0, DicomTags.CreationDate, DicomTags.CreationTime); }
        
          set { DateTimeParser.SetDateTimeAttributeValues(value, base.DicomElementProvider, 0, DicomTags.CreationDate, DicomTags.CreationTime); }
        }

        /// <summary>
        /// Gets or sets the print priority.
        /// </summary>
        /// <value>The print priority.</value>
        public PrintPriority PrintPriority
        {
            get { return IodBase.ParseEnum<PrintPriority>(base.DicomElementProvider[DicomTags.PrintPriority].GetString(0, String.Empty), PrintPriority.None); }
            set { IodBase.SetAttributeFromEnum(base.DicomElementProvider[DicomTags.PrintPriority], value, false); }
        }

        /// <summary>
        /// Gets or sets the user defined name identifying the printer.
        /// </summary>
        /// <value>The name of the printer.</value>
        public string PrinterName
        {
            get { return base.DicomElementProvider[DicomTags.PrinterName].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.PrinterName].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the DICOM Application Entity Title that issued the print operation.
        /// </summary>
        /// <value>The originator.</value>
        public string Originator
        {
            get { return base.DicomElementProvider[DicomTags.Originator].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.Originator].SetString(0, value); }
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

			dicomElementProvider[DicomTags.CreationDate].SetNullValue();
			dicomElementProvider[DicomTags.PrinterName].SetNullValue();
			dicomElementProvider[DicomTags.Originator].SetNullValue();
			dicomElementProvider[DicomTags.PrintPriority].SetNullValue();
			dicomElementProvider[DicomTags.ExecutionStatus].SetNullValue();
			dicomElementProvider[DicomTags.ExecutionStatusInfo].SetNullValue();
        }
        #endregion
    }

    #region ExecutionStatus Enum
    /// <summary>
    /// Execution status of print job.
    /// </summary>
    public enum ExecutionStatus
    {
        /// <summary>
        /// 
        /// </summary>
        None,
        /// <summary>
        /// 
        /// </summary>
        Pending,
        /// <summary>
        /// 
        /// </summary>
        Printing,
        /// <summary>
        /// 
        /// </summary>
        Done,
        /// <summary>
        /// 
        /// </summary>
        Failure
    }
    #endregion
    
}

