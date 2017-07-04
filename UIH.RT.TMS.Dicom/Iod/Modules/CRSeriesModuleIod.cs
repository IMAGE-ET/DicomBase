/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: CRSeriesModuleIod.cs
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
	/// CR Series Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2011, Part 3, Section C.8.1.1 (Table C.8-1)</remarks>
	public class CrSeriesModuleIod : IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CrSeriesModuleIod"/> class.
		/// </summary>	
		public CrSeriesModuleIod() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="CrSeriesModuleIod"/> class.
		/// </summary>
		/// <param name="dicomElementProvider">The DICOM attribute collection.</param>
		public CrSeriesModuleIod(IDicomElementProvider dicomElementProvider)
			: base(dicomElementProvider) {}

		/// <summary>
		/// Initializes the underlying collection to implement the module or sequence using default values.
		/// </summary>
		public void InitializeAttributes()
		{
			BodyPartExamined = string.Empty;
			ViewPosition = string.Empty;
			FilterType = null;
			CollimatorGridName = null;
			FocalSpots = null;
			PlateType = null;
			PhosphorType = null;
		}

		/// <summary>
		/// Checks if this module appears to be non-empty.
		/// </summary>
		/// <returns>True if the module appears to be non-empty; False otherwise.</returns>
		public bool HasValues()
		{
			if (string.IsNullOrEmpty(BodyPartExamined)
			    && string.IsNullOrEmpty(ViewPosition)
			    && string.IsNullOrEmpty(FilterType)
			    && string.IsNullOrEmpty(CollimatorGridName)
			    && IsNullOrEmpty(FocalSpots)
			    && string.IsNullOrEmpty(PlateType)
			    && string.IsNullOrEmpty(PhosphorType))
				return false;
			return true;
		}

		/// <summary>
		/// Gets an enumeration of <see cref="DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags
		{
			get
			{
				yield return DicomTags.BodyPartExamined;
				yield return DicomTags.ViewPosition;
				yield return DicomTags.FilterType;
				yield return DicomTags.CollimatorGridName;
				yield return DicomTags.FocalSpots;
				yield return DicomTags.PlateType;
				yield return DicomTags.PhosphorType;
			}
		}

		/// <summary>
		/// Gets or sets the value of BodyPartExamined in the underlying collection. Type 2.
		/// </summary>
		public string BodyPartExamined
		{
			get { return DicomElementProvider[DicomTags.BodyPartExamined].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.BodyPartExamined].SetNullValue();
					return;
				}
				DicomElementProvider[DicomTags.BodyPartExamined].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ViewPosition in the underlying collection. Type 2.
		/// </summary>
		public string ViewPosition
		{
			get { return DicomElementProvider[DicomTags.ViewPosition].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.ViewPosition].SetNullValue();
					return;
				}
				DicomElementProvider[DicomTags.ViewPosition].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of FilterType in the underlying collection. Type 3.
		/// </summary>
		public string FilterType
		{
			get { return DicomElementProvider[DicomTags.FilterType].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.FilterType] = null;
					return;
				}
				DicomElementProvider[DicomTags.FilterType].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of CollimatorGridName in the underlying collection. Type 3.
		/// </summary>
		public string CollimatorGridName
		{
			get { return DicomElementProvider[DicomTags.CollimatorGridName].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.CollimatorGridName] = null;
					return;
				}
				DicomElementProvider[DicomTags.CollimatorGridName].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of FocalSpots in the underlying collection. Type 3.
		/// </summary>
		public double[] FocalSpots
		{
			get
			{
				var dicomAttribute = DicomElementProvider[DicomTags.FocalSpots];
				if (dicomAttribute.IsNull || dicomAttribute.IsEmpty)
					return null;

				var values = new double[dicomAttribute.Count];
				for (var n = 0; n < values.Length; n++)
					values[n] = dicomAttribute.GetFloat64(n, 0);
				return values;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					DicomElementProvider[DicomTags.FocalSpots] = null;
					return;
				}

				var dicomAttribute = DicomElementProvider[DicomTags.FocalSpots];
				for (var n = 0; n < value.Length; n++)
					dicomAttribute.SetFloat64(n, value[n]);
			}
		}

		/// <summary>
		/// Gets or sets the value of PlateType in the underlying collection. Type 3.
		/// </summary>
		public string PlateType
		{
			get { return DicomElementProvider[DicomTags.PlateType].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.PlateType] = null;
					return;
				}
				DicomElementProvider[DicomTags.PlateType].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of PhosphorType in the underlying collection. Type 3.
		/// </summary>
		public string PhosphorType
		{
			get { return DicomElementProvider[DicomTags.PhosphorType].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.PhosphorType] = null;
					return;
				}
				DicomElementProvider[DicomTags.PhosphorType].SetString(0, value);
			}
		}
	}
}
