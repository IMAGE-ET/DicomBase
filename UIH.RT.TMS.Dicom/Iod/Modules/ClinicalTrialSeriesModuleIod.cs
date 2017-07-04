/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ClinicalTrialSeriesModuleIod.cs
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

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// ClinicalTrialSeries Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.7.3.2 (Table C.7-5b)</remarks>
	public class ClinicalTrialSeriesModuleIod : IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ClinicalTrialSeriesModuleIod"/> class.
		/// </summary>	
		public ClinicalTrialSeriesModuleIod() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="ClinicalTrialSeriesModuleIod"/> class.
		/// </summary>
		public ClinicalTrialSeriesModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider) { }

		/// <summary>
		/// Initializes the underlying collection to implement the module or sequence using default values.
		/// </summary>
		public void InitializeAttributes()
		{
			this.ClinicalTrialCoordinatingCenterName = null;
			this.ClinicalTrialSeriesId = null;
			this.ClinicalTrialSeriesDescription = null;
		}

		/// <summary>
		/// Checks if this module appears to be non-empty.
		/// </summary>
		/// <returns>True if the module appears to be non-empty; False otherwise.</returns>
		public bool HasValues()
		{
			if (string.IsNullOrEmpty(this.ClinicalTrialCoordinatingCenterName)
			    && string.IsNullOrEmpty(this.ClinicalTrialSeriesId)
			    && string.IsNullOrEmpty(this.ClinicalTrialSeriesDescription))
				return false;
			return true;
		}

		/// <summary>
		/// Gets or sets the value of ClinicalTrialCoordinatingCenterName in the underlying collection. Type 2.
		/// </summary>
		public string ClinicalTrialCoordinatingCenterName
		{
			get { return base.DicomElementProvider[DicomTags.ClinicalTrialCoordinatingCenterName].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.ClinicalTrialCoordinatingCenterName].SetNullValue();
					return;
				}
				base.DicomElementProvider[DicomTags.ClinicalTrialCoordinatingCenterName].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ClinicalTrialSeriesId in the underlying collection. Type 3.
		/// </summary>
		public string ClinicalTrialSeriesId
		{
			get { return base.DicomElementProvider[DicomTags.ClinicalTrialSeriesId].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.ClinicalTrialSeriesId] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.ClinicalTrialSeriesId].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ClinicalTrialSeriesDescription in the underlying collection. Type 3.
		/// </summary>
		public string ClinicalTrialSeriesDescription
		{
			get { return base.DicomElementProvider[DicomTags.ClinicalTrialSeriesDescription].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.ClinicalTrialSeriesDescription] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.ClinicalTrialSeriesDescription].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets an enumeration of <see cref="DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags {
			get {
				yield return DicomTags.ClinicalTrialCoordinatingCenterName;
				yield return DicomTags.ClinicalTrialSeriesDescription;
				yield return DicomTags.ClinicalTrialSeriesId;
			}
		}
	}
}
