using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.BenefitManagement;
using MyDodos.Domain.Wrapper;
using MySql.Data.MySqlClient;
using MyDodos.ViewModel.Common;

namespace MyDodos.Repository.BenefitManagement
{
    public class LeaveBenefitRepository : ILeaveBenefitRepository
    {
        private readonly IConfiguration _configuration;
        public LeaveBenefitRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public SaveOut SaveMasLeaveGroup(MasLeaveGroupBO objgroup)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@leaveGroupId", objgroup.LeaveGroupID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@leaveGroupName", objgroup.LeaveGroupName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@leaveGroupCategory", objgroup.LeaveGroupCategory, DbType.String, ParameterDirection.Input);
            dyParam.Add("@groupTypeId", objgroup.GroupTypeID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objgroup.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objgroup.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@leaveGroupStatus", objgroup.LeaveGroupStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@createdBy", objgroup.CreatedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@modifiedBy", objgroup.ModifiedBy, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveMasLeaveGroup";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<MasLeaveGroupBO> GetMasLeaveGroup(int LeaveGroupID, int TenantID, int LocationID)
        {
            List<MasLeaveGroupBO> output = new List<MasLeaveGroupBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@leaveGroupId", LeaveGroupID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetMasLeaveGroup";
                output = SqlMapper.Query<MasLeaveGroupBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<MasLeaveGroupBO> GetMasLeaveGroupByGroupType(int GroupTypeID, int TenantID, int LocationID)
        {
            List<MasLeaveGroupBO> output = new List<MasLeaveGroupBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@grouptypeId", GroupTypeID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetMasLeaveGroupByGroupType";
                output = SqlMapper.Query<MasLeaveGroupBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut DeleteMasLeaveGroup(int LeaveGroupID, int TenantID, int LocationID)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@leaveGroupId", LeaveGroupID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_DeleteMasLeaveGroup";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut SaveLeaveAllocReference(LeaveAllocReferenceBO objref, int LeaveGroupID)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@leaveAllocationId", objref.LeaveAllocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@leaveGroupId", LeaveGroupID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@categoryId", objref.CategoryID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@noOfLeave", objref.NoOfLeave, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@allocationPeriod", objref.AllocationPeriod, DbType.String, ParameterDirection.Input);
            dyParam.Add("@isRollOver", objref.isRollOver, DbType.Boolean, ParameterDirection.Input);
            dyParam.Add("@accureAtEnd", objref.AccureAtEnd, DbType.Boolean, ParameterDirection.Input);
            dyParam.Add("@notes", objref.Notes, DbType.String, ParameterDirection.Input);
            dyParam.Add("@tenantId", objref.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objref.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@createdBy", objref.CreatedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@modifiedBy", objref.ModifiedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@carryForward", objref.CarryForward, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveHRLeaveAllocReference";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<LeaveAllocReferenceBO> GetLeaveAllocReference(int LeaveGroupID, int TenantID, int LocationID)
        {
            List<LeaveAllocReferenceBO> output = new List<LeaveAllocReferenceBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@leaveGroupId", LeaveGroupID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetHRLeaveAllocReference";
                output = SqlMapper.Query<LeaveAllocReferenceBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut DeleteLeaveAllocReference(int LeaveGroupID, int TenantID, int LocationID)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@leaveGroupId", LeaveGroupID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_DeleteHRLeaveAllocReference";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
    }
}