using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Master;
using MyDodos.Domain.Payroll;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.Payroll;
using MyDodos.ViewModel.Common;

namespace MyDodos.Service.Payroll
{
    public class PayrollService : IPayrollService
    {
        private readonly IConfiguration _configuration;
        private readonly IPayrollRepository _payrollRepository;
        public PayrollService(IConfiguration configuration, IPayrollRepository payrollRepository)
        {
            _configuration = configuration;
            _payrollRepository = payrollRepository;
        }
        // public Response<LeaveRequestModelMsg> SavePPSponsor(PPSponsorBO sponsor)
        // {
        //     Response<LeaveRequestModelMsg> response;
        //     try
        //     {
        //         var result = _projectRepository.SavePPSponsor(sponsor);
        //         if (result.RequestID == 0)
        //         {
        //             response = new Response<LeaveRequestModelMsg>(result,200,"Sponsor Data Creation or Updation is Failed");
        //         }
        //         else
        //         {
        //             response = new Response<LeaveRequestModelMsg>(result,200,"Saved Successfully");
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         response = new Response<LeaveRequestModelMsg>(ex.Message,500);
        //     }
        //     return response;
        // }
        public Response<PayrollCountBO> GetCurrentMonth(int TenantID, int LocationID)
        {
            Response<PayrollCountBO> response;
            try
            {
                var result = _payrollRepository.GetCurrentMonth(TenantID, LocationID);
                if (result == null)
                {
                    response = new Response<PayrollCountBO>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<PayrollCountBO>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<PayrollCountBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<YearSpanValue>> GetOverallSalaryDetails(int TenantID, int LocationID)
        {
            Response<List<YearSpanValue>> response;
            try
            {
                var result = _payrollRepository.GetOverallSalaryDetails(TenantID, LocationID);
                if (result.Count == 0)
                {
                    response = new Response<List<YearSpanValue>>(result, 200, "Data not Found");
                }
                else
                {
                    foreach (var item in result)
                    {
                        var value = _payrollRepository.GetOverallSalaryYearDetails(item.YearID, item.YearSpan, TenantID, LocationID);
                        item.detailsByMonth = value;
                    }
                    response = new Response<List<YearSpanValue>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<YearSpanValue>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<SalaryPeriodBO>> GetSalaryPeriod(int TenantID, int LocationID)
        {
            Response<List<SalaryPeriodBO>> response;
            try
            {
                var result = _payrollRepository.GetSalaryPeriod(TenantID, LocationID);
                if (result.Count == 0)
                {
                    response = new Response<List<SalaryPeriodBO>>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<List<SalaryPeriodBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<SalaryPeriodBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<RegPayRollBO>> GetRegPayRoll(int SalaryPeriodID, int TenantID, int LocationID)
        {
            Response<List<RegPayRollBO>> response;
            try
            {
                var result = _payrollRepository.GetRegPayRoll(SalaryPeriodID, TenantID, LocationID);
                if (result.Count == 0)
                {
                    response = new Response<List<RegPayRollBO>>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<List<RegPayRollBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<RegPayRollBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<vwPayrollUser>> GetPayRollEmployees(vwPayrollUser objpay)
        {
            Response<List<vwPayrollUser>> response;
            try
            {
                var result = _payrollRepository.GetPayRollEmployees(objpay);
                if (result.Count == 0)
                {
                    response = new Response<List<vwPayrollUser>>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<List<vwPayrollUser>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<vwPayrollUser>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<vwPayrollUser>> GetPayRollUser(vwPayrollUser objpay)
        {
            Response<List<vwPayrollUser>> response;
            try
            {
                var result = _payrollRepository.GetPayRollUser(objpay);
                if (result.Count == 0)
                {
                    response = new Response<List<vwPayrollUser>>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<List<vwPayrollUser>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<vwPayrollUser>>(ex.Message, 500);
            }
            return response;
        }
        public Response<SalaryAdjustmentsBO> SaveSalaryAdjustments(SalaryAdjustmentsBO objpay)
        {
            Response<SalaryAdjustmentsBO> response;
            try
            {
                var result = _payrollRepository.SaveSalaryAdjustments(objpay);
                if (result == null)
                {
                    response = new Response<SalaryAdjustmentsBO>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<SalaryAdjustmentsBO>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<SalaryAdjustmentsBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> PayrollGenerateManually(int SalaryPeriodID, int TenantID, int LocationID)
        {
            Response<SaveOut> response;
            List<vwPayrollUser> users = new List<vwPayrollUser>();
            vwPayrollUser objusers = new vwPayrollUser();
            SaveOut final = new SaveOut();
            try
            {
                var result = _payrollRepository.GetRegPayRollBackUP(SalaryPeriodID, TenantID, LocationID);
                if (result[0].Status != "Not Generated")
                {
                    result.ForEach(a => a.SalaryPeriodID = SalaryPeriodID);
                    foreach (var item in result)
                    {
                        var main = _payrollRepository.SaveSalaryMainDetails(item);
                    }
                    objusers.EmpID = 0;
                    objusers.SalaryPeriodId = SalaryPeriodID;
                    objusers.TenantID = TenantID;
                    objusers.LocationID = LocationID;
                    objusers.IsExcel = false;
                    users = _payrollRepository.GetPayRollUser(objusers);
                    foreach (var val in users)
                    {
                        var appuser = _payrollRepository.GetPayrollEntAppUser(val.EmpID, val.TenantID, val.LocationID);
                        if (appuser != null)
                        {
                            val.AppUserId = Convert.ToInt32(appuser.AppUserID);
                            var LOP = _payrollRepository.CalculateLOP(SalaryPeriodID, Convert.ToInt32(appuser.PrimaryCompanyID), LocationID, Convert.ToInt32(appuser.AppUserID));
                            val.TotalDays = LOP.TotalDays;
                            val.PaidDays = LOP.PaidDays;
                            val.LOPDays = LOP.LOPDays;
                            val.TimeoffDays = LOP.TimeoffDays;
                            val.Holidays = LOP.Holidays;

                            var empdetails = _payrollRepository.SaveEmpSalaryStructDetails(val.EmpID, val.SalaryPeriodId);
                            final = _payrollRepository.SaveEmpDetails(val);
                            var updvalue = _payrollRepository.UpdateNetPayAmount(val.SalaryPeriodId, Convert.ToInt32(LOP.TotalDays), Convert.ToInt32(LOP.PaidDays), val.AppUserId);
                        }
                    }
                    //users.ForEach(a => a.SalaryPeriodId = SalaryPeriodID);
                    // foreach (var data in users)
                    // {
                    //     var getAdjust = _payrollRepository.GetSalaryAdjust(data.EmpID, data.SalaryPeriodId);

                    //     if (getAdjust.Count != 0)
                    //     {
                    //         data.IsAdjust = true;
                    //     }
                    //     else
                    //     {
                    //         data.IsAdjust = false;
                    //     }
                    //     var empdetails = _payrollRepository.SaveEmpSalaryStructDetails(data.EmpID, data.SalaryPeriodId);
                    //     final = _payrollRepository.SaveEmpDetails(data);
                    //     var paymentvalue = _payrollRepository.PayrollLOP(data.SalaryPeriodId, data.TenantID, data.LocationID, data.EmpID);
                    //     var updvalue = _payrollRepository.UpdateNetPayAmount(data.SalaryPeriodId, Convert.ToInt32(paymentvalue.TotalDays), Convert.ToInt32(paymentvalue.PaidDays), data.AppUserId);
                    // }
                }
                response = new Response<SaveOut>(final, 200, "Data Retrieved");
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<BankExcelValuesBO>> GetBankExcelDetails(BankExcelInputBO objinp)
        {
            Response<List<BankExcelValuesBO>> response;
            try
            {
                objinp.Purpose = "Payroll";
                objinp.EmpID = 0;
                var result = _payrollRepository.GetBankExcelDetails(objinp);
                if (result.Count == 0)
                {
                    response = new Response<List<BankExcelValuesBO>>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<List<BankExcelValuesBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<BankExcelValuesBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<EmployeeSalaryExcelBO>> GetSalaryEmployeesExcelDetails(BankExcelInputBO objinp)
        {
            Response<List<EmployeeSalaryExcelBO>> response;
            List<EmployeeSalaryExcelBO> result = new List<EmployeeSalaryExcelBO>();
            try
            {
                var res = _payrollRepository.GetPayrollEmployeesExcel(objinp.TenantID, objinp.LocationID);
                foreach (var item in res)
                {
                    objinp.Purpose = "Payroll Employees";
                    var value = _payrollRepository.GetSalaryEmployeesExcelDetails(objinp, item.EmpID);
                    result.Add(value);
                }
                if (result.Count == 0)
                {
                    response = new Response<List<EmployeeSalaryExcelBO>>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<List<EmployeeSalaryExcelBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<EmployeeSalaryExcelBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<SalaryPeriodBO> UpdateSalaryPeriodStatus(SalaryPeriodStatusBO objinp)
        {
            Response<SalaryPeriodBO> response;
            try
            {
                var result = _payrollRepository.UpdateSalaryPeriodStatus(objinp);
                if (result == null)
                {
                    response = new Response<SalaryPeriodBO>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<SalaryPeriodBO>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<SalaryPeriodBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<MasYearDetailsBO>> GetMasYearTenant(int TenantID, int LocationID)
        {
            Response<List<MasYearDetailsBO>> response;
            try
            {
                var result = _payrollRepository.GetMasYearTenant(TenantID, LocationID);
                if (result == null)
                {
                    response = new Response<List<MasYearDetailsBO>>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<List<MasYearDetailsBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<MasYearDetailsBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<SalaryPeriodBO>> GetSalaryPeriodMonths(int TenantID, int LocationID, int YearID)
        {
            Response<List<SalaryPeriodBO>> response;
            try
            {
                var result = _payrollRepository.GetSalaryPeriodMonths(TenantID, LocationID, YearID);
                if (result.Count == 0)
                {
                    response = new Response<List<SalaryPeriodBO>>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<List<SalaryPeriodBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<SalaryPeriodBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<EmployeeBenefitsBO>> GetPayslipEmployees(int TenantID, int LocationID)
        {
            Response<List<EmployeeBenefitsBO>> response;
            try
            {
                var result = _payrollRepository.GetPayslipEmployees(TenantID, LocationID);
                if (result.Count == 0)
                {
                    response = new Response<List<EmployeeBenefitsBO>>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<List<EmployeeBenefitsBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<EmployeeBenefitsBO>>(ex.Message, 500);
            }
            return response;
        }
    }
}