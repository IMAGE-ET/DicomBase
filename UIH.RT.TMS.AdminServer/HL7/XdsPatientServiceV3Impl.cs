using System;
using System.Linq;
using UIH.RT.Framework.Utility;
using UIH.RT.TMS.DBWrapper;
using UIH.RT.TMS.ServerBase;
using UIH.RT.TMS.HL7Server.HL7;

namespace UIH.RT.TMS.HL7Server
{
    public class XdsPatientServiceBase
    {
        public XdsPatientServiceBase()
        {
            dbWrapper = new RTDBWrapper();
        }

        protected readonly RTDBWrapper dbWrapper;

        protected bool IsPatientExist(string patientID)
        {
            var efPatients = from item in dbWrapper._RT_TMSEntities.tmspatients
                             where item.patientid == patientID
                             select item;
            if (!efPatients.Any())
            {
                return false;
            }
            return true;
        }

        //protected static void MergePatient(XdsEntities xds, string oldPatientUid, string newPatientUid)
        //{
        //    int documentPatientIdUuid = DbHelpUtility.GetIntFromString(GlobalValues.XdsDocumentEntryPatientIdUuid);
        //    int folderPatientIdUuid = DbHelpUtility.GetIntFromString(GlobalValues.XdsFolderPatientIdUuid);
        //    int submissionSetPatientIdUuid = DbHelpUtility.GetIntFromString(GlobalValues.XdsSubmissionSetPatientIdUuid);

        //    List<externalidentifier> patientExternal = (from patient in xds.externalidentifier
        //                                                where
        //                                                    (patient.identificationScheme
        //                                                     == documentPatientIdUuid
        //                                                     ||
        //                                                     patient.identificationScheme == folderPatientIdUuid
        //                                                     ||
        //                                                     patient.identificationScheme
        //                                                     == submissionSetPatientIdUuid)
        //                                                    && patient.value.Equals(oldPatientUid)
        //                                                select patient).ToList();

        //    foreach (externalidentifier externalIdentifier in patientExternal)
        //    {
        //        externalIdentifier.value = newPatientUid;
        //    }

        //    // Update patient Table 
        //    var deletedPerson =
        //        (from person in xds.personidentifies where person.patientid == oldPatientUid select person).
        //            FirstOrDefault();

        //    if (deletedPerson != null)
        //    {
        //        xds.personidentifies.DeleteObject(deletedPerson);
        //    }
        //}
    }

    //public class XdsPatientServiceV3Impl : XdsPatientServiceBase, IXdsPatientServiceV3
    //{
    //    public static readonly string DatetimeFormat = "yyyy-MM-ddTHH:mm:ss.fffffffzzzzzz";

    //    public static readonly string PatientAckDatetimeFormat = "yyyymmddhhmm";

    //    public XmlDocument AddPatient(XmlDocument request)
    //    {
    //        PatientIdentityFeedRecord patientFeed = GetPatient(request);

    //        using (var xds = new XdsEntities())
    //        {
    //            try
    //            {
    //                string registryPatientId;
    //                if (this.IsPatientExist(xds, patientFeed.PatientUid, out registryPatientId))
    //                {
    //                    patientFeed.ResultCode = PatientIdentityFeedResultCode.PatientAlreadyExists;
    //                }
    //                else
    //                {
    //                    InsertPatient(xds, patientFeed.PatientUid);
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                Platform.Log(LogLevel.Warn, ex);
    //                patientFeed.ResultCode = PatientIdentityFeedResultCode.Failure;
    //            }

    //            xds.SaveChanges();
    //        }

    //        string resultCode = patientFeed.ResultCode != PatientIdentityFeedResultCode.Success ? "CE" : "CA";

    //        return ConstructPatientAckMessage(request, resultCode, PatientFeedV3.PatientAddAck);
    //    }

    //    public XmlDocument RevisePatient(XmlDocument request)
    //    {
    //        PatientIdentityFeedRecord patientFeed = GetPatient(request);

