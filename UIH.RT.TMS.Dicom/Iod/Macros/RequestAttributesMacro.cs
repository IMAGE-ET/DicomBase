/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: RequestAttributesMacro.cs
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

using UIH.RT.TMS.Dicom.Iod.Macros.RequestAttributes;
using UIH.RT.TMS.Dicom.Iod.Macros.RequestAttributes.ScheduledProtocolCodeSequence;

namespace UIH.RT.TMS.Dicom.Iod.Macros
{
	/// <summary>
	/// RequestAttributes Macro
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section 10.6 (Table 10-9)</remarks>
	public interface IRequestAttributesMacro : IIodMacro
	{
		/// <summary>
		/// Gets or sets the value of RequestedProcedureId in the underlying collection. Type 1C.
		/// </summary>
		string RequestedProcedureId { get; set; }

		/// <summary>
		/// Gets or sets the value of AccessionNumber in the underlying collection. Type 3.
		/// </summary>
		string AccessionNumber { get; set; }

		/// <summary>
		/// Gets or sets the value of StudyInstanceUid in the underlying collection. Type 3.
		/// </summary>
		string StudyInstanceUid { get; set; }

		/// <summary>
		/// Gets or sets the value of ReferencedStudySequence in the underlying collection. Type 3.
		/// </summary>
		ISopInstanceReferenceMacro[] ReferencedStudySequence { get; set; }

		/// <summary>
		/// Gets or sets the value of RequestedProcedureDescription in the underlying collection. Type 3.
		/// </summary>
		string RequestedProcedureDescription { get; set; }

		/// <summary>
		/// Gets or sets the value of RequestedProcedureCodeSequence in the underlying collection. Type 3.
		/// </summary>
		CodeSequenceMacro RequestedProcedureCodeSequence { get; set; }

		/// <summary>
		/// Gets or sets the value of ReasonForTheRequestedProcedure in the underlying collection. Type 3.
		/// </summary>
		string ReasonForTheRequestedProcedure { get; set; }

		/// <summary>
		/// Gets or sets the value of ReasonForRequestedProcedureCodeSequence in the underlying collection. Type 3.
		/// </summary>
		CodeSequenceMacro ReasonForRequestedProcedureCodeSequence { get; set; }

		/// <summary>
		/// Gets or sets the value of ScheduledProcedureStepId in the underlying collection. Type 1C.
		/// </summary>
		string ScheduledProcedureStepId { get; set; }

		/// <summary>
		/// Gets or sets the value of ScheduledProcedureStepDescription in the underlying collection. Type 3.
		/// </summary>
		string ScheduledProcedureStepDescription { get; set; }

		/// <summary>
		/// Gets or sets the value of ScheduledProtocolCodeSequence in the underlying collection. Type 3.
		/// </summary>
		IScheduledProtocolCodeSequence[] ScheduledProtocolCodeSequence { get; set; }

		/// <summary>
		/// Creates a single instance of a ReferencedStudySequence item. Does not modify the ReferencedStudySequence in the underlying collection.
		/// </summary>
		ISopInstanceReferenceMacro CreateReferencedStudySequence();

		/// <summary>
		/// Creates a single instance of a ScheduledProtocolCodeSequence item. Does not modify the tag in the underlying collection.
		/// </summary>
		IScheduledProtocolCodeSequence CreateScheduledProtocolCodeSequence();
	}

	/// <summary>
	/// RequestAttributes Macro Base Implementation
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section 10.6 (Table 10-9)</remarks>
	internal class RequestAttributesMacro : SequenceIodBase, IRequestAttributesMacro
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RequestAttributesMacro"/> class.
		/// </summary>
		public RequestAttributesMacro() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="RequestAttributesMacro"/> class.
		/// </summary>
		/// <param name="dicomSequenceItem">The dicom sequence item.</param>
		public RequestAttributesMacro(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) {}

