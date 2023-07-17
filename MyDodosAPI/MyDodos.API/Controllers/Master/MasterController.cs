using Microsoft.AspNetCore.Mvc;
using MyDodos.Domain.Master;
using MyDodos.Service.Master;
using MyDodos.Domain.Wrapper;
using MyDodos.Service.Logger;
using MyDodos.Repository.Master;
using System;
using System.Collections.Generic;
using MyDodos.Domain.LeaveBO;
using Microsoft.AspNetCore.Authorization;

namespace MyDodos.API.Controllers.Master
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly IMasterService _masterService;
        private readonly IMasterRepository _masterRepository;
        private readonly ILoggerManager _logger;
        public MasterController(IMasterService masterService, IMasterRepository masterRepository)
        {
            _masterService = masterService;
            _masterRepository = masterRepository;
        }
        [HttpGet("GetTenantDetails")]
        public ActionResult<Response<List<TenantProfileBO>>> GetTenantDetails(MasterInputBO master)
        {
            try
            {
                var result = _masterService.GetTenantDetails(master);
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
                return StatusCode(500, new Response<List<TenantProfileBO>>(ex.Message, 500));
            }
        }
        [HttpGet("GetLocationDetails")]
        public ActionResult<Response<List<LocationBO>>> GetLocationDetails(MasterInputBO master)
        {
            try
            {
                var result = _masterService.GetLocationDetails(master);
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
                return StatusCode(500, new Response<List<LocationBO>>(ex.Message, 500));
            }
        }
        [HttpGet("GetYearDetails")]
        public ActionResult<Response<List<YearBO>>> GetYearDetails(MasterInputBO master)
        {
            try
            {
                var result = _masterService.GetYearDetails(master);
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
                return StatusCode(500, new Response<List<YearBO>>(ex.Message, 500));
            }
        }
        [HttpGet("GetShiftConfigSettings/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<ShiftConfigSettingBO>>> GetShiftConfigSettings(int TenantID, int LocationID)
        {
            try
            {
                var result = _masterRepository.GetShiftConfigSettings(TenantID,LocationID);
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
        [HttpGet("GetHRLeaveAllocReference/{LeaveGroupID}/{LocationID}/{TenantID}")]
        public ActionResult<Response<List<HRLeaveAllocReferenceBO>>> GetLeaveAllocReference(int LeaveGroupID,int LocationID, int TenantID)
        {
            try
            {
                var result = _masterService.GetLeaveAllocReference(LeaveGroupID, LocationID, TenantID);
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
        [HttpDelete("DeleteHRLeaveAllocReference/{LeaveAllocationID}")]
        public ActionResult<Response<LeaveRequestModelMsg>> DeleteLeaveAllocReference(int LeaveAllocationID)
        {
            try
            {
                var result = _masterService.DeleteLeaveAllocReference(LeaveAllocationID);
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
         [HttpGet("GetHRMasLeaveCategory/{CategoryID}/{LocationID}/{TenantID}")]
        public ActionResult<Response<List<HRMasLeaveCategoryBO>>> GetMasLeaveCategory(int CategoryID,int LocationID, int TenantID)
        {
            try
            {
                var result = _masterService.GetMasLeaveCategory(CategoryID, LocationID, TenantID);
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
        [HttpDelete("DeleteHRMasLeaveCategory/{CategoryID}")]
        public ActionResult<Response<LeaveRequestModelMsg>> DeleteMasLeaveCategory(int CategoryID)
        {
            try
            {
                var result = _masterService.DeleteMasLeaveCategory(CategoryID);
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
        [HttpPost("SaveOptionalSet")]
        public ActionResult<Response<LeaveRequestModelMsg>> SaveOptionalSet(OptionalSetBO optional)
        {
            try
            {
                var result = _masterService.SaveOptionalSet(optional);
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
        [HttpGet("GetOptionalSet/{TenantID}/{FieldId}/{OptionalSetValue}")]
        public ActionResult<Response<List<OptionalSetBO>>> GetOptionalSet(int TenantID, int FieldId,int OptionalSetValue)
        {
            try
            {
                var result = _masterService.GetOptionalSet(TenantID, FieldId,OptionalSetValue);
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
        [HttpDelete("DeleteOptionalSet/{FormId}")]
        public ActionResult<Response<LeaveRequestModelMsg>> DeleteOptionalSet(int FormId)
        {
            try
            {
                var result = _masterService.DeleteOptionalSet(FormId);
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