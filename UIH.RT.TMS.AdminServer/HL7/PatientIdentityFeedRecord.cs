using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UIH.RT.TMS.HL7Server
{
    public enum PatientIdentityFeedResultCode
    {
        Success,

        Failure,

        PatientAlreadyExists,

        PatientNotFound
    }

    public class PatientIdentityFeedRecord
    {
        public PatientIdentityFeedRecord()
        {
            Root = string.Empty;
            Extension = string.Empty;
        }

        public string Root { get; set; }

        public string Extension { get; set; }

        public string PatientUid
        {
            get
            {
                return string.Format("{0}^^^&{1}&ISO", Root, Extension);
            }
        }

        public string PatientId { get; set; }

        public PatientIdentityFeedResultCode ResultCode { get; set; }
    }
}