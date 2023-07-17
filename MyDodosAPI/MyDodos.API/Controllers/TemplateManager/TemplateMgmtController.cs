using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using MyDodos.Service.Logger;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.TemplateManager;
using MyDodos.Service.TemplateManager;
using Microsoft.AspNetCore.Authorization;

namespace MyDodos.API.Controllers.TemplateManager
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateMgmtController : ControllerBase
    {
        private readonly ITemplateMgmtService _templateMgmtService;
        private readonly ILoggerManager _logger;

        public TemplateMgmtController(ITemplateMgmtService templateMgmtService)
        {
            _templateMgmtService = templateMgmtService;
        }
        [HttpPost("GetAllTemplate")]
        public ActionResult<Response<AllTemplates>> GetAllTemplate(AllTemplates allTemplates)
        {
            try
            {
                var result = _templateMgmtService.GetAllTemplate(allTemplates);
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
                return StatusCode(500, new Response<AllTemplates>(ex.Message, 500));
            }
        }
        [HttpPost("CreateTemplates")]
        public ActionResult<Response<tblTemplateManagement>> CreateTemplates(tblTemplateManagement template)
        {
            try
            {
                var result = _templateMgmtService.CreateTemplates(template);
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
                return StatusCode(500, new Response<AllTemplates>(ex.Message, 500));
            }
        }
        [HttpGet("GetMetaTagByTemplateId/{RepoId}")]
        public ActionResult<Response<List<TemplateMetaTag>>> GetMetaTagByTemplateId(int RepoId)
        {
            try
            {
                var result = _templateMgmtService.GetMetaTagByTemplateId(RepoId);
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
                return StatusCode(500, new Response<List<TemplateMetaTag>>(ex.Message, 500));
            }
        }
        [HttpPost("GetCategoryList")]
        public ActionResult<Response<List<TemplateCategory>>> GetCategoryList(GetCategoryList categoryList)
        {
            try
            {
                var result = _templateMgmtService.GetCategoryList(categoryList);
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
                return StatusCode(500, new Response<List<TemplateCategory>>(ex.Message, 500));
            }
        }
        [HttpGet("GetTemplateType/{ProductId}/{TenantId}")]
        public ActionResult<Response<List<TemplateTypeVW>>> GetTemplateType(int ProductId, int TenantId)
        {
            try
            {
                var result = _templateMgmtService.GetTemplateType(ProductId,TenantId);
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
                return StatusCode(500, new Response<List<TemplateTypeVW>>(ex.Message, 500));
            }
        }
        [HttpGet("GetTemplateFile/{TemplateId}/{ProductId}")]
        public ActionResult<Response<TemplatePath>> GetTemplateFile(int TemplateId, int ProductId)
        {
            try
            {
                var result = _templateMgmtService.GetTemplateFile(TemplateId, ProductId);
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
                return StatusCode(500, new Response<TemplatePath>(ex.Message, 500));
            }
        }
        [HttpGet("GetAttributeList/{ProductId}")]
        public ActionResult<Response<List<TemplateAttribute>>> GetAttributeList(int ProductId)
        {
            try
            {
                var result = _templateMgmtService.GetAttributeList(ProductId);
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
                return StatusCode(500, new Response<List<TemplateAttribute>>(ex.Message, 500));
            }
        }
        [HttpDelete("DeleteTemplate/{TemplateId}")]
        public ActionResult<Response<string>> DeleteTemplate(int TemplateId)
        {
            try
            {
                var result = _templateMgmtService.DeleteTemplate(TemplateId);
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
        [HttpPut("UpdateTemplateStatus")]
        public ActionResult<Response<string>> UpdateTemplateStatus(vwInputTemp objtemp)
        {
            try
            {
                var result = _templateMgmtService.UpdateTemplateStatus(objtemp);
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
