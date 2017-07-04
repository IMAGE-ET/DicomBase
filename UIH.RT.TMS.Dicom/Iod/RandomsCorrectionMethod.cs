/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: RandomsCorrectionMethod.cs
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

namespace UIH.RT.TMS.Dicom.Iod
{
	/// <summary>
	/// Defined terms for the <see cref="UIH.RT.TMS.Dicom.DicomTags.RandomsCorrectionMethod"/> attribute 
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2011, Part 3, Section C.8.9.1 (Table C.8-60)</remarks>
	public struct RandomsCorrectionMethod
	{
		/// <summary>
		/// Represents the empty value.
		/// </summary>
		public static readonly RandomsCorrectionMethod Empty = new RandomsCorrectionMethod();

		public static readonly RandomsCorrectionMethod None = new RandomsCorrectionMethod("NONE", "no randoms correction");
		public static readonly RandomsCorrectionMethod Dlyd = new RandomsCorrectionMethod("DLYD", "delayed event subtraction");
		public static readonly RandomsCorrectionMethod Sing = new RandomsCorrectionMethod("SING", "singles estimation");

		/// <summary>
		/// Enumeration of defined terms.
		/// </summary>
		public static IEnumerable<RandomsCorrectionMethod> DefinedTerms
		{
			get
			{
				yield return None;
				yield return Dlyd;
				yield return Sing;
			}
		}

		private readonly string _code;
		private readonly string _description;

		/// <summary>
		/// Initializes a standard defined term.
		/// </summary>
		private RandomsCorrectionMethod(string code, string description)
		{
			_code = code;
			_description = description;
		}

		/// <summary>
		/// Initializes a defined term.
		/// </summary>
		/// <param name="code"></param>
		public RandomsCorrectionMethod(string code)
		{
			_description = _code = (code ?? string.Empty).ToUpperInvariant();
		}

		/// <summary>
		/// Gets the value of the defined term.
		/// </summary>
		public string Code
		{
			get { return _code ?? string.Empty; }
		}

		/// <summary>
		/// Gets a textual description of the defined term.
		/// </summary>
		public string Description
		{
			get { return _description ?? string.Empty; }
		}

		/// <summary>
		/// Gets a value indicating whether or not this represents the empty value.
		/// </summary>
		public bool IsEmpty
		{
			get { return string.IsNullOrEmpty(_code); }
		}

		/// <summary>
		/// Checks whether or not <paramref name="obj"/> represents the same defined term.
		/// </summary>
		public override bool Equals(object obj)
		{
			return obj is RandomsCorrectionMethod && Equals((RandomsCorrectionMethod) obj);
		}

		/// <summary>
		/// Checks whether or not this defined term is the same as <paramref name="other"/>.
		/// </summary>
		public bool Equals(RandomsCorrectionMethod other)
		{
			return Code == other.Code;
		}

		public override int GetHashCode()
		{
			return -0x0A4F34E4 ^ Code.GetHashCode();
		}

		public override string ToString()
		{
			return Code;
		}

		public static bool operator ==(RandomsCorrectionMethod x, RandomsCorrectionMethod y)
		{
			return Equals(x, y);
		}

		public static bool operator !=(RandomsCorrectionMethod x, RandomsCorrectionMethod y)
		{
			return !Equals(x, y);
		}

		public static implicit operator string(RandomsCorrectionMethod randomsCorrectionMethod)
		{
			return randomsCorrectionMethod.ToString();
		}

		public static explicit operator RandomsCorrectionMethod(string randomsCorrectionMethod)
		{
			return new RandomsCorrectionMethod(randomsCorrectionMethod);
		}
	}
}
