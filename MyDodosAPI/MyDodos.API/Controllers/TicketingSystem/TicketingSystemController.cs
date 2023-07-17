using Microsoft.AspNetCore.Mvc;
using MyDodos.Domain.Wrapper;
using MyDodos.Service.Logger;
using MyDodos.Service.TicketingSystem;
using MyDodos.ViewModel.TicketingSystem;
using System;
using System.Collections.Generic;

namespace MyDodos.API.Controllers.TicketingSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketingSystemController : ControllerBase
    {
        private readonly ITicketingSystemService _ticketSyatemService;
        private readonly ILoggerManager _logger;
        public TicketingSystemController(ITicketingSystemService ticketSyatemService)
        {
            _ticketSyatemService = ticketSyatemService;
        }
        [HttpPost("RiseTicket")]
        public ActionResult<Response<SaveTicket>> RiseTicket(RiseTicket inputobj)
        {
            try
            {
                var result = _ticketSyatemService.RiseTicket(inputobj);
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
                return StatusCode(500, new Response<SaveTicket>(ex.Message, 500));
            }
        }
        [HttpPost("TicketsList")]
        public ActionResult<Response<List<Ticket>>> TicketsList(TicketingInputBO inputobj)
        {
            try
            {
                var result = _ticketSyatemService.TicketsList(inputobj);
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
                return StatusCode(500, new Response<Ticket>(ex.Message, 500));
            }

        }
    }
}
