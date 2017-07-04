/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging HealthCare Inc
//// All rights reserved. 
//// 
//// author     yuxuan.duan@united-imaging.com
////
//// File:      HL7Handler.cs
////
//// Summary:   
//// 
//// 
//// Date       2014/12/25
//////////////////////////////////////////////////////////////////////////
using System;
using System.Linq;
using System.Text;
using NHapi.Base.Model;
using NHapi.Base.Parser;
using NHapi.Model.V231.Datatype;
using NHapi.Model.V231.Message;
using NHapi.Model.V231.Segment;
using UIH.RT.Framework.DataModel;
using UIH.RT.Framework.Utility;
using UIH.RT.TMS.HL7Server.HL7;

namespace UIH.RT.TMS.HL7Server
{
    public class HL7Handler
    {
        public byte[] ProcessMessage(byte[] request)
        {
            string msgIn = Encoding.UTF8.GetString(request);

            var parser = new PipeParser();

            IMessage msg = parser.Parse(msgIn, "2.3.1");
            HL7Header header = new HL7Header(msg);

            ACK reply = InitAcknowledgment(header);
            XdsPatientServiceImpl patientService = new XdsPatientServiceImpl();

            PID pid231;

            try
            {
                LogAdapter.Logger.Info(string.Format("Process HL7 event {0}", header.TriggerEvent));
                //Platform.Log(LogLevel.Info, "Process HL7 event {0}", header.TriggerEvent);
                switch (header.TriggerEvent)
                {
                    case "A01":
                    case "A04":
                    case "A05": // Insert Patient
                        {
                            pid231 = (PID) msg.GetStructure("PID");
                            PatientIdentifier patientIdentifier = GetPatientIdentifier(pid231);
                            var patientInfo = new PatientInfo() { PatientIdent = patientIdentifier };

                            //if (!PixRegistryConfigration.IsValidDomain(patient.AssigningAuthority))
                            //{
                            //    PopulateErr(reply.ERR, "PID", "1", "3", "204", "Unknown Key Identifier", "Domain don't accept.");
                            //    throw new HL7Exception("Unknown Key Identifier", ErrorCode.UnknownKeyIdentifier, ErrorSeverity.E);
                            //}

                            AnalyzePID(pid231, ref patientInfo);
                            patientService.CreatePatient(patientInfo);
                            break;
                        }

                    case "A08": // Update Patient
                        {
                            pid231 = (PID)msg.GetStructure("PID");
                            PatientIdentifier patientIdentifier = GetPatientIdentifier(pid231);
                            var patientInfo = new PatientInfo() { PatientIdent = patientIdentifier };

                            //if (!PixRegistryConfigration.IsValidDomain(patient.AssigningAuthority))
                            //{
                            //    PopulateErr(reply.ERR, "PID", "1", "3", "204", "Unknown Key Identifier", "Domain don't accept.");
                            //    throw new HL7Exception("Unknown Key Identifier", ErrorCode.UnknownKeyIdentifier, ErrorSeverity.E);
                            //}

                            AnalyzePID(pid231, ref patientInfo);
                            patientService.UpdatePatient(patientInfo);
                            break;
                        }

                    //case "A40": // Merge Patient
                    //    {
                    //        ADT_A40_PATIENT allPatient = (ADT_A40_PATIENT)msg.GetStructure("PATIENT");
                    //        PatientIdentifier patient = GetPatientIdentifier(allPatient.PID);
                    //        PatientIdentifier mergePatient = GetMrgPatientIdentifier(allPatient.MRG);

                    //        //if (!PixRegistryConfigration.IsValidDomain(patient.AssigningAuthority))
                    //        //{
                    //        //    PopulateErr(reply.ERR, "PID", "1", "3", "204", "Unknown Key Identifier", "Domain don't accept.");
                    //        //    throw new HL7Exception("Unknown Key Identifier", ErrorCode.UnknownKeyIdentifier, ErrorSeverity.E);
                    //        //}

                    //        patientService.MergePatient(patient, mergePatient);
                    //        break;
                    //    }

                    default:
                        {
                            const string ErrorMsg =
                                "Unexpected request to XDS-Registry. Valid message type are ADT^A01, ADT^A04, ADT^A05, ADT^A08, ADT^A40";
                            LogAdapter.Logger.Error(ErrorMsg);
                            //Platform.Log(LogLevel.Warn, ErrorMsg);
                            throw new Exception(ErrorMsg);
                        }
                }

                PopulateMSA(reply.MSA, "AA", header.MessageControlId);
            }
            catch (HL7Exception ex)
            {
                LogAdapter.Logger.TraceException(ex);
                //Platform.Log(LogLevel.Warn, ex);
                PopulateMSA(reply.MSA, "AE", header.MessageControlId);
            }
            catch (PatientDonotExistException ex)
            {
                LogAdapter.Logger.TraceException(ex);
                //Platform.Log(LogLevel.Warn, ex);
                PopulateMSA(reply.MSA, "AE", header.MessageControlId);
                PopulateErr(reply.ERR, "PID", "1", "3", "204", "Unknown Key Identifier", ex.Message);
            }
            catch (Exception ex)
            {
                LogAdapter.Logger.TraceException(ex);
                //Platform.Log(LogLevel.Warn, ex);
                PopulateMSA(reply.MSA, "AE", header.MessageControlId);
                PopulateErr(reply.ERR, string.Empty, string.Empty, string.Empty, "207", "Application internal error", ex.Message);
            }

            return Encoding.UTF8.GetBytes(parser.Encode(reply));
        }

