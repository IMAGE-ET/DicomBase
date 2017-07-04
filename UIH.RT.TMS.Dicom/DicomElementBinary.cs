/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: DicomElementBinary.cs
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
using System.IO;
using System.Text;
using UIH.RT.TMS.Dicom.IO;
using UIH.RT.TMS.Dicom;

namespace UIH.RT.TMS.Dicom
{
	/// <summary>
	/// <see cref="DicomElement"/> derived class used to represent tags with binary values.
	/// </summary>
	public abstract class DicomElementBinary : DicomElement
	{
		protected DicomElementBinary(DicomTag tag)
			: base(tag) {}

		protected DicomElementBinary(uint tag)
			: base(tag) {}

		protected DicomElementBinary(DicomElement attrib)
			: base(attrib) {}

		public abstract override DicomElement Copy();

		internal abstract override DicomElement Copy(bool copyBinary);

		/// <summary>
		/// Creates a <see cref="Stream"/> which maps to a view of this <see cref="DicomElementBinary"/>, and can be used to read or write to the underlying data.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Each view <see cref="Stream"/> created by this method keeps track of position independently of all other views, although the byte stream is ultimately the same.
		/// In other words, modifying one view will cause those changes to appear in other views, but each view is still guaranteed that the <see cref="Stream.Position"/>
		/// will maintain the appropriate state regardless of external changes.
		/// </para>
		/// </remarks>
		/// <returns>A new instance of a <see cref="Stream"/> which maps to a view of this <see cref="DicomElementBinary"/>.</returns>
		public abstract Stream AsStream();
	}

	#region DicomElementBinary<T>

	/// <summary>
	/// <see cref="DicomElement"/> derived class used to represent tags with binary values.
	/// </summary>
	/// <typeparam name="T">The type that the element is storing.</typeparam>
	public abstract class DicomElementBinary<T> : DicomElementBinary
		where T : struct
	{
		private NumberStyles _numberStyle = NumberStyles.Any;
		private DicomElementBinaryData<T> _values;
		private FileReference _reference;

		#region Constructors

		internal DicomElementBinary(uint tag)
			: base(tag) {}

		internal DicomElementBinary(DicomTag tag)
			: base(tag) {}

		internal DicomElementBinary(DicomTag tag, FileReference reference)
			: base(tag)
		{
			_reference = reference;
			_values = null;
		}

		internal DicomElementBinary(DicomTag tag, ByteBuffer item)
			: base(tag)
		{
			if (ByteBuffer.LocalMachineEndian != item.Endian)
				item.Swap(tag.VR.UnitSize);

			_values = new DicomElementBinaryData<T>(item);
			_reference = null;
		}

		internal DicomElementBinary(DicomElementBinary<T> attrib)
			: base(attrib)
		{
			if (attrib._reference != null)
			{
				// just reassign reference, since the object is ready-only anyways
				_reference = attrib._reference;
			}
			else
			{
				_values = attrib._values != null ? new DicomElementBinaryData<T>(attrib._values) : null;
			}
		}

		#endregion

		#region Properties

		public override sealed long Count
		{
			get
			{
				if (_reference != null) return _reference.Length/Tag.VR.UnitSize;
				return _values != null ? _values.Count : 0;
			}
			protected set { throw new NotSupportedException(); }
		}

		public override sealed uint StreamLength
		{
			get
			{
				// StreamLength always returns the length of the stream padded to an even number of bytes
				if (_reference != null) return _reference.Length + (_reference.Length%2);
				return _values != null ? (uint) (_values.Length + (_values.Length%2)) : 0;
			}
			protected set { throw new NotSupportedException(); }
		}

		/// <summary>
		/// Gets or sets the number style used in parsing and formatting the values of the element.
		/// </summary>
		public NumberStyles NumberStyle
		{
			get { return _numberStyle; }
			set { _numberStyle = value; }
		}

		/// <summary>
		/// Gets or sets the buffer object encapsulating the values of the element.
		/// </summary>
		/// <remarks>
		/// Reading from this property will cause a stored file reference to be immediately dereferenced and the data loaded into memory.
		/// Writing to this property will clear a stored file reference.
		/// </remarks>
		protected internal DicomElementBinaryData<T> Data
		{
			get
			{
				if (_reference != null)
				{
					_values = Load();
					_reference = null;
				}
				return _values;
			}
			set
			{
				_values = value;
				_reference = null;
			}
		}

		/// <summary>
		/// Gets the stored file reference if applicable.
		/// </summary>
		internal FileReference Reference
		{
			get { return _reference; }
		}

		public T this[int index]
		{
			get
			{
				if (Data == null)
					throw new IndexOutOfRangeException("The index is out of range.");
				return Data[index];
			}
		}

		#endregion

		#region Private Methods

		protected void AppendValue(T val)
		{
			if (Data == null)
				Data = new DicomElementBinaryData<T>();
			Data.AppendValue(val);
		}

		private DicomElementBinaryData<T> Load()
		{
			ByteBuffer bb;
			using (FileStream fs = File.OpenRead(_reference.Filename))
			{
				fs.Seek(_reference.Offset, SeekOrigin.Begin);

				bb = new ByteBuffer(_reference.Length);
				bb.CopyFrom(fs, (int) _reference.Length);
				fs.Close();
			}

			if (ByteBuffer.LocalMachineEndian != _reference.Endian)
				bb.Swap(Tag.VR.UnitSize);

			return new DicomElementBinaryData<T>(bb);
		}

		internal override sealed uint CalculateWriteLength(TransferSyntax syntax, DicomWriteOptions options)
		{
			return CalculateWriteLength(syntax, options, _values);
		}

		internal virtual uint CalculateWriteLength(TransferSyntax syntax, DicomWriteOptions options, DicomElementBinaryData<T> values)
		{
			// for some reason, OB element has a special override of this that isn't just a special case of the base implementation
			return base.CalculateWriteLength(syntax, options);
		}

		#endregion

		#region Abstract Methods

		protected abstract T ParseNumber(string val, CultureInfo culture);

		protected T ParseNumber(string val)
		{
			return ParseNumber(val, CultureInfo.InvariantCulture);
		}

		protected abstract string FormatNumber(T val, CultureInfo culture);

		protected string FormatNumber(T val)
		{
			return FormatNumber(val, CultureInfo.InvariantCulture);
		}

		public override void SetNullValue()
		{
			_reference = null;
			_values = new DicomElementBinaryData<T>();
		}

		public override void SetEmptyValue()
		{
			_reference = null;
			_values = null;
		}

		public override Stream AsStream()
		{
			var data = Data;
			if (data == null) Data = data = new DicomElementBinaryData<T>();
			return data.AsStream();
		}

		internal override ByteBuffer GetByteBuffer(TransferSyntax syntax, String specificCharacterSet)
		{
			ByteBuffer bb;
			if (_reference != null)
			{
				using (FileStream fs = File.OpenRead(_reference.Filename))
				{
					fs.Seek(_reference.Offset, SeekOrigin.Begin);

					bb = new ByteBuffer(_reference.Length);
					bb.CopyFrom(fs, (int) _reference.Length);
					fs.Close();
				}

				if (syntax.Endian != _reference.Endian)
					bb.Swap(Tag.VR.UnitSize);
			}
			else if (_values != null)
			{
				bb = _values.CreateByteBuffer(syntax.Endian);

				if (syntax.Endian != ByteBuffer.LocalMachineEndian)
					bb.Swap(Tag.VR.UnitSize);
			}
			else
			{
				bb = new ByteBuffer(syntax.Endian);
			}
			return bb;
		}

		public override bool Equals(object obj)
		{
			// check for null and compare types
			if (obj == null || GetType() != obj.GetType()) return false;

			var other = (DicomElementBinary<T>) obj;

			// if both are null or both are empty, consider it equal
			if ((IsNull && other.IsNull) || (IsEmpty && other.IsEmpty)) return true;

			// if exactly one is null or exactly one is empty, consider it unequal
			if ((IsNull ^ other.IsNull) || (IsEmpty ^ other.IsEmpty)) return false;

			// same non-null file reference means data will be exactly the same
			if (!ReferenceEquals(_reference, null) && ReferenceEquals(_reference, other._reference)) return true;

			// same non-null values buffer means data will be exactly the same
			if (!ReferenceEquals(_values, null) && ReferenceEquals(_values, other._values)) return true;

			// accessing Data property will force the values to be loaded from disk, if only a reference is stored
			return Data.CompareValues(other.Data);
		}

		public override int GetHashCode()
		{
			// no need to get fancy here, the only requirement is that "a.Equals(b)" implies "a.GetHashCode()==b.GetHashCode()"
			return -0x68C0B468 ^ GetType().GetHashCode();
		}

		/// <summary>
		/// The type that the element stores.
		/// </summary>
		/// <returns></returns>
		public override Type GetValueType()
		{
			return typeof (T);
		}

		/// <summary>
		/// Retrieves a value as a string.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public override bool TryGetString(int index, out String value)
		{
			if (Data == null || Data.Count <= index)
			{
				value = "";
				return false;
			}
			value = FormatNumber(Data[index]);
			return true;
		}

		/// <summary>
		/// Sets the tag value(s) from a string
		/// If the string cannot be converted into tag's VR, DicomDataException will be thrown
		/// </summary>
		/// <param name="stringValue"></param>
		public override void SetStringValue(String stringValue)
		{
			if (string.IsNullOrEmpty(stringValue))
			{
				Data = new DicomElementBinaryData<T>();
				return;
			}

			String[] stringValues = stringValue.Split(new[] {'\\'});
			var data = new DicomElementBinaryData<T>(stringValues.Length);
			for (int index = 0; index < stringValues.Length; index++)
				data[index] = ParseNumber(stringValues[index]);
			Data = data; // set after, so that we don't get partial state due to a parse exception
		}

		/// <summary>
		/// Sets the value from a string
		/// If the string cannot be converted into tag's VR, DicomDataException will be thrown
		/// If <paramref name="index"/> equals to <seealso cref="Count"/>, this method behaves the same as <seealso cref="AppendString"/>.
		/// If <paramref name="index"/> is less than 0 or greater than <see cref="Count"/>, IndexOutofBoundException will be thrown.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		public override void SetString(int index, string value)
		{
			T v = ParseNumber(value);

			SetValue(index, v);
		}

		/// <summary>
		/// Appends an element from a string
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be converted into tag VR</exception>
		public override void AppendString(string value)
		{
			T v = ParseNumber(value);

			AppendValue(v);
		}

		/// <summary>
		/// Sets an element value
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> exceeds the range of the VR or cannot convert into the VR</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		/// 
		public void SetValue(int index, T value)
		{
			if (Data == null)
			{
				if (index == 0)
				{
					AppendValue(value);
				}
				else
				{
					//get a null reference exception unless we do this.
					throw new IndexOutOfRangeException("The index is out of range.");
				}
			}
			else
			{
				Data[index] = value;
			}
		}

		public override string ToString()
		{
			// Handle pixel data reference case
			if (_reference != null)
				return String.Format("Binary tag {0} of length {1} at offset {2} stored in file", Tag, _reference.Length, _reference.Offset);

			if (_values == null)
				return String.Empty;

			StringBuilder val = null;
			foreach (T index in _values)
			{
				if (val == null)
					val = new StringBuilder(FormatNumber(index));
				else
					val.AppendFormat("\\{0}", FormatNumber(index));
			}

			if (val == null)
				return "";

			return val.ToString();
		}

		public override bool IsNull
		{
			get
			{
				if (_reference != null) return _reference.Length == 0;
				return (_values != null && _values.Count == 0);
			}
		}

		public override bool IsEmpty
		{
			get { return (Count == 0) && (_reference == null) && (_values == null); }
		}

		/// <summary>
		/// Abstract property for setting or getting the values associated with the element.
		/// </summary>
		public override sealed Object Values
		{
			get { return Data != null ? Data.ToArray() : null; }
			set
			{
				if (value == null)
				{
					// JY (2012-12-06): if value is NULL, always null the data as a non-overridable behaviour
					Data = null;
					return;
				}

				// JY (2012-12-06): let inheritors try to handle the values, and if that fails try a last chance attempt with parsing its ToString() representation
				var result = SetValuesCore(value);
				if (!result)
				{
					// JY (2009-11-06): Leaving this ToString() to use the local culture settings for *BOTH* format and parse
					//  We don't know what type the value is, so we'll assume it knows how to convert itself into a string
					//  using assuming local culture settings, and hence we'll parse it back also assuming local culture settings
					try
					{
						var parsedValue = ParseNumber(value.ToString(), CultureInfo.CurrentCulture);
						Data = new DicomElementBinaryData<T>(new[] {parsedValue}, false);
					}
					catch (Exception)
					{
						throw new DicomException(SR.InvalidType);
					}
				}
			}
		}

		protected virtual bool SetValuesCore(object value)
		{
			if (value is T[])
			{
				Data = new DicomElementBinaryData<T>((T[]) value);
			}
			else if (value is T)
			{
				Data = new DicomElementBinaryData<T>(1);
				Data.SetValue(0, (T) value);
			}
			else if (value is string)
			{
				SetStringValue((string) value);
			}
			else
			{
				return false;
			}
			return true;
		}

		#endregion
	}

