using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Administrative;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.Administrative;
using MySql.Data.MySqlClient;

namespace MyDodos.Repository.Administrative
{
    public class AdministrativeRepository : IAdministrativeRepository
    {
        private readonly IConfiguration _configuration;
        public AdministrativeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public GenSeqNum GenSequenceNo(int TenantID, int LocationID, string ServiceName)
        {
            GenSeqNum rtnval = new GenSeqNum();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@serviceName", ServiceName, DbType.String, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GenSequenceNo";

                rtnval = SqlMapper.Query<GenSeqNum>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public LeaveRequestModelMsg SaveMasDepartment(MasDepartmentBO department)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@deptId",department.DeptID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@departmentCode",department.DepartmentCode,DbType.String,ParameterDirection.Input);
            dyParam.Add("@department",department.Department,DbType.String,ParameterDirection.Input);
            dyParam.Add("@deptHead",department.DeptHead,DbType.String,ParameterDirection.Input);
            dyParam.Add("@phone",department.Phone,DbType.String,ParameterDirection.Input);
            dyParam.Add("@fax",department.Fax,DbType.String,ParameterDirection.Input);
            dyParam.Add("@deptType",department.DeptType,DbType.String,ParameterDirection.Input);
            dyParam.Add("@deptShortName",department.DeptShortName,DbType.String,ParameterDirection.Input);
            dyParam.Add("@deptStatus",department.DeptStatus,DbType.String,ParameterDirection.Input);
            dyParam.Add("@locationId",department.LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@tenantId",department.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@createdBy",department.CreatedBy,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@modifiedBy",department.ModifiedBy,DbType.Int32,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveMasDepartment";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<MasDepartmentBO> GetMasDepartment(int TenantID,int LocationID,int DeptID)
        {
            List<MasDepartmentBO> objacademic = new List<MasDepartmentBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Tenant_ID",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@Location_ID",LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@Dept_ID",DeptID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetMasDepartment";
                objacademic = SqlMapper.Query<MasDepartmentBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objacademic;
        }
        public LeaveRequestModelMsg DeleteMasDepartment(int DeptID)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Dept_ID",DeptID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_DeleteMasDepartment";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();         
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public DepartmentList GetDepartmentList(DepartmentList inputParam)
        {
            DepartmentList objLeave = new DepartmentList();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Tenant_Id", inputParam.objDepartmentInput.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", inputParam.objDepartmentInput.LocationID, DbType.Int32, ParameterDirection.Input);
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
                var query = "sp_GetDepartmentSearch";
                var data= SqlMapper.Query<MasDepartmentBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                objLeave.objDepartmentList = data;
                objLeave.objDepartmentInput = inputParam.objDepartmentInput;
                objLeave.ServerSearchables = inputParam.ServerSearchables;
                if(objLeave.objDepartmentList.Count>0)
                {
                    objLeave.ServerSearchables.RecordsTotal = objLeave.objDepartmentList[0].TotalCount;
                    objLeave.ServerSearchables.RecordsFiltered = objLeave.objDepartmentList.Count();
                }
            }
            conn.Close();
            conn.Dispose();
            return objLeave;
        }
    }
}