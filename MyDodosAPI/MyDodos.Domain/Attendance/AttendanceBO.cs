using System;
using System.Collections.Generic;
using System.ComponentModel;
using MyDodos.Domain.Employee;
namespace MyDodos.Domain.Attendance
{
    public class MachineInfo
    {
        public int MachineID { get; set; }
        public int MachineNumber { get; set; }
        public int IndRegID { get; set; }
        public string DateTimeRecord { get; set; }
        public string Mode { get; set; }
        public string DateOnlyRecord { get; set; }
        public string TimeOnlyRecord { get; set; }
    }
    public class InputDeviceData
    {
        public int DeviceID { get; set; }
        public string DeviceStatus { get; set; }
        public string DevicePurpose { get; set; }
        public string MachineUniqueID { get; set; }
        public int EntityID { get; set; }
        public int TenantID { get; set; }
    }
    public class DeviceUnique
    {
        public string UniqueID { get; set; }
        public string MachineUniqueID { get; set; }
        public int EntityID { get; set; }
        public int TenantID { get; set; }
    }
    // public class DeviceTypeMaster
    // {
    //     public int DeviceTypeID { get; set; }
    //     public string DeviceType { get; set; }
    //     public string DeviceName { get; set; }
    //     public bool IsDevice { get; set; }
    //     public int TenantID { get; set; }
    //     public int EntityID { get; set; }
    //     public int CreatedBy { get; set; }
    //     public DateTime? CreatedOn { get; set; }
    //     public int ModifiedBy { get; set; }
    //     public DateTime ModifiedOn { get; set; }
    // }
    public enum Devices
    {
        Terminal,
        RFID,
        Biometric
    }
    public class EmployeeRosterBO
    {
        public int RosterID { get; set; }
        public int ShiftID { get; set; }
        public int EmpID { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public DateTime RosterDate { get; set; }
        public bool IsRotated { get; set; }
        public bool IsLastShift { get; set; }
        public bool IsEnable { get; set; }
        public int DayID { get; set; }
        public string RosterStatus { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string EmpNumber { get; set; }
        public string EmpName { get; set; }
        public string Manager { get; set; }
        public string Department { get; set; }
        public List<EmployeeAttendanceBO> objemp { get; set; }
    }
    public class EmployeeRosterOutputBO
    {
        public int RosterID { get; set; }
        public int ShiftID { get; set; }
        public int EmpID { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public DateTime RosterDate { get; set; }
        public bool IsRotated { get; set; }
        public bool IsLastShift { get; set; }
        public bool IsEnable { get; set; }
        public int DayID { get; set; }
        public string RosterStatus { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string EmpNumber { get; set; }
        public string EmpName { get; set; }
        public string Manager { get; set; }
        public string Department { get; set; }
        public int RowNum { get; set;}
        public int TotalCount{get; set;}
        public List<EmployeeAttendanceBO> objemp { get; set; }
    }
    public class StatusCountBO
    {
        public int PresentCount { get; set; }
        public int AbsentCount { get; set;}
        public int RegularCount { get; set;}
        public int IrregularCount { get; set;}
        public int LeaveCount { get; set; }
        public int PermissionCount { get; set; }
        public int TimeOffCount { get; set;}
    }
    public class EmployeeAttendanceBO
    {
        public int EmpAttendanceID { get; set; }
        public int EmpID { get; set; }
        public int RosterID { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public String AttendanceDate { get; set; }
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public string AttendanceStatus { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string Msg { get; set;}
    }
    public class EmployeeAttendanceOutputBO
    {
        public int EmpAttendanceID { get; set; }
        public int EmpID { get; set; }
        public int RosterID { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public String AttendanceDate { get; set; }
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public string AttendanceStatus { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string Msg { get; set;}
        public int RowNum { get; set;}
        public int TotalCount{get; set;}
    }
    public class EmpDetailsBO
    {
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int DayID { get; set; }
        public int ShiftID { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
    public class EmpStatusBO
    {
        public int EmpID { get; set; }
        public string AttendanceDate { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
    }
    public class EmpAttendanceInputBO
    {
        public int EmpID { get; set; }
        public int YearID { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public string Startdate { get; set; }
        public string EndDate { get; set; }
        public int DepartmentID { get; set; }
    }
    public class AttendanceInputBO
    {
        public int EmpID { get; set; }
        public int YearID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CurrentDate { get; set; }
    }
    public class AttendanceTerminalBO
    {
        public int EmpID { get; set; }
        public int LocationID { get; set; }
        public string EmpNumber { get; set; }
        public string EmployeeName { get; set; }
        public int TenantID { get; set; }
        public string base64Images { get; set; }
        public bool IsCheckIn { get; set; }
    }
    public class ManualInandOutBO
    {
        public int EmpAttendanceID { get; set; }
        public int EmpID { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public string TimeOutMsg { get; set; }
        public string AttendanceDate { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        public string EmpIDs { get; set; }
    }
    public class LeaveRequestBO
    {
        public int LeaveID { get; set; }
        public DateTime LeaveFrom { get; set; }
        public DateTime LeaveTo { get; set; }
        public string LeaveCategory { get; set; }
        public string LeaveType { get; set; }
        public decimal NoOfDays { get; set; }
        public string LeaveReason { get; set; }
        public string LeaveStatus { get; set; }
        public DateTime RequestDate { get; set; }
        public int EmpID { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public int CreatedBy { get; set; }
        public string EmpNumber { get; set; }
        public string EmpName { get; set; }
    }
    public class PermissionRequestBO
    {
        public int PermID { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime PermDate { get; set; }
        public string PermDescription { get; set; }
        public string PermStatus { get; set; }
        public TimeSpan PermDuration { get; set; }
        public TimeSpan PermStartTime { get; set; }
        public TimeSpan PermEndTime { get; set; }
        public string PermComments { get; set; }
        public int EmpID { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public int CreatedBy { get; set; }
        public string ManagerName { get; set; }
        public string EmpNumber { get; set; }
        public string EmpName { get; set; }
    }
    public class TardiesBO
    {
        public int RosterID { get; set; }
        public int ShiftID { get; set; }
        public int EmpID { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public DateTime RosterDate { get; set; }
        public bool IsRotated { get; set; }
        public int DayID { get; set; }
        public string RosterStatus { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string EmpNumber { get; set; }
        public string EmpName { get; set; }
    }
    public class AttendanceInandOutBO
    {
        public int EmpAttendanceID { get; set; }
        public int EmpID { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public string TimeOutMsg { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
    }
    public class ShifRosterGenertedBO
    {
        public int EmpID { get; set; }
        public int YearID { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
    }
    public enum DocRepoName
    {
        [DescriptionAttribute("Upload ProfileImage")]
        ProfileImage,
    }
    public class TerminalInputBO
    {
        public string TerminalNo { get; set; }
        public string DeviceIdentifier { get; set; }
    }
    public class ShiftConfigSettingBO
    {
        public string ServiceName { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public string IdentifierType { get; set; }
        public string IdentifierColumnName { get; set; }
    }
    public class DeviceMasterBO
    {
        public int DeviceID { get; set;}
        public string DeviceName { get; set;}
        public string DeviceType { get; set;}
        public string Location { get; set;}
        public string SKUNumber { get; set;}
        public string SerialNumber { get; set;}
        public string MachineType { get; set;}
        public string DeviceIdentifier{ get; set;}
        public string ActivationKey { get; set;}
        public string IsMode { get; set;}
        public string ExpiryTime { get; set;}
        public string ExpPeriod { get; set;}
        public DateTime? ExpiryDate { get; set;}
        public string DeviceStatus { get; set;}
        public int EntityID { get; set;}
        public int TenantID { get; set;}
        public DateTime? CreatedOn { get; set;}
        public int CreatedBy { get; set;}
        public DateTime? ModifiedOn { get; set;}
        public int ModifiedBy { get; set;}
        public DateTime? StartDate { get; set;}
    }
    public class EmpAttendanceConfigBO
    {
        public int AttendConfigID { get; set;}
        public int EmpID { get; set;}
        public int LocationID { get; set;}
        public string ClockingMode { get; set;}
        public string AttendCardNo { get; set;}
        public bool IsRoster { get; set;}
        public bool IsIDCardLinked { get; set;}
        public DateTime? CreatedOn { get; set;}
        public int CreatedBy { get; set;}
        public DateTime? ModifiedOn { get; set;}
        public int ModifiedBy { get; set;}
    }
    public class EmployeeListBO
    {
        public int EmpID { get; set; }
        public string EmpNumber { get; set; }
        public string EmpName { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
    }
    public class WorkingHours
    {
        public int DayID { get; set; }
        public string Day { get; set; }
        public int MasDayID { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string ShiftName { get; set; }
        public string DayType { get; set; }
        public int ShiftID { get; set; }
        public string ShiftStatus { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public string LocationName { get; set; }
        public int CreatedBy { get; set;}
        public int ModifiedBy { get; set;}
    }
    // public class ReturnWorkHours
    // {
    //     public int WorkDayID { get; set; }
    //     public string StartTime { get; set; }
    //     public string EndTime { get; set; }
    //     public int ShiftNo { get; set; }
    //     public string ShiftName { get; set; }
    //     public string LocationName { get; set; }
    //     public int TenantID { get; set; }
    //     public int LocationID { get; set; }
    // }
    public class ReturnShiftBO
    {
        public int ShiftID { get; set; }
        public string ShiftName { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
    }
    public class ReturnShiftSchdBO
    {
        public int ShiftID { get; set; }
        public string ShiftName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string LocationName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int EmpCount { get; set; }
    }
    public class HRRoasterBO
    {
        public int RosterID { get; set; }
        public int ShiftID { get; set; }
        public int EmpID { get; set; }
        public string RosterStatus { get; set; }
        public DateTime RosterDate { get; set; }
        public bool IsRotated { get; set; }
        public bool IsLastShift { get; set; }
        public bool IsEnable { get; set; }
        public int DayID { get; set; }
        public int WeekNo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int YearId { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public int CreatedBy { get; set; }
    }
    public class ReturnDetailBO
    {
        public List<ReturnEmpShiftNameBO> ShiftSchedule { get; set; }
    }
    public class ReturnEmpShiftNameBO
    {
        public int EmpID { get; set; }
        public string FullName { get; set; }
        public bool IsRoated { get; set; }
        public int LocationID { get; set; }
        public string LocationName { get; set; }
        public int ShiftID { get; set; }
        public string ShiftName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<HRRoasterBO> AttendanceRoaster { get; set; }
    }
    public class EmployeeListsBO
    {
        public int EmpID { get; set; }
        public string EmpNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string ReportName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public int FuncRoleID { get; set; }
        public int ManagerID { get; set; }
        public int DepartmentID { get; set; }
        public int BenefitGroupID { get; set; }
        public string Gender { get; set; }
        public DateTime? DOJ { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public bool IsAttendance { get; set; }
        public int HRInchargeID { get; set; }
        public string EmpStatus { get; set; }
        public int EmpLocID { get; set; }
        public int AppUserID { get; set; }
        public string LocationName { get; set; }
        public string LocationStatus { get; set; }
        public string LocationGmt { get; set; }
    }
}