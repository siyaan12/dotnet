using Microsoft.AspNetCore.Mvc;
using MyDodos.Domain.PermissionBO;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.LoginBO;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.LeaveManagement;
using MyDodos.Service.LeaveManagement;
using MyDodos.Service.Logger;
using MyDodos.ViewModel.LeaveManagement;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace MyDodos.API.Controllers.LeaveManagement
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialPermissionController : ControllerBase
    {
        private readonly ISpecialPermissionRepository _specialPermissionRepository;
        private readonly ISpecialPermissionService _specialPermissionService;
        private readonly ILoggerManager _logger;
        public SpecialPermissionController(ISpecialPermissionRepository specialPermissionRepository, ISpecialPermissionService specialPermissionService)
        {
            _specialPermissionRepository = specialPermissionRepository;
            _specialPermissionService = specialPermissionService;
        }
        [HttpPost("SaveSpecialPermission")]
        public ActionResult<Response<int>> SaveSpecialPermission (SpecialPermissionBO objperm)
        {
            try
            {
                var result = _specialPermissionService.SaveSpecialPermission(objperm);
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
        [HttpGet("GetSpecialPermission/{PermissionID}/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<SpecialPermissionBO>>> GetSpecialPermission(int PermissionID, int TenantID, int LocationID)
        {
            try
            {
                var result = _specialPermissionService.GetSpecialPermission(PermissionID,TenantID, LocationID);
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
        [HttpDelete("DeleteSpecialPermission/{PermissionID}/{TenantID}/{LocationID}")]
        public ActionResult<Response<int>> DeleteSpecialPermission(int PermissionID,int TenantID,int LocationID)
        {
            try
            {
                var result = _specialPermissionService.DeleteSpecialPermission(PermissionID,TenantID,LocationID);
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