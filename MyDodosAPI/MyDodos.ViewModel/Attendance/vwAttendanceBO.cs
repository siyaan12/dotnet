using MyDodos.Domain.Attendance;
using MyDodos.Domain.Employee;
using MyDodos.ViewModel.ServerSearch;
using System;
using System.Collections.Generic;

namespace MyDodos.ViewModel.Attendance
{
    public class EmployeeRosterSearchBO
    {
        public  EmployeeAttendanceInputBO objinput { get; set;}
        public ServerSearchable ServerSearchables { get; set; }
        public List<EmployeeRosterOutputBO> objoutlist { get; set;}
    }
    public class AttendanceTrackingBO
    {
        public StatusCountBO objcount { get; set; }
        public EmployeeRosterSearchBO objros { get; set; }
        public List<LeaveRequestBO> objleave { get; set; }
        public List<PermissionRequestBO> objperm { get; set; }
        // public List<TardiesBO> objtardies { get; set; }
        public EmployeeBO emp { get; set; }
    }
    public class EmployeeAttendanceInputBO
    {
        public int EmpID { get; set;}
        public int TenantID { get; set;}
        public int LocationID { get; set;}
        public string AttendanceDate { get; set;}
        public int DepartmentID { get; set;}
    }
    public class EmployeeAttendanceSearchBO
    {
        public  EmployeeAttendanceInputBO objinput { get; set;}
        public ServerSearchable ServerSearchables { get; set; }
        public List<EmployeeAttendanceOutputBO> objemplist { get; set;}
    }
    // public class AttendanceRosterBO
    // {
    //     public int RosterID { get; set; }
    //     public int ShiftID { get; set; }
    //     public int EmpID { get; set; }
    //     public int LocationID { get; set; }
    //     public int TenantID { get; set; }
    //     public DateTime RosterDate { get; set; }
    //     public bool IsRotated { get; set; }
    //     public bool IsLastShift { get; set; }
    //     public bool IsEnable { get; set; }
    //     public int DayID { get; set; }
    //     public string RosterStatus { get; set; }
    //     public DateTime StartDate { get; set; }
    //     public DateTime EndDate { get; set; }
    //     public int CreatedBy { get; set; }
    //     public DateTime? CreatedOn { get; set; }
    //     public int ModifiedBy { get; set; }
    //     public DateTime? ModifiedOn { get; set; }
    // }
}