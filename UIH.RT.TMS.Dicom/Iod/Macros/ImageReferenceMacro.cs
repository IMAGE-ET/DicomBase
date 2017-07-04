/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ImageReferenceMacro.cs
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
using UIH.RT.TMS.Dicom.Iod.Macros.ImageReference;

namespace UIH.RT.TMS.Dicom.Iod.Macros
{
	/// <summary>
	/// ImageReference Macro
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.18.4 (Table C.18.4-1)</remarks>
	public interface IImageReferenceMacro : ICompositeObjectReferenceMacro
	{
		/// <summary>
		/// Gets or sets the value of ReferencedSopSequence in the underlying collection. Type 1.
		/// </summary>
		new IReferencedSopSequence ReferencedSopSequence { get; set; }

		/// <summary>
		/// Creates the value of ReferencedSopSequence in the underlying collection. Type 1.
		/// </summary>
		new IReferencedSopSequence CreateReferencedSopSequence();
	}

	namespace ImageReference
	{
		/// <summary>
		/// ReferencedSop Sequence of the ImageReference Macro
		/// </summary>
		/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.18.4 (Table C.18.4-1)</remarks>
		public interface IReferencedSopSequence : ISopInstanceReferenceMacro
		{
			/// <summary>
			/// Gets or sets the value of ReferencedFrameNumber in the underlying collection. Type 1C.
			/// </summary>
			string ReferencedFrameNumber { get; set; }

			/// <summary>
			/// Gets or sets the value of ReferencedSegmentNumber in the underlying collection. Type 1C.
			/// </summary>
			string ReferencedSegmentNumber { get; set; }

			/// <summary>
			/// Gets or sets the value of ReferencedSopSequence in the underlying collection. Type 3.
			/// </summary>
			ISopInstanceReferenceMacro ReferencedSopSequence { get; set; }

			/// <summary>
			/// Creates the value of ReferencedSopSequence in the underlying collection. Type 3.
			/// </summary>
			ISopInstanceReferenceMacro CreateReferencedSopSequence();

			/// <summary>
			/// Gets or sets the value of ReferencedRealWorldValueMappingInstanceSequence in the underlying collection. Type 3.
			/// </summary>
			ISopInstanceReferenceMacro ReferencedRealWorldValueMappingInstanceSequence { get; set; }

			/// <summary>
			/// Creates the value of ReferencedRealWorldValueMappingInstanceSequence in the underlying collection. Type 3.
			/// </summary>
			ISopInstanceReferenceMacro CreateReferencedRealWorldValueMappingInstanceSequence();

			/// <summary>
			/// Gets or sets the value of ImageIconSequence in the underlying collection. Type 3.
			/// </summary>
			object ImageIconSequence { get; set; }
		}
	}

	/// <summary>
	/// ImageReference Macro Base Implementation
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.18.4 (Table C.18.4-1)</remarks>
	internal class ImageReferenceMacro : SequenceIodBase, IImageReferenceMacro
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ImageReferenceMacro"/> class.
		/// </summary>
		public ImageReferenceMacro() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="ImageReferenceMacro"/> class.
		/// </summary>
		/// <param name="dicomSequenceItem">The dicom sequence item.</param>
		public ImageReferenceMacro(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) {}

		/// <summary>
		/// Initializes the underlying collection to implement the module or sequence using default values.
		/// </summary>
		public void InitializeAttributes()
		{
			this.CreateReferencedSopSequence();
			this.ReferencedSopSequence.InitializeAttributes();
		}

		/// <summary>
		/// Gets or sets the value of ReferencedSopSequence in the underlying collection. Type 1.
		/// </summary>
		ISopInstanceReferenceMacro ICompositeObjectReferenceMacro.ReferencedSopSequence
		{
			get { return this.ReferencedSopSequence; }
			set { this.ReferencedSopSequence = (IReferencedSopSequence) value; }
		}

