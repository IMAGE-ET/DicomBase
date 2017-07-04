/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: CIELabColor.cs
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
using System.Collections.Generic;
using System.Text;

namespace UIH.RT.TMS.Dicom.Iod
{
	public struct CIELabColor
	{
		private ushort _l;
		private ushort _a;
		private ushort _b;

		public CIELabColor(ushort l, ushort a, ushort b)
		{
			_l = l;
			_a = a;
			_b = b;
		}

		public ushort L
		{
			get { return _l; }
			set { _l = value; }
		}

		public ushort A
		{
			get { return _a; }
			set { _a = value; }
		}

		public ushort B
		{
			get { return _b; }
			set { _b = value; }
		}

		public ushort[] ToArray()
		{
			return new ushort[] {_l, _a, _b};
		}
	}
}
