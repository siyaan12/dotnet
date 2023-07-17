using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDodos.Domain.Report;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.Report;
using MyDodos.Service.Logger;
using MyDodos.Service.Report;
using MyDodos.ViewModel.Report;

namespace MyDodos.API.Controllers.Report
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IReportRepository _reportRepository;
        private readonly ILoggerManager _logger;
        public ReportController(IReportService reportService,IReportRepository reportRepository)
        {
            _reportService = reportService;
            _reportRepository = reportRepository;
        }
        [HttpGet("GetReportTypes/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<ReportTypesBO>>> GetReportTypes(int TenantID, int LocationID)
        {
            try
            {
                var result = _reportService.GetReportTypes(TenantID,LocationID);
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
        [HttpPost("GetReports")]
        public ActionResult<Response<ReportsDataSearch>> GetReports(ReportsDataSearch objReportInput)
        {
            try
            {
                var result = _reportService.GetReports(objReportInput);
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