    //        string resultCode = "CA";
    //        using (XdsEntities xds = new XdsEntities())
    //        {
    //            string registryPatientId;
    //            if (!IsPatientExist(xds, patientFeed.PatientUid, out registryPatientId))
    //            {
    //                resultCode = "CE";
    //            }
    //        }

    //        return ConstructPatientAckMessage(request, resultCode, PatientFeedV3.PatientRevisedAck);
    //    }

    //    public XmlDocument MergePatient(XmlDocument request)
    //    {
    //        // When the Document Registry receives the Resolve Duplicates message of the Patient Identity
    //        // Feed transaction, it shall merge the patient identity specified in the PriorRegistrationRole.id
    //        // attribute of the Control-Act wrapper (subsumed patient identifier) into the patient identity
    //        // specified in Patient.id attribute of the message payload (surviving patient identifier) in its
    //        // registry. After the merge, all Document Submission Sets (including all Documents and Folders
    //        // beneath them) under the secondary patient identity before the merge shall point to the primary
    //        // patient identity. The secondary patient identity shall no longer be referenced in the future
    //        // services provided by the Document Registry

    //        var patientDuplicateEntry = GetPatientDuplicateEntry(request);

    //        string resultCode = "CA";
    //        try
    //        {
    //            using (XdsEntities xds = new XdsEntities())
    //            {
    //                string registryPatientId;
    //                if (!IsPatientExist(xds, patientDuplicateEntry.NewPatient.PatientUid, out registryPatientId))
    //                {
    //                    // Generate Response Message 
    //                    return ConstructUnknownPatientIdRegistryErrorResponse();
    //                }

    //                patientDuplicateEntry.NewPatient.PatientId = registryPatientId;

    //                // Old Patient IDs
    //                foreach (PatientIdentityFeedRecord oldPatientFeed in patientDuplicateEntry.OldPatients)
    //                {
    //                    if (!IsPatientExist(xds, oldPatientFeed.PatientUid, out registryPatientId))
    //                    {
    //                        oldPatientFeed.ResultCode = PatientIdentityFeedResultCode.PatientNotFound;

    //                        // Generate Response Message
    //                        return ConstructUnknownPatientIdRegistryErrorResponse();
    //                    }

    //                    oldPatientFeed.PatientId = registryPatientId;
    //                }

    //                foreach (PatientIdentityFeedRecord oldPatientFeed in patientDuplicateEntry.OldPatients)
    //                {
    //                    // Update the patient ids in all the referencing table 
    //                    PatientIdentityFeedRecord feed = oldPatientFeed;
    //                    MergePatient(xds, feed.PatientUid, patientDuplicateEntry.NewPatient.PatientUid);
    //                }

    //                xds.SaveChanges();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            // Error happen 
    //            Platform.Log(LogLevel.Warn, ex);
    //            resultCode = "CE";
    //        }

    //        return ConstructPatientAckMessage(request, resultCode, PatientFeedV3.PatientDuplicatedResolvedAck);
    //    }

    //    private static XmlDocument ConstructUnknownPatientIdRegistryErrorResponse()
    //    {
    //        RegistryResponse response = new RegistryResponse { RequestId = string.Empty, Status = GlobalValues.ConstResponseStatusTypeFailure };

    //        response.RegistryErrorList.RegistryErrors.Add(
    //            new RegistryError()
    //            {
    //                CodeContext =
    //                    "Patient ID referenced in metadata is not known to the Registry actor "
    //                    + "via the Patient Identity Feed or is unknown because of patient identifier merge.  "
    //                    + "The codeContext shall include the value of patient ID in question.",
    //                ErrorCode = GlobalValues.ConstRegistryerrorCodeXdsUnknownPatientId,
    //                Severity = GlobalValues.ConstSeverityTypeError,
    //                Location = string.Empty
    //            });

    //        XmlDocument xdsResponse = response.SerializeToXmlDocument();
    //        return xdsResponse;
    //    }

