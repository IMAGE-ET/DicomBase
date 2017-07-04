/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: BasicFilmSessionModuleIod.cs
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
using System.ComponentModel;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
    /// <summary>
    /// Basic Film Session Presentation and Relationship Module as per Part 3, C.13.1 (pg 863) and C.13.2 (pg 863)
    /// </summary>
    public class BasicFilmSessionModuleIod : IodBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FilmSessionModuleIod"/> class.
        /// </summary>
        public BasicFilmSessionModuleIod()
            :base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilmSessionModuleIod"/> class.
        /// </summary>
        public BasicFilmSessionModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider)
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Number of copies to be printed for each film of the film session.
        /// </summary>
        /// <value>The number of copies.</value>
        public int NumberOfCopies
        {
            get { return DicomElementProvider[DicomTags.NumberOfCopies].GetInt32(0, 0); }
            set { DicomElementProvider[DicomTags.NumberOfCopies].SetInt32(0, value); }
        }

        /// <summary>
        /// Gets or sets the print priority.
        /// </summary>
        /// <value>The print priority.</value>
        public PrintPriority PrintPriority
        {
            get { return IodBase.ParseEnum<PrintPriority>(DicomElementProvider[DicomTags.PrintPriority].GetString(0, String.Empty), PrintPriority.None); }
            set { IodBase.SetAttributeFromEnum(DicomElementProvider[DicomTags.PrintPriority], value, false); }
        }

        /// <summary>
        /// Type of medium on which the print job will be printed.
        /// </summary>
        /// <value>The type of the medium.</value>
        public MediumType MediumType
        {
            get { return ParseEnum<MediumType>(DicomElementProvider[DicomTags.MediumType].GetString(0, String.Empty), MediumType.None); }
            set { SetAttributeFromEnum(DicomElementProvider[DicomTags.MediumType], value, true); }
        }

        /// <summary>
        /// Gets or sets the film destination.
        /// </summary>
        /// <value>The film destination.</value>
        public FilmDestination FilmDestination
        {
            get { return ParseEnum<FilmDestination>(base.DicomElementProvider[DicomTags.FilmDestination].GetString(0, String.Empty), FilmDestination.None); }
            set { SetAttributeFromEnum(base.DicomElementProvider[DicomTags.FilmDestination], value, false); }
        }

        /// <summary>
        /// Gets or sets the film destination string (in case you want to set it to BIN_i 
        /// </summary>
        /// <value>The film destination string.</value>
        public string FilmDestinationString
        {
            get { return base.DicomElementProvider[DicomTags.FilmDestination].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.FilmDestination].SetString(0, value); }
        }

        /// <summary>
        /// Human readable label that identifies the film session
        /// </summary>
        /// <value>The film session label.</value>
        public string FilmSessionLabel
        {
            get { return base.DicomElementProvider[DicomTags.FilmSessionLabel].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.FilmSessionLabel].SetString(0, value); }
        }

        /// <summary>
        /// Amount of memory allocated for the film session. Value is expressed in KB.
        /// </summary>
        /// <value>The memory allocation.</value>
        public int MemoryAllocation
        {
            get { return base.DicomElementProvider[DicomTags.MemoryAllocation].GetInt32(0, 0); }
            set { base.DicomElementProvider[DicomTags.MemoryAllocation].SetInt32(0, value); }
        }

        /// <summary>
        /// Identification of the owner of the film session
        /// </summary>
        /// <value>The owner id.</value>
        public string OwnerId
        {
            get { return base.DicomElementProvider[DicomTags.OwnerId].GetString(0, String.Empty); }
            set { base.DicomElementProvider[DicomTags.OwnerId].SetString(0, value); }
        }

        /// <summary>
        /// A Sequence which provides references to a set of Film Box SOP Class/Instance pairs. Zero or more Items may be included in this Sequence.
        /// </summary>
        /// <value>The referenced film box sequence list.</value>
        public SequenceIodList<ReferencedInstanceSequenceIod> ReferencedFilmBoxSequenceList
        {
            get
            {
                return new SequenceIodList<ReferencedInstanceSequenceIod>(base.DicomElementProvider[DicomTags.ReferencedFilmBoxSequence] as DicomElementSq);
            }
        }
        
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the commonly used tags in the base dicom element collection.
        /// </summary>
        public void SetCommonTags()
        {
            SetCommonTags(base.DicomElementProvider);
        }
        #endregion

        #region Public Static Methods
        /// <summary>
        /// Sets the commonly used tags in the specified dicom element collection.
        /// </summary>
        public static void SetCommonTags(IDicomElementProvider dicomElementProvider)
        {
            if (dicomElementProvider == null)
                throw new ArgumentNullException("dicomElementProvider");

            dicomElementProvider[DicomTags.NumberOfCopies].SetNullValue();
            dicomElementProvider[DicomTags.PrintPriority].SetNullValue();
            dicomElementProvider[DicomTags.MediumType].SetNullValue();
            dicomElementProvider[DicomTags.FilmDestination].SetNullValue();
            dicomElementProvider[DicomTags.FilmSessionLabel].SetNullValue();
            dicomElementProvider[DicomTags.MemoryAllocation].SetNullValue();
            dicomElementProvider[DicomTags.OwnerId].SetNullValue();
        }
        #endregion
    }

    #region Print Priority Enum
    /// <summary>
    /// enumeration for Print Priority
    /// </summary>
    [TypeConverter(typeof(BasicPrintEnumConverter<PrintPriority>))]
    public enum PrintPriority
    {
        /// <summary>
        /// 
        /// </summary>
        None,
        /// <summary>
        /// 
        /// </summary>
        High,
        /// <summary>
        /// 
        /// </summary>
        Med,
        /// <summary>
        /// 
        /// </summary>
        Low
    }
    #endregion

    #region MediumType Enum
    /// <summary>
    /// Enumeration for Medium Type (Print Film Session Module) as per Part 3, C.13.1 (2000,0030)
    /// </summary>
    [TypeConverter(typeof(BasicPrintEnumConverter<MediumType>))]
    public enum MediumType
    {
        /// <summary>
        /// 
        /// </summary>
        None,
        /// <summary>
        /// 
        /// </summary>
        Paper,
        /// <summary>
        /// 
        /// </summary>
        ClearFilm,
        /// <summary>
        /// 
        /// </summary>
        BlueFilm,
        /// <summary>
        /// 
        /// </summary>
        MammoClearFilm,
        /// <summary>
        /// 
        /// </summary>
        MammoBlueFilm
    }
    #endregion

    #region FilmDestination Enum
    /// <summary>
    /// Enumeration for Film Destination
    /// </summary>
    [TypeConverter(typeof(BasicPrintEnumConverter<FilmDestination>))]
    public enum FilmDestination
    {
        /// <summary>
        /// None
        /// </summary>
        None,
        /// <summary>
        /// the exposed film is stored in film magazine
        /// </summary>
        Magazine,
        /// <summary>
        /// the exposed film is developed in film processor
        /// </summary>
        Processor,

        /// <summary>
        /// Bin_0 to Bin_9
        /// </summary>
        Bin_0,
        Bin_1,
        Bin_2,
        Bin_3,
        Bin_4,
        Bin_5,
        Bin_6,
        Bin_7,
        Bin_8,
        Bin_9
    }
    #endregion
}
