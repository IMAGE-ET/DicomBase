/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ImageQueryIod.cs
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

namespace UIH.RT.TMS.Dicom.Iod.Iods
{
    /// <summary>
    /// IOD for common Image Query Retrieve items.  
    /// </summary>
    public class ImageQueryIod : QueryIodBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageQueryIod"/> class.
        /// </summary>
        public ImageQueryIod()
        {
            SetAttributeFromEnum(DicomElementProvider[DicomTags.QueryRetrieveLevel], QueryRetrieveLevel.Image);
        }

		public ImageQueryIod(IDicomElementProvider dicomElementProvider)
			: base(dicomElementProvider)
		{
			SetAttributeFromEnum(DicomElementProvider[DicomTags.QueryRetrieveLevel], QueryRetrieveLevel.Image);
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
        /// Gets or sets the sop instance uid.
        /// </summary>
        /// <value>The sop instance uid.</value>
        public string SopInstanceUid
        {
            get { return DicomElementProvider[DicomTags.SopInstanceUid].GetString(0, String.Empty); }
            set { DicomElementProvider[DicomTags.SopInstanceUid].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the instance number.
        /// </summary>
        /// <value>The instance number.</value>
        public string InstanceNumber
        {
            get { return DicomElementProvider[DicomTags.InstanceNumber].GetString(0, String.Empty); }
            set { DicomElementProvider[DicomTags.InstanceNumber].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the sop class uid.
        /// </summary>
        /// <value>The sop class uid.</value>
        public string SopClassUid
        {
            get { return DicomElementProvider[DicomTags.SopClassUid].GetString(0, String.Empty); }
            set { DicomElementProvider[DicomTags.SopClassUid].SetString(0, value); }
        }

		/// <summary>
		///  Get the number of Rows
		/// </summary>
		public ushort Rows
		{
			get { return DicomElementProvider[DicomTags.Rows].GetUInt16(0, 0); }
		}

		/// <summary>
		/// Get the number of columns
		/// </summary>
		public ushort Columns
		{
			get { return DicomElementProvider[DicomTags.Columns].GetUInt16(0, 0); }
		}

		/// <summary>
		/// Get the Bits Allocated
		/// </summary>
		public ushort BitsAllocated
		{
			get { return DicomElementProvider[DicomTags.BitsAllocated].GetUInt16(0, 0); }
		}

		/// <summary>
		/// Get the number of frames
		/// </summary>
		public string NumberOfFrames
		{
			get { return DicomElementProvider[DicomTags.NumberOfFrames].GetString(0, String.Empty); }
		}

		/// <summary>
		/// Get the content label
		/// </summary>
		public string ContentLabel
		{
			get { return DicomElementProvider[DicomTags.ContentLabel].GetString(0, String.Empty); }
		}

		/// <summary>
		/// Get the content description
		/// </summary>
		public string ContentDescription
		{
			get { return DicomElementProvider[DicomTags.ContentDescription].GetString(0, String.Empty); }
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
			SetAttributeFromEnum(dicomElementProvider[DicomTags.QueryRetrieveLevel], QueryRetrieveLevel.Image);

			// Set image level..
			dicomElementProvider[DicomTags.SopInstanceUid].SetNullValue();
			dicomElementProvider[DicomTags.InstanceNumber].SetNullValue();
			dicomElementProvider[DicomTags.SopClassUid].SetNullValue();
			// IHE specified Image Query Keys
			dicomElementProvider[DicomTags.Rows].SetNullValue();
			dicomElementProvider[DicomTags.Columns].SetNullValue();
			dicomElementProvider[DicomTags.BitsAllocated].SetNullValue();
			dicomElementProvider[DicomTags.NumberOfFrames].SetNullValue();
			// IHE specified Presentation State Query Keys
			dicomElementProvider[DicomTags.ContentLabel].SetNullValue();
			dicomElementProvider[DicomTags.ContentDescription].SetNullValue();
			dicomElementProvider[DicomTags.PresentationCreationDate].SetNullValue();
			dicomElementProvider[DicomTags.PresentationCreationTime].SetNullValue();
			// IHE specified Report Query Keys
			dicomElementProvider[DicomTags.ReferencedRequestSequence].SetNullValue();
			dicomElementProvider[DicomTags.ContentDate].SetNullValue();
			dicomElementProvider[DicomTags.ContentTime].SetNullValue();
			dicomElementProvider[DicomTags.ConceptNameCodeSequence].SetNullValue();
		}

    	#endregion
    }

}
