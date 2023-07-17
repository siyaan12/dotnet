using System;
using System.Collections.Generic;
using MyDodos.Domain.BenefitManagement;
using MyDodos.Domain.Holiday;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.Master;
using MyDodos.Domain.Payroll;
using MyDodos.Domain.TimeOff;
using MyDodos.ViewModel.Common;
using MyDodos.ViewModel.Payroll;

namespace MyDodos.Repository.Payroll
{
    public interface IPayrollRepository
    {
        PayrollCountBO GetCurrentMonth(int TenantID, int LocationID);
        List<BenefitGroupBO> GetBenefitGroupByGroupType(int TenantID, int GroupTypeID);
        List<MasMedicalBenefitBO> GetMasMedicalBenefit(int TenantID, int LocationID);
        SaveOut SaveOnBoardBenefits(OnBoardBenefitGroupBO objgroup);
        SaveOut SaveOnBoardStruct(OnBoardBenefitGroupBO objgroup);
        List<OtherBenefitsBO> GetOtherBenefits(int BenefitGroupID, int TenantID, int LocationID);
        List<EmployeeBenefitsBO> GetEmployeeBenefits(int EmpID, int TenantID, int LocationID);
        List<HREmpBenefitsModel> GetEmployeePayrollBenefit(int EmpID, int BenefitGroupID, string paycycle, int StructureID);
        List<HREmpSalaryStructure> GetEmployeePayrollStructure(int EmpID, int BenefitGroupID, string paycycle, int StructureID);
        SaveOut SaveEmployeeCTC(PayrollCTCBO objgroup);
        SaveOut SaveEmployeePayRollBenfits(PayrollCTCBO objgroup);
        List<YearSpanValue> GetOverallSalaryDetails(int TenantID, int LocationID);
        List<SalaryDetailsMonth> GetOverallSalaryYearDetails(int YearID, string YearSpan, int TenantID, int LocationID);
        List<SalaryPeriodBO> GetSalaryPeriod(int TenantID, int LocationID);
        List<RegPayRollBO> GetRegPayRoll(int SalaryPeriodID, int TenantID, int LocationID);
        List<vwPayrollUser> GetPayRollEmployees(vwPayrollUser objpay);
        List<vwPayrollUser> GetPayRollUser(vwPayrollUser objpay);
        List<PayrollRtnTaxBO> GetPayrollPTaxandITax(PayrollIncomeandProfissonalBO objPayroll);
        SalaryAdjustmentsBO SaveSalaryAdjustments(SalaryAdjustmentsBO objpay);
        List<vwPayrollUser> GetPayrollSalaryMonth(int TenantID, int LocationID, int SalaryPeriodID);
        List<RegPayRollBO> GetRegPayRollBackUP(int SalaryPeriodID, int TenantID, int LocationID);
        SaveOut SavePayrollSetCTC(EmpSalaryStructureCTC objPayroll);
        List<PayrollIncomeandProfissonalBO> GetPayrollTypecalc(int TenantID, int LocationID);
        SaveOut SaveSalaryMainDetails(RegPayRollBO objreg);
        List<SalaryAdjustmentsBO> GetSalaryAdjust(int EmpID, int SalaryPeriodID);
        SalaryPeriodBO GetSPSalaryPeriod(int SalaryPeriodID);
        GetLOPBO GetPayrollLOPs(int SalaryPeriodId, int TenantID, int LocationID, int EmpID, string IsLOP);
        SaveOut SaveEmpDetails(vwPayrollUser objuser);
        GetLOPBO PayrollLOP(int SalaryPeriodId, int TenantID, int LocationID, int EmpID);
        SaveOut SaveEmpSalaryStructDetails(int EmpID, int SalaryPeriodID);
        PayrollEntAppUserBO GetPayrollEntAppUser(int EmpID, int TenantID, int LocationID);
        List<DateTime> getDates(DateTime startDate, DateTime endDate);
        List<LeaveRequestModel> GetLeaveLOP(int AppUserID, DateTime StartDate, DateTime EndDate, string IsLOP, int TenantID, int LocationID);
        List<TimeoffRequestModel> GetTimeoffLOP(int AppUserID, DateTime StartDate, DateTime EndDate, string IsLOP, int TenantID, int LocationID);
        List<HolidayBO> GetHolidayLOP(int AppUserID, DateTime StartDate, DateTime EndDate, string IsLOP, int TenantID, int LocationID);
        DatesLOP GetLOPDates(int SalaryPeriodID, int TenantID, int LocationID);
        GetLOPBO CalculateLOP(int SalaryPeriodId, int TenantID, int LocationID, int AppUserID);
        List<HREmpSalaryStructure> GetEmpPayrolBycycle(int EmpID, int StructureID, string paycycle);
        List<BankExcelValuesBO> GetBankExcelDetails(BankExcelInputBO objinp);
        EmployeeSalaryExcelBO GetSalaryEmployeesExcelDetails(BankExcelInputBO objinp, int EmpID);
        List<EmployeeBenefitsBO> GetPayrollEmployeesExcel(int TenantID, int LocationID);
        SalaryPeriodBO UpdateSalaryPeriodStatus(SalaryPeriodStatusBO objinp);
        SaveOut UpdateNetPayAmount(int SalaryPeriodId, int TotalDays, int PaidDays, int AppUserId);
        List<MasYearDetailsBO> GetMasYearTenant(int TenantID, int LocationID);
        List<SalaryPeriodBO> GetSalaryPeriodMonths(int TenantID, int LocationID, int YearID);
        EmployeeBenefitsBO GetEmployeeDetailsPayroll(int AppUserID);
        DatesLOP GetEndDate(int TenantID, int LocationID,DateTime DOJ);
        SaveOut SavePayrollDynamicSetCTC(EmpSalaryStructureCTC objPayroll);
        List<EmployeeBenefitsBO> GetPayslipEmployees(int TenantID, int LocationID);
    }
}