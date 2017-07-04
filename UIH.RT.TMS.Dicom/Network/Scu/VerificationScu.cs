/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: VerificationScu.cs
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
using System.Runtime.Remoting.Messaging;
using UIH.RT.Framework.Utility;
using UIH.RT.TMS.Common;

namespace UIH.RT.TMS.Dicom.Network.Scu
{
	/// <summary>
	/// Verification SCU component.
	/// </summary>
	/// <example>Asynchronous:
	/// <code><![CDATA[
	/// using System.Threading;
	/// 
	/// private AutoResetEvent m_WaitHandle = new AutoResetEvent(false);
	///
	/// public void ASynchVerificationTest()
	/// {
	///    VerificationScu verificationScu = new VerificationScu();
	///    verificationScu.Timeout = 90;
	///    verificationScu.BeginVerify("localAe", "remoteAe", "remoteHost", 1000, new AsyncCallback(VerifyComplete), verificationScu);
	///    m_WaitHandle.WaitOne();
	/// }

	/// private void VerifyComplete(IAsyncResult ar)
	/// {
	///     VerificationScu verificationScu = (VerificationScu)ar.AsyncState;
	///     VerificationResult verificationResult = verificationScu.EndVerify(ar);
	///     m_WaitHandle.Set();
	/// }
	/// ]]></code>
	/// </example>
	public class VerificationScu : ScuBase
	{
		#region Public Events/Delegates...
		public delegate VerificationResult VerifyDelegate(string clientAETitle, string remoteAE, string remoteHost, int remotePort);
		#endregion

		#region Private Members
		/// <summary>Contains the verification result.</summary>
		private VerificationResult _verificationResult;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="VerificationScu"/> class.
		/// </summary>
		public VerificationScu()
		{
		}
		#endregion

		#region Public Properties...
		public VerificationResult Result
		{
			get { return _verificationResult; }
		}
		#endregion

		#region Public Methods

		/// <summary>
		/// Sends verification request to specified Remote Dicom Host (synchronously).
		/// </summary>
		/// <param name="clientAETitle"></param>
		/// <param name="remoteAE"></param>
		/// <param name="remoteHost"></param>
		/// <param name="remotePort"></param>
		/// <returns></returns>
		public VerificationResult Verify(string clientAETitle, string remoteAE, string remoteHost, int remotePort)
		{
            LogAdapter.Logger.InfoWithFormat("Preparing to connect to AE {0} on host {1} on port {2} for verification.", remoteAE, remoteHost, remotePort);
			try
			{
				base.ClientAETitle = clientAETitle;
				base.RemoteAE = remoteAE;
				base.RemoteHost = remoteHost;
				base.RemotePort = remotePort;
				_verificationResult = VerificationResult.Failed;
				base.Connect();
			}
			catch (Exception e)
			{
                LogAdapter.Logger.TraceException(e);
			}
			if (base.Status == ScuOperationStatus.Canceled)
				return VerificationResult.Canceled;
			else if (base.Status == ScuOperationStatus.TimeoutExpired)
				return VerificationResult.TimeoutExpired;
            else if (Status == ScuOperationStatus.AssociationRejected)
                return VerificationResult.AssociationRejected;
			else
				return _verificationResult;
		}

		/// <summary>
		/// Begins the verification in asynchronous mode.
		/// </summary>
		/// <param name="clientAETitle">The client AE title.</param>
		/// <param name="remoteAE">The remote AE.</param>
		/// <param name="remoteHost">The remote host.</param>
		/// <param name="remotePort">The remote port.</param>
		/// <param name="callback">The callback.</param>
		/// <param name="asyncState">State of the async.</param>
		/// <returns></returns>
		public IAsyncResult BeginVerify(string clientAETitle, string remoteAE, string remoteHost, int remotePort, AsyncCallback callback, object asyncState)
		{
			VerifyDelegate verifyDelegate = new VerifyDelegate(this.Verify);

			return verifyDelegate.BeginInvoke(clientAETitle, remoteAE, remoteHost, remotePort, callback, asyncState);
		}

