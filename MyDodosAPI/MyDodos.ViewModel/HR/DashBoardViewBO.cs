using System;
using System.Collections.Generic;
using MyDodos.ViewModel.Employee;
using MyDodos.ViewModel.ProjectManagement;

namespace MyDodos.Domain.HR
{
    public class DashBoard
    {
        public HRDirectorySummeryBO EmployeeSummary { get; set; }
        public AttendanceSummary AttendanceSummary { get; set; }
        public ProjectSummary ProjectSummary { get; set; }
        public List<UpcomingBirthday> TodayBirthday { get; set; }
        public List<UpcomingBirthday> UpcomingBirthdays { get; set; }
        public List<EventList> Events { get; set; }
        public List<EmployeeList> EmployeeLists { get; set; }
        public List<PPProjectListBO> Project { get; set; }
        public List<WorkingFormat> WorkingFormat { get; set; }
        public List<RecruitmentProgress> RecruitmentProgress { get; set; }
    }
    public class EmployeeSummary
    {
        public int TotalEmployee { get; set; }
        public int NewEmployees { get; set; }
        public int NumberofLeave { get; set; }
        public decimal HappinesRate { get; set; }
    }
    public class ProjectSummary
    {
        public int TotalProjects { get; set; }
        public int ActiveProjects { get; set; }
        public int TotalTeamMembers { get; set; }
        public int ActiveMembers { get; set; }
        public int InActiveMembers { get; set; }
        public int ApprovedLeave { get; set; }
    }
    public class  WorkingFormat
    {
        public int Total { get; set; }
    }
    public class RecruitmentProgress
    {
        public string FullName { get; set; }
        public DateTime? Date { get; set; }
        public string Department { get; set; }
        public string InterViewType { get; set; }
    }
    public class EventList
    {
        public string EventType { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
    }
    public class EmployeeList
    {
        public int EmpID { get; set; }
        public string EmpNumber { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime DOJ { get; set; }
        public DateTime DOB { get; set; }
        public string OfficeEmail { get; set; }
        public string PersonalMail { get; set; }
        public string Designation { get; set; }
        public string EmpStatus { get; set; }
        //public int DepartmentID { get; set; }
        public string Department { get; set; }
        //public int LocationID { get; set; }
        //public int TenantID { get; set; } 
        //public int AppUserID { get; set; }
        public string base64images { get; set; }
    }
    public class UpcomingBirthday
    {
        public int EmpID { get; set; }
        public string EmpNumber { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string base64images { get; set; }
    }
    public class AttendanceSummary
    {
        public int Regular { get; set; }
        public int Late { get; set; }
        public int Absent { get; set; }
        public int Permission { get; set; }
    }
}