        private PatientIdentifier GetPatientIdentifier(PID pid)
        {
            PatientIdentifier identifier = new PatientIdentifier();
            CX[] cxs = pid.GetPatientIdentifierList();

            foreach (var cx in cxs)
            {
                Identifier assignAuth = new Identifier(
                    cx.AssigningAuthority.NamespaceID.Value,
                    cx.AssigningAuthority.UniversalID.Value,
                    cx.AssigningAuthority.UniversalIDType.Value);
                Identifier assignFac = new Identifier(
                    cx.AssigningFacility.NamespaceID.Value,
                    cx.AssigningFacility.UniversalID.Value,
                    cx.AssigningFacility.UniversalIDType.Value);
                identifier.AssigningAuthority = assignAuth;
                identifier.AssigningFacility = assignFac;
                identifier.Id = cx.ID.Value;
                identifier.IdentifierTypeCode = cx.IdentifierTypeCode.Value;
            }

            return identifier;
        }

        private PatientIdentifier GetMrgPatientIdentifier(MRG mrg)
        {
            PatientIdentifier identifier = new PatientIdentifier();
            CX[] cxs = mrg.GetPriorPatientIdentifierList();

            foreach (var cx in cxs)
            {
                Identifier assignAuth = new Identifier(
                    cx.AssigningAuthority.NamespaceID.Value,
                    cx.AssigningAuthority.UniversalID.Value,
                    cx.AssigningAuthority.UniversalIDType.Value);
                Identifier assignFac = new Identifier(
                    cx.AssigningFacility.NamespaceID.Value,
                    cx.AssigningFacility.UniversalID.Value,
                    cx.AssigningFacility.UniversalIDType.Value);
                identifier.AssigningAuthority = assignAuth;
                identifier.AssigningFacility = assignFac;
                identifier.Id = cx.ID.Value;
                identifier.IdentifierTypeCode = cx.IdentifierTypeCode.Value;
            }

            return identifier;
        }

