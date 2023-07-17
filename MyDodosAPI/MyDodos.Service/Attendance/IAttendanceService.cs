using System.Collections.Generic;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.Attendance;
using MyDodos.Domain.Wrapper;
using Instrument.Utility;
using MyDodos.ViewModel.Attendance;
using MyDodos.ViewModel.Common;

namespace MyDodos.Service.Attendance
{
    public interface IAttendanceService
    {
        Response<int> SaveAttendanceUserLogData(ICollection<MachineInfo> attend);
        Response<int> SaveTerminal(DeviceMaster objdevices);
        Response<List<DeviceMaster>> GetTerminal(InputDeviceData objdevices);
        Response<int> UpdateDeviceMaster(InputDeviceData objdevices);
        Response<List<DeviceMaster>> GetTerminalUniqueID(DeviceUnique deviceUnique);
        Response<List<DeviceMaster>> CheckTerminalUniqueID(DeviceUnique deviceUnique);
        Response<List<DeviceMaster>> CheckTerminal(DeviceUnique deviceUnique);
        Response<GenActivationKey> GenActivationKey(GenActivationKey objKey);
        Response<int> DeleteDeviceLink(int DeviceID, int EntityID, int TenantID);
        Response<int> DeleteDevice(int DeviceID, int LocationID, int TenantID);
        Response<int> SaveDeviceTypeMaster(List<DeviceTypeMaster> objdevice);
        Response<List<DeviceTypeMaster>> GetDeviceTypeMaster(DeviceTypeMaster objdevice);
        Response<int> SaveEmpAttendanceManual(List<EmployeeRosterBO> objempros);
        Response<List<EmployeeRosterBO>> GetEmpAttendanceManual(EmpAttendanceInputBO objempros);
        Response<List<EmployeeRosterBO>>  GetEmpAttendance(AttendanceInputBO _att);
        Response<List<AttendanceTerminalBO>> GetTerminalData(TerminalInputBO objinput);
        Response<AttendanceTrackingBO> GetAttendanceTracking(EmployeeRosterSearchBO objinp);
        Response<string> AttendanceRosterGenerted(ShifRosterGenertedBO _shift);
        Response<EmployeeAttendanceBO> AttendanceInandOut(AttendanceInandOutBO _shiftin);
        Response<int> SaveAttendacePIN(EmpAttendanceConfigBO objconfig);
        Response<EmployeeRosterSearchBO> GetEmployeeAttendanceManualSearch(EmployeeRosterSearchBO objplan);
        Response<List<EmployeeListBO>> GetEmployeeList(int TenantID,int LocationID);
        // Shift Scheduling
        Response<SaveOut> AddAttendanceProfile(List<WorkingHours> hours);
        Response<List<WorkingHours>> GetShiftList(int TenantID,int LocationID);
        Response<List<ReturnShiftBO>> GetShiftName(int TenantID,int LocationID);
        Response<List<ReturnShiftSchdBO>> GetAttendanceSchedule(int TenantID,int LocationID);
        Response<SaveOut> AttendanceRoster(List<HRRoasterBO> roster);
        Response<List<WorkingHours>> GetWorkingHours(int TenantID,int LocationID,string ShiftName);
        Response<ReturnDetailBO> GetShiftRoasterEmp(int TenantID,int LocationID,int ShiftID);
        Response<List<EmployeeListsBO>> GetEmployeeList(int TenantID, int LocationID,int EmpID);
    }
}