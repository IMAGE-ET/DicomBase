/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: ExclusiveLock.cs
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
using System.Globalization;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using UIH.RT.Framework.Utility;

namespace UIH.RT.TMS.Common.Utilities
{
	public class NamedMutexLock : ExclusiveLock
	{
		private readonly string _name;
		private readonly Mutex _mutex;
		private volatile bool _mutexLocked;
		
		public NamedMutexLock(string name)
		{
			_name = name;
			_mutex = CreateMutex(name);
		}

		private static Mutex CreateMutex(string name)
		{
			try
			{
				// Open the mutex.
				return Mutex.OpenExisting(name);
			}
			catch (WaitHandleCannotBeOpenedException)
			{
				// The named mutex does not exist.
				MutexSecurity mSec = new MutexSecurity();

				MutexAccessRule rule = new MutexAccessRule(
					new SecurityIdentifier(WellKnownSidType.WorldSid, null),
					MutexRights.FullControl, AccessControlType.Allow);
				mSec.AddAccessRule(rule);

				bool mutexWasCreated;
				return new Mutex(false, name, out mutexWasCreated, mSec);
			}
			catch (UnauthorizedAccessException)
			{
				// The named mutex exists, but the user does not have the security access required to use it.
				try
				{
					var mutex = Mutex.OpenExisting(name, MutexRights.ReadPermissions | MutexRights.ChangePermissions);

					// Get the current ACL. This requires MutexRights.ReadPermissions.
					MutexSecurity mSec = mutex.GetAccessControl();

					// Now grant the user the correct rights.
					MutexAccessRule rule = new MutexAccessRule(
						new SecurityIdentifier(WellKnownSidType.WorldSid, null),
						MutexRights.FullControl, AccessControlType.Allow);
					mSec.AddAccessRule(rule);

					// Update the ACL. This requires MutexRights.ChangePermissions.
					mutex.SetAccessControl(mSec);

					return Mutex.OpenExisting(name);
				}
				catch (UnauthorizedAccessException)
				{
					return new Mutex(false, name);
				}
			}
		}

		public string Name
		{
			get { return _name; }
		}

		public override bool IsLocked
		{
			get { return _mutexLocked; }
		}

		public override bool Lock()
		{
			_mutex.WaitOne();
			_mutexLocked = true;
			return true;
		}

		public override bool Lock(TimeSpan timeSpan)
		{
			if (_mutex.WaitOne(timeSpan))
			{
				_mutexLocked = true;
				return true;
			}

			return false;
		}

		public override bool Unlock()
		{
			_mutexLocked = false;
			_mutex.ReleaseMutex();
			return true;
		}

		protected override void Dispose(bool disposing)
		{
			_mutex.Close();
		}
	}

	public class ThreadLock : ExclusiveLock
	{
		private readonly object _syncLock = new object();
		private volatile bool _isLocked;

		public override bool IsLocked
		{
			get { return _isLocked; }
		}

		public override bool Lock()
		{
			Monitor.Enter(_syncLock);
			_isLocked = true;
			return true;
		}

		public override bool Lock(TimeSpan timeSpan)
		{
			if (Monitor.TryEnter(_syncLock, timeSpan))
			{
				_isLocked = true;
				return true;
			}

			return false;
		}

		public override bool Unlock()
		{
			_isLocked = false;
			Monitor.Exit(_syncLock);
			return true;
		}
	}

	public abstract class ExclusiveLock : IDisposable
	{
		public abstract bool IsLocked { get; }
		public abstract bool Lock();
		public abstract bool Lock(TimeSpan timeSpan);
		public abstract bool Unlock();
		
	    public static void WithLock<T>(ExclusiveLock @lock, Action<T> action) where T : class 
		{
			WithLock(@lock, action, null);
		}

		public static void WithLock<T>(ExclusiveLock @lock, Action<T> action, T state)
		{
			@lock.Lock();

			try
			{
				action(state);
			}
			finally
			{
				@lock.Unlock();
			}
		}

		protected virtual void Dispose(bool disposing)
		{
		}

		#region IDisposable Members

		public void Dispose()
		{
			try
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}
			catch (Exception e)
			{
				LogAdapter.Logger.TraceException(e);
			}
		}

		#endregion

	    public static ExclusiveLock CreateFileLock(System.IO.FileInfo file)
		{
			return CreateFileSystemLock(file);
		}

		public static ExclusiveLock CreateDirectoryLock(System.IO.DirectoryInfo directory)
		{
			return CreateFileSystemLock(directory);
		}

		private static ExclusiveLock CreateFileSystemLock(System.IO.FileSystemInfo fileSystemInfo)
		{
			Platform.CheckForNullReference(fileSystemInfo, "fileSystemInfo");
			return CreateFileSystemLock(fileSystemInfo.FullName);
		}

		public static ExclusiveLock CreateFileSystemLock(string fileOrDirectoryName)
		{
			Platform.CheckForEmptyString(fileOrDirectoryName, "fileOrDirectoryName");
			var mutexName = fileOrDirectoryName.ToLower();
			foreach(var invalidFileNameChar in Path.GetInvalidFileNameChars())
				mutexName = mutexName.Replace(invalidFileNameChar.ToString(CultureInfo.InvariantCulture), "-");

            // If "Global\" prefix is not included, the mutex is considered Local\ and will not block across users
            // The ShredHostService + Desktop app run under different users
			mutexName = "Global\\cc-filesystem-lock-" + mutexName;

            LogAdapter.Logger.InfoWithFormat("Creating file system lock for: {0}\r\nMutex name: {1}", fileOrDirectoryName, mutexName);
			return new NamedMutexLock(mutexName);
		}
	}
}
