using System;
using System.Collections.Generic;
namespace MyDodos.Domain.PermissionBO
{
    public class SpecialPermissionBO
    {
        public int PermissionID { get; set; }
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
        public DateTime? CreatedOn { get; set;}
        public DateTime? ModifiedOn { get; set;}
        public int ModifiedBy { get; set;}
        public string EmpName { get; set; }
        public string ManagerName { get; set; }
        public int TotalCount { get; set; }
        public string EmpNumber { get; set;}
    }
}