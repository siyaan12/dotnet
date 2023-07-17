using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.ViewModel.Common;
using MyDodos.ViewModel.Entitlement;
using MyDodos.ViewModel.HR;
using MyDodos.ViewModel.HRMS;
using MySql.Data.MySqlClient;

namespace MyDodos.Repository.HRMS
{
    public class HrmsInstanceRepository : IHrmsInstanceRepository
    {
        private readonly IConfiguration _configuration;
        public HrmsInstanceRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));            
            return conn;
        }
        public IDbConnection GetKOPRODConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("KOPRODADBConnection"));
            return conn;
        }
        public SaveOut SaveLocation(HRMSLocationBO objlocation)
        {
            SaveOut Msg = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", objlocation.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@entityId", objlocation.EntityID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@relId", objlocation.RelID, DbType.Int32, ParameterDirection.Input);
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
                var query = "sp_initsaveloction";
                Msg = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public SaveOut SaveMasDepartment(HRMSDepartmentBO department)
        {
            SaveOut rtnval = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@deptId",department.DeptID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@relId", department.RelID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@departmentCode",department.DepartmentCode,DbType.String,ParameterDirection.Input);
            dyParam.Add("@department",department.Department,DbType.String,ParameterDirection.Input);
            dyParam.Add("@deptHead",department.DeptHead,DbType.String,ParameterDirection.Input);
            dyParam.Add("@phone",department.Phone,DbType.String,ParameterDirection.Input);
            dyParam.Add("@fax",department.Fax,DbType.String,ParameterDirection.Input);
            dyParam.Add("@deptType",department.DeptType,DbType.String,ParameterDirection.Input);
            dyParam.Add("@deptShortName",department.DeptShortName,DbType.String,ParameterDirection.Input);
            dyParam.Add("@deptStatus",department.DeptStatus,DbType.String,ParameterDirection.Input);
            dyParam.Add("@entityId",department.EntityID,DbType.Int32,ParameterDirection.Input);
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
                var query = "sp_initsavedepartment";
                rtnval = SqlMapper.Query<SaveOut>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<HREmployeeBO> initGetEmployee(int EmpID, int LocationID)
        {
            List<HREmployeeBO> Msg = new List<HREmployeeBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_initGetEmployee";
                Msg = SqlMapper.Query<HREmployeeBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();            
            return Msg;
        }
        public SaveOut SaveConsoleLeaveJournal(LeaveJournalBO objJournal)
        {
            SaveOut rtnval = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", objJournal.EmpID, DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@yearId", objJournal.YearID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@categoryId", objJournal.CategoryID, DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@tenantId", objJournal.TenantID, DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId", objJournal.LocationID, DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@benefitGroupId", objJournal.BenefitGroupID, DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@entitytype", objJournal.EntityType, DbType.String,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveConsoleLeaveJournal";
                rtnval = SqlMapper.Query<SaveOut>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        // public void GetonbordToHRMS(OnBoardingGenralBO genral, AppUserDetailsBO AppUser)
        // {
        //     _ =  GetToHRMS(genral, AppUser);
        // }
        // public Tuple<OnBoardingGenralBO, AppUserDetailsBO> GetToHRMS(OnBoardingGenralBO genral, AppUserDetailsBO AppUser)
        // {
        //     return Tuple.Create(genral,AppUser);
        // }        
    }
}