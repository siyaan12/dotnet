using Microsoft.AspNetCore.Mvc;
using MyDodos.Domain.Administrative;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.Administrative;
using MyDodos.Service.Administrative;
using MyDodos.Service.Logger;
using MyDodos.ViewModel.Administrative;
using MyDodos.Domain.LeaveBO;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace MyDodos.API.Controllers.Administrative
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrativeController : ControllerBase
    {
        private readonly IAdministrativeRepository _administrativeRepository;
        private readonly IAdministrativeService _administrativeService;
        private readonly ILoggerManager _logger;
        public AdministrativeController(IAdministrativeRepository administrativeRepository, IAdministrativeService administrativeService)
        {
            _administrativeRepository = administrativeRepository;
            _administrativeService = administrativeService;
        }
        [HttpPost("SaveMasDepartment")]
        public ActionResult<Response<LeaveRequestModelMsg>> SaveMasDepartment(MasDepartmentBO department)
        {
            try
            {
                var result = _administrativeService.SaveMasDepartment(department);
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
                return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpGet("getMasDepartment/{TenantID}/{LocationID}/{DeptID}")]
        public ActionResult<Response<List<MasDepartmentBO>>> GetMasDepartment(int TenantID, int LocationID, int DeptID)
        {
            try
            {
                var result = _administrativeService.GetMasDepartment(TenantID, LocationID, DeptID);
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
        [HttpDelete("DeleteMasDepartment/{DeptID}")]
        public ActionResult<Response<LeaveRequestModelMsg>> DeleteMasDepartment(int DeptID)
        {
            try
            {
                var result = _administrativeService.DeleteMasDepartment(DeptID);
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
                return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpPost("GetDepartmentList")]
        public ActionResult<Response<DepartmentList>> GetDepartmentList(DepartmentList objresults)
        {
            try
            {
                var result = _administrativeService.GetDepartmentList(objresults);
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
                return StatusCode(500, new Response<DepartmentList>(ex.Message, 500));
            }
        }
    }
}