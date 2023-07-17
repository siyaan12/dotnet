using Microsoft.AspNetCore.Mvc;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.Employee;
using MyDodos.ViewModel.Employee;
using MyDodos.Service.Employee;
using MyDodos.Service.Logger;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using MyDodos.Domain.Employee;
using MyDodos.ViewModel.Common;
using MyDodos.Domain.Master;
using MyDodos.Domain.Payroll;
using MyDodos.Domain.HR;
using MyDodos.ViewModel.HR;
using MyDodos.Service.HR;

namespace MyDodos.API.Controllers.Employee
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeService _employeeService;
        private readonly IOnBoardService _onboardService;
        private readonly ILoggerManager _logger;
        public EmployeeController(IEmployeeRepository employeeRepository, IEmployeeService employeeService, IOnBoardService onboardService)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
            _onboardService = onboardService;
        }
        [Authorize]
        [HttpPost("GetHRDirectory")]
        public ActionResult<Response<GetHRDirectoryList>> GetHRDirectoryList(GetHRDirectoryList objresults)
        {
            try
            {
//                 string accessTokenWithBearerPrefix = Request.Headers[HeaderNames.Authorization];
//                 string accessTokenWithoutBearerPrefix = accessTokenWithBearerPrefix.Substring("Bearer ".Length);
//                 var handler = new JwtSecurityTokenHandler();
//                     var jsonToken = handler.ReadToken(accessTokenWithoutBearerPrefix);
//                     var data = jsonToken.ToString().Split("}.")[1];
                var result = _employeeService.GetHRDirectoryList(objresults);
                if (result.StatusCode == 200)
                {
                    //result.Data.objHRDirectoryList[0].DetEncrpt = data;
            //         Password_Encrypt_Decrypt objEncDec = new Password_Encrypt_Decrypt();
            //     string Pass = objEncDec.Encrypt(accessTokenWithoutBearerPrefix);
            //    result.Data.objHRDirectoryList[0].DetDecrypt = Pass;
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
        [HttpGet("GetManagerList/{EmpID}/{TenantID}/{LocationID}")]
        public ActionResult<Response<ManagerBO>> GetManagerList(int EmpID, int TenantID,int LocationID)
        {
            try
            {
                var result = _employeeRepository.GetManagerList(EmpID, TenantID, LocationID);
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
                return StatusCode(500, new Response<ManagerBO>(ex.Message, 500));
            }
        }
        [HttpGet("GetUserView/{EmpID}/{LoginID}/{TenantID}")]
        public ActionResult<Response<UserViewBO>> GetUserView(int EmpID, int LoginID,int TenantID)
        {
            try
            {
                var result = _employeeService.GetUserView(EmpID, LoginID, TenantID);
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
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpGet("GetEmployeeViewDetails/{EmpID}/{LocationID}/{TenantID}")]
        public ActionResult<Response<HRDirectoryEmpView>> GetEmployeeViewDetails(int EmpID, int LocationID,int TenantID)
        {
            try
            {
                var result = _employeeService.GetEmployeeViewDetails(EmpID, LocationID, TenantID);
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
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpGet("GetIndividualEmpOrgzChart/{EmpID}")]
        public ActionResult<Response<OrgChartBO>> GetIndividualEmpOrgzChart(int EmpID)
        {
            try
            {
                var result = _employeeService.GetIndividualEmpOrgzChart(EmpID);
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
                return StatusCode(500, new Response<OrgChartBO>(ex.Message, 500));
            }
        }
        [HttpGet("GetIndividualOrgzChart/{EmpID}/{ManagerID}/{LocationID}/{TenantID}")]
        public ActionResult<Response<EmpReportingOrgBO>> GetIndividualOrgzChart(int EmpID,int ManagerID, int LocationID, int TenantID)
        {
            try
            {
                var result = _employeeService.GetIndividualOrgzChart(EmpID, ManagerID, LocationID, TenantID);
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
                return StatusCode(500, new Response<List<EmpReportingOrgBO>>(ex.Message, 500));
            }
        }
        [HttpPost("SaveEmployeeProfile")]
        public ActionResult<Response<int>> SaveEmployeeProfile(EmployeeProfileBO profile)
        {
            Response<int> response = new Response<int>();
            try
            {
                var result = _employeeService.SaveEmployeeProfile(profile);                
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
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpGet("GetEmployeeProfile/{AppUserID}/{ProductID}")]
        public ActionResult<Response<EmployeeProfileInputBO>> GetEmployeeProfile(int AppUserID,int ProductID)
        {
            try
            {
                var result = _employeeService.GetEmployeeProfile(AppUserID,ProductID);
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
                return StatusCode(500, new Response<EmployeeProfileInputBO>(ex.Message, 500));
            }
        }
        [HttpPost("SaveCompanyProfile")]
        public ActionResult<Response<int>> SaveCompanyProfile(CompanyProfileBO profile)
        {
            Response<int> response = new Response<int>();
            try
            {
                var result = _employeeService.SaveCompanyProfile(profile);                
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
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpGet("GetTenantProfile/{TenantID}/{ProductID}")]
        public ActionResult<Response<TenantProfileImageBO>> GetTenantProfile(int TenantID,int ProductID)
        {
            try
            {
                var result = _employeeService.GetTenantProfile(TenantID,ProductID);
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
                return StatusCode(500, new Response<TenantProfileImageBO>(ex.Message, 500));
            }
        }
        [HttpPost("SaveLocation")]
        public ActionResult<Response<SaveOut>> SaveLocation(LocationdetBO objlocation)
        {
            try
            {
                var result = _employeeService.SaveLocation(objlocation);                
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
                return StatusCode(500, new Response<SaveOut>(ex.Message, 500));
            }
        }
        [HttpGet("GetPayRollEmployees/{TenantID}/{LocationID}/{EmpID}")]
        public ActionResult<Response<List<vwPayrollUser>>> GetPayRollEmployees(int TenantID,int LocationID,int EmpID)
        {
            try
            {
                var result = _employeeService.GetPayRollEmployees(TenantID,LocationID,EmpID);
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
                return StatusCode(500, new Response<List<vwPayrollUser>>(ex.Message));
            }
        }
        [HttpPost("GetEmployeePayInfo")]
        public ActionResult<Response<OnboardingEmpBenefitsBO>> GetEmployeePayInfo(SalaryonboardPaycycle objPaycycle)
        {
            try
            {
                var result = _onboardService.GetEmployeePayrollCycle(objPaycycle);
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
                return StatusCode(500, new Response<List<OnboardingBenefitsBO>>(ex.Message, 500));
            }            
        }
        [HttpGet("GetBenefitsByEmp/{EmpID}/{TenantID}/{LocationID}")]
        public ActionResult<Response<OnboardingBenefitsBO>> GetBenefitsByEmp(int EmpID,int TenantID,int LocationID)
        {
            try
            {
                var result = _employeeService.GetBenefitsByEmp(EmpID,TenantID,LocationID);
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
                return StatusCode(500, new Response<List<OnboardingBenefitsBO>>(ex.Message, 500));
            }
        }
        [HttpGet("GetMyTeam/{TenantID}/{EmpID}")]
        public ActionResult<Response<MyTeamBO>> GetMyTeam(int TenantID,int EmpID)
        {
            try
            {
                var result = _employeeService.GetMyTeam(TenantID,EmpID);
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
                return StatusCode(500, new Response<MyTeamBO>(ex.Message, 500));
            }
        }
        [HttpGet("GetDesignation/{TenantID}")]
        public ActionResult<Response<List<EntRoles>>> GetDesignation(int TenantID)
        {
            try
            {
                var result = _employeeService.GetDesignation(TenantID);
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
                return StatusCode(500, new Response<List<EntRoles>>(ex.Message, 500));
            }
        }
        [HttpPost("EditOfficeDetails")]
        public ActionResult<Response<SaveOut>> UpdateEmployeeDesignation(EmployeeInfoBO employee)
        {
            try
            {
                var result = _employeeService.UpdateEmployeeDesignation(employee);
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
                return StatusCode(500, new Response<SaveOut>(ex.Message, 500));
            }
        }
        [HttpGet("GetManagers/{TenantID}")]
        public ActionResult<Response<List<EmpManagerDropdown>>> GetManagers(int TenantID)
        {
            try
            {
                var result = _employeeService.GetManagers(TenantID);
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
                return StatusCode(500, new Response<List<EmpManagerDropdown>>(ex.Message, 500));
            }
        }
    }
}