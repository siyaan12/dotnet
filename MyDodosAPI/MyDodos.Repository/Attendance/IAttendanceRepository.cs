using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDodos.Domain.Attendance;
using MyDodos.Domain.Employee;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.PermissionBO;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.Attendance;
using MyDodos.ViewModel.Common;

namespace MyDodos.Repository.Attendance
{
    public interface IAttendanceRepository
    {
        SaveOut SaveAttendanceUserLogData(MachineInfo attend);
        EmployeeAttendanceBO SaveSwipeAttendanceData(MachineInfo attend,int EmpID);
        SaveOut SaveAttendanceRoster(EmployeeRosterBO objrost);
        SaveOut SaveEmployeeAttendance(EmployeeAttendanceBO objatt, int RosterID);
        List<EmployeeRosterBO> GetEmpAttendanceRoster(EmpAttendanceInputBO objattend);
        EmployeeRosterSearchBO GetEmployeeRosterSearch(EmployeeRosterSearchBO objplan);
        List<EmployeeAttendanceBO> GetEmpAttendance(int EmpID, DateTime RosterDate,int TenantID,int LocationID);
        List<EmployeeRosterBO> GetAttendanceRoster(AttendanceInputBO _att);
        List<EmployeeAttendanceBO> GetAttendanceEmp(AttendanceInputBO _att);
        List<AttendanceTerminalBO> GetTerminalData(TerminalInputBO objinput);
        bool GetCheckInMsg(int EmpID, int LocationID, int TenantID);
        StatusCountBO GetCountData(EmployeeAttendanceInputBO objemp);
        // List<EmployeeRosterBO> GetGraphData(EmployeeAttendanceInputBO objemp);
        List<LeaveRequestBO> GetLeaveRequestData(EmployeeAttendanceInputBO objemp);
        List<PermissionRequestBO> GetPermissionRequestData(EmployeeAttendanceInputBO objemp);
        // List<TardiesBO> GetTardiesData(EmployeeAttendanceInputBO objemp);
        EmployeeBO GetEmployee(EmployeeAttendanceInputBO objemp);
        EmployeeAttendanceBO AttendanceInandOut(AttendanceInandOutBO _shiftin);
        string AttendanceRosterGenerted(ShifRosterGenertedBO _shift);
        List<EmployeeBO> GetAttendanceEmployeeList(int TenantID, int LocationID);
        EmpDetailsBO GetEmpDetails(int EmpID,string AttendanceDate);
        string UpdateEmployeeStatus (EmpStatusBO objsts);
        SaveOut SaveAttendacePIN(EmpAttendanceConfigBO objconfig,string AttendCardNo);
        string GetEmpNumber(int EmpID);
        //EmployeeRosterSearchBO GetEmployeeRosterSearch(EmployeeRosterSearchBO objplan);
        //List<EmployeeAttendanceBO> GetEmployeeAttendance(int EmpID, DateTime RosterDate,int TenantID,int LocationID);
        List<EmployeeListBO> GetEmployeeList(int TenantID,int LocationID);
        // Shift Scheduling
        SaveOut AddAttendanceProfile(WorkingHours hours);
        List<WorkingHours> GetShiftList(int TenantID,int LocationID);
        List<ReturnShiftBO> GetShiftName(int TenantID,int LocationID);
        List<ReturnShiftSchdBO> GetAttendanceSchedule(int TenantID,int LocationID);
        SaveOut AttendanceRoster(HRRoasterBO roster);
        List<WorkingHours> GetWorkingHours(int TenantID,int LocationID,string ShiftName);
        List<ReturnEmpShiftNameBO> GetShiftRoasterEmp(int TenantID,int LocationID,int ShiftID);
        List<HRRoasterBO> GetEmpShift(DateTime StartDate,DateTime EndDate,int ShiftID,int EmpID);
        List<EmployeeListsBO> GetEmployeeList(int TenantID, int LocationID, int EmpID);
    }
}