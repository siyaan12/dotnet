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
    public class GradeController : ControllerBase
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly IGradeService _gradeService;
        private readonly ILoggerManager _logger;
        public GradeController(IGradeRepository gradeRepository, IGradeService gradeService)
        {
            _gradeRepository = gradeRepository;
            _gradeService = gradeService;
        }
        [HttpPost("SaveGrade")]
        public ActionResult<Response<int>> SaveGrade(Grade objgrade)
        {
            try
            {
                var result = _gradeService.SaveGrade(objgrade);
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
        [HttpPost("SaveGroupTypeGrade")]
        public ActionResult<Response<int>> SaveGroupTypeGrade(RoleGrade objrolegrade)
        {
            try
            {
                var result = _gradeService.SaveGroupTypeGrade(objrolegrade);
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
                return StatusCode(500, new Response<int>(ex.Message));
            }
        }
        [HttpGet("GetGrade/{ProductID}/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<Grade>>> GetGrade(int ProductID, int TenantID, int LocationID)
        {
            try
            {
                var result = _gradeService.GetGrade(ProductID, TenantID, LocationID);
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
        [HttpDelete("DeleteGrade/{GradeID}/{TenantID}/{LocationID}")]
        public ActionResult<Response<int>> DeleteGrade(int GradeID,int TenantID,int LocationID)
        {
            try
            {
                var result = _gradeService.DeleteGrade(GradeID,TenantID,LocationID);
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