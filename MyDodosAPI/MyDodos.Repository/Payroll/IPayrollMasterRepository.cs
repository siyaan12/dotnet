using System;
using System.Collections.Generic;
using MyDodos.Domain.Payroll;
using MyDodos.ViewModel.Common;
using MyDodos.ViewModel.Payroll;

namespace MyDodos.Repository.Payroll
{
    public interface IPayrollMasterRepository
    {
        List<PayrollSalaryStructDetails> GetPayrollSalaryLineItem(int TenantID,int LocationID);
        SaveOut SavePayrolllSalaryLineItem(PayrollSalaryStructDetails objPayroll);
        SaveOut DeletePayrollSalaryLineItem(int SalLineItemId);
        SaveOut SavePayrollMasCalculation(PayrollMasCalculationBO objPayroll);
        List<PayrollMasCalculationBO> GetPayrollCalcSetting(int TenantID,int LocationID,string PayrollTypes);
        List<PayrollrtnCycleBO> GetPayrollCycle(int TenantID,int LocationID);
        SaveOut SavePayrollCycle(PayrollCycleBO objPayroll);
        SaveOut DeletePayrollCycle(int PayrollCycleID);
        List<PayrollPFContribution> GetPayrollEmpEPFSummary(int SalaryPeriodID);
        List<PayrollPFContributionSummary> GetPayrollEPFSummary(int TenantID,int LocationID, string YearSpan);
        List<YearPFSpanValue> GetOverallSalaryDetails(int TenantID, int LocationID);
        SaveOut SavePayrollEPFChanges(PayrollInputEPFBO objPayroll);
        SaveOut SavePayrollEPF(PayrollPFContribution objPayroll);
        List<PayrollPFContribution> GetPayrollEPFandESICalc(PayrollInputEPFBO objPayroll);
        List<PayrollESIContributionSummary> GetPayrollESISummary(int TenantID,int LocationID, string YearSpan);
        List<PayrollESIContribution> GetPayrollEmpESISummary(int SalaryPeriodID);
        List<YearESISpanValue> GetESISalaryDetails(int TenantID, int LocationID);
        SaveOut SaveGroupTypeLineItem(GroupSalaryStructBO objPayroll);
        SaveOut SaveMasterSturcture(PayrolloverallStructDetails objPayroll);
        List<GroupSalaryStructBO> GetPayrollMasterStructure(int TenantID, int LocationID, int StructureID);
        List<PayrolloverallStructDetails> GetPayrollStructLine(int StructureID,int TenantID,int LocationID,bool isstanditeams);
        SaveOut SavePayrollMasterRules(PayrollrulesBO objPayroll);
        List<PayrollrulesBO> GetPayrollRules(int TenantID, int LocationID, int SalLineItemID);
        SaveOut SavePayrollMasterLineIteam(PayrolloverallStructDetails objPayroll);
        SaveOut savestandardPayroll(int TenantID, int LocationID, int StructureID);
        SaveOut deletepayrolliteamdetails(int SalLineItemDetID);
        SaveOut deletepayrollstruct(int StructureID);
        string GetRuleSalaryValues(EmpSalaryStructureCTC objctc);
        List<HREmpBenefitsModel> getpayrollEmployeeBenefits(int EmpID, int BenefitGroupID);
    }
}