using System;
using System.Collections.Generic;
using KoSoft.DocRepo;
using MyDodos.Domain.HR;
using MyDodos.Domain.Payroll;

namespace MyDodos.ViewModel.Payroll
{
    public class PayrollSalaryStructDetails
    {
        public int SalLineItemID { get; set; }
        public string LineItem { get; set; }
        public string ItemType { get; set; }
        public int Itemorder { get; set; }
        public string Period { get; set; }
        public string LineItemKey { get; set; }
        public string CalcType { get; set; }
        public string LineCalcType { get; set; }
        public string PayrollLineCalcFormat { get; set; }
        public string LineItemStatus { get; set; }
        public int CreatedBy { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public string SalaryRangeMin { get; set; }
        public string SalaryRangeMax { get; set; }

    }
    public class PayrollMasCalculationBO
    {
        public int PayrollMasCalcID { get; set; }
        public decimal PayrollMin { get; set; }
        public decimal PayrollMax { get; set; }
        public decimal PayrollValues { get; set; }
        public string PayrollTypes { get; set; }
        public string PayrollCalcStatus { get; set; }
        public string PayrollCalcEntity { get; set; }
        public string PayrollCalcRemarks { get; set; }
        public string PayrollCalcRange { get; set; }
        public string PayrollCalcFormat { get; set; }
        public int CreatedBy { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
    }
    public class PayrollMasCycleBO
    {
        public int PayrollCycleID { get; set; }
        public string PayrollCycleType { get; set; }
        public string PayrollCycleMonth { get; set; }
        public string PayrollCyclePayDay { get; set; }
        public string PayrollCycleStatus { get; set; }
        public int CreatedBy { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
    }
    public class PayrollCycleBO
    {
        public int PayrollCycleID { get; set; }
        public string PayrollCycleType { get; set; }
        public string PayrollCycleStart { get; set; }
        public string PayrollCycleEnd { get; set; }
        public string PayrollCyclePayDay { get; set; }
        public string PayrollEPFPayDay { get; set; }
        public string PayrollESIPayDay { get; set; }
        public string PayrollCycleStatus { get; set; }
        public string PayrollStructureMode { get; set; }
        public string PayrollMode { get; set; }
        public int CreatedBy { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public List<PayrollSalaryStructDetails> LineIteam { get; set; }
    }
    public class PayrollrtnCycleBO
    {
        public int PayrollCycleID { get; set; }
        public string PayrollCycleType { get; set; }
        public string PayrollCycleStart { get; set; }
        public string PayrollCycleEnd { get; set; }
        public string PayrollCyclePayDay { get; set; }
        public string PayrollEPFPayDay { get; set; }
        public string PayrollStructureMode { get; set; }
        public string PayrollESIPayDay { get; set; }
        public string PayrollCycleStatus { get; set; }
        public string PayrollMode { get; set; }
        public int CreatedBy { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        //public List<PayrollSalaryStructDetails> LineIteam { get; set; }
    }
    public class PayrollIncomeandProfissonalBO
    {
        public int TenantID { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal PayrollMin { get; set; }
        public decimal PayrollMax { get; set; }
        public decimal PayrollValues { get; set; }
        public string PayrollCalcRange { get; set; }
        public string PayrollCalcEntity { get; set; }
        public string PayrollCalcFormat { get; set; }
        public int LocationID { get; set; }
    }
    public class PayrollRtnTaxBO
    {
        public decimal IncomeTaxValue { get; set; }
        public decimal ProfessionalTaxValue { get; set; }
    }
    public class PayrollInputEPFBO
    {
        public int PFContributionID { get; set; }
        public decimal PayableBasic { get; set; }
        public decimal GrossSalary { get; set; }
        public int EmpID { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
    }
    public class PayrollRtnEPFBO
    {
        public List<PayrollPFContribution> detailsByMonth { get; set; }
        public List<StgDownloadDocBO> detailsByDowload { get; set; }
    }
    public class PayrollRtnESIBO
    {
        public List<PayrollESIContribution> detailsByMonth { get; set; }
        public List<StgDownloadDocBO> detailsByDowload { get; set; }
    }
    public class GroupSalaryStructBO
    {
        public int StructureID { get; set; }
        public int GroupTypeID { get; set; }
        public int GradeID { get; set; }
        public string StructureIDs { get; set; }
        public string RoleCategory { get; set; }
        public string StructureName { get; set; }        
        public decimal SalaryMin { get; set; }
        public decimal SalaryMax { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
        public List<PayrolloverallStructDetails> LineItem { get; set; } 
    }
    public class PayrollSalaryStructLineDetails
    {
        public int SalLineItemDetID { get; set; }
        public int StructureID { get; set; }
        public int salaryLineItemID { get; set; }
        public string salaryLineItem { get; set; }
        public string salaryItemType { get; set; }
        public int salaryItemorder { get; set; }
        public bool isstanditeams { get; set; }
        public string salaryPeriod { get; set; }
        public string salaryLineItemKey { get; set; }
        public string salaryCalcType { get; set; }
        public string LineCalcType { get; set; }
        public string salaryPayrollLineCalcFormat { get; set; }
        public string salaryLineItemStatus { get; set; }
        public int CreatedBy { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public List<PayrollrulesBO> rules { get; set; }
    }
    public class PayrollRtrnStructLineDetails
    {
        public List<GroupSalaryStructBO> Groupstructure { get; set; }
        public List<PayrolloverallStructDetails> Strcture { get; set; }
    }
    public class PayrollrulesBO
    {
        public int PayrollRuleID { get; set; }
        public int SalLineItemID { get; set; }
        public string PayrollRuleItemCode { get; set; }
        public string PayrollRuleForm { get; set; }
        public string PayrollComponent { get; set; }
        public string PayrollRuleComponent { get; set; }
        public string PayrollRuleMode { get; set; }
        public string PayrollRuleStatus { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
    }
    public class PayrolloverallStructDetails
    {
        public int StructureID { get; set; }
        public int GroupTypeID { get; set; }
        public int GradeID { get; set; }
        public string RoleCategory { get; set; }
        public string StructureName { get; set; }
        public string StructureStatus { get; set; }
        public decimal SalaryMin { get; set; }
        public decimal SalaryMax { get; set; }
        public int SalLineItemDetID { get; set; }
        public bool isstanditeams { get; set; }
        public int salaryLineItemID { get; set; }
        public string salaryLineItem { get; set; }
        public string salaryItemType { get; set; }
        public int salaryItemorder { get; set; }
        public string salaryPeriod { get; set; }
        public string salaryLineItemKey { get; set; }
        public string salaryCalcType { get; set; }
        public string LineCalcType { get; set; }
        public string salaryPayrollLineCalcFormat { get; set; }
        public string LineRangesCalcType { get; set; }
        public string salaryLineItemStatus { get; set; }
        public string salarymainComponent { get; set; }
        public string salaryotherComponent { get; set; }
        public string operateMode { get; set; }
        public int CreatedBy { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public string SalaryRangeMin { get; set; }
        public string SalaryRangeMax { get; set; }
        public List<PayrollrulesBO> rules { get; set; }
    }
}
