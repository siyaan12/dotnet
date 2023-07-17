using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.HR;
using MyDodos.Domain.Wrapper;
using MySql.Data.MySqlClient;
using MyDodos.ViewModel.Employee;

namespace MyDodos.Repository.HR
{
    public class DashBoardRepository : IDashBoardRepository
    {
        private readonly IConfiguration _configuration;
        public DashBoardRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public ProjectSummary GetProjectDashboardSummery(int TenantID,int LocationID)
        {
            ProjectSummary rtnval = new ProjectSummary();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetProjectDashboardSummery";
                rtnval = SqlMapper.Query<ProjectSummary>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<EmployeeList> GetEmployeeList(int TenantID,int LocationID,int EmpID)
        {
            List<EmployeeList> rtnval = new List<EmployeeList>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@empId",EmpID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEmployee";
                rtnval = SqlMapper.Query<EmployeeList>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<UpcomingBirthday> GetUpcomingBirthday(int TenantID,int LocationID,bool TodayBirthday)
        {
            List<UpcomingBirthday> rtnval = new List<UpcomingBirthday>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@todaybirthay",TodayBirthday,DbType.Boolean,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetUpcomingBirthdays";
                rtnval = SqlMapper.Query<UpcomingBirthday>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public HRDirectorySummeryBO GetEmployeeDashBoardSummary(int TenantID,int LocationID,int DepartmentID)
        {
            HRDirectorySummeryBO Msg = new HRDirectorySummeryBO();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Tenant_ID", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Department_ID", DepartmentID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetHRDirectSummery";
                Msg = SqlMapper.Query<HRDirectorySummeryBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public AttendanceSummary GetAttendanceDashBoardSummary(int TenantID,int LocationID,int DepartmentID)
        {
            AttendanceSummary Msg = new AttendanceSummary();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Tenant_ID", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Department_ID", DepartmentID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetHRDirectSummery";
                Msg = SqlMapper.Query<AttendanceSummary>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public List<EventList> GetDashBoardEvents(int TenantID,int LocationID,int YearID)
        {
            List<EventList> Msg = new List<EventList>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@yearId", YearID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetDashBoardEvent";
                Msg = SqlMapper.Query<EventList>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
    }
}