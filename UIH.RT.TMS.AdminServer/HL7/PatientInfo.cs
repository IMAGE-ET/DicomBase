/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: yuxuan.duan@united-imaging.com
////
//// File: PatientInfo.cs
////
//// Summary: PatientInfo
//// 
//// Date: 12/29/2014 7:11:24 PM
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIH.RT.TMS.HL7Server.HL7
{
    public class PatientInfo
    {
        public PatientInfo()
        {
            PatientIdent = new PatientIdentifier();
        }
        #region fromPID

            public PatientIdentifier PatientIdent { get; set; }

            public string FirstName { get; set; }

            public string MidName { get; set; }

            public string LastName { get; set; }

            public string NamePrefix { get; set; }

            public string NameSuffix { get; set; }

            public int? Gender { get; set; }

            public DateTime? BirthDate { get; set; }

            public string HomeCurAddress { get; set; }

            public int? HomeCurProvince { get; set; }

            public int? HomeCurCountry { get; set; }

            public string HomeCurCity { get; set; }

            public string HomeCurPostal { get; set; }

            public string HomeEmail { get; set; }

            public string HomeAreaCode { get; set; }

            public string HomeCountryCode { get; set; }

            public string HomePhone { get; set; }

            public string HomeCellPhone { get; set; }

            public string HomeCurState { get; set; }

            public string SSN { get; set; }

        #endregion

        #region fromOtherSection

            //public string AttDoctorId { get; set; }

            //public string RefDoctorName { get; set; }

            //public string Nation { get; set; }

            //public string Nationality { get; set; }

            //public int? IdentityType { get; set; }

            //public string IdentityNo { get; set; }

            //public string PatientHospitalID { get; set; }

            //public int? Age { get; set; }

            //public float? Height { get; set; }

            //public float? Weight { get; set; }

            //public int? HeightUnit { get; set; }

            //public int? WeightUnit { get; set; }

            //public DateTime? DateOfWeight { get; set; }

            //public bool? IsPregnancy { get; set; }

            //public bool? IsPacemaker { get; set; }

            //public bool? IsPreTreatment { get; set; }

            //public bool? IsCalustrophobia { get; set; }

            //public string Description { get; set; }


            //public string CardId { get; set; }

            //public string OldAdmissionNo { get; set; }

            //public string InpatientArea { get; set; }

            //public string Association { get; set; }

            //public string PathologyNo { get; set; }

            //public int? PatientType { get; set; }

            //public string RoomNo { get; set; }

            //public string BedNo { get; set; }

            //public string Email { get; set; }



            //public string HomeOrgAddress { get; set; }

            //public int? HomeOrgProvince { get; set; }

            //public int? HomeOrgCountry { get; set; }

            //public string HomeOrgCity { get; set; }

            //public string HomeOrgPostal { get; set; }



            ////[DataMember]
            ////public string RelativeFirstName;

            ////[DataMember]
            ////public string RelativeLastName;

            ////[DataMember]
            ////public int? RelativeShipType;

            ////[DataMember]
            ////public string RelativeAddress;

            ////[DataMember]
            ////public int? RelativeProvince;

            ////[DataMember]
            ////public int? RelativeCountry;

            ////[DataMember]
            ////public string RelativeCity;

            ////[DataMember]
            ////public string RelativePostal;

            ////[DataMember]
            ////public string RelativeEmail;

            ////[DataMember]
            ////public string RelativeAreaCode;

            ////[DataMember]
            ////public string RelativeCountryCode;

            ////[DataMember]
            ////public string RelativePhone;

            ////[DataMember]
            ////public string RelativeCellPhone;

            //public string EmergencyFirstName { get; set; }

            //public string EmergencyLastName { get; set; }

            //public int? EmergencyShipType { get; set; }

            //public string EmergencyAddress { get; set; }

            //public int? EmergencyProvince { get; set; }

            //public int? EmergencyCountry { get; set; }

            //public string EmergencyCity { get; set; }

            //public string EmergencyPostal { get; set; }

            //public string EmergencyEmail { get; set; }

            //public string EmergencyAreaCode { get; set; }

            //public string EmergencyCountryCode { get; set; }

            //public string EmergencyPhone { get; set; }

            //public string EmergencyCellPhone { get; set; }

            //public string NativePlace { get; set; }

            //public string NativeProvince { get; set; }

            //public string EmergencyName { get; set; }

            //public bool? IsActive { get; set; }

            //public string EmergencyState { get; set; }

            ////[DataMember]
            ////public string RelativeState;

            //public string HomeOrgState { get; set; }

        #endregion

    }
}
