/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: EventsHelper.cs
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
using UIH.RT.Framework.Utility;

namespace UIH.RT.TMS.Common.Utilities
{
	/// <summary>
	/// Helper class for raising events.
	/// </summary>
	public class EventsHelper
	{
		/// <summary>
		/// Helper method for raising events safely.
		/// </summary>
		/// <param name="del">Delegate to invoke.</param>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/>.</param>
		/// <remarks>
		/// Use this method to invoke user code via delegates.
		/// This method will log any exceptions thrown in user code and immediately rethrow it.
		/// The typical usage is shown below.
		/// </remarks>
		/// <example>
		/// <code>
		/// [C#]
		/// public class PresentationImage
		/// {
		///    private event EventHandler _imageDrawingEvent;
		///    
		///    public void Draw()
		///    {
		///       EventsHelper.Fire(_imageDrawingEvent, this, new DrawImageEventArgs());
		///    }
		/// }
		/// </code>
		/// </example>
		public static void Fire(Delegate del, object sender, EventArgs e)
		{
			if (del == null)
				return;

			Delegate[] delegates = del.GetInvocationList();

			foreach(Delegate sink in delegates)
			{
				try
				{
					sink.DynamicInvoke(sender, e);
				}
				catch (Exception ex)
				{
                    LogAdapter.Logger.TraceException(ex);
					throw;
				}
			}
		}
	}
}
