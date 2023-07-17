using Microsoft.AspNetCore.Mvc;
using MyDodos.Domain.Attendance;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.Attendance;
using MyDodos.Service.Attendance;
using MyDodos.Service.Logger;
using System;
using System.Collections.Generic;
using Instrument.Utility;
using MyDodos.ViewModel.Attendance;
using Microsoft.AspNetCore.Authorization;
using MyDodos.ViewModel.Common;

namespace MyDodos.API.Controllers.Attendance
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IAttendanceService _attendanceService;
        private readonly ILoggerManager _logger;
        public AttendanceController(IAttendanceRepository attendanceRepository, IAttendanceService attendanceService)
        {
            _attendanceRepository = attendanceRepository;
            _attendanceService = attendanceService;
        }
        [HttpPost("SaveEmpAttendanceManual")]
        public ActionResult<Response<int>> SaveEmpAttendanceManual(List<EmployeeRosterBO> objempros)
        {
            Response<int> response = new Response<int>();
            try
            {
                var result = _attendanceService.SaveEmpAttendanceManual(objempros);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<string>(ex.Message));
            }
        }
        [HttpPost("GetEmpAttendanceManual")]
        public ActionResult<Response<List<EmployeeRosterBO>>> GetEmpAttendanceManual(EmpAttendanceInputBO objempros)
        {
            try
            {
                var result = _attendanceService.GetEmpAttendanceManual(objempros);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<string>(ex.Message));
            }
        }
        [HttpPost("GetEmployeeAttendanceManualSearch")]
        public ActionResult<Response<EmployeeRosterSearchBO>> GetEmployeeAttendanceManualSearch(EmployeeRosterSearchBO objplan)
        {
            try
            {
                var result = _attendanceService.GetEmployeeAttendanceManualSearch(objplan);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<string>(ex.Message));
            }
        }
        [HttpPost("GetEmpAttendance")]
        public ActionResult<Response<List<EmployeeRosterBO>>> GetEmpAttendance(AttendanceInputBO _att)
        {
            try
            {
                var result = _attendanceService.GetEmpAttendance(_att);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<string>(ex.Message, 500));
            }
        }
        [AllowAnonymous]
        [HttpPost("saveTerminal")]
        public ActionResult<Response<int>> SaveTerminal(DeviceMaster objdevices)
        {
            try
            {
                var result = _attendanceService.SaveTerminal(objdevices);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<string>(ex.Message, 500));
            }
        }
        [AllowAnonymous]
        [HttpPost("getTerminal")]
        public ActionResult<Response<List<DeviceMaster>>> GetTerminal(InputDeviceData objdevices)
        {
            try
            {
                var result = _attendanceService.GetTerminal(objdevices);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<string>(ex.Message, 500));
            }
        }
        [AllowAnonymous]
        [HttpPost("updateTerminal")]
        public ActionResult<Response<int>> UpdateTerminal(InputDeviceData objdevices)
        {
            try
            {
                var result = _attendanceService.UpdateDeviceMaster(objdevices);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [AllowAnonymous]
        [HttpPost("getTerminalUniqueID")]
        public ActionResult<Response<List<DeviceMaster>>> GetTerminalUniqueID(DeviceUnique deviceUnique)
        {
            try
            {
                var result = _attendanceService.GetTerminalUniqueID(deviceUnique);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [AllowAnonymous]
        [HttpPost("checkTerminalUniqueID")]
        public ActionResult<Response<List<DeviceMaster>>> CheckTerminalUniqueID(DeviceUnique deviceUnique)
        {
            try
            {
                var result = _attendanceService.CheckTerminalUniqueID(deviceUnique);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [AllowAnonymous]
        [HttpPost("checkTerminal")]
        public ActionResult<Response<List<DeviceMaster>>> CheckTerminal(DeviceUnique deviceUnique)
        {
            try
            {
                var result = _attendanceService.CheckTerminal(deviceUnique);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [AllowAnonymous]
        [HttpPost("genActivationKey")]
        public ActionResult<Response<GenActivationKey>> GenActivationKey(GenActivationKey deviceUnique)
        {
            try
            {
                var result = _attendanceService.GenActivationKey(deviceUnique);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<GenActivationKey>(ex.Message, 500));
            }
        }
        [AllowAnonymous]
        [HttpDelete("deleteDeviceLink/{DeviceID}/{EntityID}/{TenantID}")]
        public ActionResult<Response<int>> DeleteDeviceLink(int DeviceID, int EntityID, int TenantID)
        {
            try
            {
                var result = _attendanceService.DeleteDeviceLink(DeviceID, EntityID, TenantID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [AllowAnonymous]
        [HttpDelete("deleteDevice/{DeviceID}/{LocationID}/{TenantID}")]
        public ActionResult<Response<int>> DeleteDevice(int DeviceID, int LocationID, int TenantID)
        {
            try
            {
                var result = _attendanceService.DeleteDevice(DeviceID, LocationID, TenantID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [AllowAnonymous]
        [HttpPost("SaveDeviceTypeMaster")]
        public ActionResult<Response<int>> SaveDeviceTypeMaster(List<DeviceTypeMaster> objdevice)
        {
            Response<int> response = new Response<int>();
            try
            {
                var result = _attendanceService.SaveDeviceTypeMaster(objdevice);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<string>(ex.Message));
            }
        }
        [AllowAnonymous]
        [HttpPost("GetDeviceTypeMaster")]
        public ActionResult<Response<List<DeviceTypeMaster>>> GetDeviceTypeMaster(DeviceTypeMaster objdevice)
        {
            try
            {
                var result = _attendanceService.GetDeviceTypeMaster(objdevice);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<string>(ex.Message));
            }
        }
        [AllowAnonymous]
        [HttpPost("GetEmployeeData")]
        public ActionResult<Response<List<AttendanceTerminalBO>>> GetEmpAttendance(TerminalInputBO objinput)
        {
            try
            {
                var result = _attendanceService.GetTerminalData(objinput);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<string>(ex.Message, 500));
            }
        }
        [HttpPost("GetAttendanceTrackingData")]
        public ActionResult<Response<AttendanceTrackingBO>> GetAttendanceTracking(EmployeeRosterSearchBO objinp)
        {
            try
            {
                var result = _attendanceService.GetAttendanceTracking(objinp);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<string>(ex.Message, 500));
            }
        }
        [AllowAnonymous]
        [HttpPost("AttendanceInandOut")]
        public ActionResult<Response<EmployeeAttendanceBO>> AttendanceInandOut(AttendanceInandOutBO _shiftin)
        {
            try
            {
                var result = _attendanceService.AttendanceInandOut(_shiftin);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<string>(ex.Message, 500));
            }
        }
        [HttpPost("AddAttendanceRosterGen")]
        public ActionResult<Response<string>> AttendanceRosterGenerted(ShifRosterGenertedBO _shift)
        {
            try
            {
                var result = _attendanceService.AttendanceRosterGenerted(_shift);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<string>(ex.Message, 500));
            }
        }
        [HttpPost("SaveAttendacePIN")]
        public ActionResult<Response<int>> SaveAttendacePIN(EmpAttendanceConfigBO objconfig)
        {
            Response<int> response = new Response<int>();
            try
            {
                var result = _attendanceService.SaveAttendacePIN(objconfig);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<string>(ex.Message));
            }
        }
        [HttpGet("GetEmployeeList/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<EmployeeListBO>>> GetEmployeeList(int TenantID,int LocationID)
        {
            try
            {
                var result = _attendanceService.GetEmployeeList(TenantID,LocationID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<string>(ex.Message, 500));
            }
        }
        [AllowAnonymous]
        [HttpPost("AttendanceUserLogData")]
        public ActionResult<Response<int>> AttendanceUserLogData(ICollection<MachineInfo> attend)
        {
            Response<int> response = new Response<int>();
            try
            {
                var result = _attendanceService.SaveAttendanceUserLogData(attend);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<string>(ex.Message));
            }
        }
        // [HttpPost("AttendanceRoster")]
        // public ActionResult<Response<int>> AttendanceRoster(List<EmployeeRosterBO> roster)
        // {
        //     try
        //     {
        //         var result = _attendanceService.AttendanceRoster(roster);
        //         if (result.StatusCode == 200)
        //         {
        //             return Ok(result);
        //         }
        //         else
        //         {
        //             return StatusCode(result.StatusCode, result);
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError($"Internal Server Error{ex.Message}");
        //         return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
        //     }
        // }
        [HttpPost("SaveAttendanceWorkingHours")]
        public ActionResult<Response<SaveOut>> AddAttendanceProfile(List<WorkingHours> hours)
        {
            try
            {
                var result = _attendanceService.AddAttendanceProfile(hours);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<SaveOut>(ex.Message, 500));
            }
        }
        [HttpGet("GetShiftList/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<WorkingHours>>> GetShiftList(int TenantID,int LocationID)
        {
            try
            {
                var result = _attendanceService.GetShiftList(TenantID,LocationID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<List<WorkingHours>>(ex.Message, 500));
            }
        }
        [HttpGet("GetShiftName/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<ReturnShiftBO>>> GetShiftName(int TenantID,int LocationID)
        {
            try
            {
                var result = _attendanceService.GetShiftName(TenantID,LocationID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<List<ReturnShiftBO>>(ex.Message, 500));
            }
        }
        [HttpGet("GetAttendanceSchedule/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<ReturnShiftSchdBO>>> GetAttendanceSchedule(int TenantID,int LocationID)
        {
            try
            {
                var result = _attendanceService.GetAttendanceSchedule(TenantID,LocationID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<List<ReturnShiftSchdBO>>(ex.Message, 500));
            }
        }
        [HttpPost("AttendanceRoster")]
        public ActionResult<Response<SaveOut>> AttendanceRoster(List<HRRoasterBO> roster)
        {
            try
            {
                var result = _attendanceService.AttendanceRoster(roster);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<SaveOut>(ex.Message, 500));
            }
        }
        [HttpPost("GetWorkingHours")]
        public ActionResult<Response<List<WorkingHours>>> GetWorkingHours(WorkingHours hours)
        {
            try
            {
                var result = _attendanceService.GetWorkingHours(hours.TenantID,hours.LocationID,hours.ShiftName);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<List<WorkingHours>>(ex.Message, 500));
            }
        }
        [HttpGet("GetShiftRoasterEmp/{TenantID}/{LocationID}/{ShiftID}")]
        public ActionResult<Response<ReturnDetailBO>> GetShiftRoasterEmp(int TenantID,int LocationID,int ShiftID)
        {
            try
            {
                var result = _attendanceService.GetShiftRoasterEmp(TenantID,LocationID,ShiftID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<ReturnDetailBO>(ex.Message, 500));
            }
        }
        [HttpGet("GetEmployeeList/{TenantID}/{LocationID}/{EmpID}")]
        public ActionResult<Response<List<EmployeeListsBO>>> GetEmployeeList(int TenantID, int LocationID,int EmpID)
        {
            try
            {
                var result = _attendanceService.GetEmployeeList(TenantID,LocationID,EmpID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }  
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<List<EmployeeListsBO>>(ex.Message, 500));
            }
        }
    }
}