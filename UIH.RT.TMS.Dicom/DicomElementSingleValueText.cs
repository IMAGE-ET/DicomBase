/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: DicomElementSingleValueText.cs
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
using UIH.RT.TMS.Dicom.IO;
using UIH.RT.TMS.Dicom;

namespace UIH.RT.TMS.Dicom
{
    #region DicomElementSingleValueText
    /// <summary>
    /// <see cref="DicomElement"/> derived class for storing single value text value representation attributes.
    /// </summary>
    public abstract class DicomElementSingleValueText : DicomElement
    {
        private String _value = null;

        #region Constructors
        internal DicomElementSingleValueText(uint tag)
            : base(tag)
        {

        }

        internal DicomElementSingleValueText(DicomTag tag)
            : base(tag)
        {

        }

        internal DicomElementSingleValueText(DicomTag tag, ByteBuffer item)
            : base(tag)
        {
            _value = item.GetString();

            // Saw some Osirix images that had padding on SH attributes with a null character, just
            // pull them out here.
            _value = _value.Trim(new char[] { tag.VR.PadChar, '\0' });

            Count = 1;
            StreamLength = (uint)_value.Length;
        }

        internal DicomElementSingleValueText(DicomElementSingleValueText attrib)
            : base(attrib)
        {
			string value = attrib.Values as string;
			if (value != null)
				_value = String.Copy(value);
        }
        #endregion

        #region Abstract Method Implementation
        public override void SetNullValue()
        {
            _value = "";
            base.StreamLength = 0;
            base.Count = 1;
        }

		public override void SetEmptyValue()
		{
			_value = null;
			base.StreamLength = 0;
			base.Count = 0;
		}

        /// <summary>
        /// The StreamLength of the element.
        /// </summary>
        public override uint StreamLength
        {
            get
            {
                if (IsNull || IsEmpty)
                {
                    return 0;
                }

                if (ParentCollection!=null && ParentCollection.SpecificCharacterSet != null)
                {
                    return (uint)GetByteBuffer(TransferSyntax.ExplicitVrBigEndian, ParentCollection.SpecificCharacterSet).Length;
                }
                return base.StreamLength;
            }
        }

        public override bool TryGetString(int i, out String value)
        {
            if (i == 0)
            {
                value = _value;
                return true;
            }
            value = "";
            return false;
        }

        public override string ToString()
        {
            if (_value == null)
                return "";

            return _value;
        }

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if (obj == null || GetType() != obj.GetType()) return false;

            DicomElement a = (DicomElement)obj;
        	return Object.Equals(a.Values, _value);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override Type GetValueType()
        {
            return typeof(string);
        }

        public override bool IsNull
        {
            get
            {
                if ((_value != null) && (_value.Length == 0))
                    return true;
                return false;
            }
        }
        public override bool IsEmpty
        {
            get
            {
                if ((Count == 0) && (_value == null))
                    return true;
                return false;
            }
        }

        public override Object Values
        {
            get { return _value; }
            set
            {
                if (value is String)
                {
                    SetStringValue((string)value);
                }
                else
                {
                    throw new DicomException(SR.InvalidType);
                }
            }
        }

        public override void SetStringValue(String stringValue)
        {
            if (stringValue == null || stringValue.Length == 0)
            {
                Count = 1;
                StreamLength = 0;
                _value = "";
                return;
            }

            _value = stringValue;

            Count = 1;
            StreamLength = (uint)_value.Length;
        }

        public abstract override DicomElement Copy();
        internal abstract override DicomElement Copy(bool copyBinary);

        internal override ByteBuffer GetByteBuffer(TransferSyntax syntax, String specificCharacterSet)
        {
            ByteBuffer bb = new ByteBuffer(syntax.Endian);
           if (Tag.VR.SpecificCharacterSet)
                bb.SpecificCharacterSet = specificCharacterSet;
            
            //if (_value == null)
            //{
            //    return bb; // return empty buffer if the value is not set
            //}
            
            bb.SetString(_value, (byte)' ');
            return bb;
        }

        #endregion
    }
    #endregion

    #region DicomElementLt
    /// <summary>
    /// <see cref="DicomElementSingleValueText"/> derived class for storing LT value representation attributes.
    /// </summary>
    public class DicomElementLt : DicomElementSingleValueText
    {
        #region Constructors

        public DicomElementLt(uint tag)
            : base(tag)
        {

        }

        public DicomElementLt(DicomTag tag)
            : base(tag)
        {
            if (!tag.VR.Equals(DicomVr.LTvr)
             && !tag.MultiVR)
                throw new DicomException(SR.InvalidVR);

        }

        internal DicomElementLt(DicomTag tag, ByteBuffer item)
            : base(tag, item)
        {
        }

        internal DicomElementLt(DicomElementLt attrib)
            : base(attrib)
        {
        }

        #endregion

        public override DicomElement Copy()
        {
            return new DicomElementLt(this);
        }

        internal override DicomElement Copy(bool copyBinary)
        {
            return new DicomElementLt(this);
        }

    }
    #endregion

    #region DicomElementSt
    /// <summary>
    /// <see cref="DicomElementSingleValueText"/> derived class for storing ST value representation attributes.
    /// </summary>
    public class DicomElementSt : DicomElementSingleValueText
    {
        #region Constructors

        public DicomElementSt(uint tag)
            : base(tag)
        {

        }

        public DicomElementSt(DicomTag tag)
            : base(tag)
        {
            if (!tag.VR.Equals(DicomVr.STvr)
             && !tag.MultiVR)
                throw new DicomException(SR.InvalidVR);

        }

        internal DicomElementSt(DicomTag tag, ByteBuffer item)
            : base(tag, item)
        {
        }

        internal DicomElementSt(DicomElementSt attrib)
            : base(attrib)
        {
        }

        #endregion

        public override DicomElement Copy()
        {
            return new DicomElementSt(this);
        }

        internal override DicomElement Copy(bool copyBinary)
        {
            return new DicomElementSt(this);
        }

    }
    #endregion

    #region DicomElementUt
    /// <summary>
    /// <see cref="DicomElementSingleValueText"/> derived class for storing UT value representation attributes.
    /// </summary>
    public class DicomElementUt : DicomElementSingleValueText
    {
        #region Constructors

        public DicomElementUt(uint tag)
            : base(tag)
        {

        }

        public DicomElementUt(DicomTag tag)
            : base(tag)
        {
            if (!tag.VR.Equals(DicomVr.UTvr)
             && !tag.MultiVR)
                throw new DicomException(SR.InvalidVR);

        }

        internal DicomElementUt(DicomTag tag, ByteBuffer item)
            : base(tag, item)
        {
        }

        internal DicomElementUt(DicomElementUt attrib)
            : base(attrib)
        {

        }

        #endregion

        public override DicomElement Copy()
        {
            return new DicomElementUt(this);
        }

        internal override DicomElement Copy(bool copyBinary)
        {
            return new DicomElementUt(this);
        }

    }
    #endregion
}
