using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Administrative;
using MyDodos.Domain.Wrapper;
using MySql.Data.MySqlClient;
using MyDodos.ViewModel.Common;

namespace MyDodos.Repository.Administrative
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly IConfiguration _configuration;
        public ConfigurationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public SaveOut SaveConfigSettings(ConfigSettingsBO objconfig)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@configId", objconfig.ConfigID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@serviceName", objconfig.ServiceName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@tenantId", objconfig.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objconfig.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@generateMode", objconfig.GenerateMode, DbType.String, ParameterDirection.Input);
            dyParam.Add("@prefixValue", objconfig.PrefixValue, DbType.String, ParameterDirection.Input);
            dyParam.Add("@startNo", objconfig.StartNo, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@incrementNo", objconfig.IncrementNo, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@fixedWidth", objconfig.FixedWidth, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@identifierType", objconfig.IdentifierType, DbType.String, ParameterDirection.Input);
            dyParam.Add("@identifierColumnName", objconfig.IdentifierColumnName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@timeLimit", objconfig.timelimit, DbType.String, ParameterDirection.Input);
            dyParam.Add("@timePeriod", objconfig.timePeriod, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveConfigSettings";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<ConfigSettingsBO> GetConfigSettings(ConfigSettingsBO objconfig)
        {
            List<ConfigSettingsBO> output = new List<ConfigSettingsBO>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@serviceName", objconfig.ServiceName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@tenantId", objconfig.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objconfig.LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetConfigSettings";
                output = SqlMapper.Query<ConfigSettingsBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut DeleteConfigSettings(int ConfigID,int TenantID,int LocationID)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@configId", ConfigID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_DeleteConfigSettings";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut SaveGeneralConfigSettings(GeneralConfigSettingsBO objconfig)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@genconfigId", objconfig.GenConfigID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@dateFormat", objconfig.DateFormat, DbType.String, ParameterDirection.Input);
            dyParam.Add("@timeZone", objconfig.TimeZone, DbType.String, ParameterDirection.Input);
            dyParam.Add("@currency", objconfig.Currency, DbType.String, ParameterDirection.Input);
            dyParam.Add("@countryType", objconfig.Countrytype, DbType.String, ParameterDirection.Input);
            dyParam.Add("@countryValues", objconfig.CountryValues, DbType.String, ParameterDirection.Input);
            dyParam.Add("@tenantId", objconfig.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objconfig.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@createdBy", objconfig.CreatedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@modifiedBy", objconfig.ModifiedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@isActive", objconfig.IsActive,DbType.Boolean,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveGeneralConfigSettings";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<GeneralConfigSettingsBO> GetGeneralConfigSettings(GeneralConfigSettingsBO objconfig)
        {
            List<GeneralConfigSettingsBO> output = new List<GeneralConfigSettingsBO>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@tenantId", objconfig.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objconfig.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@isActive", objconfig.IsActive, DbType.Boolean, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetGeneralConfigSettings";
                output = SqlMapper.Query<GeneralConfigSettingsBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<ProofDetailsBO> GetProofDetails(int CountryID)
        {
            List<ProofDetailsBO> output = new List<ProofDetailsBO>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@countryId", CountryID, DbType.Int32, ParameterDirection.Input);
           
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetProofDetails";
                output = SqlMapper.Query<ProofDetailsBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut SaveTimesheetPaymentModeSettings(TSPaymentModeSettingsBO mode)
        {
            SaveOut rtnval = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@configId",mode.ConfigID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@serviceName",mode.ServiceName,DbType.String,ParameterDirection.Input);
            dyParam.Add("@generateMode",mode.GenerateMode,DbType.String,ParameterDirection.Input);
            dyParam.Add("@IsActive",mode.IsActive,DbType.Boolean,ParameterDirection.Input);
            dyParam.Add("@tenantId",mode.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",mode.LocationID,DbType.Int32,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveTimesheetPaymentModeSettings";
                rtnval = SqlMapper.Query<SaveOut>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public TSPaymentModeSettingsBO GetTimesheetPaymentModeSettings(TSPaymentModeSettingsBO mode)
        {
            TSPaymentModeSettingsBO objacademic = new TSPaymentModeSettingsBO();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",mode.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",mode.LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@generateMode",mode.GenerateMode,DbType.String,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetTimesheetPaymentModeSettings";
                objacademic = SqlMapper.Query<TSPaymentModeSettingsBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).First();
            }
            conn.Close();
            conn.Dispose();
            return objacademic;
        }
    }
}