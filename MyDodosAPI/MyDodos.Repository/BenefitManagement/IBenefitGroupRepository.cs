using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDodos.Domain.BenefitManagement;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.Common;

namespace MyDodos.Repository.BenefitManagement
{
    public interface IBenefitGroupRepository
    {
        SaveOut SaveBenefitMedPlan(BenefitGroupMedPlanBO objplan,int BenefitGroupID);
        SaveOut SaveBenefitGroup(BenefitGroupBO objgroup);
        List<BenefitGroupBO> GetBenefitGroup(int BenefitGroupID, int TenantID, int LocationID);
        List<BenefitGroupMedPlanBO> GetBenefitMedPlan(int BenefitGroupID, int TenantID, int LocationID);
        SaveOut DeleteBenefitGroup(int BenefitGroupID, int TenantID, int LocationID);
        SaveOut DeleteBenefitMedPlan(int BenefitGroupID, int TenantID, int LocationID);
    }
}