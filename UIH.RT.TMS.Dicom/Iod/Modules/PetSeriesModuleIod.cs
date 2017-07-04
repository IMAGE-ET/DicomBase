/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: PetSeriesModuleIod.cs
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
using UIH.RT.TMS.Dicom.Utilities;

namespace UIH.RT.TMS.Dicom.Iod.Modules
{
	/// <summary>
	/// PET Series Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2011, Part 3, Section C.8.9.1 (Table C.8-60)</remarks>
	public class PetSeriesModuleIod : IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PetSeriesModuleIod"/> class.
		/// </summary>	
		public PetSeriesModuleIod() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="PetSeriesModuleIod"/> class.
		/// </summary>
		/// <param name="dicomElementProvider">The DICOM attribute collection.</param>
		public PetSeriesModuleIod(IDicomElementProvider dicomElementProvider)
			: base(dicomElementProvider) {}

		/// <summary>
		/// Gets an enumeration of <see cref="DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags
		{
			get
			{
				yield return DicomTags.SeriesDate;
				yield return DicomTags.SeriesTime;
				yield return DicomTags.Units;
				yield return DicomTags.SuvType;
				yield return DicomTags.CountsSource;
				yield return DicomTags.SeriesType;
				yield return DicomTags.ReprojectionMethod;
				yield return DicomTags.NumberOfRRIntervals;
				yield return DicomTags.NumberOfTimeSlots;
				yield return DicomTags.NumberOfTimeSlices;
				yield return DicomTags.NumberOfSlices;
				yield return DicomTags.CorrectedImage;
				yield return DicomTags.RandomsCorrectionMethod;
				yield return DicomTags.AttenuationCorrectionMethod;
				yield return DicomTags.ScatterCorrectionMethod;
				yield return DicomTags.DecayCorrection;
				yield return DicomTags.ReconstructionDiameter;
				yield return DicomTags.ConvolutionKernel;
				yield return DicomTags.ReconstructionMethod;
				yield return DicomTags.DetectorLinesOfResponseUsed;
				yield return DicomTags.AcquisitionStartCondition;
				yield return DicomTags.AcquisitionStartConditionData;
				yield return DicomTags.AcquisitionTerminationCondition;
				yield return DicomTags.AcquisitionTerminationConditionData;
				yield return DicomTags.FieldOfViewShape;
				yield return DicomTags.FieldOfViewDimensions;
				yield return DicomTags.GantryDetectorTilt;
				yield return DicomTags.GantryDetectorSlew;
				yield return DicomTags.TypeOfDetectorMotion;
				yield return DicomTags.CollimatorType;
				yield return DicomTags.CollimatorGridName;
				yield return DicomTags.AxialAcceptance;
				yield return DicomTags.AxialMash;
				yield return DicomTags.TransverseMash;
				yield return DicomTags.DetectorElementSize;
				yield return DicomTags.CoincidenceWindowWidth;
				yield return DicomTags.EnergyWindowRangeSequence;
				yield return DicomTags.SecondaryCountsType;
			}
		}

		/// <summary>
		/// Initializes the underlying collection to implement the module or sequence using default values.
		/// </summary>
		public void InitializeAttributes()
		{
			SeriesDateTime = null;
			Units = Units.Empty;
			SuvType = SuvType.None;
			CountsSource = CountsSource.None;
			SeriesType = null;
			ReprojectionMethod = null;
			NumberOfRRIntervals = null;
			NumberOfTimeSlots = null;
			NumberOfTimeSlices = null;
			NumberOfSlices = 0;
			CorrectedImage = null;
			RandomsCorrectionMethod = null;
			AttenuationCorrectionMethod = null;
			ScatterCorrectionMethod = null;
			DecayCorrection = DecayCorrection.Empty;
			ReconstructionDiameter = null;
			ConvolutionKernel = null;
			ReconstructionMethod = null;
			DetectorLinesOfResponseUsed = null;
			AcquisitionStartCondition = null;
			AcquisitionStartConditionData = null;
			AcquisitionTerminationCondition = null;
			AcquisitionTerminationConditionData = null;
			FieldOfViewShape = null;
			FieldOfViewDimensions = null;
			GantryDetectorTilt = null;
			GantryDetectorSlew = null;
			TypeOfDetectorMotion = null;
			CollimatorType = null;
			CollimatorGridName = null;
			AxialAcceptance = null;
			AxialMash = null;
			TransverseMash = null;
			DetectorElementSize = null;
			CoincidenceWindowWidth = null;
			EnergyWindowRangeSequence = null;
			SecondaryCountsType = null;
		}

