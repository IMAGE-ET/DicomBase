/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: PatientStudyModuleIod.cs
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
using UIH.RT.TMS.Dicom;
using UIH.RT.TMS.Dicom.Iod.Macros;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// PatientStudy Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.7.2.2 (Table C.7.4-a)</remarks>
	public class PatientStudyModuleIod : IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PatientStudyModuleIod"/> class.
		/// </summary>	
		public PatientStudyModuleIod() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="PatientStudyModuleIod"/> class.
		/// </summary>
		public PatientStudyModuleIod(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider) { }

		/// <summary>
		/// Initializes the underlying collection to implement the module or sequence using default values.
		/// </summary>
		public void InitializeAttributes()
		{
			this.AdditionalPatientHistory = null;
			this.AdmissionId = null;
			this.AdmittingDiagnosesCodeSequence = null;
			this.AdmittingDiagnosesDescription = null;
			this.IssuerOfAdmissionId = null;
			this.IssuerOfServiceEpisodeId = null;
			this.Occupation = null;
			this.PatientsAge = null;
			this.PatientsSexNeutered = null;
			this.PatientsSize = null;
			this.PatientsWeight = null;
			this.ServiceEpisodeDescription = null;
			this.ServiceEpisodeId = null;
		}

		/// <summary>
		/// Checks if this module appears to be non-empty.
		/// </summary>
		/// <returns>True if the module appears to be non-empty; False otherwise.</returns>
		public bool HasValues()
		{
			if (string.IsNullOrEmpty(this.AdditionalPatientHistory)
			    && string.IsNullOrEmpty(this.AdmissionId)
			    && this.AdmittingDiagnosesCodeSequence == null
			    && string.IsNullOrEmpty(this.AdmittingDiagnosesDescription)
			    && string.IsNullOrEmpty(this.IssuerOfAdmissionId)
			    && string.IsNullOrEmpty(this.IssuerOfServiceEpisodeId)
			    && string.IsNullOrEmpty(this.Occupation)
			    && string.IsNullOrEmpty(this.PatientsAge)
			    && (!this.PatientsSexNeutered.HasValue || this.PatientsSexNeutered == Iod.PatientsSexNeutered.Unknown)
			    && !this.PatientsSize.HasValue
			    && !this.PatientsWeight.HasValue
			    && string.IsNullOrEmpty(this.ServiceEpisodeDescription)
			    && string.IsNullOrEmpty(this.ServiceEpisodeId))
				return false;
			return true;
		}

		/// <summary>
		/// Gets or sets the value of AdmittingDiagnosesDescription in the underlying collection. Type 3.
		/// </summary>
		public string AdmittingDiagnosesDescription
		{
			get { return base.DicomElementProvider[DicomTags.AdmittingDiagnosesDescription].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.AdmittingDiagnosesDescription] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.AdmittingDiagnosesDescription].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of AdmittingDiagnosesCodeSequence in the underlying collection. Type 3.
		/// </summary>
		public CodeSequenceMacro[] AdmittingDiagnosesCodeSequence
		{
			get
			{
				DicomElement dicomElement = base.DicomElementProvider[DicomTags.AdmittingDiagnosesCodeSequence];
				if (dicomElement.IsNull || dicomElement.Count == 0)
				{
					return null;
				}

				CodeSequenceMacro[] result = new CodeSequenceMacro[dicomElement.Count];
				DicomSequenceItem[] items = (DicomSequenceItem[]) dicomElement.Values;
				for (int n = 0; n < items.Length; n++)
					result[n] = new CodeSequenceMacro(items[n]);

				return result;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.DicomElementProvider[DicomTags.AdmittingDiagnosesCodeSequence] = null;
					return;
				}

				DicomSequenceItem[] result = new DicomSequenceItem[value.Length];
				for (int n = 0; n < value.Length; n++)
					result[n] = value[n].DicomSequenceItem;

				base.DicomElementProvider[DicomTags.AdmittingDiagnosesCodeSequence].Values = result;
			}
		}

		/// <summary>
		/// Gets or sets the value of PatientsAge in the underlying collection. Type 3.
		/// </summary>
		public string PatientsAge
		{
			get { return base.DicomElementProvider[DicomTags.PatientsAge].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.PatientsAge] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.PatientsAge].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of PatientsSize in the underlying collection. Type 3.
		/// </summary>
		public double? PatientsSize
		{
			get
			{
				double result;
				if (base.DicomElementProvider[DicomTags.PatientsSize].TryGetFloat64(0, out result))
					return result;
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					base.DicomElementProvider[DicomTags.PatientsSize] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.PatientsSize].SetFloat64(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the value of PatientsWeight in the underlying collection. Type 3.
		/// </summary>
		public double? PatientsWeight
		{
			get
			{
				double result;
				if (base.DicomElementProvider[DicomTags.PatientsWeight].TryGetFloat64(0, out result))
					return result;
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					base.DicomElementProvider[DicomTags.PatientsWeight] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.PatientsWeight].SetFloat64(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the value of Occupation in the underlying collection. Type 3.
		/// </summary>
		public string Occupation
		{
			get { return base.DicomElementProvider[DicomTags.Occupation].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.Occupation] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.Occupation].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of AdditionalPatientHistory in the underlying collection. Type 3.
		/// </summary>
		public string AdditionalPatientHistory
		{
			get { return base.DicomElementProvider[DicomTags.AdditionalPatientHistory].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.AdditionalPatientHistory] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.AdditionalPatientHistory].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of AdmissionId in the underlying collection. Type 3.
		/// </summary>
		public string AdmissionId
		{
			get { return base.DicomElementProvider[DicomTags.AdmissionId].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.AdmissionId] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.AdmissionId].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of IssuerOfAdmissionId (Retired) in the underlying collection. Type 3.
		/// </summary>
		public string IssuerOfAdmissionId
		{
			get { return base.DicomElementProvider[DicomTags.IssuerOfAdmissionIdRetired].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.IssuerOfAdmissionIdRetired] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.IssuerOfAdmissionIdRetired].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ServiceEpisodeId in the underlying collection. Type 3.
		/// </summary>
		public string ServiceEpisodeId
		{
			get { return base.DicomElementProvider[DicomTags.ServiceEpisodeId].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.ServiceEpisodeId] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.ServiceEpisodeId].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of IssuerOfServiceEpisodeId in the underlying collection. Type 3.
		/// </summary>
		public string IssuerOfServiceEpisodeId
		{
			get { return base.DicomElementProvider[DicomTags.IssuerOfServiceEpisodeIdRetired].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
                    base.DicomElementProvider[DicomTags.IssuerOfServiceEpisodeIdRetired] = null;
					return;
				}
                base.DicomElementProvider[DicomTags.IssuerOfServiceEpisodeIdRetired].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ServiceEpisodeDescription in the underlying collection. Type 3.
		/// </summary>
		public string ServiceEpisodeDescription
		{
			get { return base.DicomElementProvider[DicomTags.ServiceEpisodeDescription].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DicomElementProvider[DicomTags.ServiceEpisodeDescription] = null;
					return;
				}
				base.DicomElementProvider[DicomTags.ServiceEpisodeDescription].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of PatientsSexNeutered in the underlying collection. Type 2C.
		/// </summary>
		public PatientsSexNeutered? PatientsSexNeutered
		{
			get
			{
				if (base.DicomElementProvider[DicomTags.PatientsSexNeutered].IsEmpty)
					return null;
				return ParseEnum(base.DicomElementProvider[DicomTags.PatientsSexNeutered].GetString(0, string.Empty), Iod.PatientsSexNeutered.Unknown);
			}
			set
			{
				if (!value.HasValue)
				{
					base.DicomElementProvider[DicomTags.PatientsSexNeutered] = null;
					return;
				}
				if (value == Iod.PatientsSexNeutered.Unknown)
				{
					base.DicomElementProvider[DicomTags.PatientsSexNeutered].SetNullValue();
					return;
				}
				SetAttributeFromEnum(base.DicomElementProvider[DicomTags.PatientsSexNeutered], value);
			}
		}

		/// <summary>
		/// Gets an enumeration of <see cref="DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags {
			get {
				yield return DicomTags.AdditionalPatientHistory;
				yield return DicomTags.AdmissionId;
				yield return DicomTags.AdmittingDiagnosesCodeSequence;
				yield return DicomTags.AdmittingDiagnosesDescription;
				yield return DicomTags.IssuerOfAdmissionIdRetired;
				yield return DicomTags.IssuerOfServiceEpisodeIdRetired;
				yield return DicomTags.Occupation;
				yield return DicomTags.PatientsAge;
				yield return DicomTags.PatientsSexNeutered;
				yield return DicomTags.PatientsSize;
				yield return DicomTags.PatientsWeight;
				yield return DicomTags.ServiceEpisodeDescription;
				yield return DicomTags.ServiceEpisodeId;
			}
		}
	}
}
