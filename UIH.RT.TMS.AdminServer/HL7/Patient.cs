/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: yuxuan.duan@united-imaging.com
////
//// File: Patient.cs
////
//// Summary: Patient
//// 
//// Date: 12/25/2014 5:21:10 PM
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIH.RT.TMS.HL7Server.HL7
{
    public class Patient
    {
        public Patient()
        {
            PatientIds = new List<PatientIdentifier>();
        }

        public List<PatientIdentifier> PatientIds { get; private set; }
    }
}