		/// <summary>
		/// Checks if this module appears to be non-empty.
		/// </summary>
		/// <returns>True if the module appears to be non-empty; False otherwise.</returns>
		public bool HasValues()
		{
			if (SeriesDateTime == null
			    && Units.IsEmpty
			    && SuvType == SuvType.None
			    && CountsSource == CountsSource.None
			    && string.IsNullOrEmpty(SeriesType)
			    && string.IsNullOrEmpty(ReprojectionMethod)
			    && NumberOfRRIntervals == null
			    && NumberOfTimeSlots == null
			    && NumberOfTimeSlices == null
			    && NumberOfSlices == 0
			    && CorrectedImage == null
			    && RandomsCorrectionMethod == null
			    && string.IsNullOrEmpty(AttenuationCorrectionMethod)
			    && string.IsNullOrEmpty(ScatterCorrectionMethod)
			    && DecayCorrection.IsEmpty
			    && ReconstructionDiameter == null
			    && string.IsNullOrEmpty(ConvolutionKernel)
			    && string.IsNullOrEmpty(ReconstructionMethod)
			    && string.IsNullOrEmpty(DetectorLinesOfResponseUsed)
			    && string.IsNullOrEmpty(AcquisitionStartCondition)
			    && AcquisitionStartConditionData == null
			    && string.IsNullOrEmpty(AcquisitionTerminationCondition)
			    && AcquisitionTerminationConditionData == null
			    && string.IsNullOrEmpty(FieldOfViewShape)
			    && FieldOfViewDimensions == null
			    && GantryDetectorTilt == null
			    && GantryDetectorSlew == null
			    && string.IsNullOrEmpty(TypeOfDetectorMotion)
			    && string.IsNullOrEmpty(CollimatorType)
			    && string.IsNullOrEmpty(CollimatorGridName)
			    && AxialAcceptance == null
			    && AxialMash == null
			    && TransverseMash == null
			    && DetectorElementSize == null
			    && CoincidenceWindowWidth == null
			    && EnergyWindowRangeSequence == null
			    && string.IsNullOrEmpty(SecondaryCountsType))
				return false;
			return true;
		}

		/// <summary>
		/// Gets or sets the value of SeriesDate and SeriesTime in the underlying collection.  Type 1.
		/// </summary>
		public DateTime? SeriesDateTime
		{
			get
			{
				var date = DicomElementProvider[DicomTags.SeriesDate].GetString(0, string.Empty);
				var time = DicomElementProvider[DicomTags.SeriesTime].GetString(0, string.Empty);
				return DateTimeParser.ParseDateAndTime(string.Empty, date, time);
			}
			set
			{
				if (!value.HasValue)
					throw new ArgumentNullException("value", "Series is Type 1 Required.");
				var date = DicomElementProvider[DicomTags.SeriesDate];
				var time = DicomElementProvider[DicomTags.SeriesTime];
				DateTimeParser.SetDateTimeAttributeValues(value, date, time);
			}
		}

