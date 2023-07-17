using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.ProjectManagement;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.ProjectManagement;

namespace MyDodos.Repository.ProjectManagement
{
    public interface ITimeSheetRepository
    {
        LeaveRequestModelMsg SaveTimeSheet(PPTimeSheetBO timeSheet);
        List<TaskResourceListModel> GetTimeSheetTasks(TimesheetInputBO task);
        LeaveRequestModelMsg SaveWeekTSNonBillable(PPWeekTSNonBillableBO timeSheet);
        List<PPTimeSheetBO> GetTimeSheetList(TimesheetInputBO list);
        LeaveRequestModelMsg SaveWeekTimeSheet(PPWeekTimeSheetBO timeSheet);
        LeaveRequestModelMsg SaveMasterWeekTSNonBillable(PPWeekTSNonBillableBO timeSheet);
        PPTimeSheetBO GetTimeSheetDataList(TimesheetInputBO list);
        List<PPWeekTimeSheetBO> GetTimeSheetBillableData(int EmpID,int TimeSheetID);
        List<PPWeekTSNonBillableBO> GetTimeSheetNonBillableData(int EmpID,int TimeSheetID);
        GetTimeSheetList GetTimeSheetData(GetTimeSheetList inputParam);
        TSBillNonBillHoursBO GetTSBillNonBillHours(TimeSheetException timeSheet);
        int GetWeekcount();
        List<WeekDateRange> GetWeekDateRange(int TenantID,int LocationID,DateTime AttendanceDate);
        LeaveRequestModelMsg SaveTimeSheetTaskApply(PPWeekTimeSheetBO timeSheet);
        LeaveRequestModelMsg SaveTimesheetOverAll(vwStatusWeekTimeSheetBO timeSheet);
        List<PPTimeSheetBO> SaveTimeSheetFlagged(TimesheetInputBO timeSheet);
        TimeSheetEmpReportList GetTimeSheetEmpReportData(TimeSheetEmpReportList inputParam);
        TSExcReportResultList GetTSExcReportResult(TSExcReportResultList inputParam);
        List<DayWeekMonthRange> GetDayWeekMonthRange(int TenantID,int LocationID,int EmpID,DateTime AttendanceDate);
        List<TimeSheetWeek> GetWeekDropdown();
        List<TimeSheetException> GetConsoleTimesheet(int TenantID,int LocationID);
        //string GetWeekDateSpan (int WeekNo, int dtYear);
        //DateTime FirstDateOfWeek (int year, int weekOfYear, System.Globalization.CultureInfo ci);
        LeaveRequestModelMsg UpdateTimeSheetFlagRelease(PPWeekTSNonBillableBO timeSheet);
        List<PPWeekTSNonBillableBO> GetTimeSheetTasksNBData(PPWeekTSNonBillableBO nbtask);
        List<TimeSheetSummaryBO> GetTimeSheetSummary(int TenantID,int LocationID,int EmpID,int ManagerID);
        ProjectPaymentStatus GetTimeSheetPaidStatus(int TenantID,int YearID,int WeekNo,int EmpID);
        LeaveRequestModelMsg UpdateTimeSheetPaidStatus(UpdateTimeSheetPaidStatusBO timeSheet);
    }
}