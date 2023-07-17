using System;
using MyDodos.Domain.Wrapper;
using MyDodos.Service.Document;
using MyDodos.ViewModel.Document;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using MyDodos.Service.Logger;
using System.Collections.Generic;

namespace MyDodos.API.Controllers.Document
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDocumentFileService _documentFileService;
        private readonly ILoggerManager _logger;
        public DocumentController(IConfiguration _configuration, IHttpContextAccessor httpContextAccessor, IDocumentFileService documentFileService)
        {
            configuration = _configuration;
            _httpContextAccessor = httpContextAccessor;
            _documentFileService = documentFileService;
        }
        [HttpGet("GetEntityName/{ProductID}/{TenantID}/{EntityID}")]
        [HttpGet("GetEntityName/{ProductID}/{TenantID}/{EntityName}/{EntityID}")]
        public ActionResult<Response<List<TemplatedetailBO>>> GetDataTemplate(int ProductID, int TenantID, string EntityName, string FileType, int EntityID)
        {
            //Response<List<TemplateFieldBO>> objdata;
            try
            {
                var doc = _documentFileService.GetDataTemplate(ProductID, TenantID, FileType, EntityName, EntityID);
                if (doc.StatusCode == 200)
                {
                    return Ok(doc);
                }
                else
                {
                    return StatusCode(doc.StatusCode, doc);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server Error{ex.Message}");
            }
        }
        [HttpPost("GetEntityName")]
        public ActionResult<Response<List<TemplatedetailBO>>> GetDataTemplateCatogory(TemplatedetailBO templete)
        {
            //Response<List<TemplateFieldBO>> objdata;
            try
            {
                var doc = _documentFileService.GetDataTemplateCatogory(templete.ProductID, templete.TenantID, templete.TemplateCategory, templete.TemplateType);
                if (doc.StatusCode == 200)
                {
                    return Ok(doc);
                }
                else
                {
                    return StatusCode(doc.StatusCode, doc);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server Error{ex.Message}");
            }
        }
        [HttpPost("DocumentFiled")]
        public ActionResult<Response<TemplatexlsxBO>> DocumentFiledxlsx(StageInputReferBO data)
        {
            Response<TemplatexlsxBO> objdata;
            try
            {                    
                objdata = _documentFileService.GetDataFileds(data.ProductID, data.TenantID, data.TemplateID, data.LocationID);
                if (objdata.StatusCode == 200)
                {
                    return Ok(objdata);
                }
                else
                {
                    return StatusCode(objdata.StatusCode, objdata);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server Error{ex.Message}");
            }
        }
        [AllowAnonymous]
        [HttpPost("SaveStagingDocument")]
        public ActionResult<Response<DocFileReturnBO>> SaveStagingDocument([FromForm] DocFileInputBO docBO)
        {
            try
            {
                var httpRequest = _httpContextAccessor.HttpContext;
                var inputJson = JsonConvert.DeserializeObject<StgreturnDataReferBO>(Request.Form["InputJson"]);
                var result = _documentFileService.SaveStagingDocument(docBO, inputJson);
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
                return StatusCode(500, new Response<DocFileReturnBO>(ex.Message, 500));
            }
        }
        [HttpPost("GetBulkStageData")]
        public ActionResult<Response<List<StgreturnDataReferBO>>> GetBulkStageData(StageInputReferBO input)
        {
            try
            {
                var result = _documentFileService.GetStageData(input);
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
                return StatusCode(500, new Response<List<StgreturnDataReferBO>>(ex.Message, 500));
            }
        }
        [HttpPost("GetAllStageSerachData")]
        public ActionResult<Response<StageSearchBO>> GetAllStageSerachData(StageSearchBO input)
        {
            try
            {
                var result = _documentFileService.GetAllStageSerachData(input);
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
                return StatusCode(500, new Response<StageSearchBO>(ex.Message, 500));
            }
        }
        [HttpPost("SaveAllStagingData")]
        public ActionResult<Response<int>> SaveAllStagingData(InputBulkBO objstage)
        {
            Response<int> response = new Response<int>();
            try
            {
                if (objstage.EntityName == "Employee")
                {
                    response = _documentFileService.SaveBulkEmployee(objstage);
                }
                else if (objstage.EntityName == "Holiday")
                {
                    response = _documentFileService.SaveBulkEmployee(objstage);
                }
                else
                {
                    response = new Response<int>(response.Data, 400, "Invalid Template Name");
                }
                if (response.StatusCode == 200)
                {
                    var mm = _documentFileService.SaveStageCompleted(objstage.EntityName, objstage.UniqueBatchNO, objstage.StgDataID);
                    return Ok(response);
                }
                else
                {
                    return StatusCode(response.StatusCode, response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server Error{ex.Message}");
            }
        }
        
    }
}