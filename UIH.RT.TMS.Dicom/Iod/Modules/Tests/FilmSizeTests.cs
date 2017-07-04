/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: FilmSizeTests.cs
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

#if UNIT_TESTS

using System.Collections.Generic;
using NUnit.Framework;

namespace UIH.RT.TMS.Dicom.Iod.Modules.Tests
{
	[TestFixture]
	public class FilmSizeTests
	{
		[Test]
		public void Null_DicomString_Test()
		{
			var filmSize = FilmSize.FromDicomString(null);
			Assert.IsNull(filmSize);
		}

		[Test]
		public void Empty_DicomString_Test()
		{
			var filmSize = FilmSize.FromDicomString(string.Empty);
			Assert.IsNull(filmSize);
		}

		[Test]
		public void Supported_FilmSizes_Test()
		{
			var supportedFilmSizes = FilmSize.StandardFilmSizes;
			Assert.IsNotEmpty(new List<FilmSize>(supportedFilmSizes));
		}

		[Test]
		public void Valid_FilmSize_Test()
		{
			var filmSizeInInches = FilmSize.FromDicomString("11INX22IN");
			Assert.AreEqual(filmSizeInInches.DicomString, "11INX22IN");
			Assert.AreEqual(filmSizeInInches.GetWidth(FilmSize.FilmSizeUnit.Inch), 11);
			Assert.AreEqual(filmSizeInInches.GetHeight(FilmSize.FilmSizeUnit.Inch), 22);
			Assert.AreEqual(filmSizeInInches.GetWidth(FilmSize.FilmSizeUnit.Centimeter), 11 * LengthInMillimeter.Inch / 10);
			Assert.AreEqual(filmSizeInInches.GetHeight(FilmSize.FilmSizeUnit.Centimeter), 22 * LengthInMillimeter.Inch / 10);

			var filmSizeInCentimeter = FilmSize.FromDicomString("11CMX22CM");
			Assert.AreEqual(filmSizeInCentimeter.DicomString, "11CMX22CM");
			Assert.AreEqual(filmSizeInCentimeter.GetWidth(FilmSize.FilmSizeUnit.Centimeter), 11);
			Assert.AreEqual(filmSizeInCentimeter.GetHeight(FilmSize.FilmSizeUnit.Centimeter), 22);
			Assert.AreEqual(filmSizeInCentimeter.GetWidth(FilmSize.FilmSizeUnit.Inch), 11 * 10 / LengthInMillimeter.Inch);
			Assert.AreEqual(filmSizeInCentimeter.GetHeight(FilmSize.FilmSizeUnit.Inch), 22 * 10 / LengthInMillimeter.Inch);
		}

		[Test]
		[ExpectedException(typeof(FilmSize.InvalidFilmSizeException))]
		public void Lower_Case_X_Test()
		{
			FilmSize.FromDicomString("11MMx22MM");
		}

		[Test]
		[ExpectedException(typeof(FilmSize.InvalidFilmSizeException))]
		public void Invalid_Units_Test()
		{
			FilmSize.FromDicomString("11MMX22MM");
		}

		[Test]
		[ExpectedException(typeof(FilmSize.InvalidFilmSizeException))]
		public void Two_Different_Units_Test()
		{
			FilmSize.FromDicomString("11INX22CM");
		}

	}
}

#endif
