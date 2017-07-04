/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: DicomCompressedPixelData.cs
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
using UIH.RT.Framework.Utility;
using UIH.RT.TMS.Common;
using UIH.RT.TMS.Dicom.Codec;
using UIH.RT.TMS.Dicom.IO;
using UIH.RT.TMS.Dicom.Iod;
using UIH.RT.TMS.Dicom.Iod.Macros;

namespace UIH.RT.TMS.Dicom
{
	/// <summary>
	/// Class representing compressed pixel data.
	/// </summary>
	public class DicomCompressedPixelData : DicomPixelData
	{
		#region Protected Members

		private readonly DicomFragmentSequence _sq;

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor from a <see cref="DicomMessageBase"/> instance.
		/// </summary>
		/// <param name="msg">The message to initialize the object from.</param>
		public DicomCompressedPixelData(DicomMessageBase msg)
			: base(msg)
		{
			_sq = (DicomFragmentSequence) msg.DataSet[DicomTags.PixelData];
		}

		/// <summary>
		/// Constructor from a <see cref="DicomDataset"/> instance.
		/// </summary>
		/// <param name="collection">The collection to initialize the object from.</param>
		public DicomCompressedPixelData(DicomDataset collection) : base(collection)
		{
			_sq = (DicomFragmentSequence) collection[DicomTags.PixelData];
		}

		public DicomCompressedPixelData(DicomMessageBase msg, byte[] frameData) : base(msg)
		{
			_sq = new DicomFragmentSequence(DicomTags.PixelData);
			AddFrameFragment(frameData);
			//ByteBuffer buffer = new ByteBuffer(frameData);
			//DicomFragment fragment = new DicomFragment(buffer);
			//_sq.AddFragment(fragment);
			NumberOfFrames = 1;
		}

