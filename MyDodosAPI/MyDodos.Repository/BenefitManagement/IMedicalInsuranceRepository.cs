using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDodos.Domain.BenefitManagement;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.BenefitManagement;
using MyDodos.ViewModel.Common;

namespace MyDodos.Repository.BenefitManagement
{
    public interface IMedicalInsuranceRepository
    {
        List<PlanTypeCategoryBO> GetPlanTypeCategory (int PlanTypeID,int TenantID,int LocationID);
        SaveOut SaveHRBenefitPlans(HRBenefitPlansBO objplan);
        List<HRBenefitPlansBO> GetHRBenefitPlans(HRBenefitPlansBO objplan);
        SaveOut DeleteHRBenefitPlans(HRBenefitPlansBO objplan);
        HRBenefitPlansSearchBO GetHRBenefitPlansSearch(HRBenefitPlansSearchBO objplan);
        List<HRBenefitPlansBO> GetHRBenefitPlansByEmp(int PlanTypeID,int TenantID,int LocationID);
    }
}