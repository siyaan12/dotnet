using System;
using System.Collections.Generic;
using KoSoft.DocRepo;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Payroll;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.Attendance;
using MyDodos.Repository.HR;
using MyDodos.Repository.Payroll;
using MyDodos.ViewModel.Attendance;
using MyDodos.ViewModel.Common;
using MyDodos.ViewModel.Payroll;
using MyDodos.Repository.Administrative;

namespace MyDodos.Service.Payroll
{
    public class PayrollMasterService : IPayrollMasterService
    {
        private readonly IConfiguration _configuration;
        private readonly IPayrollMasterRepository _payrollMasterRepository;
        private readonly IPayrollRepository _payrollRepository;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly ICommonRepository _commonRepository;
        private readonly IAdministrativeRepository _administrativeRepository;
        public PayrollMasterService(IConfiguration configuration, ICommonRepository commonRepository, IPayrollMasterRepository payrollMasterRepository, IPayrollRepository payrollRepository, IAttendanceRepository attendanceRepository,IAdministrativeRepository administrativeRepository)
        {
            _configuration = configuration;
            _payrollMasterRepository = payrollMasterRepository;
            _payrollRepository = payrollRepository;
            _attendanceRepository = attendanceRepository;
            _commonRepository = commonRepository;
            _administrativeRepository = administrativeRepository;
        }
        public Response<PayrollrtnCycleBO> GetPayrollMasCycle(int TenantID, int LocationID)
        {
            Response<PayrollrtnCycleBO> response;
            try
            {
                PayrollrtnCycleBO cycle = new PayrollrtnCycleBO();
                var result = _payrollMasterRepository.GetPayrollCycle(TenantID, LocationID);
                if (result.Count == 0)
                {
                    response = new Response<PayrollrtnCycleBO>(cycle, 200, "Data not Found");
                }
                else
                {
                    cycle = result[0];
                    //cycle.LineIteam = _payrollMasterRepository.GetPayrollSalaryLineItem(TenantID, LocationID);
                    response = new Response<PayrollrtnCycleBO>(cycle, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<PayrollrtnCycleBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> SavePayrollCycle(PayrollCycleBO objPayroll)
        {
            Response<SaveOut> response;
            try
            {
                SaveOut result = new SaveOut();
                result = _payrollMasterRepository.SavePayrollCycle(objPayroll);
                // if (objPayroll.LineIteam.Count > 0)
                // {
                //     foreach (var obj in objPayroll.LineIteam)
                //     {
                //         result = _payrollMasterRepository.SavePayrolllSalaryLineItem(obj);
                //     }
                // }
                if (result.Id == 0)
                {
                    response = new Response<SaveOut>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<SaveOut>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> DeletePayrollCycle(int PayrollCycleID)
        {
            Response<SaveOut> response;
            try
            {
                var result = _payrollMasterRepository.DeletePayrollCycle(PayrollCycleID);
                if (result.Id == 0)
                {
                    response = new Response<SaveOut>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<SaveOut>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<PayrollSalaryStructDetails>> GetPayrollSalaryLineItem(int TenantID, int LocationID)
        {
            Response<List<PayrollSalaryStructDetails>> response;
            try
            {
                var result = _payrollMasterRepository.GetPayrollSalaryLineItem(TenantID, LocationID);
                if (result.Count == 0)
                {
                    response = new Response<List<PayrollSalaryStructDetails>>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<List<PayrollSalaryStructDetails>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<PayrollSalaryStructDetails>>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> SavePayrolllSalaryLineItem(List<PayrollSalaryStructDetails> objPayroll)
        {
            Response<SaveOut> response;
            try
            {
                SaveOut result = new SaveOut();
                int i = 0;
                foreach (var obj in objPayroll)
                {
                    if(obj.SalLineItemID > 0)
                        obj.Itemorder = i;
                    result = _payrollMasterRepository.SavePayrolllSalaryLineItem(obj);
                    i++;
                }

                if (result.Id == 0)
                {
                    response = new Response<SaveOut>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<SaveOut>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> DeletePayrollSalaryLineItem(int SalLineItemId)
        {
            Response<SaveOut> response;
            try
            {
                var result = _payrollMasterRepository.DeletePayrollSalaryLineItem(SalLineItemId);
                if (result.Id == 0)
                {
                    response = new Response<SaveOut>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<SaveOut>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> SavePayrollMasCalculation(PayrollMasCalculationBO objPayroll)
        {
            Response<SaveOut> response;
            try
            {
                var result = _payrollMasterRepository.SavePayrollMasCalculation(objPayroll);

                if (result.Id == 0)
                {
                    response = new Response<SaveOut>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<SaveOut>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<PayrollMasCalculationBO>> GetPayrollCalcSetting(int TenantID, int LocationID, string PayrollTypes)
        {
            Response<List<PayrollMasCalculationBO>> response;
            try
            {
                var result = _payrollMasterRepository.GetPayrollCalcSetting(TenantID, LocationID, PayrollTypes);
                if (result.Count == 0)
                {
                    response = new Response<List<PayrollMasCalculationBO>>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<List<PayrollMasCalculationBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<PayrollMasCalculationBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<YearPFSpanValue>> GetOverallEPFSummary(int TenantID, int LocationID)
        {
            Response<List<YearPFSpanValue>> response;
            try
            {
                var result = _payrollMasterRepository.GetOverallSalaryDetails(TenantID, LocationID);
                if (result.Count == 0)
                {
                    response = new Response<List<YearPFSpanValue>>(result, 200, "Data not Found");
                }
                else
                {
                    foreach (var item in result)
                    {
                        var value = _payrollMasterRepository.GetPayrollEPFSummary(TenantID, LocationID, item.YearSpan);
                        if (value.Count > 0)
                        {
                            item.detailsByMonth = value;
                            result[0].AmountPaid = item.detailsByMonth[0].AmountPaid;
                            result[0].EPFDate = item.detailsByMonth[0].EPFDate;
                            result[0].ESIDate = item.detailsByMonth[0].ESIDate;
                            result[0].TotalEmployee = item.detailsByMonth[0].EmpTotals;
                        }
                    }
                    response = new Response<List<YearPFSpanValue>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<YearPFSpanValue>>(ex.Message, 500);
            }
            return response;
        }
        public Response<PayrollRtnEPFBO> GetPayrollEmpEPFSummary(int TenantID, int LocationID, int SalaryPeriodID)
        {
            Response<PayrollRtnEPFBO> response;
            try
            {
                PayrollRtnEPFBO objrtn = new PayrollRtnEPFBO();
                objrtn.detailsByMonth = _payrollMasterRepository.GetPayrollEmpEPFSummary(SalaryPeriodID);
                if (objrtn.detailsByMonth.Count == 0)
                {
                    response = new Response<PayrollRtnEPFBO>(objrtn, 200, "Data not Found");
                }
                else
                {
                    objrtn.detailsByDowload = _commonRepository.GetStgDownloadDoc(TenantID,0, SalaryPeriodID, "PayrollPFECRFile");
                    // DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                    // objrtn.detailsByDowload = objdoc.GetDocument(0, SalaryPeriodID, "", "PayrollPFECRFile", TenantID);
                    // if (objrtn.detailsByDowload.Count > 0)
                    // {
                    //     foreach (var item in objrtn.detailsByDowload)
                    //     {
                    //         emp.EmpID = item.CreatedBy;
                    //         emp.TenantID = TenantID;
                    //         emp.LocationID = LocationID;
                    //         var empdet = _attendanceRepository.GetEmployee(emp);
                    //         if (empdet != null)
                    //         {
                    //             item.FullName = empdet.FullName;
                    //         }
                    //     }

                    // }
                    response = new Response<PayrollRtnEPFBO>(objrtn, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<PayrollRtnEPFBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> SavePayrollEPFChanges(PayrollInputEPFBO objPayroll)
        {
            Response<SaveOut> response;
            try
            {
                var result = _payrollMasterRepository.SavePayrollEPFChanges(objPayroll);

                if (result.Id == 0)
                {
                    response = new Response<SaveOut>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<SaveOut>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> SaveConsolePayrollEPFandESI(int TenantID, int LocationID, int SalaryPeriodID)
        {
            Response<SaveOut> response;
            try
            {
                SaveOut obj = new SaveOut();
                PayrollInputEPFBO objPayroll = new PayrollInputEPFBO();
                var result = _payrollRepository.GetPayrollSalaryMonth(TenantID, LocationID, SalaryPeriodID);
                if (result.Count > 0)
                {
                    foreach (var item in result)
                    {
                        objPayroll.EmpID = item.EmpID;
                        objPayroll.PayableBasic = item.BasicSalary;
                        objPayroll.GrossSalary = item.GrossSalary;
                        objPayroll.TenantID = item.TenantID;
                        objPayroll.LocationID = item.LocationID;
                        var rtnobj = _payrollMasterRepository.GetPayrollEPFandESICalc(objPayroll);
                        if (rtnobj.Count > 0)
                        {
                            var rtn = rtnobj[0];
                            rtn.SalaryPeriodID = item.SalaryPeriodId;
                            rtn.EmpID = item.EmpID;
                            rtn.GrossSalary = item.GrossSalary;
                            rtn.Basic = item.BasicSalary;
                            rtn.TenantID = item.TenantID;
                            rtn.LocationID = item.LocationID;
                            rtn.AmountPaid = rtn.TotalPF;
                            rtn.NoOfDay = item.PaidDays;
                            rtn.VPF = 0;
                            rtn.NCPandLOPday = 0;
                            obj = _payrollMasterRepository.SavePayrollEPF(rtn);
                        }
                    }
                }
                if (result.Count == 0)
                {
                    response = new Response<SaveOut>(obj, 200, "Data not Found");
                }
                else
                {
                    response = new Response<SaveOut>(obj, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> SavePayrollEPF(List<PayrollPFContribution> objPayroll)
        {
            Response<SaveOut> response;
            try
            {
                SaveOut result = new SaveOut();
                foreach (var obj in objPayroll)
                {
                    result = _payrollMasterRepository.SavePayrollEPF(obj);
                }
                if (result.Id == 0)
                {
                    response = new Response<SaveOut>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<SaveOut>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<YearESISpanValue>> GetOverallESISummary(int TenantID, int LocationID)
        {
            Response<List<YearESISpanValue>> response;
            try
            {
                var result = _payrollMasterRepository.GetESISalaryDetails(TenantID, LocationID);
                if (result.Count == 0)
                {
                    response = new Response<List<YearESISpanValue>>(result, 200, "Data not Found");
                }
                else
                {
                    foreach (var item in result)
                    {
                        var value = _payrollMasterRepository.GetPayrollESISummary(TenantID, LocationID, item.YearSpan);
                        if (value.Count > 0)
                        {
                            item.detailsByMonth = value;
                            result[0].AmountPaid = item.detailsByMonth[0].AmountPaid;
                            result[0].ESIDate = item.ESIDate;
                            result[0].TotalEmployee = item.detailsByMonth[0].EmpTotals;
                        }
                    }
                    response = new Response<List<YearESISpanValue>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<YearESISpanValue>>(ex.Message, 500);
            }
            return response;
        }
        public Response<PayrollRtnESIBO> GetPayrollEmpESISummary(int TenantID, int LocationID, int SalaryPeriodID)
        {
            Response<PayrollRtnESIBO> response;
            try
            {
                PayrollRtnESIBO objrtn = new PayrollRtnESIBO();
                objrtn.detailsByMonth = _payrollMasterRepository.GetPayrollEmpESISummary(SalaryPeriodID);
                if (objrtn.detailsByMonth.Count == 0)
                {
                    response = new Response<PayrollRtnESIBO>(objrtn, 200, "Data not Found");
                }
                else
                {
                    objrtn.detailsByDowload = _commonRepository.GetStgDownloadDoc(TenantID,0, SalaryPeriodID, "PayrollESIFile");
                    // DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                    // var doc = objdoc.GetDocument(0, SalaryPeriodID, "", "PayrollESIFile", TenantID);
                    // if (doc.Count > 0)
                    // {
                    //     foreach (var item in objrtn.detailsByDowload)
                    //     {
                            
                    //         if (empdet != null)
                    //         {
                    //             objrtn.detailsByDowload = empdet;
                                
                    //         }
                    //     }
                    // }
                    response = new Response<PayrollRtnESIBO>(objrtn, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<PayrollRtnESIBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> SaveMasterSturcture(PayrolloverallStructDetails objPayroll)
        {
            Response<SaveOut> response;
            try
            {
                SaveOut result = new SaveOut();
                result = _payrollMasterRepository.SaveMasterSturcture(objPayroll);
                if (result.Id == 0)
                {
                    response = new Response<SaveOut>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<SaveOut>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> SaveGroupTypeLineItem(GroupSalaryStructBO objPayroll)
        {
            Response<SaveOut> response;
            try
            {
                SaveOut result = new SaveOut();                
                result = _payrollMasterRepository.SaveGroupTypeLineItem(objPayroll);                                
                if (result.Id == 0)
                {
                    response = new Response<SaveOut>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<SaveOut>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<GroupSalaryStructBO>> GetPayrollMasterStructure(int TenantID, int LocationID, int StructureID)
        {
            Response<List<GroupSalaryStructBO>> response;
            try
            {
                var result = _payrollMasterRepository.GetPayrollMasterStructure(TenantID, LocationID, StructureID);
                if (result.Count == 0)
                {
                    response = new Response<List<GroupSalaryStructBO>>(result, 200, "Data not Found");
                }
                else
                {
                    foreach (var item in result)
                    {
                        item.LineItem = _payrollMasterRepository.GetPayrollStructLine(item.StructureID,0,0,false);
                        if (item.StructureID > 0)
                        {
                            var payroll = _payrollMasterRepository.savestandardPayroll(TenantID, LocationID, item.StructureID);
                        }
                        if (item.LineItem.Count > 0)
                        {
                            foreach (var item1 in item.LineItem)
                            {
                                item1.rules = _payrollMasterRepository.GetPayrollRules(TenantID, LocationID, item1.salaryLineItemID);
                            }
                        }
                    }
                    response = new Response<List<GroupSalaryStructBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<GroupSalaryStructBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> SavePayrollMasterRules(PayrollrulesBO objPayroll)
        {
            Response<SaveOut> response;
            try
            {
                SaveOut result = new SaveOut();       
                if(objPayroll.PayrollRuleID == 0)
                {
                    var num = _administrativeRepository.GenSequenceNo(objPayroll.TenantID,objPayroll.LocationID,"PayrollRule");
                    objPayroll.PayrollRuleItemCode=num.Id;
                }         
                result = _payrollMasterRepository.SavePayrollMasterRules(objPayroll);                                
                if (result.Id == 0)
                {
                    response = new Response<SaveOut>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<SaveOut>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> SaveOrderLineItem(List<PayrolloverallStructDetails> objPayroll)
        {
            Response<SaveOut> response;
            try
            {
                SaveOut result = new SaveOut();
                int i = 0;
                foreach (var item in objPayroll)
                {
                    item.salaryItemorder = i;
                    result = _payrollMasterRepository.SavePayrollMasterLineIteam(item);
                    i++;                    
                }                
                response = new Response<SaveOut>(result, 200, "Data Retrieved");
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> SavePayrolllOverAllLineItem(PayrolloverallStructDetails objPayroll)
        {
            Response<SaveOut> response;
            try
            {
                SaveOut result = new SaveOut();
                if(objPayroll.GroupTypeID > 0)
                    result = _payrollMasterRepository.SaveMasterSturcture(objPayroll); 
                else
                 result.Id = 1;             

                if (result.Id == 0)
                {
                    response = new Response<SaveOut>(result, 200, "Data not Found");
                }
                else
                {
                    if(objPayroll.GroupTypeID > 0 && objPayroll.isstanditeams && !string.IsNullOrEmpty(objPayroll.salaryLineItem))
                      objPayroll.StructureID = result.Id;
                    // if(objPayroll.StructureID == 0)
                    //     objPayroll.StructureID = result.Id;
                    result = _payrollMasterRepository.SavePayrollMasterLineIteam(objPayroll);
                    if(objPayroll.rules.Count > 0)
                    {
                        foreach (var obj in objPayroll.rules)
                        {
                            if(obj.PayrollRuleID==0)
                            {
                                var num = _administrativeRepository.GenSequenceNo(obj.TenantID,obj.LocationID,"PayrollRule");
                                obj.PayrollRuleItemCode=num.Id;
                            }
                            obj.SalLineItemID = result.Id;
                            var mm = _payrollMasterRepository.SavePayrollMasterRules(obj);
                        }
                    }
                    //_payrollMasterRepository.GetPayrollMasterStructure(objPayroll.TenantID,objPayroll.LocationID);
                    response = new Response<SaveOut>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> deletepayrollstruct(int StructureID)
        {
            Response<SaveOut> response;
            try
            {
                var result = _payrollMasterRepository.deletepayrollstruct(StructureID);
                if (result.Id == 0)
                {
                    response = new Response<SaveOut>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<SaveOut>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> deletepayrolliteamdetails(int SalLineItemDetID)
        {
            Response<SaveOut> response;
            try
            {
                var result = _payrollMasterRepository.deletepayrolliteamdetails(SalLineItemDetID);
                if (result.Id == 0)
                {
                    response = new Response<SaveOut>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<SaveOut>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<PayrollrulesBO>> GetPayRollRuleDropDown(int TenantID, int LocationID, int SalLineItemID)
        {
            Response<List<PayrollrulesBO>> response;
            try
            {
                var result = _payrollMasterRepository.GetPayrollRules(TenantID, LocationID, SalLineItemID);
                if (result.Count == 0)
                {
                    response = new Response<List<PayrollrulesBO>>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<List<PayrollrulesBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<PayrollrulesBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<PayrollRtrnStructLineDetails> GetPayrollSalaryStructLineDetails(int StructureID,int TenantID,int LocationID,bool isstanditeams)
        {
            Response<PayrollRtrnStructLineDetails> response;
            PayrollRtrnStructLineDetails objreturn = new PayrollRtrnStructLineDetails();
            try
            {
                if(StructureID > 0)
                    objreturn.Groupstructure = _payrollMasterRepository.GetPayrollMasterStructure(TenantID, LocationID, StructureID);
                objreturn.Strcture = _payrollMasterRepository.GetPayrollStructLine(StructureID, TenantID, LocationID, isstanditeams);
                if (objreturn.Strcture.Count == 0)
                {
                    response = new Response<PayrollRtrnStructLineDetails>(objreturn, 200, "Data not Found");
                }
                else
                {
                    foreach (var item in objreturn.Strcture)
                    {
                      item.rules = _payrollMasterRepository.GetPayrollRules(item.TenantID, item.LocationID, item.SalLineItemDetID);                        
                    }
                    response = new Response<PayrollRtrnStructLineDetails>(objreturn, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<PayrollRtrnStructLineDetails>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> GetPayrollSalaryStructLineData(int StructureID,int TenantID,int LocationID,bool isstanditeams)
        {
            Response<SaveOut> response;
            SaveOut result = new SaveOut();
            try
            {
                // if(StructureID > 0)
                //     objreturn.Groupstructure = _payrollMasterRepository.GetPayrollMasterStructure(TenantID, LocationID, StructureID);
                var result1 = _payrollMasterRepository.GetPayrollStructLine(0, TenantID, LocationID, false);
                if (result1.Count == 0)
                { 
                    response = new Response<SaveOut>(result, 200, "Data not Found");
                }
                else
                {
                    foreach (var item in result1)
                    {
                        item.rules = _payrollMasterRepository.GetPayrollRules(item.TenantID,item.LocationID,item.SalLineItemDetID);
                        item.SalLineItemDetID = 0;
                        item.StructureID = StructureID;
                        result =   _payrollMasterRepository.SavePayrollMasterLineIteam(item);
                        if(result.Id > 0)
                        {
                        if(item.rules.Count > 0)
                        {
                            foreach (var item1 in item.rules)
                            {
                                item1.SalLineItemID = result.Id;
                                item1.PayrollRuleID = 0;
                                result = _payrollMasterRepository.SavePayrollMasterRules(item1);
                            }
                        }
                        }
                    }
                    response = new Response<SaveOut>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<GroupSalaryStructBO>> GetPayrollMasterSalaryStructure(int TenantID, int LocationID)
        {
            Response<List<GroupSalaryStructBO>> response;
            try
            {
                var result = _payrollMasterRepository.GetPayrollMasterStructure(TenantID, LocationID, 0);
                if (result.Count == 0)
                {
                    response = new Response<List<GroupSalaryStructBO>>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<List<GroupSalaryStructBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<GroupSalaryStructBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<PayrolloverallStructDetails>> GetPayrollStructLine(int TenantID, int LocationID, int StructureID)
        {
            Response<List<PayrolloverallStructDetails>> response;
            try
            {
                var result = _payrollMasterRepository.GetPayrollStructLine(StructureID,TenantID,LocationID,false);
                if (result.Count == 0)
                {
                    response = new Response<List<PayrolloverallStructDetails>>(result, 200, "Data not Found");
                }
                else
                {
                    foreach (var item in result)
                    {
                        item.rules = _payrollMasterRepository.GetPayrollRules(TenantID, LocationID, item.SalLineItemDetID);
                    }
                    response = new Response<List<PayrolloverallStructDetails>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<PayrolloverallStructDetails>>(ex.Message, 500);
            }
            return response;
        }
        //master save call
        public Response<SaveOut> SavePayrolllOverAllStructureLineItem(PayrolloverallStructDetails objPayroll)
        {
            Response<SaveOut> response;
            try
            {
                SaveOut result = new SaveOut();
                //result = _payrollMasterRepository.SaveMasterSturcture(objPayroll);   
                result = _payrollMasterRepository.SavePayrollMasterLineIteam(objPayroll);           

                if (result.Id == 0)
                {
                    response = new Response<SaveOut>(result, 200, "Data not Found");
                }
                else
                {
                    // if(objPayroll.StructureID == 0)
                    //     objPayroll.StructureID = result.Id;
                    if(objPayroll.rules.Count > 0)
                    {
                        foreach (var obj in objPayroll.rules)
                        {
                            if(obj.PayrollRuleID==0)
                            {
                                var num = _administrativeRepository.GenSequenceNo(obj.TenantID,obj.LocationID,"PayrollRule");
                                obj.PayrollRuleItemCode=num.Id;
                            }
                            obj.SalLineItemID = result.Id;
                            var mm = _payrollMasterRepository.SavePayrollMasterRules(obj);
                        }
                    }
                    response = new Response<SaveOut>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message, 500);
            }
            return response;
        }
    }
}