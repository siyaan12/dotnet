using System;
using System.Collections.Generic;

namespace MyDodos.Domain.BenefitManagement
{
    public class MasDisabilityBenefitBO
    {
        public int DisabilityBenefitID { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public string DisabilityType { get; set; }
        public int BenefitValue { get; set; }
        public string BenefitValueType { get; set; }
        public int ConditionValue { get; set; }
        public string ConditionValueType { get; set; }
        public string Notes { get; set; }
        public string BenefitStatus { get; set; }
        public int GroupTypeID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}