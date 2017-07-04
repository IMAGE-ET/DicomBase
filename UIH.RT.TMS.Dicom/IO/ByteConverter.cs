/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ByteConverter.cs
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

﻿using System;

namespace UIH.RT.TMS.Dicom.IO
{
    public class ByteConverter
    {
        public static bool NeedToSwapBytes(Endian endian)
        {
            if (BitConverter.IsLittleEndian)
            {
                return Endian.Little != endian;
            }

            return Endian.Big != endian;
        }

        public static byte[] ToByteArray(ushort[] words)
        {
            int count = words.Length;
            var bytes = new byte[words.Length*2];
            for (int i = 0; i < count; i++)
            {
                Array.Copy(BitConverter.GetBytes(words[i]), 0, bytes, i * 2, 2);
            }

            return bytes;
        }

        public static ushort[] ToUInt16Array(byte[] bytes)
        {
            int count = bytes.Length/2;
            var words = new ushort[count];
            for (int i = 0, p = 0; i < count; i++, p += 2)
            {
                words[i] = BitConverter.ToUInt16(bytes, p);
            }

            return words;
        }

        public static short[] ToInt16Array(byte[] bytes)
        {
            int count = bytes.Length/2;
            var words = new short[count];
            for (int i = 0, p = 0; i < count; i++, p += 2)
            {
                words[i] = BitConverter.ToInt16(bytes, p);
            }

            return words;
        }

        public static uint[] ToUInt32Array(byte[] bytes)
        {
            int count = bytes.Length / 4;
            var dwords = new uint[count];
            for (int i = 0, p = 0; i < count; i++, p += 4)
            {
                dwords[i] = BitConverter.ToUInt32(bytes, p);
            }
            return dwords;
        }

        public static int[] ToInt32Array(byte[] bytes)
        {
            int count = bytes.Length / 4;
            var dwords = new int[count];
            for (int i = 0, p = 0; i < count; i++, p += 4)
            {
                dwords[i] = (int)BitConverter.ToUInt32(bytes, p);
            }
            return dwords;
        }

        public static float[] ToFloatArray(byte[] bytes)
        {
            int count = bytes.Length / 4;
            var floats = new float[count];
            for (int i = 0, p = 0; i < count; i++, p += 4)
            {
                floats[i] = BitConverter.ToSingle(bytes, p);
            }
            return floats;
        }

        public static double[] ToDoubleArray(byte[] bytes)
        {
            int count = bytes.Length / 8;
            var doubles = new double[count];
            for (int i = 0, p = 0; i < count; i++, p += 8)
            {
                doubles[i] = BitConverter.ToDouble(bytes, p);
            }
            return doubles;
        }

        public static void SwapBytes(ushort[] words)
        {
            for (int i = 0; i < words.Length; i++)
            {
                ushort u = words[i];
                words[i] = unchecked((ushort) ((u >> 8) | (u << 8)));
            }
        }

        public static void SwapBytes(short[] words)
        {
            int count = words.Length;
            for (int i = 0; i < count; i++)
            {
                short u = words[i];
                words[i] = unchecked((short)((u >> 8) | (u << 8)));
            }
        }
    }
}
