using System.Collections.Generic;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.ProjectManagement;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.ProjectManagement;
using System;

namespace MyDodos.Service.ProjectManagement
{
    public interface ITimeSheetService
    {
        Response<LeaveRequestModelMsg> SaveTimeSheet(PPTimeSheetBO timeSheet);
        Response<TimeSheetTaskBO> GetTimeSheetTasks(TimesheetInputBO task);
        Response<LeaveRequestModelMsg> SaveWeekTSNonBillable(BillNonBillable timeSheet);
        Response<List<PPTimeSheetBO>> GetTimeSheetList(TimesheetInputBO list);
        Response<PPTimeSheetBO> TSBillableNonBillableList(TimesheetInputBO list);
        Response<GetTimeSheetList> GetTimeSheetData(GetTimeSheetList timesheet);
        Response<int> GetWeekcount();
        Response<List<WeekDateRange>> GetWeekDateRange(int TenantID,int LocationID,DateTime AttendanceDate);
        Response<LeaveRequestModelMsg> SaveTimeSheetTaskApply(PPWeekTimeSheetBO timeSheet);
        Response<TimeSheetFlaggedBO> SaveTimeSheetFlagged(TimesheetInputBO timeSheet);
        Response<List<TimeSheetWeek>> GetWeekDropdown();
        Response<TimeSheetEmpReportList> GetTimeSheetEmpReportData(TimeSheetEmpReportList timesheet);
        Response<TSExcReportResultList> GetTSExcReportResult(TSExcReportResultList timesheet);
        Response<List<TimeSheetException>> GetConsoleTimesheet(int TenantID,int LocationID);
        Response<List<TimeSheetSummaryBO>> GetTimeSheetSummary(int TenantID, int LocationID,int EmpID,int ManagerID);
        Response<LeaveRequestModelMsg> UpdateTimeSheetPaidStatus(List<UpdateTimeSheetPaidStatusBO> project);
    }
}