	#endregion

	#region DicomElementAt

	/// <summary>
	/// <see cref="DicomElementBinary"/> derived class for storing AT value representation tags.
	/// </summary>
	/// 
	public class DicomElementAt : DicomElementBinary<uint>
	{
		#region Constructors

		public DicomElementAt(uint tag)
			: base(tag) {}

		public DicomElementAt(DicomTag tag)
			: base(tag)
		{
			if (!tag.VR.Equals(DicomVr.ATvr)
			    && !tag.MultiVR)
				throw new DicomException(SR.InvalidVR);
		}

		internal DicomElementAt(DicomTag tag, ByteBuffer item)
			: base(tag)
		{
			if (ByteBuffer.LocalMachineEndian != item.Endian)
				item.Swap(tag.VR.UnitSize/2);

			var buffer = item.ToBytes();
			var values = new uint[item.Length/tag.VR.UnitSize];
			for (int i = 0; i < values.Length; i++)
			{
				Buffer.BlockCopy(buffer, i*tag.VR.UnitSize, values, i*tag.VR.UnitSize + 2, 2);
				Buffer.BlockCopy(buffer, i*tag.VR.UnitSize + 2, values, i*tag.VR.UnitSize, 2);
			}
			Data = new DicomElementBinaryData<uint>(values, false);
		}

		internal DicomElementAt(DicomElementAt attrib)
			: base(attrib) {}

		#endregion

		#region Operators

		#endregion

		#region Abstract Method Implementation

		internal override ByteBuffer GetByteBuffer(TransferSyntax syntax, String specificCharacterSet)
		{
			var values = Data.ToArray();
			var buffer = new byte[values.Length*Tag.VR.UnitSize];
			for (int i = 0; i < values.Length; i++)
			{
				Buffer.BlockCopy(values, i*Tag.VR.UnitSize + 2, buffer, i*Tag.VR.UnitSize, 2);
				Buffer.BlockCopy(values, i*Tag.VR.UnitSize, buffer, i*Tag.VR.UnitSize + 2, 2);
			}

			ByteBuffer bb = new ByteBuffer(buffer, syntax.Endian);
			if (syntax.Endian != ByteBuffer.LocalMachineEndian)
			{
				bb.Swap(Tag.VR.UnitSize/2);
			}

			return bb;
		}

		public override DicomElement Copy()
		{
			return new DicomElementAt(this);
		}

		internal override DicomElement Copy(bool copyBinary)
		{
			return new DicomElementAt(this);
		}

		protected override uint ParseNumber(string val, CultureInfo culture)
		{
			if (string.IsNullOrEmpty(val))
				throw new DicomDataException("Null values invalid for AT VR");

			uint parseVal;
			if (!uint.TryParse(val.Trim(), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out parseVal))
				throw new DicomDataException(String.Format("Invalid uint format value for tag {0}: {1}", Tag, val));
			return parseVal;
		}

		protected override string FormatNumber(uint val, CultureInfo culture)
		{
			return val.ToString("X", CultureInfo.InvariantCulture);
		}

		public override string ToString()
		{
			if (Data == null)
				return string.Empty;

			StringBuilder val = null;
			foreach (uint index in Data)
			{
				if (val == null)
					val = new StringBuilder(FormatNumber(index));
				else
					val.AppendFormat("\\{0}", FormatNumber(index));
			}
			return val == null ? string.Empty : val.ToString();
		}

		/// <summary>
		/// Retrieves an Int16 value from an AT element.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		/// <list type="bullet">
		/// <item>If the value doesn't exist</item>
		/// <item>The value exceeds Int16 range.</item>
		/// </list>
		///     
		/// If the method returns false, the returned <paramref name="value"/> is not reliable..
		/// </remarks>
		/// 
		public override bool TryGetInt16(int index, out Int16 value)
		{
			uint val;
			if (Data == null || !Data.TryGetValue(index, out val))
			{
				value = 0;
				return false;
			}
			value = (Int16) val;
			return val <= Int16.MaxValue;
		}

		/// <summary>
		/// Retrieves an Int32 value from an AT element.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// <list type="bullet">
		/// <item>If the value doesn't exist</item>
		/// <item>The value cannot be converted into Int32</item>
		/// <item>The value is an integer but too big or too small to fit into an Int32</item>
		/// </list>
		/// If the method returns false, the returned <paramref name="index"/> is not reliable..
		/// </remarks>
		public override bool TryGetInt32(int index, out Int32 value)
		{
			uint val;
			if (Data == null || !Data.TryGetValue(index, out val))
			{
				value = 0;
				return false;
			}
			value = (Int32) val;
			return val <= Int32.MaxValue;
		}

		/// <summary>
		/// Retrieves an Int64 value from an AT element.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		/// <list type="bullet">
		/// <item>If the value doesn't exist</item>
		/// </list>
		/// 
		/// If the method returns false, the returned <paramref name="value"/> is not reliable..
		/// </remarks>
		public override bool TryGetInt64(int index, out Int64 value)
		{
			uint val;
			if (Data == null || !Data.TryGetValue(index, out val))
			{
				value = 0;
				return false;
			}
			value = val;
			return true;
		}

		/// <summary>
		/// Retrieves an UInt16 value from an AT element.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		/// <list type="bullet">
		/// <item>The value doesn't exist</item>
		/// <item>The value exceeds UInt16 range.</item>
		/// </list>
		///     
		/// If the method returns false, the returned <paramref name="value"/> is not reliable..
		/// </remarks>
		public override bool TryGetUInt16(int index, out UInt16 value)
		{
			uint val;
			if (Data == null || !Data.TryGetValue(index, out val))
			{
				value = 0;
				return false;
			}
			value = (UInt16) val;
			return val >= UInt16.MinValue && val <= UInt16.MaxValue;
		}

