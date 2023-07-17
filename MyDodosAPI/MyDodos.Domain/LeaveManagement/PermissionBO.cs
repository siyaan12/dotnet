using System;
using System.Collections.Generic;
namespace MyDodos.Domain.PermissionBO
{
    public class PermissionModel
    {
        public int PermID { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime PermDate { get; set; }
        public string PermDescription { get; set; }
        public string PermStatus { get; set; }
        public TimeSpan PermDuration { get; set; }
        public TimeSpan PermStartTime { get; set; }
        public TimeSpan PermEndTime { get; set; }
        public string PermComments { get; set; }
        public int EmpID { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public int YearId { get; set; }
        public int CreatedBy { get; set; }
        public string EmpName { get; set; }
        public string ManagerName { get; set; }
        public int TotalCount { get; set; }
        public string EmpNumber { get; set;}
        public int ProductID { get; set; }
        public string EntityName { get; set; }
        public string EntityType { get; set; }
        public string Designation { get; set; }
    }
    public class PermissionRequestModelMsg
    {
        public string Msg { get; set; }
        //public string ErrorMsg { get; set; }
        public int RequestID { get; set; }
    }
}