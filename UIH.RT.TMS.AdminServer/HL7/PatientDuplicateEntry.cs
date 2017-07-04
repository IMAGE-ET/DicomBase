using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UIH.RT.TMS.HL7Server
{
    public class PatientDuplicateEntry
    {
        public PatientDuplicateEntry()
        {
            OldPatients = new List<PatientIdentityFeedRecord>();
        }

        public PatientIdentityFeedRecord NewPatient { get; set; }

        public List<PatientIdentityFeedRecord> OldPatients { get; set; }
    }
}