		/// <summary>
		/// Gets or sets the value of ReferencedSopSequence in the underlying collection. Type 1.
		/// </summary>
		public IReferencedSopSequence ReferencedSopSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.ReferencedSopSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}
				return new ReferencedSopSequenceType(((DicomSequenceItem[]) dicomElement.Values)[0]);
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value", "ReferencedSopSequence is Type 1 Required.");
				base.DicomElementProvider[DicomTags.ReferencedSopSequence].Values = new DicomSequenceItem[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Creates the value of ReferencedSopSequence in the underlying collection. Type 1.
		/// </summary>
		ISopInstanceReferenceMacro ICompositeObjectReferenceMacro.CreateReferencedSopSequence()
		{
			return this.CreateReferencedSopSequence();
		}

		/// <summary>
		/// Creates the value of ReferencedSopSequence in the underlying collection. Type 1.
		/// </summary>
		public IReferencedSopSequence CreateReferencedSopSequence()
		{
			DicomElement dicomElement = base.DicomElementProvider[DicomTags.ReferencedSopSequence];
			if (dicomElement.IsNull || dicomElement.Count == 0)
			{
				DicomSequenceItem dicomSequenceItem = new DicomSequenceItem();
				dicomElement.Values = new DicomSequenceItem[] {dicomSequenceItem};
				ReferencedSopSequenceType iodBase = new ReferencedSopSequenceType(dicomSequenceItem);
				iodBase.InitializeAttributes();
				return iodBase;
			}
			return new ReferencedSopSequenceType(((DicomSequenceItem[]) dicomElement.Values)[0]);
		}

		/// <summary>
		/// ReferencedSop Sequence of the ImageReference Macro Base Implementation
		/// </summary>
		/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.18.4 (Table C.18.4-1)</remarks>
		internal class ReferencedSopSequenceType : SopInstanceReferenceMacro, IReferencedSopSequence
		{
			/// <summary>
			/// Initializes a new instance of the <see cref="ReferencedSopSequenceType"/> class.
			/// </summary>
			public ReferencedSopSequenceType() : base() {}

			/// <summary>
			/// Initializes a new instance of the <see cref="ReferencedSopSequenceType"/> class.
			/// </summary>
			/// <param name="dicomSequenceItem">The dicom sequence item.</param>
			public ReferencedSopSequenceType(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) {}

			/// <summary>
			/// Initializes the underlying collection to implement the module using default values.
			/// </summary>
			public override void InitializeAttributes()
			{
				base.InitializeAttributes();
				this.ReferencedFrameNumber = null;
				this.ReferencedSegmentNumber = null;
				this.ReferencedSopSequence = null;
				this.ReferencedRealWorldValueMappingInstanceSequence = null;
				this.ImageIconSequence = null;
			}

			/// <summary>
			/// Gets or sets the value of ReferencedFrameNumber in the underlying collection. Type 1C.
			/// </summary>
			public string ReferencedFrameNumber
			{
				get { return base.DicomElementProvider[DicomTags.ReferencedFrameNumber].ToString(); }
				set
				{
					if (string.IsNullOrEmpty(value))
						base.DicomElementProvider[DicomTags.ReferencedFrameNumber] = null;
					base.DicomElementProvider[DicomTags.ReferencedFrameNumber].SetStringValue(value);
				}
			}

			/// <summary>
			/// Gets or sets the value of ReferencedSegmentNumber in the underlying collection. Type 1C.
			/// </summary>
			public string ReferencedSegmentNumber
			{
				get { return base.DicomElementProvider[DicomTags.ReferencedSegmentNumber].ToString(); }
				set
				{
					if (string.IsNullOrEmpty(value))
						base.DicomElementProvider[DicomTags.ReferencedSegmentNumber] = null;
					base.DicomElementProvider[DicomTags.ReferencedSegmentNumber].SetStringValue(value);
				}
			}

			/// <summary>
			/// Gets or sets the value of ReferencedSopSequence in the underlying collection. Type 3.
			/// </summary>
			public ISopInstanceReferenceMacro ReferencedSopSequence
			{
				get
				{
					DicomElement dicomElement = base.DicomElementProvider[DicomTags.ReferencedSopSequence];
					if (dicomElement.IsNull || dicomElement.Count == 0)
					{
						return null;
					}
					return new SopInstanceReferenceMacro(((DicomSequenceItem[]) dicomElement.Values)[0]);
				}
				set
				{
					if (value == null)
					{
						base.DicomElementProvider[DicomTags.ReferencedSopSequence] = null;
						return;
					}
					base.DicomElementProvider[DicomTags.ReferencedSopSequence].Values = new DicomSequenceItem[] {value.DicomSequenceItem};
				}
			}

			/// <summary>
			/// Creates the value of ReferencedSopSequence in the underlying collection. Type 3.
			/// </summary>
			public ISopInstanceReferenceMacro CreateReferencedSopSequence()
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.ReferencedSopSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					DicomSequenceItem dicomSequenceItem = new DicomSequenceItem();
					dicomElement.Values = new DicomSequenceItem[] {dicomSequenceItem};
					SopInstanceReferenceMacro iodBase = new SopInstanceReferenceMacro(dicomSequenceItem);
					iodBase.InitializeAttributes();
					return iodBase;
				}
				return new SopInstanceReferenceMacro(((DicomSequenceItem[]) dicomElement.Values)[0]);
			}

			/// <summary>
			/// Gets or sets the value of ReferencedRealWorldValueMappingInstanceSequence in the underlying collection. Type 3.
			/// </summary>
			public ISopInstanceReferenceMacro ReferencedRealWorldValueMappingInstanceSequence
			{
				get
				{
					DicomElement dicomElement = base.DicomElementProvider[DicomTags.ReferencedRealWorldValueMappingInstanceSequence];
					if (dicomElement.IsNull || dicomElement.Count == 0)
					{
						return null;
					}
					return new SopInstanceReferenceMacro(((DicomSequenceItem[]) dicomElement.Values)[0]);
				}
				set
				{
					if (value == null)
					{
						base.DicomElementProvider[DicomTags.ReferencedRealWorldValueMappingInstanceSequence] = null;
						return;
					}
					base.DicomElementProvider[DicomTags.ReferencedRealWorldValueMappingInstanceSequence].Values = new DicomSequenceItem[] {value.DicomSequenceItem};
				}
			}

			/// <summary>
			/// Creates the value of ReferencedRealWorldValueMappingInstanceSequence in the underlying collection. Type 3.
			/// </summary>
			public ISopInstanceReferenceMacro CreateReferencedRealWorldValueMappingInstanceSequence()
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.ReferencedRealWorldValueMappingInstanceSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					DicomSequenceItem dicomSequenceItem = new DicomSequenceItem();
					dicomElement.Values = new DicomSequenceItem[] {dicomSequenceItem};
					SopInstanceReferenceMacro iodBase = new SopInstanceReferenceMacro(dicomSequenceItem);
					iodBase.InitializeAttributes();
					return iodBase;
				}
				return new SopInstanceReferenceMacro(((DicomSequenceItem[]) dicomElement.Values)[0]);
			}

			/// <summary>
			/// NOT IMPLEMENTED. Gets or sets the value of ImageIconSequence in the underlying collection. Type 3.
			/// </summary>
			public object ImageIconSequence
			{
				// TODO - Implement this.
				get { throw new NotImplementedException(); }
				set { 
					//throw new NotImplementedException(); 
				}
			}
		}
	}
}
