using System;
using System.Collections.Generic;
using MyDodos.Domain.Administrative;

namespace MyDodos.Domain.LoginBO
{
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
        public bool IsReptManager { get; set; }
        public int HRInchargeID { get; set; }
        public string EmpStatus { get; set; }
        public string ProfileImage { get; set; }
        public string base64Images { get; set; }
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
}