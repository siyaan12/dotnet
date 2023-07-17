using System;
using System.Collections.Generic;
using MyDodos.Domain.LoginBO;
namespace MyDodos.Domain.Master
{
    public class MasterInputBO
    {
        public string EntityName { get; set; }
        public int ProductID { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
    }
    public class TenantProfileBO
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
    }
    public class LocationBO
    {
        public int LocationID { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string LocationAddress1 { get; set; }
        public string LocationGmt { get; set; }
        public string LocationCurrencySymbol { get; set; }
        public DateTime GmtDate { get; set; }
    }
    public class YearBO
    {
        public int YearID { get; set; }
        public int StartYear { get; set; }
    }
    public class ShiftConfigSettingBO
    {
        public string ServiceName { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public string IdentifierType { get; set; }
        public string IdentifierColumnName { get; set; }
    }
    public class HRLeaveAllocReferenceBO
    {
        public int LeaveAllocationID { get; set; }
        public int LeaveGroupID { get; set; }
        public int CategoryID { get; set; }
        public decimal NoOfLeave { get; set; }
        public string AllocationPeriod { get; set; }
        public bool isRollOver { get; set; }
        public bool AccureAtEnd { get; set; }
        public string Notes { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public int CreatedBy { get; set; }
    }
    public class HRMasLeaveCategoryBO
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryType { get; set; }
        public string Gender { get; set; }
        public bool IsIncidentBased { get; set; }
        public bool IsTimeOffCategory { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public int CreatedBy { get; set; }
    }
    public class OptionalSetBO
    {
        public int FormId { get; set; }
        public string FormName { get; set; }
        public int FieldId { get; set; }
        public string FieldName { get; set; }
        public int OptionalSetValue { get; set; }
        public string OptionalSetText { get; set; }
        public string Status { get; set; }
        public int TenantID { get; set; }
        public int CreatedBy { get; set; }
    }
    public class LocationdetBO
    {
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string LocationAddress1 { get; set; }
        public string LocationAddress2 { get; set; }
        public string LocationCity { get; set; }
        public string LocationState { get; set; }
        public string LocationZip { get; set; }
        public string LocationCountry { get; set; }
        public string LocationStatus { get; set; }
        public string LocationCurrency { get; set; }
        public string OfficePhoneNumber { get; set; }
        public int CreatedBy { get; set; }
    }
    
}