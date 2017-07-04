/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: FileProcessor.cs
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
using System.IO;
using UIH.RT.Framework.Utility;
using UIH.RT.TMS.DicomCommon;

namespace UIH.RT.TMS.Common.Utilities
{
    /// <summary>
    /// A helper class providing methods for processing files.
    /// </summary>
    public class FileProcessor
    {
        /// <summary>
        /// Delegate for use by the <see cref="FileProcessor.Process(string,string,ProcessFile,bool)"/> method.
        /// </summary>
        /// <param name="filePath">The path to the file to be processed.</param>
        public delegate void ProcessFile(string filePath);

        /// <summary>
        /// Delegate for use by the <see cref="FileProcessor.Process(string,string,ProcessFileCancellable,bool)"/> method.
        /// </summary>
        /// <param name="filePath">The path to the file to be processed.</param>
        /// <param name="cancel">Gets whether or not the entire processing operation should be cancelled.</param>
        public delegate void ProcessFileCancellable(string filePath, out bool cancel);

        /// <summary>
        /// Processes all files on the given <paramref name="path"/> matching the specified <paramref name="searchPattern"/>.
        /// </summary>
        /// <remarks>
        /// The input <paramref name="path"/> can be a file or a directory.
        /// </remarks>
        /// <param name="path">The root path to the file(s) to be processed.</param>
        /// <param name="searchPattern">The search pattern to be used.  A value of <b>null</b> or <b>""</b> indicates that all files are a match.</param>
        /// <param name="proc">The method to call for each matching file.</param>
        /// <param name="recursive">Whether or not the <paramref name="path"/> should be searched recursively.</param>
        public static void Process(string path, string searchPattern, ProcessFile proc, bool recursive)
        {
            Process(path, searchPattern,
                delegate(string filePath, out bool cancel)
                {
                    cancel = false;
                    proc(filePath);
                },
                recursive);
        }

        /// <summary>
        /// Processes all files on the given <paramref name="path"/> matching the specified <paramref name="searchPattern"/>.
        /// </summary>
        /// <remarks>
        /// The input <paramref name="path"/> can be a file or a directory.
        /// </remarks>
        /// <param name="path">The root path to the file(s) to be processed.</param>
        /// <param name="searchPattern">The search pattern to be used.  A value of <b>null</b> or <b>""</b> indicates that all files are a match.</param>
        /// <param name="proc">The method to call for each matching file.</param>
        /// <param name="recursive">Whether or not the <paramref name="path"/> should be searched recursively.</param>
        public static bool Process(string path, string searchPattern, ProcessFileCancellable proc, bool recursive)
        {
            Platform.CheckForNullReference(path, "path");
            Platform.CheckForEmptyString(path, "path");
            Platform.CheckForNullReference(proc, "proc");

            bool cancel;

            // If the path is a directory, process its contents
            if (Directory.Exists(path))
            {
                ProcessDirectory(path, searchPattern, proc, recursive, out cancel);
            }
            // If the path is a file, just process the file
            else if (File.Exists(path))
            {
                proc(path, out cancel);
            }
            else
            {
                throw new FileNotFoundException(String.Format(SR.ExceptionPathDoesNotExist, path));
            }

            return cancel;
        }

        private static void ProcessDirectory(string path, string searchPattern, ProcessFileCancellable proc, bool recursive, out bool cancel)
        {
            cancel = false;

            // Process files in this directory
            string[] fileList;
            GetFiles(path, searchPattern, out fileList);

            if (fileList != null)
            {
                for (int i = 0; i < fileList.Length; i++)
                {
                    proc(fileList[i], out cancel);
                    if (cancel)
                        return;
                    fileList[i] = null;
                }
            }

            if (!recursive) return;

            // If recursive, then descend into lower directories and process those as well
            TraverseDirectories(path, searchPattern, proc);
        }

        private static void GetFiles(string path, string searchPattern, out string[] fileList)
        {
            fileList = null;

            try
            {
                fileList = string.IsNullOrEmpty(searchPattern)
                    ? Directory.GetFiles(path)
                    : Directory.GetFiles(path, searchPattern);
            }
            catch (Exception e)
            {
                LogAdapter.Logger.TraceException(e);
                throw;
            }
        }

        private static void TraverseDirectories(string path, string searchPattern, ProcessFileCancellable proc)
        {
            string[] dirList;

            try
            {
                dirList = Directory.GetDirectories(path);
            }
            catch (Exception e)
            {
                LogAdapter.Logger.TraceException(e);
                throw;
            }

            for (int i = 0; i < dirList.Length; i++)
            {
                bool cancel;
                ProcessDirectory(dirList[i], searchPattern, proc, true, out cancel);
                if (cancel)
                    break;
                dirList[i] = null;
            }
        }
    }
}