		/// <summary>
		/// Gets or sets the value of Units in the underlying collection. Type 1.
		/// </summary>
		public Units Units
		{
			get { return (Units) DicomElementProvider[DicomTags.Units].GetString(0, string.Empty); }
			set
			{
				if (value == Units.Empty)
					throw new ArgumentOutOfRangeException("value", "Units is Type 1 Required.");
				DicomElementProvider[DicomTags.Units].GetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of SuvType in the underlying collection. Type 3.
		/// </summary>
		public SuvType SuvType
		{
			get { return ParseEnum(DicomElementProvider[DicomTags.SuvType].GetString(0, string.Empty), SuvType.None); }
			set
			{
				if (value == SuvType.None)
				{
					DicomElementProvider[DicomTags.SuvType] = null;
					return;
				}
				SetAttributeFromEnum(DicomElementProvider[DicomTags.SuvType], value);
			}
		}

		/// <summary>
		/// Gets or sets the value of CountsSource in the underlying collection. Type 1.
		/// </summary>
		public CountsSource CountsSource
		{
			get { return ParseEnum(DicomElementProvider[DicomTags.CountsSource].GetString(0, string.Empty), CountsSource.None); }
			set
			{
				if (value == CountsSource.None)
					throw new ArgumentOutOfRangeException("value", "CountsSource is Type 1 Required.");
				SetAttributeFromEnum(DicomElementProvider[DicomTags.CountsSource], value);
			}
		}

		/// <summary>
		/// Gets or sets the value of SeriesType in the underlying collection. Type 1.
		/// </summary>
		public string SeriesType
		{
			get { return DicomElementProvider[DicomTags.SeriesType].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("value", "SeriesType is Type 1 Required.");
				DicomElementProvider[DicomTags.SeriesType].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ReprojectionMethod in the underlying collection. Type 2C.
		/// </summary>
		public string ReprojectionMethod
		{
			get { return DicomElementProvider[DicomTags.ReprojectionMethod].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.ReprojectionMethod] = null;
					return;
				}
				DicomElementProvider[DicomTags.ReprojectionMethod].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of NumberOfRRIntervals in the underlying collection. Type 1C.
		/// </summary>
		public int? NumberOfRRIntervals
		{
			get
			{
				int result;
				if (DicomElementProvider[DicomTags.NumberOfRRIntervals].TryGetInt32(0, out result))
					return result;
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					DicomElementProvider[DicomTags.NumberOfRRIntervals] = null;
					return;
				}
				DicomElementProvider[DicomTags.NumberOfRRIntervals].SetInt32(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the value of NumberOfTimeSlots in the underlying collection. Type 1C.
		/// </summary>
		public int? NumberOfTimeSlots
		{
			get
			{
				int result;
				if (DicomElementProvider[DicomTags.NumberOfTimeSlots].TryGetInt32(0, out result))
					return result;
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					DicomElementProvider[DicomTags.NumberOfTimeSlots] = null;
					return;
				}
				DicomElementProvider[DicomTags.NumberOfTimeSlots].SetInt32(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the value of NumberOfTimeSlices in the underlying collection. Type 1C.
		/// </summary>
		public int? NumberOfTimeSlices
		{
			get
			{
				int result;
				if (DicomElementProvider[DicomTags.NumberOfTimeSlices].TryGetInt32(0, out result))
					return result;
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					DicomElementProvider[DicomTags.NumberOfTimeSlices] = null;
					return;
				}
				DicomElementProvider[DicomTags.NumberOfTimeSlices].SetInt32(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the value of NumberOfSlices in the underlying collection. Type 1.
		/// </summary>
		public int NumberOfSlices
		{
			get { return DicomElementProvider[DicomTags.NumberOfSlices].GetInt32(0, 0); }
			set { DicomElementProvider[DicomTags.NumberOfSlices].SetInt32(0, value); }
		}

		/// <summary>
		/// Gets or sets the value of CorrectedImage in the underlying collection. Type 2.
		/// </summary>
		public CorrectedImage[] CorrectedImage
		{
			get
			{
				var dicomAttribute = DicomElementProvider[DicomTags.CorrectedImage];
				if (dicomAttribute.IsNull || dicomAttribute.IsEmpty)
					return null;

				var values = new CorrectedImage[dicomAttribute.Count];
				for (int n = 0; n < values.Length; n++)
					values[n] = (CorrectedImage) dicomAttribute.GetString(n, string.Empty);
				return values;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					DicomElementProvider[DicomTags.CorrectedImage].SetNullValue();
					return;
				}

				var dicomAttribute = DicomElementProvider[DicomTags.CorrectedImage];
				for (int n = 0; n < value.Length; n++)
					dicomAttribute.SetString(n, value[n]);
			}
		}

		/// <summary>
		/// Gets or sets the value of RandomsCorrectionMethod in the underlying collection. Type 3.
		/// </summary>
		public RandomsCorrectionMethod? RandomsCorrectionMethod
		{
			get
			{
				string result;
				if (DicomElementProvider[DicomTags.RandomsCorrectionMethod].TryGetString(0, out result))
					return (RandomsCorrectionMethod) result;
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					DicomElementProvider[DicomTags.RandomsCorrectionMethod] = null;
					return;
				}
				DicomElementProvider[DicomTags.RandomsCorrectionMethod].SetString(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the value of AttenuationCorrectionMethod in the underlying collection. Type 3.
		/// </summary>
		public string AttenuationCorrectionMethod
		{
			get { return DicomElementProvider[DicomTags.AttenuationCorrectionMethod].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.AttenuationCorrectionMethod] = null;
					return;
				}
				DicomElementProvider[DicomTags.AttenuationCorrectionMethod].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ScatterCorrectionMethod in the underlying collection. Type 3.
		/// </summary>
		public string ScatterCorrectionMethod
		{
			get { return DicomElementProvider[DicomTags.ScatterCorrectionMethod].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.ScatterCorrectionMethod] = null;
					return;
				}
				DicomElementProvider[DicomTags.ScatterCorrectionMethod].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of DecayCorrection in the underlying collection. Type 1.
		/// </summary>
		public DecayCorrection DecayCorrection
		{
			get { return (DecayCorrection) DicomElementProvider[DicomTags.DecayCorrection].GetString(0, string.Empty); }
			set
			{
				if (value == DecayCorrection.Empty)
					throw new ArgumentOutOfRangeException("value", "DecayCorrection is Type 1 Required.");
				DicomElementProvider[DicomTags.DecayCorrection].GetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ReconstructionDiameter in the underlying collection. Type 3.
		/// </summary>
		public double? ReconstructionDiameter
		{
			get
			{
				double result;
				if (DicomElementProvider[DicomTags.ReconstructionDiameter].TryGetFloat64(0, out result))
					return result;
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					DicomElementProvider[DicomTags.ReconstructionDiameter] = null;
					return;
				}
				DicomElementProvider[DicomTags.ReconstructionDiameter].SetFloat64(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ConvolutionKernel in the underlying collection. Type 3.
		/// </summary>
		public string ConvolutionKernel
		{
			get { return DicomElementProvider[DicomTags.ConvolutionKernel].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.ConvolutionKernel] = null;
					return;
				}
				DicomElementProvider[DicomTags.ConvolutionKernel].SetStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the value of ReconstructionMethod in the underlying collection. Type 3.
		/// </summary>
		public string ReconstructionMethod
		{
			get { return DicomElementProvider[DicomTags.ReconstructionMethod].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.ReconstructionMethod] = null;
					return;
				}
				DicomElementProvider[DicomTags.ReconstructionMethod].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of DetectorLinesOfResponseUsed in the underlying collection. Type 3.
		/// </summary>
		public string DetectorLinesOfResponseUsed
		{
			get { return DicomElementProvider[DicomTags.DetectorLinesOfResponseUsed].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.DetectorLinesOfResponseUsed] = null;
					return;
				}
				DicomElementProvider[DicomTags.DetectorLinesOfResponseUsed].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of AcquisitionStartCondition in the underlying collection. Type 3.
		/// </summary>
		public string AcquisitionStartCondition
		{
			get { return DicomElementProvider[DicomTags.AcquisitionStartCondition].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.AcquisitionStartCondition] = null;
					return;
				}
				DicomElementProvider[DicomTags.AcquisitionStartCondition].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of AcquisitionStartConditionData in the underlying collection. Type 3.
		/// </summary>
		public int? AcquisitionStartConditionData
		{
			get
			{
				int result;
				if (DicomElementProvider[DicomTags.AcquisitionStartConditionData].TryGetInt32(0, out result))
					return result;
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					DicomElementProvider[DicomTags.AcquisitionStartConditionData] = null;
					return;
				}
				DicomElementProvider[DicomTags.AcquisitionStartConditionData].SetInt32(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the value of AcquisitionTerminationCondition in the underlying collection. Type 3.
		/// </summary>
		public string AcquisitionTerminationCondition
		{
			get { return DicomElementProvider[DicomTags.AcquisitionTerminationCondition].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.AcquisitionTerminationCondition] = null;
					return;
				}
				DicomElementProvider[DicomTags.AcquisitionTerminationCondition].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of AcquisitionTerminationConditionData in the underlying collection. Type 3.
		/// </summary>
		public int? AcquisitionTerminationConditionData
		{
			get
			{
				int result;
				if (DicomElementProvider[DicomTags.AcquisitionTerminationConditionData].TryGetInt32(0, out result))
					return result;
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					DicomElementProvider[DicomTags.AcquisitionTerminationConditionData] = null;
					return;
				}
				DicomElementProvider[DicomTags.AcquisitionTerminationConditionData].SetInt32(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the value of FieldOfViewShape in the underlying collection. Type 3.
		/// </summary>
		public string FieldOfViewShape
		{
			get { return DicomElementProvider[DicomTags.FieldOfViewShape].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.FieldOfViewShape] = null;
					return;
				}
				DicomElementProvider[DicomTags.FieldOfViewShape].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of FieldOfViewDimensions in the underlying collection. Type 3.
		/// </summary>
		public double[] FieldOfViewDimensions
		{
			get
			{
				var dicomAttribute = DicomElementProvider[DicomTags.FieldOfViewDimensions];
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
					DicomElementProvider[DicomTags.FieldOfViewDimensions] = null;
					return;
				}

				var dicomAttribute = DicomElementProvider[DicomTags.FieldOfViewDimensions];
				for (var n = 0; n < value.Length; n++)
					dicomAttribute.SetFloat64(n, value[n]);
			}
		}

		/// <summary>
		/// Gets or sets the value of GantryDetectorTilt in the underlying collection. Type 3.
		/// </summary>
		public double? GantryDetectorTilt
		{
			get
			{
				double result;
				if (DicomElementProvider[DicomTags.GantryDetectorTilt].TryGetFloat64(0, out result))
					return result;
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					DicomElementProvider[DicomTags.GantryDetectorTilt] = null;
					return;
				}
				DicomElementProvider[DicomTags.GantryDetectorTilt].SetFloat64(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the value of GantryDetectorSlew in the underlying collection. Type 3.
		/// </summary>
		public double? GantryDetectorSlew
		{
			get
			{
				double result;
				if (DicomElementProvider[DicomTags.GantryDetectorSlew].TryGetFloat64(0, out result))
					return result;
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					DicomElementProvider[DicomTags.GantryDetectorSlew] = null;
					return;
				}
				DicomElementProvider[DicomTags.GantryDetectorSlew].SetFloat64(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the value of TypeOfDetectorMotion in the underlying collection. Type 3.
		/// </summary>
		public string TypeOfDetectorMotion
		{
			get { return DicomElementProvider[DicomTags.TypeOfDetectorMotion].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.TypeOfDetectorMotion] = null;
					return;
				}
				DicomElementProvider[DicomTags.TypeOfDetectorMotion].SetString(0, value);
			}
		}

		/// <summary>
		/// Gets or sets the value of CollimatorType in the underlying collection. Type 2.
		/// </summary>
		public string CollimatorType
		{
			get { return DicomElementProvider[DicomTags.CollimatorType].GetString(0, string.Empty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.CollimatorType].SetNullValue();
					return;
				}
				DicomElementProvider[DicomTags.CollimatorType].SetString(0, value);
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
		/// Gets or sets the value of AxialAcceptance in the underlying collection. Type 3.
		/// </summary>
		public double? AxialAcceptance
		{
			get
			{
				double result;
				if (DicomElementProvider[DicomTags.AxialAcceptance].TryGetFloat64(0, out result))
					return result;
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					DicomElementProvider[DicomTags.AxialAcceptance] = null;
					return;
				}
				DicomElementProvider[DicomTags.AxialAcceptance].SetFloat64(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the value of AxialMash in the underlying collection. Type 3.
		/// </summary>
		public int[] AxialMash
		{
			get
			{
				var result = new int[2];
				if (DicomElementProvider[DicomTags.AxialMash].TryGetInt32(0, out result[0])
				    && DicomElementProvider[DicomTags.AxialMash].TryGetInt32(1, out result[1]))
					return result;
				return null;
			}
			set
			{
				if (value == null || value.Length != 2)
				{
					DicomElementProvider[DicomTags.AxialMash] = null;
					return;
				}
				DicomElementProvider[DicomTags.AxialMash].SetInt32(0, value[0]);
				DicomElementProvider[DicomTags.AxialMash].SetInt32(1, value[1]);
			}
		}

		/// <summary>
		/// Gets or sets the value of TransverseMash in the underlying collection. Type 3.
		/// </summary>
		public int? TransverseMash
		{
			get
			{
				int result;
				if (DicomElementProvider[DicomTags.TransverseMash].TryGetInt32(0, out result))
					return result;
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					DicomElementProvider[DicomTags.TransverseMash] = null;
					return;
				}
				DicomElementProvider[DicomTags.TransverseMash].SetInt32(0, value.Value);
			}
		}

		/// <summary>
		/// Gets or sets the value of DetectorElementSize in the underlying collection. Type 3.
		/// </summary>
		public double[] DetectorElementSize
		{
			get
			{
				var result = new double[2];
				if (DicomElementProvider[DicomTags.DetectorElementSize].TryGetFloat64(0, out result[0])
				    && DicomElementProvider[DicomTags.DetectorElementSize].TryGetFloat64(1, out result[1]))
					return result;
				return null;
			}
			set
			{
				if (value == null || value.Length != 2)
				{
					DicomElementProvider[DicomTags.DetectorElementSize] = null;
					return;
				}
				DicomElementProvider[DicomTags.DetectorElementSize].SetFloat64(0, value[0]);
				DicomElementProvider[DicomTags.DetectorElementSize].SetFloat64(1, value[1]);
			}
		}

		/// <summary>
		/// Gets or sets the value of CoincidenceWindowWidth in the underlying collection. Type 3.
		/// </summary>
		public double? CoincidenceWindowWidth
		{
			get
			{
				double result;
				if (DicomElementProvider[DicomTags.CoincidenceWindowWidth].TryGetFloat64(0, out result))
					return result;
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					DicomElementProvider[DicomTags.CoincidenceWindowWidth] = null;
					return;
				}
				DicomElementProvider[DicomTags.CoincidenceWindowWidth].SetFloat64(0, value.Value);
			}
		}

		/// <summary>
		/// NOT IMPLEMENTED. Gets or sets the value of EnergyWindowRangeSequence in the underlying collection. Type 3.
		/// </summary> 		
		public object EnergyWindowRangeSequence
		{
			// TODO - Implement this.
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		/// <summary>
		/// Gets or sets the value of SecondaryCountsType in the underlying collection. Type 3.
		/// </summary>
		public string SecondaryCountsType
		{
			get { return DicomElementProvider[DicomTags.SecondaryCountsType].ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					DicomElementProvider[DicomTags.SecondaryCountsType] = null;
					return;
				}
				DicomElementProvider[DicomTags.SecondaryCountsType].SetStringValue(value);
			}
		}
	}
}
