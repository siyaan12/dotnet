using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using MyDodos.Service.ConnectionString;

namespace MyDodos.API.Controllers.Document
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionStringController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConnectionStringService _connectionStringService;
        public ConnectionStringController(IConfiguration _configuration, IHttpContextAccessor httpContextAccessor, IConnectionStringService connectionStringService)
        {
            configuration = _configuration;
            _httpContextAccessor = httpContextAccessor;
            _connectionStringService = connectionStringService;
        }
    }
}