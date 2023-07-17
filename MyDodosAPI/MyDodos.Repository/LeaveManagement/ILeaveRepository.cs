using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.LoginBO;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.Common;
using MyDodos.ViewModel.LeaveManagement;

namespace MyDodos.Repository.LeaveManagement
{
    public interface ILeaveRepository
    {
        Response<List<LoginLocationBO>> GetLocation(int TenantID, int LocationID);
        LeaveRequestModelMsg AddNewLeaveRequest(LeaveRequestModel leave);
        List<HRGetMyLeaveList> GetLeaveStatus(LeaveRequestModel leave);
        List<LeaveRequestModel> GetReportandEmpList(LeaveRequestModel leave);
        List<MasLeaveCategoryBO> GetCategoryList(LeaveRequestModel leave);
        List<MasLeaveCategoryBO> GetEmployeeCategory(LeaveRequestModel leave);
        List<HRGetMyLeaveList> GetLeave(int LeaveID, int TenantID, int LocationID);
        int SaveLeaveAlloc(HRGetMyLeaveList leave);
        int SaveCategoryList(HRGetMyLeaveList leave);
        SaveOut SaveLeaveCategoryMaster(HRVwBeneftisLeave_BO category);
        List<HRVwBeneftisLeave_BO> GetLeaveCategoryMaster(int TenantID,int LocationID);
        GetMyLeaveList GetMyLeaveList(GetMyLeaveList inputParam);
        List<HRGetMyLeaveList> GetLeaveLOPList(LeaveRequestModel leave);
        //Mobile API
        LeaveRequestModelMsg MAddNewLeaveRequest(LeaveRequestModel leave);
        List<HRGetMyLeaveList> MGetLeave(int LeaveID, int TenantID, int LocationID);
        int MSaveLeaveAlloc(HRGetMyLeaveList leave);
        List<HRGetMyLeaveList> MGetMyLeaveList(GetMyLeaveListInputs inputParam);
        List<MobileLeaveCategoryBO> MGetCategoryList(LeaveRequestModel leave);
        int MSaveCategoryList(HRGetMyLeaveList leave);
    }
}