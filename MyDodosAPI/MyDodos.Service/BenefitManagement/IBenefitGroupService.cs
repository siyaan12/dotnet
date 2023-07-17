using System.Collections.Generic;
using MyDodos.Domain.BenefitManagement;
using MyDodos.Domain.Wrapper;

namespace MyDodos.Service.BenefitManagement
{
    public interface IBenefitGroupService
    {
        Response<int> SaveBenefitGroup(BenefitGroupBO objgroup);
        Response<List<BenefitGroupBO>> GetBenefitGroup(int BenefitGroupID, int TenantID, int LocationID);
        Response<int> DeleteBenefitGroup(int BenefitGroupID, int TenantID, int LocationID);
    }
}