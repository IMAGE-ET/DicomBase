/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: FilmConsumptionSequenceIod.cs
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
using UIH.RT.TMS.Dicom.Iod.Modules;

namespace UIH.RT.TMS.Dicom.Iod.Sequences
{
    /// <summary>
    /// Film Consumption Sequence.  
    /// </summary>
    /// <remarks>As per Part 3, Table C4.17, pg 260</remarks>
    public class FilmConsumptionSequenceIod : SequenceIodBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FilmConsumptionSequenceIod"/> class.
        /// </summary>
        public FilmConsumptionSequenceIod()
            :base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilmConsumptionSequenceIod"/> class.
        /// </summary>
        /// <param name="dicomSequenceItem">The dicom sequence item.</param>
        public FilmConsumptionSequenceIod(DicomSequenceItem dicomSequenceItem)
            : base(dicomSequenceItem)
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Number of films actually printed.
        /// </summary>
        /// <value>The number of films.</value>
        public int NumberOfFilms
        {
            get { return base.DicomElementProvider[DicomTags.NumberOfFilms].GetInt32(0, 0); }
            set { base.DicomElementProvider[DicomTags.NumberOfFilms].SetInt32(0, value); }
        }

        /// <summary>
        /// Type(s) of medium on which images were printed.
        /// </summary>
        /// <value>The type of the medium.</value>
        public MediumType MediumType
        {
            get { return IodBase.ParseEnum<MediumType>(base.DicomElementProvider[DicomTags.MediumType].GetString(0, String.Empty), MediumType.None); }
            set { IodBase.SetAttributeFromEnum(base.DicomElementProvider[DicomTags.MediumType], value); }
        }

        /// <summary>
        /// Size(s) of film on which images were printed.
        /// </summary>
        /// <value>The film size id.</value>
        public FilmSize FilmSizeId
        {
            get { return FilmSize.FromDicomString(base.DicomElementProvider[DicomTags.FilmSizeId].GetString(0, String.Empty)); }
            set { IodBase.SetAttributeFromEnum(base.DicomElementProvider[DicomTags.FilmSizeId], value.DicomString); }
        }
        
        #endregion
    }
    
}
