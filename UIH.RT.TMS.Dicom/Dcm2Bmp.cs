/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: yuxuan.duan@united-imaging.com
////
//// File: Dcm2Bmp.cs
////
//// Summary: Dcm2Bmp
//// 
//// Date: 9/25/2014 10:41:03 AM
//////////////////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace UIH.RT.TMS.Dicom
{
    enum COMPRESSION_MODE
    {
        COMPRESS_NONE = 0,
        COMPRESS_RLE,
        COMPRESS_JPEGLOSSY,
        COMPRESS_JPEGLOSSY12BIT,
        COMPRESS_JPEGLOSSLESS,
        COMPRESS_JPEGLOSSLESS2
    };

    public class Dcm2Bmp
    {
        private DicomDataset _dcmDataset;
        public DicomDataset DcmDataset
        {
            get { return _dcmDataset; }
            set { _dcmDataset = value; }
        }

        private int _width;
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        private int _height;
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        private Bitmap _bitmap;
        private byte[] _pixelData;
        private COMPRESSION_MODE _compressionMode;
        private string _photometric;
        private int _bitsAllocated;
        private int _bitsStored;
        private int _samplesPerPixel;
        private int _bitsPerPixel;
        private int _highBit;
        private int _pixelRepresentation;
        private float _winWidth;
        private float _winCenter;

        public Dcm2Bmp()
        {
            _dcmDataset = null;
            _pixelData = null;
            _width = 0;
            _height = 0;
            _compressionMode = COMPRESSION_MODE.COMPRESS_NONE;
            _photometric = null;
            _bitsAllocated = 0;
            _bitsStored = 0;
            _samplesPerPixel = 0;
            _bitsPerPixel = 0;
            _bitmap = null;
            _highBit = 0;
            _pixelRepresentation = 0;
            _winWidth = 0;
            _winCenter = 0;
        }

        public Dcm2Bmp(DicomDataset dataset)
        {
            _dcmDataset = dataset;
            _pixelData = null;
            _width = _dcmDataset[DicomTags.Columns].GetInt32(0, 512);
            _height = _dcmDataset[DicomTags.Rows].GetInt32(0, 512);
            _compressionMode = COMPRESSION_MODE.COMPRESS_NONE;
            _photometric = _dcmDataset[DicomTags.PhotometricInterpretation].ToString();
            _bitsAllocated = _dcmDataset[DicomTags.BitsAllocated].GetInt32(0, 8);
            _samplesPerPixel = _dcmDataset[DicomTags.SamplesPerPixel].GetInt32(0, 1);
            _bitsPerPixel = _bitsAllocated * _samplesPerPixel;
            _bitmap = new Bitmap(_width, _height);
        }

        public Dcm2Bmp(byte[] pixelData, int width, int height, string photometric, int bitsAllocated, int bitsStored, int samplesPerPixel, 
                       int highBit, int pixelRepresentation, float winWidth, float winCenter)
        {
            _dcmDataset = null;
            _pixelData = pixelData;
            _width = width;
            _height = height;
            _compressionMode = COMPRESSION_MODE.COMPRESS_NONE;
            _photometric = photometric;
            _bitsAllocated = bitsAllocated;
            _bitsStored = bitsStored;
            _samplesPerPixel = samplesPerPixel;
            _bitsPerPixel = _bitsAllocated * _samplesPerPixel;
            _bitmap = new Bitmap(_width, _height);
            _highBit = highBit;
            _pixelRepresentation = pixelRepresentation;
            _winWidth = winWidth;
            _winCenter = winCenter;

        }

        public Bitmap Dicom2Bitmap()
        {
            if (null == _photometric)
                return null;
            if (_photometric.Equals("MONOCHROME1") || _photometric.Equals("MONOCHROME2") && (_compressionMode == COMPRESSION_MODE.COMPRESS_NONE))
            {
                return CreateGrayBitmap();
            }
            else if (!(_photometric.Equals("RGB")) && (_compressionMode == COMPRESSION_MODE.COMPRESS_JPEGLOSSY))
            {
                 //
            }
            else if (_photometric.Equals("RGB") && (_compressionMode == COMPRESSION_MODE.COMPRESS_NONE))
            {
                if (_bitsPerPixel == 16)
                {
                    //
                }
                else if (_bitsPerPixel == 24)
                {
                    //return CreateColorBitmap();
                }
                else if (_bitsPerPixel == 32)
                {
                    //
                }
            }
            else if (_photometric.Equals("YBR_FULL"))
            {
                //
            }
            else
            {
                //
            }
            return null;
        }

        private Bitmap CreateGrayBitmap()
        {
            byte[] tempPixelData = _pixelData;
            if(null == _pixelData)
            {
                DicomElement tempDcmElement = _dcmDataset[DicomTags.PixelData];
                var temp = tempDcmElement as DicomElementBinary;

                if (null == temp)
                    return null;

                var stream = temp.AsStream();
                //stream.Seek(0, SeekOrigin.Begin);

                tempPixelData = new byte[stream.Length];
                stream.Read(tempPixelData, 0, (int)stream.Length);
            }

            if (_bitsAllocated == 8)
            {
                _pixelData = tempPixelData;
            }
            else if (_bitsAllocated == 16)
            {
                _pixelData = Convert16BitsTo8Bits(tempPixelData);
            }
            else if (_bitsAllocated == 32)
            {
                //_pixelData = Convert32BitsTo8Bits(tempPixelData); //rarely use!
            }
            else
            {
                return null;
            }
            //指定8位格式，即256色
            _bitmap = new Bitmap(_width, _height, PixelFormat.Format8bppIndexed);

            //将该位图存入内存中
            MemoryStream curImageStream = new MemoryStream();
            _bitmap.Save(curImageStream, ImageFormat.Bmp);
            curImageStream.Flush();

            //由于位图数据需要DWORD对齐（4byte倍数），计算需要补位的个数
            int curPadNum = ((_width * 8 + 31) / 32 * 4) - _width;

            //最终生成的位图数据大小(byte)
            int bitmapDataSize = ((_width * 8 + 31) / 32 * 4) * _height;

            //数据部分相对文件开始偏移，具体可以参考位图文件格式,记录图像数据区的起始位置。
            int dataOffset = ReadData(curImageStream, 10, 4);

            //改变调色板，因为默认的调色板是32位彩色的，需要修改为256色的调色板
            int paletteStart = 54;
            int paletteEnd = dataOffset;
            int color = 0;
            for (int i = paletteStart; i < paletteEnd; i += 4)
            {
                byte[] tempColor = new byte[4];
                tempColor[0] = (byte)color;
                tempColor[1] = (byte)color;
                tempColor[2] = (byte)color;
                tempColor[3] = (byte)0;
                color++;

                curImageStream.Position = i;
                curImageStream.Write(tempColor, 0, 4);
            }

            //最终生成的位图数据，以及大小，高度没有变，宽度需要调整
            byte[] destImageData = new byte[bitmapDataSize];
            int destWidth = _width + curPadNum;

            //生成最终的位图数据，注意的是，位图数据 从左到右，从下到上，所以需要颠倒
            for (int originalRowIndex = _height - 1; originalRowIndex >= 0; originalRowIndex--)
            {
                int destRowIndex = _height - originalRowIndex - 1;

                for (int dataIndex = 0; dataIndex < _width; dataIndex++)
                {
                    //同时还要注意，新的位图数据的宽度已经变化destWidth，否则会产生错位
                    destImageData[destRowIndex * destWidth + dataIndex] = _pixelData[originalRowIndex * _width + dataIndex];
                }
            }

            //将流的Position移到数据段   
            curImageStream.Position = dataOffset;

            //将新位图数据写入内存中
            curImageStream.Write(destImageData, 0, bitmapDataSize);
            curImageStream.Flush();

            //将内存中的位图写入Bitmap对象
            _bitmap = new Bitmap(curImageStream);
            return _bitmap;
        }

        private byte[] Convert16BitsTo8Bits(byte[] pixelData)
        {
            if (null != _dcmDataset)
            {
                _pixelRepresentation = _dcmDataset[DicomTags.PixelRepresentation].GetInt32(0, 0);
                _highBit = _dcmDataset[DicomTags.HighBit].GetInt16(0, 15);
                _bitsStored = _dcmDataset[DicomTags.BitsStored].GetInt16(0, 8);
                _winWidth = _dcmDataset[DicomTags.WindowWidth].GetFloat32(0, 0);
                _winCenter = _dcmDataset[DicomTags.WindowCenter].GetFloat32(0, 0);
            }
            //int bIsSigned = _dcmDataset.GetInt(Tags.PixelRepresentation, 0);
            //short nHighBit = (short)_dcmDataset.GetInt(Tags.HighBit, 15);
            //short nBitStored = (short)_dcmDataset.GetInt(Tags.BitsStored, 8);
            ////string sModality = _dcmDataset.GetString(Tags.Modality, 0);
            //short pPoint;
            if (_highBit < 15)
            {
                ushort nMask;
                short nSignBit;

                if (_pixelRepresentation == 0) // Unsigned integer
                {
                    nMask = (ushort)(0xffff << (_highBit + 1));

                    for (int i = 0; i < _width * _height; i++)
                    {
                        ushort uPoint = BitConverter.ToUInt16(pixelData, i * 2);
                        uPoint &= (ushort)~nMask;

                        pixelData[i * 2] = (byte)uPoint;
                        pixelData[i * 2 + 1] = (byte)(uPoint >> 8);
                    }
                }
                else
                {
                    nSignBit = (short)(1 << _highBit);
                    nMask = (ushort)(0xffff << (_highBit + 1));

                    for (int i = 0; i < _width * _height; i++)
                    {
                        ushort sPoint = (ushort)BitConverter.ToInt16(pixelData, i * 2);
                        if ((sPoint & nSignBit) != 0)//negative
                            sPoint |= nMask;
                        else
                            sPoint |= (ushort)~nMask;

                        pixelData[i * 2] = (byte)sPoint;
                        pixelData[i * 2 + 1] = (byte)(sPoint >> 8);
                    }
                }
            }

            float fSlope;
            float fShift;
            float fValue;
            float fWindowCenter = _winCenter;
            float fWindowWidth = _winWidth;
            //int nCount = _width * _height * 2;
            //fWindowCenter = _dcmDataset.GetFloat(Tags.WindowCenter, 0);
            //fWindowWidth = _dcmDataset.GetFloat(Tags.WindowWidth, 0);

            byte[] newPixelData = new byte[_width * _height + 4];

            for (int i = 0; i < _width * _height; i++)
            {
                if(_pixelRepresentation == 0)
                {
                    ushort upp = BitConverter.ToUInt16(pixelData, 2 * i);
                    if (_bitsStored == 8)
                    {
                        newPixelData[i] = (byte)upp;
                    }
                    else
                    {
                        fShift = fWindowCenter - fWindowWidth / 2.0f;
                        fSlope = 255.0f / fWindowWidth;//斜率

                        fValue = (upp - fShift) * fSlope;

                        if (fValue < 0)
                            fValue = 0;
                        else if (fValue > 255)
                            fValue = 255;

                        newPixelData[i] = (byte)fValue;
                    }
                }
                else
                {
                    short pp = BitConverter.ToInt16(pixelData, 2 * i);

                    //1. 标准算法
                    //float fMax = fWindowCenter + fWindowWidth / 2;
                    //float fMin = fWindowCenter - fWindowWidth / 2;

                    //if (pp <= fWindowCenter - 0.5 - (fWindowWidth-1)/2)
                    //{
                    //    fValue = 0; 
                    //}
                    //else if(pp > fWindowCenter-0.5 + (fWindowWidth-1)/2)
                    //{
                    //    fValue = 255;
                    //}
                    //else
                    //{
                    //    fValue = (float)((pp-(fWindowCenter-0.5))/(fWindowWidth-1)+0.5)*255;
                    //}

                    //newPixelData[i] = (byte)fValue;

                    //2. 经典算法
                    if (_bitsStored == 8)
                    {
                        newPixelData[i] = (byte)pp;
                    }
                    else
                    {
                        fShift = fWindowCenter - fWindowWidth / 2.0f;
                        fSlope = 255.0f / fWindowWidth;//斜率

                        fValue = (pp - fShift) * fSlope;

                        if (fValue < 0)
                            fValue = 0;
                        else if (fValue > 255)
                            fValue = 255;

                        newPixelData[i] = (byte)fValue;
                    }
                }
             }

            return newPixelData;
        }

        private static int ReadData(MemoryStream curStream, int startPosition, int length)
        {
            int result = -1;

            byte[] tempData = new byte[length];
            curStream.Position = startPosition;
            curStream.Read(tempData, 0, length);
            result = BitConverter.ToInt32(tempData, 0);

            return result;
        }

    }
}
