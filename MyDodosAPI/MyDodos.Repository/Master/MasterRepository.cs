using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.Master;
using MyDodos.Domain.Wrapper;
using MySql.Data.MySqlClient;

namespace MyDodos.Repository.Master
{
    public class MasterRepository : IMasterRepository
    {
        private readonly IConfiguration _configuration;
        public MasterRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public List<TenantProfileBO> GetTenantDetails(MasterInputBO master)
        {
            List<TenantProfileBO> objrange = new List<TenantProfileBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Product_ID",master.ProductID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@Entity_Name",master.EntityName,DbType.String,ParameterDirection.Input);
            dyParam.Add("@Tenant_ID",master.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@Location_ID",master.LocationID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetMasterDetails";
                objrange = SqlMapper.Query<TenantProfileBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objrange;
        }
        public List<LocationBO> GetLocationDetails(MasterInputBO master)
        {
            List<LocationBO> objrange = new List<LocationBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Product_ID",master.ProductID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@Entity_Name",master.EntityName,DbType.String,ParameterDirection.Input);
            dyParam.Add("@Tenant_ID",master.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@Location_ID",master.LocationID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetMasterDetails";
                objrange = SqlMapper.Query<LocationBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objrange;
        }
        public List<YearBO> GetYearDetails(MasterInputBO master)
        {
            List<YearBO> objrange = new List<YearBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Product_ID",master.ProductID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@Entity_Name",master.EntityName,DbType.String,ParameterDirection.Input);
            dyParam.Add("@Tenant_ID",master.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@Location_ID",master.LocationID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetMasterDetails";
                objrange = SqlMapper.Query<YearBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objrange;
        }
        public Response<List<ShiftConfigSettingBO>> GetShiftConfigSettings(int TenantID, int LocationID)
        {
            Response<List<ShiftConfigSettingBO>> response;
            try
            {
           
                List<ShiftConfigSettingBO> obj = new List<ShiftConfigSettingBO>();
                DynamicParameters dyParam = new DynamicParameters();
                dyParam.Add("@LocationID", LocationID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@serviceName", "Attendance", DbType.String, ParameterDirection.Input);
                var conn = this.GetConnection();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (conn.State == ConnectionState.Open)
                {
                    var query = "sp_GetConfigSettingsData";
                    obj = SqlMapper.Query<ShiftConfigSettingBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();                              
                }
                conn.Close();
                conn.Dispose();
                response = new Response<List<ShiftConfigSettingBO>>(obj, 200,"Data Reterived");
            }
            catch(Exception ex)
            {
                response = new Response<List<ShiftConfigSettingBO>>(ex.Message, 500);
            }
            return response;
        }
        public List<HRLeaveAllocReferenceBO> GetLeaveAllocReference(int LeaveGroupID, int LocationID, int TenantID)
        {
            List<HRLeaveAllocReferenceBO> objleave = new List<HRLeaveAllocReferenceBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@leaveGroupId",LeaveGroupID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetHRLeaveAllocReference";
                objleave = SqlMapper.Query<HRLeaveAllocReferenceBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objleave;
        }
        public LeaveRequestModelMsg DeleteLeaveAllocReference(int LeaveAllocationID)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@leaveAllocationId",LeaveAllocationID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_DeleteHRLeaveAllocReference";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();         
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<HRMasLeaveCategoryBO> GetMasLeaveCategory(int CategoryID, int LocationID, int TenantID)
        {
            List<HRMasLeaveCategoryBO> objleave = new List<HRMasLeaveCategoryBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@categoryId",CategoryID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetHRMasLeaveCategory";
                objleave = SqlMapper.Query<HRMasLeaveCategoryBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objleave;
        }
        public LeaveRequestModelMsg DeleteMasLeaveCategory(int CategoryID)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@categoryId",CategoryID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_DeleteHRMasLeaveCategory";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();         
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public LeaveRequestModelMsg SaveOptionalSet(OptionalSetBO optional)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@formId",optional.FormId,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@formName",optional.FormName,DbType.String,ParameterDirection.Input);
            dyParam.Add("@fieldId",optional.FieldId,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@fieldName",optional.FieldName,DbType.String,ParameterDirection.Input);
            dyParam.Add("@optionalSetValue",optional.OptionalSetValue,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@optionalSetText",optional.OptionalSetText,DbType.String,ParameterDirection.Input);
            dyParam.Add("@status",optional.Status,DbType.String,ParameterDirection.Input);
            dyParam.Add("@tenantId",optional.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@createdBy",optional.CreatedBy,DbType.Int32,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveOptionalSet";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<OptionalSetBO> GetOptionalSet(int TenantID, int FieldId,int OptionalSetValue)
        {
            List<OptionalSetBO> objacademic = new List<OptionalSetBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@fieldId",FieldId,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@optionalSetValue",OptionalSetValue,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetOptionalSet";
                objacademic = SqlMapper.Query<OptionalSetBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objacademic;
        }
        public LeaveRequestModelMsg DeleteOptionalSet(int FormId)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@formId",FormId,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_DeleteOptionalSet";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();         
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
    }
}