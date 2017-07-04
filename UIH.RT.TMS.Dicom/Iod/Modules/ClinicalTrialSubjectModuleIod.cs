/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ClinicalTrialSubjectModuleIod.cs
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

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// ClinicalTrialSubject Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.7.1.3 (Table C.7-2b)</remarks>
	public class ClinicalTrialSubjectModuleIod : IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ClinicalTrialSubjectModuleIod"/> class.
		/// </summary>	
		public ClinicalTrialSubjectModuleIod() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="ClinicalTrialSubjectModuleIod"/> class.
		/// </summary>
		public ClinicalTrialSubjectModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider) {}

		/// <summary>
		/// Initializes the underlying collection to implement the module or sequence using default values.
		/// </summary>
		public void InitializeAttributes()
		{
			this.ClinicalTrialProtocolId = " ";
			this.ClinicalTrialProtocolName = null;
			this.ClinicalTrialSiteId = null;
			this.ClinicalTrialSiteName = null;
			this.ClinicalTrialSponsorName = " ";
			this.ClinicalTrialSubjectId = null;
			this.ClinicalTrialSubjectReadingId = null;
		}

		/// <summary>
		/// Checks if this module appears to be non-empty.
		/// </summary>
		/// <returns>True if the module appears to be non-empty; False otherwise.</returns>
		public bool HasValues()
		{
			if (string.IsNullOrEmpty(this.ClinicalTrialProtocolId)
				&& string.IsNullOrEmpty(this.ClinicalTrialProtocolName)
				&& string.IsNullOrEmpty(this.ClinicalTrialSiteId)
				&& string.IsNullOrEmpty(this.ClinicalTrialSiteName)
				&& string.IsNullOrEmpty(this.ClinicalTrialSponsorName)
				&& string.IsNullOrEmpty(this.ClinicalTrialSubjectId)
				&& string.IsNullOrEmpty(this.ClinicalTrialSubjectReadingId))
				return false;
			return true;
		}

		/// <summary>
		/// Gets or sets the value of ClinicalTrialSponsorName in the underlying collection. Type 1.
		/// </summary>
		public string ClinicalTrialSponsorName
		{
			get { return base.DicomElementProvider[DicomTags.ClinicalTrialSponsorName].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("value", "ClinicalTrialSponsorName is Type 1 Required.");
				base.DicomElementProvider[DicomTags.ClinicalTrialSponsorName].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ClinicalTrialProtocolId in the underlying collection. Type 1.
		/// </summary>
		public string ClinicalTrialProtocolId
		{
			get { return base.DicomElementProvider[DicomTags.ClinicalTrialProtocolId].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("value", "ClinicalTrialProtocolId is Type 1 Required.");
				base.DicomElementProvider[DicomTags.ClinicalTrialProtocolId].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ClinicalTrialProtocolName in the underlying collection. Type 2.
		/// </summary>
		public string ClinicalTrialProtocolName
		{
			get { return base.DicomElementProvider[DicomTags.ClinicalTrialProtocolName].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.ClinicalTrialProtocolName].SetNullValue();
					return;
				}
				base.DicomElementProvider[DicomTags.ClinicalTrialProtocolName].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ClinicalTrialSiteId in the underlying collection. Type 2.
		/// </summary>
		public string ClinicalTrialSiteId
		{
			get { return base.DicomElementProvider[DicomTags.ClinicalTrialSiteId].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.ClinicalTrialSiteId].SetNullValue();
					return;
				}
				base.DicomElementProvider[DicomTags.ClinicalTrialSiteId].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ClinicalTrialSiteName in the underlying collection. Type 2.
		/// </summary>
		public string ClinicalTrialSiteName
		{
			get { return base.DicomElementProvider[DicomTags.ClinicalTrialSiteName].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.ClinicalTrialSiteName].SetNullValue();
					return;
				}
				base.DicomElementProvider[DicomTags.ClinicalTrialSiteName].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ClinicalTrialSubjectId in the underlying collection. Type 1C.
		/// </summary>
		public string ClinicalTrialSubjectId
		{
			get { return base.DicomElementProvider[DicomTags.ClinicalTrialSubjectId].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.ClinicalTrialSubjectId] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.ClinicalTrialSubjectId].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ClinicalTrialSubjectReadingId in the underlying collection. Type 1C.
		/// </summary>
		public string ClinicalTrialSubjectReadingId
		{
			get { return base.DicomElementProvider[DicomTags.ClinicalTrialSubjectReadingId].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.ClinicalTrialSubjectReadingId] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.ClinicalTrialSubjectReadingId].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets an enumeration of <see cref="DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags {
			get {
				yield return DicomTags.ClinicalTrialProtocolId;
				yield return DicomTags.ClinicalTrialProtocolName;
				yield return DicomTags.ClinicalTrialSiteId;
				yield return DicomTags.ClinicalTrialSiteName;
				yield return DicomTags.ClinicalTrialSponsorName;
				yield return DicomTags.ClinicalTrialSubjectId;
				yield return DicomTags.ClinicalTrialSubjectReadingId;
			}
		}
	}
}
