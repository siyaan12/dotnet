using Microsoft.AspNetCore.Mvc;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.HR;
using MyDodos.Service.HR;
using MyDodos.Service.Logger;
using System;
using MyDodos.Domain.HR;
using MyDodos.ViewModel.Common;
using Microsoft.AspNetCore.Authorization;

namespace MyDodos.API.Controllers.HR
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IDashBoardService _dashBoardService;
        private readonly IDashBoardRepository _dashBoardRepository;
        private readonly ILoggerManager _logger;
        public DashBoardController(IDashBoardService dashBoardService, IDashBoardRepository dashBoardRepository)
        {
            _dashBoardService = dashBoardService;
            _dashBoardRepository = dashBoardRepository;
        }
       [HttpGet("GetDashBoardList/{TenantID}/{LocationID}/{YearID}")]
        public ActionResult<Response<DashBoard>> GetDashBoardList(int TenantID, int LocationID, int YearID)
        {
            try
            {
                var result = _dashBoardService.GetDashBoardList(TenantID, LocationID, YearID);
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
                return StatusCode(500, new Response<DashBoard>(ex.Message, 500));
            }
        }
    }
}