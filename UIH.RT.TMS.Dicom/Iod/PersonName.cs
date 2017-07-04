/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: PersonName.cs
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
using System.Globalization;
using UIH.RT.TMS.Common.Utilities;
using System.ComponentModel;

namespace UIH.RT.TMS.Dicom.Iod
{
    public enum PersonNameComparisonOptions
    {
        CaseSensitive,
        CaseInsensitive,
    }

	/// <summary>
	/// <see cref="TypeConverter"/> for <see cref="PersonName"/> class.
	/// </summary>
	public class PersonNameConverter : TypeConverter
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		public PersonNameConverter()
		{
		}

		/// <summary>
		/// Override of <see cref="TypeConverter.CanConvertTo"/>.
		/// </summary>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(string))
				return true;

			return base.CanConvertFrom(context, destinationType);
		}

		/// <summary>
		/// Override of <see cref="TypeConverter.CanConvertFrom"/>.
		/// </summary>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
				return true;

			return base.CanConvertFrom(context, sourceType);
		}

		/// <summary>
		/// Override of <see cref="TypeConverter.ConvertFrom(ITypeDescriptorContext,CultureInfo,object)"/>.
		/// </summary>
		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			if (value is string)
				return new PersonName(value as string);

			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>
		/// Override of <see cref="TypeConverter.ConvertTo(ITypeDescriptorContext,CultureInfo,object,Type)"/>.
		/// </summary>
		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
		{
			if (value is PersonName && destinationType == typeof(string))
				return value.ToString();

			return base.ConvertTo(context, culture, value, destinationType);
		}
	}

	/// <summary>
    /// Encapsulates the DICOM Person's Name.
    /// </summary>
    [TypeConverter(typeof(PersonNameConverter))]
    public class PersonName : IEquatable<PersonName>, IFormattable
    {
		private string _personsName;
		private string _formattedName;

		private readonly ComponentGroup[] _componentGroups = { ComponentGroup.GetEmptyComponentGroup(), 
                                                        ComponentGroup.GetEmptyComponentGroup(),
                                                        ComponentGroup.GetEmptyComponentGroup() };
		/// <summary>
        /// Protected constructor.
        /// </summary>
        protected PersonName()
        {
			SetInternalPersonName("");
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="personsName">The Person's Name as a string.</param>
        public PersonName(string personsName)
        {
			SetInternalPersonName(personsName);
        }

		/// <summary>
		/// Gets whether or not the value is empty.
		/// </summary>
        public bool IsEmpty
        {
            get { return String.IsNullOrEmpty(InternalPersonName); }
        }

        /// <summary>
        /// Gets or sets the internal person name value, unparsed.
        /// </summary>
		protected virtual string InternalPersonName
        {
            get { return _personsName; }
            set { SetInternalPersonName(value); }
		}

		#region Public Properties

		/// <summary>
		/// Gets the patient's last (family) name.
		/// </summary>
		public String LastName
        {
            get { return this.SingleByte.FamilyName; }
        }

		/// <summary>
		/// Gets the patient's first (given) name.
		/// </summary>
        public String FirstName
        {
            get { return this.SingleByte.GivenName; }
        }

        /// <summary>
        /// Gets the patient's middle name.
        /// </summary>
        public String MiddleName
        {
            get { return SingleByte.MiddleName; }
        }

        /// <summary>
        /// Gets the patient's title (prefix).
        /// </summary>
        public String Title
        {
            get { return SingleByte.Prefix; }
        }

        /// <summary>
        /// Gets the patient's suffix.
        /// </summary>
        public String Suffix
        {
            get { return SingleByte.Suffix; }
        }

		/// <summary>
		/// Gets the patient's name, formatted like: Last Name, First Name.
		/// </summary>
		public String FormattedName
		{
			get { return _formattedName; }
		}

		/// <summary>
		/// Gets the single byte <see cref="ComponentGroup"/> in the <see cref="PersonName"/>.
		/// </summary>
        public ComponentGroup SingleByte
        {
            get 
            {
                return _componentGroups[0];
            }
        }

		/// <summary>
		/// Gets the ideographic <see cref="ComponentGroup"/> in the <see cref="PersonName"/>.
		/// </summary>
		public ComponentGroup Ideographic
        {
            get
            {
                return _componentGroups[1];
            }
        }

		/// <summary>
		/// Gets the phonetic <see cref="ComponentGroup"/> in the <see cref="PersonName"/>.
		/// </summary>
		public ComponentGroup Phonetic
        {
            get
            {
                return _componentGroups[2];
            }
		}

		#endregion

		/// <summary>
		/// Gets the Person's Name as a string.
		/// </summary>
		/// <returns>A string representation of the Person's Name.</returns>
		public override string ToString()
		{
			return _personsName;
		}

		/// <summary>
		/// Implicit cast to a String object, for ease of use.
		/// </summary>
		public static implicit operator String(PersonName pn)
		{
			return pn.ToString();
		}

		/// <summary>
		/// Serves as a hash function for a particular type. <see cref="M:System.Object.GetHashCode"></see> is suitable for use in hashing algorithms and data structures like a hash table.
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:System.Object"></see>.
		/// </returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

        /// <summary>
        /// Returns a value indicating whether two <see cref="PersonName"/> are the same.
        /// </summary>
        /// <param name="other">Another <see cref="PersonName"/> to compare with</param>
        /// <param name="options">Comparison option</param>
        /// <returns>
        /// <b>true</b> if this person's name is the same as the other. <b>false</b> otherwise.
        /// </returns>
        public bool AreSame(PersonName other, PersonNameComparisonOptions options)
        {
            if (other == null)
                return false;

            return
                SingleByte.AreSame(other.SingleByte, options) && 
                Phonetic.AreSame(other.Phonetic, options) &&
                Ideographic.AreSame(other.Ideographic, options);
        }

		public override bool Equals(object obj)
		{
			if (obj == this)
				return true;

			if (obj is PersonName)
				return this.Equals((PersonName)obj);

			return false;
		}

		#region IEquatable<PersonName> Members

		public bool Equals(PersonName other)
		{
			if (other == null)
				return false;

			return InternalPersonName == other.InternalPersonName;
		}

		#endregion

		#region IFormattable Members

		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (formatProvider != null)
			{
				ICustomFormatter formatter = formatProvider.GetFormat(this.GetType()) as ICustomFormatter;
				if (formatter != null)
					return formatter.Format(format, this, formatProvider);
			}

			return ToString();

		}

		#endregion

		private void SetInternalPersonName(string personsName)
		{
			_personsName = personsName ?? "";
			BreakApartIntoComponentGroups();
			SetFormattedName();
		}

    	private void BreakApartIntoComponentGroups()
        {
            // if there's no name, don't do anything
            if (String.IsNullOrEmpty(this.InternalPersonName))
                return;

            string[] componentGroupsStrings = this.InternalPersonName.Split('=');

            if (componentGroupsStrings.GetUpperBound(0) >= 0 && componentGroupsStrings[0] != string.Empty)
                _componentGroups[0] = new ComponentGroup(componentGroupsStrings[0]);

            if (componentGroupsStrings.GetUpperBound(0) > 0 && componentGroupsStrings[1] != string.Empty)
                _componentGroups[1] = new ComponentGroup(componentGroupsStrings[1]);

            if (componentGroupsStrings.GetUpperBound(0) > 1 && componentGroupsStrings[2] != string.Empty)
                _componentGroups[2] = new ComponentGroup(componentGroupsStrings[2]);
		}
		
		private void SetFormattedName()
		{
			//by default, the formatted name is LastName, FirstName
			_formattedName = StringUtilities.Combine<string>(new string[] { this.LastName, this.FirstName }, ", ");
		}
    }
}
