using Microsoft.AspNetCore.Mvc;
using MyDodos.Domain.HR;
using MyDodos.Domain.Wrapper;
using MyDodos.Service.HR;
using MyDodos.Service.Logger;
using MyDodos.Repository.HR;
using System;
using System.Collections.Generic;
using MyDodos.Domain.Administrative;
using MyDodos.ViewModel.Common;
using Microsoft.AspNetCore.Authorization;
using KoSoft.DocRepo;
using MyDodos.Domain.AzureStorage;

namespace MyDodos.API.Controllers.HR
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ICommonService _commonService;
        private readonly ILoggerManager _logger;
        public CommonController(ICommonService commonService)
        {
            _commonService = commonService;

        }
        /* Country List Details */
        [HttpGet("GetCountryList/{ProductID}/{CountryID}")]
        public ActionResult<Response<CountryListBO>> GetCountryList(int ProductID, int CountryID)
        {
            try
            {
                var result = _commonService.GetCountryList(ProductID, CountryID);
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
        [HttpPost("SaveMultiSelectCountry")]
        public ActionResult<Response<SaveOut>> SaveMultiSelectCountry(ConfigSettingsBO country)
        {
            try
            {
                var result = _commonService.SaveMultiSelectCountry(country);
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
                return StatusCode(500, new Response<SaveOut>(ex.Message, 500));
            }
        }
        [HttpGet("GetCountryByTenant/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<ConfigSettingsBO>>> GetCountryByTenant(int TenantID, int LocationID)
        {
            try
            {
                var result = _commonService.GetCountryByTenant(TenantID, LocationID);
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
                return StatusCode(500, new Response<string>(ex.Message, 500));
            }
        }
        [HttpGet("GetTimeZoneDetails")]
        public ActionResult<Response<List<TimeZoneBO>>> GetTimeZoneDetails()
        {
            try
            {
                var result = _commonService.GetTimeZoneDetails();
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
                return StatusCode(500, new Response<string>(ex.Message, 500));
            }
        }
        [HttpGet("GetCurrencyDetails")]
        public ActionResult<Response<List<CurrencyBO>>> GetCurrencyDetails()
        {
            try
            {
                var result = _commonService.GetCurrencyDetails();
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
                return StatusCode(500, new Response<string>(ex.Message, 500));
            }
        }
        [HttpGet("GetDocInfo/{TenantID}/{LocationID}/{EmpID}")]
        public ActionResult<Response<List<GenDocument>>> GetDocInfo(int TenantID, int LocationID, int EmpID)
        {
            try
            {
                var result = _commonService.GetDocInfo(TenantID, LocationID, EmpID);
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
                return StatusCode(500, new Response<List<GenDocument>>(ex.Message, 500));
            }
        }
        [HttpGet("DownloadDocs/{docId}/{productId}")]
        public ActionResult<Response<AzureDocURLBO>> DownloadDocs(int docId, int productId)
        {
            try
            {
                var result = _commonService.DownloadDocs(docId, productId);
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
                return StatusCode(500, new Response<AzureDocURLBO>(ex.Message, 500));
            }
        }
        [HttpGet("DeleteDocs/{docId}/{productId}")]
        public ActionResult<Response<AzureDocURLBO>> DeleteDocs(int docId, int productId)
        {
            try
            {
                var result = _commonService.DeleteDocs(docId, productId);
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
                return StatusCode(500, new Response<AzureDocURLBO>(ex.Message, 500));
            }
        }
        [HttpGet("DownloadBase64Docs/{docId}/{productId}")]
        public ActionResult<Response<AzureDocURLBO>> Base64DownloadDocs(int docId, int productId)
        {
            try
            {
                var result = _commonService.Base64DownloadDocs(docId, productId);
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
                return StatusCode(500, new Response<AzureDocURLBO>(ex.Message, 500));
            }
        }
        [HttpGet("GetMasterWeekDays/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<WeekDayBO>>> GetMasterWeekDays(int TenantID, int LocationID)
        {
            try
            {
                var result = _commonService.GetMasterWeekDays(TenantID, LocationID);
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
                return StatusCode(500, new Response<List<WeekDayBO>>(ex.Message, 500));
            }
        }
        [HttpPost("SaveStgDownloadDoc")]
        public ActionResult<Response<SaveOut>> SaveStgDownloadDoc(StgDownloadDocBO stgdown)
        {
            try
            {
                var result = _commonService.SaveStgDownloadDoc(stgdown);
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
                return StatusCode(500, new Response<SaveOut>(ex.Message, 500));
            }
        }
        [HttpPost("GetStgDownloadDoc")]
        public ActionResult<Response<List<StgDownloadDocBO>>> GetStgDownloadDoc(StgDownloadDocBO stgdown)
        {
            try
            {
                var result = _commonService.GetStgDownloadDoc(stgdown.TenantID, stgdown.DownloadDocID, stgdown.EntityID, stgdown.Entity);
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
                return StatusCode(500, new Response<List<StgDownloadDocBO>>(ex.Message, 500));
            }
        }
        [AllowAnonymous]
        [HttpPost("ExecutiveScript")]
        public ActionResult<Response<string>> ExecutiveScript(ExecutiveScript script)
        {
            try
            {
                var result = _commonService.ExecutiveScript(script);
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
                return StatusCode(500, new Response<string>(ex.Message, 500));
            }
        }
    }
}