    //    private PatientIdentityFeedRecord GetPatient(XmlDocument request)
    //    {
    //        if (request.DocumentElement != null)
    //        {
    //            var patientIdNode =
    //                request.DocumentElement.SelectSingleNode(
    //                    ".//*[local-name()='controlActProcess']/*[local-name()='subject']/*[local-name()='registrationEvent']/*[local-name()='subject1']/*[local-name()='patient']/*[local-name()=\"id\"]");

    //            if (patientIdNode == null)
    //            {
    //                throw new Exception();
    //            }

    //            var patientFeed = new PatientIdentityFeedRecord();
    //            if (patientIdNode.Attributes != null)
    //            {
    //                patientFeed.Root = patientIdNode.Attributes["root"].Value;
    //                patientFeed.Extension = patientIdNode.Attributes["extension"].Value;
    //            }

    //            return patientFeed;
    //        }

    //        throw new Exception();
    //    }

    //    private PatientDuplicateEntry GetPatientDuplicateEntry(XmlDocument request)
    //    {
    //        PatientDuplicateEntry duplicateEntry = new PatientDuplicateEntry();

    //        // New Patient UID 
    //        XmlNode newPatientNode =
    //            request.DocumentElement.SelectSingleNode(
    //                ".//*[local-name()='controlActProcess']/*[local-name()='subject']/*[local-name()='registrationEvent']/*[local-name()='subject1']/*[local-name()='patient']/*[local-name()=\"id\"]");

    //        if (newPatientNode == null)
    //        {
    //            throw new Exception("Node registration Event does not exist in the request xml");
    //        }

    //        duplicateEntry.NewPatient = new PatientIdentityFeedRecord
    //        {
    //            Root = newPatientNode.Attributes["root"].Value,
    //            Extension = newPatientNode.Attributes["extension"].Value
    //        };

    //        // Old Patient UID 
    //        var oldPatientNodes =
    //            request.DocumentElement.SelectNodes(
    //                ".//*[local-name()=\"replacementOf\"]/*[local-name()=\"priorRegistration\"]/*[local-name()=\"id\"]");
    //        if (oldPatientNodes == null)
    //        {
    //            throw new Exception("Node replacementOf or one it's child nodes does not exist in the request xml");
    //        }

    //        duplicateEntry.OldPatients = new List<PatientIdentityFeedRecord>();
    //        foreach (XmlElement oldPatientNode in oldPatientNodes)
    //        {
    //            duplicateEntry.OldPatients.Add(
    //                new PatientIdentityFeedRecord()
    //                {
    //                    Root = oldPatientNode.Attributes["root"].Value,
    //                    Extension = oldPatientNode.Attributes["extension"].Value
    //                });
    //        }

    //        return duplicateEntry;
    //    }

    //    private XmlDocument ConstructPatientAckMessage(XmlDocument requestMessage, string resultCode, string template)
    //    {
    //        // ReSharper disable PossibleNullReferenceException
    //        // id 
    //        string originalMessageId =
    //            requestMessage.DocumentElement.SelectSingleNode(".//*[local-name()='id']").Attributes["root"].Value;

    //        // interaction id\root
    //        string interactionId =
    //            requestMessage.DocumentElement.SelectSingleNode(".//*[local-name()='interactionId']").Attributes["root"].
    //                Value;

    //        // receiver 
    //        string receiverRoot =
    //            requestMessage.DocumentElement.SelectSingleNode(
    //                ".//*[local-name()='receiver']/*[local-name()='device']/*[local-name()='id']").Attributes["root"].
    //                Value;

    //        // sender 
    //        string senderRoot =
    //            requestMessage.DocumentElement.SelectSingleNode(
    //                ".//*[local-name()='sender']/*[local-name()='device']/*[local-name()='id']").Attributes["root"].
    //                Value;
    //        // ReSharper restore PossibleNullReferenceException

    //        // Replace Value
    //        // $NEW.GUID$
    //        template = template.Replace("$NEW.GUID$", Guid.NewGuid().ToString());

    //        // $creationTime$
    //        template = template.Replace("$creationTime$", DateTime.Now.ToString(PatientAckDatetimeFormat));

