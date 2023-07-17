using Microsoft.AspNetCore.Mvc;
using MyDodos.Domain.Wrapper;
using MyDodos.Service.Logger;
using System;
using System.Collections.Generic;
using MyDodos.Service.Payroll;
using MyDodos.ViewModel.Employee;
using MyDodos.Domain.Payroll;
using MyDodos.ViewModel.Payroll;
using MyDodos.ViewModel.Common;
using Microsoft.AspNetCore.Authorization;
using MyDodos.Service.HR;

namespace MyDodos.API.Controllers.ProjectManagement
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollController : ControllerBase
    {
        private readonly IPayrollService _payrollService;
        private readonly IPayrollRevisonService _payrollRevisonService;
        private readonly IPayrollMasterService _payrollMasterService;
        private readonly IPayrollSlipService _payrollSlipService;
        private readonly ILoggerManager _logger;
        public PayrollController(IPayrollService payrollService, IPayrollRevisonService payrollRevisonService, IPayrollSlipService payrollSlipService, IPayrollMasterService payrollMasterService)
        {
            _payrollService = payrollService;
            _payrollRevisonService = payrollRevisonService;
            _payrollSlipService = payrollSlipService;
            _payrollMasterService = payrollMasterService;
        }
        [HttpPost("GetPayrollEmployeeSearch")]
        public ActionResult<Response<GetHRDirectoryList>> GetPayrollEmployeeSearch(GetHRDirectoryList objresult)
        {
            try
            {
                var result = _payrollRevisonService.GetPayrollEmployeeSearch(objresult);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<GetHRDirectoryList>(ex.Message, 500));
            }
        }
        [HttpGet("GetCurrentMonth/{TenantID}/{LocationID}")]
        public ActionResult<Response<PayrollCountBO>> GetCurrentMonth(int TenantID, int LocationID)
        {
            try
            {
                var result = _payrollService.GetCurrentMonth(TenantID, LocationID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<string>(ex.Message));
            }
        }
        /*Master Get and Save call Start*/
        [HttpGet("GetPayrollSalaryLineItem/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<PayrollSalaryStructDetails>>> GetPayrollSalaryLineItem(int TenantID, int LocationID)
        {
            try
            {
                var result = _payrollMasterService.GetPayrollSalaryLineItem(TenantID, LocationID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<List<PayrollSalaryStructDetails>>(ex.Message));
            }
        }
        [HttpGet("GetPayrollMasCycle/{TenantID}/{LocationID}")]
        public ActionResult<Response<SaveOut>> GetPayrollMasCycle(int TenantID, int LocationID)
        {
            try
            {
                var result = _payrollMasterService.GetPayrollMasCycle(TenantID, LocationID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<SaveOut>(ex.Message));
            }
        }
        [HttpPost("SavePayrollCycle")]
        public ActionResult<Response<SaveOut>> SavePayrollCycle(PayrollCycleBO objPayroll)
        {
            try
            {
                var result = _payrollMasterService.SavePayrollCycle(objPayroll);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<SaveOut>(ex.Message));
            }
        }
        [HttpDelete("DeletePayrollCycle/{PayrollCycleID}")]
        public ActionResult<Response<SaveOut>> DeletePayrollCycle(int PayrollCycleID)
        {
            try
            {
                var result = _payrollMasterService.DeletePayrollCycle(PayrollCycleID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<SaveOut>(ex.Message));
            }
        }
        [HttpPost("SavePayrolllSalaryLineItem")]
        public ActionResult<Response<SaveOut>> SavePayrolllSalaryLineItem(List<PayrollSalaryStructDetails> objPayroll)
        {
            try
            {
                var result = _payrollMasterService.SavePayrolllSalaryLineItem(objPayroll);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<SaveOut>(ex.Message));
            }
        }
        [HttpDelete("DeletePayrollSalaryLineItem/{SalLineItemId}")]
        public ActionResult<Response<SaveOut>> DeletePayrollSalaryLineItem(int SalLineItemId)
        {
            try
            {
                var result = _payrollMasterService.DeletePayrollSalaryLineItem(SalLineItemId);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<SaveOut>(ex.Message));
            }
        }
        [HttpPost("SavePayrollMasCalculation")]
        public ActionResult<Response<SaveOut>> SavePayrollMasCalculation(PayrollMasCalculationBO objPayroll)
        {
            try
            {
                var result = _payrollMasterService.SavePayrollMasCalculation(objPayroll);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<SaveOut>(ex.Message));
            }
        }
        /*Master Get and Save call End*/
        [HttpGet("GetOverallSalaryDetails/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<YearSpanValue>>> GetOverallSalaryDetails(int TenantID, int LocationID)
        {
            try
            {
                var result = _payrollService.GetOverallSalaryDetails(TenantID, LocationID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<string>(ex.Message));
            }
        }
        [HttpGet("GetSalaryPeriod/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<SalaryPeriodBO>>> GetSalaryPeriod(int TenantID, int LocationID)
        {
            try
            {
                var result = _payrollService.GetSalaryPeriod(TenantID, LocationID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<string>(ex.Message));
            }
        }
        [HttpGet("GetPayrollCalcSetting/{TenantID}/{LocationID}/{PayrollTypes}")]
        public ActionResult<Response<List<PayrollMasCalculationBO>>> GetPayrollCalcSetting(int TenantID, int LocationID, string PayrollTypes)
        {
            try
            {
                var result = _payrollMasterService.GetPayrollCalcSetting(TenantID, LocationID, PayrollTypes);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<List<PayrollMasCalculationBO>>(ex.Message));
            }
        }
        [HttpGet("GetRegPayRoll/{SalaryPeriodID}/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<RegPayRollBO>>> GetRegPayRoll(int SalaryPeriodID, int TenantID, int LocationID)
        {
            try
            {
                var result = _payrollService.GetRegPayRoll(SalaryPeriodID, TenantID, LocationID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<string>(ex.Message));
            }
        }
        [HttpPost("GetPayRollEmployees")]
        public ActionResult<Response<List<vwPayrollUser>>> GetPayRollEmployees(vwPayrollUser objpay)
        {
            try
            {
                var result = _payrollService.GetPayRollEmployees(objpay);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<string>(ex.Message));
            }
        }
        [HttpPost("GetPayRollUser")]
        public ActionResult<Response<List<vwPayrollUser>>> GetPayRollUser(vwPayrollUser objpay)
        {
            try
            {
                var result = _payrollService.GetPayRollUser(objpay);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<string>(ex.Message));
            }
        }
        [HttpPost("SaveSalaryAdjustments")]
        public ActionResult<Response<SalaryAdjustmentsBO>> SaveSalaryAdjustments(SalaryAdjustmentsBO objpay)
        {
            try
            {
                var result = _payrollService.SaveSalaryAdjustments(objpay);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<string>(ex.Message));
            }
        }
        [HttpGet("GetPayrollEmpEPFSummary/{TenantID}/{LocationID}/{SalaryPeriodID}")]
        public ActionResult<Response<PayrollRtnEPFBO>> GetPayrollEmpEPFSummary(int TenantID, int LocationID, int SalaryPeriodID)
        {
            try
            {
                var result = _payrollMasterService.GetPayrollEmpEPFSummary(TenantID, LocationID, SalaryPeriodID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<List<PayrollPFContribution>>(ex.Message));
            }
        }
        [HttpGet("GetOverallEPFSummary/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<YearPFSpanValue>>> GetOverallEPFSummary(int TenantID, int LocationID)
        {
            try
            {
                var result = _payrollMasterService.GetOverallEPFSummary(TenantID, LocationID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<List<YearPFSpanValue>>(ex.Message));
            }
        }
        [HttpPost("SavePayrollEPFChanges")]
        public ActionResult<Response<SaveOut>> SavePayrollEPFChanges(PayrollInputEPFBO objPayroll)
        {
            try
            {
                var result = _payrollMasterService.SavePayrollEPFChanges(objPayroll);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<SaveOut>(ex.Message));
            }
        }
        [HttpPost("SavePayrollEPF")]
        public ActionResult<Response<SaveOut>> SavePayrollEPF(List<PayrollPFContribution> objPayroll)
        {
            try
            {
                var result = _payrollMasterService.SavePayrollEPF(objPayroll);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<SaveOut>(ex.Message));
            }
        }
        [HttpGet("SaveConsolePayrollEPFandESI/{TenantID}/{LocationID}/{SalaryPeriodID}")]
        public ActionResult<Response<SaveOut>> SaveConsolePayrollEPFandESI(int TenantID, int LocationID, int SalaryPeriodID)
        {
            try
            {
                var result = _payrollMasterService.SaveConsolePayrollEPFandESI(TenantID, LocationID, SalaryPeriodID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<SaveOut>(ex.Message));
            }
        }
        [HttpGet("GetPayrollEmpESISummary/{TenantID}/{LocationID}/{SalaryPeriodID}")]
        public ActionResult<Response<PayrollRtnESIBO>> GetPayrollEmpESISummary(int TenantID, int LocationID, int SalaryPeriodID)
        {
            try
            {
                var result = _payrollMasterService.GetPayrollEmpESISummary(TenantID, LocationID, SalaryPeriodID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<List<PayrollPFContribution>>(ex.Message));
            }
        }
        [HttpGet("GetOverallESISummary/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<YearPFSpanValue>>> GetOverallESISummary(int TenantID, int LocationID)
        {
            try
            {
                var result = _payrollMasterService.GetOverallESISummary(TenantID, LocationID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<List<YearPFSpanValue>>(ex.Message));
            }
        }
        [HttpGet("PayrollGenerateManually/{SalaryPeriodID}/{TenantID}/{LocationID}")]
        public ActionResult<Response<SaveOut>> PayrollGenerateManually(int SalaryPeriodID, int TenantID, int LocationID)
        {
            try
            {
                var result = _payrollService.PayrollGenerateManually(SalaryPeriodID, TenantID, LocationID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<string>(ex.Message));
            }
        }
        [HttpPost("SaveandDowloadECRFile")]
        public ActionResult<Response<RtnEPFandESIBO>> DowloadECRFile(EPFandESIBO _objpayroll)
        {
            try
            {
                var result = _payrollSlipService.DowloadECRFile(_objpayroll);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<SaveOut>(ex.Message));
            }
        }
        [HttpPost("SaveandDowloadESIFile")]
        public ActionResult<Response<RtnEPFandESIBO>> PayrollDowloadESIFile(EPFandESIBO _objpayroll)
        {
            try
            {
                var result = _payrollSlipService.PayrollDowloadESIFile(_objpayroll);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<SaveOut>(ex.Message));
            }
        }
        [HttpPost("DowloadECRtxtFile")]
        public ActionResult<Response<RtnEPFandESIBO>> DowloadECRtxtFile(EPFandESIBO _objpayroll)
        {
            try
            {
                var result = _payrollSlipService.DowloadECRtxtFile(_objpayroll);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<SaveOut>(ex.Message));
            }
        }
        [HttpPost("GetBankExcelDetails")]
        public ActionResult<Response<List<BankExcelValuesBO>>> GetBankExcelDetails(BankExcelInputBO objinp)
        {
            try
            {
                var result = _payrollService.GetBankExcelDetails(objinp);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<string>(ex.Message));
            }
        }
        [HttpPost("GetSalaryEmployeesExcelDetails")]
        public ActionResult<Response<List<EmployeeSalaryExcelBO>>> GetSalaryEmployeesExcelDetails(BankExcelInputBO objinp)
        {
            try
            {
                var result = _payrollService.GetSalaryEmployeesExcelDetails(objinp);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<string>(ex.Message));
            }
        }
        [HttpPost("UpdateSalaryPeriodStatus")]
        public ActionResult<Response<SalaryPeriodBO>> UpdateSalaryPeriodStatus(SalaryPeriodStatusBO objinp)
        {
            try
            {
                var result = _payrollService.UpdateSalaryPeriodStatus(objinp);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<string>(ex.Message));
            }
        }
        [HttpGet("GetMasYearTenant/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<MasYearDetailsBO>>> GetMasYearTenant(int TenantID, int LocationID)
        {
            try
            {
                var result = _payrollService.GetMasYearTenant(TenantID, LocationID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<string>(ex.Message));
            }
        }
        [HttpGet("GetSalaryPeriodMonths/{TenantID}/{LocationID}/{YearID}")]
        public ActionResult<Response<SaveOut>> GetSalaryPeriodMonths(int TenantID, int LocationID, int YearID)
        {
            try
            {
                var result = _payrollService.GetSalaryPeriodMonths(TenantID, LocationID, YearID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<string>(ex.Message));
            }
        }
        [HttpPost("SavePayrolllOverAllLineItem")]
        public ActionResult<Response<SaveOut>> SavePayrolllOverAllLineItem(PayrolloverallStructDetails objPayroll)
        {
            try
            {
                var result = _payrollMasterService.SavePayrolllOverAllLineItem(objPayroll);
                if (result.StatusCode == 200)
                {
                    if(objPayroll.StructureID > 0){
                    var item =_payrollMasterService.GetPayrollSalaryStructLineData(objPayroll.StructureID,objPayroll.TenantID,objPayroll.LocationID,objPayroll.isstanditeams);
                    }
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<SaveOut>(ex.Message));
            }
        }
        [HttpGet("GetPayrollMasterStructure/{TenantID}/{LocationID}/{StructureID}")]
        public ActionResult<Response<List<GroupSalaryStructBO>>> GetPayrollMasterStructure(int TenantID, int LocationID, int StructureID)
        {
            try
            {
                var result = _payrollMasterService.GetPayrollMasterStructure(TenantID, LocationID, StructureID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<List<GroupSalaryStructBO>>(ex.Message));
            }
        }        
        [HttpPost("SavePayrollMasterRules")]
        public ActionResult<Response<SaveOut>> SavePayrollMasterRules(PayrollrulesBO objPayroll)
        {
            try
            {
                var result = _payrollMasterService.SavePayrollMasterRules(objPayroll);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<SaveOut>(ex.Message));
            }
        }
        [HttpPost("SaveOrderLineItem")]
        public ActionResult<Response<SaveOut>> SaveOrderLineItem(List<PayrolloverallStructDetails> objPayroll)
        {
            try
            {
                var result = _payrollMasterService.SaveOrderLineItem(objPayroll);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<SaveOut>(ex.Message));
            }
        }
        [HttpDelete("deletepayrolliteamdetails/{SalLineItemDetID}")]
        public ActionResult<Response<SaveOut>> deletepayrolliteamdetails(int SalLineItemDetID)
        {
            try
            {
                var result = _payrollMasterService.deletepayrolliteamdetails(SalLineItemDetID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<SaveOut>(ex.Message));
            }
        }
        [HttpDelete("deletepayrollstruct/{StructureID}")]
        public ActionResult<Response<SaveOut>> deletepayrollstruct(int StructureID)
        {
            try
            {
                var result = _payrollMasterService.deletepayrollstruct(StructureID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<SaveOut>(ex.Message));
            }
        }
        [HttpGet("GetPayRollRuleDropDown/{TenantID}/{LocationID}/{StructureID}")]
        public ActionResult<Response<List<PayrolloverallStructDetails>>> GetPayRollRuleDropDown(int TenantID, int LocationID, int StructureID)
        {
            try
            {
                var result = _payrollMasterService.GetPayrollStructLine(TenantID, LocationID, StructureID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {               
                return StatusCode(500, new Response<List<PayrolloverallStructDetails>>(ex.Message));
            }
        }
        [HttpPost("GetPayrollSalaryStructLineDetails")]
        public ActionResult<Response<PayrollRtrnStructLineDetails>> GetPayrollSalaryStructLineDetails(PayrollSalaryStructLineDetails input)
        {
            try
            {
                var result = _payrollMasterService.GetPayrollSalaryStructLineDetails(input.StructureID,input.TenantID,input.LocationID,input.isstanditeams);
                if (result.StatusCode == 200)
                {
                    if(input.StructureID > 0){
                    var item =_payrollMasterService.GetPayrollSalaryStructLineData(input.StructureID,input.TenantID,input.LocationID,input.isstanditeams);
                    }
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<List<PayrollSalaryStructLineDetails>>(ex.Message));
            }
        }
        [HttpPost("GetPayrollSalaryStructLineData")]
        public ActionResult<Response<SaveOut>> GetPayrollSalaryStructLineData(PayrollSalaryStructLineDetails input)
        {
            try
            {
                var result = _payrollMasterService.GetPayrollSalaryStructLineData(input.StructureID,input.TenantID,input.LocationID,input.isstanditeams);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<List<PayrollSalaryStructLineDetails>>(ex.Message));
            }
        }
        [HttpGet("GetPayrollMasterSalaryStructure/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<GroupSalaryStructBO>>> GetPayrollMasterSalaryStructure(int TenantID, int LocationID)
        {
            try
            {
                var result = _payrollMasterService.GetPayrollMasterSalaryStructure(TenantID, LocationID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<List<GroupSalaryStructBO>>(ex.Message));
            }
        }
        // [HttpGet("GetPayrollStructLine/{StructureID}")]
        // public ActionResult<Response<List<PayrollSalaryStructLineDetails>>> GetPayrollStructLine(int StructureID)
        // {
        //     try
        //     {
        //         var result = _payrollMasterService.GetPayrollStructLine(StructureID);
        //         if (result.StatusCode == 200)
        //         {
        //             return Ok(result);
        //         }
        //         else
        //         {
        //             return StatusCode(result.StatusCode, result);
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, new Response<List<PayrollSalaryStructLineDetails>>(ex.Message));
        //     }
        // }
        [HttpPost("SavePayrolllOverAllStructureLineItem")]
        public ActionResult<Response<SaveOut>> SavePayrolllOverAllStructureLineItem(PayrolloverallStructDetails objPayroll)
        {
            try
            {
                var result = _payrollMasterService.SavePayrolllOverAllStructureLineItem(objPayroll);
                if (result.StatusCode == 200)
                {
                    var item =_payrollMasterService.GetPayrollMasterStructure(objPayroll.TenantID,objPayroll.LocationID,objPayroll.StructureID);
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<SaveOut>(ex.Message));
            }
        }
        [HttpGet("GetPayslipEmployees/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<EmployeeBenefitsBO>>> GetPayslipEmployees(int TenantID, int LocationID)
        {
            try
            {
                var result = _payrollService.GetPayslipEmployees(TenantID, LocationID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<string>(ex.Message));
            }
        }
    }
}