using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDodos.Domain.PermissionBO;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.LeaveManagement;
using MyDodos.Service.LeaveManagement;
using MyDodos.Service.Logger;
using MyDodos.ViewModel.LeaveManagement;
using System;
using System.Collections.Generic;

namespace MyDodos.API.Controllers.LeaveManagement
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IPermissionService _permissionService;
        private readonly ILoggerManager _logger;
        public PermissionController(IPermissionRepository permissionRepository, IPermissionService permissionService)
        {
            _permissionRepository = permissionRepository;
            _permissionService = permissionService;
        }
        [HttpPost("SaveLeave")]
        public ActionResult<Response<PermissionRequestModelMsg>> SavePermission(PermissionModel permission)
        {
            try
            {
                var result = _permissionService.AddNewPermissionRequest(permission);
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
                return StatusCode(500, new Response<PermissionRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpPost("GetMyPermissionList")]
        public ActionResult<Response<GetMyPermissionList>> GetMyPermissionList(GetMyPermissionList objresults)
        {
            try
            {
                var result = _permissionService.GetMyPermissionList(objresults);
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
                return StatusCode(500, new Response<GetMyPermissionList>(ex.Message, 500));
            }
        }
        [HttpGet("GetMyPermission/{TenantID}/{LocationID}/{PermID}")]
        public ActionResult<Response<List<PermissionModel>>> GetMyPermission(int TenantID, int LocationID, int PermID)
        {
            try
            {
                var result = _permissionService.GetMyPermission(TenantID, LocationID, PermID);
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
        [HttpPost("MSavePermission")]
        public ActionResult<Response<PermissionRequestModelMsg>> MSavePermission(PermissionModel permission)
        {
            try
            {
                var result = _permissionService.MAddNewPermissionRequest(permission);
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
                return StatusCode(500, new Response<PermissionRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpGet("MGetMyPermission/{TenantID}/{LocationID}/{PermID}")]
        public ActionResult<Response<List<PermissionModel>>> MGetMyPermission(int TenantID, int LocationID, int PermID)
        {
            try
            {
                var result = _permissionService.MGetMyPermission(TenantID, LocationID, PermID);
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
                return StatusCode(500, new Response<List<PermissionModel>>(ex.Message, 500));
            }
        }
        [HttpPost("MGetMyPermissionList")]
        public ActionResult<Response<List<PermissionModel>>> MGetMyPermissionList(GetMyPermissionListInputs objresults)
        {
            try
            {
                var result = _permissionService.MGetMyPermissionList(objresults);
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
                return StatusCode(500, new Response<List<PermissionModel>>(ex.Message, 500));
            }
        }
    }
}