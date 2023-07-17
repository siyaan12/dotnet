using System.Collections.Generic;
using MyDodos.Domain.BenefitManagement;
using MyDodos.Domain.Wrapper;

namespace MyDodos.Service.BenefitManagement
{
    public interface IDisabilityBenefitService
    {
        Response<int> SaveMasDisabilityBenefit(MasDisabilityBenefitBO objben);
        Response<List<MasDisabilityBenefitBO>> GetMasDisabilityBenefit(int DisabilityBenefitID, int TenantID, int LocationID);
        Response<int> DeleteMasDisabilityBenefit(int DisabilityBenefitID, int TenantID, int LocationID);
    }
}