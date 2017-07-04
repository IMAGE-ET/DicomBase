using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UIH.RT.TMS.HL7Server
{
    public enum ErrorCode
    {
        MessageAccepted = 0,
        SegmentSequenceError = 100,
        RequiredFieldMissing = 101,
        DataTypeError = 102,
        TableValueNotFound = 103,
        UnsupportedMessageType = 200,
        UnsupportedEventCode = 201,
        UnsupportedProcessingId = 202,
        UnsupportedVersionId = 203,
        UnknownKeyIdentifier = 204,
        DuplicateKeyIdentifier = 205,
        ApplicationRecordLocked = 206,
        ApplicationInternalError = 207,
    }

    public enum ErrorSeverity
    {
        D, //Default
        E, //Error
        F, //Fatal Error
        I, //Information
        W, //Warning
    }

    public class HL7Exception : Exception
    {
        public HL7Exception(string message)
            : base(message.Substring(0, message.Length > 80 ? 80 : message.Length))
        {
        }

        public HL7Exception(string message, ErrorCode hl7ErrorCode, ErrorSeverity severity)
            : base(message.Substring(0, message.Length > 80 ? 80 : message.Length))
        {
            ErrorCode = hl7ErrorCode;
            Severity = severity;
        }

        public ErrorCode ErrorCode { get; private set; }

        public ErrorSeverity Severity { get; private set; }
    }
}