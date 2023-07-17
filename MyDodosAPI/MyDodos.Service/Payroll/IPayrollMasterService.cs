using System;
using System.Collections.Generic;
using MyDodos.Domain.Payroll;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.Common;
using MyDodos.ViewModel.Payroll;

namespace MyDodos.Service.Payroll
{
    public interface IPayrollMasterService
    {
        Response<List<PayrollSalaryStructDetails>> GetPayrollSalaryLineItem(int TenantID,int LocationID);
        Response<SaveOut> SavePayrolllSalaryLineItem(List<PayrollSalaryStructDetails> objPayroll);
        Response<SaveOut> DeletePayrollSalaryLineItem(int SalLineItemId);
        Response<SaveOut> SavePayrollMasCalculation(PayrollMasCalculationBO objPayroll);
        Response<PayrollrtnCycleBO> GetPayrollMasCycle(int TenantID, int LocationID);
        Response<SaveOut> SavePayrollCycle(PayrollCycleBO objPayroll);
        Response<SaveOut> DeletePayrollCycle(int PayrollCycleID);
        Response<List<PayrollMasCalculationBO>> GetPayrollCalcSetting(int TenantID,int LocationID,string PayrollTypes);
        Response<List<YearPFSpanValue>> GetOverallEPFSummary(int TenantID, int LocationID);
        Response<PayrollRtnEPFBO> GetPayrollEmpEPFSummary(int TenantID, int LocationID, int SalaryPeriodID);
        Response<SaveOut> SavePayrollEPFChanges(PayrollInputEPFBO objPayroll);
        Response<SaveOut> SavePayrollEPF(List<PayrollPFContribution> objPayroll);
        Response<SaveOut> SaveConsolePayrollEPFandESI(int TenantID, int LocationID, int SalaryPeriodID);
        Response<List<YearESISpanValue>> GetOverallESISummary(int TenantID, int LocationID);
        Response<PayrollRtnESIBO> GetPayrollEmpESISummary(int TenantID, int LocationID, int SalaryPeriodID);
        Response<SaveOut> SaveMasterSturcture(PayrolloverallStructDetails objPayroll);
        Response<SaveOut> SaveGroupTypeLineItem(GroupSalaryStructBO objPayroll);
        Response<SaveOut> SavePayrollMasterRules(PayrollrulesBO objPayroll);
        Response<List<GroupSalaryStructBO>> GetPayrollMasterStructure(int TenantID, int LocationID, int StructureID);
        Response<SaveOut> SavePayrolllOverAllLineItem(PayrolloverallStructDetails objPayroll);
        Response<SaveOut> SaveOrderLineItem(List<PayrolloverallStructDetails> objPayroll);
        Response<SaveOut> deletepayrollstruct(int StructureID);
        Response<SaveOut> deletepayrolliteamdetails(int SalLineItemDetID);
        Response<List<PayrollrulesBO>> GetPayRollRuleDropDown(int TenantID, int LocationID,int SalLineItemID);
        Response<PayrollRtrnStructLineDetails> GetPayrollSalaryStructLineDetails(int StructureID,int TenantID,int LocationID,bool isstanditeams);
        Response<List<GroupSalaryStructBO>> GetPayrollMasterSalaryStructure(int TenantID, int LocationID);
        Response<List<PayrolloverallStructDetails>> GetPayrollStructLine(int TenantID, int LocationID,int StructureID);
        Response<SaveOut> SavePayrolllOverAllStructureLineItem(PayrolloverallStructDetails objPayroll);
        Response<SaveOut> GetPayrollSalaryStructLineData(int StructureID,int TenantID,int LocationID,bool isstanditeams);        
    }
}