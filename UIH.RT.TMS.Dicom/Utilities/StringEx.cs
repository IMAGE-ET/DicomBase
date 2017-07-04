/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: StringEx.cs
////
//// Summary:
////
////
//// Date: 2014/08/19
//////////////////////////////////////////////////////////////////////////
using System.Text;

namespace UIH.RT.TMS.Dicom.Utilities
{
    public static class StringEx
    {
        public static bool IsChinese(this string str)
        {
            int strLen = str.Length;

            int bytLeng = Encoding.UTF8.GetBytes(str).Length;

            return strLen < bytLeng;
        }

        /// <summary>
        /// Convert a Chinese string to utf8 string
        /// </summary>
        /// <param name="str">the Chinese string</param>
        /// <param name="srcharacterSet">the DICOM DataSet Specific characterSet</param>
        /// <returns>the utf8 encoded Chinese string</returns>
        public static string ToUTF8ChineseString(this string str, string srcharacterSet)
        {
            ///////////////////////////////////////////////////
            // the str from DICOM is already encode with the specific character set
            // so we need decode the bytes by the specific character set
            //////////////////////////////////////////////////
            var srcEncode = SpecificCharacterSetParser.GetEncoding(srcharacterSet);
            var bytes = srcEncode.GetBytes(str);

            if (str.IsGBChinese())
            {
                var gbEncode = Encoding.GetEncoding("GB18030");
                var utfBytes = Encoding.Convert(gbEncode, Encoding.UTF8, bytes);
                return Encoding.UTF8.GetString(utfBytes);
            }
            if (str.IsUTF8Chinese())
            {
                return Encoding.UTF8.GetString(bytes);
            }
            return str;
        }

        private static bool IsGBChinese(this string str)
        {
            int strLen = str.Length;

            int bytLeng = Encoding.UTF8.GetBytes(str).Length;

            // GBK: 2 bit is a Chinese word
            return strLen * 2 == bytLeng;
        }

        private static bool IsUTF8Chinese(this string str)
        {
            int strLen = str.Length;

            int bytLeng = Encoding.UTF8.GetBytes(str).Length;

            // UTF8: more than 3 bit is a Chinese word
            return strLen * 3 <= bytLeng;
        }
    }
}
