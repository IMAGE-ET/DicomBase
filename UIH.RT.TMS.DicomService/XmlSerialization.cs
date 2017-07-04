/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: XmlSerialization.cs
////
//// Summary:
////
////
//// Date: 2014/08/19
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace UIH.RT.TMS.DicomService
{
    public class XmlSerialization
    {
        public static T Deserialize<T>(string path) where T : class
        {
            if (string.IsNullOrWhiteSpace(path))
                return null;

            try
            {
                using (TextReader reader = new StreamReader(path))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return serializer.Deserialize(reader) as T;
                }
            }
            catch
            {
                return null;
            }
        }

        public static T DeserializeFromString<T>(string serializedString) where T : class
        {
            if (string.IsNullOrWhiteSpace(serializedString))
                return null;

            try
            {
                using (StringReader reader = new StringReader(serializedString))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return serializer.Deserialize(reader) as T;
                }
            }
            catch
            {
                return null;
            }
        }

        public static bool Serialize<T>(T instance, string path) where T : class
        {
            if (string.IsNullOrWhiteSpace(path))
                return false;

            if (null == instance)
                return false;

            try
            {
                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(writer, instance);
                    writer.Flush();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static string SerializeToString<T>(T instance) where T : class
        {
            if (null == instance)
                return null;

            try
            {
                using (TextWriter writer = new StringWriter())
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));

                    serializer.Serialize(writer, instance);
                    return writer.ToString();
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
