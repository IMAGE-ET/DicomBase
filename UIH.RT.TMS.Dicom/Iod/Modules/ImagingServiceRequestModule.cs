/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ImagingServiceRequestModule.cs
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
    /// As per Dicom Doc 3, Table C.4-12 (pg 248)
    /// </summary>
    public class ImagingServiceRequestModule : IodBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ImagingServiceRequestModule"/> class.
        /// </summary>
        public ImagingServiceRequestModule()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImagingServiceRequestModule"/> class.
        /// </summary>
		public ImagingServiceRequestModule(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider)
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the imaging service request comments.
        /// </summary>
        /// <value>The imaging service request comments.</value>
        public string ImagingServiceRequestComments
        {
            get { return base.DicomElementProvider[DicomTags.ImagingServiceRequestComments].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.ImagingServiceRequestComments].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the requesting physician.
        /// </summary>
        /// <value>The requesting physician.</value>
        public PersonName RequestingPhysician
        {
            get { return new PersonName(base.DicomElementProvider[DicomTags.RequestingPhysician].GetString(0, String.Empty)); }
            set { base.DicomElementProvider[DicomTags.RequestingPhysician].SetString(0, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the name of the referring physicians.
        /// </summary>
        /// <value>The name of the referring physicians.</value>
        public PersonName ReferringPhysiciansName
        {
            get { return new PersonName(base.DicomElementProvider[DicomTags.ReferringPhysiciansName].GetString(0, String.Empty)); }
            set { base.DicomElementProvider[DicomTags.ReferringPhysiciansName].SetString(0, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the requesting service.
        /// </summary>
        /// <value>The requesting service.</value>
        public string RequestingService
        {
            get { return base.DicomElementProvider[DicomTags.RequestingService].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.RequestingService].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the accession number.
        /// </summary>
        /// <value>The accession number.</value>
        public string AccessionNumber
        {
            get { return base.DicomElementProvider[DicomTags.AccessionNumber].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.AccessionNumber].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the issue date of imaging service request.
        /// </summary>
        /// <value>The issue date of imaging service request.</value>
        public DateTime? IssueDateOfImagingServiceRequest
        {
        	get { return DateTimeParser.ParseDateAndTime(String.Empty, 
        					base.DicomElementProvider[DicomTags.IssueDateOfImagingServiceRequest].GetString(0, String.Empty), 
                  base.DicomElementProvider[DicomTags.IssueTimeOfImagingServiceRequest].GetString(0, String.Empty)); }

            set { DateTimeParser.SetDateTimeAttributeValues(value, base.DicomElementProvider[DicomTags.IssueDateOfImagingServiceRequest], base.DicomElementProvider[DicomTags.IssueTimeOfImagingServiceRequest]); }
        }

        /// <summary>
        /// Gets or sets the placer order number imaging service request.
        /// </summary>
        /// <value>The placer order number imaging service request.</value>
        public string PlacerOrderNumberImagingServiceRequest
        {
            get { return base.DicomElementProvider[DicomTags.PlacerOrderNumberImagingServiceRequest].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.PlacerOrderNumberImagingServiceRequest].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the filler order number imaging service request.
        /// </summary>
        /// <value>The filler order number imaging service request.</value>
        public string FillerOrderNumberImagingServiceRequest
        {
            get { return base.DicomElementProvider[DicomTags.FillerOrderNumberImagingServiceRequest].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.FillerOrderNumberImagingServiceRequest].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the admission id.
        /// </summary>
        /// <value>The admission id.</value>
        public string AdmissionId
        {
            get { return base.DicomElementProvider[DicomTags.AdmissionId].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.AdmissionId].SetString(0, value); }
        }
        
        #endregion

    }
}
