using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.BenefitManagement;
using MyDodos.Domain.Wrapper;
using MySql.Data.MySqlClient;
using MyDodos.ViewModel.Common;
using MyDodos.ViewModel.Entitlement;

namespace MyDodos.Repository.BenefitManagement
{
    public class GradeRepository : IGradeRepository
    {
        private readonly IConfiguration _configuration;
        public GradeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public SaveOut SaveGrade(Grade objgrade)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@gradeId", objgrade.GradeID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@salaryMin", objgrade.SalaryMin, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@salaryMax", objgrade.SalaryMax, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@groupTypeId", objgrade.GroupTypeID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objgrade.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objgrade.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@stageNo", objgrade.StageNo, DbType.Int16, ParameterDirection.Input);
            dyParam.Add("@gradeName", objgrade.GradeName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@createdBy", objgrade.CreatedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@modifiedBy", objgrade.ModifiedBy, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveGrade";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<Grade> GetGrade(int TenantID, int LocationID)
        {
            List<Grade> output = new List<Grade>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetGrade";
                output = SqlMapper.Query<Grade>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut DeleteGrade(int GradeID, int TenantID, int LocationID)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@gradeId", GradeID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_DeleteGrade";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut SaveRole(EntRolesBO objrole)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@roleId", objrole.RoleId, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@roleName", objrole.RoleName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@roleDesc", objrole.RoleDescription, DbType.String, ParameterDirection.Input);
            dyParam.Add("@reportToRoleId", objrole.ReportToRoleId, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@roleCategory", objrole.RoleCategory, DbType.String, ParameterDirection.Input);
            dyParam.Add("@productId", objrole.KproductId, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@userTypeId", objrole.UserTypeID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objrole.TenantID, DbType.String, ParameterDirection.Input);
            dyParam.Add("@createdBy", objrole.CreatedBy, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveGrade";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
    }
}