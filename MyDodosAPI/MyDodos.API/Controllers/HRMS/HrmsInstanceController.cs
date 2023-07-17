using System;
using Microsoft.AspNetCore.Mvc;
using MyDodos.Domain.Wrapper;
using MyDodos.Service.HRMS;
using MyDodos.Service.Logger;
using MyDodos.ViewModel.Common;
using MyDodos.ViewModel.HRMS;

namespace MyDodos.API.Controllers.HRMS
{
    [Route("api/[controller]")]
    [ApiController]
    public class HrmsInstanceController : ControllerBase
    {
        private readonly IHrmsInstanceService _hrmsInstanceService;
        private readonly ILoggerManager _logger;
        public HrmsInstanceController(IHrmsInstanceService hrmsInstanceService)
        {
            _hrmsInstanceService = hrmsInstanceService;
        }
        [HttpPost("SaveLocation")]
        public ActionResult<Response<SaveOut>> SaveLocation(HRMSLocationBO objlocation)
        {
            try
            {
                var result = _hrmsInstanceService.SaveLocation(objlocation);
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
        [HttpPost("SaveMasDepartment")]
        public ActionResult<Response<SaveOut>> SaveMasDepartment(HRMSDepartmentBO department)
        {
            try
            {
                var result = _hrmsInstanceService.SaveMasDepartment(department);
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
        [HttpPost("SaveConsoleLeaveJournal")]
        public ActionResult<Response<SaveOut>> SaveConsoleLeaveJournal(LeaveJournalBO objJournal)
        {
            try
            {
                var result = _hrmsInstanceService.SaveConsoleLeaveJournal(objJournal);
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
    }
}