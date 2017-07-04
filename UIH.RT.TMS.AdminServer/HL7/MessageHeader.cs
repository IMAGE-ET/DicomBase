/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: yuxuan.duan@united-imaging.com
////
//// File: MessageHeader.cs
////
//// Summary: MessageHeader
//// 
//// Date: 12/25/2014 5:18:28 PM
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIH.RT.TMS.HL7Server.HL7
{
    public class MessageHeader
    {
        public Identifier SendingFacility { get; set; }

        public Identifier SendingApplication { get; set; }

        public Identifier ReceivingFacility { get; set; }

        public Identifier ReceivingApplication { get; set; }

        public string MessageCode { get; set; }

        public string TriggerEvent { get; set; }

        public string MessageStructure { get; set; }

        private DateTime MessageDate { get; set; }
    }
}