		/// <summary>
		/// Retrieves an UInt32 value from an AT element.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		/// <list>
		/// <item>If the value doesn't exist.</item>
		/// </list>
		/// 
		/// If the method returns false, the returned <paramref name="value"/> is not reliable..
		/// </remarks>
		public override bool TryGetUInt32(int index, out UInt32 value)
		{
			value = 0;
			return Data != null && Data.TryGetValue(index, out value);
		}

		/// <summary>
		/// Retrieves an UInt64 value from an AT element.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		/// <list>
		/// <item>If the value doesn't exist.</item>
		/// </list>
		/// 
		/// If the method returns false, the returned <paramref name="value"/> is not reliable..
		/// </remarks>
		/// 
		public override bool TryGetUInt64(int index, out UInt64 value)
		{
			uint val;
			if (Data == null || !Data.TryGetValue(index, out val))
			{
				value = 0;
				return false;
			}
			value = val;
			return true;
		}

		/// <summary>
		/// Retrieves the string representation of an AT value in hexadecimal format.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public override bool TryGetString(int index, out string value)
		{
			uint val;
			if (Data == null || !Data.TryGetValue(index, out val))
			{
				value = string.Empty;
				return false;
			}
			value = val.ToString("X8"); // Convert to HEX
			return true;
		}

		/// <summary>
		/// Sets an AT value.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit unsigned int</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		public override void SetUInt16(int index, UInt16 value)
		{
			// If the source value cannot fit into the destination throw exception
			if (value < UInt32.MinValue)
				throw new DicomDataException(String.Format("Invalid AT value {0} for tag {1}.", value, Tag));

			SetValue(index, value);
		}

		/// <summary>
		/// Sets an AT value.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		public override void SetUInt32(int index, UInt32 value)
		{
			SetValue(index, value);
		}

		/// <summary>
		/// Sets an AT value.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit unsigned int</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		public override void SetUInt64(int index, UInt64 value)
		{
			// If the source value cannot fit into the destination throw exception
			if (value < UInt32.MinValue || value > UInt32.MaxValue)
				throw new DicomDataException(String.Format("Invalid AT value {0} for tag {1}.", value, Tag));

			SetValue(index, (uint) value);
		}

		/// <summary>
		/// Sets an AT value.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit unsigned int</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		public override void SetInt16(int index, Int16 value)
		{
			// If the source value cannot fit into the destination throw exception
			if (value < UInt32.MinValue)
				throw new DicomDataException(String.Format("Invalid AT value {0} for tag {1}.", value, Tag));

			SetValue(index, (uint) value);
		}

		/// <summary>
		/// Sets an AT value.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit unsigned int</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		public override void SetInt32(int index, Int32 value)
		{
			// If the source value cannot fit into the destination throw exception
			if (value < UInt32.MinValue)
				throw new DicomDataException(String.Format("Invalid AT value {0} for tag {1}.", value, Tag));

			SetValue(index, (uint) value);
		}

		/// <summary>
		/// Sets an AT value.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit unsigned int</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		public override void SetInt64(int index, Int64 value)
		{
			// If the source value cannot fit into the destination throw exception
			if (value < UInt32.MinValue || value > UInt32.MaxValue)
				throw new DicomDataException(String.Format("Invalid AT value {0} for tag {1}.", value, Tag));

			SetValue(index, (uint) value);
		}

		/// <summary>
		/// Appends an AT value.
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit unsigned int</exception>
		public override void AppendUInt16(UInt16 value)
		{
			AppendValue(value);
		}

		/// <summary>
		/// Appends an AT value.
		/// </summary>
		/// <param name="value"></param>
		public override void AppendUInt32(UInt32 value)
		{
			// If the source value cannot fit into the destination throw exception
			if (value < uint.MinValue || value > uint.MaxValue)
				throw new DicomDataException(String.Format("Invalid AT value {0} for tag {1}.", value, Tag));

			AppendValue(value);
		}

		/// <summary>
		/// Appends an AT value.
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit unsigned int</exception>
		public override void AppendUInt64(UInt64 value)
		{
			// If the source value cannot fit into the destination throw exception
			if (value < UInt32.MinValue || value > UInt32.MaxValue)
				throw new DicomDataException(String.Format("Invalid AT value {0} for tag {1}.", value, Tag));

			AppendValue((UInt32) value);
		}

		/// <summary>
		/// Appends an AT value.
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit unsigned int</exception>
		public override void AppendInt16(Int16 value)
		{
			if (value < UInt32.MinValue)
				throw new DicomDataException(String.Format("Invalid AT value {0} for tag {1}.", value, Tag));

			AppendValue((UInt32) value);
		}

		/// <summary>
		/// Appends an AT value.
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit unsigned int</exception>
		public override void AppendInt32(Int32 value)
		{
			// If the source value cannot fit into the destination throw exception
			if (value < UInt32.MinValue)
				throw new DicomDataException(String.Format("Invalid value {0} for tag {1}.", value, Tag));

			AppendValue((UInt32) value);
		}

		/// <summary>
		/// Appends an AT value.
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit unsigned int</exception>
		public override void AppendInt64(Int64 value)
		{
			// If the source value cannot fit into the destination throw exception
			if (value < UInt32.MinValue || value > UInt32.MaxValue)
				throw new DicomDataException(String.Format("Invalid value {0} for tag {1}.", value, Tag));
			AppendValue((UInt32) value);
		}

		#endregion
	}

	#endregion

	#region DicomElementFd

	/// <summary>
	/// <see cref="DicomElementBinary"/> derived class for storing FD value representation tags.
	/// </summary>
	public class DicomElementFd : DicomElementBinary<double>
	{
		#region Constructors

		public DicomElementFd(uint tag)
			: base(tag) {}

		public DicomElementFd(DicomTag tag)
			: base(tag)
		{
			if (!tag.VR.Equals(DicomVr.FDvr)
			    && !tag.MultiVR)
				throw new DicomException(SR.InvalidVR);
		}

		internal DicomElementFd(DicomTag tag, ByteBuffer item)
			: base(tag, item) {}

		internal DicomElementFd(DicomElementFd attrib)
			: base(attrib) {}

		#endregion

		#region Operators

		#endregion

		#region Abstract Method Implementation

		public override DicomElement Copy()
		{
			return new DicomElementFd(this);
		}

		internal override DicomElement Copy(bool copyBinary)
		{
			return new DicomElementFd(this);
		}

		protected override double ParseNumber(string val, CultureInfo culture)
		{
			if (string.IsNullOrEmpty(val))
				throw new DicomDataException("Null values invalid for FD VR");

			double parseVal;
			if (!double.TryParse(val.Trim(), NumberStyle, culture, out parseVal))
				throw new DicomDataException(String.Format("Invalid double format value for tag {0}: {1}", Tag, val));
			return parseVal;
		}

		protected override string FormatNumber(double val, CultureInfo culture)
		{
			return val.ToString(culture);
		}

		/// <summary>
		/// Sets an FD value.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 32-bit floating-point</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		/// 
		public override void SetFloat32(int index, float value)
		{
			SetValue(index, value);
		}

		/// <summary>
		/// Sets an FD value.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> is null or  cannot be fit into 32-bit floating-point</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		/// 
		public override void SetFloat64(int index, double value)
		{
			SetValue(index, value);
		}

		/// <summary>
		/// Sets an FD value.
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> is null or  cannot be fit into 32-bit floating-point</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		/// 
		public override void AppendFloat32(float value)
		{
			AppendValue(value);
		}

		/// <summary>
		/// Sets an FD value.
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> is null or  cannot be fit into 32-bit floating-point</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		/// 
		public override void AppendFloat64(double value)
		{
			AppendValue(value);
		}

		#endregion

		/// <summary>
		/// Retrieves a float value from an FD element.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		/// <list type="bullet">
		/// <item>If the value doesn't exist</item>
		/// <item>The value is too big or too small to fit into a float (eg, 1E+100)</item>
		/// </list>
		/// If the method returns <b>false</b>, the returned <paramref name="value"/> is not reliable.
		/// </remarks>
		/// 
		public override bool TryGetFloat32(int index, out float value)
		{
			double val;
			if (Data == null || !Data.TryGetValue(index, out val))
			{
				value = 0;
				return false;
			}
			value = (float) val;
			return val >= float.MinValue && val <= float.MaxValue; // this isn't right, the real test should be about precision
		}

		/// <summary>
		/// Retrieves a double value from an FD element.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		/// <list type="bullet">
		/// <item>If the value doesn't exist</item>
		/// </list>
		///  
		/// If the method returns <b>false</b>, the returned <paramref name="value"/> is not reliable.
		/// 
		/// </remarks>
		/// 
		public override bool TryGetFloat64(int index, out double value)
		{
			value = 0;
			return Data != null && Data.TryGetValue(index, out value);
		}
	}

	#endregion

	#region DicomElementFl

	/// <summary>
	/// <see cref="DicomElementBinary"/> derived class for storing FL value representation tags.
	/// </summary>
	public class DicomElementFl : DicomElementBinary<float>
	{
		#region Constructors

		public DicomElementFl(uint tag)
			: base(tag) {}

		public DicomElementFl(DicomTag tag)
			: base(tag)
		{
			if (!tag.VR.Equals(DicomVr.FLvr)
			    && !tag.MultiVR)
				throw new DicomException(SR.InvalidVR);
		}

		internal DicomElementFl(DicomTag tag, ByteBuffer item)
			: base(tag, item) {}

		internal DicomElementFl(DicomElementFl attrib)
			: base(attrib) {}

		#endregion

		#region Operators

		#endregion

		#region Abstract Method Implementation

