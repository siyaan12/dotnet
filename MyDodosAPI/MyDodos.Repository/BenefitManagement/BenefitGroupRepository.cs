using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Wrapper;
using MySql.Data.MySqlClient;
using MyDodos.ViewModel.Common;
using MyDodos.Domain.BenefitManagement;

namespace MyDodos.Repository.BenefitManagement
{
    public class BenefitGroupRepository : IBenefitGroupRepository
    {
        private readonly IConfiguration _configuration;
        public BenefitGroupRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public SaveOut SaveBenefitGroup(BenefitGroupBO objgroup)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@benefitGroupId", objgroup.BenefitGroupID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@groupName", objgroup.GroupName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@groupCategory", objgroup.GroupCategory, DbType.String, ParameterDirection.Input);
            dyParam.Add("@leaveGroupId", objgroup.LeaveGroupID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@benefitGroupStatus", objgroup.BenefitGroupStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@isBusinessCard", objgroup.IsBusinessCard, DbType.Boolean, ParameterDirection.Input);
            dyParam.Add("@isCorpCard", objgroup.IsCorpCard, DbType.Boolean, ParameterDirection.Input);
            dyParam.Add("@isGuestHouse", objgroup.IsGuestHouse, DbType.Boolean, ParameterDirection.Input);
            dyParam.Add("@isperks", objgroup.isPerks, DbType.Boolean, ParameterDirection.Input);
            dyParam.Add("@groupTypeId", objgroup.GroupTypeID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@groupType", objgroup.GroupType, DbType.String, ParameterDirection.Input);
            dyParam.Add("@tenantId", objgroup.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objgroup.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@reviewdBy", objgroup.ReviewedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@reviewedOn", objgroup.ReviewedOn, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@approvedBy", objgroup.ApprovedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@approvedOn", objgroup.ApprovedOn, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@createdBy", objgroup.CreatedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@modifiedBy", objgroup.ModifedBy, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveBenefitGroup";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut SaveBenefitMedPlan(BenefitGroupMedPlanBO objplan, int BenefitGroupID)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@benGrpMedPlanId", objplan.BenGrpMedPlanID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@benefitGroupId", BenefitGroupID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@medBenePlanId", objplan.MedBenePlanID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objplan.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objplan.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@createdBy", objplan.CreatedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@modifiedBy", objplan.ModifedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@isCheckList", objplan.IsCheckList, DbType.Boolean, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveBenefitGroupMedPlan";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<BenefitGroupBO> GetBenefitGroup(int BenefitGroupID, int TenantID, int LocationID)
        {
            List<BenefitGroupBO> output = new List<BenefitGroupBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@benefitGroupId", BenefitGroupID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetBenefitGroup";
                output = SqlMapper.Query<BenefitGroupBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<BenefitGroupMedPlanBO> GetBenefitMedPlan(int BenefitGroupID, int TenantID, int LocationID)
        {
            List<BenefitGroupMedPlanBO> output = new List<BenefitGroupMedPlanBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@benefitGroupId", BenefitGroupID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetBenefitGroupMedPlan";
                output = SqlMapper.Query<BenefitGroupMedPlanBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut DeleteBenefitGroup(int BenefitGroupID, int TenantID, int LocationID)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@benefitGroupId", BenefitGroupID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_DeleteBenefitGroup";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut DeleteBenefitMedPlan(int BenefitGroupID, int TenantID, int LocationID)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@benefitGroupId", BenefitGroupID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_DeleteBenefitGroupMedPlan";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
    }
}