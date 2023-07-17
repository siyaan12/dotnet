using System;
using System.Collections.Generic;
using MyDodos.Domain.AuthBO;
using MyDodos.Domain.LeaveBO;

namespace MyDodos.Domain.BenefitManagement
{
    public class BenefitCategoryGradeBO
    {
        public List<RtnUserGroupBO> objgrade { get; set; }
        public List<HRVwBeneftisLeave_BO> objleave { get; set; }
    }
    public class MasLeaveGroupBO
    {
        public int LeaveGroupID { get; set; }
        public string LeaveGroupName { get; set; }
        public string LeaveGroupCategory { get; set; }
        public int GroupTypeID { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public string LeaveGroupStatus { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public List<LeaveAllocReferenceBO> objalloc { get; set; }
    }
    public class LeaveAllocReferenceBO
    {
        public int LeaveAllocationID { get; set; }
        public int LeaveGroupID { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public decimal? NoOfLeave { get; set; }
        public string AllocationPeriod { get; set; }
        public bool? isRollOver { get; set; }
        public bool? AccureAtEnd { get; set; }
        public string Notes { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string CarryForward { get; set; }
    }
}