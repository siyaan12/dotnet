using Microsoft.AspNetCore.Mvc;
using MyDodos.Domain.ProjectManagement;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.ProjectManagement;
using MyDodos.Service.ProjectManagement;
using MyDodos.Service.Logger;
using MyDodos.ViewModel.ProjectManagement;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace MyDodos.API.Controllers.ProjectManagement
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSheetController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectService _projectService;
        private readonly ITimeSheetRepository _timeSheetRepository;
        private readonly ITimeSheetService _timeSheetService;
        private readonly ILoggerManager _logger;
        public TimeSheetController(IProjectRepository projectRepository, IProjectService projectService,ITimeSheetService timeSheetService)
        {
            _projectRepository = projectRepository;
            _projectService = projectService;
            _timeSheetService = timeSheetService;
        }
        [HttpPost("SaveTimeSheet")]
        public ActionResult<Response<LeaveRequestModelMsg>> SaveTimeSheet(PPTimeSheetBO timeSheet)
        {
            try
            {
                var result = _timeSheetService.SaveTimeSheet(timeSheet);
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
                return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpPost("GetTimeSheetTasks")]
        public ActionResult<Response<TimeSheetTaskBO>> GetTimeSheetTasks(TimesheetInputBO task)
        {
            try
            {
                var result = _timeSheetService.GetTimeSheetTasks(task);
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
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("SaveWeekTSNonBillable")]
        public ActionResult<Response<LeaveRequestModelMsg>> SaveWeekTSNonBillable(BillNonBillable timeSheet)
        {
            try
            {
                var result = _timeSheetService.SaveWeekTSNonBillable(timeSheet);
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
                return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpPost("GetTimeSheetList")]
        public ActionResult<Response<List<PPTimeSheetBO>>> GetTimeSheetList(TimesheetInputBO list)
        {
            try
            {
                var result = _timeSheetService.GetTimeSheetList(list);
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
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("GetTSBillableNonBillableList")]
        public ActionResult<Response<PPTimeSheetBO>> TSBillableNonBillableList(TimesheetInputBO list)
        {
            try
            {
                var result = _timeSheetService.TSBillableNonBillableList(list);
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
                return StatusCode(500, new Response<PPTimeSheetBO>(ex.Message, 500));
            }
        }
        [HttpPost("GetTimeSheetExceptionData")]
        public ActionResult<Response<GetTimeSheetList>> GetTimeSheetData(GetTimeSheetList timesheet)
        {
            try
            {
                var result = _timeSheetService.GetTimeSheetData(timesheet);
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
                return StatusCode(500, new Response<GetTimeSheetList>(ex.Message, 500));
            }
        }
        [HttpGet("GetWeekcount")]
        public ActionResult<Response<int>> GetWeekcount()
        {
            try
            {
                var result = _timeSheetService.GetWeekcount();
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
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("GetWeekDateRange")]
        public ActionResult<Response<List<WeekDateRange>>> GetWeekDateRange(WeekDateRange range)
        {
            try
            {
                var result = _timeSheetService.GetWeekDateRange(range.TenantID,range.LocationID,range.AttendanceDate);
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
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("SaveTimeSheetTaskApply")]
        public ActionResult<Response<LeaveRequestModelMsg>> SaveTimeSheetTaskApply(PPWeekTimeSheetBO timeSheet)
        {
            try
            {
                var result = _timeSheetService.SaveTimeSheetTaskApply(timeSheet);
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
                return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpPost("SaveTimeSheetFlagged")]
        public ActionResult<Response<TimeSheetFlaggedBO>> SaveTimeSheetFlagged(TimesheetInputBO timeSheet)
        {
            try
            {
                var result = _timeSheetService.SaveTimeSheetFlagged(timeSheet);
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
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("GetTimeSheetEmployeeReport")]
        public ActionResult<Response<TimeSheetEmpReportList>> GetTimeSheetEmpReportData(TimeSheetEmpReportList report)
        {
            try
            {
                var result = _timeSheetService.GetTimeSheetEmpReportData(report);
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
                return StatusCode(500, new Response<TimeSheetEmpReportList>(ex.Message, 500));
            }
        }
         [HttpPost("GetTimeSheetReportResult")]
        public ActionResult<Response<TSExcReportResultList>> GetTSExcReportResult(TSExcReportResultList report)
        {
            try
            {
                var result = _timeSheetService.GetTSExcReportResult(report);
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
                return StatusCode(500, new Response<TSExcReportResultList>(ex.Message, 500));
            }
        }
        [HttpGet("GetWeekDropdown")]
        public ActionResult<List<TimeSheetWeek>> GetWeekDropdown()
        {
            try
            {
                var result = _timeSheetService.GetWeekDropdown();
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
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [AllowAnonymous]
        [HttpGet("GetConsoleTimesheet/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<TimeSheetException>>> GetConsoleTimesheet(int TenantID,int LocationID)
        {
            try
            {
                var result = _timeSheetService.GetConsoleTimesheet(TenantID, LocationID);
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
                return StatusCode(500, new Response<List<TimeSheetException>>(ex.Message, 500));
            }
        }
        [HttpGet("GetTimeSheetSummary/{TenantID}/{LocationID}/{EmpID}/{ManagerID}")]
        public ActionResult<Response<List<TimeSheetSummaryBO>>> GetTimeSheetSummary(int TenantID,int LocationID,int EmpID,int ManagerID)
        {
            try
            {
                var result = _timeSheetService.GetTimeSheetSummary(TenantID, LocationID, EmpID,ManagerID);
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
                return StatusCode(500, new Response<List<TimeSheetSummaryBO>>(ex.Message, 500));
            }
        }
        [HttpPost("UpdateTimeSheetPaidStatus")]
        public ActionResult<Response<LeaveRequestModelMsg>> UpdateTimeSheetPaidStatus(List<UpdateTimeSheetPaidStatusBO> project)
        {
            try
            {
                var result = _timeSheetService.UpdateTimeSheetPaidStatus(project);
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
                return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
            }
        }
    }
}