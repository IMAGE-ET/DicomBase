/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ContextGroupBase.cs
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UIH.RT.TMS.Common;
using UIH.RT.TMS.Common.Utilities;
using UIH.RT.TMS.Dicom.Iod.Macros;

namespace UIH.RT.TMS.Dicom.Iod.ContextGroups
{
	/// <summary>
	/// Base class representing a single DICOM context group.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Defines the baseline items that part of the context group, as well as provides methods for looking up
	/// the specific <see cref="ContextGroupItemBase"/> based on a <see cref="CodeSequenceMacro"/>.
	/// </para>
	/// <para>For additional information, please refer to the DICOM Standard 2008 PS 3.16.</para>
	/// </remarks>
	public abstract partial class ContextGroupBase<T> : IEnumerable<T> where T : ContextGroupBase<T>.ContextGroupItemBase
	{
		/// <summary>
		/// Gets the context ID of this group.
		/// </summary>
		public readonly int ContextId;

		/// <summary>
		/// Gets the name of this context group.
		/// </summary>
		public readonly string ContextGroupName;

		/// <summary>
		/// Gets a value indicating whether or not this context group is extensible.
		/// </summary>
		public readonly bool IsExtensible;

		/// <summary>
		/// Gets the version date of this context group.
		/// </summary>
		public readonly DateTime Version;

		/// <summary>
		/// Constructs a <see cref="ContextGroupBase{T}"/>.
		/// </summary>
		/// <param name="contextId">The context ID of this group.</param>
		/// <param name="contextGroupName">The name of this context group.</param>
		/// <param name="isExtensible">A value indicating whether or not this context group is extensible.</param>
		/// <param name="version">The value indicating whether or not this context group is extensible.</param>
		/// <exception cref="ArgumentException">Thrown if <paramref name="contextGroupName"/> is <code>null</code> or empty.</exception>
		protected ContextGroupBase(int contextId, string contextGroupName, bool isExtensible, DateTime version)
		{
			Platform.CheckForEmptyString(contextGroupName, "contextGroupName");

			this.ContextId = contextId;
			this.ContextGroupName = contextGroupName;
			this.IsExtensible = isExtensible;
			this.Version = version;
		}

		/// <summary>
		/// Gets an enumerator that iterates through the defined items of this context group.
		/// </summary>
		/// <returns>A <see cref="IEnumerator"/> object that can be used to iterate through the items of this context group.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>
		/// Gets an enumerator that iterates through the defined items of this context group.
		/// </summary>
		/// <returns>A <see cref="IEnumerator{T}"/> object that can be used to iterate through the items of this context group.</returns>
		public abstract IEnumerator<T> GetEnumerator();

		/// <summary>
		/// Creates a new code item to extend this context group.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Called by the base item when a lookup is performed on an extensible context group and the code does not exist in the group
		/// (as determined by <see cref="GetEnumerator"/>).
		/// </para>
		/// <para>
		/// Inheritors must override this method to call an appropriate constructor if the group is extensible.
		/// The default implementation always returns <code>null</code>.
		/// </para>
		/// </remarks>
		/// <param name="codingSchemeDesignator">The designator of the coding scheme in which this code is defined.</param>
		/// <param name="codingSchemeVersion">The version of the coding scheme in which this code is defined, if known. Should be <code>null</code> if not explicitly specified.</param>
		/// <param name="codeValue">The value of this code.</param>
		/// <param name="codeMeaning">The Human-readable meaning of this code.</param>
		/// <returns>A new code item.</returns>
		protected virtual T CreateContextGroupItem(string codingSchemeDesignator, string codingSchemeVersion, string codeValue, string codeMeaning)
		{
			return null;
		}

		/// <summary>
		/// Writes the value of the specified code item to the given code sequence.
		/// </summary>
		/// <remarks>
		/// The default implementation calls <see cref="ContextGroupItemBase.WriteToCodeSequence"/>.
		/// </remarks>
		/// <param name="value">The code item whose value should be written.</param>
		/// <param name="codeSequence">The code sequence to which the code is to be written.</param>
		public virtual void WriteToCodeSequence(T value, CodeSequenceMacro codeSequence)
		{
			value.WriteToCodeSequence(codeSequence);
		}

		/// <summary>
		/// Looks up a code item in the context group given the details specified by a code sequence.
		/// </summary>
		/// <remarks>
		/// The default implementation calls <see cref="Lookup(UIH.RT.TMS.Dicom.Iod.Macros.CodeSequenceMacro,bool)"/>.
		/// </remarks>
		/// <param name="codeSequence">The code sequence containing the code that is to be looked up.</param>
		/// <returns>A matching baseline code item if one is found, an extending code item if the context group is extensible and a match wasn't found, or <code>null</code> otherwise.</returns>
		public virtual T Lookup(CodeSequenceMacro codeSequence)
		{
			return Lookup(codeSequence, false);
		}

		/// <summary>
		/// Looks up a code item in the context group given the details specified by a code sequence.
		/// </summary>
		/// <remarks>
		/// The default implementation iterates through <see cref="GetEnumerator"/> and calls <see cref="ContextGroupItemBase.Equals(string,string,string,string,bool)"/> to find a match.
		/// </remarks>
		/// <param name="codeSequence">The code sequence containing the code that is to be looked up.</param>
		/// <param name="compareCodingSchemeVersion">A value indicating whether or not the coding scheme version should be compared when looking for a match.</param>
		/// <returns>A matching baseline code item if one is found, an extending code item if the context group is extensible and a match wasn't found, or <code>null</code> otherwise.</returns>
		public virtual T Lookup(CodeSequenceMacro codeSequence, bool compareCodingSchemeVersion)
		{
		    T result = this.FirstOrDefault(c => c.Equals(codeSequence, compareCodingSchemeVersion));
			if (result == null && this.IsExtensible)
				result = CreateContextGroupItem(codeSequence.CodingSchemeDesignator, codeSequence.CodingSchemeVersion, codeSequence.CodeValue, codeSequence.CodeMeaning);
			return result;
		}

		/// <summary>
		/// Looks up a code item in the context group given the details specified by a code sequence.
		/// </summary>
		/// <remarks>
		/// The default implementation iterates through <see cref="GetEnumerator"/> and calls <see cref="ContextGroupItemBase.Equals(string,string,string,string,bool)"/> to find a match.
		/// </remarks>
		/// <param name="codingSchemeDesignator">The designator of the coding scheme of the code to be looked up.</param>
		/// <param name="codingSchemeVersion">The version of the coding scheme of the code to be looked up.</param>
		/// <param name="codeValue">The value of this code of the code to be looked up.</param>
		/// <param name="codeMeaning">The Human-readable meaning of this code of the code to be looked up.</param>
		/// <param name="compareCodingSchemeVersion">A value indicating whether or not the coding scheme version should be compared when looking for a match.</param>
		/// <returns>A matching baseline code item if one is found, an extending code item if the context group is extensible and a match wasn't found, or <code>null</code> otherwise.</returns>
		public virtual T Lookup(string codingSchemeDesignator, string codeValue, string codeMeaning, string codingSchemeVersion, bool compareCodingSchemeVersion)
		{
			T result = this.FirstOrDefault(c => c.Equals(codingSchemeDesignator, codeValue, codeMeaning, codingSchemeVersion, compareCodingSchemeVersion));
			if (result == null && this.IsExtensible)
				result = CreateContextGroupItem(codingSchemeDesignator, codingSchemeVersion, codeValue, codeMeaning);
			return result;
		}
	}
}
