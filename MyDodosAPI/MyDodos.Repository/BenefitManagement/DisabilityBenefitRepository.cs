using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Wrapper;
using MySql.Data.MySqlClient;
using MyDodos.ViewModel.Common;
using MyDodos.Domain.BenefitManagement;

namespace MyDodos.Repository.BenefitManagement
{
    public class DisabilityBenefitRepository : IDisabilityBenefitRepository
    {
        private readonly IConfiguration _configuration;
        public DisabilityBenefitRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public SaveOut SaveMasDisabilityBenefit(MasDisabilityBenefitBO objben)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@disabilityBenefitId", objben.DisabilityBenefitID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objben.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objben.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@disabilityType", objben.DisabilityType, DbType.String, ParameterDirection.Input);
            dyParam.Add("@benefitValue", objben.BenefitValue, DbType.Int16, ParameterDirection.Input);
            dyParam.Add("@benefitValueType", objben.BenefitValueType, DbType.String, ParameterDirection.Input);
            dyParam.Add("@conditionValue", objben.ConditionValue, DbType.Int16, ParameterDirection.Input);
            dyParam.Add("@conditionValueType", objben.ConditionValueType, DbType.String, ParameterDirection.Input);
            dyParam.Add("@notes", objben.Notes, DbType.String, ParameterDirection.Input);
            dyParam.Add("@benefitStatus", objben.BenefitStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@groupTypeId", objben.GroupTypeID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@createdBy", objben.CreatedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@modifiedBy", objben.ModifiedBy, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveHRMasDisabilityBenefit";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<MasDisabilityBenefitBO> GetMasDisabilityBenefit(int DisabilityBenefitID, int TenantID, int LocationID)
        {
            List<MasDisabilityBenefitBO> output = new List<MasDisabilityBenefitBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@disabilityBenefitId", DisabilityBenefitID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetHRMasDisabilityBenefit";
                output = SqlMapper.Query<MasDisabilityBenefitBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut DeleteMasDisabilityBenefit(int DisabilityBenefitID, int TenantID, int LocationID)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@disabilityBenefitId", DisabilityBenefitID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_DeleteHRMasDisabilityBenefit";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<MasDisabilityBenefitBO> GetMasDisabilityBenefitByGroupType(int GroupTypeID, int TenantID, int LocationID)
        {
            List<MasDisabilityBenefitBO> output = new List<MasDisabilityBenefitBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@groupTypeId", GroupTypeID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetHRMasDisabilityBenefitByGroupType";
                output = SqlMapper.Query<MasDisabilityBenefitBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
    }
}