		public override DicomElement Copy()
		{
			return new DicomElementFl(this);
		}

		internal override DicomElement Copy(bool copyBinary)
		{
			return new DicomElementFl(this);
		}

		protected override float ParseNumber(string val, CultureInfo culture)
		{
			if (string.IsNullOrEmpty(val))
				throw new DicomDataException("Null values invalid for FL VR");

			float parseVal;
			if (!float.TryParse(val.Trim(), NumberStyle, culture, out parseVal))
				throw new DicomDataException(String.Format("Invalid float format value for tag {0}: {1}", Tag, val));
			return parseVal;
		}

		protected override string FormatNumber(float val, CultureInfo culture)
		{
			return val.ToString(culture);
		}

		/// <summary>
		/// Retrieves a float value from an FL element.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		/// <list>
		/// <item>If the value doesn't exist</item>
		/// <item>The value is infinite</item>
		/// </list>
		///     
		/// If the method returns false, the returned <paramref name="value"/> is not reliable.
		/// 
		/// </remarks>
		/// 
		public override bool TryGetFloat32(int index, out float value)
		{
			value = 0;
			return Data != null && Data.TryGetValue(index, out value) && (!float.IsInfinity(value) && !float.IsNaN(value));
		}

		/// <summary>
		/// Retrieves a double value from an FL element.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// <list>
		/// <item>If the value doesn't exist</item>
		/// </list>
		/// 
		/// If the method returns false, the returned <paramref name="value"/> is not reliable.
		/// 
		/// </remarks>
		/// 
		public override bool TryGetFloat64(int index, out double value)
		{
			float val;
			if (Data == null || !Data.TryGetValue(index, out val))
			{
				value = 0;
				return false;
			}
			value = (double) (decimal) val; // casting to decimal then double seems to prevent precision loss
			return !float.IsInfinity(val) && !float.IsNaN(val);
		}

		/// <summary>
		/// Sets an FL value.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 32-bit floating-point</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		/// 
		public override void SetFloat32(int index, float value)
		{
			SetValue(index, value);
		}

		/// <summary>
		/// Sets an FL value.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 32-bit floating-point</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		/// 
		public override void SetFloat64(int index, double value)
		{
			if (value < float.MinValue || value > float.MaxValue)
				throw new DicomDataException(String.Format("Invalid FL value {0} for tag {1}", value, Tag));

			SetValue(index, (float) value);
		}

		/// <summary>
		/// Appendss an FL value.
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 32-bit floating-point</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		/// 
		public override void AppendFloat32(float value)
		{
			AppendValue(value);
		}

		/// <summary>
		/// Appends an FL value.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> is null or  cannot be fit into 32-bit floating-point</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		/// 
		public override void AppendFloat64(double value)
		{
			if (value < float.MinValue || value > float.MaxValue)
				throw new DicomDataException(String.Format("Invalid FL value {0} for tag {1}", value, Tag));

			AppendValue((float) value);
		}

		#endregion
	}

	#endregion

	#region DicomElementOb

	/// <summary>
	/// <see cref="DicomElementBinary"/> derived class for storing OB value representation tags.
	/// </summary>
	public class DicomElementOb : DicomElementBinary<byte>
	{
		#region Constructors

		public DicomElementOb(uint tag)
			: base(tag) {}

		public DicomElementOb(DicomTag tag)
			: base(tag)
		{
			if (!tag.VR.Equals(DicomVr.OBvr)
			    && !tag.MultiVR)
				throw new DicomException(SR.InvalidVR);
		}

		internal DicomElementOb(DicomTag tag, ByteBuffer item)
			: base(tag, item) {}

		internal DicomElementOb(DicomElementOb attrib)
			: base(attrib) {}

		internal DicomElementOb(DicomTag tag, FileReference reference)
			: base(tag, reference) {}

		#endregion

		#region Abstract Method Implementation

		public override DicomElement Copy()
		{
			return new DicomElementOb(this);
		}

		internal override DicomElement Copy(bool copyBinary)
		{
			return new DicomElementOb(this);
		}

		protected override bool SetValuesCore(object value)
		{
			var values = value as byte[];
			if (values != null)
			{
				Data = new DicomElementBinaryData<byte>(values);
				return true;
			}
			return false;
		}

		protected override byte ParseNumber(string val, CultureInfo culture)
		{
			if (string.IsNullOrEmpty(val))
				throw new DicomDataException("Null values invalid for OB VR");

			byte parseVal;
			if (! byte.TryParse(val.Trim(), NumberStyle, culture, out parseVal))
				throw new DicomDataException(String.Format("Invalid byte format value for tag {0}: {1}", Tag, val));
			return parseVal;
		}

		protected override string FormatNumber(byte val, CultureInfo culture)
		{
			return val.ToString(culture);
		}

		public override int GetHashCode()
		{
			// fixed value so that OB and OW attributes can be considered equal if their contents are the same
			return -0x5375F220;
		}

		public override bool Equals(object obj)
		{
			// OB and OW attributes are to be considered equal if their contents are the same
			// check for null and compare run-time types.
			if (obj == null || (GetType() != obj.GetType() && !(obj is DicomElementOw))) return false;

			var other = (DicomElementBinary<byte>) obj;

			// if both are null or both are empty, consider it equal
			if ((IsNull && other.IsNull) || (IsEmpty && other.IsEmpty)) return true;

			// if exactly one is null or exactly one is empty, consider it unequal
			if ((IsNull ^ other.IsNull) || (IsEmpty ^ other.IsEmpty)) return false;

			// same non-null file reference means data will be exactly the same
			if (!ReferenceEquals(Reference, null) && ReferenceEquals(Reference, other.Reference)) return true;

			// accessing Data property will force the values to be loaded from disk, if only a reference is stored
			return Data.CompareValues(other.Data);
		}

		internal override uint CalculateWriteLength(TransferSyntax syntax, DicomWriteOptions options, DicomElementBinaryData<byte> values)
		{
			uint length = 0;
			length += 4; // element tag
			if (syntax.ExplicitVr)
			{
				length += 2; // vr
				length += 6; // length
			}
			else
			{
				length += 4; // length
			}
			if (values != null)
			{
				length += (uint) values.Length;
			}
			return length;
		}

		#endregion
	}

	#endregion

	#region DicomElementOf

	/// <summary>
	/// <see cref="DicomElementBinary"/> derived class for storing OF value representation tags.
	/// </summary>
	public class DicomElementOf : DicomElementBinary<float>
	{
		public DicomElementOf(uint tag)
			: base(tag) {}

		public DicomElementOf(DicomTag tag)
			: base(tag)
		{
			if (!tag.VR.Equals(DicomVr.OFvr)
			    && !tag.MultiVR)
				throw new DicomException(SR.InvalidVR);
		}

		internal DicomElementOf(DicomTag tag, ByteBuffer item)
			: base(tag, item) {}

		internal DicomElementOf(DicomElementOf attrib)
			: base(attrib) {}

		internal DicomElementOf(DicomTag tag, FileReference reference)
			: base(tag, reference) {}

		#region Abstract Method Implementation

		public override string ToString()
		{
			return Tag + " of length " + base.StreamLength;
		}

		public override DicomElement Copy()
		{
			return new DicomElementOf(this);
		}

		internal override DicomElement Copy(bool copyBinary)
		{
			return new DicomElementOf(this);
		}

		protected override bool SetValuesCore(object value)
		{
			var values = value as float[];
			if (values != null)
			{
				Data = new DicomElementBinaryData<float>(values);
				return true;
			}
			return false;
		}

		protected override float ParseNumber(string val, CultureInfo culture)
		{
			if (string.IsNullOrEmpty(val))
				throw new DicomDataException("Null values invalid for OF VR");

			float parseVal;
			if (!float.TryParse(val.Trim(), NumberStyle, culture, out parseVal))
				throw new DicomDataException(String.Format("Invalid float format value for tag {0}: {1}", Tag, val));
			return parseVal;
		}

		protected override string FormatNumber(float val, CultureInfo culture)
		{
			return val.ToString(culture);
		}

		#endregion
	}

	#endregion

	#region DicomElementOw

	/// <summary>
	/// <see cref="DicomElementBinary"/> derived class for storing OW value representation tags.
	/// </summary>
	public class DicomElementOw : DicomElementBinary<byte>
	{
		public DicomElementOw(uint tag)
			: base(tag) {}

		public DicomElementOw(DicomTag tag)
			: base(tag)
		{
			if (!tag.VR.Equals(DicomVr.OWvr)
			    && !tag.MultiVR)
				throw new DicomException(SR.InvalidVR);
		}

		internal DicomElementOw(DicomTag tag, ByteBuffer item)
			: base(tag, item) {}

		internal DicomElementOw(DicomElementOw attrib)
			: base(attrib) {}

		internal DicomElementOw(DicomTag tag, FileReference reference)
			: base(tag, reference) {}

		#region Abstract Method Implementation

		public override string ToString()
		{
			return Tag + " of length " + base.StreamLength;
		}

		public override DicomElement Copy()
		{
			return new DicomElementOw(this);
		}

		internal override DicomElement Copy(bool copyBinary)
		{
			return new DicomElementOw(this);
		}

		protected override bool SetValuesCore(object value)
		{
			if (value is byte[])
			{
				Data = new DicomElementBinaryData<byte>((byte[]) value);
				return true;
			}
			else if (value is ushort[])
			{
				var values = (ushort[]) value;
				var buffer = new byte[values.Length*Tag.VR.UnitSize];
				Buffer.BlockCopy(values, 0, buffer, 0, buffer.Length);
				Data = new DicomElementBinaryData<byte>(buffer, false);
			}
			else if (value is short[])
			{
				var values = (short[]) value;
				var buffer = new byte[values.Length*Tag.VR.UnitSize];
				Buffer.BlockCopy(values, 0, buffer, 0, buffer.Length);
				Data = new DicomElementBinaryData<byte>(buffer, false);
			}
			return false;
		}

