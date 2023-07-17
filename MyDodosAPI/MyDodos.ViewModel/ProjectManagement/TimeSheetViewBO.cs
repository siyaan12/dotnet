using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyDodos.Domain.ProjectManagement;
using MyDodos.ViewModel.ServerSearch;

namespace MyDodos.ViewModel.ProjectManagement
{
    public class GetTimeSheetList
    {
        public GetTimeSheetInputs objTimeSheetInput { get; set; }
        public ServerSearchable ServerSearchables { get; set; }
        public List<TimeSheetException> objTimeSheetList { get; set; }
    }
    public class GetTimeSheetInputs
    {
        public int TimeSheetID { get; set; }
        public int YearID { get; set; }
        public int EmpID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int ManagerID { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public string TimeSheetStatus { get; set; }
    }
     public class WeekDateRange
    {
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public DateTime AttendanceDate { get; set; }
        public DateTime DateField { get; set; }
        public int DayOfWeek { get; set; }
        public DateTime FirstDayOfWeek { get; set; }
        public DateTime LastDayOfWeek { get; set; }
    }
    public class vwStatusWeekTimeSheetBO
    {
        public int PTaskID { get; set; }
        public int WeekNo { get; set; }
        public int YearID { get; set; }
        public int EmpID { get; set; }
        public int TimeSheetID { get; set; }
        public string TimeSheetStatus { get; set; }
        public string TaskStatus { get; set; }
        public string PTaskIDs { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
    }
    public class TimeSheetException
    {
        public int WeekNo { get; set; }
        public int YearID { get; set; }
        public int EmpID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int ManagerID { get; set; }
        public string FullName { get; set; }
        public string EmpNumber { get; set; }
        public int TimeSheetID { get; set; }
        public string TimeSheetStatus { get; set; }
        public decimal ProjectHours { get; set; }
        public decimal NonProjectHours { get; set; }
        public decimal TotalHours { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int TotalCount { get; set; }
        public TSBillNonBillHoursBO TSHours { get; set; }
        //public ProjectPaymentStatus PaidStatus { get; set; }
    }
    public class TSBillNonBillHoursBO
    {
        public decimal ProjectHours { get; set; }
        public decimal NonProjectHours { get; set; }
        public decimal TotalHours { get; set; }
    }
    public class TimeSheetEmpReportList
    {
        public TSEmpReportInputs objTSEmpReportInput { get; set; }
        public ServerSearchable ServerSearchables { get; set; }
        public List<TSExceptionEmpReport> objTSEmpReportList { get; set; }
    }
    public class TSEmpReportInputs
    {
        public int EmpID { get; set; }
        public int ManagerID { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public string FirstName { get; set; }
    }
    public class TSExceptionEmpReport
    {
        public int EmpID { get; set; }
        public int ManagerID { get; set; }
        public string EmpNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string EmpStatus {  get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int TotalCount { get; set; }
    }
    public class TSExcReportResultList
    {
        public TSReportResultInputs objTSReportResultInput { get; set; }
        public ServerSearchable ServerSearchables { get; set; }
        public List<TSExcReportResult> objTSReportResultList { get; set; }
    }
    public class TSReportResultInputs
    {
        public int EmpID { get; set; }
        public int ProjectID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
    }
    public class TSExcReportResult
    {
        public int EmpID { get; set; }
        public string EmpNumber { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public string EmpStatus {  get; set; }
        public int ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public int PTaskID { get; set; }
        public string TaskName { get; set; }
        public DateTime? EstStartDate { get; set; }
        public DateTime? EstEndDate { get; set; }
        public decimal EstEffort { get; set; }
        public decimal TotalEffort { get; set; }
        public string ReferenceNo { get; set; }
        public int TotalCount { get; set; }
        public decimal ExcessTime { get; set; }
        public string Comments { get; set; }
    }
     public class DayWeekMonthRange
    {
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int EmpID { get; set; }
        public DateTime AttendanceDate { get; set; }
        public int DayRange { get; set; }
        public DateTime Day { get; set; }
        public int WeekRange { get; set; }
        public DateTime Week { get; set; }
        public int MonthRange { get; set; }
        public DateTime Month { get; set; }
        public DateTime FirstDayOfWeek { get; set; }
        public DateTime LastDayOfWeek { get; set; }
    }
    public class TimeSheetWeek {
        [Key]

        public int WeekNo { get; set; }
        public string Week { get; set; }
        public DateTime WeekStart { get; set; }
        public DateTime WeekEnd { get; set; }
    }
    public class TimeSheetSummaryBO
    {
        public string TimeSheetStatus { get; set; }
        public int Count { get; set; }
    }
    public class UpdateTimeSheetPaidStatusBO
    {
        public bool IsPaidStatus { get; set; }
        public int TimeSheetID { get; set; }
        public string TimeSheetStatus { get; set; }
        public int TenantID { get; set; }
    }
    public class UpdateTimeSheetPaidStatus
    {
        public List<UpdateTimeSheetPaidStatusBO> PaidStatus { get; set; }
    }
}