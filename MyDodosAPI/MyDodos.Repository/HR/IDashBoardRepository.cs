using System;
using System.Collections.Generic;
using MyDodos.Domain.HR;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.Employee;

namespace MyDodos.Repository.HR
{
    public interface IDashBoardRepository
    {
        ProjectSummary GetProjectDashboardSummery(int TenantID,int LocationID);
        List<EmployeeList> GetEmployeeList(int TenantID,int LocationID,int EmpID);
        List<UpcomingBirthday> GetUpcomingBirthday(int TenantID,int LocationID,bool TodayBirthday);
        HRDirectorySummeryBO GetEmployeeDashBoardSummary(int TenantID,int LocationID,int DepartmentID);
        AttendanceSummary GetAttendanceDashBoardSummary(int TenantID,int LocationID,int DepartmentID);
        List<EventList> GetDashBoardEvents(int TenantID,int LocationID,int YearID);
    }
}