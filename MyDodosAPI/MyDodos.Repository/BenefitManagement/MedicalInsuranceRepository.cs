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
using MyDodos.ViewModel.BenefitManagement;

namespace MyDodos.Repository.BenefitManagement
{
    public class MedicalInsuranceRepository : IMedicalInsuranceRepository
    {
        private readonly IConfiguration _configuration;
        public MedicalInsuranceRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public List<PlanTypeCategoryBO> GetPlanTypeCategory (int PlanTypeID,int TenantID,int LocationID)
        {
            List<PlanTypeCategoryBO> objplan = new List<PlanTypeCategoryBO>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@planTypeId", PlanTypeID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID,DbType.Int32,ParameterDirection.Input);
             var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPlanTypeCategory";
                objplan = SqlMapper.Query<PlanTypeCategoryBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objplan;
        }
        public SaveOut SaveHRBenefitPlans(HRBenefitPlansBO objplan)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@benePlanId", objplan.BenePlanID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objplan.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objplan.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@planName", objplan.PlanName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@provider", objplan.Provider, DbType.String, ParameterDirection.Input);
            dyParam.Add("@groupPolicyNumber", objplan.GroupPolicyNumber, DbType.String, ParameterDirection.Input);
            dyParam.Add("@isEnrollmentRequired", objplan.IsEnrollmentRequired, DbType.Boolean, ParameterDirection.Input);
            dyParam.Add("@enrollmentStart", objplan.EnrollmentStart, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@enrollmentEnd", objplan.EnrollmentEnd, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@yearId", objplan.YearID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@planStatus", objplan.PlanStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@deductibleInd", objplan.DeductibleInd, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@deductibleFamily", objplan.DeductibleFamily, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@outOfPocketInd", objplan.OutOfPocketInd, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@outOfPocketFamily", objplan.OutOfPocketFamily, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@planTypeId", objplan.PlanTypeID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@planType", objplan.PlanType, DbType.String, ParameterDirection.Input);
            dyParam.Add("@premiumInd", objplan.PremiumInd, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@premiumFamily", objplan.PremiumFamily, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@createdBy", objplan.CreatedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@modifiedBy", objplan.ModifiedBy, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveHRBenefitPlans";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<HRBenefitPlansBO> GetHRBenefitPlans(HRBenefitPlansBO objplan)
        {
            List<HRBenefitPlansBO> output = new List<HRBenefitPlansBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@planTypeId", objplan.PlanTypeID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objplan.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objplan.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@planStatus", objplan.PlanStatus, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetHRBenefitPlans";
                output = SqlMapper.Query<HRBenefitPlansBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut DeleteHRBenefitPlans(HRBenefitPlansBO objplan)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@benePlanId", objplan.BenePlanID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objplan.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objplan.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@planStatus", objplan.PlanStatus, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_DeleteHRBenefitPlans";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public HRBenefitPlansSearchBO GetHRBenefitPlansSearch(HRBenefitPlansSearchBO objplan)
        {
            HRBenefitPlansSearchBO output = new HRBenefitPlansSearchBO();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", objplan.objinput.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objplan.objinput.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@planStatus", objplan.objinput.PlanStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@searchData", objplan.ServerSearchables.search_data, DbType.String, ParameterDirection.Input);
            dyParam.Add("@pageNo", objplan.ServerSearchables.page_No, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@pageSize", objplan.ServerSearchables.page_Size, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@orderByColumn", objplan.ServerSearchables.orderBy_Column, DbType.String, ParameterDirection.Input);
            dyParam.Add("@orderBy", objplan.ServerSearchables.order_By, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetHRBenefitPlansSearch";
                var data= SqlMapper.Query<HRBenefitPlansOutputBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                output.objplanlist = data;
                output.objinput = objplan.objinput;
                output.ServerSearchables = objplan.ServerSearchables;
                if(output.objplanlist.Count > 0)
                {
                    output.ServerSearchables.RecordsTotal = output.objplanlist[0].TotalCount;
                }
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<HRBenefitPlansBO> GetHRBenefitPlansByEmp(int PlanTypeID,int TenantID,int LocationID)
        {
            List<HRBenefitPlansBO> output = new List<HRBenefitPlansBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@planTypeId", PlanTypeID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetMedicalInsurancePlansByEmp";
                output = SqlMapper.Query<HRBenefitPlansBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
    }
}