/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: DicomServiceManager.cs
////
//// Summary:
////
////
//// Date: 2014/08/19
//////////////////////////////////////////////////////////////////////////
#region License

// Copyright (c) 2011 - 2014, **** Inc.
// All rights reserved.
// http://www.****.com

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UIH.RT.Framework.Utility;
using UIH.RT.TMS.Common;
using UIH.RT.TMS.Dicom.Network;
using UIH.RT.TMS.Dicom.Network.Scp;
using UIH.RT.TMS.DicomService.Model;

namespace UIH.RT.TMS.DicomService
{
    public abstract class DicomServiceManager : ThreadedService
    {
        #region Private Members

        private readonly List<DicomScp<DicomScpContext>> _listenerList = new List<DicomScp<DicomScpContext>>();
        private IList<ServerPartition> _partitions;
        private readonly object _syncLock = new object();

        #endregion

        #region Private Methods

        protected DicomServiceManager()
            : base(typeof(DicomServiceManager).ToString())
        { 
            
        }

        protected DicomServiceManager(string name)
            : base(name)
        {
            
        }

        protected abstract IList<ServerPartition> CreateServerPartitions();

        protected virtual bool CreateVerify(DicomScpContext context,
                                          ServerAssociationParameters assocParms,
                                          out DicomRejectResult result,
                                          out DicomRejectReason reason)
        {
            return AssociationVerifier.Verify(context,
                assocParms,
                out result,
                out reason);
        }

        private void StartListeners(ServerPartition part)
        {
            var parms = new DicomScpContext(part);

            //TODO support IPV6
            var scp = new DicomScp<DicomScpContext>(parms, CreateVerify)
            {
                ListenPort = part.Port,
                AeTitle = part.AeTitle
            };

            if (scp.Start(IPAddress.Any))
            {
                _listenerList.Add(scp);
                LogAdapter.Logger.InfoWithFormat("Start listen on {0} for server partition {1}",
                    part.Port, part.Description);
            }
            else
            {
                LogAdapter.Logger.InfoWithFormat("Unable to listen on {0} for server partition {1}",
                    part.Port, part.Description);
                LogAdapter.Logger.InfoWithFormat("Unable to listen on {0} for server partition {1}",
                    part.Port, part.Description);
            }
        }

        #endregion

        #region Protected Methods

        protected override bool Initialize()
        {
            // Initialize DICOM Service Parameter Here 
            _partitions = CreateServerPartitions();
            return true;
        }

        
        protected override void Run()
        {
            foreach (var part in _partitions.Where(part => part.Enable))
            {
                StartListeners(part);
            }
        }

        protected override void Stop()
        {
            lock (_syncLock)
            {
                foreach (var scp in _listenerList)
                {
                    scp.Stop();
                }

            }
        }

        #endregion
    }

}
