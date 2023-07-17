using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDodos.Domain.Administrative;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.Administrative;
using MyDodos.Service.Administrative;
using MyDodos.Service.Logger;
using MyDodos.ViewModel.Common;
using System;
using System.Collections.Generic;

namespace MyDodos.API.Controllers.Administrative
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationRepository _configurationRepository;
        private readonly IConfigurationService _configurationService;
        private readonly ILoggerManager _logger;
        public ConfigurationController(IConfigurationRepository configurationRepository, IConfigurationService configurationService)
        {
            _configurationRepository = configurationRepository;
            _configurationService = configurationService;
        }
        [HttpPost("SaveConfigSettings")]
        public ActionResult<Response<int>> SaveConfigSettings(List<ConfigSettingsBO> objconfig)
        {
            try
            {
                var result = _configurationService.SaveConfigSettings(objconfig);
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
        [HttpPost("GetConfigSettings")]
        public ActionResult<Response<List<ConfigSettingsBO>>> GetConfigSettings(ConfigSettingsBO objconfig)
        {
            try
            {
                var result = _configurationService.GetConfigSettings(objconfig);
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
        [HttpDelete("DeleteConfigSettings/{ConfigID}/{TenantID}/{LocationID}")]
        public ActionResult<Response<int>> DeleteConfigSettings(int ConfigID,int TenantID,int LocationID)
        {
            try
            {
                var result = _configurationService.DeleteConfigSettings(ConfigID,TenantID,LocationID);
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
        [HttpPost("SaveGeneralConfigSettings")]
        public ActionResult<Response<int>> SaveGeneralConfigSettings(GeneralConfigSettingsBO objconfig)
        {
            try
            {
                var result = _configurationService.SaveGeneralConfigSettings(objconfig);
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
        [HttpPost("GetGeneralConfigSettings")]
        public ActionResult<Response<List<GeneralConfigSettingsBO>>> GetGeneralConfigSettings(GeneralConfigSettingsBO objconfig)
        {
            try
            {
                var result = _configurationService.GetGeneralConfigSettings(objconfig);
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
        [HttpGet("GetProofDetails/{CountryID}")]
        public ActionResult<Response<List<ProofDetailsBO>>> GetProofDetails(int CountryID)
        {
            try
            {
                var result = _configurationService.GetProofDetails(CountryID);
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
        [HttpPost("SaveTimesheetPaymentModeSettings")]
        public ActionResult<Response<SaveOut>> SaveTimesheetPaymentModeSettings(TSPaymentModeSettingsBO mode)
        {
            try
            {
                var result = _configurationService.SaveTimesheetPaymentModeSettings(mode);
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
        [HttpPost("GetTimesheetPaymentModeSettings")]
        public ActionResult<Response<TSPaymentModeSettingsBO>> GetTimesheetPaymentModeSettings(TSPaymentModeSettingsBO mode)
        {
            try
            {
                var result = _configurationService.GetTimesheetPaymentModeSettings(mode);
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
                return StatusCode(500, new Response<TSPaymentModeSettingsBO>(ex.Message, 500));
            }
        }
    }
}