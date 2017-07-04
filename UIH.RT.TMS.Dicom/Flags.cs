/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: Flags.cs
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

namespace UIH.RT.TMS.Dicom
{
    /// <summary>
    /// Static helper class for checking if flags have been set.
    /// </summary>
    public static class Flags
    {
        public static bool IsSet(DicomDumpOptions options, DicomDumpOptions flag)
        {
            return (options & flag) == flag;
        }
        public static bool IsSet(DicomReadOptions options, DicomReadOptions flag)
        {
            return (options & flag) == flag;
        }
        public static bool IsSet(DicomWriteOptions options, DicomWriteOptions flag)
        {
            return (options & flag) == flag;
        }
    }

    /// <summary>
    /// An enumerated value to specify options when generating a dump of a DICOM object.
    /// </summary>
    [Flags]
    public enum DicomDumpOptions
    {
        None = 0,
        ShortenLongValues = 1,
        Restrict80CharactersPerLine= 2,
        KeepGroupLengthElements = 4,
        Default = ShortenLongValues | Restrict80CharactersPerLine
    }

    /// <summary>
    /// An enumerated value to specify options when reading DICOM files. 
    /// </summary>
    [Flags]
    public enum DicomReadOptions
    {
        None = 0,
        KeepGroupLengths = 1,
        UseDictionaryForExplicitUN = 2,
        AllowSeekingForContext = 4,
        ReadNonPart10Files = 8,
        DoNotStorePixelDataInDataSet = 16,
        StorePixelDataReferences = 32,
        Default = UseDictionaryForExplicitUN | AllowSeekingForContext | ReadNonPart10Files
    }

    /// <summary>
    /// An enumerated value to specify options when writing DICOM files.
    /// </summary>
    [Flags]
    public enum DicomWriteOptions
    {
        None = 0,
        CalculateGroupLengths = 1,
        ExplicitLengthSequence = 2,
        ExplicitLengthSequenceItem = 4,
        WriteFragmentOffsetTable = 8,
        Default = WriteFragmentOffsetTable
    }
}
