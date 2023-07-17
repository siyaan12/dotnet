using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.LoginBO;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.Common;
using MyDodos.ViewModel.LeaveManagement;
using MySql.Data.MySqlClient;

namespace MyDodos.Repository.LeaveManagement
{
    public class LeaveRepository : ILeaveRepository
    {
        private readonly IConfiguration _configuration;
        public LeaveRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public Response<List<LoginLocationBO>> GetLocation(int TenantID, int LocationID)
        {
            Response<List<LoginLocationBO>> response;            
            try
            {
            List<LoginLocationBO> obj = new List<LoginLocationBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Tenant_ID", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetLocation";
                obj = SqlMapper.Query<LoginLocationBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();                
            }
            conn.Close();
            conn.Dispose();
            response = new Response<List<LoginLocationBO>>(obj, 200, "Data Retreived");
                
            }
            catch (Exception ex)
            {
                response = new Response<List<LoginLocationBO>>(ex.Message, 500);
            }
            return response;
        }
        public LeaveRequestModelMsg AddNewLeaveRequest(LeaveRequestModel leave)
        {
            LeaveRequestModelMsg Msg = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Leave_ID", leave.LeaveID, DbType.Int32, ParameterDirection.Input);
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
            dyParam.Add("@leaveSession", leave.LeaveSession, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Leave_Comments", leave.LeaveComments, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Leave_Status", leave.LeaveStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Created_By", leave.CreatedBy, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveLeaveRequest";
                Msg = SqlMapper.Query<LeaveRequestModelMsg>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public int SaveLeaveAlloc(HRGetMyLeaveList leave)
        {
            int Msg = 0;
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Leave_ID", leave.LeaveID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Emp_ID", leave.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Year_ID", leave.YearID, DbType.Int32, ParameterDirection.Input);            
            dyParam.Add("@Leave_From", leave.LeaveFrom, DbType.Date, ParameterDirection.Input);
            dyParam.Add("@Leave_To", leave.LeaveTo, DbType.Date, ParameterDirection.Input);
            dyParam.Add("@Category_ID", leave.LeaveCategoryID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@No_OfDays", leave.NoOfDays, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@Created_By", leave.CreatedBy, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveLeaveAlloc";
                Msg = SqlMapper.Query<int>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public int SaveCategoryList(HRGetMyLeaveList leave)
        {
            int Msg = 0;
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Emp_ID", leave.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Category_ID", leave.LeaveCategoryID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Year_ID", leave.YearID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Tenant_ID", leave.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", leave.LocationID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_UpdateLeaveAlloc";
                Msg = SqlMapper.Query<int>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public List<HRGetMyLeaveList> GetLeaveStatus(LeaveRequestModel leave)
        {
            List<HRGetMyLeaveList> Msg = new List<HRGetMyLeaveList>();
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
                var query = "sp_GetLeaveStatus";
                Msg = SqlMapper.Query<HRGetMyLeaveList>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public List<LeaveRequestModel> GetReportandEmpList(LeaveRequestModel leave)
        {
            List<LeaveRequestModel> Msg = new List<LeaveRequestModel>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Emp_ID", leave.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Tenant_ID", leave.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", leave.LocationID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetReportandEmpList";
                Msg = SqlMapper.Query<LeaveRequestModel>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public List<HRGetMyLeaveList> GetLeave(int LeaveID, int TenantID, int LocationID)
        {
            List<HRGetMyLeaveList> Msg = new List<HRGetMyLeaveList>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Leave_ID", LeaveID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Tenant_ID", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", LocationID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetLeave";
                Msg = SqlMapper.Query<HRGetMyLeaveList>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public List<MasLeaveCategoryBO> GetCategoryList(LeaveRequestModel leave)
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
                var query = "sp_GetEmpCategory";
                Msg = SqlMapper.Query<MasLeaveCategoryBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
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
                var query = "sp_GetEmpCategory";
                Msg = SqlMapper.Query<MasLeaveCategoryBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public SaveOut SaveLeaveCategoryMaster(HRVwBeneftisLeave_BO category)
        {
            SaveOut obj = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@categoryId", category.CategoryID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@categoryName", category.CategoryName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@categoryType", category.CategoryType, DbType.String, ParameterDirection.Input);
            dyParam.Add("@gender", category.Gender, DbType.String, ParameterDirection.Input);
            dyParam.Add("@isIncidentBased", category.IsIncidentBased, DbType.Boolean, ParameterDirection.Input);
            dyParam.Add("@isTimeOffCategory", category.IsTimeOffCategory, DbType.Boolean, ParameterDirection.Input);
            dyParam.Add("@tenantId", category.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", category.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@createdBy", category.CreatedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@modifiedBy", category.ModifiedBy, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveLeaveCategory";
                obj = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return obj;
        }
        public List<HRVwBeneftisLeave_BO> GetLeaveCategoryMaster(int TenantID,int LocationID)
        {
            List<HRVwBeneftisLeave_BO> Msg = new List<HRVwBeneftisLeave_BO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Tenant_ID", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", LocationID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetLeaveCategory";
                Msg = SqlMapper.Query<HRVwBeneftisLeave_BO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public GetMyLeaveList GetMyLeaveList(GetMyLeaveList inputParam)
        {
            GetMyLeaveList objLeave = new GetMyLeaveList();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Tenant_Id", inputParam.objMyLeaveListInput.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", inputParam.objMyLeaveListInput.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Year_ID", inputParam.objMyLeaveListInput.YearID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Emp_ID", inputParam.objMyLeaveListInput.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@ManagerEmp_ID", inputParam.objMyLeaveListInput.ManagerID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Leave_Status", inputParam.objMyLeaveListInput.LeaveStatus, DbType.String, ParameterDirection.Input);
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
                var query = "sp_GetMyLeaveList";
                var data= SqlMapper.Query<HRGetMyLeaveList>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                objLeave.objMyLeaveList = data;
                //objLeave.objMyLeaveListInput = inputParam.objMyLeaveListInput;
                objLeave.ServerSearchables = inputParam.ServerSearchables;
                if(objLeave.objMyLeaveList.Count > 0)
                {
                    objLeave.ServerSearchables.RecordsTotal = objLeave.objMyLeaveList[0].TotalCount;
                    objLeave.ServerSearchables.RecordsFiltered = objLeave.objMyLeaveList.Count();
                }
            }
            conn.Close();
            conn.Dispose();
            return objLeave;
        }
        public List<HRGetMyLeaveList> GetLeaveLOPList(LeaveRequestModel leave)
        {
            List<HRGetMyLeaveList> Msg = new List<HRGetMyLeaveList>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Emp_ID", leave.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Year_ID", leave.YearID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Tenant_ID", leave.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", leave.LocationID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetLeaveLOP";
                Msg = SqlMapper.Query<HRGetMyLeaveList>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public LeaveRequestModelMsg MAddNewLeaveRequest(LeaveRequestModel leave)
        {
            LeaveRequestModelMsg Msg = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Leave_ID", leave.LeaveID, DbType.Int32, ParameterDirection.Input);
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
            dyParam.Add("@leaveSession", leave.LeaveSession, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Leave_Comments", leave.LeaveComments, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Leave_Status", leave.LeaveStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Created_By", leave.CreatedBy, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveLeaveRequest";
                Msg = SqlMapper.Query<LeaveRequestModelMsg>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public List<HRGetMyLeaveList> MGetLeave(int LeaveID, int TenantID, int LocationID)
        {
            List<HRGetMyLeaveList> Msg = new List<HRGetMyLeaveList>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Leave_ID", LeaveID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Tenant_ID", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", LocationID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetLeave";
                Msg = SqlMapper.Query<HRGetMyLeaveList>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public int MSaveLeaveAlloc(HRGetMyLeaveList leave)
        {
            int Msg = 0;
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Leave_ID", leave.LeaveID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Emp_ID", leave.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Year_ID", leave.YearID, DbType.Int32, ParameterDirection.Input);            
            dyParam.Add("@Leave_From", leave.LeaveFrom, DbType.Date, ParameterDirection.Input);
            dyParam.Add("@Leave_To", leave.LeaveTo, DbType.Date, ParameterDirection.Input);
            dyParam.Add("@Category_ID", leave.LeaveCategoryID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@No_OfDays", leave.NoOfDays, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@Created_By", leave.CreatedBy, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveLeaveAlloc";
                Msg = SqlMapper.Query<int>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public List<HRGetMyLeaveList> MGetMyLeaveList(GetMyLeaveListInputs inputParam)
        {
            List<HRGetMyLeaveList> objLeave = new List<HRGetMyLeaveList>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", inputParam.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", inputParam.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@yearId", inputParam.YearID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@empId", inputParam.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@managerId", inputParam.ManagerID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@leaveStatus", inputParam.LeaveStatus, DbType.String, ParameterDirection.Input);
            
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_MGetMyLeaveList";
                objLeave = SqlMapper.Query<HRGetMyLeaveList>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objLeave;
        }
        public List<MobileLeaveCategoryBO> MGetCategoryList(LeaveRequestModel leave)
        {
            List<MobileLeaveCategoryBO> Msg = new List<MobileLeaveCategoryBO>();
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
                var query = "sp_GetEmpCategory";
                Msg = SqlMapper.Query<MobileLeaveCategoryBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public int MSaveCategoryList(HRGetMyLeaveList leave)
        {
            int Msg = 0;
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Emp_ID", leave.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Category_ID", leave.LeaveCategoryID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Year_ID", leave.YearID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Tenant_ID", leave.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", leave.LocationID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_UpdateLeaveAlloc";
                Msg = SqlMapper.Query<int>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
    }
}