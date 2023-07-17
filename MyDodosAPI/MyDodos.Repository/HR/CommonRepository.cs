using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Administrative;
using MyDodos.Domain.HR;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.Common;
using MySql.Data.MySqlClient;

namespace MyDodos.Repository.HR
{
    public class CommonRepository : ICommonRepository
    {
        private readonly IConfiguration _configuration;
        public CommonRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("KOPRODADBConnection"));
            return conn;
        }
        public IDbConnection GetDodosConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public List<CountryBO> GetCountryList(int ProductID, int CountryID)
        {

            List<CountryBO> obj = new List<CountryBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@productId", ProductID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@countryId", CountryID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetCountryDetails";
                obj = SqlMapper.Query<CountryBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return obj;
        }
        public Response<List<StatesBO>> GetStateList(int ProductID, int CountryID)
        {
            Response<List<StatesBO>> response;
            try
            {
                List<StatesBO> obj = new List<StatesBO>();
                DynamicParameters dyParam = new DynamicParameters();
                dyParam.Add("@productId", ProductID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@countryId", CountryID, DbType.Int32, ParameterDirection.Input);
                var conn = this.GetConnection();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (conn.State == ConnectionState.Open)
                {
                    var query = "sp_GetStatesDetails";
                    obj = SqlMapper.Query<StatesBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                }
                conn.Close();
                conn.Dispose();
                response = new Response<List<StatesBO>>(obj, 200);
            }
            catch (Exception ex)
            {
                response = new Response<List<StatesBO>>(ex.Message, 500);
            }
            return response;
        }
        public SaveOut SaveMultiSelectCountry(ConfigSettingsBO country)
        {
            SaveOut rtnval = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@configId", country.ConfigID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", country.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@incrementNo", country.IncrementNo, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@identifierType", country.IdentifierType, DbType.String, ParameterDirection.Input);
            dyParam.Add("@identifierColumnName", country.IdentifierColumnName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@serviceName", country.ServiceName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@locationId", country.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@generateMode", country.GenerateMode, DbType.String, ParameterDirection.Input);
            dyParam.Add("@prefixValue", country.PrefixValue, DbType.String, ParameterDirection.Input);
            dyParam.Add("@startNo", country.StartNo, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@fixedWidth", country.FixedWidth, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@timelimit", country.timelimit, DbType.String, ParameterDirection.Input);
            dyParam.Add("@timePeriod", country.timePeriod, DbType.String, ParameterDirection.Input);
            dyParam.Add("@isActive", country.IsActive, DbType.Boolean, ParameterDirection.Input);
            var conn = this.GetDodosConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveMultiSelectCountry";
                rtnval = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<ConfigSettingsBO> GetCountryByTenant(int TenantID, int LocationID)
        {
            List<ConfigSettingsBO> result = new List<ConfigSettingsBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetDodosConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetCountrybyTenant";
                result = SqlMapper.Query<ConfigSettingsBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return result;
        }
        public List<TimeZoneBO> GetTimeZoneDetails()
        {
            List<TimeZoneBO> objtime = new List<TimeZoneBO>();
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetTimeZone";
                objtime = SqlMapper.Query<TimeZoneBO>(conn, query, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objtime;
        }
        public List<CurrencyBO> GetCurrencyDetails()
        {
            List<CurrencyBO> objtime = new List<CurrencyBO>();
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetCurrencyDetails";
                objtime = SqlMapper.Query<CurrencyBO>(conn, query, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objtime;
        }
        public List<WeekDayBO> GetMasterWeekDays(int TenantID, int LocationID)
        {
            List<WeekDayBO> result = new List<WeekDayBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetDodosConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetMasWeek";
                result = SqlMapper.Query<WeekDayBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return result;
        }
        public SaveOut SaveStgDownloadDoc(StgDownloadDocBO stgdown)
        {
            SaveOut rtnval = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@downloadDocId", stgdown.DownloadDocID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@downloadDocName", stgdown.DownloadDocName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@docId", stgdown.DocID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@repositoryId", stgdown.RepositoryID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@docType", stgdown.DocType, DbType.String, ParameterDirection.Input);
            dyParam.Add("@isDownload", stgdown.IsDownload, DbType.Boolean, ParameterDirection.Input);
            dyParam.Add("@downloadBy", stgdown.DownloadBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@downloadOn", stgdown.DownloadOn, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@downloadDocStatus", stgdown.DownloadDocStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@downloadComments", stgdown.DownloadComments, DbType.String, ParameterDirection.Input);
            dyParam.Add("@tenantId", stgdown.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", stgdown.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@entity", stgdown.Entity, DbType.String, ParameterDirection.Input);
            dyParam.Add("@entityId", stgdown.EntityID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@createdBy", stgdown.CreatedBy, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetDodosConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveStgDownloadDoc";
                rtnval = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<StgDownloadDocBO> GetStgDownloadDoc(int TenantID, int DownloadDocID,int EntityID,string Entity)
        {
            List<StgDownloadDocBO> result = new List<StgDownloadDocBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@downloadDocId", DownloadDocID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@entityId", EntityID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@entity", Entity, DbType.String, ParameterDirection.Input);
            var conn = this.GetDodosConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetStgDownloadDoc";
                result = SqlMapper.Query<StgDownloadDocBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return result;
        }
        public string GetExecutiveScript(string Entity)
        {
            string result = string.Empty;
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@queryText", Entity, DbType.String, ParameterDirection.Input);
            var conn = this.GetDodosConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_ExecutiveScript";
                result = SqlMapper.Query<string>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return result;
        }
        public string ExecutiveScript(ExecutiveScript script)
        {
            string rtnval =  string.Empty;
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@queryText", script.queryText, DbType.String, ParameterDirection.Input);
            
            
            var conn = this.GetDodosConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_ExecutiveScript";
                rtnval = SqlMapper.Query<string>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
    }
}