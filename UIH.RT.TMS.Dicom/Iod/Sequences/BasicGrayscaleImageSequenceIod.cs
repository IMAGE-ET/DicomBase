/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: BasicGrayscaleImageSequenceIod.cs
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
using System.IO;
using UIH.RT.Framework.Utility;
using UIH.RT.TMS.Common;

namespace UIH.RT.TMS.Dicom.Iod.Sequences
{
    /// <summary>
    /// Scheduled Procedure Step Sequence
    /// </summary>
    /// <remarks>As per Dicom Doc 3, Table C.13-5 (pg 871)</remarks>
    public class BasicGrayscaleImageSequenceIod : SequenceIodBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicGrayscaleImageSequenceIod"/> class.
        /// </summary>
        public BasicGrayscaleImageSequenceIod()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicGrayscaleImageSequenceIod"/> class.
        /// </summary>
        /// <param name="dicomSequenceItem">The dicom sequence item.</param>
        public BasicGrayscaleImageSequenceIod(DicomSequenceItem dicomSequenceItem)
            : base(dicomSequenceItem)
        {
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the samples per pixel.  Number of samples (planes) in this image.
        /// <para>Possible values for Basic Grayscale Sequence Iod is 1.</para>
        /// </summary>
        /// <value>The samples per pixel.</value>
        /// <remarks>See Part 3, C.7.6.3.1.1 for more info.</remarks>
        public ushort SamplesPerPixel
        {
            get { return base.DicomElementProvider[DicomTags.SamplesPerPixel].GetUInt16(0, 0); }
            set { base.DicomElementProvider[DicomTags.SamplesPerPixel].SetUInt16(0, value); }
        }

        /// <summary>
        /// Gets or sets the photometric interpretation.
        /// <para>Possible values for Basic Grayscale SequenceIod are MONOCHOME1 or MONOCHROME2.</para>
        /// </summary>
        /// <value>The photometric interpretation.</value>
        public PhotometricInterpretation PhotometricInterpretation
        {
            get { return PhotometricInterpretation.FromCodeString(base.DicomElementProvider[DicomTags.PhotometricInterpretation].GetString(0, String.Empty)); }
            set
            {
                if (value == null)
                    base.DicomElementProvider[DicomTags.PhotometricInterpretation] = null;
                else
                    base.DicomElementProvider[DicomTags.PhotometricInterpretation].SetStringValue(value.Code);
            }
        }

        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>The rows.</value>
        public ushort Rows
        {
            get { return base.DicomElementProvider[DicomTags.Rows].GetUInt16(0, 0); }
            set { base.DicomElementProvider[DicomTags.Rows].SetUInt16(0, value); }
        }

        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        /// <value>The columns.</value>
        public ushort Columns
        {
            get { return base.DicomElementProvider[DicomTags.Columns].GetUInt16(0, 0); }
            set { base.DicomElementProvider[DicomTags.Columns].SetUInt16(0, value); }
        }

        /// <summary>
        /// Gets or sets the pixel aspect ratio.
        /// </summary>
        /// <value>The pixel aspect ratio.</value>
        public PixelAspectRatio PixelAspectRatio
        {
            get { return PixelAspectRatio.FromString(base.DicomElementProvider[DicomTags.PixelAspectRatio].ToString()); }
            set
            {
                if (value == null || value.IsNull)
                    base.DicomElementProvider[DicomTags.PixelAspectRatio].SetNullValue();
                else
                    base.DicomElementProvider[DicomTags.PixelAspectRatio].SetStringValue(value.ToString());
            }
        }

        /// <summary>
        /// Gets or sets the bits allocated.
        /// <para>Possible values for Bits Allocated are 8 (if Bits Stored = 8) or 16 (if Bits Stored = 12).</para>
        /// </summary>
        /// <value>The bits allocated.</value>
        public ushort BitsAllocated
        {
            get { return base.DicomElementProvider[DicomTags.BitsAllocated].GetUInt16(0, 0); }
            set { base.DicomElementProvider[DicomTags.BitsAllocated].SetUInt16(0, value); }
        }

        /// <summary>
        /// Gets or sets the bits stored.
        /// <para>Possible values for Bits Stored are 8 or 12.</para>
        /// </summary>
        /// <value>The bits stored.</value>
        public ushort BitsStored
        {
            get { return base.DicomElementProvider[DicomTags.BitsStored].GetUInt16(0, 0); }
            set { base.DicomElementProvider[DicomTags.BitsStored].SetUInt16(0, value); }
        }

        /// <summary>
        /// Gets or sets the high bit.
        /// <para>Possible values for High Bit are 7 (if Bits Stored = 8) or 11 (if Bits Stored = 12).</para>
        /// </summary>
        /// <value>The high bit.</value>
        public ushort HighBit
        {
            get { return base.DicomElementProvider[DicomTags.HighBit].GetUInt16(0, 0); }
            set { base.DicomElementProvider[DicomTags.HighBit].SetUInt16(0, value); }
        }

        /// <summary>
        /// Gets or sets the pixel representation.Data representation of the pixel samples. 
        /// Each sample shall have the same pixel representation. 
        /// <para>Possible values for Basic Grayscale Sequence Iod is 0 (000H).</para>
        /// </summary>
        /// <value>The pixel representation.</value>
        public ushort PixelRepresentation
        {
            get { return base.DicomElementProvider[DicomTags.PixelRepresentation].GetUInt16(0, 0); }
            set { base.DicomElementProvider[DicomTags.PixelRepresentation].SetUInt16(0, value); }
        }

        /// <summary>
        /// Gets or sets the pixel data.
        /// </summary>
        /// <value>The pixel data.</value>
        public byte[] PixelData
        {
            get
            {
                DicomElement element = base.DicomElementProvider[DicomTags.PixelData];
                if (!element.IsNull && !element.IsEmpty)
                    return (byte[])element.Values;
                else
                    return null;
            }
            set { base.DicomElementProvider[DicomTags.PixelData].Values = value; }
        }

        #endregion

        /// <summary>
        /// Adds the dicom file values.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <exception cref="FileNotFoundException"/>
        public void AddDicomFileValues(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            DicomFile dicomFile = new DicomFile(filePath);
            dicomFile.Load();
            AddDicomFileValues(dicomFile.DataSet);
        }

        /// <summary>
        /// Adds the attribute values for the specified <see cref="dicomFile"/>.  Tags it sets are:
        /// ImageType, SopClassUid, SopInstanceUid, StudyInstanceUid, SamplesPerPixel, PhotometricInterpretation,NumberOfFrames,
        /// Rows, Columns, BitsAllocated,BitsStored, HighBit,  PixelRepresentation, SmallestImagePixelValue, LargestImagePixelValue,
        /// WindowCenter, WindowWidth, PixelData.
        /// </summary>
        public void AddDicomFileValues(DicomFile dicomFile)
        {
            if (dicomFile == null)
                throw new ArgumentNullException("dicomFile");
            AddDicomFileValues(dicomFile.DataSet);
        }

        public void AddDicomFileValues(IDicomElementProvider dicomElements)
        {
			uint[] dicomTags = new uint[]
                {
                    DicomTags.ImageType,
                    DicomTags.SopClassUid,
                    DicomTags.SopInstanceUid,
                    DicomTags.StudyInstanceUid,
                    DicomTags.SamplesPerPixel,
                    DicomTags.PhotometricInterpretation,
                    DicomTags.NumberOfFrames,
                    DicomTags.Rows,
                    DicomTags.Columns,
                    DicomTags.BitsAllocated,
                    DicomTags.BitsStored,
                    DicomTags.HighBit,
                    DicomTags.PixelRepresentation,
                    DicomTags.SmallestImagePixelValue,
                    DicomTags.LargestImagePixelValue,
                    DicomTags.WindowCenter,
                    DicomTags.WindowWidth,
                    DicomTags.PixelData
                };

            foreach (uint dicomTag in dicomTags)
            {
                try
                {

                    DicomElement dicomElement;
                    if (dicomElements.TryGetAttribute(dicomTag, out dicomElement))
                        DicomElementProvider[dicomTag].Values = dicomElement.Values;
                }
                catch (Exception ex)
                {
                    LogAdapter.Logger.TraceException(ex);
                    throw;
                }
            }
        }
    }
}
