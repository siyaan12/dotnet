using System.Collections.Generic;
using MyDodos.Domain.BenefitManagement;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.BenefitManagement;

namespace MyDodos.Service.BenefitManagement
{
    public interface IMedicalInsuranceService
    {
        Response<List<PlanTypeCategoryBO>> GetPlanTypeCategory (int PlanTypeID,int TenantID,int LocationID);
        Response<int> SaveHRBenefitPlans(HRBenefitPlansBO objplan);
        Response<List<HRBenefitPlansBO>> GetHRBenefitPlans(HRBenefitPlansBO objplan);
        Response<int> DeleteHRBenefitPlans(HRBenefitPlansBO objplan);
        Response<HRBenefitPlansSearchBO> GetHRBenefitPlansSearch(HRBenefitPlansSearchBO objplan);
    }
}