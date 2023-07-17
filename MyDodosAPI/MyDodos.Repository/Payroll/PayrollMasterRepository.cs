using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Payroll;
using MyDodos.ViewModel.Common;
using MyDodos.ViewModel.Payroll;
using MySql.Data.MySqlClient;

namespace MyDodos.Repository.Payroll
{
    public class PayrollMasterRepository : IPayrollMasterRepository
    {
        private readonly IConfiguration _configuration;
        public PayrollMasterRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public List<PayrollSalaryStructDetails> GetPayrollSalaryLineItem(int TenantID,int LocationID)
        {
            List<PayrollSalaryStructDetails> output = new List<PayrollSalaryStructDetails>();
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
                var query = "sp_GetPayrollSalaryLineItem";
                output = SqlMapper.Query<PayrollSalaryStructDetails>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut SavePayrolllSalaryLineItem(PayrollSalaryStructDetails objPayroll)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@salLineItemId", objPayroll.SalLineItemID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@lineItem", objPayroll.LineItem, DbType.String, ParameterDirection.Input);
            dyParam.Add("@itemType", objPayroll.ItemType, DbType.String, ParameterDirection.Input);
            dyParam.Add("@itemorder", objPayroll.Itemorder, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@period", objPayroll.Period, DbType.String, ParameterDirection.Input);
            dyParam.Add("@lineItemKey", objPayroll.LineItemKey, DbType.String, ParameterDirection.Input);
            dyParam.Add("@calcType", objPayroll.CalcType, DbType.String, ParameterDirection.Input);
            dyParam.Add("@lineCalcType", objPayroll.LineCalcType, DbType.String, ParameterDirection.Input);
            dyParam.Add("@payrollLineCalcFormat", objPayroll.PayrollLineCalcFormat, DbType.String, ParameterDirection.Input);
            dyParam.Add("@lineItemStatus", objPayroll.LineItemStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@tenantId", objPayroll.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objPayroll.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@createdBy", objPayroll.CreatedBy, DbType.Int32, ParameterDirection.Input);
            
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SavePayrolllSalaryLineItem";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut DeletePayrollSalaryLineItem(int SalLineItemId)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@salLineItemId", SalLineItemId, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_DeletePayrollSalaryLineItem";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut SavePayrollMasCalculation(PayrollMasCalculationBO objPayroll)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@payrollMasCalcId", objPayroll.PayrollMasCalcID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@payrollMin", objPayroll.PayrollMin, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@payrollMax", objPayroll.PayrollMax, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@payrollValues", objPayroll.PayrollValues, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@payrollTypes", objPayroll.PayrollTypes, DbType.String, ParameterDirection.Input);
            dyParam.Add("@payrollCalcStatus", objPayroll.PayrollCalcStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@payrollCalcEntity", objPayroll.PayrollCalcEntity, DbType.String, ParameterDirection.Input);
            dyParam.Add("@payrollCalcRemarks", objPayroll.PayrollCalcRemarks, DbType.String, ParameterDirection.Input);
            dyParam.Add("@payrollCalcRange", objPayroll.PayrollCalcRange, DbType.String, ParameterDirection.Input);
            dyParam.Add("@payrollCalcFormat", objPayroll.PayrollCalcFormat, DbType.String, ParameterDirection.Input);
            dyParam.Add("@locationId", objPayroll.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objPayroll.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@createdBy", objPayroll.CreatedBy, DbType.Int32, ParameterDirection.Input);
            
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SavePayrollMasCalculation";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<PayrollMasCalculationBO> GetPayrollCalcSetting(int TenantID,int LocationID,string PayrollTypes)
        {
            List<PayrollMasCalculationBO> output = new List<PayrollMasCalculationBO>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@payrollTypes", PayrollTypes, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPayrollCalcSetting";
                output = SqlMapper.Query<PayrollMasCalculationBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }        
        public List<PayrollrtnCycleBO> GetPayrollCycle(int TenantID,int LocationID)
        {
            List<PayrollrtnCycleBO> output = new List<PayrollrtnCycleBO>();
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
                var query = "sp_GetPayrollCycle";
                output = SqlMapper.Query<PayrollrtnCycleBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut SavePayrollCycle(PayrollCycleBO objPayroll)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@payrollCycleId", objPayroll.PayrollCycleID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@payrollCycleType", objPayroll.PayrollCycleType, DbType.String, ParameterDirection.Input);
            dyParam.Add("@PayrollCycleStart", objPayroll.PayrollCycleStart, DbType.String, ParameterDirection.Input);
            dyParam.Add("@PayrollCycleEnd", objPayroll.PayrollCycleEnd, DbType.String, ParameterDirection.Input);
            dyParam.Add("@payrollCyclePayDay", objPayroll.PayrollCyclePayDay, DbType.String, ParameterDirection.Input);
            dyParam.Add("@payrollePFPayDay", objPayroll.PayrollEPFPayDay, DbType.String, ParameterDirection.Input);
            dyParam.Add("@payrolleSIPayDay", objPayroll.PayrollESIPayDay, DbType.String, ParameterDirection.Input);
            dyParam.Add("@payrollStructureMode", objPayroll.PayrollStructureMode, DbType.String, ParameterDirection.Input);
            dyParam.Add("@payrollCycleStatus", objPayroll.PayrollCycleStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@payrollMode", objPayroll.PayrollMode, DbType.String, ParameterDirection.Input);
            dyParam.Add("@locationId", objPayroll.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objPayroll.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@createdBy", objPayroll.CreatedBy, DbType.Int32, ParameterDirection.Input);
            
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SavePayrollCycle";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut DeletePayrollCycle(int PayrollCycleID)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@payrollMasCycleId", PayrollCycleID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_DeletePayrollCycle";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        /*Paroll EPF*/
        public List<PayrollPFContribution> GetPayrollEmpEPFSummary(int SalaryPeriodID)
        {
            List<PayrollPFContribution> output = new List<PayrollPFContribution>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@salaryPeriodID", SalaryPeriodID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPayrollEmpEPFSummary";
                output = SqlMapper.Query<PayrollPFContribution>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<PayrollPFContributionSummary> GetPayrollEPFSummary(int TenantID,int LocationID, string YearSpan)
        {
            List<PayrollPFContributionSummary> output = new List<PayrollPFContributionSummary>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@yearSpan", YearSpan, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPayrollEPFSummary";
                output = SqlMapper.Query<PayrollPFContributionSummary>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<YearPFSpanValue> GetOverallSalaryDetails(int TenantID, int LocationID)
        {
            List<YearPFSpanValue> output = new List<YearPFSpanValue>();
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
                output = SqlMapper.Query<YearPFSpanValue>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut SavePayrollEPFChanges(PayrollInputEPFBO objPayroll)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@pfContributionId", objPayroll.PFContributionID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@payableBasic", objPayroll.PayableBasic, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@grossSalary", objPayroll.GrossSalary, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@tenantId", objPayroll.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objPayroll.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@createdBy", objPayroll.CreatedBy, DbType.Int32, ParameterDirection.Input);
            
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SavePayrollEPFChanges";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut SavePayrollEPF(PayrollPFContribution objPayroll)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@pfContributionId", objPayroll.PFContributionID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@empId", objPayroll.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objPayroll.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@salaryPeriodId", objPayroll.SalaryPeriodID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objPayroll.LocationID, DbType.Int32, ParameterDirection.Input);            
            dyParam.Add("@basic", objPayroll.Basic, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@payableBasic", objPayroll.PayableBasic, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@empShareDue", objPayroll.EmpShareDue, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@totalEmpPf", objPayroll.TotalEmpPF, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@vPF", objPayroll.VPF, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@ePSScheme", objPayroll.EPSScheme, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@ePF", objPayroll.EPF, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@empShareDue", objPayroll.EmpShareDue, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@pFAdminCharges", objPayroll.PFAdminCharges, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@eDLI", objPayroll.EDLI, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@totalPF", objPayroll.TotalPF, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@nCPandlOPday", objPayroll.NCPandLOPday, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@amountPaid", objPayroll.AmountPaid, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@noOfDay", objPayroll.NoOfDay, DbType.Decimal, ParameterDirection.Input);            
            dyParam.Add("@grossSalary", objPayroll.GrossSalary, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@eSIeesCont", objPayroll.ESIEesCont, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@eSIersCont", objPayroll.ESIErsCont, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@pFDate", objPayroll.EPFDate, DbType.String, ParameterDirection.Input);
            dyParam.Add("@esiDate", objPayroll.ESIDate, DbType.String, ParameterDirection.Input);
            // dyParam.Add("@pFDate", objPayroll.PFDate.Date.ToString(), DbType.String, ParameterDirection.Input);
            // dyParam.Add("@esiDate", objPayroll.ESIDate.Date.ToString(), DbType.String, ParameterDirection.Input);
            dyParam.Add("@createdBy", objPayroll.CreatedBy, DbType.Int32, ParameterDirection.Input);
            
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SavePayrollEPF";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<PayrollPFContribution> GetPayrollEPFandESICalc(PayrollInputEPFBO objPayroll)
        {
            List<PayrollPFContribution> output = new List<PayrollPFContribution>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@empId", objPayroll.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@grossSalary", objPayroll.GrossSalary, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@payableBasic", objPayroll.PayableBasic, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objPayroll.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objPayroll.TenantID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPayrollEPFandESICalc";
                output = SqlMapper.Query<PayrollPFContribution>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<YearESISpanValue> GetESISalaryDetails(int TenantID, int LocationID)
        {
            List<YearESISpanValue> output = new List<YearESISpanValue>();
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
                output = SqlMapper.Query<YearESISpanValue>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<PayrollESIContribution> GetPayrollEmpESISummary(int SalaryPeriodID)
        {
            List<PayrollESIContribution> output = new List<PayrollESIContribution>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@salaryPeriodID", SalaryPeriodID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPayrollEmpESISummary";
                output = SqlMapper.Query<PayrollESIContribution>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<PayrollESIContributionSummary> GetPayrollESISummary(int TenantID,int LocationID, string YearSpan)
        {
            List<PayrollESIContributionSummary> output = new List<PayrollESIContributionSummary>();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@yearSpan", YearSpan, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPayrollESISummary";
                output = SqlMapper.Query<PayrollESIContributionSummary>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut SaveMasterSturcture(PayrolloverallStructDetails objPayroll)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@structureId", objPayroll.StructureID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@groupTypeId", objPayroll.GroupTypeID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@gradeId", objPayroll.GradeID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@roleCategory", objPayroll.RoleCategory, DbType.String, ParameterDirection.Input);
            dyParam.Add("@structureName", objPayroll.StructureName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@structureStatus", objPayroll.StructureStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@salaryMin", objPayroll.SalaryMin, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@salaryMax", objPayroll.SalaryMax, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@tenantId", objPayroll.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objPayroll.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@createdBy", objPayroll.CreatedBy, DbType.Int32, ParameterDirection.Input);
            
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveMasterSturcture";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut SaveGroupTypeLineItem(GroupSalaryStructBO objPayroll)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@structureId", objPayroll.StructureID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@groupTypeId", objPayroll.GroupTypeID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@structureIds", objPayroll.StructureIDs, DbType.String, ParameterDirection.Input);
            dyParam.Add("@tenantId", objPayroll.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objPayroll.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@createdBy", objPayroll.CreatedBy, DbType.Int32, ParameterDirection.Input);
            
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveGroupTypeLineItem";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<GroupSalaryStructBO> GetPayrollMasterStructure(int TenantID, int LocationID, int StructureID)
        {
            List<GroupSalaryStructBO> output = new List<GroupSalaryStructBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@structureId", StructureID, DbType.Int32, ParameterDirection.Input);
            
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPayrollMasterStructure";
                output = SqlMapper.Query<GroupSalaryStructBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<PayrolloverallStructDetails> GetPayrollStructLine(int StructureID,int TenantID,int LocationID,bool isstanditeams)
        {
            List<PayrolloverallStructDetails> output = new List<PayrolloverallStructDetails>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@structureId", StructureID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@isstanditeams", isstanditeams, DbType.Boolean, ParameterDirection.Input);
            
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPayrollStructLine";
                output = SqlMapper.Query<PayrolloverallStructDetails>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut SavePayrollMasterRules(PayrollrulesBO objPayroll)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@payrollRuleId", objPayroll.PayrollRuleID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@salLineItemId", objPayroll.SalLineItemID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@payrollRuleItemCode", objPayroll.PayrollRuleItemCode, DbType.String, ParameterDirection.Input);
            dyParam.Add("@payrollRuleForm", objPayroll.PayrollRuleForm, DbType.String, ParameterDirection.Input);
            dyParam.Add("@payrollRuleComponent", objPayroll.PayrollRuleComponent, DbType.String, ParameterDirection.Input);
            dyParam.Add("@payrollRuleMode", objPayroll.PayrollRuleMode, DbType.String, ParameterDirection.Input);
            dyParam.Add("@locationId", objPayroll.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objPayroll.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@createdBy", objPayroll.CreatedBy, DbType.Int32, ParameterDirection.Input);
            
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_savePayrollRules";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<PayrollrulesBO> GetPayrollRules(int TenantID, int LocationID, int SalLineItemID)
        {
            List<PayrollrulesBO> output = new List<PayrollrulesBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@salLineItemId", SalLineItemID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPayrollRules";
                output = SqlMapper.Query<PayrollrulesBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut SavePayrollMasterLineIteam(PayrolloverallStructDetails objPayroll)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@salLineItemDetId", objPayroll.SalLineItemDetID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@structureId", objPayroll.StructureID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@salaryLineItemId", objPayroll.salaryLineItemID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@LineCalcType", objPayroll.LineCalcType, DbType.String, ParameterDirection.Input);
            dyParam.Add("@salaryLineItem", objPayroll.salaryLineItem, DbType.String, ParameterDirection.Input);
            dyParam.Add("@salaryItemType", objPayroll.salaryItemType, DbType.String, ParameterDirection.Input);
            dyParam.Add("@salaryItemorder", objPayroll.salaryItemorder, DbType.String, ParameterDirection.Input);
            dyParam.Add("@salaryPeriod", objPayroll.salaryPeriod, DbType.String, ParameterDirection.Input);
            dyParam.Add("@isstanditeams", objPayroll.isstanditeams, DbType.Boolean, ParameterDirection.Input);
            dyParam.Add("@salaryLineItemKey", objPayroll.salaryLineItemKey, DbType.String, ParameterDirection.Input);
            dyParam.Add("@salaryCalcType", objPayroll.salaryCalcType, DbType.String, ParameterDirection.Input);
            dyParam.Add("@lineRangesCalcType", objPayroll.LineRangesCalcType, DbType.String, ParameterDirection.Input);
            dyParam.Add("@salaryRangeMin", objPayroll.SalaryRangeMin, DbType.String, ParameterDirection.Input);
            dyParam.Add("@salaryRangeMax", objPayroll.SalaryRangeMax, DbType.String, ParameterDirection.Input);
            dyParam.Add("@salarymainComponent", objPayroll.salarymainComponent, DbType.String, ParameterDirection.Input);
            dyParam.Add("@salaryotherComponent", objPayroll.salaryotherComponent, DbType.String, ParameterDirection.Input);
            dyParam.Add("@operateMode", objPayroll.operateMode, DbType.String, ParameterDirection.Input);
            dyParam.Add("@salaryLineItemStatus", objPayroll.StructureStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@salaryPayrollLineCalcFormat", objPayroll.salaryPayrollLineCalcFormat, DbType.String, ParameterDirection.Input);
            dyParam.Add("@tenantId", objPayroll.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objPayroll.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@createdBy", objPayroll.CreatedBy, DbType.Int32, ParameterDirection.Input);
            
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SavePayrollMasterLineIteam";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut savestandardPayroll(int TenantID, int LocationID, int StructureID)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@structureId", StructureID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_savestandardPayroll";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut deletepayrolliteamdetails(int SalLineItemDetID)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@salLineItemDetId", SalLineItemDetID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_deletepayrolllineiteamdetail";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public SaveOut deletepayrollstruct(int StructureID)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@structureId", StructureID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_deletepayrollstruct";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public string GetRuleSalaryValues(EmpSalaryStructureCTC objctc)
        {
            string output = string.Empty;
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@tenantId", objctc.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objctc.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@payrollrulecode", objctc.PayrollCode, DbType.String, ParameterDirection.Input);
            dyParam.Add("@empId", objctc.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@structureId", objctc.payrollStructID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetRuleSalaryValues";
                output = SqlMapper.Query<string>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<HREmpBenefitsModel> getpayrollEmployeeBenefits(int EmpID, int BenefitGroupID)
        {
            List<HREmpBenefitsModel> output = new List<HREmpBenefitsModel>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@benefitGroupId", BenefitGroupID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_getpayrollEmployeeBenefits";
                output = SqlMapper.Query<HREmpBenefitsModel>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
    }
}