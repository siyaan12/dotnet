using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.ConnectionString;
using MySql.Data.MySqlClient;

namespace MyDodos.Repository.ConnectionString
{
    public class ConnectionStringRepository : IConnectionStringRepository
    {
        private readonly IConfiguration _configuration;
        public ConnectionStringRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetServiceConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("KOPRODADBConnection"));
            return conn;
        }
        public List<ConnectionStringBO> GetTenantConnectionString(int tenantConnectionId, int productId, int tenantId)
        {
           List<ConnectionStringBO> obj = new List<ConnectionStringBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantConnectionId", tenantConnectionId, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@productId", productId, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", tenantId, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetServiceConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetTenantConnString";
                obj = SqlMapper.Query<ConnectionStringBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();                
            }
            conn.Close();
            conn.Dispose();
            return obj;
        } 
    }
}