/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: MultiframeOverlayModule.cs
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

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// C.9.3 Multi-frame Overlay Module, PS 3.3 - 2008
	/// </summary>
	public class MultiframeOverlayModule : IodBase
	{
		#region Constructors
        /// <summary>
		/// Initializes a new instance of the <see cref="MultiframeOverlayModule"/> class.
        /// </summary>
        public MultiframeOverlayModule()
        {
        }

        /// <summary>
		/// Initializes a new instance of the <see cref="MultiframeOverlayModule"/> class.
        /// </summary>
		public MultiframeOverlayModule(IDicomElementProvider dicomElementProvider)
			: base(dicomElementProvider)
        {
        }
        #endregion

		/// <summary>
		/// Number of Frames in Overlay. Required if Overlay data contains multiple frames.
		/// </summary>
		/// <remarks>
		/// <para>
		/// A Multi-frame Overlay is defined as an Overlay whose overlay data consists of a sequential set of
		/// individual Overlay frames. A Multi-frame Overlay is transmitted as a single contiguous stream of
		/// overlay data. Frame delimiters are not contained within the data stream.
		/// </para>
		/// <para>
		///Each individual frame shall be defined (and thus can be identified) by the Attributes in the Overlay
		///Plane Module (see C.9.2).
		/// </para>
		/// <para>
		///The total number of frames contained within a Multi-frame Overlay is conveyed in the Number of
		///Frames in Overlay (60xx,0015).
		/// </para>
		/// <para>
		///The frames within a Multi-frame Overlay shall be conveyed as a logical sequence. If Multi-frame
		///Overlays are related to a Multi-frame Image, the order of the Overlay Frames are one to one with
		///the order of the Image frames. Otherwise, no attribute is used to indicate the sequencing of the
		///Overlay Frames. If Image Frame Origin (60xx,0051) is present, the Overlay frames are applied
		///one to one to the Image frames, beginning at the indicated frame number. Otherwise, no attribute
		///is used to indicated the sequencing of the Overlay Frames.
		/// </para>
		/// <para>
		///The Number of Frames in Overlay (60xx,0015) plus the Image Frame Origin (60xx,0051) minus 1
		///shall be less than or equal to the total number of frames in the Multi-frame Image.
		/// </para>
		/// <para>
		///If the Overlay data are embedded in the pixel data, then the Image Frame Origin (60xx,0051)
		///must be 1 and the Number of Frames in Overlay (60xx,0015) must equal the number of frames in
		///the Multi-frame Image.
		/// </para>
		/// </remarks>
		public ushort NumberOfFramesInOverlay
		{
			get { return DicomElementProvider[DicomTags.NumberOfFramesInOverlay].GetUInt16(0, 0); }
			set { DicomElementProvider[DicomTags.NumberOfFramesInOverlay].SetUInt16(0, value); }
		}

		/// <summary>
		/// Frame number of Multi-frame Image to which this overlay applies; frames are numbered from 1.
		/// </summary>
		public ushort ImageFrameOrigin
		{
			get { return DicomElementProvider[DicomTags.ImageFrameOrigin].GetUInt16(0, 0); }
			set { DicomElementProvider[DicomTags.ImageFrameOrigin].SetUInt16(0, value); }
		}
	}
}
