using System.Collections.Generic;
using MyDodos.Domain.BenefitManagement;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.BenefitManagement;

namespace MyDodos.Service.BenefitManagement
{
    public interface ILeaveBenefitService
    {
        Response<List<HRVwBeneftisLeave_BO>> GetBenefitLeaveCategory(int TenantID, int LocationID);
        Response<int> SaveLeaveBenefits(MasLeaveGroupBO objgroup);
        Response<List<MasLeaveGroupBO>> GetLeaveBenefits(int LeaveGroupID, int TenantID, int LocationID);
        Response<List<MasLeaveGroupBO>> GetLeaveBenefitsByGroupType(int GroupTypeID, int TenantID, int LocationID);
        Response<int> DeleteLeaveBenefits(int LeaveGroupID, int TenantID, int LocationID);
    }
}