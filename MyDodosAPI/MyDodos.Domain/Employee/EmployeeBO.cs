using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDodos.Domain.Employee
 {
    public class EmployeeBO
    {
        public int EmpID { get; set; }
        public string EmpNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int RoleID { get; set; }
        public int ManagerID { get; set; }
        public int DepartmentID { get; set; }
        public int BenefitGroupID { get; set; }
        public string Gender { get; set; }
        public DateTime? DOJ { get; set; }
        public DateTime? DOB { get; set; }
        public string OfficeEmail { get; set; }
        public string PersonalMail { get; set; }
        public string MobileNo { get; set; }
        public string EmpType { get; set; }
        public string BloodGroup { get; set; }
        public string Nationality { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public bool IsCompensate { get; set; }
        public bool IsTimeSheet { get; set; }
        public bool IsAttendance { get; set; }
        public int HRInchargeID { get; set; }
        public string EmpStatus { get; set; }
        public string ProfileImage { get; set; }
        public int AppUserID { get; set; }
        public bool IsCompleted { get; set; }
        public string base64images { get; set;}
    }
    public class TenantProfiledataBO
    {
        public int TenantID { get; set; }
        public string TenantCode { get; set; } 
        public string TenantName { get; set; }
        public string TenantType { get; set; }
        public int ParentTenantID { get; set; }
        public string TaxID { get; set; }
        public string InCorpState { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Website { get; set; }
        public string PrimaryPhone { get; set; }
        public string AlternatePhone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string TenantStatus { get; set; }
        public string CreatedFromIP { get; set; }
        public string GeoRegion { get; set; }
        public string BillAddress1 { get; set; }
        public string BillAddress2 { get; set; }
        public string BillCity { get; set; }
        public string BillZipCode { get; set; }
        public string BillState { get; set; }
        public string BillCountry { get; set; }
        public string TenantRegNo { get; set; }
        public string ContactUsEmailID { get; set; }
        public string ContactUsTelephone { get; set; }
        public string ShortName { get; set; }
        public string TenantAccountNo { get; set; }
        public string CarrierType { get; set; }
        public string AdministratorName { get; set; }
        public string AdministratorEmail { get; set; }
        public string AdministratorPhone { get; set; }
        public int ProductID { get; set; }
        public string base64Images { get; set; }
        public string FileName { get; set; }
    }
    public class TenantProfileImageBO
    {
        public int TenantID { get; set; }
        public string TenantCode { get; set; } 
        public string TenantName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PrimaryPhone { get; set; }
        public string Email { get; set; }
        public int ProductID { get; set; }
        public string RegularLogobase64Images { get; set; }
        public string SmallLogobase64Images { get; set; }
        public string RegularFileName { get; set; }
        public string SmallFileName { get; set; }
    }
    public class TenantPaymentModeBO
    {
        public int ConfigID { get; set; }
        public string TenantName { get; set; }
        public string PaymentMode { get; set; }
        public string IdentifierType { get; set; }
        public bool IsActive { get; set; }
        public int TenantID { get; set; }
    }
}