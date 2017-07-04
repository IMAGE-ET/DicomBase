/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: yuxuan.duan@united-imaging.com
////
//// File: HL7Header.cs
////
//// Summary: HL7Header
//// 
//// Date: 12/25/2014 4:34:13 PM
//////////////////////////////////////////////////////////////////////////

using NHapi.Base.Model;

namespace UIH.RT.TMS.HL7Server.HL7
{
    public class HL7Header
    {
        private readonly NHapi.Model.V231.Segment.MSH _msh231;

        private readonly NHapi.Model.V25.Segment.MSH _msh25;

        public HL7Header(IMessage msgIn)
        {
            if (msgIn.Version.Equals("2.3.1"))
            {
                _msh231 = (NHapi.Model.V231.Segment.MSH)msgIn.GetStructure("MSH");
                this.SendingApplicaiton = new Identifier(
                    _msh231.SendingApplication.NamespaceID.Value,
                    _msh231.SendingApplication.UniversalID.Value,
                    _msh231.SendingApplication.UniversalIDType.Value);
                this.SendingFacility = new Identifier(
                    _msh231.SendingFacility.NamespaceID.Value,
                    _msh231.SendingFacility.UniversalID.Value,
                    _msh231.SendingFacility.UniversalIDType.Value);
                this.ReceivingApplication = new Identifier(
                    _msh231.ReceivingApplication.NamespaceID.Value,
                    _msh231.ReceivingApplication.UniversalID.Value,
                    _msh231.ReceivingApplication.UniversalIDType.Value);
                this.ReceivingFacility = new Identifier(
                    _msh231.ReceivingFacility.NamespaceID.Value,
                    _msh231.ReceivingFacility.UniversalID.Value,
                    _msh231.ReceivingFacility.UniversalIDType.Value);
                this.MessageControlId = _msh231.MessageControlID.Value;
                this.MessageCode = _msh231.MessageType.MessageType.Value;
                this.TriggerEvent = _msh231.MessageType.TriggerEvent.Value;
                this.MessageStructure = _msh231.MessageType.MessageStructure.Value;
                this.MessageDate = _msh231.DateTimeOfMessage.TimeOfAnEvent.Value;
            }
            else
            {
                _msh25 = (NHapi.Model.V25.Segment.MSH)msgIn.GetStructure("MSH");
                this.SendingApplicaiton = new Identifier(
                    _msh25.SendingApplication.NamespaceID.Value,
                    _msh25.SendingApplication.UniversalID.Value,
                    _msh25.SendingApplication.UniversalIDType.Value);
                this.SendingFacility = new Identifier(
                    _msh25.SendingFacility.NamespaceID.Value,
                    _msh25.SendingFacility.UniversalID.Value,
                    _msh25.SendingFacility.UniversalIDType.Value);
                this.ReceivingApplication = new Identifier(
                    _msh25.ReceivingApplication.NamespaceID.Value,
                    _msh25.ReceivingApplication.UniversalID.Value,
                    _msh25.ReceivingApplication.UniversalIDType.Value);
                this.ReceivingFacility = new Identifier(
                    _msh25.ReceivingFacility.NamespaceID.Value,
                    _msh25.ReceivingFacility.UniversalID.Value,
                    _msh25.ReceivingFacility.UniversalIDType.Value);
                this.MessageControlId = _msh25.MessageControlID.Value;
                this.MessageCode = _msh25.MessageType.MessageCode.Value;
                this.TriggerEvent = _msh25.MessageType.TriggerEvent.Value;
                this.MessageStructure = _msh25.MessageType.MessageStructure.Value;
                this.MessageDate = _msh25.DateTimeOfMessage.Time.Value;
            }
        }

        public Identifier SendingApplicaiton { get; private set; }

        public Identifier SendingFacility { get; private set; }

        public Identifier ReceivingApplication { get; private set; }

        public Identifier ReceivingFacility { get; private set; }

        public string MessageControlId { get; private set; }

        public string MessageCode { get; private set; }

        public string TriggerEvent { get; private set; }

        public string MessageStructure { get; private set; }

        public string MessageDate { get; private set; }
    }
}
