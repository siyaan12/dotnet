using Microsoft.AspNetCore.Mvc;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.LoginBO;
using MyDodos.Domain.Wrapper;
using MyDodos.Domain.Holiday;
using MyDodos.Repository.LeaveManagement;
using MyDodos.Service.LeaveManagement;
using MyDodos.Service.Logger;
using MyDodos.ViewModel.LeaveManagement;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace MyDodos.API.Controllers.LeaveManagement
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveRepository _leaveRepository;
        private readonly ILeaveService _leaveService;
        private readonly ILoggerManager _logger;
        public LeaveController(ILeaveRepository leaveRepository, ILeaveService leaveService)
        {
            _leaveRepository = leaveRepository;
            _leaveService = leaveService;
        }
        [HttpGet("GetLocation/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<LoginLocationBO>>> GetLocation(int TenantID, int LocationID)
        {
            try
            {
                var result = _leaveRepository.GetLocation(TenantID, LocationID);
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
        [HttpPost("SaveLeave")]
        public ActionResult<Response<LeaveRequestModelMsg>> SaveLeave(LeaveRequestModel leave)
        {
            try
            {
                var result = _leaveService.AddNewLeaveRequest(leave);
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

                _logger.LogError($"Internal Server Error{ex.Message + "  "  + ex.InnerException}");
                return StatusCode(500, new Response<List<LoginLocationBO>>(ex.Message + "  "  + ex.InnerException, 500));
            }
        }
        // [HttpPost("SaveLeave")]
        // public ActionResult<Response<LeaveRequestModelMsg>> SaveLeave(LeaveRequestModel leave)
        // {
        //     // try
        //     // {
        //         var result = _leaveService.AddNewLeaveRequest(leave);
        //         if (result.StatusCode == 200)
        //         {
        //             return Ok(result);
        //         }
        //         else
        //         {
        //             return StatusCode(result.StatusCode, result);
        //         }
        //     // }
        //     // catch (Exception ex)
        //     // {

        //     //     _logger.LogError($"Internal Server Error{ex.Message + "  "  + ex.InnerException}");
        //     //     return StatusCode(500, new Response<List<LoginLocationBO>>(ex.Message + "  "  + ex.InnerException, 500));
        //     // }

        // }
        [HttpPost("GetEmpLeaveCategory")]
        public ActionResult<Response<List<MasLeaveCategoryBO>>> GetEmpLeaveCategory(LeaveRequestModel leave)
        {
            try
            {
                var result = _leaveService.GetCategoryList(leave);

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
                return StatusCode(500, new Response<List<MasLeaveCategoryBO>>(ex.Message, 500));
            }
        }
        [HttpGet("GetEmpLeaveList/{teantID}/{YearId}/{LocationID}/{EmpId}")]
        public ActionResult<Response<GetMyLeaveBO>> GetEmpLeaveList(int teantID, int YearId, int LocationID, int EmpId)
        {
            try
            {
                var result = _leaveService.GetEmpLeaveList(teantID, YearId, LocationID, EmpId);
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
        [HttpPost("SaveEmployeeHoliday")]
        public ActionResult<Response<int>> SaveEmployeeHoliday(EmployeeHolidayBO holiday)
        {
            try
            {
                var result = _leaveService.SaveEmployeeHoliday(holiday);
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
        [HttpPost("SaveLeaveCategory")]
        public ActionResult<Response<int>> SaveLeaveCategory(HRVwBeneftisLeave_BO category)
        {
            try
            {
                var result = _leaveService.SaveLeaveCategoryMaster(category);
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
        [HttpGet("GetEmpLeaveCategory/{TenantID}/{LocationID}")]
        public ActionResult<Response<HRVwBeneftisLeave_BO>> GetEmpLeaveCategory(int TenantID, int LocationID)
        {
            try
            {
                var result = _leaveService.GetLeaveCategoryMaster(TenantID, LocationID);
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
        [HttpPost("GetMyLeaveList")]
        public ActionResult<Response<GetMyLeaveList>> GetMyLeaveList(GetMyLeaveList objresults)
        {
            try
            {
                var result = _leaveService.GetMyLeaveList(objresults);
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
                return StatusCode(500, new Response<GetMyLeaveList>(ex.Message, 500));
            }
        }
        [HttpPost("MSaveLeave")]
        public ActionResult<Response<LeaveRequestModelMsg>> MSaveLeave(LeaveRequestModel leave)
        {
            try
            {
                var result = _leaveService.MAddNewLeaveRequest(leave);
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

                _logger.LogError($"Internal Server Error{ex.Message + "  "  + ex.InnerException}");
                return StatusCode(500, new Response<List<LoginLocationBO>>(ex.Message + "  "  + ex.InnerException, 500));
            }
        }
        [HttpPost("MGetMyLeaveList")]
        public ActionResult<Response<List<HRGetMyLeaveList>>> MGetMyLeaveList(GetMyLeaveListInputs objresults)
        {
            try
            {
                var result = _leaveService.MGetMyLeaveList(objresults);
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
                return StatusCode(500, new Response<List<HRGetMyLeaveList>>(ex.Message, 500));
            }
        }
        [HttpPost("MGetEmpLeaveCategory")]
        public ActionResult<Response<List<MobileLeaveCategoryBO>>> MGetEmpLeaveCategory(LeaveRequestModel leave)
        {
            try
            {
                var result = _leaveService.MGetCategoryList(leave);

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
                return StatusCode(500, new Response<List<MobileLeaveCategoryBO>>(ex.Message, 500));
            }
        }
    }
}