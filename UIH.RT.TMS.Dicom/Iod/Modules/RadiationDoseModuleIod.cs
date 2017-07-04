/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: RadiationDoseModuleIod.cs
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
using UIH.RT.TMS.Dicom.Iod.Macros;
using UIH.RT.TMS.Dicom.Iod.Sequences;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
    /// <summary>
    /// As per Dicom DOC 3 Table C.4-16
    /// </summary>
    public class RadiationDoseModuleIod : IodBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RadiationDoseModuleIod"/> class.
        /// </summary>
        public RadiationDoseModuleIod()
            :base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RadiationDoseModuleIod"/> class.
        /// </summary>
		public RadiationDoseModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider)
        {
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Anatomic structure, space or region that has been exposed to ionizing radiation. 
        /// The sequence may have zero or one Items.
        /// </summary>
        /// <value>The anatomic structure space or region sequence list.</value>
        public SequenceIodList<CodeSequenceMacro> AnatomicStructureSpaceOrRegionSequenceList
        {
            get
            {
                return new SequenceIodList<CodeSequenceMacro>(base.DicomElementProvider[DicomTags.AnatomicStructureSpaceOrRegionSequence] as DicomElementSq);
            }
        }

        /// <summary>
        /// Total duration of X-Ray exposure during fluoroscopy in seconds (pedal time) during this Performed Procedure Step.
        /// </summary>
        /// <value>The total time of fluoroscopy.</value>
        public ushort TotalTimeOfFluoroscopy
        {
            get { return base.DicomElementProvider[DicomTags.TotalTimeOfFluoroscopy].GetUInt16(0, 0); }
            set { base.DicomElementProvider[DicomTags.TotalTimeOfFluoroscopy].SetUInt16(0, value); }
        }

        /// <summary>
        /// Total number of exposures made during this Performed Procedure Step. 
        /// The number includes non-digital and digital exposures.
        /// </summary>
        /// <value>The total number of exposures.</value>
        public ushort TotalNumberOfExposures
        {
            get { return base.DicomElementProvider[DicomTags.TotalNumberOfExposures].GetUInt16(0, 0); }
            set { base.DicomElementProvider[DicomTags.TotalNumberOfExposures].SetUInt16(0, value); }
        }

        /// <summary>
        /// Distance in mm from the source to detector center. 
        /// <para>Note: This value is traditionally referred to as Source Image Receptor Distance (SID).</para>
        /// </summary>
        /// <value>The distance source to detector.</value>
        public float DistanceSourceToDetector
        {
            get { return base.DicomElementProvider[DicomTags.DistanceSourceToDetector].GetFloat32(0, 0.0F); }
            set { base.DicomElementProvider[DicomTags.DistanceSourceToDetector].SetFloat32(0, value); }
        }

        /// <summary>
        /// Distance in mm from the source to the surface of the patient closest to the source during this Performed Procedure Step.
        /// Note: This may be an estimated value based on assumptions about the patient�s body size and habitus.
        /// </summary>
        /// <value>The distance source to entrance.</value>
        public float DistanceSourceToEntrance
        {
            get { return base.DicomElementProvider[DicomTags.DistanceSourceToEntrance].GetFloat32(0, 0.0F); }
            set { base.DicomElementProvider[DicomTags.DistanceSourceToEntrance].SetFloat32(0, value); }
        }

        /// <summary>
        /// Average entrance dose value measured in dGy at the surface of the patient during this Performed Procedure Step.
        /// Note: This may be an estimated value based on assumptions about the patient�s body size and habitus.
        /// </summary>
        /// <value>The entrance dose.</value>
        public ushort EntranceDose
        {
            get { return base.DicomElementProvider[DicomTags.EntranceDose].GetUInt16(0, 0); }
            set { base.DicomElementProvider[DicomTags.EntranceDose].SetUInt16(0, value); }
        }

        /// <summary>
        /// Average entrance dose value measured in mGy at the surface of the patient during this Performed Procedure Step.
        /// Note: This may be an estimated value based on assumptions about the patient�s body size and habitus.
        /// </summary>
        /// <value>The entrance dose in mgy.</value>
        public float EntranceDoseInMgy
        {
            get { return base.DicomElementProvider[DicomTags.EntranceDoseInMgy].GetFloat32(0, 0.0F); }
            set { base.DicomElementProvider[DicomTags.EntranceDoseInMgy].SetFloat32(0, value); }
        }

        /// <summary>
        /// Typical dimension of the exposed area at the detector plane. If Rectangular: ExposeArea1 is row dimension followed by column (ExposeArea2); if Round: ExposeArea1 is diameter. Measured in mm.
        /// </summary>
        /// <value>The exposed area1.</value>
        public float ExposedArea1
        {
            get { return base.DicomElementProvider[DicomTags.ExposedArea].GetFloat32(0, 0.0F); }
            set { base.DicomElementProvider[DicomTags.ExposedArea].SetFloat32(0, value); }
        }

        /// <summary>
        /// Typical dimension of the exposed area at the detector plane. If Rectangular: ExposeArea2 is column dimension (ExposeArea1 is column); if Round: ExposeArea2 is Null...
        /// </summary>
        /// <value>The exposed area2.</value>
        public float ExposedArea2
        {
            get { return base.DicomElementProvider[DicomTags.ExposedArea].GetFloat32(1, 0.0F); }
            set { base.DicomElementProvider[DicomTags.ExposedArea].SetFloat32(1, value); }
        }

        /// <summary>
        /// Total area-dose-product to which the patient was exposed, accumulated over the complete Performed
        /// Procedure Step and measured in dGy*cm*cm, including fluoroscopy.
        /// <para>Notes: 1. The sum of the area dose product of all images of a Series or a Study may not result in
        /// the total area dose product to which the patient was exposed.</para>
        /// <para>2. This may be an estimated value based on assumptions about the patient�s body size and habitus.</para>
        /// </summary>
        /// <value>The image and fluoroscopy area dose product.</value>
        public float ImageAndFluoroscopyAreaDoseProduct
        {
            get { return base.DicomElementProvider[DicomTags.ImageAndFluoroscopyAreaDoseProduct].GetFloat32(0, 0.0F); }
            set { base.DicomElementProvider[DicomTags.ImageAndFluoroscopyAreaDoseProduct].SetFloat32(0, value); }
        }

        /// <summary>
        /// User-defined comments on any special conditions related to radiation dose encountered during this Performed Procedure Step.
        /// </summary>
        /// <value>The comments on radiation dose.</value>
        public string CommentsOnRadiationDose
        {
            get { return base.DicomElementProvider[DicomTags.CommentsOnRadiationDose].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.CommentsOnRadiationDose].SetString(0, value); }
        }

        /// <summary>
        /// Exposure Dose Sequence will contain Total Number of Exposures (0040,0301) items plus an item for
        /// each fluoroscopy episode not already counted as an exposure.
        /// </summary>
        /// <value>The exposure dose sequence list.</value>
        public SequenceIodList<ExposureDoseSequenceIod> ExposureDoseSequenceList
        {
            get
            {
                return new SequenceIodList<ExposureDoseSequenceIod>(base.DicomElementProvider[DicomTags.ExposureDoseSequence] as DicomElementSq);
            }
        }
        
        #endregion

    }
}
