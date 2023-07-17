using System;
using System.Collections.Generic;
using MyDodos.Domain.HR;
using MyDodos.ViewModel.Entitlement;

namespace MyDodos.ViewModel.HRMS
{
    public class HRMSDepartmentBO
    {
        public int DeptID { get; set; }
        public int RelID { get; set; }
        public string DepartmentCode { get; set; }
        public string Department { get; set; }
        public string DeptHead { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string DeptType { get; set; }
        public string DeptShortName { get; set; }
        public string DeptStatus { get; set; }
        public int EntityID { get; set; }
        public int TenantID { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int TotalCount { get; set; }
    }
    public class HRMSLocationBO
    {
        public int EntityID { get; set; }
        public int RelID { get; set; }
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
    public class HREmployeeBO
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
        public int RellEmpID { get; set; }
        public int RellID { get; set; }
        public string ManagerName { get; set;}
        public string DepartmentName { get; set;}
        public string Designation { get; set;}
    }
    public class HRrtnOnboardBO
    {
        public AppUserDetailsBO Objappuser { get; set; }
        public List<HREmployeeBO> Employee { get; set; }
		public List<EmployeeAddress> Address { get; set; }		
    }
    public class LeaveJournalBO
    {
        public int EmpID { get; set; }
        public int YearID { get; set; }
        public int CategoryID { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public int BenefitGroupID { get; set; }
        public string EntityType { get; set; }
    }
}