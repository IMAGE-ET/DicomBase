/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ViewCodeSequenceIod.cs
////
//// Summary:
////
////
//// Date: 2014/08/19
//////////////////////////////////////////////////////////////////////////
#region License

// Copyright (c) 2011 - 2013, United-Imaging Inc.
// All rights reserved.
// http://www.united-imaging.com

#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace UIH.RT.TMS.Dicom.Iod.Sequences
{

	public class ViewCodeSequenceIod : SequenceIodBase
	{
		public ViewCodeSequenceIod()
		{
		}

		public ViewCodeSequenceIod(DicomSequenceItem sequenceItem)
			: base(sequenceItem)
		{
		}

		public string CodeValue
		{
			get { return base.DicomSequenceItem[DicomTags.CodeValue].GetString(0, ""); }
			set { base.DicomSequenceItem[DicomTags.CodeValue].SetString(0, value); }
		}

		public string CodingSchemeDesignator
		{
			get { return base.DicomSequenceItem[DicomTags.CodingSchemeDesignator].GetString(0, ""); }
			set { base.DicomSequenceItem[DicomTags.CodingSchemeDesignator].SetString(0, value); }
		}

		public string CodingSchemeVersion
		{
			get { return base.DicomSequenceItem[DicomTags.CodingSchemeVersion].GetString(0, ""); }
			set { base.DicomSequenceItem[DicomTags.CodingSchemeVersion].SetString(0, value); }
		}

		public string CodeMeaning
		{
			get { return base.DicomSequenceItem[DicomTags.CodeMeaning].GetString(0, ""); }
			set { base.DicomSequenceItem[DicomTags.CodeMeaning].SetString(0, value); }
		}

		public string ContextIdentifier
		{
			get { return base.DicomSequenceItem[DicomTags.ContextIdentifier].GetString(0, ""); }
			set { base.DicomSequenceItem[DicomTags.ContextIdentifier].SetString(0, value); }
		}

		public string MappingResource
		{
			get { return base.DicomSequenceItem[DicomTags.MappingResource].GetString(0, ""); }
			set { base.DicomSequenceItem[DicomTags.MappingResource].SetString(0, value); }
		}

		public DateTime? ContextGroupVersion
		{
			get { return base.DicomSequenceItem[DicomTags.ContextGroupVersion].GetDateTime(0); }
			set { base.DicomSequenceItem[DicomTags.ContextGroupVersion].SetDateTime(0, value); }
		}

		public string ContextGroupExtensionFlag
		{
			get { return base.DicomSequenceItem[DicomTags.ContextGroupExtensionFlag].GetString(0, ""); }
			set { base.DicomSequenceItem[DicomTags.ContextGroupExtensionFlag].SetString(0, value); }
		}

		public DateTime? ContextGroupLocalVersion
		{
			get { return base.DicomSequenceItem[DicomTags.ContextGroupLocalVersion].GetDateTime(0); }
			set { base.DicomSequenceItem[DicomTags.ContextGroupLocalVersion].SetDateTime(0, value); }
		}

		public string ContextGroupExtensionCreatorUid
		{
			get { return base.DicomSequenceItem[DicomTags.ContextGroupExtensionCreatorUid].GetString(0, ""); }
			set { base.DicomSequenceItem[DicomTags.ContextGroupExtensionCreatorUid].SetString(0, value); }
		}
	}
}
