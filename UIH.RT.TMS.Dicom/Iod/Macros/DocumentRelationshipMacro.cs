/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: DocumentRelationshipMacro.cs
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
using UIH.RT.TMS.Dicom.Iod.Macros.DocumentRelationship;
using UIH.RT.TMS.Dicom.Iod.Macros.ImageReference;
using UIH.RT.TMS.Dicom.Iod.Sequences;

namespace UIH.RT.TMS.Dicom.Iod.Macros
{
	/// <summary> 
	/// DocumentRelationship Macro
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.17.3 (Table C.17-6)</remarks>
	public interface IDocumentRelationshipMacro : IIodMacro
	{
		/// <summary>
		/// Gets or sets the value of ObservationDateTime in the underlying collection. Type 1C.
		/// </summary>
		DateTime? ObservationDateTime { get; set; }

		/// <summary>
		/// Gets or sets the value of ContentSequence in the underlying collection. Type 1C.
		/// </summary>
		IContentSequence[] ContentSequence { get; set; }

		/// <summary>
		/// Creates a single instance of a ContentSequence item. Does not modify the ContentSequence in the underlying collection.
		/// </summary>
		IContentSequence CreateContentSequence();
	}

	/// <summary> 
	/// DocumentRelationship Macro Base Implementation
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.17.3 (Table C.17-6)</remarks>
	internal class DocumentRelationshipMacro : SequenceIodBase, IDocumentRelationshipMacro
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentRelationshipMacro"/> class.
		/// </summary>
		public DocumentRelationshipMacro() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentRelationshipMacro"/> class.
		/// </summary>
		/// <param name="dicomSequenceItem">The dicom sequence item.</param>
		public DocumentRelationshipMacro(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) {}

		/// <summary>
		/// Initializes the underlying collection to implement the module or sequence using default values.
		/// </summary>
		public void InitializeAttributes()
		{
			this.ObservationDateTime = null;
			this.ContentSequence = null;
		}

