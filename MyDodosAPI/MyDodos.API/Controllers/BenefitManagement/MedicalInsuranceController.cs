using Microsoft.AspNetCore.Mvc;
using MyDodos.Domain.BenefitManagement;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.BenefitManagement;
using MyDodos.Service.BenefitManagement;
using MyDodos.Service.Logger;
using System;
using System.Collections.Generic;
using Instrument.Utility;
using MyDodos.ViewModel.BenefitManagement;
using Microsoft.AspNetCore.Authorization;

namespace MyDodos.API.Controllers.BenefitManagement
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalInsuranceController : ControllerBase
    {
        private readonly IMedicalInsuranceRepository _medicalInsuranceRepository;
        private readonly IMedicalInsuranceService _medicalInsuranceService;
        private readonly ILoggerManager _logger;
        public MedicalInsuranceController(IMedicalInsuranceRepository medicalInsuranceRepository, IMedicalInsuranceService medicalInsuranceService)
        {
            _medicalInsuranceRepository = medicalInsuranceRepository;
            _medicalInsuranceService = medicalInsuranceService;
        }
        [HttpGet("GetPlanTypeCategory/{PlanTypeID}/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<PlanTypeCategoryBO>>> GetPlanTypeCategory(int PlanTypeID,int TenantID, int LocationID)
        {
            try
            {
                var result = _medicalInsuranceService.GetPlanTypeCategory(PlanTypeID,TenantID, LocationID);
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
         [HttpPost("SaveHRBenefitPlans")]
        public ActionResult<Response<int>> SaveHRBenefitPlans(HRBenefitPlansBO objplan)
        {
            try
            {
                var result = _medicalInsuranceService.SaveHRBenefitPlans(objplan);
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
        [HttpPost("GetHRBenefitPlans")]
        public ActionResult<Response<List<HRBenefitPlansBO>>> GetHRBenefitPlans(HRBenefitPlansBO objplan)
        {
            try
            {
                var result = _medicalInsuranceService.GetHRBenefitPlans(objplan);
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
        [HttpPost("GetHRBenefitPlansSearch")]
        public ActionResult<Response<HRBenefitPlansSearchBO>> GetHRBenefitPlansSearch(HRBenefitPlansSearchBO objplan)
        {
            try
            {
                var result = _medicalInsuranceService.GetHRBenefitPlansSearch(objplan);
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
        [HttpPost("DeleteHRBenefitPlans")]
        public ActionResult<Response<int>> DeleteHRBenefitPlans(HRBenefitPlansBO objplan)
        {
            try
            {
                var result = _medicalInsuranceService.DeleteHRBenefitPlans(objplan);
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