		protected override byte ParseNumber(string val, CultureInfo culture)
		{
			if (string.IsNullOrEmpty(val))
				throw new DicomDataException("Null values invalid for OW VR");

			byte parseVal;
			if (!byte.TryParse(val.Trim(), NumberStyle, culture, out parseVal))
				throw new DicomDataException(String.Format("Invalid byte format value for tag {0}: {1}", Tag, val));
			return parseVal;
		}

		protected override string FormatNumber(byte val, CultureInfo culture)
		{
			return val.ToString(culture);
		}

		public override int GetHashCode()
		{
			// fixed value so that OB and OW attributes can be considered equal if their contents are the same
			return -0x5375F220;
		}

		public override bool Equals(object obj)
		{
			// OB and OW attributes are to be considered equal if their contents are the same
			// check for null and compare run-time types.
			if (obj == null || (GetType() != obj.GetType() && !(obj is DicomElementOb))) return false;

			var other = (DicomElementBinary<byte>) obj;

			// if both are null or both are empty, consider it equal
			if ((IsNull && other.IsNull) || (IsEmpty && other.IsEmpty)) return true;

			// if exactly one is null or exactly one is empty, consider it unequal
			if ((IsNull ^ other.IsNull) || (IsEmpty ^ other.IsEmpty)) return false;

			// same non-null file reference means data will be exactly the same
			if (!ReferenceEquals(Reference, null) && ReferenceEquals(Reference, other.Reference)) return true;

			// accessing Data property will force the values to be loaded from disk, if only a reference is stored
			return Data.CompareValues(other.Data);
		}

		#endregion
	}

	#endregion

	#region DicomElementSl

	/// <summary>
	/// <see cref="DicomElementBinary"/> derived class for storing SL value representation tags.
	/// </summary>
	public class DicomElementSl : DicomElementBinary<int>
	{
		public DicomElementSl(uint tag)
			: base(tag) {}

		public DicomElementSl(DicomTag tag)
			: base(tag)
		{
			if (!tag.VR.Equals(DicomVr.SLvr)
			    && !tag.MultiVR)
				throw new DicomException(SR.InvalidVR);
		}

		internal DicomElementSl(DicomTag tag, ByteBuffer item)
			: base(tag, item) {}

		internal DicomElementSl(DicomElementSl attrib)
			: base(attrib) {}

		#region Abstract Method Implementation

		public override DicomElement Copy()
		{
			return new DicomElementSl(this);
		}

		internal override DicomElement Copy(bool copyBinary)
		{
			return new DicomElementSl(this);
		}

		protected override int ParseNumber(string val, CultureInfo culture)
		{
			if (string.IsNullOrEmpty(val))
				throw new DicomDataException("Null values invalid for SL VR");

			int parseVal;
			if (!int.TryParse(val.Trim(), NumberStyle, culture, out parseVal))
				throw new DicomDataException(String.Format("Invalid int format value for tag {0}: {1}", Tag, val));
			return parseVal;
		}

		protected override string FormatNumber(int val, CultureInfo culture)
		{
			return val.ToString(culture);
		}

		/// <summary>
		/// Sets an SL value.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		/// 
		public override void SetInt16(int index, Int16 value)
		{
			SetValue(index, value);
		}

		/// <summary>
		/// Sets an SL value.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		/// 
		public override void SetUInt16(int index, UInt16 value)
		{
			SetValue(index, value);
		}

		/// <summary>
		/// Sets an SL value.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		/// 
		public override void SetInt32(int index, int value)
		{
			SetValue(index, value);
		}

		/// <summary>
		/// Sets an SL value.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 32-bit signed int</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		/// 
		public override void SetUInt32(int index, UInt32 value)
		{
			if (value > int.MaxValue)
				throw new DicomDataException(String.Format("Invalid SL value '{0}' for {1}.", value, Tag));
			SetValue(index, (int) value);
		}

		/// <summary>
		/// Sets an SL value.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 32-bit signed int</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		/// 
		public override void SetInt64(int index, Int64 value)
		{
			if (value < int.MinValue || value > int.MaxValue)
				throw new DicomDataException(String.Format("Invalid SL value '{0}' for {1}.", value, Tag));

			SetValue(index, (int) value);
		}

		/// <summary>
		/// Sets an SL value.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 32-bit signed int</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		/// 
		public override void SetUInt64(int index, UInt64 value)
		{
			if (value > int.MaxValue)
				throw new DicomDataException(String.Format("Invalid SL value '{0}' for {1}.", value, Tag));

			SetValue(index, (int) value);
		}

		/// <summary>
		/// Appends an SL value.
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// 
		public override void AppendInt16(Int16 value)
		{
			AppendValue(value);
		}

		/// <summary>
		/// Appends an SL value.
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 32-bit signed int</exception>
		///
		public override void AppendInt32(int value)
		{
			AppendValue(value);
		}

		/// <summary>
		/// Appends an SL value.
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 32-bit signed int</exception>
		///
		public override void AppendInt64(Int64 value)
		{
			if (value < Int32.MinValue || value > Int32.MaxValue)
				throw new DicomDataException(String.Format("Invalid SL value '{0}' tag {1}.", value, Tag));

			AppendValue((int) value);
		}

		/// <summary>
		/// Appends an SL value.
		/// 
		/// </summary>
		/// <param name="value"></param>
		///
		public override void AppendUInt16(UInt16 value)
		{
			AppendValue(value);
		}

		/// <summary>
		/// Appends an SL value.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 32-bit signed int</exception>
		///
		public override void AppendUInt32(UInt32 value)
		{
			if (value > int.MaxValue)
				throw new DicomDataException(String.Format("Invalid SL value '{0}' for {1}.", value, Tag));

			AppendValue((int) value);
		}

		/// <summary>
		/// Appends an SL value.
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 32-bit signed int</exception>
		///
		public override void AppendUInt64(UInt64 value)
		{
			if (value > Int32.MaxValue)
				throw new DicomDataException(String.Format("Invalid SL value '{0}' for {1}.", value, Tag));

			AppendValue((int) value);
		}

		/// <summary>
		/// Retrieves an Int16 value from an SL element.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		///     If the value doesn't exist
		///     The value cannot be converted into Int16 (eg, floating-point number 1.102 cannot be converted into Int16)
		///     The value is an integer but outside the range of type  Int16 (eg, 100000)
		/// 
		/// If the method returns false, the returned <paramref name="value"/> is not reliable.
		/// 
		/// </remarks>
		/// 
		public override bool TryGetInt16(int index, out Int16 value)
		{
			int val;
			if (Data == null || !Data.TryGetValue(index, out val))
			{
				value = 0;
				return false;
			}
			value = (Int16) val;
			return val >= Int16.MinValue && val <= Int16.MaxValue;
		}

		/// <summary>
		/// Retrieves an Int32 value from an SL element.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		///     If the value doesn't exist
		///     The value cannot be converted into Int32 (eg, floating-point number 1.102 cannot be converted into Int32)
		///     The value is an integer but outside the range of type  Int16 (eg, 100000)
		/// 
		/// If the method returns false, the returned <paramref name="value"/> is not reliable.
		/// 
		/// </remarks>
		/// 
		public override bool TryGetInt32(int index, out Int32 value)
		{
			value = 0;
			return Data != null && Data.TryGetValue(index, out value);
		}

		/// <summary>
		/// Retrieves an Int64 value from an SL element.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		///     If the value doesn't exist
		/// 
		/// If the method returns false, the returned <paramref name="value"/> is not reliable.
		/// 
		/// </remarks>
		/// 
		public override bool TryGetInt64(int index, out Int64 value)
		{
			int val;
			if (Data == null || !Data.TryGetValue(index, out val))
			{
				value = 0;
				return false;
			}
			value = val;
			return true;
		}

		/// <summary>
		/// Retrieves an UInt16 value from an SL element.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		///     If the value doesn't exist
		///     The value cannot be converted into UInt16 (eg, floating-point number 1.102 cannot be converted into UInt16)
		///     The value is an integer but outside the range of type  UInt16 (eg, -100)
		/// 
		/// If the method returns false, the returned <paramref name="value"/> is not reliable.
		/// 
		/// </remarks>
		/// 
		public override bool TryGetUInt16(int index, out UInt16 value)
		{
			int val;
			if (Data == null || !Data.TryGetValue(index, out val))
			{
				value = 0;
				return false;
			}
			value = (UInt16) val;
			return val >= UInt16.MinValue && val <= UInt16.MaxValue;
		}

		/// <summary>
		/// Retrieves an UInt32 value from an SL element.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		///     If the value doesn't exist
		///     The value cannot be converted into UInt32 (eg, floating-point number 1.102 cannot be converted into UInt32)
		///     The value is an integer but outside the range of type  UInt32 (eg, -100)
		/// 
		/// If the method returns false, the returned <paramref name="value"/> is not reliable.
		/// 
		/// </remarks>
		/// 
		public override bool TryGetUInt32(int index, out UInt32 value)
		{
			int val;
			if (Data == null || !Data.TryGetValue(index, out val))
			{
				value = 0;
				return false;
			}
			value = (UInt32) val;
			return val >= UInt32.MinValue;
		}

