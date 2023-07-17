using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.TimeOff;
using MyDodos.Domain.LeaveBO;
using MyDodos.Repository.TimeOff;
using MySql.Data.MySqlClient;
using MyDodos.ViewModel.TimeOff;

namespace MyDodos.Repository.LeaveManagement
{
    public class TimeOffRepository : ITimeOffRepository
    {
        private readonly IConfiguration _configuration;
        public TimeOffRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public LeaveRequestModelMsg AddNewTimeOffRequest(TimeoffRequestModel leave)
        {
            LeaveRequestModelMsg Msg = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Timeoff_ID", leave.TimeoffID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Emp_ID", leave.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Teant_ID", leave.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", leave.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Year_ID", leave.YearID, DbType.Int32, ParameterDirection.Input);            
            dyParam.Add("@Leave_From", leave.LeaveFrom, DbType.Date, ParameterDirection.Input);
            dyParam.Add("@Leave_To", leave.LeaveTo, DbType.Date, ParameterDirection.Input);
            dyParam.Add("@Leave_Type", leave.LeaveType, DbType.String, ParameterDirection.Input);
            dyParam.Add("@LeaveCategory_ID", leave.LeaveCategoryID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@NoOf_Days", leave.NoOfDays, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@Leave_Reason", leave.LeaveReason, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Manager_Comments", leave.ManagerComments, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Leave_Status", leave.LeaveStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Is_Salary", leave.IsSalary, DbType.Boolean, ParameterDirection.Input);
            dyParam.Add("@Created_By", leave.CreatedBy, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveTimeoffRequest";
                Msg = SqlMapper.Query<LeaveRequestModelMsg>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public List<TimeoffRequestModel> GetTimeOffLeaveStatus(LeaveRequestModel leave)
        {
            List<TimeoffRequestModel> Msg = new List<TimeoffRequestModel>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Emp_ID", leave.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Year_ID", leave.YearID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Tenant_ID", leave.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", leave.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Leave_Status", leave.LeaveStatus, DbType.String, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetTimeOffLeaveStatus";
                Msg = SqlMapper.Query<TimeoffRequestModel>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public List<MasLeaveCategoryBO> GetEmployeeCategory(LeaveRequestModel leave)
        {
            List<MasLeaveCategoryBO> Msg = new List<MasLeaveCategoryBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Emp_ID", leave.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Year_ID", leave.YearID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Tenant_ID", leave.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", leave.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@BenefitGroup_ID", leave.BenefitGroupID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEmpTimeOffCategory";
                Msg = SqlMapper.Query<MasLeaveCategoryBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public List<HRVwBeneftisLeave_BO> GetTimeOffLeaveCategory(LeaveRequestModel leave)
        {
            List<HRVwBeneftisLeave_BO> Msg = new List<HRVwBeneftisLeave_BO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Emp_ID", leave.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Year_ID", leave.YearID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Tenant_ID", leave.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", leave.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@BenefitGroup_ID", leave.BenefitGroupID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEmpTimeOffCategory";
                Msg = SqlMapper.Query<HRVwBeneftisLeave_BO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public GetMyTimeoffList GetMyTimeoffList(GetMyTimeoffList inputParam)
        {
            GetMyTimeoffList objLeave = new GetMyTimeoffList();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Tenant_Id", inputParam.objMyTimeoffListInput.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", inputParam.objMyTimeoffListInput.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Year_ID", inputParam.objMyTimeoffListInput.YearID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Emp_ID", inputParam.objMyTimeoffListInput.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@ManagerEmp_ID", inputParam.objMyTimeoffListInput.ManagerID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Leave_Status", inputParam.objMyTimeoffListInput.LeaveStatus, DbType.String, ParameterDirection.Input);
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
                var query = "sp_GetMyTimeoffList";
                var data= SqlMapper.Query<TimeoffRequestModel>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                objLeave.objMyTimeoffList = data;
                //objLeave.objMyLeaveListInput = inputParam.objMyLeaveListInput;
                objLeave.ServerSearchables = inputParam.ServerSearchables;
                if(objLeave.objMyTimeoffList.Count>0)
                {
                    objLeave.ServerSearchables.RecordsTotal = objLeave.objMyTimeoffList[0].TotalCount;
                    objLeave.ServerSearchables.RecordsFiltered = objLeave.objMyTimeoffList.Count();
                }
            }
            conn.Close();
            conn.Dispose();
            return objLeave;
        }
        public List<TimeoffRequestModel> GetTimeOffLeave(int TimeoffID, int TenantID, int LocationID)
        {
            List<TimeoffRequestModel> Msg = new List<TimeoffRequestModel>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Timeoff_ID", TimeoffID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Tenant_ID", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", LocationID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetTimeOffLeave";
                Msg = SqlMapper.Query<TimeoffRequestModel>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
    }
}