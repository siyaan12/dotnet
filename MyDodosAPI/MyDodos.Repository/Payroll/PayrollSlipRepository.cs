using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace MyDodos.Repository.Payroll
{
    public class PayrollSlipRepository : IPayrollSlipRepository
    {
        private readonly IConfiguration _configuration;
        public PayrollSlipRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }        
    }
}