using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Dashboard;
using MySql.Data.MySqlClient;

namespace MyDodos.Repository.Dashboard
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly IConfiguration _configuration;
        public DashboardRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public DashboardCountBO GetDashboardCount(DashboardInputBO objinp)
        {
            DashboardCountBO output = new DashboardCountBO();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@tenantId", objinp.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objinp.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@dateValue", objinp.DateValue, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetDashboardCount";
                output = SqlMapper.Query<DashboardCountBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
    }
}