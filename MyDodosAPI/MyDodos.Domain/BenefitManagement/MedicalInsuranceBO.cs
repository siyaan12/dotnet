using System;
using System.Collections.Generic;

namespace MyDodos.Domain.BenefitManagement
{
    public class PlanTypeCategoryBO
    {
        public int PlanTypeID { get; set;}
        public string PlanType { get; set;}
        public string PlanTypeCategory { get; set;}
        public int TenantID { get; set;}
        public int LocationID { get; set;}
        public int CreatedBy { get; set;}
        public DateTime? CreatedOn { get; set;}
        public int ModifiedBy { get; set;}
        public DateTime? ModifiedOn { get; set;}
        public List<HRBenefitPlansBO> objmedplan { get; set; }
    }
    public class HRBenefitPlansBO
    {
        public int BenePlanID { get; set;}
        public int TenantID { get; set;}
        public int LocationID { get; set;}
        public string PlanName { get; set;}
        public string Provider { get; set;}
        public string GroupPolicyNumber { get; set;}
        public bool IsEnrollmentRequired { get; set;}
        public DateTime? EnrollmentStart { get; set;} 
        public DateTime? EnrollmentEnd { get; set;}
        public int YearID { get; set;}
        public string PlanStatus { get; set;}
        public decimal? DeductibleInd { get; set;}
        public decimal? DeductibleFamily { get; set;}
        public decimal? OutOfPocketInd { get; set;}
        public decimal? OutOfPocketFamily { get; set;}
        public int PlanTypeID { get; set;}
        public string PlanType { get; set;}
        public decimal? PremiumInd { get; set;}
        public decimal? PremiumFamily { get; set;}
        public int CreatedBy { get; set;}
        public DateTime? CreatedOn { get; set;}
        public int ModifiedBy { get; set;}
        public DateTime? ModifiedOn { get; set;}
    }
    public class HRBenefitPlansOutputBO
    {
        public int BenePlanID { get; set;}
        public int TenantID { get; set;}
        public int LocationID { get; set;}
        public string PlanName { get; set;}
        public string Provider { get; set;}
        public string GroupPolicyNumber { get; set;}
        public bool IsEnrollmentRequired { get; set;}
        public DateTime? EnrollmentStart { get; set;} 
        public DateTime? EnrollmentEnd { get; set;}
        public int YearID { get; set;}
        public string PlanStatus { get; set;}
        public decimal DeductibleInd { get; set;}
        public decimal DeductibleFamily { get; set;}
        public decimal OutOfPocketInd { get; set;}
        public decimal OutOfPocketFamily { get; set;}
        public int PlanTypeID { get; set;}
        public string PlanType { get; set;}
        public decimal PremiumInd { get; set;}
        public decimal PremiumFamily { get; set;}
        public int CreatedBy { get; set;}
        public DateTime? CreatedOn { get; set;}
        public int ModifiedBy { get; set;}
        public DateTime? ModifiedOn { get; set;}
        public int RowNum { get; set;}
        public int TotalCount { get; set;}
    }
}