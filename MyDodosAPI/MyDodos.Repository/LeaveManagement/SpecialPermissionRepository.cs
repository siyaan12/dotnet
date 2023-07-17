using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.PermissionBO;
using MyDodos.ViewModel.Common;
using MySql.Data.MySqlClient;

namespace MyDodos.Repository.LeaveManagement
{
    public class SpecialPermissionRepository : ISpecialPermissionRepository
    {
        private readonly IConfiguration _configuration;
        public SpecialPermissionRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public SaveOut SaveSpecialPermission (SpecialPermissionBO objperm)
        {
            SaveOut result = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@permissionId", objperm.PermissionID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@permDate", objperm.PermDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@permDescription", objperm.PermDescription, DbType.String, ParameterDirection.Input);
            dyParam.Add("@permStatus", objperm.PermStatus, DbType.String, ParameterDirection.Input);            
            dyParam.Add("@permDuration", objperm.PermDuration, DbType.Time, ParameterDirection.Input);
            dyParam.Add("@permStartTime", objperm.PermStartTime, DbType.Time, ParameterDirection.Input);
            dyParam.Add("@permEndTime", objperm.PermEndTime, DbType.Time, ParameterDirection.Input);
            dyParam.Add("@permComments", objperm.PermComments, DbType.String, ParameterDirection.Input);
            dyParam.Add("@empId", objperm.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objperm.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objperm.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@yearId", objperm.YearId, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@craeatedBy", objperm.CreatedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@modifiedBy", objperm.ModifiedBy, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveSpecialPermission";
                result = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();            
            return result;
        }
        public List<SpecialPermissionBO> GetSpecialPermission(int PermissionID, int TenantID, int LocationID)
        {
            List<SpecialPermissionBO> output = new List<SpecialPermissionBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@permissionId", PermissionID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetSpecialPermission";
                output = SqlMapper.Query<SpecialPermissionBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut DeleteSpecialPermission(int PermissionID, int TenantID, int LocationID)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@permissionId", PermissionID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_DeleteSpecialPermission";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        
    }
}