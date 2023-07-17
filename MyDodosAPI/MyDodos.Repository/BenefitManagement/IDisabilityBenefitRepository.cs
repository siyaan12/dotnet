using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDodos.Domain.BenefitManagement;
using MyDodos.ViewModel.Common;

namespace MyDodos.Repository.BenefitManagement
{
    public interface IDisabilityBenefitRepository
    {
        SaveOut SaveMasDisabilityBenefit(MasDisabilityBenefitBO objben);
        List<MasDisabilityBenefitBO> GetMasDisabilityBenefit(int DisabilityBenefitID, int TenantID, int LocationID);
        SaveOut DeleteMasDisabilityBenefit(int DisabilityBenefitID, int TenantID, int LocationID);
        List<MasDisabilityBenefitBO> GetMasDisabilityBenefitByGroupType(int GroupTypeID, int TenantID, int LocationID);
    }
}