		/// <summary>
		/// Initializes the underlying collection to implement the module or sequence using default values.
		/// </summary>
		public void InitializeAttributes() {}

		/// <summary>
		/// Gets or sets the value of RequestedProcedureId in the underlying collection. Type 1C.
		/// </summary>
		public string RequestedProcedureId
		{
			get { return base.DicomElementProvider[DicomTags.RequestedProcedureId].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.RequestedProcedureId] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.RequestedProcedureId].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of AccessionNumber in the underlying collection. Type 3.
		/// </summary>
		public string AccessionNumber
		{
			get { return base.DicomElementProvider[DicomTags.AccessionNumber].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.AccessionNumber] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.AccessionNumber].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of StudyInstanceUid in the underlying collection. Type 3.
		/// </summary>
		public string StudyInstanceUid
		{
			get { return base.DicomElementProvider[DicomTags.StudyInstanceUid].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.StudyInstanceUid] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.StudyInstanceUid].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ReferencedStudySequence in the underlying collection. Type 3.
		/// </summary>
		public ISopInstanceReferenceMacro[] ReferencedStudySequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.ReferencedStudySequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}

				ISopInstanceReferenceMacro[] result = new ISopInstanceReferenceMacro[dicomElement.Count];
				DicomSequenceItem[] items = (DicomSequenceItem[]) dicomElement.Values;
				for (int n = 0; n < items.Length; n++)
					result[n] = new SopInstanceReferenceMacro(items[n]);

				return result;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.DicomElementProvider[DicomTags.ReferencedStudySequence] = null;
					return;
				}

				DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
				for (int n = 0; n < value.Length; n++)
					result[n] = value[n].DicomSequenceItem;

				base.DicomElementProvider[DicomTags.ReferencedStudySequence].Values = result;
			}
		}

		/// <summary>
		/// Creates a single instance of a ReferencedStudySequence item. Does not modify the ReferencedStudySequence in the underlying collection.
		/// </summary>
		public ISopInstanceReferenceMacro CreateReferencedStudySequence()
		{
			ISopInstanceReferenceMacro iodBase = new SopInstanceReferenceMacro(new DicomSequenceItem());
			iodBase.InitializeAttributes();
			return iodBase;
		}

		/// <summary>
		/// Gets or sets the value of RequestedProcedureDescription in the underlying collection. Type 3.
		/// </summary>
		public string RequestedProcedureDescription
		{
			get { return base.DicomElementProvider[DicomTags.RequestedProcedureDescription].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.RequestedProcedureDescription] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.RequestedProcedureDescription].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of RequestedProcedureCodeSequence in the underlying collection. Type 3.
		/// </summary>
		public CodeSequenceMacro RequestedProcedureCodeSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.RequestedProcedureCodeSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}
				return new CodeSequenceMacro(((DicomSequenceItem[]) dicomElement.Values)[0]);
			}
			set
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.RequestedProcedureCodeSequence];
				if (value == null)
				{
					base.DicomElementProvider[DicomTags.RequestedProcedureCodeSequence] = null;
					return;
				}
				dicomElement.Values = new DicomSequenceItem[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Gets or sets the value of ReasonForTheRequestedProcedure in the underlying collection. Type 3.
		/// </summary>
		public string ReasonForTheRequestedProcedure
		{
			get { return base.DicomElementProvider[DicomTags.ReasonForTheRequestedProcedure].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.ReasonForTheRequestedProcedure] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.ReasonForTheRequestedProcedure].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ReasonForRequestedProcedureCodeSequence in the underlying collection. Type 3.
		/// </summary>
		public CodeSequenceMacro ReasonForRequestedProcedureCodeSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.ReasonForRequestedProcedureCodeSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}
				return new CodeSequenceMacro(((DicomSequenceItem[]) dicomElement.Values)[0]);
			}
			set
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.ReasonForRequestedProcedureCodeSequence];
				if (value == null)
				{
					base.DicomElementProvider[DicomTags.ReasonForRequestedProcedureCodeSequence] = null;
					return;
				}
				dicomElement.Values = new DicomSequenceItem[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Gets or sets the value of ScheduledProcedureStepId in the underlying collection. Type 1C.
		/// </summary>
		public string ScheduledProcedureStepId
		{
			get { return base.DicomElementProvider[DicomTags.ScheduledProcedureStepId].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.ScheduledProcedureStepId] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.ScheduledProcedureStepId].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ScheduledProcedureStepDescription in the underlying collection. Type 3.
		/// </summary>
		public string ScheduledProcedureStepDescription
		{
			get { return base.DicomElementProvider[DicomTags.ScheduledProcedureStepDescription].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.ScheduledProcedureStepDescription] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.ScheduledProcedureStepDescription].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ScheduledProtocolCodeSequence in the underlying collection. Type 3.
		/// </summary>
		public IScheduledProtocolCodeSequence[] ScheduledProtocolCodeSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.ScheduledProtocolCodeSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}

				IScheduledProtocolCodeSequence[] result = new IScheduledProtocolCodeSequence[dicomElement.Count];
				DicomSequenceItem[] items = (DicomSequenceItem[]) dicomElement.Values;
				for (int n = 0; n < items.Length; n++)
					result[n] = new ScheduledProtocolCodeSequenceClass(items[n]);

				return result;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.DicomElementProvider[DicomTags.ScheduledProtocolCodeSequence] = null;
					return;
				}

				DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
				for (int n = 0; n < value.Length; n++)
					result[n] = value[n].DicomSequenceItem;

				base.DicomElementProvider[DicomTags.ScheduledProtocolCodeSequence].Values = result;
			}
		}

		/// <summary>
		/// Creates a single instance of a ScheduledProtocolCodeSequence item. Does not modify the tag in the underlying collection.
		/// </summary>
		public IScheduledProtocolCodeSequence CreateScheduledProtocolCodeSequence()
		{
			IScheduledProtocolCodeSequence iodBase = new ScheduledProtocolCodeSequenceClass(new DicomSequenceItem());
			iodBase.InitializeAttributes();
			return iodBase;
		}

		/// <summary>
		/// ScheduledProtocol Code Sequence
		/// </summary>
		/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section 10.6 (Table 10-9)</remarks>
		internal class ScheduledProtocolCodeSequenceClass : CodeSequenceMacro, IScheduledProtocolCodeSequence
		{
			/// <summary>
			/// Initializes a new instance of the <see cref="ScheduledProtocolCodeSequenceClass"/> class.
			/// </summary>
			public ScheduledProtocolCodeSequenceClass() : base() {}

			/// <summary>
			/// Initializes a new instance of the <see cref="ScheduledProtocolCodeSequenceClass"/> class.
			/// </summary>
			/// <param name="dicomSequenceItem">The dicom sequence item.</param>
			public ScheduledProtocolCodeSequenceClass(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) {}

			/// <summary>
			/// Initializes the underlying collection to implement the module or sequence using default values.
			/// </summary>
			public virtual void InitializeAttributes() {}

			/// <summary>
			/// Gets or sets the value of ProtocolContextSequence in the underlying collection. Type 3.
			/// </summary>
			public IProtocolContextSequence[] ProtocolContextSequence
			{
				get
				{
					DicomElement dicomElement = base.DicomElementProvider[DicomTags.ProtocolContextSequence];
					if (dicomElement.IsNull || dicomElement.Count == 0)
					{
						return null;
					}

					ProtocolContextSequenceClass[] result = new ProtocolContextSequenceClass[dicomElement.Count];
					DicomSequenceItem[] items = (DicomSequenceItem[]) dicomElement.Values;
					for (int n = 0; n < items.Length; n++)
						result[n] = new ProtocolContextSequenceClass(items[n]);

					return result;
				}
				set
				{
					if (value == null || value.Length == 0)
					{
						base.DicomElementProvider[DicomTags.ProtocolContextSequence] = null;
						return;
					}

					DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
					for (int n = 0; n < value.Length; n++)
						result[n] = value[n].DicomSequenceItem;

					base.DicomElementProvider[DicomTags.ProtocolContextSequence].Values = result;
				}
			}

			/// <summary>
			/// Creates a single instance of a ProtocolContextSequence item. Does not modify the tag in the underlying collection.
			/// </summary>
			public IProtocolContextSequence CreateProtocolContextSequence()
			{
				IProtocolContextSequence iodBase = new ProtocolContextSequenceClass(new DicomSequenceItem());
				iodBase.InitializeAttributes();
				return iodBase;
			}

			/// <summary>
			/// ProtocolContext Sequence
			/// </summary>
			/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section 10.6 (Table 10-9)</remarks>
			internal class ProtocolContextSequenceClass : ContentItemMacro, IProtocolContextSequence
			{
				/// <summary>
				/// Initializes a new instance of the <see cref="ProtocolContextSequenceClass"/> class.
				/// </summary>
				public ProtocolContextSequenceClass() : base() {}

				/// <summary>
				/// Initializes a new instance of the <see cref="ProtocolContextSequenceClass"/> class.
				/// </summary>
				/// <param name="dicomSequenceItem">The dicom sequence item.</param>
				public ProtocolContextSequenceClass(DicomSequenceItem dicomSequenceItem) : base(dicomSequenceItem) {}

				/// <summary>
				/// Initializes the underlying collection to implement the module or sequence using default values.
				/// </summary>
				public virtual void InitializeAttributes() {}

				/// <summary>
				/// Gets or sets the value of ContentItemModifierSequence in the underlying collection. Type 3.
				/// </summary>
				public ContentItemMacro[] ContentItemModifierSequence
				{
					get
					{
						DicomElement dicomElement = base.DicomElementProvider[DicomTags.ContentItemModifierSequence];
						if (dicomElement.IsNull || dicomElement.Count == 0)
						{
							return null;
						}

						ContentItemMacro[] result = new ContentItemMacro[dicomElement.Count];
						DicomSequenceItem[] items = (DicomSequenceItem[]) dicomElement.Values;
						for (int n = 0; n < items.Length; n++)
							result[n] = new ContentItemMacro(items[n]);

						return result;
					}
					set
					{
						if (value == null || value.Length == 0)
						{
							base.DicomElementProvider[DicomTags.ContentItemModifierSequence] = null;
							return;
						}

						DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
						for (int n = 0; n < value.Length; n++)
							result[n] = value[n].DicomSequenceItem;

						base.DicomElementProvider[DicomTags.ContentItemModifierSequence].Values = result;
					}
				}
			}
		}
	}

	namespace RequestAttributes {
		/// <summary>
		/// ScheduledProtocol Code Sequence
		/// </summary>
		/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section 10.6 (Table 10-9)</remarks>
		public interface IScheduledProtocolCodeSequence : IIodMacro {
			/// <summary>
			/// Gets or sets the value of ProtocolContextSequence in the underlying collection. Type 3.
			/// </summary>
			IProtocolContextSequence[] ProtocolContextSequence { get; set; }

			/// <summary>
			/// Creates a single instance of a ProtocolContextSequence item. Does not modify the tag in the underlying collection.
			/// </summary>
			IProtocolContextSequence CreateProtocolContextSequence();
		}

		namespace ScheduledProtocolCodeSequence {
			/// <summary>
			/// ProtocolContext Sequence
			/// </summary>
			/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section 10.6 (Table 10-9)</remarks>
			public interface IProtocolContextSequence : IIodMacro {
				/// <summary>
				/// Gets or sets the value of ContentItemModifierSequence in the underlying collection. Type 3.
				/// </summary>
				ContentItemMacro[] ContentItemModifierSequence { get; set; }
			}
		}
	}
}
