/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: StorageCommitScu.cs
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
using UIH.RT.Framework.Utility;
using UIH.RT.TMS.Common;

namespace UIH.RT.TMS.Dicom.Network.Scu
{
	/// <summary>
	/// WARNING:  THIS IS TEST CODE AND NOT A COMPLETE STORAGE COMMITMENT SCU IMPLEMENTATION AT THIS TIME!!
	/// </summary>
	public class StorageCommitScu : ScuBase
	{
		#region Public Events/Delegates...
		/// <summary>
		/// Delegate for starting Send in ASynch mode with <see cref="StorageCommitScu.BeginCommit"/>.
		/// </summary>
		public delegate void CommitDelegate();
		#endregion

		#region Private Variables...
		private List<StorageInstance> _storageInstanceList = new List<StorageInstance>();
		#endregion

		#region Constructors
		public StorageCommitScu(string localAe, string remoteAe, string remoteHost, int remotePort)
		{
			ClientAETitle = localAe;
			RemoteAE = remoteAe;
			RemoteHost = remoteHost;
			RemotePort = remotePort;
		}
		#endregion

		#region Public Properties...
		/// <summary>
		/// Gets or sets the file list, which contains a list of all the files.
		/// </summary>
		/// <value>The file list.</value>
		public List<StorageInstance> StorageInstanceList
		{
			get { return _storageInstanceList; }
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Adds the specified file to <see cref="StorageInstanceList"/>.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		public void AddFile(string fileName)
		{
			AddStorageInstance(new StorageInstance(fileName));
		}

		/// <summary>
		/// Adds the specified storage instanceto <see cref="StorageInstanceList"/>.
		/// </summary>
		/// <param name="storageInstance">The storage instance.</param>
		public void AddStorageInstance(StorageInstance storageInstance)
		{
			StorageInstanceList.Add(storageInstance);
		}

		/// <summary>
		/// Add a list of <see cref="StorageInstance"/>s to transfer with the class.
		/// </summary>
		/// <param name="list">The list of storage instances to transfer.</param>
		public void AddStorageInstanceList(IList<StorageInstance> list)
		{
			if (list != null)
				StorageInstanceList.AddRange(list);
		}

		/// <summary>
		/// Sends the SOP Instances in the <see cref="StorageInstanceList"/>.
		/// </summary>
		public void Commit()
		{
			if (_storageInstanceList == null)
			{
				string message =
					String.Format(
						"Attempting to open a Storage Commitment association from {0} to {1} without setting the files to commit.",
						ClientAETitle, RemoteAE);
				LogAdapter.Logger.Error(message);
				throw new ApplicationException(message);
			}

			if (_storageInstanceList.Count == 0)
			{
				string message =
					String.Format("Not creating DICOM Storage Commitment SCU connection from {0} to {1}, no files to commit.",
					              ClientAETitle, RemoteAE);
				LogAdapter.Logger.Error(message);
				throw new ApplicationException(message);
			}

            LogAdapter.Logger.InfoWithFormat("Preparing to connect to AE {0} on host {1} on port {2} and committing {3} images.",
			             RemoteAE, RemoteHost, RemotePort, _storageInstanceList.Count);

			// the connect launches the actual send in a background thread.
			Connect();
		}

		/// <summary>
		/// Begins a storage commitment association with the files in <see cref="StorageInstanceList"/> in 
		/// asynchronous mode.  See the example in the class comment
		/// for an example on how to use this.
		/// </summary>
		/// <param name="callback">The callback.</param>
		/// <param name="asyncState">State of the async.</param>
		/// <returns></returns>
		public IAsyncResult BeginCommit(AsyncCallback callback, object asyncState)
		{
			CommitDelegate commitDelegate = new CommitDelegate(this.Commit);
			return commitDelegate.BeginInvoke(callback, asyncState);
		}

		/// <summary>
		/// Ends the storage commit (asynchronous mode).  See the example in the class comment
		/// for an example on how to use this.
		/// </summary>
		/// <param name="ar">The ar.</param>
		public void EndCommit(IAsyncResult ar)
		{
			CommitDelegate commitDelegate = ((System.Runtime.Remoting.Messaging.AsyncResult)ar).AsyncDelegate as CommitDelegate;

			if (commitDelegate != null)
			{
				commitDelegate.EndInvoke(ar);
			}
			else
			{
				throw new InvalidOperationException("cannot end invoke, asynchresult is null");
			}
		}
		#endregion

		#region Protected Overridden Methods...
		/// <summary>
		/// Scan the files to send, and create presentation contexts for each abstract syntax to send.
		/// </summary>
		protected override void SetPresentationContexts()
		{
			byte pcid = AssociationParameters.AddPresentationContext(SopClass.StorageCommitmentPushModelSopClass);

			AssociationParameters.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
			AssociationParameters.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

			pcid = AssociationParameters.AddPresentationContext(SopClass.MrImageStorage);

			AssociationParameters.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
			AssociationParameters.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

		}

		/// <summary>
		/// Called when received associate accept.  For StorageScu, we then attempt to send the first file.
		/// </summary>
		/// <param name="client">The client.</param>
		/// <param name="association">The association.</param>
		public override void OnReceiveAssociateAccept(DicomClient client, ClientAssociationParameters association)
		{
			base.OnReceiveAssociateAccept(client, association);

            LogAdapter.Logger.InfoWithFormat("Association Accepted:\r\n{0}", association.ToString());

			byte pcid = association.FindAbstractSyntaxWithTransferSyntax(SopClass.StorageCommitmentPushModelSopClass,
			                                                             TransferSyntax.ExplicitVrLittleEndian);
			if (pcid == 0)
				pcid = association.FindAbstractSyntaxWithTransferSyntax(SopClass.StorageCommitmentPushModelSopClass,
				                                                        TransferSyntax.ImplicitVrLittleEndian);
			if (pcid == 0)
			{
				client.SendAssociateAbort(DicomAbortSource.ServiceUser, DicomAbortReason.NotSpecified);
				return;
			}

			DicomMessage msg = new DicomMessage();

			msg.RequestedSopInstanceUid = "1.2.840.10008.1.20.1.1";
			msg.ActionTypeId = 1;
			msg.DataSet[DicomTags.TransactionUid].SetStringValue(DicomUid.GenerateUid().UID);

			foreach (StorageInstance instance in StorageInstanceList)
			{
				DicomSequenceItem item = new DicomSequenceItem();

				msg.DataSet[DicomTags.ReferencedSopSequence].AddSequenceItem(item);

				item[DicomTags.ReferencedSopClassUid].SetStringValue(instance.SopClass.Uid);
				item[DicomTags.ReferencedSopInstanceUid].SetStringValue(instance.SopInstanceUid);
			}

		}

		/// <summary>
		/// Called when received response message.  If there are more files to send, will send them here.
		/// </summary>
		/// <param name="client">The client.</param>
		/// <param name="association">The association.</param>
		/// <param name="presentationID">The presentation ID.</param>
		/// <param name="message">The message.</param>
		public override void OnReceiveResponseMessage(DicomClient client, ClientAssociationParameters association, byte presentationID, DicomMessage message)
		{
			if (Status == ScuOperationStatus.Canceled)
			{
                LogAdapter.Logger.InfoWithFormat("Cancel request received, releasing association from {0} to {1}", association.CallingAE, association.CalledAE);
				client.SendReleaseRequest();
				StopRunningOperation();
				return;
			}
		}

		/// <summary>
		/// Called when [receive request message].
		/// </summary>
		/// <param name="client">The client.</param>
		/// <param name="association">The association.</param>
		/// <param name="presentationID">The presentation ID.</param>
		/// <param name="message">The message.</param>
		public override void OnReceiveRequestMessage(DicomClient client, ClientAssociationParameters association, byte presentationID, DicomMessage message)
		{
			if (message.CommandField == DicomCommandField.NEventReportRequest)
			{
				LogAdapter.Logger.Info("N-EVENT-REPORT-RQ messages currently not supported by StorageCommitScu.  Aborting connection.");
				client.SendAssociateAbort(DicomAbortSource.ServiceUser, DicomAbortReason.NotSpecified);
				StopRunningOperation(ScuOperationStatus.UnexpectedMessage);
				throw new Exception("The method or operation is not implemented.");
			}
			else
			{
				LogAdapter.Logger.Info("Unexpected OnReceiveRequestMessage callback on client.");
				try
				{
					client.SendAssociateAbort(DicomAbortSource.ServiceUser, DicomAbortReason.NotSpecified);
				}
				catch (Exception ex)
				{
					LogAdapter.Logger.TraceException(ex);
				}
				StopRunningOperation(ScuOperationStatus.UnexpectedMessage);
				throw new Exception("The method or operation is not implemented.");
			}
		}
		#endregion
	}
}
