/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ImageSopInstanceReferenceDictionary.cs
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

using System.Collections.Generic;
using UIH.RT.TMS.Common;
using UIH.RT.TMS.Dicom.Iod.Macros;

namespace UIH.RT.TMS.Dicom.Iod
{
	public class ImageSopInstanceReferenceDictionary
	{
		private readonly Dictionary<string, IList<int>> _frameDictionary = new Dictionary<string, IList<int>>();
		private readonly Dictionary<string, IList<uint>> _segmentDictionary = new Dictionary<string, IList<uint>>();
		private readonly bool _emptyDictionaryMatchesAll;

		public ImageSopInstanceReferenceDictionary(IEnumerable<ImageSopInstanceReferenceMacro> imageSopReferences) : this(imageSopReferences ?? new ImageSopInstanceReferenceMacro[0], false) {}

		public ImageSopInstanceReferenceDictionary(IEnumerable<ImageSopInstanceReferenceMacro> imageSopReferences, bool emptyDictionaryMatchesAll)
		{
			Platform.CheckForNullReference(imageSopReferences, "imageSopReferences");

			_emptyDictionaryMatchesAll = emptyDictionaryMatchesAll;

			foreach (ImageSopInstanceReferenceMacro imageSopReference in imageSopReferences)
			{
				DicomElementIs frames = imageSopReference.ReferencedFrameNumber;
				List<int> frameList = null;
				if (!frames.IsNull && !frames.IsEmpty && frames.Count > 0)
				{
					frameList = new List<int>();
					for (int n = 0; n < frames.Count; n++)
						frameList.Add(frames.GetInt32(n, -1));
				}
				_frameDictionary.Add(imageSopReference.ReferencedSopInstanceUid, frameList);

				DicomElementUs segments = imageSopReference.ReferencedSegmentNumber;
				List<uint> segmentList = null;
				if (!segments.IsNull && !segments.IsEmpty && segments.Count > 0)
				{
					segmentList = new List<uint>();
					for (int n = 0; n < segments.Count; n++)
						segmentList.Add(segments.GetUInt32(n, 0));
				}
				_segmentDictionary.Add(imageSopReference.ReferencedSopInstanceUid, segmentList);
			}
		}

		public bool IsEmpty
		{
			get { return _frameDictionary.Count == 0; }
		}

		public bool ReferencesSop(string imageSopInstanceUid)
		{
			return ReferencesAny(imageSopInstanceUid);
		}

		public bool ReferencesAny(string imageSopInstanceUid)
		{
			if (_emptyDictionaryMatchesAll && this.IsEmpty)
				return true; // return true if dictionary is empty and empty matches all

			if (_frameDictionary.ContainsKey(imageSopInstanceUid))
				return true;
			return false;
		}

		public bool ReferencesAllFrames(string imageSopInstanceUid)
		{
			if (_emptyDictionaryMatchesAll && this.IsEmpty)
				return true; // return true if dictionary is empty and empty matches all

			if (_frameDictionary.ContainsKey(imageSopInstanceUid))
			{
				IList<int> frames = _frameDictionary[imageSopInstanceUid];
				if (frames == null)
					return true;
			}
			return false;
		}

		public bool ReferencesAllSegments(string imageSopInstanceUid) 
		{
			if (_emptyDictionaryMatchesAll && this.IsEmpty)
				return true; // return true if dictionary is empty and empty matches all

			if (_segmentDictionary.ContainsKey(imageSopInstanceUid))
			{
				IList<uint> segments = _segmentDictionary[imageSopInstanceUid];
				if (segments == null)
					return true;
			}
			return false;
		}

		public bool ReferencesFrame(string imageSopInstanceUid, int frameNumber) 
		{
			if (_emptyDictionaryMatchesAll && this.IsEmpty)
				return true; // return true if dictionary is empty and empty matches all

			if (_frameDictionary.ContainsKey(imageSopInstanceUid))
			{
				IList<int> frames = _frameDictionary[imageSopInstanceUid];
				if (frames == null || frames.Contains(frameNumber))
					return true;
			}
			return false;
		}

		public bool ReferencesSegment(string imageSopInstanceUid, uint segmentNumber) 
		{
			if (_emptyDictionaryMatchesAll && this.IsEmpty)
				return true; // return true if dictionary is empty and empty matches all

			if (_segmentDictionary.ContainsKey(imageSopInstanceUid))
			{
				IList<uint> segments = _segmentDictionary[imageSopInstanceUid];
				if (segments == null || segments.Contains(segmentNumber))
					return true;
			}
			return false;
		}
	}
}
