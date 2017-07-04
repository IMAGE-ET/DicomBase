/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: yuxuan.duan@united-imaging.com
////
//// File: DM_HL7_Identifier.cs
////
//// Summary: DM_HL7_Identifier
//// 
//// Date: 12/25/2014 5:01:02 PM
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIH.RT.TMS.HL7Server.HL7
{
    public class Identifier
    {
        public Identifier(string namespaceId, string universalId, string universalType)
        {
            if (namespaceId != null)
            {
                this.NamespaceId = namespaceId.Trim();
            }

            if (universalId != null)
            {
                this.UniversalId = universalId.Trim();
            }

            if (universalType != null)
            {
                this.UniversalIdType = universalType.Trim();
            }

            if (this.UniversalId != null)
            {
                this.UniversalKey = this.UniversalId;
                if (this.UniversalIdType != null)
                {
                    this.UniversalKey = this.UniversalKey + "&" + this.UniversalIdType;
                }
            }
        }

        private string _namespaceId;
        public string NamespaceId
        {
            get
            {
                return this._namespaceId;
            }

            set
            {
                this._namespaceId = value != null ? value.Trim() : null;
            }
        }

        private string _universalId;
        public string UniversalId
        {
            get
            {
                return this._universalId;
            }

            set
            {
                this._universalId = value != null ? value.Trim() : null;

                if (this._universalId != null)
                {
                    this.UniversalKey = this.UniversalId;
                    if (this.UniversalIdType != null)
                    {
                        this.UniversalKey = this.UniversalKey + "&" + this.UniversalIdType;
                    }
                }
            }
        }

        private string _universalType;
        public string UniversalIdType
        {
            get
            {
                return this._universalType;
            }

            set
            {
                this._universalType = value != null ? value.Trim() : null;

                if (this._universalId != null)
                {
                    this.UniversalKey = this.UniversalId;
                    if (this.UniversalIdType != null)
                    {
                        this.UniversalKey = this.UniversalKey + "&" + this.UniversalIdType;
                    }
                }
            }
        }

        private string UniversalKey { get; set; }

        public void AddPatientId(PatientId pid, string newId)
        {
            if (this.NamespaceId != null)
            {
                pid.AddId(this.NamespaceId, newId);
            }

            if (this.UniversalKey != null)
            {
                pid.AddId(this.UniversalKey, newId);
            }
        }

        public string GetAuthorityNameString()
        {
            StringBuilder sb = new StringBuilder();
            if (this._namespaceId != null)
            {
                sb.Append(this._namespaceId);
            }

            if (this._universalId != null)
            {
                sb.Append("&");
                sb.Append(this._universalId);
                if (this.UniversalIdType != null)
                {
                    sb.Append("&");
                    sb.Append(this.UniversalIdType);
                }
            }

            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            Identifier id = obj as Identifier;
            if (id != null)
            {
                if (id.NamespaceId != null && this.NamespaceId != null)
                {
                    return this.NamespaceId.Equals(id.NamespaceId, StringComparison.OrdinalIgnoreCase);
                }

                if (id.UniversalId != null && this.UniversalId != null && id.UniversalIdType != null
                    && this.UniversalIdType != null)
                {
                    return this.UniversalId.Equals(id.UniversalId, StringComparison.OrdinalIgnoreCase)
                           && this.UniversalIdType.Equals(id.UniversalIdType, StringComparison.OrdinalIgnoreCase);
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            int result = 17;

            if (this.NamespaceId != null)
            {
                result = (37 * result) + this.NamespaceId.GetHashCode();
            }

            if (this.UniversalId != null)
            {
                result = (37 * result) + this.UniversalId.GetHashCode();
            }

            if (this.UniversalIdType != null)
            {
                result = (37 * result) + this.UniversalIdType.GetHashCode();
            }

            return result;
        }
    }
}
