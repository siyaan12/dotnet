using System;
using System.Collections.Generic;
namespace MyDodos.Domain.ProjectManagement
{
     public class PPTimeSheetBO
    {
        public int TimeSheetID { get; set; }
        public int WeekNo { get; set; }
        public int YearID { get; set; }
        public int EmpID { get; set; }
        public string Comments { get; set; }
        public string TimeSheetStatus { get; set; }
        public DateTime? SubmitedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsLocked { get; set; }
        public DateTime? Locktime { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsTSException { get; set; }
        public bool IsCurrException { get; set; }
        public DateTime? ExceptionCleardate { get; set; }
        public decimal TSHours { get; set; }
        public decimal TSNonBillHours { get; set; }
        public DateTime? ReleaseDate { get; set; } 
        public int ApprovedBy { get; set; }
        public string ManagerComment { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
        public int TotalCount { get; set; }
        public bool IsPaidStatus { get; set; }
        public DateTime GetDateTime { get; set; }
        public List<PPWeekTimeSheetBO> TimeSheetBillableData { get; set; }
        public List<PPWeekTSNonBillableBO> TimeSheetNonBillableData { get; set; }
    }
    public class TimeSheetFlaggedBO
    {
        public int TimeSheetCount { get; set; }
    }
    public class PPWeekTSNonBillableBO
    {
        public int TimeSheetTaskID { get; set; }
        public int PTaskID { get; set; }
        public decimal Day1 { get; set; }
        public decimal Day2 { get; set; }
        public decimal Day3 { get; set; }
        public decimal Day4 { get; set; }
        public decimal Day5 { get; set; }
        public decimal Day6 { get; set; }
        public decimal Day7 { get; set; }
        public int WeekNo { get; set; }
        public int YearID { get; set; }
        public string Comments { get; set; }
        public int EmpID { get; set; }
        public int TimeSheetID { get; set; }
        public string TimeSheetNBStatus { get; set; }
        public DateTime? EstStartDate { get; set; }
        public string LeaveHoliday { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime GetDateTime { get; set; }
    }
    public class TaskResourceListModel
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public int EmpID { get; set; }
        public int PTaskID { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string TaskPriority { get; set; }
        public DateTime EstStartDate { get; set; }
        public DateTime EstEndDate { get; set; }
        public DateTime ActStartDate { get; set; }
        public DateTime ActEndDate { get; set; }
        public int TimeSheetID { get; set; }
        public int TimeUnquieID { get; set; }
        public string TimeSheetStatus { get; set; }
    }
    public class TimeSheetTaskBO
    {
        //public int ProductID { get; set; }
        //public string ProjectName { get; set; }
        public int TimeSheetID { get; set; }
        public List<TaskResourceListModel> Task { get; set; }
    }
    public class PPWeekTimeSheetBO
    {
        public int TimeSheetTaskID { get; set; }
        public int PTaskID { get; set; }
        public decimal Day1 { get; set; }
        public decimal Day2 { get; set; }
        public decimal Day3 { get; set; }
        public decimal Day4 { get; set; }
        public decimal Day5 { get; set; }
        public decimal Day6 { get; set; }
        public decimal Day7 { get; set; }
        public int WeekNo { get; set; }
        public int YearID { get; set; }
        public string Comments { get; set; }
        public int EmpID { get; set; }
        public int TimeSheetID { get; set; }
        public string WTimeSheetStatus { get; set; }
        public string PTaskIDs { get; set; }
        public string TaskName { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
    }
    public class BillNonBillable
    {
        public int TimeSheetID { get; set; }
        public List<PPWeekTimeSheetBO> Billable { get; set;}
        public List<PPWeekTSNonBillableBO> NonBillable { get; set; }
    }
    public class TimeSheetStatusBO
    {
        public string TimeSheetIDs { get; set; }
        public bool IsPaidStatus { get; set; }
    }
    public class TimesheetInputBO
    {
        public int EmpID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TimeSheetID { get; set; }
        public int YearID { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int ProjectID { get; set; }
        public string TimeSheetStatus { get; set; }
        public DateTime GetDateTime { get; set; }
    }
}