		/// <summary>
		/// Gets or sets the value of ObservationDateTime in the underlying collection. Type 1C.
		/// </summary>
		public DateTime? ObservationDateTime
		{
			get { return base.DicomElementProvider[DicomTags.ObservationDateTime].GetDateTime(0); }
			set
			{
				if (!value.HasValue)
				{
					base.DicomElementProvider[DicomTags.ObservationDateTime] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.ObservationDateTime].SetDateTime(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ContentSequence in the underlying collection. Type 1C.
		/// </summary>
		public IContentSequence[] ContentSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.ContentSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}

				IContentSequence[] result = new IContentSequence[dicomElement.Count];
				DicomSequenceItem[] items = (DicomSequenceItem[]) dicomElement.Values;
				for (int n = 0; n < items.Length; n++)
					result[n] = new ContentSequenceType(items[n]);

				return result;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.DicomElementProvider[DicomTags.ContentSequence] = null;
					return;
				}

				DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
				for (int n = 0; n < value.Length; n++)
					result[n] = value[n].DicomSequenceItem;

				base.DicomElementProvider[DicomTags.ContentSequence].Values = result;
			}
		}

		/// <summary>
		/// Creates a single instance of a ContentSequence item. Does not modify the ContentSequence in the underlying collection.
		/// </summary>
		public IContentSequence CreateContentSequence()
		{
			ContentSequenceType iodBase = new ContentSequenceType(new DicomSequenceItem());
			iodBase.InitializeAttributes();
			return iodBase;
		}
	}

	namespace DocumentRelationship
	{
		public interface IContentSequence : IIodMacro, IDocumentContentMacro, IDocumentRelationshipMacro
		{
			/// <summary>
			/// Gets or sets the value of RelationshipTyp in the underlying collection. Type 1.
			/// </summary>
			RelationshipType RelationshipType { get; set; }

			/// <summary>
			/// Gets or sets the value of ReferencedContentItemIdentifier in the underlying collection. Type 1C.
			/// </summary>
			uint[] ReferencedContentItemIdentifier { get; set; }
		}

		internal class ContentSequenceType : SequenceIodBase, IContentSequence
		{
			public ContentSequenceType() : base() {}
			public ContentSequenceType(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) {}

			/// <summary>
			/// Initializes the underlying collection to implement the module or sequence using default values.
			/// </summary>
			public void InitializeAttributes()
			{
				this.RelationshipType = RelationshipType.Contains;
				this.ReferencedContentItemIdentifier = null;
				this.ConceptNameCodeSequence = null;
				this.TextValue = null;
				this.DateTime = null;
				this.Date = null;
				this.Time = null;
				this.PersonName = null;
				this.Uid = null;
				this.ObservationDateTime = null;
				this.ContentSequence = null;
			}

			/// <summary>
			/// Gets or sets the value of RelationshipTyp in the underlying collection. Type 1.
			/// </summary>
			public RelationshipType RelationshipType
			{
				get { return ParseEnum(base.DicomElementProvider[DicomTags.RelationshipType].GetString(0, string.Empty), RelationshipType.Unknown); }
				set
				{
					if (value == RelationshipType.Unknown)
						throw new ArgumentOutOfRangeException("value", "RelationshipType is Type 1 Required.");
					SetAttributeFromEnum(base.DicomElementProvider[DicomTags.RelationshipType], value, true);
				}
			}

			/// <summary>
			/// Gets or sets the value of ReferencedContentItemIdentifier in the underlying collection. Type 1C.
			/// </summary>
			public uint[] ReferencedContentItemIdentifier
			{
				get { return (uint[])base.DicomElementProvider[DicomTags.ReferencedContentItemIdentifier].Values; }
				set
				{
					if (value == null || value.Length == 0)
					{
						base.DicomElementProvider[DicomTags.ReferencedContentItemIdentifier] = null;
						return;
					}
					base.DicomElementProvider[DicomTags.ReferencedContentItemIdentifier].Values = value;
				}
			}

			#region IDocumentContentMacro Members

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
			public IImageReferenceMacro InitializeImageReferenceAttributes() {
				this.InitializeAttributes();
				this.ValueType = ValueType.Image;
				((IImageReferenceMacro)this).CreateReferencedSopSequence();
				((IImageReferenceMacro)this).ReferencedSopSequence.InitializeAttributes();
				return this;
			}

			/// <summary>
			/// Gets or sets the value of ReferencedSopSequence in the underlying collection. Type 1.
			/// </summary>
			ISopInstanceReferenceMacro ICompositeObjectReferenceMacro.ReferencedSopSequence {
				get { return ((IImageReferenceMacro)this).ReferencedSopSequence; }
				set { ((IImageReferenceMacro)this).ReferencedSopSequence = (IReferencedSopSequence)value; }
			}

			/// <summary>
			/// Gets or sets the value of ReferencedSopSequence in the underlying collection. Type 1.
			/// </summary>
			IReferencedSopSequence IImageReferenceMacro.ReferencedSopSequence {
				get {
					DicomElement dicomElement = base.DicomElementProvider[DicomTags.ReferencedSopSequence];
					if (dicomElement.IsNull || dicomElement.Count == 0) {
						return null;
					}
					return new ImageReferenceMacro.ReferencedSopSequenceType(((DicomSequenceItem[])dicomElement.Values)[0]);
				}
				set {
					if (value == null)
						throw new ArgumentNullException("value", "ReferencedSopSequence is Type 1 Required.");
					base.DicomElementProvider[DicomTags.ReferencedSopSequence].Values = new DicomSequenceItem[] { value.DicomSequenceItem };
				}
			}

			/// <summary>
			/// Creates the value of ReferencedSopSequence in the underlying collection. Type 1.
			/// </summary>
			ISopInstanceReferenceMacro ICompositeObjectReferenceMacro.CreateReferencedSopSequence() {
				return ((IImageReferenceMacro)this).CreateReferencedSopSequence();
			}

			/// <summary>
			/// Creates the value of ReferencedSopSequence in the underlying collection. Type 1.
			/// </summary>
			IReferencedSopSequence IImageReferenceMacro.CreateReferencedSopSequence() {
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.ReferencedSopSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0) {
					DicomSequenceItem dicomSequenceItem = new DicomSequenceItem();
					dicomElement.Values = new DicomSequenceItem[] { dicomSequenceItem };
					ImageReferenceMacro.ReferencedSopSequenceType iodBase = new ImageReferenceMacro.ReferencedSopSequenceType(dicomSequenceItem);
					iodBase.InitializeAttributes();
					return iodBase;
				}
				return new ImageReferenceMacro.ReferencedSopSequenceType(((DicomSequenceItem[])dicomElement.Values)[0]);
			}

			#endregion

			#region IContainerMacro Members

			/// <summary>
			/// Initializes the underlying collection to implement the module or sequence with a value type of CONTAINER using default values.
			/// </summary>
			public IContainerMacro InitializeContainerAttributes() {
				this.InitializeAttributes();
				this.ValueType = ValueType.Container;
				((IContainerMacro)this).ContinuityOfContent = ContinuityOfContent.Separate;
				((IContainerMacro)this).ContentTemplateSequence = null;
				return this;
			}

			/// <summary>
			/// Gets or sets the value of ContinuityOfContent in the underlying collection. Type 1.
			/// </summary>
			ContinuityOfContent IContainerMacro.ContinuityOfContent {
				get { return ParseEnum(base.DicomElementProvider[DicomTags.ContinuityOfContent].GetString(0, string.Empty), ContinuityOfContent.Unknown); }
				set {
					if (value == ContinuityOfContent.Unknown)
						throw new ArgumentNullException("value", "Continuity of Content is Type 1 Required.");
					SetAttributeFromEnum(base.DicomElementProvider[DicomTags.ContinuityOfContent], value);
				}
			}

			/// <summary>
			/// Gets or sets the value of ContentTemplateSequence in the underlying collection. Type 1C.
			/// </summary>
			ContentTemplateSequence IContainerMacro.ContentTemplateSequence {
				get {
					DicomElement dicomElement = base.DicomElementProvider[DicomTags.ContentTemplateSequence];
					if (dicomElement.IsNull || dicomElement.Count == 0) {
						return null;
					}
					return new ContentTemplateSequence(((DicomSequenceItem[])dicomElement.Values)[0]);
				}
				set {
					if (value == null) {
						base.DicomElementProvider[DicomTags.ContentTemplateSequence] = null;
						return;
					}
					base.DicomElementProvider[DicomTags.ContentTemplateSequence].Values = new DicomSequenceItem[] { value.DicomSequenceItem };
				}
			}

			/// <summary>
			/// Creates the value of ContentTemplateSequence in the underlying collection. Type 1C.
			/// </summary>
			ContentTemplateSequence IContainerMacro.CreateContentTemplateSequence() {
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.ContentTemplateSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0) {
					DicomSequenceItem dicomSequenceItem = new DicomSequenceItem();
					dicomElement.Values = new DicomSequenceItem[] { dicomSequenceItem };
					ContentTemplateSequence iodBase = new ContentTemplateSequence(dicomSequenceItem);
					iodBase.InitializeAttributes();
					return iodBase;
				}
				return new ContentTemplateSequence(((DicomSequenceItem[])dicomElement.Values)[0]);
			}

			#endregion

			#region IDocumentRelationshipMacro Members

			/// <summary>
			/// Gets or sets the value of ObservationDateTime in the underlying collection. Type 1C.
			/// </summary>
			public DateTime? ObservationDateTime
			{
				get { return base.DicomElementProvider[DicomTags.ObservationDateTime].GetDateTime(0); }
				set
				{
					if (!value.HasValue)
					{
						base.DicomElementProvider[DicomTags.ObservationDateTime] = null;
						return;
					}
					base.DicomElementProvider[DicomTags.ObservationDateTime].SetDateTime(0, value);
				}
			}

			/// <summary>
			/// Gets or sets the value of ContentSequence in the underlying collection. Type 1C.
			/// </summary>
			public IContentSequence[] ContentSequence
			{
				get
				{
					DicomElement dicomElement = base.DicomElementProvider[DicomTags.ContentSequence];
					if (dicomElement.IsNull || dicomElement.Count == 0)
					{
						return null;
					}

					IContentSequence[] result = new IContentSequence[dicomElement.Count];
					DicomSequenceItem[] items = (DicomSequenceItem[]) dicomElement.Values;
					for (int n = 0; n < items.Length; n++)
						result[n] = new ContentSequenceType(items[n]);

					return result;
				}
				set
				{
					if (value == null || value.Length == 0)
					{
						base.DicomElementProvider[DicomTags.ContentSequence] = null;
						return;
					}

					DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
					for (int n = 0; n < value.Length; n++)
						result[n] = value[n].DicomSequenceItem;

					base.DicomElementProvider[DicomTags.ContentSequence].Values = result;
				}
			}

			/// <summary>
			/// Creates a single instance of a ContentSequence item. Does not modify the ContentSequence in the underlying collection.
			/// </summary>
			public IContentSequence CreateContentSequence()
			{
				ContentSequenceType iodBase = new ContentSequenceType(new DicomSequenceItem());
				iodBase.InitializeAttributes();
				return iodBase;
			}

			#endregion
		}

		public enum RelationshipType
		{
			Contains,
			HasProperties,
			HasObsContext,
			HasAcqContext,
			InferredFrom,
			SelectedFrom,
			HasConceptMod,
			Unknown
		}
	}
}
