/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: IDicomCharacterSetParser.cs
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

namespace UIH.RT.TMS.Dicom
{
    /// <summary>
    /// Public interface used to define a parser to convert between raw bytes
    /// and Unicode.
    /// </summary>
    public interface IDicomCharacterSetParser
    {
        byte[] Encode(string unicodeString, string specificCharacterSet);
        string Decode(byte[] repertoireStringAsRaw, string specificCharacterSet);
        string EncodeAsIsomorphicString(string unicodeString, string specificCharacterSet);
        string DecodeFromIsomorphicString(string repertoireStringAsUnicode, string specificCharacterSet);
        string ConvertRawToIsomorphicString(byte[] repertoireStringAsRaw);
        bool IsVRRelevant(string vr);
    }
}
