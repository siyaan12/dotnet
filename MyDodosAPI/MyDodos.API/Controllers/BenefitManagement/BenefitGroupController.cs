using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDodos.Domain.BenefitManagement;
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
    public class BenefitGroupController : ControllerBase
    {
        private readonly IBenefitGroupRepository _benefitGroupRepository;
        private readonly IBenefitGroupService _benefitGroupService;
        private readonly ILoggerManager _logger;
        public BenefitGroupController(IBenefitGroupRepository benefitGroupRepository, IBenefitGroupService benefitGroupService)
        {
            _benefitGroupRepository = benefitGroupRepository;
            _benefitGroupService = benefitGroupService;
        }
        [HttpPost("SaveBenefitGroup")]
        public ActionResult<Response<int>> SaveBenefitGroup(BenefitGroupBO objgroup)
        {
            try
            {
                var result = _benefitGroupService.SaveBenefitGroup(objgroup);
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
        [HttpGet("GetBenefitGroup/{BenefitGroupID}/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<BenefitGroupBO>>> GetBenefitGroup(int BenefitGroupID, int TenantID, int LocationID)
        {
            try
            {
                var result = _benefitGroupService.GetBenefitGroup(BenefitGroupID, TenantID, LocationID);
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
        [HttpDelete("DeleteBenefitGroup/{BenefitGroupID}/{TenantID}/{LocationID}")]
        public ActionResult<Response<int>> DeleteBenefitGroup(int BenefitGroupID, int TenantID, int LocationID)
        {
            try
            {
                var result = _benefitGroupService.DeleteBenefitGroup(BenefitGroupID, TenantID, LocationID);
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