/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: SystemResources.cs
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace UIH.RT.TMS.Common.Utilities
{
	/// <summary>
	/// Memory and storage size units
	/// </summary>
	public enum SizeUnits
	{
		/// <summary>
		/// Bytes
		/// </summary>
		Bytes,
		/// <summary>
		/// Kilobytes
		/// </summary>
		Kilobytes,
		/// <summary>
		/// Megabytes
		/// </summary>
		Megabytes,
		/// <summary>
		/// Gigabytes
		/// </summary>
		Gigabytes
	}

	/// <summary>
	/// Provides convenience methods for querying system resources.
	/// </summary>
	public static class SystemResources
	{
		private static volatile PerformanceCounter _memoryPerformanceCounter;
		private static readonly object _syncRoot = new object();

		private static PerformanceCounter MemoryPerformanceCounter
		{
			get
			{
				if (_memoryPerformanceCounter == null)
				{
					lock (_syncRoot)
					{
						if (_memoryPerformanceCounter == null)
							_memoryPerformanceCounter = new PerformanceCounter("Memory", "Available Bytes");
					}
				}

				return _memoryPerformanceCounter;
			}
		}

		/// <summary>
		/// Gets the available physical memory.
		/// </summary>
		/// <param name="units"></param>
		/// <returns></returns>
		public static long GetAvailableMemory(SizeUnits units)
		{
			long availableBytes = Convert.ToInt64(MemoryPerformanceCounter.NextValue());

			if (units == SizeUnits.Bytes)
				return availableBytes;
			else if (units == SizeUnits.Kilobytes)
				return availableBytes / 1024;
			else if (units == SizeUnits.Megabytes)
				return availableBytes / 1048576;
			else
				return availableBytes / 1073741824;
		}

        [DllImport("kernel32.dll", EntryPoint = "GetDiskFreeSpaceExA")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetDiskFreeSpaceEx(string lpDirectoryName, out long lpFreeBytesAvailableToCaller,
                                            out long lpTotalNumberOfBytes, out long lpTotalNumberOfFreeBytes);

        public static DriveInformation GetDriveInformation(string path)
	    {
            long available, total, free;
            bool result = GetDiskFreeSpaceEx(path, out available, out total, out free);

            if (result)
            {
                return new DriveInformation()
                           {
                               RootDirectory = System.IO.Path.GetPathRoot(path),
                               Total = total,
                               Free = free
                           };
            }

            throw new Win32Exception(string.Format("Unable to get drive information {0}. Error code: {1}", path, 0));
	    }
	}

    public class DriveInformation
    {
        public string RootDirectory { get; set; }

        public long Total { get; set; }

        public long Free { get; set; }
    }
}
