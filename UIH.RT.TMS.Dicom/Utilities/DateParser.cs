/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: DateParser.cs
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
using System.Globalization;

namespace UIH.RT.TMS.Dicom.Utilities
{
	/// <summary>
	/// The DateParser class parses dates that are formatted correctly according to Dicom.
	/// 
	/// We use the TryParseExact function to parse the dates because it is far more efficient
	/// than ParseExact since it does not throw exceptions.
	/// 
	/// See http://blogs.msdn.com/ianhu/archive/2005/12/19/505702.aspx for a good profile
	/// comparision of the different Parse/Convert methods.
	/// </summary>
	public static class DateParser
	{
		public const string DicomDateFormat = "yyyyMMdd";

		/// <summary>
		/// Attempts to parse the date string exactly, according to accepted Dicom format(s).
		/// Will *not* throw an exception if the format is invalid.
		/// </summary>
		/// <param name="dicomDate">the dicom date string</param>
		/// <returns>a nullable DateTime</returns>
		public static DateTime? Parse(string dicomDate)
		{
			DateTime date;
			if (!Parse(dicomDate, out date))
				return null;

			return date;
		}

		/// <summary>
		/// Attempts to parse the date string exactly, according to accepted Dicom format(s).
		/// Will *not* throw an exception if the format is invalid.
		/// </summary>
		/// <param name="dicomDate">the dicom date string</param>
		/// <param name="date">returns the date as a DateTime object</param>
		/// <returns>true on success, false otherwise</returns>
		public static bool Parse(string dicomDate, out DateTime date)
		{
			// This method is used in DicomElement Get/TryGet,
			// which allow leading/trailing spaces in the string
			// They are considered valid DICOM date/time.
			if (dicomDate != null)
				dicomDate = dicomDate.Trim();

			return DateTime.TryParseExact(dicomDate, DicomDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
		}

		/// <summary>
		/// Convert a DateTime object into a DA string
		/// </summary>
		/// <param name="datetime"></param>
		/// <returns>The DICOM formatted string</returns>
		public static string ToDicomString(DateTime datetime)
		{
			return datetime.ToString(DicomDateFormat, CultureInfo.InvariantCulture);
		}
	}
}
