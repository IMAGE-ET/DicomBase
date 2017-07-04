/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ImagePixelMacroIod.cs
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

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
    /// <summary>
    /// Image Pixel Macro Module as per Part 3 Table C.7-11b page 303
    /// </summary>
    public class ImagePixelMacroIod : IodBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ImagePixelMacroIod"/> class.
        /// </summary>
        public ImagePixelMacroIod()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImagePixelMacroIod"/> class.
        /// </summary>
		public ImagePixelMacroIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider)
        {
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the samples per pixel.  Number of samples (planes) in this image.
        /// <para>
        /// Samples per Pixel (0028,0002) is the number of separate planes in this image. One, three, and
        /// four image planes are defined. Other numbers of image planes are allowed, but their meaning is
        /// not defined by this Standard. 
        /// </para>
        /// <para>
        /// For monochrome (gray scale) and palette color images, the number of planes is 1. 
        /// </para>
        /// <para>
        /// For RGB and other three vector color models, the value of this attribute is 3. 
        /// </para>
        /// <para>
        /// For four vector color models, the value of this attribute is 4.
        /// </para>
        /// </summary>
        /// <value>The samples per pixel.</value>
        /// <remarks>See Part 3, C.7.6.3.1.1 for more info.</remarks>
        public ushort SamplesPerPixel
        {
            get { return DicomElementProvider[DicomTags.SamplesPerPixel].GetUInt16(0, 0); }
            set { DicomElementProvider[DicomTags.SamplesPerPixel].SetUInt16(0, value); }
        }

        /// <summary>
        /// Gets or sets the photometric interpretation.
        /// </summary>
        /// <value>The photometric interpretation.</value>
        public PhotometricInterpretation PhotometricInterpretation
        {
            get { return PhotometricInterpretation.FromCodeString(DicomElementProvider[DicomTags.PhotometricInterpretation].GetString(0, String.Empty)); }
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
        /// Gets or sets the bits allocated.
        /// </summary>
        /// <value>The bits allocated.</value>
        public ushort BitsAllocated
        {
            get { return DicomElementProvider[DicomTags.BitsAllocated].GetUInt16(0, 0); }
            set { DicomElementProvider[DicomTags.BitsAllocated].SetUInt16(0, value); }
        }

        /// <summary>
        /// Gets or sets the bits stored.
        /// </summary>
        /// <value>The bits stored.</value>
        public ushort BitsStored
        {
            get { return DicomElementProvider[DicomTags.BitsStored].GetUInt16(0, 0); }
            set { DicomElementProvider[DicomTags.BitsStored].SetUInt16(0, value); }
        }

        /// <summary>
        /// Gets or sets the high bit.
        /// </summary>
        /// <value>The high bit.</value>
        public ushort HighBit
        {
            get { return DicomElementProvider[DicomTags.HighBit].GetUInt16(0, 0); }
            set { DicomElementProvider[DicomTags.HighBit].SetUInt16(0, value); }
        }

        /// <summary>
        /// Gets or sets the pixel representation.Data representation of the pixel samples. 
        /// Each sample shall have the same pixel representation. Enumerated Values: 
        /// <para>0000H = unsigned integer. </para>
        /// <para>0001H = 2's complement</para>
        /// </summary>
        /// <value>The pixel representation.</value>
        public string PixelRepresentation
        {
            get { return DicomElementProvider[DicomTags.PixelRepresentation].GetString(0, String.Empty); }
            set { DicomElementProvider[DicomTags.PixelRepresentation].SetString(0, value); }
        }

        /// <summary>
        /// Gets or sets the pixel data.
        /// </summary>
        /// <value>The pixel data.</value>
        public byte[] PixelData
        {
            get 
            {
            	DicomElement element = DicomElementProvider[DicomTags.PixelData];
				if (!element.IsNull && !element.IsEmpty)
                    return (byte[])DicomElementProvider[DicomTags.PixelData].Values;
                else
                    return null;
            }
            set { DicomElementProvider[DicomTags.PixelData].Values = value; }
        }

        /// <summary>
        /// Gets or sets the planar configuration.
        /// </summary>
        /// <value>The planar configuration.</value>
        public ushort PlanarConfiguration
        {
            get { return base.DicomElementProvider[DicomTags.PlanarConfiguration].GetUInt16(0, 0); }
            set { base.DicomElementProvider[DicomTags.PlanarConfiguration].SetUInt16(0, value); }
        }

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
        /// Gets or sets the smallest image pixel value.
        /// </summary>
        /// <value>The smallest image pixel value.</value>
        public ushort SmallestImagePixelValue
        {
            get { return base.DicomElementProvider[DicomTags.SmallestImagePixelValue].GetUInt16(0, 0); }
            set { base.DicomElementProvider[DicomTags.SmallestImagePixelValue].SetUInt16(0, value); }
        }

        /// <summary>
        /// Gets or sets the largest image pixel value.
        /// </summary>
        /// <value>The largest image pixel value.</value>
        public ushort LargestImagePixelValue
        {
            get { return base.DicomElementProvider[DicomTags.LargestImagePixelValue].GetUInt16(0, 0); }
            set { base.DicomElementProvider[DicomTags.LargestImagePixelValue].SetUInt16(0, value); }
        }

        /// <summary>
        /// Gets or sets the red palette color lookup table descriptor.
        /// </summary>
        /// <value>The red palette color lookup table descriptor.</value>
        public ushort RedPaletteColorLookupTableDescriptor
        {
            get { return base.DicomElementProvider[DicomTags.RedPaletteColorLookupTableDescriptor].GetUInt16(0, 0); }
            set { base.DicomElementProvider[DicomTags.RedPaletteColorLookupTableDescriptor].SetUInt16(0, value); }
        }

        public ushort GreenPaletteColorLookupTableDescriptor
        {
            get { return base.DicomElementProvider[DicomTags.GreenPaletteColorLookupTableDescriptor].GetUInt16(0, 0); }
            set { base.DicomElementProvider[DicomTags.GreenPaletteColorLookupTableDescriptor].SetUInt16(0, value); }
        }

        public ushort BluePaletteColorLookupTableDescriptor
        {
            get { return base.DicomElementProvider[DicomTags.BluePaletteColorLookupTableDescriptor].GetUInt16(0, 0); }
            set { base.DicomElementProvider[DicomTags.BluePaletteColorLookupTableDescriptor].SetUInt16(0, value); }
        }

        //TODO: Red Palette Color Lookup Table Data
        //TODO: Green Palette Color Lookup Table Data
        //TODO: Blue Palette Color Lookup Table Data
        //TODO: IccProfile
        
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

            //dicomElementProvider[DicomTags.NumberOfCopies].SetNullValue();
            //dicomElementProvider[DicomTags.PrintPriority].SetNullValue();
            //dicomElementProvider[DicomTags.MediumType].SetNullValue();
            //dicomElementProvider[DicomTags.FilmDestination].SetNullValue();
            //dicomElementProvider[DicomTags.FilmSessionLabel].SetNullValue();
            //dicomElementProvider[DicomTags.MemoryAllocation].SetNullValue();
            //dicomElementProvider[DicomTags.OwnerId].SetNullValue();
        }

        #endregion
    }
    
}

