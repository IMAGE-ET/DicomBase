/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ISeriesData.cs
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

using System.Runtime.Serialization;

namespace UIH.RT.TMS.Dicom.Iod
{
	public interface ISeriesData
	{
		/// <summary>
		/// Gets the Study Instance UID of the identified series.
		/// </summary>
		[DicomField(DicomTags.StudyInstanceUid)]
		string StudyInstanceUid { get; }

		/// <summary>
		/// Gets the Series Instance UID of the identified series.
		/// </summary>
		[DicomField(DicomTags.SeriesInstanceUid)]
		string SeriesInstanceUid { get; }

		/// <summary>
		/// Gets the modality of the identified series.
		/// </summary>
		[DicomField(DicomTags.Modality)]
		string Modality { get; }

		/// <summary>
		/// Gets the series description of the identified series.
		/// </summary>
		[DicomField(DicomTags.SeriesDescription)]
		string SeriesDescription { get; }

		/// <summary>
		/// Gets the series number of the identified series.
		/// </summary>
		[DicomField(DicomTags.SeriesNumber)]
		int SeriesNumber { get; }

		/// <summary>
		/// Gets the number of composite object instances belonging to the identified series.
		/// </summary>
		[DicomField(DicomTags.NumberOfSeriesRelatedInstances)]
		int? NumberOfSeriesRelatedInstances { get; }
	}
}
