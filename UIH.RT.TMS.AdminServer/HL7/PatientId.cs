/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: yuxuan.duan@united-imaging.com
////
//// File: PatientID.cs
////
//// Summary: PatientID
//// 
//// Date: 12/25/2014 5:05:18 PM
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIH.RT.TMS.HL7Server.HL7
{
    public class PatientId
    {
        private readonly Dictionary<string, List<string>> _idTable = new Dictionary<string, List<string>>();

        // private string _localId = null;

        public void AddId(string homeSystem, string homeId)
        {
            if (null == homeSystem)
            {
                return;
            }

            if (null == homeId)
            {
                this._idTable.Remove(homeSystem);
                return;
            }

            List<string> ids = this._idTable[homeSystem];
            if (ids == null)
            {
                ids = new List<string>();
                this._idTable.Add(homeSystem, ids);
            }

            ids.Add(homeId);

        }
    }
}