		/// <summary>
		/// Constructor from a <see cref="DicomUncompressedPixelData"/> instance.
		/// </summary>
		/// <param name="pd">The uuncompressed pixel data attribute to initialize the object from.</param>
		public DicomCompressedPixelData(DicomUncompressedPixelData pd) : base(pd)
		{
			_sq = new DicomFragmentSequence(DicomTags.PixelData);
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Update an <see cref="DicomDataset"/> with pixel data related tags.
		/// </summary>
		/// <param name="dataset">The collection to update.</param>
		public override void UpdateAttributeCollection(DicomDataset dataset)
		{
			if (dataset.Contains(DicomTags.NumberOfFrames) || NumberOfFrames > 1)
				dataset[DicomTags.NumberOfFrames].SetInt32(0, NumberOfFrames);
			if (dataset.Contains(DicomTags.PlanarConfiguration))
				dataset[DicomTags.PlanarConfiguration].SetInt32(0, PlanarConfiguration);
			if (dataset.Contains(DicomTags.LossyImageCompression) || LossyImageCompression.Length > 0)
				dataset[DicomTags.LossyImageCompression].SetString(0, LossyImageCompression);
			if (dataset.Contains(DicomTags.LossyImageCompressionRatio) || (LossyImageCompressionRatio != 1.0f && LossyImageCompressionRatio != 0.0f))
				dataset[DicomTags.LossyImageCompressionRatio].SetFloat32(0, LossyImageCompressionRatio);
			if (dataset.Contains(DicomTags.LossyImageCompressionMethod) || LossyImageCompressionMethod.Length > 0)
				dataset[DicomTags.LossyImageCompressionMethod].SetString(0, LossyImageCompressionMethod);
			if (dataset.Contains(DicomTags.DerivationDescription) || DerivationDescription.Length > 0)
			{
				string currentValue = dataset[DicomTags.DerivationDescription].ToString();

				dataset[DicomTags.DerivationDescription].SetStringValue(DerivationDescription);
				if (!currentValue.Equals(DerivationDescription))
				{
					DicomSequenceItem item = new DicomSequenceItem();
					CodeSequenceMacro macro = new CodeSequenceMacro(item);
					macro.CodeMeaning = "Lossy Compression";
					macro.CodeValue = "113040";
					macro.CodingSchemeDesignator = "DCM";
					macro.ContextGroupVersion = new DateTime(2005, 8, 22);
					macro.ContextIdentifier = "7203";
					macro.MappingResource = "DCMR";

					dataset[DicomTags.DerivationCodeSequence].AddSequenceItem(item);
				}
			}
			if (dataset.Contains(DicomTags.RescaleSlope) || DecimalRescaleSlope != 1.0M || DecimalRescaleIntercept != 0.0M)
				dataset[DicomTags.RescaleSlope].SetString(0, RescaleSlope);
			if (dataset.Contains(DicomTags.RescaleIntercept) || DecimalRescaleSlope != 1.0M || DecimalRescaleIntercept != 0.0M)
				dataset[DicomTags.RescaleIntercept].SetString(0, RescaleIntercept);

			if (dataset.Contains(DicomTags.WindowCenter) || LinearVoiLuts.Count > 0)
				Window.SetWindowCenterAndWidth(dataset, LinearVoiLuts);

			//Remove the palette color lut, if the pixels were translated to RGB
			if (dataset.Contains(DicomTags.RedPaletteColorLookupTableData)
			    && dataset.Contains(DicomTags.BluePaletteColorLookupTableData)
			    && dataset.Contains(DicomTags.GreenPaletteColorLookupTableData)
			    && !HasPaletteColorLut)
			{
				dataset.RemoveElement(DicomTags.BluePaletteColorLookupTableDescriptor);
				dataset.RemoveElement(DicomTags.BluePaletteColorLookupTableData);
				dataset.RemoveElement(DicomTags.RedPaletteColorLookupTableDescriptor);
				dataset.RemoveElement(DicomTags.RedPaletteColorLookupTableData);
				dataset.RemoveElement(DicomTags.GreenPaletteColorLookupTableDescriptor);
				dataset.RemoveElement(DicomTags.GreenPaletteColorLookupTableData);
			}

			dataset.SaveDicomFields(this);
			dataset[DicomTags.PixelData] = _sq;
		}

		/// <summary>
		/// Update a <see cref="DicomMessageBase"/> with pixel data related tags.
		/// </summary>
		/// <param name="message">The message to update.</param>
		public override void UpdateMessage(DicomMessageBase message)
		{
			UpdateAttributeCollection(message.DataSet);
			DicomFile file = message as DicomFile;
			if (file != null)
				file.TransferSyntax = TransferSyntax;
		}

		/// <summary>
		/// Get a specific frame's data in uncompressed format.
		/// </summary>
		/// <remarks>
		/// <para>
		/// If a DICOM file is loaded with the <see cref="DicomReadOptions.StorePixelDataReferences"/>
		/// option set, this method will only load the specific frame's data from the source file to
		/// do the decompress, thus reducing memory usage to only the frame being decompressed.
		/// </para>
		/// </remarks>
		/// <param name="frame">A zero offset frame to request.</param>
		/// <param name="photometricInterpretation">The photometric interpretation of the output data</param>
		/// <returns>A byte array containing the frame.</returns>
		public override byte[] GetFrame(int frame, out string photometricInterpretation)
		{
			DicomUncompressedPixelData pd = new DicomUncompressedPixelData(this);

			IDicomCodec codec = DicomCodecRegistry.GetCodec(TransferSyntax);
			if (codec == null)
			{
                LogAdapter.Logger.ErrorWithFormat("Unable to get registered codec for {0}", TransferSyntax);

				throw new DicomCodecException("No registered codec for: " + TransferSyntax.Name);
			}

			DicomCodecParameters parameters = DicomCodecRegistry.GetCodecParameters(TransferSyntax, null);

			codec.DecodeFrame(frame, this, pd, parameters);

			pd.TransferSyntax = TransferSyntax.ExplicitVrLittleEndian;

			photometricInterpretation = pd.PhotometricInterpretation;

			return pd.GetData();
		}

		/// <summary>
		/// Append a compressed pixel data fragment's worth of data.  It is assumed an entire frame is
		/// contained within <paramref name="data"/>.
		/// </summary>
		/// <param name="data">The fragment data.</param>
		public void AddFrameFragment(byte[] data)
		{
			DicomFragmentSequence sequence = _sq;
			if ((data.Length%2) == 1)
				throw new DicomCodecException("Fragment being appended is incorrectly an odd length: " + data.Length);

			uint offset = 0;
			foreach (DicomFragment fragment in sequence.Fragments)
			{
				offset += (8 + fragment.Length);
			}
			sequence.OffsetTable.Add(offset);

			ByteBuffer buffer = new ByteBuffer();
			buffer.Append(data, 0, data.Length);
			sequence.Fragments.Add(new DicomFragment(buffer));
		}

		public void AddFrameFragment(ByteBuffer bb)
		{
			DicomFragmentSequence sequence = _sq;
			if ((bb.Length%2) == 1)
				throw new DicomCodecException("Fragment being appended is incorrectly an odd length: " + bb.Length);

			uint offset = 0;
			foreach (DicomFragment fragment in sequence.Fragments)
			{
				offset += (8 + fragment.Length);
			}
			sequence.OffsetTable.Add(offset);

			sequence.Fragments.Add(new DicomFragment(bb));
		}

		public uint GetCompressedFrameSize(int frame)
		{
			List<DicomFragment> list = GetFrameFragments(frame);
			uint length = 0;
			foreach (DicomFragment frag in list)
				length += frag.Length;
			return length;
		}

		public byte[] GetFrameFragmentData(int frame)
		{
			List<DicomFragment> list = GetFrameFragments(frame);
			uint length = 0;
			foreach (DicomFragment frag in list)
				length += frag.Length;

			byte[] data = new byte[length];
			uint offset = 0;
			foreach (DicomFragment frag in list)
			{
				Array.Copy(frag.GetByteArray(), 0, data, (int) offset, (int) frag.Length);
				offset += frag.Length;
			}
			return data;
		}

		/// <summary>
		/// Get the pixel data fragments for a frame.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Note that if an offset table was not included within the pixel data, this method will
		/// attempt to guess which fragments are contained within a frame.
		/// </para>
		/// </remarks>
		/// <param name="frame">The zero offset frame to get the fragments for.</param>
		/// <returns>A list of fragments associated with the frame.</returns>
		public List<DicomFragment> GetFrameFragments(int frame)
		{
			if (frame < 0 || frame >= NumberOfFrames)
				throw new IndexOutOfRangeException("Requested frame out of range!");

			List<DicomFragment> fragments = new List<DicomFragment>();
			DicomFragmentSequence sequence = _sq;
			int fragmentCount = sequence.Fragments.Count;

			if (NumberOfFrames == 1)
			{
				foreach (DicomFragment frag in sequence.Fragments)
					fragments.Add(frag);
				return fragments;
			}

			if (fragmentCount == NumberOfFrames)
			{
				fragments.Add(sequence.Fragments[frame]);
				return fragments;
			}

			if (sequence.HasOffsetTable && sequence.OffsetTable.Count == NumberOfFrames)
			{
				uint offset = sequence.OffsetTable[frame];
				uint stop = 0xffffffff;
				uint pos = 0;

				if ((frame + 1) < NumberOfFrames)
				{
					stop = sequence.OffsetTable[frame + 1];
				}

				int i = 0;
				while (pos < offset && i < fragmentCount)
				{
					pos += (8 + sequence.Fragments[i].Length);
					i++;
				}

				// check for invalid offset table
				if (pos != offset)
					goto GUESS_FRAME_OFFSET;

				while (offset < stop && i < fragmentCount)
				{
					fragments.Add(sequence.Fragments[i]);
					offset += (8 + sequence.Fragments[i].Length);
					i++;
				}

				return fragments;
			}

			GUESS_FRAME_OFFSET:
			if (sequence.Fragments.Count > 0)
			{
				uint fragmentSize = sequence.Fragments[0].Length;

				bool allSameLength = true;
				for (int i = 0; i < fragmentCount; i++)
				{
					if (sequence.Fragments[i].Length != fragmentSize)
					{
						allSameLength = false;
						break;
					}
				}

				if (allSameLength)
				{
					if ((fragmentCount%NumberOfFrames) != 0)
						throw new DicomDataException("Unable to determine frame length from pixel data sequence!");

					int count = fragmentCount/NumberOfFrames;
					int start = frame*count;

					for (int i = 0; i < fragmentCount; i++)
					{
						fragments.Add(sequence.Fragments[start + i]);
					}

					return fragments;
				}
				else
				{
					// what if a single frame ends on a fragment boundary?

					int count = 0;
					int start = 0;

					for (int i = 0; i < fragmentCount && count < frame; i++, start++)
					{
						if (sequence.Fragments[i].Length != fragmentSize)
							count++;
					}

					for (int i = start; i < fragmentCount; i++)
					{
						fragments.Add(sequence.Fragments[i]);
						if (sequence.Fragments[i].Length != fragmentSize)
							break;
					}

					return fragments;
				}
			}

			return fragments;
		}

		#endregion
	}
}
