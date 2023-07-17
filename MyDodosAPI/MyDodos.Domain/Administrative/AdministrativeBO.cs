using System;
using System.Collections.Generic;
namespace MyDodos.Domain.Administrative
{
    public class MasDepartmentBO
    {
        public int DeptID { get; set; }
        public string DepartmentCode { get; set; }
        public string Department { get; set; }
        public string DeptHead { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string DeptType { get; set; }
        public string DeptShortName { get; set; }
        public string DeptStatus { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int TotalCount { get; set; }
    }
}