        private void AnalyzePID(PID pid, ref PatientInfo patientInfo)
        {
            IType[] vals;

            #region getPatientName

            var patientName = pid.GetPatientName().FirstOrDefault();
            if (patientName != null)
            {
                patientInfo.FirstName = patientName.GivenName.Value;
                patientInfo.MidName = patientName.MiddleInitialOrName.Value;
                patientInfo.LastName = patientName.FamilyLastName.FamilyName.Value;
                patientInfo.NamePrefix = patientName.PrefixEgDR.Value;
                patientInfo.NameSuffix = patientName.SuffixEgJRorIII.Value;
            }

            #endregion

            #region getPatientBirthday

            vals = pid.GetField((int)PIDSection.Birthday);
            if(vals.Length != 0)
            {
                if(vals[0].TypeName.ToUpper().Equals("TS"))
                {
                    TS ts = (TS)vals[0];
                    string dateString = ts.Components[0].ToString();
                    patientInfo.BirthDate = DateTime.ParseExact(dateString, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                }
            }

            #endregion

            #region getPatientGender

            string gender;
            vals = pid.GetField((int)PIDSection.Gender);
            if(vals.Length != 0)
            {
                gender = vals[0].ToString();
                switch (gender)
                {
                    case "M":
                        patientInfo.Gender = (int)Gender.Male;
                        break;
                    case "F":
                        patientInfo.Gender = (int)Gender.Female;
                        break;
                    case "A":
                    case "N":
                    case "O":
                    case "U":
                        patientInfo.Gender = (int)Gender.Other;
                        break;
                    default:
                        patientInfo.Gender = null;
                        break;
                }
            }

            #endregion

            #region getPatientAddress
            
            var patientAddress = pid.GetPatientAddress().FirstOrDefault();
            if (patientAddress != null)
            {
                patientInfo.HomeCurAddress = patientAddress.StreetAddress.Value;
                patientInfo.HomeCurCity = patientAddress.City.Value;

                string country = patientAddress.Country.Value;
                switch (country)
                {
                    case "CHN":
                    case "HKG":
                    case "MAC":
                    case "TWN":
                        patientInfo.HomeCurCountry = (int)CountryType.China;
                        break;
                    default:
                        patientInfo.HomeCurCountry = (int)CountryType.Foreign;
                        break;
                }

                if (patientInfo.HomeCurCountry == (int)CountryType.China)
                {
                    Province province;
                    if(Enum.TryParse(patientAddress.StateOrProvince.Value, out province))
                        patientInfo.HomeCurProvince = (int)province;//此处可能要修改enum省份的枚举值，符合ISO标准

                }
                else
                {
                    patientInfo.HomeCurState = patientAddress.StateOrProvince.Value;
                }

                patientInfo.HomeCurPostal = patientAddress.ZipOrPostalCode.Value;
            }

            #endregion

            #region getPatientPhoneNum

            var patientHomePhoneNum = pid.GetPhoneNumberHome().FirstOrDefault();
            if (patientHomePhoneNum != null)
            {
                patientInfo.HomeCountryCode = patientHomePhoneNum.CountryCode.Value;
                patientInfo.HomeAreaCode = patientHomePhoneNum.AreaCityCode.Value;
                patientInfo.HomePhone = patientHomePhoneNum.PhoneNumber.Value;
                patientInfo.HomeEmail = patientHomePhoneNum.EmailAddress.Value;
            }

            #endregion

            #region SSN

            vals = pid.GetField((int)PIDSection.SSN);
            if(vals.Length != 0)
            {
                patientInfo.SSN = vals[0].ToString();
            }

            #endregion
        }

        private ACK InitAcknowledgment(HL7Header hl7Header)
        {
            ACK reply = new ACK();

            Identifier serverApplication = hl7Header.ReceivingApplication;
            Identifier serverFacility = hl7Header.ReceivingFacility;
            //Identifier sendingApplication = PixRegistryConfigration.ReceivingApplication;
            //Identifier sendingFacility = PixRegistryConfigration.ReceivingFacility;
            Identifier sendingApplication = hl7Header.SendingApplicaiton;
            Identifier sendingFacility = hl7Header.SendingFacility;

            string @event = hl7Header.TriggerEvent;

            PopulateMSH(
                reply.MSH,
                "ACK",
                @event,
                hl7Header.MessageControlId,
                serverApplication,
                serverFacility,
                sendingApplication,
                sendingFacility);

            return reply;
        }

        private static void PopulateMSH(
            MSH msh,
            string type,
            string @event,
            string id,
            Identifier sendingApplication,
            Identifier sendingFacility,
            Identifier receivingAppliation,
            Identifier receivingFacility)
        {
            // MSH-1
            msh.FieldSeparator.Value = "|";

            // MSH-2
            msh.EncodingCharacters.Value = "^~\\&";

            // MSH-3
            HD hd = msh.SendingApplication;
            hd.NamespaceID.Value = sendingApplication.NamespaceId;
            hd.UniversalID.Value = sendingApplication.UniversalId;
            hd.UniversalIDType.Value = sendingApplication.UniversalIdType;

            // MSH-4
            hd = msh.SendingFacility;
            hd.NamespaceID.Value = sendingFacility.NamespaceId;
            hd.UniversalID.Value = sendingFacility.UniversalId;
            hd.UniversalIDType.Value = sendingFacility.UniversalIdType;

            // MSH-5
            hd = msh.ReceivingApplication;
            hd.NamespaceID.Value = receivingAppliation.NamespaceId;
            hd.UniversalID.Value = receivingAppliation.UniversalId;
            hd.UniversalIDType.Value = receivingAppliation.UniversalIdType;

            // MSH-6
            hd = msh.ReceivingFacility;
            hd.NamespaceID.Value = receivingFacility.NamespaceId;
            hd.UniversalID.Value = receivingFacility.UniversalId;
            hd.UniversalIDType.Value = receivingFacility.UniversalIdType;

            // MSH-7
            msh.DateTimeOfMessage.TimeOfAnEvent.SetShortDate(DateTime.Now);

            // MSH-9
            msh.MessageType.MessageType.Value = type;
            msh.MessageType.TriggerEvent.Value = @event;

            // MSH-10 
            msh.MessageControlID.Value = GenerateControlId();

            // MSH-11
            msh.ProcessingID.ProcessingID.Value = "P";

            // MSH-12 
            msh.VersionID.VersionID.Value = "2.3.1";
        }

        private static void PopulateMSA(MSA msa, string acknowledgmentCode, string messageControlId)
        {
            // MSA-1 
            msa.AcknowledgementCode.Value = acknowledgmentCode;

            // MSA-2 
            msa.MessageControlID.Value = messageControlId;
        }

        private static void PopulateErr(
            ERR err,
            string segmentId,
            string sequence,
            string fieldPosition,
            string hl7ErrorCode,
            string hl7ErrorText,
            string alternateText = "")
        {
            ELD eld = err.GetErrorCodeAndLocation(0);
            eld.SegmentID.Value = segmentId;
            eld.Sequence.Value = sequence;
            eld.FieldPosition.Value = fieldPosition;
            eld.CodeIdentifyingError.Identifier.Value = hl7ErrorCode;
            eld.CodeIdentifyingError.Text.Value = hl7ErrorText;
            eld.CodeIdentifyingError.AlternateText.Value = alternateText;
        }

        private static readonly object SyncObject = new object();

        private static int _countNumber = 0;

        private static string _lastTimeStamp = null;

        private static string GenerateControlId()
        {
            lock (SyncObject)
            {
                if (_lastTimeStamp == null)
                {
                    _lastTimeStamp = DateTime.Now.ToString("yyyyMMddhhmmss");
                }

                var timeStamp = DateTime.Now.ToString("yyyyMMddhhmmss");
                if (_lastTimeStamp.Equals(timeStamp))
                {
                    _countNumber++;
                }
                else
                {
                    _lastTimeStamp = timeStamp;
                    _countNumber = 0;
                }

                return string.Format("UIHXDS-{0}-{1}", timeStamp, _countNumber);
            }
        }
    }
}