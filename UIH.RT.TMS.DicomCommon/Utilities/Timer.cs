/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: Timer.cs
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
using System.Threading;
using UIH.RT.Framework.Utility;

namespace UIH.RT.TMS.Common.Utilities
{
	/// <summary>
	/// A delegate for use by a <see cref="Timer"/> object.
	/// </summary>
	public delegate void TimerDelegate(object state);

	/// <summary>
	/// Implements a simple timer class that handles marshalling delegates back to the thread on which
	/// this object was allocated (usually the main UI thread).
	/// </summary>
	/// <remarks>
	/// This class <B>must</B> be instantiated from within a UI thread, otherwise an exception
	/// could be thrown upon construction (unless the thread has a custom <see cref="SynchronizationContext"/>).  
	/// This class relies on <see cref="SynchronizationContext.Current"/> being non-null in order to do the marshalling.
	/// Also, this class is very simple and may not be as accurate as other timer classes.
	/// </remarks>
	public sealed class Timer : IDisposable
	{
		private enum State
		{
			Starting,
			Started,
			Stopping,
			Stopped
		} 

		private readonly SynchronizationContext _synchronizationContext;
		private readonly object _stateObject;
		private readonly TimerDelegate _elapsedDelegate;

		private readonly object _startStopLock;
		private volatile State _state;
		private volatile int _intervalMilliseconds;
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="elapsedDelegate">The delegate to execute on a timer.</param>
		public Timer(TimerDelegate elapsedDelegate)
			: this(elapsedDelegate, null)
		{ 
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="elapsedDelegate">The delegate to execute on a timer.</param>
		/// <param name="stateObject">A user defined state object.</param>
		public Timer(TimerDelegate elapsedDelegate, object stateObject)
			: this(elapsedDelegate, stateObject, 1000)
		{
		}

		public Timer(TimerDelegate elapsedDelegate, object stateObject, TimeSpan interval)
			: this(elapsedDelegate, stateObject, (int)interval.TotalMilliseconds)
		{
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="elapsedDelegate">The delegate to execute on a timer.</param>
		/// <param name="stateObject">A user defined state object.</param>
		/// <param name="intervalMilliseconds">The time to wait in milliseconds.</param>
		public Timer(TimerDelegate elapsedDelegate, object stateObject, int intervalMilliseconds)
		{
			_synchronizationContext = SynchronizationContext.Current;

			Platform.CheckForNullReference(_synchronizationContext, "SynchronizationContext.Current");
			Platform.CheckForNullReference(elapsedDelegate, "elapsedDelegate");
			
			_stateObject = stateObject; 
			_elapsedDelegate = elapsedDelegate;

			_startStopLock = new object();
			_state = State.Stopped;
			_intervalMilliseconds = intervalMilliseconds;
		}

		/// <summary>
		/// Gets whether or not the timer is currently running.
		/// </summary>
		public bool Enabled
		{
			get { return _state != State.Stopped; }	
		}

		/// <summary>
		/// Sets the timer interval in milliseconds.
		/// </summary>
		/// <remarks>
		/// The default value is 1000 milliseconds, or 1 second.
		/// </remarks>
		public int IntervalMilliseconds
		{
			get { return _intervalMilliseconds; }
			set { _intervalMilliseconds = value; }
		}

		/// <summary>
		/// Starts the timer.
		/// </summary>
		public void Start()
		{
			lock (_startStopLock)
			{
				if (_state != State.Stopped)
					return;

				_state = State.Starting;
				ThreadPool.QueueUserWorkItem(RunThread);
				Monitor.Wait(_startStopLock);
			}
		}

		/// <summary>
		/// Stops the timer.
		/// </summary>
		public void Stop()
		{
			lock(_startStopLock)
			{
				if (_state != State.Started)
					return;

				_state = State.Stopping;
				Monitor.Pulse(_startStopLock);
				Monitor.Wait(_startStopLock);
			}
		}

		#region IDisposable Members

		/// <summary>
		/// Implementation of the <see cref="IDisposable"/> pattern.
		/// </summary>
		public void Dispose()
		{
			try
			{
				Stop();
				GC.SuppressFinalize(this);
			}
			catch (Exception e)
			{
				LogAdapter.Logger.TraceException(e);
			}
		}

		#endregion

		private void OnElapsed(object nothing)
		{
			if (!Enabled)
				return;

			_elapsedDelegate(_stateObject);
		}

		private void RunThread(object nothing)
		{
			lock (_startStopLock)
			{
				_state = State.Started;
				//Signal started.
				Monitor.Pulse(_startStopLock);

				while (_state != State.Stopping)
				{
					Monitor.Wait(_startStopLock, _intervalMilliseconds);
					if (_state == State.Stopping)
						break;

					_synchronizationContext.Post(OnElapsed, null);
				}

				_state = State.Stopped;
				//Signal stopped.
				Monitor.Pulse(_startStopLock);
			}
		}
	}
}
