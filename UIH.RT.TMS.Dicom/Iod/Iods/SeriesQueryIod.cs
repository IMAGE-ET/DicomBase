/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: SeriesQueryIod.cs
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
using UIH.RT.TMS.Dicom.Iod.Sequences;
using UIH.RT.TMS.Dicom.Utilities;

namespace UIH.RT.TMS.Dicom.Iod.Iods
{
    /// <summary>
    /// IOD for common Series Query Retrieve items.
    /// </summary>
    public class SeriesQueryIod : QueryIodBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SeriesQueryIod"/> class.
        /// </summary>
        public SeriesQueryIod()
        {
            SetAttributeFromEnum(DicomElementProvider[DicomTags.QueryRetrieveLevel], QueryRetrieveLevel.Series);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SeriesQueryIod"/> class.
        /// </summary>
		public SeriesQueryIod(IDicomElementProvider dicomElementProvider)
			: base(dicomElementProvider)
        {
            SetAttributeFromEnum(DicomElementProvider[DicomTags.QueryRetrieveLevel], QueryRetrieveLevel.Series);
        }
        #endregion

        #region Public Properties

		/// <summary>
		/// Gets or sets the study instance uid.
		/// </summary>
		/// <value>The study instance uid.</value>
		public string StudyInstanceUid
		{
			get { return DicomElementProvider[DicomTags.StudyInstanceUid].GetString(0, String.Empty); }
			set { DicomElementProvider[DicomTags.StudyInstanceUid].SetString(0, value); }
		}

        /// <summary>
        /// Gets or sets the series instance uid.
        /// </summary>
        /// <value>The series instance uid.</value>
        public string SeriesInstanceUid
        {
            get { return DicomElementProvider[DicomTags.SeriesInstanceUid].GetString(0, String.Empty); }
            set { DicomElementProvider[DicomTags.SeriesInstanceUid].SetString(0, value); }
        }

		/// <summary>
		/// Gets or sets the modality.
		/// </summary>
		/// <value>The modality.</value>
		public string Modality
		{
			get { return DicomElementProvider[DicomTags.Modality].GetString(0, String.Empty); }
			set { DicomElementProvider[DicomTags.Modality].SetString(0, value); }
		}

		/// <summary>
        /// Gets or sets the series description.
        /// </summary>
        /// <value>The series description.</value>
        public string SeriesDescription
        {
            get { return DicomElementProvider[DicomTags.SeriesDescription].GetString(0, String.Empty); }
            set { DicomElementProvider[DicomTags.SeriesDescription].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the series number.
        /// </summary>
        /// <value>The series number.</value>
        public string SeriesNumber
        {
            get { return DicomElementProvider[DicomTags.SeriesNumber].GetString(0, String.Empty); }
            set { DicomElementProvider[DicomTags.SeriesNumber].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the number of series related instances.
        /// </summary>
        /// <value>The number of series related instances.</value>
        public uint NumberOfSeriesRelatedInstances
        {
            get { return DicomElementProvider[DicomTags.NumberOfSeriesRelatedInstances].GetUInt32(0, 0); }
            set { DicomElementProvider[DicomTags.NumberOfSeriesRelatedInstances].SetUInt32(0, value); }
        }

        /// <summary>
        /// Gets or sets the series date.
        /// </summary>
        /// <value>The series date.</value>
        public DateTime? SeriesDate
        {
            get { return DateTimeParser.ParseDateAndTime(String.Empty, 
                    DicomElementProvider[DicomTags.SeriesDate].GetString(0, String.Empty), 
                    DicomElementProvider[DicomTags.SeriesTime].GetString(0, String.Empty)); }

            set { DateTimeParser.SetDateTimeAttributeValues(value, DicomElementProvider[DicomTags.SeriesDate], DicomElementProvider[DicomTags.SeriesTime]); }
        }

        /// <summary>
        /// Gets or sets the performed procedure step start date.
        /// </summary>
        /// <value>The performed procedure step start date.</value>
        public DateTime? PerformedProcedureStepStartDate
        {
            get { return DateTimeParser.ParseDateAndTime(String.Empty, 
                    DicomElementProvider[DicomTags.PerformedProcedureStepStartDate].GetString(0, String.Empty), 
                    DicomElementProvider[DicomTags.PerformedProcedureStepStartTime].GetString(0, String.Empty)); }

            set { DateTimeParser.SetDateTimeAttributeValues(value, DicomElementProvider[DicomTags.PerformedProcedureStepStartDate], DicomElementProvider[DicomTags.PerformedProcedureStepStartTime]); }
        }

		/// <summary>
		/// Gets the request attributes sequence list.
		/// </summary>
		/// <value>The request attributes sequence list.</value>
		public SequenceIodList<RequestAttributesSequenceIod> RequestAttributesSequence
		{
			get
			{
				return new SequenceIodList<RequestAttributesSequenceIod>(DicomElementProvider[DicomTags.RequestAttributesSequence] as DicomElementSq);
			}
		}
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the common tags for a query retrieve request.
        /// </summary>
        public void SetCommonTags()
        {
            SetCommonTags(DicomElementProvider);
        }

        public static void SetCommonTags(IDicomElementProvider dicomElementProvider)
        {
			SetAttributeFromEnum(dicomElementProvider[DicomTags.QueryRetrieveLevel], QueryRetrieveLevel.Series);

			dicomElementProvider[DicomTags.SeriesInstanceUid].SetNullValue();
			dicomElementProvider[DicomTags.Modality].SetNullValue();
			dicomElementProvider[DicomTags.SeriesDescription].SetNullValue();
			dicomElementProvider[DicomTags.NumberOfSeriesRelatedInstances].SetNullValue();
			dicomElementProvider[DicomTags.SeriesNumber].SetNullValue();
			dicomElementProvider[DicomTags.SeriesDate].SetNullValue();
			dicomElementProvider[DicomTags.SeriesTime].SetNullValue();
			dicomElementProvider[DicomTags.RequestAttributesSequence].SetNullValue();
			dicomElementProvider[DicomTags.PerformedProcedureStepStartDate].SetNullValue();
			dicomElementProvider[DicomTags.PerformedProcedureStepStartTime].SetNullValue();
        }
        #endregion
    }

}
