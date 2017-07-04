/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: VoiDataLut.cs
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
using System.Collections.Generic;
using System.Linq;
using UIH.RT.TMS.Common.Utilities;

namespace UIH.RT.TMS.Dicom.Iod
{
	public class VoiDataLut : DataLut
	{
		private bool _alreadyCorrected;

		#region Constructors

		public VoiDataLut(int firstMappedPixelValue, int bitsPerEntry, int[] data)
			: this(firstMappedPixelValue, bitsPerEntry, data, null)
		{
		}

		public VoiDataLut(int firstMappedPixelValue, int bitsPerEntry, int[] data, string explanation)
			: base(firstMappedPixelValue, bitsPerEntry, data, explanation)
		{
		}

		public VoiDataLut(VoiDataLut item)
			: base(item)
		{
		}

		protected VoiDataLut(DataLut dataLut)
			: base(	dataLut.FirstMappedPixelValue, dataLut.BitsPerEntry, dataLut.Data,
					dataLut.Explanation, dataLut.MinOutputValue, dataLut.MaxOutputValue)
		{
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Corrects the <see cref="MinOutputValue"/> and <see cref="MaxOutputValue"/> when <see cref="BitsPerEntry"/> is 16.
		/// </summary>
		/// <remarks>
		/// Some vendors set the <see cref="BitsPerEntry"/> to 16, but then only encode the lut values to the same bit depth
		/// as the actual pixel values (e.g. 12 bits).  This can make the images look really bad.  Apparently, this is the
		/// workaround David Clunie suggests.
		/// </remarks>
		/// <seealso cref="http://groups.google.com/group/comp.protocols.dicom/browse_thread/thread/6a033444802a35fc/0f0a9a1e35c1468e?lnk=gst"/>
		public void CorrectMinMaxOutput()
		{
			if (!_alreadyCorrected && BitsPerEntry == 16)
			{
				_alreadyCorrected = true;
				CorrectMinMaxOutputInternal();
			}
		}

		#endregion

		#region Static Methods

		#region Private

		private static void GetMinMaxPixelValue(int bitsStored, bool isSigned, out int min, out int max)
		{
			if (isSigned)
			{
				min = -(1 << (bitsStored - 1));
				max = (1 << (bitsStored - 1)) - 1;
			}
			else
			{
				min = 0;
				max = (1 << bitsStored) - 1;
			}
		}

		private unsafe void CorrectMinMaxOutputInternal()
		{
			int minLutValue = int.MaxValue;
			int maxLutValue = int.MinValue;

			int numberOfEntries = Data.Length;
			fixed (int* data = Data)
			{
				int* pData = data;
				int i = 0;
				while(i < numberOfEntries)
				{
					if (*pData < minLutValue)
						minLutValue = *pData;
					if (*pData > maxLutValue)
						maxLutValue = *pData;

					++pData;
					++i;
				}
			}

			bool isSigned = FirstMappedPixelValue < 0;
			int trueDepth = 16; //start at 16 and go downwards.

			while(trueDepth > 0)
			{
				int minAllowedValue, maxAllowedValue;
				GetMinMaxPixelValue(trueDepth, isSigned, out minAllowedValue, out maxAllowedValue);
				if (minLutValue < minAllowedValue || maxLutValue > maxAllowedValue)
				{
					//we've found 'true depth minus 1' (we started at 16).
					++trueDepth;
					GetMinMaxPixelValue(trueDepth, isSigned, out minAllowedValue, out maxAllowedValue);

					BitsPerEntry = trueDepth;
					MinOutputValue = minAllowedValue;
					MaxOutputValue = maxAllowedValue;
					
					break;
				}
				else
				{
					//go down one value.
					--trueDepth;
				}
			}
		}

		private static double GetRescaleSlope(IDicomElementProvider elementProvider)
		{
			DicomElement rescaleSlopeElement = elementProvider[DicomTags.RescaleSlope];
			if (rescaleSlopeElement == null)
				return 1.0;
			else
				return rescaleSlopeElement.GetFloat64(0, 1);
		}

		private static List<VoiDataLut> Convert(IEnumerable<DataLut> dataLuts)
		{
            return dataLuts.Select(dataLut => new VoiDataLut(dataLut)).ToList();
		}

		#endregion

		#region Private Factory

		private static List<VoiDataLut> Create(DicomElementSq voiLutSequence, int pixelRepresentation)
		{
			bool isFirstMappedPixelSigned = pixelRepresentation != 0;
			
			List<DataLut> dataLuts = DataLut.Create(voiLutSequence, isFirstMappedPixelSigned, false);
			return Convert(dataLuts);
		}

		private static List<VoiDataLut> Create(DicomElementSq voiLutSequence, int bitsStored, int pixelRepresentation, double rescaleSlope, double rescaleIntercept)
		{
			int minPixelValue;
			int maxPixelValue;
			bool isSigned = pixelRepresentation != 0;

			GetMinMaxPixelValue(bitsStored, isSigned, out minPixelValue, out maxPixelValue);

			double minModalityLutValue = minPixelValue * rescaleSlope + rescaleIntercept;
			double maxModalityLutValue = maxPixelValue * rescaleSlope + rescaleIntercept;

			bool isFirstMappedPixelValueSigned = minModalityLutValue < 0 || maxModalityLutValue < 0;

			List<DataLut> dataLuts = DataLut.Create(voiLutSequence, isFirstMappedPixelValueSigned, false);
			return Convert(dataLuts);
		}

		private static List<VoiDataLut> Create(DicomElementSq voiLutSequence, DicomElementSq modalityLutSequence, int pixelRepresentation)
		{
			ModalityDataLut modalityLut = ModalityDataLut.Create(modalityLutSequence, pixelRepresentation);
			if (modalityLut == null)
				throw new DicomDataException("Input Modality Lut Sequence is not valid.");

			//Hounsfield units are always signed.
			bool isFirstMappedPixelValueSigned = pixelRepresentation != 0 || modalityLut.ModalityLutType == "HU";

			List<DataLut> dataLuts = Create(voiLutSequence, isFirstMappedPixelValueSigned, false);
			return Convert(dataLuts);
		}

		#endregion

		#region Public Factory

		public static List<VoiDataLut> Create(IDicomElementProvider elementProvider)
		{
			if (elementProvider == null)
				throw new ArgumentNullException("elementProvider");

			DicomElementSq voiLutSequence = elementProvider[DicomTags.VoiLutSequence] as DicomElementSq;
			if (voiLutSequence == null)
				return new List<VoiDataLut>();

			DicomElementSq modalityLutSequence = elementProvider[DicomTags.ModalityLutSequence] as DicomElementSq;
			int pixelRepresentation = GetPixelRepresentation(elementProvider);

			if (IsValidAttribute(modalityLutSequence))
				return Create(voiLutSequence, modalityLutSequence, pixelRepresentation);

			DicomElement rescaleInterceptElement = elementProvider[DicomTags.RescaleIntercept];
			if (IsValidAttribute(rescaleInterceptElement))
			{
				double rescaleSlope = GetRescaleSlope(elementProvider);
				double rescaleIntercept = rescaleInterceptElement.GetFloat64(0, 0);
				int bitsStored = GetBitsStored(elementProvider);

				return Create(voiLutSequence, bitsStored, pixelRepresentation, rescaleSlope, rescaleIntercept);
			}

			return Create(voiLutSequence, pixelRepresentation);
		}

		#endregion
		#endregion
	}
}