		/// <summary>
		/// Retrieves an UInt64 value from a SL element.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		///     If the value doesn't exist
		///     The value cannot be converted into UInt64 (eg, floating-point number 1.102 cannot be converted into UInt64)
		///     The value is an integer but outside the range of type  UInt64 (eg, -100)
		/// 
		/// If the method returns false, the returned <paramref name="value"/> is not reliable.
		/// 
		/// </remarks>
		/// 
		public override bool TryGetUInt64(int index, out UInt64 value)
		{
			int val;
			if (Data == null || !Data.TryGetValue(index, out val))
			{
				value = 0;
				return false;
			}
			value = (UInt64) val;
			return val >= (decimal) UInt64.MinValue;
		}

		#endregion
	}

	#endregion

	#region DicomElementSs

	/// <summary>
	/// <see cref="DicomElementBinary"/> derived class for storing SS value representation tags.
	/// </summary>
	public class DicomElementSs : DicomElementBinary<short>
	{
		#region Constructors

		public DicomElementSs(uint tag)
			: base(tag) {}

		public DicomElementSs(DicomTag tag)
			: base(tag)
		{
			if (!tag.VR.Equals(DicomVr.SSvr)
			    && !tag.MultiVR)
				throw new DicomException(SR.InvalidVR);
		}

		internal DicomElementSs(DicomTag tag, ByteBuffer item)
			: base(tag, item) {}

		internal DicomElementSs(DicomElementSs attrib)
			: base(attrib) {}

		#endregion

		#region Operators

		#endregion

		#region Abstract Method Implementation

		public override DicomElement Copy()
		{
			return new DicomElementSs(this);
		}

		internal override DicomElement Copy(bool copyBinary)
		{
			return new DicomElementSs(this);
		}

		protected override short ParseNumber(string val, CultureInfo culture)
		{
			if (string.IsNullOrEmpty(val))
				throw new DicomDataException("Null values invalid for SS VR");

			short parseVal;
			if (!short.TryParse(val.Trim(), NumberStyle, culture, out parseVal))
				throw new DicomDataException(String.Format("Invalid short format value for tag {0}: {1}", Tag, val));
			return parseVal;
		}

		protected override string FormatNumber(short val, CultureInfo culture)
		{
			return val.ToString(culture);
		}

		/// <summary>
		/// Appends an SS value.
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit signed int</exception>
		/// 
		public override void AppendInt16(Int16 value)
		{
			AppendValue(value);
		}

		/// <summary>
		/// Appends an SS value.
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit signed int</exception>
		/// 
		public override void AppendInt32(Int32 value)
		{
			if (value < Int16.MinValue || value > Int16.MaxValue)
				throw new DicomDataException(String.Format("Invalid SS value {0} for tag {1}.", value, Tag));

			AppendValue((short) value);
		}

		/// <summary>
		/// Appends an SS value.
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit signed int</exception>
		/// 
		public override void AppendInt64(Int64 value)
		{
			if (value < Int16.MinValue || value > Int16.MaxValue)
				throw new DicomDataException(String.Format("Invalid SS value {0} for tag {1}.", value, Tag));

			AppendValue((short) value);
		}

		/// <summary>
		/// Appends an SS value.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit signed int</exception>
		/// 
		public override void AppendUInt16(UInt16 value)
		{
			if (value > Int16.MaxValue)
				throw new DicomDataException(String.Format("Invalid SS value {0} for tag {1}.", value, Tag));

			AppendValue((short) value);
		}

		/// <summary>
		/// Appends an SS value.
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit signed int</exception>
		/// 
		public override void AppendUInt32(UInt32 value)
		{
			if (value > Int16.MaxValue)
				throw new DicomDataException(String.Format("Invalid SS value {0} for tag {1}.", value, Tag));

			AppendValue((short) value);
		}

		/// <summary>
		/// Appends an SL value.
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit signed int</exception>
		/// 
		public override void AppendUInt64(UInt64 value)
		{
			if (value > (UInt64) Int16.MaxValue)
				throw new DicomDataException(String.Format("Invalid SS value {0} for tag {1}.", value, Tag));

			AppendValue((short) value);
		}

		/// <summary>
		/// Retrieves an Int16 value from an SS element.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		/// <list type="bullet">
		/// <item>The value doesn't exist</item>
		/// </list>
		///     
		/// If the method returns false, the returned <paramref name="value"/> is not reliable.
		/// 
		/// </remarks>
		/// 
		public override bool TryGetInt16(int index, out Int16 value)
		{
			value = 0;
			return Data != null && Data.TryGetValue(index, out value);
		}

		/// <summary>
		/// Retrieves an Int32 value from an SS element.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		/// <list type="bullet">
		/// <item>The value doesn't exist.</item>
		/// </list>
		///     
		/// If the method returns false, the returned <paramref name="value"/> is not reliable.
		/// 
		/// </remarks>
		/// 
		public override bool TryGetInt32(int index, out Int32 value)
		{
			short val;
			if (Data == null || !Data.TryGetValue(index, out val))
			{
				value = 0;
				return false;
			}
			value = val;
			return true;
		}

		/// <summary>
		/// Retrieves an Int64 value from an SS element.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		/// <list type="bullet">
		/// <item>The value doesn't exist.</item>
		/// </list>
		/// 
		/// If the method returns false, the returned <paramref name="value"/> is not reliable.
		/// 
		/// </remarks>
		/// 
		public override bool TryGetInt64(int index, out Int64 value)
		{
			short val;
			if (Data == null || !Data.TryGetValue(index, out val))
			{
				value = 0;
				return false;
			}
			value = val;
			return true;
		}

		/// <summary>
		/// Retrieves an UInt16 value from an SS element.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		/// <list type="bullet">
		/// <item>The value doesn't exist.</item>
		/// <item>The value exceeds the UInt16 range.</item>
		/// </list>
		/// 
		/// If the method returns false, the returned <paramref name="value"/> is not reliable.
		/// 
		/// </remarks>
		/// 
		public override bool TryGetUInt16(int index, out UInt16 value)
		{
			short val;
			if (Data == null || !Data.TryGetValue(index, out val))
			{
				value = 0;
				return false;
			}
			value = (UInt16) val;
			return val >= UInt16.MinValue;
		}

		/// <summary>
		/// Retrieves an UInt32 value from an SS element.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		/// <list type="bullet">
		/// <item>The value doesn't exist.</item>
		/// <item>The value exceeds the UInt32 range.</item>
		/// </list>
		/// 
		/// If the method returns false, the returned <paramref name="value"/> is not reliable.
		/// 
		/// </remarks>
		/// 
		public override bool TryGetUInt32(int index, out UInt32 value)
		{
			short val;
			if (Data == null || !Data.TryGetValue(index, out val))
			{
				value = 0;
				return false;
			}
			value = (UInt32) val;
			return val >= UInt32.MinValue;
		}

		/// <summary>
		/// Retrieves an UInt64 value from an SS element.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		/// <list type="bullet">
		/// <item>The value doesn't exist.</item>
		/// <item>The value exceeds the UInt64 range.</item>
		/// </list>
		/// 
		/// If the method returns false, the returned <paramref name="value"/> is not reliable.
		/// 
		/// </remarks>
		/// 
		public override bool TryGetUInt64(int index, out UInt64 value)
		{
			short val;
			if (Data == null || !Data.TryGetValue(index, out val))
			{
				value = 0;
				return false;
			}
			value = (UInt64) val;
			return val >= (decimal) UInt64.MinValue;
		}

		/// <summary>
		/// Sets an SS value.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		/// 
		public override void SetInt16(int index, Int16 value)
		{
			SetValue(index, value);
		}

		/// <summary>
		/// Sets an SS value.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit signed int</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		/// 
		public override void SetInt32(int index, Int32 value)
		{
			if (value < Int16.MinValue || value > Int16.MaxValue)
				throw new DicomDataException(String.Format("Invalid SS value {0} for tag {1}.", value, Tag));

			SetValue(index, (short) value);
		}

		/// <summary>
		/// Sets an SS value.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit signed int</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		/// 
		public override void SetInt64(int index, Int64 value)
		{
			if (value < Int16.MinValue || value > Int16.MaxValue)
				throw new DicomDataException(String.Format("Invalid SS value {0} for tag {1}.", value, Tag));

			SetValue(index, (short) value);
		}

		/// <summary>
		/// Sets an SS value.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit signed int</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		/// 
		public override void SetUInt16(int index, UInt16 value)
		{
			if (value > Int16.MaxValue)
				throw new DicomDataException(String.Format("Invalid SS value {0} for tag {1}.", value, Tag));

			SetValue(index, (short) value);
		}

		/// <summary>
		/// Sets an SS value.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit signed int</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		/// 
		public override void SetUInt32(int index, UInt32 value)
		{
			if (value > Int16.MaxValue)
				throw new DicomDataException(String.Format("Invalid SS value {0} for tag {1}.", value, Tag));

			SetValue(index, (short) value);
		}

		/// <summary>
		/// Sets an SS value.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit signed int</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		/// 
		public override void SetUInt64(int index, UInt64 value)
		{
			if (value > (UInt64) Int16.MaxValue)
				throw new DicomDataException(String.Format("Invalid SS value {0} for tag {1}.", value, Tag));

			SetValue(index, (short) value);
		}

		#endregion
	}

	#endregion

	#region DicomElementUl

	/// <summary>
	/// <see cref="DicomElementBinary"/> derived class for storing UL value representation tags.
	/// </summary>
	public class DicomElementUl : DicomElementBinary<uint>
	{
		#region Constructors

		public DicomElementUl(uint tag)
			: base(tag) {}

