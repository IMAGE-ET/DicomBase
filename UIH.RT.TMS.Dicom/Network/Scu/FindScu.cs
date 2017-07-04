/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: FindScu.cs
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
using System.Globalization;
using System.Runtime.Remoting.Messaging;
using UIH.RT.Framework.Utility;
using UIH.RT.TMS.Common;
using UIH.RT.TMS.Dicom.Iod;
using UIH.RT.TMS.Dicom.Iod.Iods;

namespace UIH.RT.TMS.Dicom.Network.Scu
{

    #region FindScuBase

    /// <summary>
    /// FindScuBase class which does all the work of Find requests.  Subclasses should override the <see cref="FindSopClass"/> property.
    /// See <see cref="PatientRootFindScu"/>, <see cref="StudyRootFindScu"/>, and <see cref="PatientStudyOnlyFindScu"/>.
    /// </summary>
    public abstract class FindScuBase : ScuBase
    {
        #region Public Events/Delegates

        public delegate IList<DicomDataset> FindDelegate(
            string clientAETitle, string remoteAE, string remoteHost, int remotePort, DicomDataset requestDataset);

        public delegate IList<T> GenericFindDelegate<T>(
            string clientAETitle, string remoteAE, string remoteHost, int remotePort, T iod);

        #endregion

        #region Private Variables

        /// <summary>
        /// Attribute Collection of the request.
        /// </summary>
        protected DicomDataset _requestDataset;

        /// <summary>
        /// Results List (of DicomDataset)
        /// </summary>
        private List<DicomDataset> _results;

        /// <summary>
        /// Association isn't closed and is left open for the next find request when set to true.
        /// </summary>
        private bool _reuseAssociation = false;

        /// <summary>
        /// Maximum results to receive before sending a C-CANCEL-RQ message.
        /// </summary>
        private int _maxResults = -1;

        // Flag if a C-CANCEL-RQ has already been sent for an association.
        private bool _cancelSent = false;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the results of the find request.
        /// </summary>
        /// <value>The results.</value>
        public List<DicomDataset> Results
        {
            get { return _results; }
        }

        /// <summary>
        /// Specifies the find sop class.
        /// </summary>
        /// <value>The find sop class.</value>
        /// <remarks>Abstract so subclass can specify.</remarks>
        public abstract SopClass FindSopClass { get; }

        /// <summary>
        /// When set to true, the association will be left open after Find() completes for reuse.
        /// </summary>
        public bool ReuseAssociation
        {
            get { return _reuseAssociation; }
            set { _reuseAssociation = value; }
        }

