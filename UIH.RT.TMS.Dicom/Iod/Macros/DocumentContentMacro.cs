/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: DocumentContentMacro.cs
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
using UIH.RT.TMS.Dicom.Iod.Sequences;

namespace UIH.RT.TMS.Dicom.Iod.Macros
{
	public interface IDocumentContentMacro : IIodMacro, IImageReferenceMacro, IContainerMacro
	{
		/// <summary>
		/// Gets or sets the value of ValueType in the underlying collection. Type 1.
		/// </summary>
		ValueType ValueType { get; set; }

		/// <summary>
		/// Gets or sets the value of ConceptNameCodeSequence in the underlying collection. Type 1C.
		/// </summary>
		CodeSequenceMacro ConceptNameCodeSequence { get; set; }

		/// <summary>
		/// Gets or sets the value of TextValue in the underlying collection. Type 1C.
		/// </summary>
		string TextValue { get; set; }

		/// <summary>
		/// Gets or sets the value of DateTime in the underlying collection. Type 1C.
		/// </summary>
		DateTime? DateTime { get; set; }

		/// <summary>
		/// Gets or sets the value of Date in the underlying collection. Type 1C.
		/// </summary>
		DateTime? Date { get; set; }

		/// <summary>
		/// Gets or sets the value of Time in the underlying collection. Type 1C.
		/// </summary>
		DateTime? Time { get; set; }

		/// <summary>
		/// Gets or sets the value of PersonName in the underlying collection. Type 1C.
		/// </summary>
		string PersonName { get; set; }

		/// <summary>
		/// Gets or sets the value of Uid in the underlying collection. Type 1C.
		/// </summary>
		string Uid { get; set; }

		/// <summary>
		/// Initializes the underlying collection to implement the module or sequence with a value type of IMAGE using default values.
		/// </summary>
		IImageReferenceMacro InitializeImageReferenceAttributes();

		/// <summary>
		/// Initializes the underlying collection to implement the module or sequence with a value type of CONTAINER using default values.
		/// </summary>
		IContainerMacro InitializeContainerAttributes();
	}

	/// <summary>
	/// DocumentContent Macro Base Implementation
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.17.3 (Table C.17-5)</remarks>
	internal class DocumentContentMacro : SequenceIodBase, IDocumentContentMacro
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentContentMacro"/> class.
		/// </summary>
		public DocumentContentMacro() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentContentMacro"/> class.
		/// </summary>
		/// <param name="dicomSequenceItem">The dicom sequence item.</param>
		public DocumentContentMacro(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) {}

		/// <summary>
		/// Initializes the underlying collection to implement the module or sequence using default values.
		/// </summary>
		public virtual void InitializeAttributes()
		{
			this.ConceptNameCodeSequence = null;
			this.TextValue = null;
			this.DateTime = null;
			this.Date = null;
			this.Time = null;
			this.PersonName = null;
			this.Uid = null;
		}

		#region Regular DocumentContentMacro Members

