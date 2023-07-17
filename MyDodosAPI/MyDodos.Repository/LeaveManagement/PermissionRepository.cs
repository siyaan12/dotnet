using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.PermissionBO;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.LeaveManagement;
using MySql.Data.MySqlClient;

namespace MyDodos.Repository.LeaveManagement
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly IConfiguration _configuration;
        public PermissionRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public PermissionRequestModelMsg AddNewPermissionRequest(PermissionModel permission)
        {
            PermissionRequestModelMsg Msg = new PermissionRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Perm_ID", permission.PermID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Perm_Date", permission.PermDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@Perm_Description", permission.PermDescription, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Perm_Status", permission.PermStatus, DbType.String, ParameterDirection.Input);            
            dyParam.Add("@Perm_Duration", permission.PermDuration, DbType.Time, ParameterDirection.Input);
            dyParam.Add("@Perm_StartTime", permission.PermStartTime, DbType.Time, ParameterDirection.Input);
            dyParam.Add("@Perm_EndTime", permission.PermEndTime, DbType.Time, ParameterDirection.Input);
            dyParam.Add("@Perm_Comments", permission.PermComments, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Emp_ID", permission.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", permission.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Tenant_ID", permission.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Year_ID", permission.YearId, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Created_By", permission.CreatedBy, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SavePermission";
                Msg = SqlMapper.Query<PermissionRequestModelMsg>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public List<PermissionModel> GetPermission(int TenantID, int LocationID, int PermID)
        {
            List<PermissionModel> Msg = new List<PermissionModel>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Tenant_ID", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Perm_ID", PermID, DbType.Int32, ParameterDirection.Input);
            
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPermission";
                Msg = SqlMapper.Query<PermissionModel>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public GetMyPermissionList GetMyPermissionList(GetMyPermissionList inputParam)
        {
            GetMyPermissionList objPerm = new GetMyPermissionList();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Tenant_Id", inputParam.objMyPermissionListInput.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", inputParam.objMyPermissionListInput.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Year_ID", inputParam.objMyPermissionListInput.YearID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Emp_ID", inputParam.objMyPermissionListInput.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@ManagerEmp_ID", inputParam.objMyPermissionListInput.ManagerID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Perm_Status", inputParam.objMyPermissionListInput.PermStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@search_data", inputParam.ServerSearchables.search_data, DbType.String, ParameterDirection.Input);
            dyParam.Add("@page_No", inputParam.ServerSearchables.page_No, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@page_Size", inputParam.ServerSearchables.page_Size, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@orderBy_Column", inputParam.ServerSearchables.orderBy_Column, DbType.String, ParameterDirection.Input);
            dyParam.Add("@order_By", inputParam.ServerSearchables.order_By, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetMyPermissionList";
                var data= SqlMapper.Query<PermissionModel>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                objPerm.objMyPermissionList = data;
                objPerm.objMyPermissionListInput = inputParam.objMyPermissionListInput;
                objPerm.ServerSearchables = inputParam.ServerSearchables;
                if(objPerm.objMyPermissionList.Count>0)
                {
                    objPerm.ServerSearchables.RecordsTotal = objPerm.objMyPermissionList[0].TotalCount;
                    objPerm.ServerSearchables.RecordsFiltered = objPerm.objMyPermissionList.Count();
                }
            }
            conn.Close();
            conn.Dispose();
            return objPerm;
        }
        public PermissionRequestModelMsg MAddNewPermissionRequest(PermissionModel permission)
        {
            PermissionRequestModelMsg Msg = new PermissionRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Perm_ID", permission.PermID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Perm_Date", permission.PermDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@Perm_Description", permission.PermDescription, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Perm_Status", permission.PermStatus, DbType.String, ParameterDirection.Input);            
            dyParam.Add("@Perm_Duration", permission.PermDuration, DbType.Time, ParameterDirection.Input);
            dyParam.Add("@Perm_StartTime", permission.PermStartTime, DbType.Time, ParameterDirection.Input);
            dyParam.Add("@Perm_EndTime", permission.PermEndTime, DbType.Time, ParameterDirection.Input);
            dyParam.Add("@Perm_Comments", permission.PermComments, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Emp_ID", permission.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", permission.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Tenant_ID", permission.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Year_ID", permission.YearId, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Created_By", permission.CreatedBy, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SavePermission";
                Msg = SqlMapper.Query<PermissionRequestModelMsg>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public List<PermissionModel> MGetPermission(int TenantID, int LocationID, int PermID)
        {
            List<PermissionModel> Msg = new List<PermissionModel>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Tenant_ID", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Perm_ID", PermID, DbType.Int32, ParameterDirection.Input);
            
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPermission";
                Msg = SqlMapper.Query<PermissionModel>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public List<PermissionModel> MGetMyPermissionList(GetMyPermissionListInputs inputParam)
        {
            List<PermissionModel> objPerm = new List<PermissionModel>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", inputParam.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", inputParam.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@yearId", inputParam.YearID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@empId", inputParam.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@managerId", inputParam.ManagerID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@permStatus", inputParam.PermStatus, DbType.String, ParameterDirection.Input);
            
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_MGetMyPermissionList";
                objPerm = SqlMapper.Query<PermissionModel>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                
            }
            conn.Close();
            conn.Dispose();
            return objPerm;
        }
    }
}