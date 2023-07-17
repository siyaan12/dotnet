using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyDodos.Domain.Dashboard;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.Dashboard;
using MyDodos.Service.Dashboard;
using MyDodos.Service.Logger;

namespace MyDodos.API.Controllers.Dashboard
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IDashboardService _dashboardService;
        private readonly ILoggerManager _logger;
        public DashboardController(IDashboardRepository dashboardRepository, IDashboardService dashboardService)
        {
            _dashboardRepository = dashboardRepository;
            _dashboardService = dashboardService;
        }
        [HttpPost("GetDashboardCount")]
        public ActionResult<Response<DashboardCountBO>> GetDashboardCount(DashboardInputBO objinp)
        {
            try
            {
                var result = _dashboardService.GetDashboardCount(objinp);
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