		/// <summary>
		/// Gets or sets the value of ValueType in the underlying collection. Type 1.
		/// </summary>
		public virtual ValueType ValueType
		{
			get { return ParseEnum(base.DicomElementProvider[DicomTags.ValueType].GetString(0, string.Empty), ValueType.None); }
			set
			{
				if (value == ValueType.None)
					throw new ArgumentNullException("value", "ValueType is a required Type 1.");
				SetAttributeFromEnum(base.DicomElementProvider[DicomTags.ValueType], value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ConceptNameCodeSequence in the underlying collection. Type 1C.
		/// </summary>
		public CodeSequenceMacro ConceptNameCodeSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.ConceptNameCodeSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}
				return new CodeSequenceMacro(((DicomSequenceItem[]) dicomElement.Values)[0]);
			}
			set
			{
				if (value == null)
				{
					base.DicomElementProvider[DicomTags.ConceptNameCodeSequence] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.ConceptNameCodeSequence].Values = new DicomSequenceItem[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Gets or sets the value of TextValue in the underlying collection. Type 1C.
		/// </summary>
		public string TextValue
		{
			get { return base.DicomElementProvider[DicomTags.TextValue].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.TextValue] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.TextValue].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of DateTime in the underlying collection. Type 1C.
		/// </summary>
		public DateTime? DateTime
		{
			get { return base.DicomElementProvider[DicomTags.Datetime].GetDateTime(0); }
			set
			{
				if (!value.HasValue)
				{
					base.DicomElementProvider[DicomTags.Datetime] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.Datetime].SetDateTime(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of Date in the underlying collection. Type 1C.
		/// </summary>
		public DateTime? Date
		{
			get { return base.DicomElementProvider[DicomTags.Date].GetDateTime(0); }
			set
			{
				if (!value.HasValue)
				{
					base.DicomElementProvider[DicomTags.Date] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.Date].SetDateTime(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of Time in the underlying collection. Type 1C.
		/// </summary>
		public DateTime? Time
		{
			get { return base.DicomElementProvider[DicomTags.Time].GetDateTime(0); }
			set
			{
				if (!value.HasValue)
				{
					base.DicomElementProvider[DicomTags.Time] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.Time].SetDateTime(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of PersonName in the underlying collection. Type 1C.
		/// </summary>
		public string PersonName
		{
			get { return base.DicomElementProvider[DicomTags.PersonName].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.PersonName] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.PersonName].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of Uid in the underlying collection. Type 1C.
		/// </summary>
		public string Uid
		{
			get { return base.DicomElementProvider[DicomTags.Uid].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.Uid] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.Uid].SetString(0, value);
			}
		}

		#endregion

		#region IImageReferenceMacro Members

		/// <summary>
		/// Initializes the underlying collection to implement the module or sequence with a value type of IMAGE using default values.
		/// </summary>
		public IImageReferenceMacro InitializeImageReferenceAttributes()
		{
			this.InitializeAttributes();
			this.ValueType = ValueType.Image;
			((IImageReferenceMacro) this).CreateReferencedSopSequence();
			((IImageReferenceMacro) this).ReferencedSopSequence.InitializeAttributes();
			return this;
		}

		/// <summary>
		/// Gets or sets the value of ReferencedSopSequence in the underlying collection. Type 1.
		/// </summary>
		ISopInstanceReferenceMacro ICompositeObjectReferenceMacro.ReferencedSopSequence
		{
			get { return ((IImageReferenceMacro) this).ReferencedSopSequence; }
			set { ((IImageReferenceMacro) this).ReferencedSopSequence = (IReferencedSopSequence) value; }
		}

		/// <summary>
		/// Gets or sets the value of ReferencedSopSequence in the underlying collection. Type 1.
		/// </summary>
		IReferencedSopSequence IImageReferenceMacro.ReferencedSopSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.ReferencedSopSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}
				return new ImageReferenceMacro.ReferencedSopSequenceType(((DicomSequenceItem[]) dicomElement.Values)[0]);
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
			return ((IImageReferenceMacro) this).CreateReferencedSopSequence();
		}

		/// <summary>
		/// Creates the value of ReferencedSopSequence in the underlying collection. Type 1.
		/// </summary>
		IReferencedSopSequence IImageReferenceMacro.CreateReferencedSopSequence()
		{
			DicomElement dicomElement = base.DicomElementProvider[DicomTags.ReferencedSopSequence];
			if (dicomElement.IsNull || dicomElement.Count == 0)
			{
				DicomSequenceItem dicomSequenceItem = new DicomSequenceItem();
				dicomElement.Values = new DicomSequenceItem[] {dicomSequenceItem};
				ImageReferenceMacro.ReferencedSopSequenceType iodBase = new ImageReferenceMacro.ReferencedSopSequenceType(dicomSequenceItem);
				iodBase.InitializeAttributes();
				return iodBase;
			}
			return new ImageReferenceMacro.ReferencedSopSequenceType(((DicomSequenceItem[]) dicomElement.Values)[0]);
		}

		#endregion

		#region IContainerMacro Members

		/// <summary>
		/// Initializes the underlying collection to implement the module or sequence with a value type of CONTAINER using default values.
		/// </summary>
		public IContainerMacro InitializeContainerAttributes()
		{
			this.InitializeAttributes();
			this.ValueType = ValueType.Container;
			((IContainerMacro) this).ContinuityOfContent = ContinuityOfContent.Separate;
			((IContainerMacro) this).ContentTemplateSequence = null;
			return this;
		}

		/// <summary>
		/// Gets or sets the value of ContinuityOfContent in the underlying collection. Type 1.
		/// </summary>
		ContinuityOfContent IContainerMacro.ContinuityOfContent
		{
			get { return ParseEnum(base.DicomElementProvider[DicomTags.ContinuityOfContent].GetString(0, string.Empty), ContinuityOfContent.Unknown); }
			set
			{
				if (value == ContinuityOfContent.Unknown)
					throw new ArgumentNullException("value", "Continuity of Content is Type 1 Required.");
				SetAttributeFromEnum(base.DicomElementProvider[DicomTags.ContinuityOfContent], value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ContentTemplateSequence in the underlying collection. Type 1C.
		/// </summary>
		ContentTemplateSequence IContainerMacro.ContentTemplateSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.ContentTemplateSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}
				return new ContentTemplateSequence(((DicomSequenceItem[]) dicomElement.Values)[0]);
			}
			set
			{
				if (value == null)
				{
					base.DicomElementProvider[DicomTags.ContentTemplateSequence] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.ContentTemplateSequence].Values = new DicomSequenceItem[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Creates the value of ContentTemplateSequence in the underlying collection. Type 1C.
		/// </summary>
		ContentTemplateSequence IContainerMacro.CreateContentTemplateSequence()
		{
			DicomElement dicomElement = base.DicomElementProvider[DicomTags.ContentTemplateSequence];
			if (dicomElement.IsNull || dicomElement.Count == 0)
			{
				DicomSequenceItem dicomSequenceItem = new DicomSequenceItem();
				dicomElement.Values = new DicomSequenceItem[] {dicomSequenceItem};
				ContentTemplateSequence iodBase = new ContentTemplateSequence(dicomSequenceItem);
				iodBase.InitializeAttributes();
				return iodBase;
			}
			return new ContentTemplateSequence(((DicomSequenceItem[]) dicomElement.Values)[0]);
		}

		#endregion
	}
}
