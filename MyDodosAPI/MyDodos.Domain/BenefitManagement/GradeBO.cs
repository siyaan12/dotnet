using System;
using System.Collections.Generic;

namespace MyDodos.Domain.BenefitManagement
{
    public class Grade
    {
        public int GradeID { get; set; }
        public decimal SalaryMin { get; set; }
        public decimal SalaryMax { get; set; }
        public int GroupTypeID { get; set; }
        public string RoleCategory { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int StageNo { get; set; }
        public string GradeName { get; set; }
        public int CreatedBy { get; set;}
        public DateTime? CreatedOn { get; set;}
        public int ModifiedBy { get; set;}
        public DateTime? ModifiedOn { get; set;}
    }
    public class RoleGrade
    {
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int ProductID { get; set; }
        public int GroupTypeID { get; set; }
        public int CreatedBy { get; set; }        
    }
}