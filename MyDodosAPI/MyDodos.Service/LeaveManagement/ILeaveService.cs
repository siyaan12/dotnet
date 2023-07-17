using System.Collections.Generic;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.Holiday;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.LeaveManagement;
using MyDodos.Domain.Mail;

namespace MyDodos.Service.LeaveManagement
{
    public interface ILeaveService
    {
        //Response<AuthLoginBO> GetLogin(InputLogin inputLogin);
        Response<LeaveRequestModelMsg> AddNewLeaveRequest(LeaveRequestModel leave);
        Response<List<MasLeaveCategoryBO>> GetCategoryList(LeaveRequestModel leave);
        Response<GetMyLeaveBO> GetEmpLeaveList(int teantID, int YearId, int LocationID, int EmpId);
        Response<int> SaveEmployeeHoliday(EmployeeHolidayBO _holiday);
        Response<int> SaveLeaveCategoryMaster(HRVwBeneftisLeave_BO category);
        Response<List<HRVwBeneftisLeave_BO>> GetLeaveCategoryMaster(int TenantID,int LocationID);
        Response<GetMyLeaveList> GetMyLeaveList(GetMyLeaveList objresult);
        int SendLeave(LeaveRequestModel leave, List<MailNotifyBO> standarddata);
        // Mobile API
        Response<LeaveRequestModelMsg> MAddNewLeaveRequest(LeaveRequestModel leave);
        Response<List<HRGetMyLeaveList>> MGetMyLeaveList(GetMyLeaveListInputs objresult);
        Response<List<MobileLeaveCategoryBO>> MGetCategoryList(LeaveRequestModel leave);
    }
}