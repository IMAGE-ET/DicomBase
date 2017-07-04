/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: DicomMessageBase.cs
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
using System.Text;
using UIH.RT.Framework.Utility;
using UIH.RT.TMS.Dicom.Codec;
using UIH.RT.TMS.Common;
using UIH.RT.TMS.Dicom.Iod.Modules;
using UIH.RT.TMS.Dicom;

namespace UIH.RT.TMS.Dicom
{
    /// <summary>
    /// Base class for DICOM Files and Messages
    /// </summary>
    /// <seealso cref="DicomFile"/>
    /// <seealso cref="DicomMessage"/>
    public abstract class DicomMessageBase
    {
        /// <summary>
        /// The Transfer Syntax of the DICOM file or message
        /// </summary>
        public abstract TransferSyntax TransferSyntax { get; set; }
    
        /// <summary>
        /// The Meta information for the message.
        /// </summary>
        public DicomDataset MetaInfo
        {
            get;
            internal set;
        }

        /// <summary>
        /// The DataSet for the message.
        /// </summary>
        public DicomDataset DataSet
        {
            get; internal set;
        }

        public void ChangeTransferSyntax(TransferSyntax newTransferSyntax)
        {
            ChangeTransferSyntax(newTransferSyntax, null, null);
        }

        public void ChangeTransferSyntax(TransferSyntax newTransferSyntax, IDicomCodec inputCodec, DicomCodecParameters inputParameters)
        {
            IDicomCodec codec = inputCodec;
            DicomCodecParameters parameters = inputParameters;
            if (newTransferSyntax.Encapsulated && TransferSyntax.Encapsulated)
                throw new DicomCodecException("Source and destination transfer syntaxes encapsulated");

            if (newTransferSyntax.Encapsulated)
            {
                if (codec == null)
                {
                    codec = DicomCodecRegistry.GetCodec(newTransferSyntax);
                    if (codec == null)
                    {
                        LogAdapter.Logger.ErrorWithFormat("Unable to get registered codec for {0}", newTransferSyntax);
                        throw new DicomCodecException("No registered codec for: " + newTransferSyntax.Name);
                    }
                }
                if (parameters == null)
                    parameters = DicomCodecRegistry.GetCodecParameters(newTransferSyntax, DataSet);

            	DicomElement pixelData;
                if (DataSet.TryGetAttribute(DicomTags.PixelData, out pixelData))
                {
                    if (pixelData.IsNull)
                        throw new DicomCodecException("Sop pixel data has no valid value and cannot be compressed.");

                	new OverlayPlaneModuleIod(DataSet).ExtractEmbeddedOverlays();

					var pd = new DicomUncompressedPixelData(DataSet);
					using (var pixelStream = ((DicomElementBinary) pixelData).AsStream())
					{
						//Before compression, make the pixel data more "typical", so it's harder to mess up the codecs.
						//NOTE: Could combine mask and align into one method so we're not iterating twice, but I prefer having the methods separate.
						if (DicomUncompressedPixelData.RightAlign(pixelStream, pd.BitsAllocated, pd.BitsStored, pd.HighBit))
						{
							var newHighBit = (ushort) (pd.HighBit - pd.LowBit);
							LogAdapter.Logger.WarnWithFormat("Right aligned pixel data (High Bit: {0}->{1}).", pd.HighBit, newHighBit);

							pd.HighBit = newHighBit; //correct high bit after right-aligning.
							DataSet[DicomTags.HighBit].SetUInt16(0, newHighBit);
						}
						if (DicomUncompressedPixelData.ZeroUnusedBits(pixelStream, pd.BitsAllocated, pd.BitsStored, pd.HighBit))
						{
							LogAdapter.Logger.Warning("Zeroed some unused bits before compression.");
						}
					}

                	// Set transfer syntax before compression, the codecs need it.
					var fragments = new DicomCompressedPixelData(pd) { TransferSyntax = newTransferSyntax };
                	codec.Encode(pd, fragments, parameters);
                    fragments.UpdateMessage(this);

                    //TODO: should we validate the number of frames in the compressed data?
                    if (!DataSet.TryGetAttribute(DicomTags.PixelData, out pixelData) || pixelData.IsNull)
                        throw new DicomCodecException("Sop has no pixel data after compression.");
                }
                else
                {
                    //A bit cheap, but check for basic image attributes - if any exist
                    // and are non-empty, there should probably be pixel data too.

                    DicomElement element;
                    if (DataSet.TryGetAttribute(DicomTags.Rows, out element) && !element.IsNull)
                        throw new DicomCodecException("Suspect Sop appears to be an image (Rows is non-empty), but has no pixel data.");

                    if (DataSet.TryGetAttribute(DicomTags.Columns, out element) && !element.IsNull)
                        throw new DicomCodecException("Suspect Sop appears to be an image (Columns is non-empty), but has no pixel data.");

                    TransferSyntax = newTransferSyntax;
                }
            }
            else
            {
                if (codec == null)
                {
                    codec = DicomCodecRegistry.GetCodec(TransferSyntax);
                    if (codec == null)
                    {
                        LogAdapter.Logger.ErrorWithFormat("Unable to get registered codec for {0}", TransferSyntax);

                        throw new DicomCodecException("No registered codec for: " + TransferSyntax.Name);
                    }

                    if (parameters == null)
                        parameters = DicomCodecRegistry.GetCodecParameters(TransferSyntax, DataSet);
                }

				DicomElement pixelData;
				if (DataSet.TryGetAttribute(DicomTags.PixelData, out pixelData))
				{
					if (pixelData.IsNull)
						throw new DicomCodecException("Sop pixel data has no valid value and cannot be decompressed.");

					var fragments = new DicomCompressedPixelData(DataSet);
                    var pd = new DicomUncompressedPixelData(fragments);

                    codec.Decode(fragments, pd, parameters);

                    pd.TransferSyntax = TransferSyntax.ExplicitVrLittleEndian;
                    TransferSyntax = TransferSyntax.ExplicitVrLittleEndian;

                    pd.UpdateMessage(this);

					//TODO: should we validate the number of frames in the decompressed data?
					if (!DataSet.TryGetAttribute(DicomTags.PixelData, out pixelData) || pixelData.IsNull)
						throw new DicomCodecException("Sop has no pixel data after decompression.");
				}
                else
                {
					//NOTE: doing this for consistency, really.
					DicomElement element;
					if (DataSet.TryGetAttribute(DicomTags.Rows, out element) && !element.IsNull)
						throw new DicomCodecException("Suspect Sop appears to be an image (Rows is non-empty), but has no pixel data.");

					if (DataSet.TryGetAttribute(DicomTags.Columns, out element) && !element.IsNull)
						throw new DicomCodecException("Suspect Sop appears to be an image (Columns is non-empty), but has no pixel data.");
					
					TransferSyntax = TransferSyntax.ExplicitVrLittleEndian;
                }
            }
        }

