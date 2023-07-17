using System;
using System.Collections.Generic;
namespace MyDodos.Domain.LeaveBO
{
    public class LeaveRequestModel
    {
        public int LeaveID { get; set; }
        public DateTime LeaveFrom { get; set; }
        public DateTime LeaveTo { get; set; }
        public string LeaveCategory { get; set; }
        public string LeaveType { get; set; }
        public decimal NoOfDays { get; set; }
        public string LeaveReason { get; set; }
        public string LeaveSession { get; set; }
        public string LeaveStatus { get; set; }
        public string LeaveComments { get; set; }
        public DateTime RequestDate { get; set; }
        public decimal IsLOP { get; set; }
        public int EmpID { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public int LeaveCategoryID { get; set; }
        public int YearID { get; set; }
        public int BenefitGroupID { get; set; }
        public int CreatedBy { get; set; }
        public int TotalLeaveCount { get; set;}
        public string EmpNumber { get; set;}
        public string EmpName { get; set;}
        public int ProductID { get; set; }
        public string EntityName { get; set; }
        public string EntityType { get; set; }
        public decimal LOPCount { get; set; }
    }
    public class LeaveRequestModelMsg {
        public string Msg { get; set; }
        //public string ErrorMsg { get; set; }
        public int RequestID { get; set; }
    }
    public class MasLeaveCategoryBO {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryType { get; set; }
        public string NoOfLeave { get; set; }
        public string Accured { get; set; }
        public string Availed { get; set; }
        public string NoOfRolledOver { get; set; }
        public string LOPCount { get; set; }
        public string balancecount { get; set; }
    }
    public class HRVwBeneftisLeave_BO
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryType { get; set; }
        public string Gender { get; set; }
        public bool IsIncidentBased { get; set; }
        public bool IsTimeOffCategory { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set;}
        public int ModifiedBy { get; set;}
        public DateTime? ModifiedOn { get; set;}
    }
    public class MobileLeaveCategoryBO 
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryType { get; set; }
    }
}