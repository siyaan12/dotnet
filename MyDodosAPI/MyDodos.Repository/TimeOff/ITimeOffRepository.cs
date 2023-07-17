using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.TimeOff;
using MyDodos.ViewModel.TimeOff;

namespace MyDodos.Repository.TimeOff
{
    public interface ITimeOffRepository
    {
        LeaveRequestModelMsg AddNewTimeOffRequest(TimeoffRequestModel leave);
        List<TimeoffRequestModel> GetTimeOffLeaveStatus(LeaveRequestModel leave);
        List<MasLeaveCategoryBO> GetEmployeeCategory(LeaveRequestModel leave);
        List<HRVwBeneftisLeave_BO> GetTimeOffLeaveCategory(LeaveRequestModel leave);
        GetMyTimeoffList GetMyTimeoffList(GetMyTimeoffList inputParam);
        List<TimeoffRequestModel> GetTimeOffLeave(int TimeoffID, int TenantID, int LocationID);
    }
}