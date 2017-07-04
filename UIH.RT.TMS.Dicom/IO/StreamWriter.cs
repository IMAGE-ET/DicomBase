/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: StreamWriter.cs
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

using System.IO;
using UIH.RT.TMS.Dicom;

namespace UIH.RT.TMS.Dicom.IO
{
    internal enum DicomWriteStatus
    {
        Success,
        UnknownError
    }

    internal class DicomStreamWriter
    {
        #region Private Members
        private const uint UndefinedLength = 0xFFFFFFFF;

        private Stream _stream = null;
        private BinaryWriter _writer = null;
        private TransferSyntax _syntax = null;
        private Endian _endian;

        private ushort _group = 0xffff;
        #endregion

        #region Public Constructors
        public DicomStreamWriter(Stream stream)
        {
            _stream = stream;
            TransferSyntax = TransferSyntax.ExplicitVrLittleEndian;
        }
        #endregion

        #region Public Properties
        public TransferSyntax TransferSyntax
        {
            get { return _syntax; }
            set
            {
                _syntax = value;
                if (_endian != _syntax.Endian || _writer == null)
                {
                    _endian = _syntax.Endian;
                    _writer = EndianBinaryWriter.Create(_stream, _endian);
                }
            }
        }

        #endregion

        public DicomWriteStatus Write(TransferSyntax syntax, DicomDataset dataset, DicomWriteOptions options)
        {
            TransferSyntax = syntax;

            foreach (DicomElement item in dataset)
            {
                if (item.Tag.Element == 0x0000)
                    continue;

                if (item.IsEmpty)
                    continue;

                if (Flags.IsSet(options, DicomWriteOptions.CalculateGroupLengths)
                    && item.Tag.Group != _group && item.Tag.Group <= 0x7fe0)
                {
                    _group = item.Tag.Group;
                    _writer.Write((ushort)_group);
                    _writer.Write((ushort)0x0000);
                    if (_syntax.ExplicitVr)
                    {
                        _writer.Write((byte)'U');
                        _writer.Write((byte)'L');
                        _writer.Write((ushort)4);
                    }
                    else
                    {
                        _writer.Write((uint)4);
                    }
                    _writer.Write((uint)dataset.CalculateGroupWriteLength(_group, _syntax, options));
                }

                _writer.Write((ushort)item.Tag.Group);
                _writer.Write((ushort)item.Tag.Element);

                if (_syntax.ExplicitVr)
                {
                    _writer.Write((byte)item.Tag.VR.Name[0]);
                    _writer.Write((byte)item.Tag.VR.Name[1]);
                }

                if (item is DicomElementSq)
                {
                    DicomElementSq sq = item as DicomElementSq;

                    if (_syntax.ExplicitVr)
                        _writer.Write((ushort)0x0000);

                    if (Flags.IsSet(options, DicomWriteOptions.ExplicitLengthSequence))
                    {
                        int hl = _syntax.ExplicitVr ? 12 : 8;
                        _writer.Write((uint)sq.CalculateWriteLength(_syntax, options & ~DicomWriteOptions.CalculateGroupLengths) - (uint)hl);
                    }
                    else
                    {
                        _writer.Write((uint)UndefinedLength);
                    }

                    foreach (DicomSequenceItem ids in item.Values as DicomSequenceItem[])
                    {
                        _writer.Write((ushort)DicomTag.Item.Group);
                        _writer.Write((ushort)DicomTag.Item.Element);

                        if (Flags.IsSet(options, DicomWriteOptions.ExplicitLengthSequenceItem))
                        {
                            _writer.Write((uint)ids.CalculateWriteLength(_syntax, options & ~DicomWriteOptions.CalculateGroupLengths));
                        }
                        else
                        {
                            _writer.Write((uint)UndefinedLength);
                        }

                        Write(this.TransferSyntax, ids, options & ~DicomWriteOptions.CalculateGroupLengths);

                        if (!Flags.IsSet(options, DicomWriteOptions.ExplicitLengthSequenceItem))
                        {
                            _writer.Write((ushort)DicomTag.ItemDelimitationItem.Group);
                            _writer.Write((ushort)DicomTag.ItemDelimitationItem.Element);
                            _writer.Write((uint)0x00000000);
                        }
                    }

                    if (!Flags.IsSet(options, DicomWriteOptions.ExplicitLengthSequence))
                    {
                        _writer.Write((ushort)DicomTag.SequenceDelimitationItem.Group);
                        _writer.Write((ushort)DicomTag.SequenceDelimitationItem.Element);
                        _writer.Write((uint)0x00000000);
                    }
                }

                else if (item is DicomFragmentSequence)
                {
                    DicomFragmentSequence fs = item as DicomFragmentSequence;

                    if (_syntax.ExplicitVr)
                        _writer.Write((ushort)0x0000);
                    _writer.Write((uint)UndefinedLength);

                    _writer.Write((ushort)DicomTag.Item.Group);
                    _writer.Write((ushort)DicomTag.Item.Element);

                    if (Flags.IsSet(options, DicomWriteOptions.WriteFragmentOffsetTable) && fs.HasOffsetTable)
                    {
                        _writer.Write((uint)fs.OffsetTableBuffer.Length);
                        fs.OffsetTableBuffer.CopyTo(_writer);
                    }
                    else
                    {
                        _writer.Write((uint)0x00000000);
                    }

                    foreach (DicomFragment bb in fs.Fragments)
                    {
                        _writer.Write((ushort)DicomTag.Item.Group);
                        _writer.Write((ushort)DicomTag.Item.Element);
                        _writer.Write((uint)bb.Length);
                        bb.GetByteBuffer(_syntax).CopyTo(_writer);
                    }

                    _writer.Write((ushort)DicomTag.SequenceDelimitationItem.Group);
                    _writer.Write((ushort)DicomTag.SequenceDelimitationItem.Element);
                    _writer.Write((uint)0x00000000);
                }
                else
                {
                    DicomElement de = item;
                	ByteBuffer theData = de.GetByteBuffer(_syntax, dataset.SpecificCharacterSet);
                    if (_syntax.ExplicitVr)
                    {
                        if (de.Tag.VR.Is16BitLengthField)
                        {
							_writer.Write((ushort)theData.Length);
                        }
                        else
                        {
                            _writer.Write((ushort)0x0000);
                            _writer.Write((uint)theData.Length);
                        }
                    }
                    else
                    {
						_writer.Write((uint)theData.Length);
                    }

					if (theData.Length > 0)
						theData.CopyTo(_writer);
                }
            }

            return DicomWriteStatus.Success;
        }
    }
}
