/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: DicomRleCodecFactory.cs
////
//// Summary:
////
////
//// Date: 2014/08/19
//////////////////////////////////////////////////////////////////////////
#region License

// Copyright (c) 2011 - 2013, United-Imaging Inc.
// All rights reserved.
// http://www.united-imaging.com

#endregion

#region Inline Attributions
// The source code contained in this file is based on an original work
// from
//
// mDCM: A C# DICOM library
//
// Copyright (c) 2008  Colby Dillion
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//
// Author:
//    Colby Dillion (colby.dillion@gmail.com)
#endregion

using System;
using System.ComponentModel.Composition;
using System.Xml;
using UIH.RT.TMS.Common;

namespace UIH.RT.TMS.Dicom.Codec.Rle
{
    /// <summary>
    /// Default codec factory for the DICOM RLE Transfer Syntax.
    /// </summary>
    [Export(typeof(IDicomCodecFactory))]
    public class DicomRleCodecFactory : IDicomCodecFactory
    {
        private readonly string _name = TransferSyntax.RleLossless.Name;
        private readonly TransferSyntax _transferSyntax = TransferSyntax.RleLossless;

        public string Name
        {
            get { return _name; }
        }

        public bool Enabled
        {
            get { return true; }
        }

        public TransferSyntax CodecTransferSyntax
        {
            get { return _transferSyntax; }
        }

        virtual public DicomCodecParameters GetCodecParameters(DicomDataset dataSet)
        {
			DicomRleCodecParameters codecParms = new DicomRleCodecParameters { ConvertPaletteToRGB = true };

			return codecParms;
		}
		public DicomCodecParameters GetCodecParameters(XmlDocument parms)
		{
			DicomRleCodecParameters codecParms = new DicomRleCodecParameters();

			XmlElement element = parms.DocumentElement;

			if (element != null && element.Attributes["convertFromPalette"]!=null)
			{
				String boolString = element.Attributes["convertFromPalette"].Value;
				bool convert;
				if (false == bool.TryParse(boolString, out convert))
					throw new ApplicationException("Invalid convertFromPalette value specified for RLE: " + boolString);
				codecParms.ConvertPaletteToRGB = convert;
			}
			else
				codecParms.ConvertPaletteToRGB = true;

			return codecParms;
		}
        public IDicomCodec GetDicomCodec()
        {
            return new DicomRleCodec();
        }
    }
}
