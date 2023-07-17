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
    public class DisabilityBenefitController : ControllerBase
    {
        private readonly IDisabilityBenefitRepository _disabilityBenefitRepository;
        private readonly IDisabilityBenefitService _disabilityBenefitService;
        private readonly ILoggerManager _logger;
        public DisabilityBenefitController(IDisabilityBenefitRepository disabilityBenefitRepository, IDisabilityBenefitService disabilityBenefitService)
        {
            _disabilityBenefitRepository = disabilityBenefitRepository;
            _disabilityBenefitService = disabilityBenefitService;
        }
        [HttpPost("SaveMasDisabilityBenefit")]
        public ActionResult<Response<int>> SaveMasDisabilityBenefit(MasDisabilityBenefitBO objben)
        {
            try
            {
                var result = _disabilityBenefitService.SaveMasDisabilityBenefit(objben);
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
        [HttpGet("GetMasDisabilityBenefit/{DisabilityBenefitID}/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<MasDisabilityBenefitBO>>> GetMasDisabilityBenefit(int DisabilityBenefitID, int TenantID, int LocationID)
        {
            try
            {
                var result = _disabilityBenefitService.GetMasDisabilityBenefit(DisabilityBenefitID, TenantID, LocationID);
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
        [HttpDelete("DeleteMasDisabilityBenefit/{DisabilityBenefitID}/{TenantID}/{LocationID}")]
        public ActionResult<Response<int>> DeleteMasDisabilityBenefit(int DisabilityBenefitID, int TenantID, int LocationID)
        {
            try
            {
                var result = _disabilityBenefitService.DeleteMasDisabilityBenefit(DisabilityBenefitID, TenantID, LocationID);
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