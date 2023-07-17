using System;
using System.Collections.Generic;
namespace MyDodos.Domain.TimeOff
{
    public class TimeoffRequestModel
    {
        public int TimeoffID { get; set; }
        public DateTime LeaveFrom { get; set; }
        public DateTime LeaveTo { get; set; }
        public string LeaveCategory { get; set; }
        public string LeaveType { get; set; }
        public decimal NoOfDays { get; set; }
        public string LeaveReason { get; set; }
        public string LeaveStatus { get; set; }
        public string LeaveComments { get; set; }
        public string ManagerComments { get; set; }
        public DateTime RequestDate { get; set; }
        public decimal IsLOP { get; set; }
        public int EmpID { get; set; }
        public string EmpNumber { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public int YearID { get; set; }
        public bool IsSalary { get; set; }
        public int LeaveCategoryID { get; set; }
        public string EmpName { get; set; }
        public string ManagerName { get; set; }
        public int CreatedBy { get; set; }
        public int TotalCount { get; set; }
        public int ProductID { get; set; }
        public string EntityName { get; set; }
        public string EntityType { get; set; }
    }
    public class TimeoffRequestModelMsg {
        public string Msg { get; set; }
        public int RequestID { get; set; }
    }
}