        /// <summary>
        /// Load the contents of attributes in the message into a structure or class.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method will use reflection to look at the contents of the object specified by
        /// <paramref name="obj"/> and copy the values of attributes within the <see cref="MetaInfo"/>
        /// and <see cref="DataSet"/> for the message to fields in the object with 
        /// the <see cref="DicomFieldAttribute"/> element set for them.
        /// </para>
        /// </remarks>
        /// <param name="obj"></param>
        public void LoadDicomFields(object obj)
        {
            MetaInfo.LoadDicomFields(obj);
            DataSet.LoadDicomFields(obj);
        }

        #region Dump
        /// <summary>
        /// Dump the contents of the message to a StringBuilder.
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="prefix"></param>
        /// <param name="options"></param>
        public abstract void Dump(StringBuilder sb, string prefix, DicomDumpOptions options);

        /// <summary>
        /// Dump the contents of the message to a string.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="options"></param>
        /// <returns>The dump of the message.</returns>
        public string Dump(string prefix, DicomDumpOptions options)
        {
            var sb = new StringBuilder();
            Dump(sb, prefix, options);
            return sb.ToString();
        }

        /// <summary>
        /// Dump the contents of themessage to a string with the default dump options.
        /// </summary>
        /// <param name="prefix">A prefix to place in front of each dump line.</param>
        /// <returns>The dump of the message.</returns>
        public string Dump(string prefix)
        {
            return Dump(prefix, DicomDumpOptions.Default);
        }

        /// <summary>
        /// Dump the contents of the message to a string with the default options set.
        /// </summary>
        /// <returns>The dump of the message.</returns>
        public string Dump()
        {
            return Dump(String.Empty, DicomDumpOptions.Default);
        }
        #endregion

		/// <summary>
		/// Hash override that sums the hashes of the attributes within the message.
		/// </summary>
		/// <returns>The sum of the hashes of the attributes in the message.</returns>
		public override int GetHashCode()
		{
			if (MetaInfo.Count > 0 || DataSet.Count > 0)
			{
				int hash = 0;
				foreach (DicomElement attrib in MetaInfo)
					hash += attrib.GetHashCode();
				foreach (DicomElement attrib in DataSet)
					hash += attrib.GetHashCode();
				return hash;
			}
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
		    var failureReasons = new List<DicomElementComparisonResult>();
		    return Equals(obj, ref failureReasons);
		}

        /// <summary>
		/// Check if the contents of the DicomDataset is identical to another DicomDataset instance.
		/// </summary>
		/// <remarks>
		/// <para>
		/// This method compares the contents of two element collections to see if they are equal.  The method
		/// will step through each of the tags within the collection, and compare them to see if they are equal.  The
		/// method will also recurse into sequence attributes to be sure they are equal.</para>
		/// </remarks>
		/// <param name="obj">The object to compare to.</param>
        /// <param name="comparisonResults">A list of <see cref="DicomElementComparisonResult"/>  describing why the objects are not equal.</param>
		/// <returns>true if the collections are equal.</returns>
		public bool Equals(object obj, ref List<DicomElementComparisonResult> comparisonResults)
		{
			var a = obj as DicomFile;
			if (a == null)
			{
			    var result = new DicomElementComparisonResult
			                     {
			                         ResultType = ComparisonResultType.InvalidType,
			                         Details =
			                             String.Format("Comparison object is invalid type: {0}",
			                                           obj.GetType())
			                     };
			    comparisonResults.Add(result);

				return false;
			}

            if (!MetaInfo.Equals(a.MetaInfo, ref comparisonResults))
				return false;
            if (!DataSet.Equals(a.DataSet, ref comparisonResults))
				return false;

			return true;
		}
	}
}
