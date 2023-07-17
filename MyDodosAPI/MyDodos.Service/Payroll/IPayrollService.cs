using System;
using System.Collections.Generic;
using MyDodos.Domain.Master;
using MyDodos.Domain.Payroll;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.Common;

namespace MyDodos.Service.Payroll
{
    public interface IPayrollService
    {
        Response<PayrollCountBO> GetCurrentMonth(int TenantID, int LocationID);
        Response<List<YearSpanValue>> GetOverallSalaryDetails(int TenantID, int LocationID);
        Response<List<SalaryPeriodBO>> GetSalaryPeriod(int TenantID, int LocationID);
        Response<List<RegPayRollBO>> GetRegPayRoll(int SalaryPeriodID, int TenantID, int LocationID);
        Response<List<vwPayrollUser>> GetPayRollEmployees(vwPayrollUser objpay);
        Response<List<vwPayrollUser>> GetPayRollUser(vwPayrollUser objpay);
        Response<SalaryAdjustmentsBO> SaveSalaryAdjustments(SalaryAdjustmentsBO objpay);
        Response<SaveOut> PayrollGenerateManually(int SalaryPeriodID, int TenantID, int LocationID);
        Response<List<BankExcelValuesBO>> GetBankExcelDetails(BankExcelInputBO objinp);
        Response<List<EmployeeSalaryExcelBO>> GetSalaryEmployeesExcelDetails(BankExcelInputBO objinp);
        Response<SalaryPeriodBO> UpdateSalaryPeriodStatus(SalaryPeriodStatusBO objinp);
        Response<List<MasYearDetailsBO>> GetMasYearTenant(int TenantID, int LocationID);
        Response<List<SalaryPeriodBO>> GetSalaryPeriodMonths(int TenantID, int LocationID, int YearID);
        Response<List<EmployeeBenefitsBO>> GetPayslipEmployees(int TenantID, int LocationID);
    }
}