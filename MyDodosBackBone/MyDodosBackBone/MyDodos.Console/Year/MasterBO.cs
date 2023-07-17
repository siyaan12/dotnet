using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master
{
    public enum DBType
    {
        [DescriptionAttribute("SQL")]
        SQL,
        [DescriptionAttribute("MYSQL")]
        MYSQL
    }
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
    public class LoginLocationBO
    {
        public int LocationID { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string LocationAddress1 { get; set; }
        public string LocationGmt { get; set; }
        public string LocationCurrencySymbol { get; set; }
        public DateTime GmtDate { get; set; }
    }
    public class LoginEmployeeBO
    {
        public int EmpID { get; set; }
        public string EmpNumber { get; set; }
        public string FullName { get; set; }
        public int RoleID { get; set; }
        public int ManagerID { get; set; }
        public int DepartmentID { get; set; }
        public int BenefitGroupID { get; set; }
        public string Gender { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public bool IsCompensate { get; set; }
        public bool IsTimeSheet { get; set; }
        public bool IsAttendance { get; set; }
        public int HRInchargeID { get; set; }
        public string EmpStatus { get; set; }
        public string ProfileImage { get; set; }
        public int AppUserID { get; set; }
    }
    public class LoginYearBO
    {
        public int YearID { get; set; }
        public int StartYear { get; set; }
    }
    public class LoginBO
    {
        public LoginEmployeeBO Employee { get; set; }
        public LoginLocationBO Location { get; set; }
        public LoginYearBO Year { get; set; }
    }
    public class MasYear
    {
        public int YearID { get; set; }
        public int StartYear { get; set; }
        public string YearName { get; set; }
        public int OptionalHoliday { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsNotify { get; set; }
        public string NotifyContent { get; set; }
        public string YearStatus { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
    }
    public class DeviceMasterBO
    {
        public int DeviceID { get; set; }
        public string DeviceName { get; set; }
        public string DeviceType { get; set; }
        public string Location { get; set; }
        public string SKUNumber { get; set; }
        public string SerialNumber { get; set; }
        public string MachineType { get; set; }
        public string DeviceIdentifier { get; set; }
        public string ActivationKey { get; set; }
        public string IsMode { get; set; }
        public string ExpiryTime { get; set; }
        public string ExpPeriod { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string DeviceStatus { get; set; }
        public int EntityID { get; set; }
        public int TenantID { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? StartDate { get; set; }
    }
    public class EntSubscribeProdBO
    {
        public int SubscriptionID { get; set; }
        public int TenantID { get; set; }
        public int KProductID { get; set; }
        public string CurrentPackage { get; set; }
        public string SubscribedOn { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsPrimaryProd { get; set; }
        public int OwnerID { get; set; }
        public string SubscriptionStatus { get; set; }
        public DateTime UnSubscribedOn { get; set; }
        public int AllowedEntityCount { get; set; }
        public int AllowedUserAccounts { get; set; }
    }
    public class LocationBO
    {
        public int LocationID { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string LocationAddress1 { get; set; }
        public string LocationAddress2 { get; set; }
        public string LocationCity { get; set; }
        public string LocationState { get; set; }
        public string LocationZip { get; set; }
        public string LocationCountry { get; set; }
        public int ServerTimeoff { get; set; }
        public string LocationStatus { get; set; }
        public string LocationGmt { get; set; }
        public int TenantID { get; set; }
        public string LocationLongitude { get; set; }
        public string LocationCurrency { get; set; }
        public string LocationCurrencySymbol { get; set; }
        public string OfficePhoneNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
    }
    public class YearBO
    {
        public int YearID { get; set; }
        public int StartMonth { get; set; }
        public int StartYear { get; set; }
        public int EndMonth { get; set; }
        public int EndYear { get; set; }
        public string YearStatus  { get; set; }
        public string YearName { get; set; }
        public bool YearEndProcessDone { get; set; }
        public bool sHolidayLock { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public bool isReview { get; set; }
        public string YearSpan { get; set; }
        public int OptionalHoliday { get; set; }
        public bool IsEmployee { get; set; }
        public DateOnly DueDate { get; set; }
        public bool IsNotify { get; set; }
        public string NotifyContent { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy{ get; set; }
    }
    public class LeaveReportBO
    {
        public int LeaveReportID { get; set;}
        public string LeaveFrom { get; set;}
        public string LeaveTo { get; set;}
        public string LeaveCategory { get; set;}
        public string LeaveType { get; set;}
        public decimal NoOfDays { get; set;}
        public string LeaveReason { get; set;}
        public string LeaveStatus { get; set;}
        public string LeaveSession { get; set;}
        public string LeaveComments { get; set;}
        public DateTime RequestDate { get; set;}
        public int IsLOP { get; set;}
        public int EmpID { get; set;}
        public string EmpName { get; set;}
        public int LocationID { get; set;}
        public int TenantID { get; set;}
        public int YearID { get; set;}
        public int LeaveCategoryID { get; set;}
        public DateTime CreatedOn { get; set;}
        public DateTime ModifiedOn { get; set;}
        public int CreatedBy { get; set;}
        public string ModifiedBy { get; set;}
    }
    public class TimesheetBO
    {
        public int TimesheetReportID { get; set; }
        public int TimesheetID { get; set;}
        public string WeekNo{ get; set; }
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ResourceName { get; set; }
        public int HoursReported { get; set; }
        public int BilledHours { get; set; }
        public string Symbol { get; set; }
        public string Rate { get; set; }
        public string BilledAmount { get; set; }
        public string PaidAmount { get; set; }
        public string BalanceAmount { get; set; }
        public DateTime AssignStart { get; set; }
        public DateTime AssignEnd { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
    public class TimeSheetException
    {
        public int WeekNo { get; set; }
        public int YearID { get; set; }
        public int EmpID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int ManagerID { get; set; }
        public string FullName { get; set; }
        public string EmpNumber { get; set; }
        public int TimeSheetID { get; set; }
        public string TimeSheetStatus { get; set; }
        public decimal ProjectHours { get; set; }
        public decimal NonProjectHours { get; set; }
        public decimal TotalHours { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int TotalCount { get; set; }
    }
    public class MailNotifyBO
    {
        public int EmpID { get; set; }
        public int EntityID { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmpName { get; set; }
        public string HRName { get; set; }
        public string ReportingEmail { get; set; }
        public string ReportName { get; set; }
        public string HREmail { get; set; }
        public int ProductID { get; set; }
        public int TenantID { get; set; }
        public string Msg { get; set; }
    }
    public class MailInputModel
    {
        public int EntityID { get; set; }
        public int EmpID { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public int YearID { get; set; }
        public int ProductID { get; set; }
        public string EntitySubject { get; set; }
        public string EntityStatus { get; set; }
        public string EntityName { get; set; }
        public string EntityType { get; set; }
    }
    public class SalaryPeriodBO
    {
        public int SalaryPeriodID { get; set; }
        public string PeriodType { get; set; }
        public int YearID { get; set; }
        public string PeriodFrom { get; set; }
        public string PeriodTo { get; set;}
        public string PeriodStatus { get; set;}
        public string ProcessStatus { get; set;}
        public bool IsBaseSalarySet { get; set;}
        public bool IsLeaveCalc {get; set; }
        public bool IsOtherDeduction { get; set; }
        public bool IsBankInstruction { get; set; }
        public bool IsSalaryCredited { get; set; }
        public string Comments { get; set; }
        public int TenantID {get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
    public class SaveResult
    {
        public int Id { get; set; }
        public string Msg { get; set; }
    }
}