		/// <summary>
		/// Call this to End the asyncronous verification request.
		/// </summary>
		/// <param name="ar">The ar.</param>
		/// <returns></returns>
		public VerificationResult EndVerify(IAsyncResult ar)
		{
			VerifyDelegate verifyDelegate = ((AsyncResult)ar).AsyncDelegate as VerifyDelegate;
			if (verifyDelegate != null)
			{
				return verifyDelegate.EndInvoke(ar);
			}
			else
				throw new InvalidOperationException("cannot get results, asynchresult is null");
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Generic routine to send the next C-ECHO-RQ message.
		/// </summary>
		/// <param name="client">DICOM Client class</param>
		/// <param name="association">Association Parameters</param>
		private void SendVerificationRequest(DicomClient client, ClientAssociationParameters association)
		{
			byte pcid = association.FindAbstractSyntax(SopClass.VerificationSopClass);

			client.SendCEchoRequest(pcid, client.NextMessageID());
		}
		#endregion

		#region Overridden Methods...
		/// <summary>
		/// Called when received associate accept.  We send the verificationrequest.
		/// </summary>
		/// <param name="client">The client.</param>
		/// <param name="association">The association.</param>
		public override void OnReceiveAssociateAccept(DicomClient client, ClientAssociationParameters association)
		{
			base.OnReceiveAssociateAccept(client, association);
			if (Canceled)
				client.SendAssociateAbort(DicomAbortSource.ServiceUser,DicomAbortReason.NotSpecified);
			else
				SendVerificationRequest(client, association);
		}

		/// <summary>
		/// Called when received response message.  Sets the <see cref="Result"/> property as appropriate.
		/// </summary>
		/// <param name="client">The client.</param>
		/// <param name="association">The association.</param>
		/// <param name="presentationID">The presentation ID.</param>
		/// <param name="message">The message.</param>
		public override void OnReceiveResponseMessage(DicomClient client, ClientAssociationParameters association, byte presentationID, DicomMessage message)
		{
			if (message.Status.Status != DicomState.Success)
			{
                LogAdapter.Logger.ErrorWithFormat("Failure status received in sending verification: {0}", message.Status.Description);
				_verificationResult = VerificationResult.Failed;
			}
			else if (_verificationResult == VerificationResult.Canceled)
			{
                LogAdapter.Logger.Info("Verification was canceled");
			}
			else
			{
                LogAdapter.Logger.Info("Success status received in sending verification!");
				_verificationResult = VerificationResult.Success;
			}
			client.SendReleaseRequest();
			StopRunningOperation();
		}

		/// <summary>
		/// Adds the verification presentation context.
		/// </summary>
		protected override void SetPresentationContexts()
		{
			byte pcid = base.AssociationParameters.FindAbstractSyntax(SopClass.VerificationSopClass);
			if (pcid == 0)
			{
				pcid = base.AssociationParameters.AddPresentationContext(SopClass.VerificationSopClass);

				base.AssociationParameters.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
				base.AssociationParameters.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);
			}
		}
		#endregion

		#region IDisposable Members

		private bool _disposed = false;
		/// <summary>
		/// Disposes the specified disposing.
		/// </summary>
		/// <param name="disposing">if set to <c>true</c> [disposing].</param>
		protected override void Dispose(bool disposing)
		{
			if (_disposed)
				return;
			if (disposing)
			{
				// Dispose of other Managed objects, ie

			}
			// FREE UNMANAGED RESOURCES
			base.Dispose(true);
			_disposed = true;
		}
		#endregion

	}

	/// <summary>
	/// Enumeration fot verification result.
	/// </summary>
	public enum VerificationResult
	{
		/// <summary></summary>
		Failed = 0,

		/// <summary></summary>
		Success = 1,

		/// <summary></summary>
		TimeoutExpired = 2,

		/// <summary></summary>
		Canceled = 3,

        AssociationRejected
	}
}