		public DicomElementUl(DicomTag tag)
			: base(tag)
		{
			if (!tag.VR.Equals(DicomVr.ULvr)
			    && !tag.MultiVR)
				throw new DicomException(SR.InvalidVR);
		}

		internal DicomElementUl(DicomTag tag, ByteBuffer item)
			: base(tag, item) {}

		internal DicomElementUl(DicomElementUl attrib)
			: base(attrib) {}

		#endregion

		#region Operators

		#endregion

		#region Abstract Method Implementation

		public override DicomElement Copy()
		{
			return new DicomElementUl(this);
		}

		internal override DicomElement Copy(bool copyBinary)
		{
			return new DicomElementUl(this);
		}

		protected override uint ParseNumber(string val, CultureInfo culture)
		{
			if (string.IsNullOrEmpty(val))
				throw new DicomDataException("Null values invalid for UL VR");

			uint parseVal;
			if (! uint.TryParse(val.Trim(), NumberStyle, culture, out parseVal))
				throw new DicomDataException(String.Format("Invalid uint format value for tag {0}: {1}", Tag, val));
			return parseVal;
		}

		protected override string FormatNumber(uint val, CultureInfo culture)
		{
			return val.ToString(culture);
		}

		/// <summary>
		/// Appends an UL value.
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 32-bit unsigned int</exception>
		/// 
		public override void AppendInt16(Int16 value)
		{
			if (value < uint.MinValue)
				throw new DicomDataException(String.Format("Invalid UL value '{0}' for {1}.", value, Tag));
			AppendValue((uint) value);
		}

		/// <summary>
		/// Appends an UL value.
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 32-bit unsigned int</exception>
		/// 
		public override void AppendInt32(Int32 value)
		{
			if (value < uint.MinValue)
				throw new DicomDataException(String.Format("Invalid UL value '{0}' for {1}.", value, Tag));

			AppendValue((uint) value);
		}

		/// <summary>
		/// Appends an UL value.
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 32-bit unsigned int</exception>
		/// 
		public override void AppendInt64(Int64 value)
		{
			if (value < uint.MinValue || value > uint.MaxValue)
				throw new DicomDataException(String.Format("Invalid UL value '{0}' for {1}.", value, Tag));

			AppendValue((uint) value);
		}

		/// <summary>
		/// Appends an UL value.
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// 
		public override void AppendUInt16(UInt16 value)
		{
			AppendValue(value);
		}

		/// <summary>
		/// Appends an UL value.
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// 
		public override void AppendUInt32(UInt32 value)
		{
			AppendValue(value);
		}

		/// <summary>
		/// Appends an UL value.
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 32-bit unsigned int</exception>
		/// 
		public override void AppendUInt64(UInt64 value)
		{
			if (value < uint.MinValue || value > uint.MaxValue)
				throw new DicomDataException(String.Format("Invalid UL value '{0}' for {1}.", value, Tag));

			AppendValue((uint) value);
		}

		/// <summary>
		/// Sets an UL value.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 32-bit unsigned int</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		/// 
		public override void SetInt16(int index, Int16 value)
		{
			if (value < uint.MinValue)
				throw new DicomDataException(String.Format("Invalid UL value '{0}' for {1}.", value, Tag));

			SetValue(index, (uint) value);
		}

		/// <summary>
		/// Sets an UL value.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 32-bit unsigned int</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		/// 
		public override void SetInt32(int index, Int32 value)
		{
			if (value < uint.MinValue)
				throw new DicomDataException(String.Format("Invalid UL value '{0}' for {1}.", value, Tag));

			SetValue(index, (uint) value);
		}

		/// <summary>
		/// Sets an UL value.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 32-bit unsigned int</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		/// 
		public override void SetInt64(int index, Int64 value)
		{
			if (value < uint.MinValue || value > uint.MaxValue)
				throw new DicomDataException(String.Format("Invalid UL value '{0}' for {1}.", value, Tag));
			SetValue(index, (uint) value);
		}

		/// <summary>
		/// Sets an UL value.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		/// 
		public override void SetUInt16(int index, UInt16 value)
		{
			SetValue(index, value);
		}

		/// <summary>
		/// Sets an UL value.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		/// 
		public override void SetUInt32(int index, UInt32 value)
		{
			SetValue(index, value);
		}

		/// <summary>
		/// Sets an UL value.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 32-bit unsigned int</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		/// 
		public override void SetUInt64(int index, UInt64 value)
		{
			if (value > uint.MaxValue)
				throw new DicomDataException(String.Format("Invalid UL value '{0}' for {1}.", value, Tag));
			SetValue(index, (uint) value);
		}

		/// <summary>
		/// Retrieves an Int16 value from an UL element.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		/// <list type="bullet">
		/// <item>The value doesn't exist.</item>
		/// <item>The value exceeds the Int16 range</item>
		/// </list>
		/// 
		/// If the method returns false, the returned <paramref name="value"/> is not reliable.
		/// 
		/// </remarks>
		/// 
		public override bool TryGetInt16(int index, out Int16 value)
		{
			uint val;
			if (Data == null || !Data.TryGetValue(index, out val))
			{
				value = 0;
				return false;
			}
			value = (Int16) val;
			return val <= Int16.MaxValue;
		}

		/// <summary>
		/// Retrieves an Int32 value from an UL element.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		/// <list type="bullet">
		/// <item>The value doesn't exist.</item>
		/// <item>The value exceeds the Int32 range</item>
		/// </list>
		/// 
		/// If the method returns false, the returned <paramref name="value"/> is not reliable.
		/// 
		/// </remarks>
		/// 
		public override bool TryGetInt32(int index, out Int32 value)
		{
			uint val;
			if (Data == null || !Data.TryGetValue(index, out val))
			{
				value = 0;
				return false;
			}
			value = (Int32) val;
			return val <= Int32.MaxValue;
		}

		/// <summary>
		/// Retrieves an Int64 value from an UL element.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		/// <list type="bullet">
		/// <item>The value doesn't exist.</item>
		/// </list>
		/// 
		/// If the method returns false, the returned <paramref name="value"/> is not reliable.
		/// 
		/// </remarks>
		/// 
		public override bool TryGetInt64(int index, out Int64 value)
		{
			uint val;
			if (Data == null || !Data.TryGetValue(index, out val))
			{
				value = 0;
				return false;
			}
			value = val;
			return true;
		}

		/// <summary>
		/// Retrieves an UInt16 value from an UL element.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		/// <list type="bullet">
		/// <item>The value doesn't exist.</item>
		/// </list>
		/// If the method returns false, the returned <paramref name="value"/> is not reliable.
		/// 
		/// </remarks>
		/// 
		public override bool TryGetUInt16(int index, out UInt16 value)
		{
			uint val;
			if (Data == null || !Data.TryGetValue(index, out val))
			{
				value = 0;
				return false;
			}
			value = (UInt16) val;
			return val <= UInt16.MaxValue;
		}

		/// <summary>
		/// Retrieves an UInt32 value from an UL element.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		/// <list type="bullet">
		/// <item>The value doesn't exist.</item>
		/// </list>
		/// 
		/// If the method returns false, the returned <paramref name="value"/> is not reliable.
		/// 
		/// </remarks>
		/// 
		public override bool TryGetUInt32(int index, out UInt32 value)
		{
			value = 0;
			return Data != null && Data.TryGetValue(index, out value);
		}

		/// <summary>
		/// Retrieves an UInt64 value from an UL element.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		/// <list type="bullet">
		/// <item>The value doesn't exist.</item>
		/// </list>
		/// 
		/// If the method returns false, the returned <paramref name="value"/> is not reliable.
		/// 
		/// </remarks>
		/// 
		public override bool TryGetUInt64(int index, out UInt64 value)
		{
			uint val;
			if (Data == null || !Data.TryGetValue(index, out val))
			{
				value = 0;
				return false;
			}
			value = val;
			return true;
		}

		#endregion
	}

	#endregion

	#region DicomElementUn

	/// <summary>
	/// <see cref="DicomElementBinary"/> derived class for storing UN value representation tags.
	/// </summary>
	public class DicomElementUn : DicomElementBinary<byte>
	{
		#region Constructors

		public DicomElementUn(uint tag)
			: base(tag) {}

		public DicomElementUn(DicomTag tag)
			: base(tag) {}

		internal DicomElementUn(DicomTag tag, ByteBuffer item)
			: base(tag)
		{
			Data = new DicomElementBinaryData<byte>(item);
		}

		internal DicomElementUn(DicomElementUn attrib)
			: base(attrib) {}

		#endregion

		#region Abstract Method Implementation

		public override string ToString()
		{
			return Tag; // TODO
		}

		public override DicomElement Copy()
		{
			return new DicomElementUn(this);
		}

		internal override DicomElement Copy(bool copyBinary)
		{
			return new DicomElementUn(this);
		}

		protected override bool SetValuesCore(object value)
		{
			var values = value as byte[];
			if (values != null)
			{
				Data = new DicomElementBinaryData<byte>(values);
				return true;
			}
			return false;
		}

		protected override byte ParseNumber(string val, CultureInfo culture)
		{
			if (string.IsNullOrEmpty(val))
				throw new DicomDataException("Null values invalid for UN VR");

			byte parseVal;
			if (!byte.TryParse(val.Trim(), NumberStyle, culture, out parseVal))
				throw new DicomDataException(String.Format("Invalid byte format value for tag {0}: {1}", Tag, val));
			return parseVal;
		}

		protected override string FormatNumber(byte val, CultureInfo culture)
		{
			return val.ToString(culture);
		}

