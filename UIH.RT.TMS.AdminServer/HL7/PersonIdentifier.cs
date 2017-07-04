/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: yuxuan.duan@united-imaging.com
////
//// File: PersonIdentifier.cs
////
//// Summary: PersonIdentifier
//// 
//// Date: 12/25/2014 4:51:17 PM
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIH.RT.TMS.HL7Server.HL7
{
    public class PersonIdentifier
    {
        public PersonIdentifier()
        {
        }

        public PersonIdentifier(string id, Identifier assigningAuthority)
        {
            this.Id = id;
            this.AssigningAuthority = assigningAuthority;
        }

        public string Id { get; set; }

        public Identifier AssigningAuthority { get; set; }

        public string IdentifierTypeCode { get; set; }

        public Identifier AssigningFacility { get; set; }

        //public string PatientUid
        //{
        //    get
        //    {
        //        return string.Format("{0}^^^{1}", Id, AssigningAuthority.GetAuthorityNameString());
        //    }
        //}
    }
}