    //        // $INTERACTION.ID$
    //        template = template.Replace("$INTERACTION.ID$", interactionId);

    //        // $RECEIVER.ROOT$ - Swap Sender & Receiver fromthe original message
    //        template = template.Replace("$RECEIVER.ROOT$", senderRoot);

    //        // $SENDER.ROOT$ - Swap Sender & Receiver fromthe original message
    //        template = template.Replace("$SENDER.ROOT$", receiverRoot);

    //        // $RESULT.CODE$
    //        template = template.Replace("$RESULT.CODE$", resultCode);

    //        // $ORIGINAL.MESSAGE.ID$
    //        template = template.Replace("$ORIGINAL.MESSAGE.ID$", originalMessageId);

    //        var responseXml = new XmlDocument();
    //        responseXml.LoadXml(template);

    //        return responseXml;
    //    }
    //}

    public class XdsPatientServiceImpl : XdsPatientServiceBase
    {
        //public void CreatePatient(PatientIdentifier patient)
        //{
        //    using (XdsEntities xds = new XdsEntities())
        //    {
        //        string registryPatientId;
        //        if (IsPatientExist(xds, patient.PatientUid, out registryPatientId))
        //        {
        //            throw new Exception(string.Format("the patient {0} is already exist in database", patient.PatientUid));
        //        }

        //        InsertPatient(xds, patient.PatientUid);

        //        xds.SaveChanges();
        //    }
        //}

        public void CreatePatient(PatientInfo patientInfo)
        {
            try
            {
                if (IsPatientExist(patientInfo.PatientIdent.Id))
                {
                    throw new Exception(string.Format("the patient {0} is already exist in database", patientInfo.PatientIdent.Id));
                }

                var efPatient = new tmspatient()
                {
                    uid = RTDBDicomUIDManager.Instance().CreateObjectUID(),
                    patientid = patientInfo.PatientIdent.Id,
                    firstname = patientInfo.FirstName,
                    middlename = patientInfo.MidName,
                    lastname = patientInfo.LastName,
                    nameprefix = patientInfo.NamePrefix,
                    namesuffix = patientInfo.NameSuffix,
                    patientsex = patientInfo.Gender,
                    patientbirthdate = patientInfo.BirthDate,
                    address = patientInfo.HomeCurAddress,
                    province = patientInfo.HomeCurProvince,
                    state = patientInfo.HomeCurState,
                    country = patientInfo.HomeCurCountry,
                    city = patientInfo.HomeCurCity,
                    postcode = patientInfo.HomeCurPostal,
                    email = patientInfo.HomeEmail,
                    areacode = patientInfo.HomeAreaCode,
                    countrycode = patientInfo.HomeCountryCode,
                    telephone = patientInfo.HomePhone,
                    ssn = patientInfo.SSN,
                    isactive = true,
                    isfromhis = true
                };

                //create empty course, site, prescription when add a new patient.
               // tmscourse efCourse = tmscourse.Createtmscourse(RTDBDicomUIDManager.Instance().CreateObjectUID(), efPatient.uid, false);
                tmscourse efCourse = new tmscourse();
                efCourse.uid = RTDBDicomUIDManager.Instance().CreateObjectUID();
                efCourse.patientuid = efPatient.uid;
                efCourse.isdefault = false;
                efCourse.coursename = "Course1";
                efCourse.isdefault = true;
                var efSite = new tmssite();
                efSite.uid = RTDBDicomUIDManager.Instance().CreateObjectUID();
                efSite.courseuid = efCourse.uid;
                efSite.isdefault = true;
                efSite.name = "Site";
                var efPrescription = new tmsprescription();
                efPrescription.uid = RTDBDicomUIDManager.Instance().CreateObjectUID();
                efPrescription.siteuid = efSite.uid;
                efPrescription.isdefault = true;
                efPrescription.name = "Rx";
                efPrescription.sitename = "Site";
                efSite.tmsprescriptions.Add(efPrescription);
                efCourse.tmssites.Add(efSite);
                efPatient.tmscourses.Add(efCourse);

                dbWrapper.Save(efPatient);
            }
            catch (Exception ex)
            {
                LogAdapter.Logger.TraceException(ex);
            }
        }

