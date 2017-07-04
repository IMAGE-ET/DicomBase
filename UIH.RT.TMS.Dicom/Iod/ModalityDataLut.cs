/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ModalityDataLut.cs
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

namespace UIH.RT.TMS.Dicom.Iod
{
	public class ModalityDataLut : DataLut
	{
		#region Private Fields

		private readonly string _modalityLutType;
		
		#endregion

		#region Constructors

		public ModalityDataLut(int firstMappedPixelValue, int bitsPerEntry, int[] data, string modalityLutType)
			: this(firstMappedPixelValue, bitsPerEntry, data, modalityLutType, null)
		{
		}

		public ModalityDataLut(int firstMappedPixelValue, int bitsPerEntry, int[] data, string modalityLutType, string explanation)
			: base(firstMappedPixelValue, bitsPerEntry, data, explanation)
		{
			_modalityLutType = modalityLutType;
		}

		public ModalityDataLut(ModalityDataLut item)
			: base(item)
		{
			_modalityLutType = item.ModalityLutType;
		}

		protected ModalityDataLut(DataLut dataLut, string modalityLutType)
			: base(dataLut.FirstMappedPixelValue, dataLut.BitsPerEntry, dataLut.Data,
					dataLut.Explanation, dataLut.MinOutputValue, dataLut.MaxOutputValue)
		{
			_modalityLutType = modalityLutType;
		}

		#endregion

		#region Public Properties

		public string ModalityLutType
		{
			get { return _modalityLutType; }
		}

		#endregion

		#region Internal/Public Static Factory Methods
		
		internal static ModalityDataLut Create(DicomElementSq modalityLutSequence, int pixelRepresentation)
		{
			List<DataLut> data = DataLut.Create(modalityLutSequence, pixelRepresentation != 0, false);
			if (data.Count == 0)
				return null;

			string modalityLutType = ((DicomSequenceItem[]) modalityLutSequence.Values)[0][DicomTags.ModalityLutType].ToString();
			return new ModalityDataLut(data[0], modalityLutType);
		}

		public static ModalityDataLut Create(IDicomElementProvider dicomElementProvider)
		{
			DicomElementSq modalityLutSequence = (DicomElementSq)dicomElementProvider[DicomTags.ModalityLutSequence];
			int pixelRepresentation = GetPixelRepresentation(dicomElementProvider);

			return Create(modalityLutSequence, pixelRepresentation);
		}

		#endregion
	}
}