        /// <summary>
        /// When set to -1, FindScu will receive all results.  When set to a positive number,
        /// FindScu will send a C-CANCEL-RQ message after receiving the specified number of 
        /// responses.
        /// </summary>
        public int MaxResults
        {
            get { return _maxResults; }
            set { _maxResults = value; }
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets the request attribute collection.
        /// </summary>
        /// <value>The request attribute collection.</value>
        protected DicomDataset RequestDataset
        {
            get { return _requestDataset; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Close the association, if <see cref="ReuseAssociation"/> is in use.
        /// </summary>
        public void CloseAssociation()
        {
            if (Status == ScuOperationStatus.Running && ReuseAssociation)
                _dicomClient.SendReleaseRequest();
        }

        /// <summary>
        /// Trace from: 44133 SSFS_PRA_APP_Import_CommunicationPrototal\n
        /// Description: Performs the find request to the specified remote dicom server with the specified attributes.
        /// </summary>
        /// <param name="clientAETitle">The client AE title.</param>
        /// <param name="remoteAE">The remote AE.</param>
        /// <param name="remoteHost">The remote host.</param>
        /// <param name="remotePort">The remote port.</param>
        /// <param name="requestDataset">The request attribute collection.</param>
        /// <returns></returns>
        public IList<DicomDataset> Find(string clientAETitle, string remoteAE, string remoteHost, int remotePort,
                                        DicomDataset requestDataset)
        {
            if (requestDataset == null)
                throw new ArgumentNullException("requestDataset");

            _requestDataset = requestDataset;
            if (!ValidateQuery())
                throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture,
                                                                  "Invalid query for SCU type {0}",
                                                                  GetType()
                                                                      .FullName.Substring(
                                                                          GetType().FullName.LastIndexOf(".") + 1)));

            _results = new List<DicomDataset>();

            if (ReuseAssociation && Status == ScuOperationStatus.Running)
            {
                ProgressEvent.Reset();

                SendFindRequest(_dicomClient, _associationParameters);

                ProgressEvent.WaitOne();
            }
            else
            {
                ClientAETitle = clientAETitle;
                RemoteAE = remoteAE;
                RemoteHost = remoteHost;
                RemotePort = remotePort;

                Connect();
            }

            return _results;
        }

        
        /// <summary>
        /// Begins the find request in asynchronous mode.
        /// </summary>
        /// <param name="clientAETitle">The client AE title.</param>
        /// <param name="remoteAE">The remote AE.</param>
        /// <param name="remoteHost">The remote host.</param>
        /// <param name="remotePort">The remote port.</param>
        /// <param name="requestDataset">The request attribute collection.</param>
        /// <param name="callback">The callback.</param>
        /// <param name="asyncState">State of the async.</param>
        /// <returns></returns>
        public IAsyncResult BeginFind(string clientAETitle, string remoteAE, string remoteHost, int remotePort,
                                      DicomDataset requestDataset, AsyncCallback callback, object asyncState)
        {
            FindDelegate findDelegate = this.Find;

            return findDelegate.BeginInvoke(clientAETitle, remoteAE, remoteHost, remotePort, requestDataset, callback,
                                            asyncState);
        }

        /// <summary>
        /// Ends the asynchronous find request.
        /// </summary>
        /// <param name="ar">The ar.</param>
        /// <returns></returns>
        public IList<DicomDataset> EndFind(IAsyncResult ar)
        {
            FindDelegate findDelegate = ((AsyncResult) ar).AsyncDelegate as FindDelegate;
            if (findDelegate != null)
            {
                return findDelegate.EndInvoke(ar);
            }
            else
                throw new InvalidOperationException("cannot get results, asynchresult is null");
        }

        /// <summary>
        /// Ends the asynchronous find request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ar">The ar.</param>
        /// <returns></returns>
        public IList<T> EndFind<T>(IAsyncResult ar)
        {
            GenericFindDelegate<T> findDelegate = ((AsyncResult) ar).AsyncDelegate as GenericFindDelegate<T>;
            if (findDelegate != null)
            {
                return findDelegate.EndInvoke(ar);
            }
            else
                throw new InvalidOperationException("cannot get results, asynchresult is null");
        }

        #endregion

        #region Protected Abstract Methods

        protected abstract bool ValidateQuery();

        #endregion

        #region Private Methods

        /// <summary>
        /// Sends the find request.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="association">The association.</param>
        private void SendFindRequest(DicomClient client, ClientAssociationParameters association)
        {
            byte pcid = association.FindAbstractSyntax(FindSopClass);

            if (pcid > 0)
            {
                DicomMessage dicomMessage = new DicomMessage(new DicomDataset(), RequestDataset.Copy());
                client.SendCFindRequest(pcid, client.NextMessageID(), dicomMessage);
            }
        }

        #endregion

        #region Overridden Methods

        /// <summary>
        /// Called when received associate accept.  here is where send the find request.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="association">The association.</param>
        public override void OnReceiveAssociateAccept(DicomClient client, ClientAssociationParameters association)
        {
            base.OnReceiveAssociateAccept(client, association);
            if (Canceled)
                client.SendAssociateAbort(DicomAbortSource.ServiceUser, DicomAbortReason.NotSpecified);
            else
                SendFindRequest(client, association);
        }

        /// <summary>
        /// Called when received response message.  Sets the <see cref="Results"/> property as appropriate.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="association">The association.</param>
        /// <param name="presentationID">The presentation ID.</param>
        /// <param name="message">The message.</param>
        public override void OnReceiveResponseMessage(DicomClient client, ClientAssociationParameters association,
                                                      byte presentationID, DicomMessage message)
        {
            if (message.Status.Status == DicomState.Pending)
            {
                // one result sent back, add to result list
                _results.Add(message.DataSet);
                if (_maxResults != -1 && !_cancelSent && _results.Count > _maxResults)
                {
                    client.SendCFindCancelRequest(presentationID, message.MessageIdBeingRespondedTo);
                    _cancelSent = true;
                }
            }
            else
            {
                _cancelSent = false;
                DicomStatus status = DicomStatuses.LookupQueryRetrieve(message.Status.Code);
                if (status.Status != DicomState.Success)
                {
                    if (status.Status == DicomState.Cancel)
                    {
                        if (LogInformation)
                            LogAdapter.Logger.InfoWithFormat("Cancel status received in Find Scu: {0}", status);
                        Status = ScuOperationStatus.Canceled;
                    }
                    else if (status.Status == DicomState.Failure)
                    {
                        LogAdapter.Logger.ErrorWithFormat("Failure status received in Find Scu: {0}", status);
                        Status = ScuOperationStatus.Failed;
                        FailureDescription = status.ToString();
                    }
                    else if (status.Status == DicomState.Warning)
                    {
                        LogAdapter.Logger.WarnWithFormat("Warning status received in Find Scu: {0}", status);
                    }
                    else if (Status == ScuOperationStatus.Canceled)
                    {
                        if (LogInformation) LogAdapter.Logger.Info("Client cancelled Find Scu operation.");
                    }
                }
                else
                {
                    if (LogInformation) LogAdapter.Logger.Info("Success status received in Find Scu!");
                }
                if (!ReuseAssociation)
                {
                    client.SendReleaseRequest();
                    StopRunningOperation();
                }
                else
                {
                    ProgressEvent.Set();
                }
            }
        }

        /// <summary>
        /// Adds the appropriate Patient Root presentation context.
        /// </summary>
        protected override void SetPresentationContexts()
        {
            byte pcid = AssociationParameters.FindAbstractSyntax(FindSopClass);
            if (pcid == 0)
            {
                pcid = AssociationParameters.AddPresentationContext(FindSopClass);

                AssociationParameters.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
                AssociationParameters.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);
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

    #endregion

    #region PatientRootFindScu Class

    /// <summary>
    /// Patient Root Find Scu
    /// <para>
    /// <example>
    /// <code><![CDATA[
    /// // Example with manually adding requested attributes
    ///  PatientRootFindScu patientRootFindScu = new PatientRootFindScu();
    /// DicomDataset DicomDataset = new DicomDataset();
    ///  DicomDataset[DicomTags.QueryRetrieveLevel].SetStringValue("PATIENT");
    ///  DicomDataset[DicomTags.PatientsName].SetString(0, "*");
    ///  DicomDataset[DicomTags.PatientId].SetNullValue();
    ///  DicomDataset[DicomTags.AccessionNumber].SetNullValue();
    ///  DicomDataset[DicomTags.StudyDate].SetNullValue();
    ///  patientRootFindScu.Find("myClientAeTitle", "MyServerAeTitle", "127.0.0.1", 5678, DicomDataset);
    /// ]]></code>
    /// </example>
    /// </para>
    /// </summary>
    public class PatientRootFindScu : FindScuBase
    {
        #region Public Properties

        /// <summary>
        /// Specifies the find sop class (PatientRootQueryRetrieveInformationModelFind)
        /// </summary>
        /// <value>The find sop class.</value>
        /// <remarks>Abstract so subclass can specify.</remarks>
        public override SopClass FindSopClass
        {
            get { return SopClass.PatientRootQueryRetrieveInformationModelFind; }
        }

        /// <summary>
        /// Gets the query retrieve level.
        /// </summary>
        /// <value>The query retrieve level.</value>
        public QueryRetrieveLevel QueryRetrieveLevel
        {
            get
            {
                if (RequestDataset != null)
                    return IodBase.ParseEnum(RequestDataset[DicomTags.QueryRetrieveLevel].GetString(0, String.Empty),
                                             QueryRetrieveLevel.None);
                else
                    return QueryRetrieveLevel.None;
            }
        }

        #endregion

        #region Protected Overridden Methods

        /// <summary>
        /// Determines whether [is query level valid].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is query level valid]; otherwise, <c>false</c>.
        /// </returns>
        protected override bool ValidateQuery()
        {
            // Patient root is always valid unless none
            return (QueryRetrieveLevel != QueryRetrieveLevel.None);
        }

        #endregion
    }

    #endregion

    #region StudyRootFindScu Class

    /// <summary>
    /// Study Root Find Scu
    /// <para>
    /// <example>
    /// FindScuBase findScu = new StudyRootFindScu();
    /// QueryResultIod queryResultIod = new QueryResultIod();
    /// queryResultIod.SetCommonTags(QueryRetrieveLevel.Study);
    /// patientRootFindScu.Find("myClientAeTitle", "MyServerAeTitle", "127.0.0.1", 5678, queryResultIod);
    /// </example>
    /// </para>
    /// <para>
    /// <example>
    ///  StudyRootFindScu findScu = new StudyRootFindScu();
    /// DicomDataset DicomDataset = new DicomDataset();
    ///  DicomDataset[DicomTags.QueryRetrieveLevel].SetStringValue("STUDY");
    ///  DicomDataset[DicomTags.PatientsName].SetString(0, "*");
    ///  DicomDataset[DicomTags.PatientId].SetNullValue();
    ///  DicomDataset[DicomTags.AccessionNumber].SetNullValue();
    ///  DicomDataset[DicomTags.StudyDate].SetNullValue();
    ///  DicomDataset[DicomTags.StudyDate].SetNullValue();
    ///  DicomDataset[DicomTags.StudyInstanceUid].SetNullValue();
    ///  DicomDataset[DicomTags.StudyId].SetNullValue();
    ///  findScu.Find("myClientAeTitle", "MyServerAeTitle", "127.0.0.1", 5678, DicomDataset);
    /// </example>
    /// </para>
    /// <para>See <see cref="PatientRootFindScu"/> for more examples.</para>
    /// </summary>
    public class StudyRootFindScu : FindScuBase
    {
        #region Public Properties

        /// <summary>
        /// Specifies the find sop class (StudyRootQueryRetrieveInformationModelFind)
        /// </summary>
        /// <value>The find sop class.</value>
        public override SopClass FindSopClass
        {
            get { return SopClass.StudyRootQueryRetrieveInformationModelFind; }
        }

        /// <summary>
        /// Gets the query retrieve level.
        /// </summary>
        /// <value>The query retrieve level.</value>
        public QueryRetrieveLevel QueryRetrieveLevel
        {
            get
            {
                if (RequestDataset != null)
                    return IodBase.ParseEnum(RequestDataset[DicomTags.QueryRetrieveLevel].GetString(0, String.Empty),
                                             QueryRetrieveLevel.None);
                else
                    return QueryRetrieveLevel.None;
            }
        }

        #endregion

        #region Protected Overridden Methods

        /// <summary>
        /// Determines whether [is query level valid].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is query level valid]; otherwise, <c>false</c>.
        /// </returns>
        protected override bool ValidateQuery()
        {
            // Study root is always valid unless none or Patient
            return (QueryRetrieveLevel != QueryRetrieveLevel.None && QueryRetrieveLevel != QueryRetrieveLevel.Patient);
        }

        #endregion
    }

    #endregion

    #region PatientStudyOnlyFindScu Class

    /// <summary>
    /// Patient Study Root Find Scu
    /// <para>See <see cref="PatientRootFindScu"/> for examples.</para>
    /// </summary>
    public class PatientStudyOnlyFindScu : FindScuBase
    {
        #region Public Properties

        /// <summary>
        /// Specifies the find sop class (PatientStudyOnlyQueryRetrieveInformationModelFindRetired)
        /// </summary>
        /// <value>The find sop class.</value>
        public override SopClass FindSopClass
        {
            get { return SopClass.PatientStudyOnlyQueryRetrieveInformationModelFindRetired; }
        }

        /// <summary>
        /// Gets the query retrieve level.
        /// </summary>
        /// <value>The query retrieve level.</value>
        public QueryRetrieveLevel QueryRetrieveLevel
        {
            get
            {
                if (RequestDataset != null)
                    return IodBase.ParseEnum(RequestDataset[DicomTags.QueryRetrieveLevel].GetString(0, String.Empty),
                                             QueryRetrieveLevel.None);
                else
                    return QueryRetrieveLevel.None;
            }
        }

        #endregion

        #region Protected Overridden Methods

        /// <summary>
        /// Determines whether [is query level valid].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is query level valid]; otherwise, <c>false</c>.
        /// </returns>
        protected override bool ValidateQuery()
        {
            // Patient Study only is only valid unless for patient and study
            return (QueryRetrieveLevel == QueryRetrieveLevel.Patient || QueryRetrieveLevel != QueryRetrieveLevel.Study);
        }

        #endregion
    }

    #endregion
}
