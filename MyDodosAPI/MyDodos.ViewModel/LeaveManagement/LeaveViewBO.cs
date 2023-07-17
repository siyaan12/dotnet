using System;
using System.Collections.Generic;
using MyDodos.Domain.Holiday;
using MyDodos.Domain.LeaveBO;
using MyDodos.ViewModel.ServerSearch;

namespace MyDodos.ViewModel.LeaveManagement
{
    public class GetMyLeaveBO {
        //public HRBenefitsByGroupBO LeaveJournal { get; set; }
        public List<HRGetMyLeaveList> OutStandLeave { get; set; }
        public List<HRGetMyLeaveList> OutStandRequest { get; set; }
        public List<HRGetMyLeaveList> TeamMember { get; set; } 
        public List<HolidayBO> EmpHoliday { get; set; }
        public List<HolidayBO> EmpOptinalHoliday { get; set; }
        public HolidayBO UpcomingHoliday { get; set; }
        public List<MasYearBO> YearDetails { get; set; }
        public List<MasLeaveCategoryBO> LeaveJourney { get; set; }
        public List<HolidayBO> Holiday { get; set; }
        public List<HRGetMyLeaveList> LOPLeave { get; set; }
    }
    public class HRGetMyLeaveList {
        public int LeaveID { get; set; }
        public DateTime LeaveFrom { get; set; }
        public DateTime LeaveTo { get; set; }
        public string LeaveCategory { get; set; }
        public string LeaveType { get; set; }
        public decimal NoOfDays { get; set; }
        public string LeaveReason { get; set; }
        public string LeaveStatus { get; set; }
        public string LeaveComments { get; set; }
        public DateTime RequestDate { get; set; }
        public decimal IsLOP { get; set; }
        public decimal LopCount { get; set; }
        public int EmpID { get; set; }
        public string EmpNumber { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public int LeaveCategoryID { get; set; }
        public int YearID { get; set; }
        public int CreatedBy { get; set; }
        public string EmpName { get; set; }
        public string ManagerName { get; set; }
        public int TotalCount { get; set; }
        public string Designation { get; set; }     
    }
    public class MyTeameLeaveList {
        public int LeaveID { get; set; }
        public DateTime LeaveFrom { get; set; }
        public DateTime LeaveTo { get; set; }
        public string LeaveCategory { get; set; }
        public string LeaveType { get; set; }
        public decimal NoOfDays { get; set; }
        public string LeaveReason { get; set; }
        public string LeaveStatus { get; set; }
        public string LeaveComments { get; set; }
        public DateTime RequestDate { get; set; }
        public decimal IsLOP { get; set; }
        public int EmpID { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public int LeaveCategoryID { get; set; }
        public int YearID { get; set; }
        public int CreatedBy { get; set; }
        public string EmpName { get; set; }
        public string ManagerName { get; set; }
        public int TotalCount { get; set; }
    }
    public class GetMyLeaveList
    {
        public GetMyLeaveListInputs objMyLeaveListInput { get; set; }
        public ServerSearchable ServerSearchables { get; set; }
        public List<HRGetMyLeaveList> objMyLeaveList { get; set; }

    }
    public class GetMyLeaveListInputs
    {
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int YearID { get; set; }
        public int EmpID { get; set; }
        public int ManagerID { get; set; }
        public string LeaveStatus { get; set; }
    }
}