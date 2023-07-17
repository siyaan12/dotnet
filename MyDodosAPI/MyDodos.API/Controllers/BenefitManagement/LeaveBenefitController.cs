using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDodos.Domain.BenefitManagement;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.BenefitManagement;
using MyDodos.Service.BenefitManagement;
using MyDodos.Service.Logger;
using System;
using System.Collections.Generic;

namespace MyDodos.API.Controllers.BenefitManagement
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveBenefitController : ControllerBase
    {
        private readonly ILeaveBenefitRepository _leaveBenefitRepository;
        private readonly ILeaveBenefitService _leaveBenefitService;
        private readonly ILoggerManager _logger;
        public LeaveBenefitController(ILeaveBenefitRepository leaveBenefitRepository, ILeaveBenefitService leaveBenefitService)
        {
            _leaveBenefitRepository = leaveBenefitRepository;
            _leaveBenefitService = leaveBenefitService;
        }
        [HttpGet("GetBenefitLeaveCategory/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<HRVwBeneftisLeave_BO>>> GetBenefitLeaveCategory(int TenantID, int LocationID)
        {
            try
            {
                var result = _leaveBenefitService.GetBenefitLeaveCategory(TenantID, LocationID);
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
        [HttpPost("SaveLeaveBenefits")]
        public ActionResult<Response<int>> SaveLeaveBenefits(MasLeaveGroupBO objgroup)
        {
            try
            {
                var result = _leaveBenefitService.SaveLeaveBenefits(objgroup);
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
        [HttpGet("GetLeaveBenefits/{LeaveGroupID}/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<HRBenefitPlansBO>>> GetLeaveBenefits(int LeaveGroupID, int TenantID, int LocationID)
        {
            try
            {
                var result = _leaveBenefitService.GetLeaveBenefits(LeaveGroupID, TenantID, LocationID);
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
        [HttpDelete("DeleteLeaveBenefits/{LeaveGroupID}/{TenantID}/{LocationID}")]
        public ActionResult<Response<int>> DeleteLeaveBenefits(int LeaveGroupID, int TenantID, int LocationID)
        {
            try
            {
                var result = _leaveBenefitService.DeleteLeaveBenefits(LeaveGroupID, TenantID, LocationID);
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
        [HttpGet("GetLeaveBenefitsByGroupType/{GroupTypeID}/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<HRBenefitPlansBO>>> GetLeaveBenefitsByGroupType(int GroupTypeID, int TenantID, int LocationID)
        {
            try
            {
                var result = _leaveBenefitService.GetLeaveBenefitsByGroupType(GroupTypeID, TenantID, LocationID);
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
    }
}