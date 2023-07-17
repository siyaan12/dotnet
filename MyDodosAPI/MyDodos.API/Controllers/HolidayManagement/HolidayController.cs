using Microsoft.AspNetCore.Mvc;
using MyDodos.Domain.AuthBO;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.LoginBO;
using MyDodos.Domain.Wrapper;
using MyDodos.Domain.Holiday;
using MyDodos.Repository.Holiday;
using MyDodos.Service.Auth;
using MyDodos.Service.HolidayManagement;
using MyDodos.Service.Logger;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace MyDodos.API.Controllers.HolidayManagement
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        private readonly IHolidayRepository _holidayRepository;
        private readonly IHolidayService _holidayService;
        private readonly ILoggerManager _logger;
        public HolidayController(IHolidayRepository holidayRepository, IHolidayService holidayService)
        {
            _holidayRepository = holidayRepository;
            _holidayService = holidayService;
        }
        [HttpGet("GetYearHolidayDetails/{TenantID}/{LocationID}/{YearID}")]
        public ActionResult<Response<List<MasYearBO>>> GetYearHolidayDetails(int TenantID, int LocationID, int YearID)
        {
            try
            {
                var result = _holidayService.GetYearHolidayDetails(TenantID, LocationID, YearID);
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
                return StatusCode(500, new Response<List<MasYearBO>>(ex.Message, 500));
            }
        }
        [HttpPost("SaveMasYear")]
        public ActionResult<Response<LeaveRequestModelMsg>> SaveMasYear(MasYear year)
        {
            try
            {
                var result = _holidayService.SaveMasYear(year);
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
        [HttpGet("GetEmployeeHoliday/{EmpID}/{YearID}")]
        public ActionResult<Response<List<HolidayBO>>> GetEmployeeHoliday(int EmpID, int YearID)
        {
            try
            {
                var result = _holidayService.GetEmployeeHoliday(EmpID, YearID);
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
                return StatusCode(500, new Response<List<MasYearBO>>(ex.Message, 500));
            }
        }
    }
}