        public void UpdatePatient(PatientInfo patientInfo)
        {
            try
            {
                if (!IsPatientExist(patientInfo.PatientIdent.Id))
                {
                    throw new PatientDonotExistException(string.Format("the patient {0} is not exist in database", patientInfo.PatientIdent.Id));
                }

                var efPatients = from item in dbWrapper._RT_TMSEntities.tmspatients
                                 where item.patientid == patientInfo.PatientIdent.Id
                                 select item;
                var efPatient = efPatients.FirstOrDefault();

                if (null != efPatient)
                {
                    if (null != patientInfo.FirstName)
                        efPatient.firstname = patientInfo.FirstName;
                    if (null != patientInfo.MidName)
                        efPatient.middlename = patientInfo.MidName;
                    if (null != patientInfo.LastName)
                        efPatient.lastname = patientInfo.LastName;
                    if (null != patientInfo.NamePrefix)
                        efPatient.nameprefix = patientInfo.NamePrefix;
                    if (null != patientInfo.NameSuffix)
                        efPatient.namesuffix = patientInfo.NameSuffix;
                    if (null != patientInfo.Gender)
                        efPatient.patientsex = patientInfo.Gender;
                    if (null != patientInfo.BirthDate)
                        efPatient.patientbirthdate = patientInfo.BirthDate;
                    if (null != patientInfo.HomeCurAddress)
                        efPatient.address = patientInfo.HomeCurAddress;
                    if (null != patientInfo.HomeCurProvince)
                        efPatient.province = patientInfo.HomeCurProvince;
                    if (null != patientInfo.HomeCurState)
                        efPatient.state = patientInfo.HomeCurState;
                    if (null != patientInfo.HomeCurCountry)
                        efPatient.country = patientInfo.HomeCurCountry;
                    if (null != patientInfo.HomeCurCity)
                        efPatient.city = patientInfo.HomeCurCity;
                    if (null != patientInfo.HomeCurPostal)
                        efPatient.postcode = patientInfo.HomeCurPostal;
                    if (null != patientInfo.HomeEmail)
                        efPatient.email = patientInfo.HomeEmail;
                    if (null != patientInfo.HomeAreaCode)
                        efPatient.areacode = patientInfo.HomeAreaCode;
                    if (null != patientInfo.HomeCountryCode)
                        efPatient.countrycode = patientInfo.HomeCountryCode;
                    if (null != patientInfo.HomePhone)
                        efPatient.telephone = patientInfo.HomePhone;
                    if (null != patientInfo.SSN)
                        efPatient.ssn = patientInfo.SSN;
                    efPatient.isfromhis = true;
                }

                dbWrapper.Save(efPatient);
            }
            catch (Exception ex)
            {
                LogAdapter.Logger.TraceException(ex);
            }
        }

        //public void MergePatient(PatientIdentifier patient, PatientIdentifier patientMerge)
        //{
        //    using (XdsEntities xds = new XdsEntities())
        //    {
        //        string registryPatientId;
        //        if (!IsPatientExist(xds, patient.PatientUid, out registryPatientId))
        //        {
        //            throw new PatientDonotExistException(string.Format("the patient {0} is not exist in database", patient.PatientUid));
        //        }

        //        if (!IsPatientExist(xds, patientMerge.PatientUid, out registryPatientId))
        //        {
        //            throw new PatientDonotExistException(string.Format("the merged patient {0} is not exist in database", patientMerge.PatientUid));
        //        }

        //        MergePatient(xds, patientMerge.PatientUid, patient.PatientUid);

        //        xds.SaveChanges();
        //    }
        //}

    }

    [Serializable]
    public class PatientDonotExistException : Exception
    {
        public PatientDonotExistException()
        {
        }

        public PatientDonotExistException(string message)
            : base(message)
        {
        }

        public PatientDonotExistException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}