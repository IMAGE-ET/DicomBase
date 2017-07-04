/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: CodingSchemeIdentificationSequenceIod.cs
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

namespace UIH.RT.TMS.Dicom.Iod.Sequences
{
	/// <summary>
	/// CodingSchemeIdentification Sequence Item
	/// </summary>
	/// <remarks>
	/// <para>As defined in the DICOM Standard 2009, Part 3, Section C.12.1 (Table C.12-1)</para>
	/// </remarks>
	public class CodingSchemeIdentificationSequenceItem
		: SequenceIodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CodingSchemeIdentificationSequence"/> class.
		/// </summary>
		public CodingSchemeIdentificationSequenceItem() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodingSchemeIdentificationSequence"/> class.
		/// </summary>
		/// <param name="dicomSequenceItem">The DICOM sequence item.</param>
		public CodingSchemeIdentificationSequenceItem(DicomSequenceItem dicomSequenceItem)
			: base(dicomSequenceItem) {}

		/// <summary>
		/// Gets or sets the value of CodingSchemeDesignator in the underlying collection. Type 1.
		/// </summary>
		public string CodingSchemeDesignator
		{
			get { return DicomElementProvider[DicomTags.CodingSchemeDesignator].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("value", "CodingSchemeDesignator is Type 1 Required.");
				DicomElementProvider[DicomTags.CodingSchemeDesignator].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of CodingSchemeRegistry in the underlying collection. Type 1C.
		/// </summary>
		public string CodingSchemeRegistry
		{
			get { return DicomElementProvider[DicomTags.CodingSchemeRegistry].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.CodingSchemeRegistry] = null;
					return;
				}
				DicomElementProvider[DicomTags.CodingSchemeRegistry].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of CodingSchemeUid in the underlying collection. Type 1C.
		/// </summary>
		public string CodingSchemeUid
		{
			get { return DicomElementProvider[DicomTags.CodingSchemeUid].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.CodingSchemeUid] = null;
					return;
				}
				DicomElementProvider[DicomTags.CodingSchemeUid].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of CodingSchemeExternalId in the underlying collection. Type 2C.
		/// </summary>
		public string CodingSchemeExternalId
		{
			get { return DicomElementProvider[DicomTags.CodingSchemeExternalId].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.CodingSchemeExternalId] = null;
					return;
				}
				DicomElementProvider[DicomTags.CodingSchemeExternalId].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of CodingSchemeName in the underlying collection. Type 3.
		/// </summary>
		public string CodingSchemeName
		{
			get { return DicomElementProvider[DicomTags.CodingSchemeName].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.CodingSchemeName] = null;
					return;
				}
				DicomElementProvider[DicomTags.CodingSchemeName].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of CodingSchemeVersion in the underlying collection. Type 3.
		/// </summary>
		public string CodingSchemeVersion
		{
			get { return DicomElementProvider[DicomTags.CodingSchemeVersion].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.CodingSchemeVersion] = null;
					return;
				}
				DicomElementProvider[DicomTags.CodingSchemeVersion].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of CodingSchemeResponsibleOrganization in the underlying collection. Type 3.
		/// </summary>
		public string CodingSchemeResponsibleOrganization
		{
			get { return DicomElementProvider[DicomTags.CodingSchemeResponsibleOrganization].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.CodingSchemeResponsibleOrganization] = null;
					return;
				}
				DicomElementProvider[DicomTags.CodingSchemeResponsibleOrganization].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets an enumeration of <see cref="UIH.RT.TMS.Dicom.DicomTag"/>s used by this sequence item.
		/// </summary>
		public static IEnumerable<uint> DefinedTags
		{
			get
			{
				yield return DicomTags.CodingSchemeDesignator;
				yield return DicomTags.CodingSchemeRegistry;
				yield return DicomTags.CodingSchemeUid;
				yield return DicomTags.CodingSchemeExternalId;
				yield return DicomTags.CodingSchemeName;
				yield return DicomTags.CodingSchemeVersion;
				yield return DicomTags.CodingSchemeResponsibleOrganization;
			}
		}
	}
}
