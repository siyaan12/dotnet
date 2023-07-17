using System.Collections.Generic;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.TimeOff;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.TimeOff;

namespace MyDodos.Service.TimeOff
{
    public interface ITimeOffService
    {
        Response<LeaveRequestModelMsg> AddNewTimeOffRequest(TimeoffRequestModel leave);
        Response<GetTimeoffRequestBO> GetTimeoffRequestList(int TenantID, int YearId, int LocationID, int EmpId);
        Response<List<HRVwBeneftisLeave_BO>> GetTimeOffLeaveCategory(LeaveRequestModel leave);
        Response<GetMyTimeoffList> GetMyTimeoffList(GetMyTimeoffList objresult);

    }
}