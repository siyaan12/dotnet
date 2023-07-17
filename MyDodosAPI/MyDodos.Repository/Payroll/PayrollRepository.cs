using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.BenefitManagement;
using MyDodos.Domain.Holiday;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.Master;
using MyDodos.Domain.Payroll;
using MyDodos.Domain.TimeOff;
using MyDodos.ViewModel.Common;
using MyDodos.ViewModel.Payroll;
using MySql.Data.MySqlClient;

namespace MyDodos.Repository.Payroll
{
    public class PayrollRepository : IPayrollRepository
    {
        private readonly IConfiguration _configuration;
        public PayrollRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public PayrollCountBO GetCurrentMonth(int TenantID, int LocationID)
        {
            PayrollCountBO output = new PayrollCountBO();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetSalaryPeriodCurrentMonth";
                output = SqlMapper.Query<PayrollCountBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<BenefitGroupBO> GetBenefitGroupByGroupType(int TenantID, int GroupTypeID)
        {
            List<BenefitGroupBO> output = new List<BenefitGroupBO>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@groupTypeId", GroupTypeID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetBenefitGroupByGroupType";
                output = SqlMapper.Query<BenefitGroupBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<MasMedicalBenefitBO> GetMasMedicalBenefit(int TenantID, int LocationID)
        {
            List<MasMedicalBenefitBO> output = new List<MasMedicalBenefitBO>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetHRMasMedicalBenefit";
                output = SqlMapper.Query<MasMedicalBenefitBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut SaveOnBoardBenefits(OnBoardBenefitGroupBO objgroup)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", objgroup.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@benefitGroupId", objgroup.BenefitGroupID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@groupTypeId", objgroup.GroupTypeID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@leaveGroupId", objgroup.LeaveGroupID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objgroup.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objgroup.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@createdBy", objgroup.CreatedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@modifiedBy", objgroup.ModifiedBy, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveOnBoardBenefits";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut SaveOnBoardStruct(OnBoardBenefitGroupBO objgroup)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", objgroup.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@benefitGroupId", objgroup.BenefitGroupID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@groupTypeId", objgroup.GroupTypeID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@leaveGroupId", objgroup.LeaveGroupID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@structureId", objgroup.StructureID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objgroup.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objgroup.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@createdBy", objgroup.CreatedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@modifiedBy", objgroup.ModifiedBy, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveEmployeeStructData";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<OtherBenefitsBO> GetOtherBenefits(int BenefitGroupID, int TenantID, int LocationID)
        {
            List<OtherBenefitsBO> output = new List<OtherBenefitsBO>();
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
                var query = "sp_GetOtherBenefits";
                output = SqlMapper.Query<OtherBenefitsBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<EmployeeBenefitsBO> GetEmployeeBenefits(int EmpID, int TenantID, int LocationID)
        {
            List<EmployeeBenefitsBO> output = new List<EmployeeBenefitsBO>();
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
                var query = "sp_GetBenefitsByEmployee";
                output = SqlMapper.Query<EmployeeBenefitsBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<HREmpBenefitsModel> GetEmployeePayrollBenefit(int EmpID, int BenefitGroupID, string paycycle, int StructureID)
        {
            List<HREmpBenefitsModel> output = new List<HREmpBenefitsModel>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@benefitId", BenefitGroupID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@payCycle", paycycle, DbType.String, ParameterDirection.Input);
            dyParam.Add("@structureId", StructureID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEmployeePayrollByCycle";
                output = SqlMapper.Query<HREmpBenefitsModel>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<HREmpSalaryStructure> GetEmployeePayrollStructure(int EmpID, int BenefitGroupID, string paycycle, int StructureID)
        {
            List<HREmpSalaryStructure> output = new List<HREmpSalaryStructure>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@benefitId", BenefitGroupID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@payCycle", paycycle, DbType.String, ParameterDirection.Input);
            dyParam.Add("@structureId", StructureID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEmpPayrollStructByCycle";
                output = SqlMapper.Query<HREmpSalaryStructure>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut SaveEmployeeCTC(PayrollCTCBO objgroup)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", objgroup.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objgroup.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objgroup.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@benefitGroupId", objgroup.BenefitGroupID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@yearId", objgroup.YearID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@minCTC", objgroup.MinCTC, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@maxCTC", objgroup.MaxCTC, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@setCTC", objgroup.SetCTC, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@structureId", objgroup.StructureID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@empSalStrucId", objgroup.EmpSalStrucID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@notes", objgroup.ExceptionNotes, DbType.String, ParameterDirection.Input);
            dyParam.Add("@lineItemKey", objgroup.LineItemKey, DbType.String, ParameterDirection.Input);
            dyParam.Add("@createdBy", objgroup.CreatedBy, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveEmployeeSetCTC";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut SaveEmployeePayRollBenfits(PayrollCTCBO objgroup)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", objgroup.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objgroup.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objgroup.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@benefitGroupId", objgroup.BenefitGroupID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@yearId", objgroup.YearID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@minCTC", objgroup.MinCTC, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@maxCTC", objgroup.MaxCTC, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@grossSalary", objgroup.GrossSalary, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@setCTC", objgroup.SetCTC, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@notes", objgroup.ExceptionNotes, DbType.String, ParameterDirection.Input);
            dyParam.Add("@createdBy", objgroup.CreatedBy, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveEmpPayRollBenfits";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<YearSpanValue> GetOverallSalaryDetails(int TenantID, int LocationID)
        {
            List<YearSpanValue> output = new List<YearSpanValue>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetSalaryYearDetails";
                output = SqlMapper.Query<YearSpanValue>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<SalaryDetailsMonth> GetOverallSalaryYearDetails(int YearID, string YearSpan, int TenantID, int LocationID)
        {
            List<SalaryDetailsMonth> output = new List<SalaryDetailsMonth>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@yearId", YearID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@yearSpan", YearSpan, DbType.String, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetOverallSalaryDetails";
                output = SqlMapper.Query<SalaryDetailsMonth>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<SalaryPeriodBO> GetSalaryPeriod(int TenantID, int LocationID)
        {
            List<SalaryPeriodBO> result = new List<SalaryPeriodBO>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetSalaryYearDetails";
                var output = SqlMapper.Query<SalaryPeriodBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
                dyParam.Add("@yearId", output.YearID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@yearSpan", output.YearSpan, DbType.String, ParameterDirection.Input);
                dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);

                var query1 = "sp_GetHRsalaryPeriodDetails";
                result = SqlMapper.Query<SalaryPeriodBO>(conn, query1, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return result;
        }
        public List<RegPayRollBO> GetRegPayRoll(int SalaryPeriodID, int TenantID, int LocationID)
        {
            List<RegPayRollBO> output = new List<RegPayRollBO>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@salaryPeriodId", SalaryPeriodID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetRegPayRoll";
                output = SqlMapper.Query<RegPayRollBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<vwPayrollUser> GetPayRollEmployees(vwPayrollUser objpay)
        {
            List<vwPayrollUser> output = new List<vwPayrollUser>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@empId", objpay.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@salaryPeriodId", objpay.SalaryPeriodId, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objpay.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objpay.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@isExcel", objpay.IsExcel, DbType.Boolean, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPayrollUsers";
                output = SqlMapper.Query<vwPayrollUser>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<vwPayrollUser> GetPayRollUser(vwPayrollUser objpay)
        {
            List<vwPayrollUser> output = new List<vwPayrollUser>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@empId", objpay.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@salaryPeriodId", objpay.SalaryPeriodId, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objpay.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objpay.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@isExcel", objpay.IsExcel, DbType.Boolean, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPayrollUsers";
                output = SqlMapper.Query<vwPayrollUser>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<PayrollRtnTaxBO> GetPayrollPTaxandITax(PayrollIncomeandProfissonalBO objPayroll)
        {
            List<PayrollRtnTaxBO> output = new List<PayrollRtnTaxBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", objPayroll.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objPayroll.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@grossSalary", objPayroll.GrossSalary, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@payrollMin", objPayroll.PayrollMin, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@payrollMax", objPayroll.PayrollMax, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@payrollValues", objPayroll.PayrollValues, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@payrollCalcRange", objPayroll.PayrollCalcRange, DbType.String, ParameterDirection.Input);
            dyParam.Add("@payrollCalcEntity", objPayroll.PayrollCalcEntity, DbType.String, ParameterDirection.Input);
            dyParam.Add("@payrollCalcFormat", objPayroll.PayrollCalcFormat, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPayrollPTaxandITax";
                output = SqlMapper.Query<PayrollRtnTaxBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<PayrollIncomeandProfissonalBO> GetPayrollTypecalc(int TenantID, int LocationID)
        {
            List<PayrollIncomeandProfissonalBO> output = new List<PayrollIncomeandProfissonalBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPayrollTypecalc";
                output = SqlMapper.Query<PayrollIncomeandProfissonalBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut SavePayrollSetCTC(EmpSalaryStructureCTC objPayroll)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", objPayroll.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objPayroll.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objPayroll.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@benefitGroupId", objPayroll.BenefitGroupID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@setCTC", objPayroll.SetCTC, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@empSalStrucId", objPayroll.EmpSalStrucID, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@grossSalary", objPayroll.GrossSalary, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@professionalTax", objPayroll.ProfessionalTax, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@incomeTax", objPayroll.IncomeTax, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@payrollLineCalcFormat", objPayroll.PayrollLineCalcFormat, DbType.String, ParameterDirection.Input);
            dyParam.Add("@salaryRangeMin", objPayroll.SalaryRangeMin, DbType.String, ParameterDirection.Input);
            dyParam.Add("@salaryRangeMax", objPayroll.SalaryRangeMax, DbType.String, ParameterDirection.Input);
            dyParam.Add("@payrollcode", objPayroll.PayrollCode, DbType.String, ParameterDirection.Input);
            dyParam.Add("@ratevalues", objPayroll.Ratevalues, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@lineItemKey", objPayroll.LineItemKey, DbType.String, ParameterDirection.Input);
            dyParam.Add("@createdBy", objPayroll.CreatedBy, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SavePayrollSetCTC";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SalaryAdjustmentsBO SaveSalaryAdjustments(SalaryAdjustmentsBO objpay)
        {
            SalaryAdjustmentsBO output = new SalaryAdjustmentsBO();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@adjustmentId", objpay.AdjustmentID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@adjustmentLineItemId", objpay.AdjustmentLineItemID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@adjustmentType", objpay.AdjustmentType, DbType.String, ParameterDirection.Input);
            dyParam.Add("@adjustmentLineItem", objpay.AdjustmentLineItem, DbType.String, ParameterDirection.Input);
            dyParam.Add("@amount", objpay.Amount, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@adjustmentMode", objpay.AdjustmentMode, DbType.String, ParameterDirection.Input);
            dyParam.Add("@existingAmount", objpay.ExistingAmount, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@adjustmentComments", objpay.AdjustmentComments, DbType.String, ParameterDirection.Input);
            dyParam.Add("@empId", objpay.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@salaryPeriodId", objpay.SalaryPeriodID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@changedAmount", objpay.ChangedAmount, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@tenantId", objpay.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objpay.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@createdBy", objpay.CreatedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@modifiedBy", objpay.ModifedBy, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveSalaryAdjustments";
                output = SqlMapper.Query<SalaryAdjustmentsBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<RegPayRollBO> GetRegPayRollBackUP(int SalaryPeriodID, int TenantID, int LocationID)
        {
            List<RegPayRollBO> output = new List<RegPayRollBO>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@salaryPeriodId", SalaryPeriodID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetRegPayRollByBackUP";
                output = SqlMapper.Query<RegPayRollBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<vwPayrollUser> GetPayrollSalaryMonth(int TenantID, int LocationID, int SalaryPeriodID)
        {
            List<vwPayrollUser> output = new List<vwPayrollUser>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@salaryPeriodId", SalaryPeriodID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPayrollSalaryMonth";
                output = SqlMapper.Query<vwPayrollUser>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut SaveSalaryMainDetails(RegPayRollBO objreg)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@empTotals", objreg.EmpTotals, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@attendanceClosingDate", objreg.AttendanceClosingDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@payDate", objreg.PayDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@totalAdjustments", objreg.TotalAdjustments, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@datePeriod", objreg.DatePeriod, DbType.String, ParameterDirection.Input);
            dyParam.Add("@payrollCost", objreg.PayrollCost, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@professionTaxAmount", objreg.ProfessionTaxAmount, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@netPay", objreg.NetPay, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@incomeTaxAmount", objreg.IncomeTaxAmount, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@totalDeductionAmount", objreg.TotalDeductionAmount, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@totalTax", objreg.TotalTax, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@status", objreg.Status, DbType.String, ParameterDirection.Input);
            dyParam.Add("@salaryPeriodId", objreg.SalaryPeriodID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveSalaryMainDetails";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<SalaryAdjustmentsBO> GetSalaryAdjust(int EmpID, int SalaryPeriodID)
        {
            List<SalaryAdjustmentsBO> output = new List<SalaryAdjustmentsBO>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@salaryPeriodId", SalaryPeriodID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@empID", EmpID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetSalaryAdjust";
                output = SqlMapper.Query<SalaryAdjustmentsBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SalaryPeriodBO GetSPSalaryPeriod(int SalaryPeriodID)
        {
            SalaryPeriodBO output = new SalaryPeriodBO();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@salaryPeriodId", SalaryPeriodID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_StatusGet";
                output = SqlMapper.Query<SalaryPeriodBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public GetLOPBO PayrollLOP(int SalaryPeriodId, int TenantID, int LocationID, int EmpID)
        {
            GetLOPBO output = new GetLOPBO();

            List<string> objstring = new List<string>();
            objstring.Add("Leave");
            objstring.Add("TimeOff");
            objstring.Add("Holiday");

            foreach (var obj in objstring)
            {
                if (obj == "Leave")
                {
                    var LOPValue = GetPayrollLOPs(SalaryPeriodId, TenantID, LocationID, EmpID, obj);
                    output.LOPDays = LOPValue.NoofDays;
                    output.TotalDays = LOPValue.TotalDays;
                }
                if (obj == "TimeOff")
                {
                    var LOPValue = GetPayrollLOPs(SalaryPeriodId, TenantID, LocationID, EmpID, obj);
                    output.TimeoffDays = LOPValue.NoofDays;
                    output.TotalDays = LOPValue.TotalDays;
                }
                if (obj == "Holiday")
                {
                    var LOPValue = GetPayrollLOPs(SalaryPeriodId, TenantID, LocationID, EmpID, obj);
                    output.Holidays = LOPValue.NoofDays;
                    output.TotalDays = LOPValue.TotalDays;
                }
            }
            decimal Total = decimal.Zero;
            decimal IsLOP = decimal.Zero;
            Total = output.LOPDays + output.TimeoffDays;
            if (Total > decimal.Zero)
            {
                IsLOP = Total;
            }
            else
            {
                IsLOP = 0;
            }
            output.PaidDays = output.TotalDays - IsLOP;
            output.LOPDays = IsLOP;

            return output;
        }
        public GetLOPBO GetPayrollLOPs(int SalaryPeriodId, int TenantID, int LocationID, int EmpID, string IsLOP)
        {
            GetLOPBO output = new GetLOPBO();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@salaryPeriodId", SalaryPeriodId, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@isLOP", IsLOP, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPayrollLOP";
                output = SqlMapper.Query<GetLOPBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut SaveEmpDetails(vwPayrollUser objuser)
        {
            SaveOut output = new SaveOut();

            var payvalue = CalculateLOP(objuser.SalaryPeriodId, objuser.TenantID, objuser.LocationID, objuser.AppUserId);
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@appUserId", objuser.AppUserId, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@benefitGroupId", objuser.BenefitGroupID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@empId", objuser.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@empNumber", objuser.EmpNumber, DbType.String, ParameterDirection.Input);
            dyParam.Add("@empStatus", objuser.EmpStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@empType", objuser.EmpType, DbType.String, ParameterDirection.Input);
            dyParam.Add("@firstName", objuser.FirstName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@lastName", objuser.LastName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@managerId", objuser.ManagerID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@netPayAmount", objuser.NetPayAmount, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@officeEmail", objuser.OfficeEmail, DbType.String, ParameterDirection.Input);
            dyParam.Add("@paidDays", payvalue.PaidDays, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@totalDays", payvalue.TotalDays, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@holidays", payvalue.Holidays, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@lopDays", payvalue.LOPDays, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@salaryPeriodId", objuser.SalaryPeriodId, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@grossSalary", objuser.GrossSalary, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@basicSalary", objuser.BasicSalary, DbType.Decimal, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveEmpAddDetailsByMonth";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut SaveEmpSalaryStructDetails(int EmpID, int SalaryPeriodID)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@salaryPeriodId", SalaryPeriodID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveEmpSalaryStructDetails";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public PayrollEntAppUserBO GetPayrollEntAppUser(int EmpID, int TenantID, int LocationID)
        {
            PayrollEntAppUserBO output = new PayrollEntAppUserBO();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@empID", EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPayrollEntAppUser";
                output = SqlMapper.Query<PayrollEntAppUserBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public GetLOPBO CalculateLOP(int SalaryPeriodId, int TenantID, int LocationID, int AppUserID)
        {
            GetLOPBO output = new GetLOPBO();
            var empdetails = GetEmployeeDetailsPayroll(AppUserID);
            string sDate1 = Convert.ToString(empdetails.DOJ);
            DateTime datevalue1 = (Convert.ToDateTime(sDate1.ToString()));

            var period1 = GetSPSalaryPeriod(SalaryPeriodId);

            String sDate2 = Convert.ToString(period1.PeriodTo);
            DateTime datevalue2 = (Convert.ToDateTime(sDate2.ToString()));
            

            if(datevalue1.Year.ToString() == datevalue2.Year.ToString() && datevalue1.Month.ToString() == datevalue2.Month.ToString())
            {
                List<string> objstring1 = new List<string>();
                objstring1.Add("Leave");
                objstring1.Add("TimeOff");
                objstring1.Add("Holiday");

            var valuedates = GetLOPDates(SalaryPeriodId, TenantID, LocationID);
            var datesvalue = GetEndDate(TenantID,LocationID,datevalue1);
            List<DateTime> perioddate = getDates(valuedates.StartDate, valuedates.EndDate);
            List<DateTime> valuedate = getDates(datesvalue.StartDate, datesvalue.EndDate);
            List<DateTime> resdate = new List<DateTime>();
            foreach (var item in objstring1)
            {
                if (item == "Leave")
                {
                    resdate.Clear();
                    var lop = GetLeaveLOP(AppUserID, valuedates.StartDate, valuedates.EndDate, item, TenantID, LocationID);
                    decimal NoofDays = lop.Sum(a => a.LOPCount);
                    foreach (var data in lop)
                    {
                        resdate = getDates(Convert.ToDateTime(data.LeaveFrom), Convert.ToDateTime(data.LeaveTo));
                    }
                    var expdate = resdate.Except(perioddate).ToList();
                    if (expdate.Count() != 0)
                    {
                        output.LOPDays = NoofDays - Convert.ToDecimal(expdate.Count());
                    }
                    else
                    {
                        output.LOPDays = NoofDays;
                    }
                }
                if (item == "TimeOff")
                {
                    resdate.Clear();
                    var lop = GetTimeoffLOP(AppUserID, valuedates.StartDate, valuedates.EndDate, item, TenantID, LocationID);
                    decimal NoofDays = lop.Sum(a => a.NoOfDays);
                    foreach (var data in lop)
                    {
                        resdate = getDates(Convert.ToDateTime(data.LeaveFrom), Convert.ToDateTime(data.LeaveTo));
                    }
                    var expdate = resdate.Except(perioddate).ToList();
                    if (expdate.Count() != 0)
                    {
                        output.TimeoffDays = NoofDays - Convert.ToDecimal(expdate.Count());
                    }
                    else
                    {
                        output.TimeoffDays = NoofDays;
                    }
                }
                if (item == "Holiday")
                {
                    var hdays = GetHolidayLOP(AppUserID, valuedates.StartDate, valuedates.EndDate, item, TenantID, LocationID);
                    output.Holidays = hdays.Count();
                }
            }
            output.TotalDays = perioddate.Count();
            output.PaidDays =valuedate.Count();
            output.PaidDays = output.PaidDays - (output.LOPDays + output.TimeoffDays);
            var TimeoffDaysValue = output.LOPDays + output.TimeoffDays;
            output.TimeoffDays = TimeoffDaysValue;
            }
            else
            {
            //var period = GetSPSalaryPeriod(SalaryPeriodId);
            List<string> objstring = new List<string>();
            objstring.Add("Leave");
            objstring.Add("TimeOff");
            objstring.Add("Holiday");

            var datevalue = GetLOPDates(SalaryPeriodId, TenantID, LocationID);

            List<DateTime> perioddates = getDates(datevalue.StartDate, datevalue.EndDate);
            List<DateTime> resdates = new List<DateTime>();
            foreach (var item in objstring)
            {
                if (item == "Leave")
                {
                    resdates.Clear();
                    var lop = GetLeaveLOP(AppUserID, datevalue.StartDate, datevalue.EndDate, item, TenantID, LocationID);
                    decimal NoofDays = lop.Sum(a => a.NoOfDays);
                    foreach (var data in lop)
                    {
                        resdates = getDates(Convert.ToDateTime(data.LeaveFrom), Convert.ToDateTime(data.LeaveTo));
                    }
                    var expdate = resdates.Except(perioddates).ToList();
                    if (expdate.Count() != 0)
                    {
                        output.LOPDays = NoofDays - Convert.ToDecimal(expdate.Count());
                    }
                    else
                    {
                        output.LOPDays = NoofDays;
                    }
                }
                if (item == "TimeOff")
                {
                    resdates.Clear();
                    var lop = GetTimeoffLOP(AppUserID, datevalue.StartDate, datevalue.EndDate, item, TenantID, LocationID);
                    decimal NoofDays = lop.Sum(a => a.NoOfDays);
                    foreach (var data in lop)
                    {
                        resdates = getDates(Convert.ToDateTime(data.LeaveFrom), Convert.ToDateTime(data.LeaveTo));
                    }
                    var expdate = resdates.Except(perioddates).ToList();
                    if (expdate.Count() != 0)
                    {
                        output.TimeoffDays = NoofDays - Convert.ToDecimal(expdate.Count());
                    }
                    else
                    {
                        output.TimeoffDays = NoofDays;
                    }
                }
                if (item == "Holiday")
                {
                    var hdays = GetHolidayLOP(AppUserID, datevalue.StartDate, datevalue.EndDate, item, TenantID, LocationID);
                    output.Holidays = hdays.Count();
                }
            }
            
            output.TotalDays = perioddates.Count();
            output.PaidDays = output.TotalDays - (output.LOPDays + output.TimeoffDays);
            var TimeoffDaysValue = output.LOPDays + output.TimeoffDays;
            output.TimeoffDays = TimeoffDaysValue;
            }
            return output;
            
        }
        public DatesLOP GetLOPDates(int SalaryPeriodID, int TenantID, int LocationID)
        {
            DatesLOP output = new DatesLOP();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@salaryPeriodId", SalaryPeriodID, DbType.String, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetDatesLOP";
                output = SqlMapper.Query<DatesLOP>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public DatesLOP GetEndDate(int TenantID, int LocationID,DateTime DOJ)
        {
            DatesLOP output = new DatesLOP();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@doj", DOJ, DbType.DateTime, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEndDate";
                output = SqlMapper.Query<DatesLOP>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<DateTime> getDates(DateTime StartDate, DateTime EndDate)
        {
            List<DateTime> dates = new List<DateTime>();
            dates.Clear();
            for (DateTime date = StartDate; date <= EndDate; date = date.AddDays(1))
            {
                dates.Add(date);
            }
            return dates;
        }
        public List<LeaveRequestModel> GetLeaveLOP(int AppUserID, DateTime StartDate, DateTime EndDate, string IsLOP, int TenantID, int LocationID)
        {
            List<LeaveRequestModel> output = new List<LeaveRequestModel>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@appUserId", AppUserID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@startDate", StartDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@endDate", EndDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@isLOP", IsLOP, DbType.String, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetLOPByUser";
                output = SqlMapper.Query<LeaveRequestModel>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<TimeoffRequestModel> GetTimeoffLOP(int AppUserID, DateTime StartDate, DateTime EndDate, string IsLOP, int TenantID, int LocationID)
        {
            List<TimeoffRequestModel> output = new List<TimeoffRequestModel>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@appUserId", AppUserID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@startDate", StartDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@endDate", EndDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@isLOP", IsLOP, DbType.String, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetLOPByUser";
                output = SqlMapper.Query<TimeoffRequestModel>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<HolidayBO> GetHolidayLOP(int AppUserID, DateTime StartDate, DateTime EndDate, string IsLOP, int TenantID, int LocationID)
        {
            List<HolidayBO> output = new List<HolidayBO>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@appUserId", AppUserID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@startDate", StartDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@endDate", EndDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@isLOP", IsLOP, DbType.String, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetLOPByUser";
                output = SqlMapper.Query<HolidayBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<HREmpSalaryStructure> GetEmpPayrolBycycle(int EmpID, int StructureID, string paycycle)
        {
            List<HREmpSalaryStructure> output = new List<HREmpSalaryStructure>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@structureId", StructureID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@payCycle", paycycle, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEmpPayrollStructByID";
                output = SqlMapper.Query<HREmpSalaryStructure>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<BankExcelValuesBO> GetBankExcelDetails(BankExcelInputBO objinp)
        {
            List<BankExcelValuesBO> output = new List<BankExcelValuesBO>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@tenantId", objinp.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objinp.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@salaryPeriodId", objinp.SalaryPeriodID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@purpose", objinp.Purpose, DbType.String, ParameterDirection.Input);
            dyParam.Add("@empId", objinp.EmpID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetBankDetailsforExcel";
                output = SqlMapper.Query<BankExcelValuesBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public EmployeeSalaryExcelBO GetSalaryEmployeesExcelDetails(BankExcelInputBO objinp, int EmpID)
        {
            EmployeeSalaryExcelBO output = new EmployeeSalaryExcelBO();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@tenantId", objinp.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objinp.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@salaryPeriodId", objinp.SalaryPeriodID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@purpose", objinp.Purpose, DbType.String, ParameterDirection.Input);
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetBankDetailsforExcel";
                output = SqlMapper.Query<EmployeeSalaryExcelBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<EmployeeBenefitsBO> GetPayrollEmployeesExcel(int TenantID, int LocationID)
        {
            List<EmployeeBenefitsBO> output = new List<EmployeeBenefitsBO>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPayrollEmployees";
                output = SqlMapper.Query<EmployeeBenefitsBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SalaryPeriodBO UpdateSalaryPeriodStatus(SalaryPeriodStatusBO objinp)
        {
            SalaryPeriodBO output = new SalaryPeriodBO();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@tenantId", objinp.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objinp.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@salaryPeriodId", objinp.SalaryPeriodID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@processStatus", objinp.ProcessStatus, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_UpdateSalaryPeriodStatus";
                output = SqlMapper.Query<SalaryPeriodBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut UpdateNetPayAmount(int SalaryPeriodId, int TotalDays, int PaidDays, int AppUserId)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@salaryPeriodId", SalaryPeriodId, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@totalDays", TotalDays, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@paidDays", PaidDays, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@appUserId", AppUserId, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetLOPDeductions";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<MasYearDetailsBO> GetMasYearTenant(int TenantID, int LocationID)
        {
            List<MasYearDetailsBO> output = new List<MasYearDetailsBO>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetMasYearTenant";
                output = SqlMapper.Query<MasYearDetailsBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<SalaryPeriodBO> GetSalaryPeriodMonths(int TenantID, int LocationID, int YearID)
        {
            List<SalaryPeriodBO> output = new List<SalaryPeriodBO>();
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
                var query = "sp_GetSalaryPeriod";
                output = SqlMapper.Query<SalaryPeriodBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public EmployeeBenefitsBO GetEmployeeDetailsPayroll(int AppUserID)
        {
            EmployeeBenefitsBO output = new EmployeeBenefitsBO();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@appUserId", AppUserID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEmployeeDetailsPayroll";
                output = SqlMapper.Query<EmployeeBenefitsBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut SavePayrollDynamicSetCTC(EmpSalaryStructureCTC objPayroll)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", objPayroll.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objPayroll.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objPayroll.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@benefitGroupId", objPayroll.BenefitGroupID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@setCTC", objPayroll.SetCTC, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@empSalStrucId", objPayroll.EmpSalStrucID, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@grossSalary", objPayroll.GrossSalary, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@professionalTax", objPayroll.ProfessionalTax, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@incomeTax", objPayroll.IncomeTax, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@payrollLineCalcFormat", objPayroll.PayrollLineCalcFormat, DbType.String, ParameterDirection.Input);
            dyParam.Add("@payrollCode", objPayroll.PayrollCode, DbType.String, ParameterDirection.Input);
            dyParam.Add("@payrollValues", objPayroll.PayrollValues, DbType.String, ParameterDirection.Input);
            dyParam.Add("@lineItemKey", objPayroll.LineItemKey, DbType.String, ParameterDirection.Input);
            dyParam.Add("@createdBy", objPayroll.CreatedBy, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SavePayrollDynamicSETCTC";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<EmployeeBenefitsBO> GetPayslipEmployees(int TenantID, int LocationID)
        {
            List<EmployeeBenefitsBO> output = new List<EmployeeBenefitsBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPayslipEmployees";
                output = SqlMapper.Query<EmployeeBenefitsBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
    }
}