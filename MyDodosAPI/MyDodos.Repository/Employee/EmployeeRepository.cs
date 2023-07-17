using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.Employee;
using MyDodos.ViewModel.HR;
using MySql.Data.MySqlClient;
using MyDodos.Repository.HR;
using MyDodos.Domain.HR;
using MyDodos.Domain.AuthBO;
using MyDodos.Domain.Employee;
using MyDodos.Domain.Master;
using MyDodos.ViewModel.Common;
using MyDodos.Domain.Payroll;

namespace MyDodos.Repository.Employee
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IOnBoardRepository _onBoardRepository;
        public EmployeeRepository(IConfiguration configuration,IOnBoardRepository onBoardRepository)
        {
            _configuration = configuration;
            _onBoardRepository = onBoardRepository;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public GetHRDirectoryList GetHRDirectoryList(GetHRDirectoryList inputParam)
        {
            GetHRDirectoryList objLeave = new GetHRDirectoryList();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", inputParam.objHRDirectoryInput.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", inputParam.objHRDirectoryInput.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@departmentId", inputParam.objHRDirectoryInput.DepartmentID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@isCompleted", inputParam.objHRDirectoryInput.IsCompleted, DbType.Boolean, ParameterDirection.Input);
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
                var query = "sp_GetHRDirectory";
                var data= SqlMapper.Query<GetHRDirectoryBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                objLeave.objHRDirectoryList = data;
                //objLeave.objMyLeaveListInput = inputParam.objMyLeaveListInput;
                objLeave.ServerSearchables = inputParam.ServerSearchables;
                if(objLeave.objHRDirectoryList.Count>0)
                {
                    objLeave.ServerSearchables.RecordsTotal = objLeave.objHRDirectoryList[0].TotalCount;
                    objLeave.ServerSearchables.RecordsFiltered = objLeave.objHRDirectoryList.Count();
                    //var emp = _onBoardRepository.GetEmployee()
                    // foreach (var item in rtnval.objOnboardList)
                    // {
                    //     var trans = GetBPTransName(item.EmpOnboardingID);       
                    //     item.TransStatus = trans;         
                    // } 
                }
            }
            conn.Close();
            conn.Dispose();
            return objLeave;
        }
        public List<HRDirectorySummeryBO> GetHRDirectorySummery(int TenantID,int LocationID,int DepartmentID)
        {
            List<HRDirectorySummeryBO> Msg = new List<HRDirectorySummeryBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Tenant_ID", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Department_ID", DepartmentID, DbType.String, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetHRDirectSummery";
                Msg = SqlMapper.Query<HRDirectorySummeryBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public Response<List<ManagerBO>> GetManagerList(int EmpID, int TenantID,int LocationID)
        {
            Response<List<ManagerBO>> response;            
            try
            {
            List<ManagerBO> obj = new List<ManagerBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Tenant_ID", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Emp_ID", EmpID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEmpMangerList";
                obj = SqlMapper.Query<ManagerBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            response = new Response<List<ManagerBO>>(obj, 200, "Data Retreived");
                
            }
            catch (Exception ex)
            {
                response = new Response<List<ManagerBO>>(ex.Message, 500);
            }
            return response;
        }
        public List<EmployeeView> GetUserView(int EmpID,int LoginID,int TenantID)
        {
            List<EmployeeView> Msg = new List<EmployeeView>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@loginId", LoginID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetUserView";
                Msg = SqlMapper.Query<EmployeeView>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public HRDirectoryEmpView GetEmployeeViewDetails(int EmpID,int LocationID,int TenantID)
        {
            HRDirectoryEmpView Msg = new HRDirectoryEmpView();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEmployeeViewDirectory";
                Msg = SqlMapper.Query<HRDirectoryEmpView>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public HREmpPersonalInfo GetEmployeePersonalInfo(int EmpID,int LocationID,int TenantID)
        {
            HREmpPersonalInfo Msg = new HREmpPersonalInfo();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEmployeeViewDirectory";
                Msg = SqlMapper.Query<HREmpPersonalInfo>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public HREmpAddress GetEmployeeAddressInfo(int EmpID)
        {
            HREmpAddress Msg = new HREmpAddress();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Employee_ID", EmpID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetHRAddress";
                Msg = SqlMapper.Query<HREmpAddress>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
         public List<BPTransNameBO> GetBPTransName(int EmpOnboardingID)
        {
            List<BPTransNameBO> rtnval = new List<BPTransNameBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empOnboardingId", EmpOnboardingID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetBPTransName";
                rtnval = SqlMapper.Query<BPTransNameBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
         public List<OnboardPersonalDetail> GetEmpOnBoard(int EmpID)
        {
            List<OnboardPersonalDetail> rtnval = new List<OnboardPersonalDetail>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEmpOnBoard";
                rtnval = SqlMapper.Query<OnboardPersonalDetail>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public InCompleteCountBO GetHRDirInCompleteCount(int TenantID,int LocationID)
        {
            InCompleteCountBO Msg = new InCompleteCountBO();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetHRDirInCompleteCount";
                Msg = SqlMapper.Query<InCompleteCountBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public List<EmpReportingBO> GetEmpReports(int EmpID)
        {
            List<EmpReportingBO> rtnval = new List<EmpReportingBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEmpReports";
                rtnval = SqlMapper.Query<EmpReportingBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<EmpDirectorBO> GetEmpDirects(int EmpID)
        {
            List<EmpDirectorBO> rtnval = new List<EmpDirectorBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEmpDirects";
                rtnval = SqlMapper.Query<EmpDirectorBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<EmpColleaguesBO> GetEmpColleagues(int EmpID)
        {
            List<EmpColleaguesBO> rtnval = new List<EmpColleaguesBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEmpColleagues";
                rtnval = SqlMapper.Query<EmpColleaguesBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<EmpReportingOrgBO> GetParentsOrgzChart(int EmpID, int ManagerID,  int LocationID,int TenantID)
        {
            List<EmpReportingOrgBO> rtnval = new List<EmpReportingOrgBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@managerId", ManagerID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEmpOrganizationHierarchy";
                rtnval = SqlMapper.Query<EmpReportingOrgBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<EmpReportingOrgBO> GetChaildsOrgzChart(int EmpID, int ManagerID,  int LocationID,int TenantID)
        {
            List<EmpReportingOrgBO> rtnval = new List<EmpReportingOrgBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@managerId", ManagerID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEmpOrganizationHierarchy";
                rtnval = SqlMapper.Query<EmpReportingOrgBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        // public List<EmpReportingOrgBO> GetIndividualOrgzChart(int EmpID, int ManagerID,  int LocationID,int TenantID)
        // {
        //     List<EmpReportingOrgBO> rtnval = new List<EmpReportingOrgBO>();
        //     DynamicParameters dyParam = new DynamicParameters();
        //     dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
        //     dyParam.Add("@managerId", ManagerID, DbType.Int32, ParameterDirection.Input);
        //     dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
        //     dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
        //     var conn = this.GetConnection();
        //     if (conn.State == ConnectionState.Closed)
        //     {
        //         conn.Open();
        //     }
        //     if (conn.State == ConnectionState.Open)
        //     {
        //         var query = "sp_GetEmpOrganizationHierarchy";
        //         rtnval = SqlMapper.Query<EmpReportingOrgBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
        //     }
        //     conn.Close();
        //     conn.Dispose();
        //     return rtnval;
        // }
        public List<EmpColleaguesOrgBO> GetIndividualOrgzChartss(int EmpID,int LocationID,int TenantID)
        {
            List<EmpColleaguesOrgBO> rtnval = new List<EmpColleaguesOrgBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEmpOrganizationHierarchy2";
                rtnval = SqlMapper.Query<EmpColleaguesOrgBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public OnBoardRequestModelMsg SaveEmployeeProfile(EmployeeProfileInputBO profile)
        {
            OnBoardRequestModelMsg Msg = new OnBoardRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@appUserId", profile.AppUserID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@firstName", profile.FirstName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@lastName", profile.LastName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@primaryEmail", profile.PrimaryEmail, DbType.String, ParameterDirection.Input);
            dyParam.Add("@secondaryEmail", profile.SecondaryEmail, DbType.String, ParameterDirection.Input);
            dyParam.Add("@contactPhone", profile.ContactPhone, DbType.String, ParameterDirection.Input);
            //dyParam.Add("@department", profile.Department, DbType.String, ParameterDirection.Input);
            //dyParam.Add("@userStatus", profile.UserStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@tenantId", profile.PrimaryCompanyID, DbType.Int32, ParameterDirection.Input);
            //dyParam.Add("@modifiedBy", profile.ModifiedBy, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_UpdateEmpProfile";
                Msg = SqlMapper.Query<OnBoardRequestModelMsg>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public EmployeeProfileInputBO GetEmployeeProfile(int AppUserID,int ProductID)
        {
            EmployeeProfileInputBO rtnval = new EmployeeProfileInputBO();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@appUserId", AppUserID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEmployeeProfile";
                rtnval = SqlMapper.Query<EmployeeProfileInputBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public TenantProfiledataBO GetTenantProfile(int TenantID,int ProductID)
        {
            TenantProfiledataBO rtnval = new TenantProfiledataBO();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetTenantProfile";
                rtnval = SqlMapper.Query<TenantProfiledataBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public TenantProfileImageBO GetTenantProfileImage(int TenantID,int ProductID)
        {
            TenantProfileImageBO rtnval = new TenantProfileImageBO();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetTenantProfile";
                rtnval = SqlMapper.Query<TenantProfileImageBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public OnBoardRequestModelMsg UpdateTenantProfile(TenantProfiledataBO profile)
        {
            OnBoardRequestModelMsg Msg = new OnBoardRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", profile.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@primaryPhone", profile.PrimaryPhone, DbType.String, ParameterDirection.Input);
            dyParam.Add("@email", profile.Email, DbType.String, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_UpdateTenantProfile";
                Msg = SqlMapper.Query<OnBoardRequestModelMsg>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public SaveOut SaveLocation(LocationdetBO objlocation)
        {
            SaveOut Msg = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", objlocation.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objlocation.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationCode", objlocation.LocationCode, DbType.String, ParameterDirection.Input);
            dyParam.Add("@locationName", objlocation.LocationName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@locationAddress1", objlocation.LocationAddress1, DbType.String, ParameterDirection.Input);
            dyParam.Add("@locationAddress2", objlocation.LocationAddress2, DbType.String, ParameterDirection.Input);
            dyParam.Add("@locationCity", objlocation.LocationCity, DbType.String, ParameterDirection.Input);
            dyParam.Add("@locationState", objlocation.LocationState, DbType.String, ParameterDirection.Input);
            dyParam.Add("@locationZip", objlocation.LocationZip, DbType.String, ParameterDirection.Input);
            dyParam.Add("@locationCountry", objlocation.LocationCountry, DbType.String, ParameterDirection.Input);
            dyParam.Add("@officePhoneNumber", objlocation.OfficePhoneNumber, DbType.String, ParameterDirection.Input);
            dyParam.Add("@createdBy", objlocation.CreatedBy, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveLoction";
                Msg = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public SaveOut SaveAccountDetails(AccountDetailsBO objaccount)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@empSalAccId", objaccount.EmpSalAccID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@empId", objaccount.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@accNumber", objaccount.AccNumber, DbType.String, ParameterDirection.Input);
            dyParam.Add("@bankName", objaccount.BankName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@branchName", objaccount.BranchName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@ifscCode", objaccount.IFSCCode, DbType.String, ParameterDirection.Input);
            dyParam.Add("@accType", objaccount.AccType, DbType.String, ParameterDirection.Input);
            dyParam.Add("@accStatus", objaccount.AccStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@pfNO", objaccount.PFNO, DbType.String, ParameterDirection.Input);
            dyParam.Add("@esiNO", objaccount.ESINO, DbType.String, ParameterDirection.Input);
            dyParam.Add("@createdBy", objaccount.CreatedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@modifiedBy", objaccount.ModifedBy, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveAccountDetails";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();                
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public AccountDetailsBO GetAccountDetails(int EmpID)
        {
            AccountDetailsBO Msg = new AccountDetailsBO();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetAccountDetails";
                Msg = SqlMapper.Query<AccountDetailsBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public List<vwPayrollUser> EmpPayrollSalaryMonth(int TenantID,int LocationID,int EmpID)
        {
            List<vwPayrollUser> Msg = new List<vwPayrollUser>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEmpPayrollSalaryMonth";
                Msg = SqlMapper.Query<vwPayrollUser>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public List<vwPayrollUser> GetPayHistory(int TenantID,int LocationID,int SalaryPeriodId)
        {
            List<vwPayrollUser> Msg = new List<vwPayrollUser>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@salaryPeriodId", SalaryPeriodId, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPayHistory";
                Msg = SqlMapper.Query<vwPayrollUser>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public int GetBenefitGroupByEmp(int EmpID)
        {
            int output = new int();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetBenefitGroupByEmp";
                output = SqlMapper.Query<int>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();            
            return output;
        }
        public List<EmpPersonalDetail> GetEmpDetails(int EmpID,int TenantID,int LocationID)
        {
            List<EmpPersonalDetail> obj = new List<EmpPersonalDetail>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEmployee";
                obj = SqlMapper.Query<EmpPersonalDetail>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return obj;
        }
        public List<vwProjectBO> GetEmpProjectList(int EmpID,int ProjectID,bool IsProjectManager)
        {
            List<vwProjectBO> obj = new List<vwProjectBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@projectId", ProjectID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@isProjectManager", IsProjectManager, DbType.Boolean, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetMyTeam";
                obj = SqlMapper.Query<vwProjectBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return obj;
        }
        public List<EmpPersonalDetail> GetProjectmanager(int EmpID,int ProjectID,bool IsProjectManager)
        {
            List<EmpPersonalDetail> obj = new List<EmpPersonalDetail>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@projectId", ProjectID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@isProjectManager", IsProjectManager, DbType.Boolean, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetMyTeam";
                obj = SqlMapper.Query<EmpPersonalDetail>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return obj;
        }
        public List<EntRoles> GetDesignation(int TenantID)
        {
            List<EntRoles> obj = new List<EntRoles>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetDesignation";
                obj = SqlMapper.Query<EntRoles>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return obj;
        }
        public SaveOut UpdateEmployeeDesignation(EmployeeInfoBO employee)
        {
            SaveOut rtnval = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId",employee.EmpID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@empNumber",employee.EmpNumber,DbType.String,ParameterDirection.Input);
            //dyParam.Add("@doj",employee.DOJ,DbType.DateTime,ParameterDirection.Input);
            //dyParam.Add("@officeEmail",employee.OfficeEmail,DbType.String,ParameterDirection.Input);
            //dyParam.Add("@phone",employee.PersonalMail,DbType.String,ParameterDirection.Input);
            dyParam.Add("@empStatus",employee.EmpStatus,DbType.String,ParameterDirection.Input);
            dyParam.Add("@empLocId",employee.EmpLocID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@departmentId",employee.DepartmentID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@managerId",employee.ManagerID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@funcRoleId",employee.FuncRoleID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@tenantId",employee.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@modifedBy",employee.ModifedBy,DbType.Int32,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_UpdateEmpDesignation";
                rtnval = SqlMapper.Query<SaveOut>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<EmpManagerDropdown> GetManagers(int TenantID,int LocationID,int ProjectID)
        {
            List<EmpManagerDropdown> objproject = new List<EmpManagerDropdown>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@projectId",ProjectID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetProjectManagerDropDown";
                objproject = SqlMapper.Query<EmpManagerDropdown>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objproject;
        }
    }
}