		internal override ByteBuffer GetByteBuffer(TransferSyntax syntax, String specificCharacterSet)
		{
			ByteBuffer bb;
			if (Reference != null)
			{
				using (FileStream fs = File.OpenRead(Reference.Filename))
				{
					fs.Seek(Reference.Offset, SeekOrigin.Begin);

					bb = new ByteBuffer(Reference.Length);
					bb.CopyFrom(fs, (int) Reference.Length);
					fs.Close();
				}
			}
			else if (Data != null)
			{
				bb = Data.CreateByteBuffer(syntax.Endian);
			}
			else
			{
				bb = new ByteBuffer(syntax.Endian);
			}
			return bb;
		}

		#endregion
	}

	#endregion

	#region DicomElementUs

	/// <summary>
	/// <see cref="DicomElementBinary"/> derived class for storing US value representation tags.
	/// </summary>
	public class DicomElementUs : DicomElementBinary<ushort>
	{
		#region Constructors

		public DicomElementUs(uint tag)
			: base(tag) {}

		public DicomElementUs(DicomTag tag)
			: base(tag)
		{
			if (!tag.VR.Equals(DicomVr.USvr)
			    && !tag.MultiVR)
				throw new DicomException(SR.InvalidVR);
		}

		internal DicomElementUs(DicomTag tag, ByteBuffer item)
			: base(tag, item) {}

		internal DicomElementUs(DicomElementUs attrib)
			: base(attrib) {}

		#endregion

		#region Abstract Method Implementation

		public override DicomElement Copy()
		{
			return new DicomElementUs(this);
		}

		internal override DicomElement Copy(bool copyBinary)
		{
			return new DicomElementUs(this);
		}

		protected override ushort ParseNumber(string val, CultureInfo culture)
		{
			if (string.IsNullOrEmpty(val))
				throw new DicomDataException("Null values invalid for US VR");

			ushort parseVal;
			if (!ushort.TryParse(val.Trim(), NumberStyle, culture, out parseVal))
				throw new DicomDataException(String.Format("Invalid ushort format value for tag {0}: {1}", Tag, val));
			return parseVal;
		}

		protected override string FormatNumber(ushort val, CultureInfo culture)
		{
			return val.ToString(culture);
		}

		/// <summary>
		/// Sets an US value.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit unsigned int</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		public override void SetInt16(int index, short value)
		{
			if (value < UInt16.MinValue)
				throw new DicomDataException(String.Format("Invalid US value '{0}' for tag {1}.", value, Tag));
			SetValue(index, (ushort) value);
		}

		/// <summary>
		/// Sets an US value.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit unsigned int</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		public override void SetInt32(int index, int value)
		{
			if (value < UInt16.MinValue || value > UInt16.MaxValue)
				throw new DicomDataException(String.Format("Invalid US value '{0}' for tag {1}.", value, Tag));
			SetValue(index, (ushort) value);
		}

		/// <summary>
		/// Sets an US value.
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit unsigned int</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		/// 
		public override void SetInt64(int index, Int64 value)
		{
			if (value < UInt16.MinValue || value > UInt16.MaxValue)
				throw new DicomDataException(String.Format("Invalid US value '{0}' for tag {1}.", value, Tag));
			SetValue(index, (ushort) value);
		}

		/// <summary>
		/// Sets an US value.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		public override void SetUInt16(int index, UInt16 value)
		{
			SetValue(index, value);
		}

		/// <summary>
		/// Sets an US value.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit unsigned int</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		public override void SetUInt32(int index, uint value)
		{
			if (value < UInt16.MinValue || value > UInt16.MaxValue)
				throw new DicomDataException(String.Format("Invalid US value '{0}' for tag {1}.", value, Tag));
			SetValue(index, (ushort) value);
		}

		/// <summary>
		/// Sets an US value.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit unsigned int</exception>
		/// <exception cref="IndexOutofBoundException">if <paramref name="index"/> is negative or greater than <seealso cref="Count"/></exception>
		public override void SetUInt64(int index, UInt64 value)
		{
			if (value < UInt16.MinValue || value > UInt16.MaxValue)
				throw new DicomDataException(String.Format("Invalid US value '{0}' for tag {1}.", value, Tag));
			SetValue(index, (ushort) value);
		}

		/// <summary>
		/// Appends an US value.
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit unsigned int</exception>
		public override void AppendInt16(Int16 value)
		{
			if (value < UInt16.MinValue)
				throw new DicomDataException(String.Format("Invalid US value '{0}' for tag {1}.", value, Tag));
			AppendValue((UInt16) value);
		}

		/// <summary>
		/// Appends an US value.
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit unsigned int</exception>
		public override void AppendInt32(Int32 value)
		{
			if (value < UInt16.MinValue || value > UInt16.MaxValue)
				throw new DicomDataException(String.Format("Invalid US value '{0}' for tag {1}.", value, Tag));

			AppendValue((UInt16) value);
		}

		/// <summary>
		/// Appends an US value.
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit unsigned int</exception>
		public override void AppendInt64(Int64 value)
		{
			if (value < UInt16.MinValue || value > UInt16.MaxValue)
				throw new DicomDataException(String.Format("Invalid US value '{0}' for tag {1}.", value, Tag));
			AppendValue((UInt16) value);
		}

		/// <summary>
		/// Appends an US value.
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit unsigned int</exception>
		public override void AppendUInt16(UInt16 value)
		{
			if (value < UInt16.MinValue || value > UInt16.MaxValue)
				throw new DicomDataException(String.Format("Invalid US value '{0}' for tag {1}.", value, Tag));
			AppendValue(value);
		}

		/// <summary>
		/// Appends an US value.
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit unsigned int</exception>
		public override void AppendUInt32(UInt32 value)
		{
			if (value < UInt16.MinValue || value > UInt16.MaxValue)
				throw new DicomDataException(String.Format("Invalid US value '{0}' for tag {1}.", value, Tag));
			AppendValue((UInt16) value);
		}

		/// <summary>
		/// Appends an US value.
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="DicomDataException">If <paramref name="value"/> cannot be fit into 16-bit unsigned int</exception>
		public override void AppendUInt64(UInt64 value)
		{
			if (value < UInt16.MinValue || value > UInt16.MaxValue)
				throw new DicomDataException(String.Format("Invalid US value '{0}' for tag {1}.", value, Tag));
			AppendValue((UInt16) value);
		}

		/// <summary>
		/// Retrieves an Int16 value from an US element.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// <list type="bullet">
		/// <item>The value doesn't exist.</item>
		/// <item>The value exceeds the Int16 range</item>
		/// </list>
		/// 
		/// If the method returns false, the returned <paramref name="value"/> is not reliable.
		/// </remarks>
		public override bool TryGetInt16(int index, out short value)
		{
			ushort val;
			if (Data == null || !Data.TryGetValue(index, out val))
			{
				value = 0;
				return false;
			}
			value = (Int16) val;
			return val <= Int16.MaxValue;
		}

		/// <summary>
		/// Retrieves an Int32 value from an US element.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		/// <list type="bullet">
		/// <item>The value doesn't exist.</item>
		/// </list>
		/// 
		/// If the method returns false, the returned <paramref name="value"/> is not reliable.
		/// </remarks>
		public override bool TryGetInt32(int index, out Int32 value)
		{
			ushort val;
			if (Data == null || !Data.TryGetValue(index, out val))
			{
				value = 0;
				return false;
			}
			value = val;
			return true;
		}

		/// <summary>
		/// Retrieves an Int64 value from an US element.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		/// <list type="bullet">
		/// <item>The value doesn't exist.</item>
		/// </list>
		/// 
		/// If the method returns false, the returned <paramref name="value"/> is not reliable.
		/// </remarks>
		public override bool TryGetInt64(int index, out Int64 value)
		{
			ushort val;
			if (Data == null || !Data.TryGetValue(index, out val))
			{
				value = 0;
				return false;
			}
			value = val;
			return true;
		}

		/// <summary>
		/// Retrieves an UInt16 value from an US element.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		/// <list type="bullet">
		/// <item>The value doesn't exist.</item>
		/// </list>
		/// 
		/// If the method returns false, the returned <paramref name="value"/> is not reliable.
		/// </remarks>
		public override bool TryGetUInt16(int index, out ushort value)
		{
			value = 0;
			return Data != null && Data.TryGetValue(index, out value);
		}

		/// <summary>
		/// Retrieves an UInt32 value from an US element.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		/// <list type="bullet">
		/// <item>The value doesn't exist.</item>
		/// </list>
		/// 
		/// If the method returns false, the returned <paramref name="value"/> is not reliable.
		/// </remarks>
		public override bool TryGetUInt32(int index, out UInt32 value)
		{
			ushort val;
			if (Data == null || !Data.TryGetValue(index, out val))
			{
				value = 0;
				return false;
			}
			value = val;
			return true;
		}

		/// <summary>
		/// Retrieves an UInt64 value from an US element.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		/// <returns><b>true</b>if value can be retrieved. <b>false</b> otherwise (see remarks)</returns>
		/// <remarks>
		/// This method returns <b>false</b> if
		/// <list type="bullet">
		/// <item>The value doesn't exist.</item>
		/// </list>
		/// 
		/// If the method returns false, the returned <paramref name="value"/> is not reliable.
		/// </remarks>
		public override bool TryGetUInt64(int index, out UInt64 value)
		{
			ushort val;
			if (Data == null || !Data.TryGetValue(index, out val))
			{
				value = 0;
				return false;
			}
			value = val;
			return true;
		}

		#endregion
	}

	#endregion
}
