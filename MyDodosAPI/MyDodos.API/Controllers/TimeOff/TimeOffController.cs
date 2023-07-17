using Microsoft.AspNetCore.Mvc;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.TimeOff;
using MyDodos.Domain.LoginBO;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.TimeOff;
using MyDodos.Service.TimeOff;
using MyDodos.Service.Logger;
using MyDodos.ViewModel.TimeOff;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace MyDodos.API.Controllers.TimeOff
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TimeOffController : ControllerBase
    {
        private readonly ITimeOffRepository _timeoffRepository;
        private readonly ITimeOffService _timeoffService;
        private readonly ILoggerManager _logger;
        public TimeOffController(ITimeOffRepository timeoffRepository, ITimeOffService timeoffService)
        {
            _timeoffRepository = timeoffRepository;
            _timeoffService = timeoffService;
        }
        [HttpPost("SaveTimeoffRequest")]
        public ActionResult<Response<LeaveRequestModelMsg>> SaveTimeoffRequest(TimeoffRequestModel leave)
        {
            try
            {
                var result = _timeoffService.AddNewTimeOffRequest(leave);
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
                return StatusCode(500, new Response<List<LoginLocationBO>>(ex.Message, 500));
            }
        }
        [HttpGet("GetTimeoffRequestList/{TenantID}/{YearId}/{LocationID}/{EmpId}")]
        public ActionResult<Response<GetTimeoffRequestBO>> GetTimeoffRequestList(int TenantID, int YearId, int LocationID, int EmpId)
        {
            try
            {
                var result = _timeoffService.GetTimeoffRequestList(TenantID, YearId, LocationID, EmpId);
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
                return StatusCode(500, new Response<List<LoginLocationBO>>(ex.Message, 500));
            }
        }
        [HttpPost("GetTimeOffLeaveCategory")]
        public ActionResult<Response<List<HRVwBeneftisLeave_BO>>> GetTimeOffLeaveCategory(LeaveRequestModel leave)
        {
            try
            {
                var result = _timeoffService.GetTimeOffLeaveCategory(leave);
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
                return StatusCode(500, new Response<List<HRVwBeneftisLeave_BO>>(ex.Message, 500));
            }
        }
        [HttpPost("GetMyTimeoffList")]
        public ActionResult<Response<GetMyTimeoffList>> GetMyTimeoffList(GetMyTimeoffList objresults)
        {
            try
            {
                var result = _timeoffService.GetMyTimeoffList(objresults);
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
                return StatusCode(500, new Response<GetMyTimeoffList>(ex.Message, 500));
            }
        }
    }
}