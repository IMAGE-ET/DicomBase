/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: yuxuan.duan@united-imaging.com
////
//// File: PatientIdentifier.cs
////
//// Summary: PatientIdentifier
//// 
//// Date: 12/25/2014 5:17:17 PM
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIH.RT.TMS.HL7Server.HL7
{
    public class PatientIdentifier : PersonIdentifier
    {
        public PatientIdentifier()
        {
        }

        public PatientIdentifier(string id, Identifier assigningAuthority)
            : base(id, assigningAuthority)
        {
        }
    }
}
