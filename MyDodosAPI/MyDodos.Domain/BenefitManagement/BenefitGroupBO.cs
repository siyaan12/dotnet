using System;
using System.Collections.Generic;

namespace MyDodos.Domain.BenefitManagement
{
    public class BenefitGroupMedPlanBO
    {
        public int BenGrpMedPlanID { get; set; }
        public int BenefitGroupID { get; set; }
        public int MedBenePlanID { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsCheckList { get; set; }
        public string PlanType { get; set; }
    }
    public class BenefitGroupBO
    {
        public int BenefitGroupID { get; set; }
        public string GroupName { get; set; }
        public string GroupCategory { get; set; }
        public int LeaveGroupID { get; set; }
        public string BenefitGroupStatus { get; set; }
        public bool IsBusinessCard { get; set; }
        public bool IsCorpCard { get; set; }
        public bool IsGuestHouse { get; set; }
        public bool isPerks { get; set; }
        public int GroupTypeID { get; set; }
        public string GroupType { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int ReviewedBy { get; set; }
        public DateTime? ReviewedOn { get; set; }
        public int ApprovedBy { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public List<BenefitGroupMedPlanBO> objplan { get; set;}
    }
}