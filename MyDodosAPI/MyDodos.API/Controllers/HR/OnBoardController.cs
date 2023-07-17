using Microsoft.AspNetCore.Mvc;
using MyDodos.Domain.HR;
using MyDodos.Domain.AuthBO;
using MyDodos.Domain.AzureStorage;
using MyDodos.Domain.Wrapper;
using MyDodos.Service.HR;
using MyDodos.Service.Logger;
using MyDodos.Repository.HR;
using MyDodos.ViewModel.HR;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using MyDodos.ViewModel.Business;
using MyDodos.ViewModel.Document;
using KoSoft.DocRepo;
using MyDodos.ViewModel.Entitlement;
using MyDodos.Domain.Payroll;
using MyDodos.Domain.BenefitManagement;
using MyDodos.ViewModel.Employee;
using MyDodos.ViewModel.Common;
using MyDodos.Service.HRMS;
using MyDodos.ViewModel.HRMS;

namespace MyDodos.API.Controllers.HR
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OnBoardController : ControllerBase
    {
        private readonly IOnBoardService _onBoardService;
        private readonly IOnBoardRepository _onBoardRepository;
        private readonly ILoggerManager _logger;
        private readonly IHrmsInstanceService _hrmsInstanceService;
        public OnBoardController(IOnBoardService onBoardService,IOnBoardRepository onBoardRepository, IHrmsInstanceService hrmsInstanceService)
        {
            _onBoardService = onBoardService;
            _onBoardRepository = onBoardRepository;
            _hrmsInstanceService = hrmsInstanceService;
        }
        [HttpPost("SaveOnBoardSetting")]
        public ActionResult<Response<int>> UpdateOnBoardSetting(BPProcessBO bpBo)
        {
            try
            {
                var result = _onBoardService.UpdateOnBoardSetting(bpBo);
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
        [HttpPost("GetOnBoardSetting")]
        public ActionResult<Response<List<BPTransaction>>> GetOnBoardSetting(BPTransaction bptrans)
        {
            try
            {
                var result = _onBoardRepository.GetOnBoardSetting(bptrans.TenantID, bptrans.LocationID, bptrans.TransOrder, bptrans.ProcessCategory);
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
                return StatusCode(500, new Response<List<BPTransaction>>(ex.Message, 500));
            }
        }
        [HttpPost("SaveBusinessOnboard")]
        public ActionResult<Response<int>> SaveBusinessOnboard(BPTransInstance bprocess)
        {
            try
            {
                var result = _onBoardRepository.SaveBusinessOnboard(bprocess);
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
        [HttpGet("GetEntRoles/{ProductID}/{GroupType}")]
        public ActionResult<Response<List<EntRolesBO>>> GetEntRoles(int ProductID, string GroupType)
        {
            try
            {
                var result = _onBoardService.GetEntRoles(ProductID, GroupType);
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
                return StatusCode(500, new Response<List<EntRolesBO>>(ex.Message, 500));
            }
        }
        [HttpPost("GetEntTenantRoles")]
        public ActionResult<Response<List<EntRolesBO>>> GetEntTenantRoles(InpurtEntRolesBO roles)
        {
            try
            {
                var result = _onBoardService.GetEntTenantRoles(roles);
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
                return StatusCode(500, new Response<List<EntRolesBO>>(ex.Message, 500));
            }
        }
        [HttpGet("GetAccountTypes/{ProductID}/{TenantID}")]
        public ActionResult<Response<List<UserGroupBO>>> GetAccountTypes(int ProductID, int TenantID)
        {
            try
            {
                var result = _onBoardService.GetAccountTypes(ProductID, TenantID);
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
                return StatusCode(500, new Response<List<UserGroupBO>>(ex.Message, 500));
            }
        }
        [HttpGet("GetCountryProof/{CountryId}")]
        public ActionResult<Response<List<IDProofDocumnent>>> GetMasDocProof(int CountryId)
        {
            try
            {
                var result = _onBoardRepository.GetMasDocProof(CountryId);
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
                return StatusCode(500, new Response<List<IDProofDocumnent>>(ex.Message, 500));
            }
        }
        [HttpPost("SaveOnboardEducation")]
        public ActionResult<Response<int>> SaveOnboardEducation(HRInputEmpEducation onEdu)
        {
            try
            {
                var result = _onBoardService.SaveOnboardEducation(onEdu);
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
        [HttpGet("GetOnboardEducation/{EmpID}")]
        public ActionResult<Response<List<HREmpEducation>>> GetOnboardEducation(int EmpID)
        {
            try
            {
                var result = _onBoardService.GetOnboardEducation(EmpID);
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
                return StatusCode(500, new Response<List<IDProofDocumnent>>(ex.Message, 500));
            }
        }
        [HttpDelete("RemoveEducation/{EmpEduID}")]
        public ActionResult<Response<List<HREmpEducation>>> RemoveEducation(int EmpEduID)
        {
            try
            {
                var result = _onBoardRepository.DeleteOnboardEducation(EmpEduID);
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
                return StatusCode(500, new Response<List<IDProofDocumnent>>(ex.Message, 500));
            }
        }
        [HttpPost("SaveOnBoardingForm")]
        public ActionResult<Response<OnBoardRequestModelMsg>> AddOnBoardingForm(EmpOnboardingModBO onboarding)
        {
            try
            {
                var result = _onBoardService.AddOnBoardingForm(onboarding);
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
                return StatusCode(500, new Response<OnBoardRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpPost("GetOnboardList")]
        public ActionResult<Response<OnboardSearchBO>> GetOnboardList(OnboardSearchBO search)
        {
            try
            {
                var result = _onBoardRepository.GetOnboardList(search);
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
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpGet("HROnboardResource/{productId}/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<OnBoardingResourceBO>>> HROnboardResource(int productId, int TenantID, int LocationID)
        {
            Response<List<OnBoardingResourceBO>> response = new Response<List<OnBoardingResourceBO>>();
            try
            {
                var retobj = _onBoardService.GetHROnboardResource(productId, TenantID, LocationID);                
                if (retobj.StatusCode == 200)
                {
                    return Ok(retobj);
                }
                else
                {
                    return StatusCode(retobj.StatusCode, retobj);
                }  
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<List<OnBoardingResourceBO>>(ex.Message, 500));
            }
        }
        [HttpPost("SaveHROnboardGenral")]
        public ActionResult<Response<OnBoardRequestModelMsg>> SaveHROnboardGenral(OnBoardingGenralBO genral)
        {
            try
            {
                var result = _onBoardService.SaveHROnboardGenral(genral);                
                if (result.StatusCode == 200)
                {
                    if(result.Data.Item2.AppUser != null && genral.ReqStatus == "Completed")
                    {
                      _hrmsInstanceService.GetToHRMS(result.Data.Item1, result.Data.Item2);
                     //return Ok(response = new Response<HRrtnOnboardBO>(res.Data, 200, "Saved Successfully"));
                    }                    
                var response = new Response<OnBoardRequestModelMsg>(result.Data.Item3, 200, "Saved Successfully");
                    return Ok(response);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }  
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<OnBoardRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpGet("GetOnboard/{EmpOnboardingID}")]
        public ActionResult<Response<OnBoardingRequestBO>> GetOnboard(int EmpOnboardingID)
        {
            try
            {
                var result = _onBoardService.GetOnboard(EmpOnboardingID);
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
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpGet("GetOnboardTrack/{EmpOnboardingID}")]
        public ActionResult<Response<OnBoardingRequest>> GetOnboardTrack(int EmpOnboardingID)
        {
            try
            {
                var result = _onBoardService.GetOnboardTrack(EmpOnboardingID,0);
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
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("SaveOnBoardEmployee")]
        public ActionResult<Response<int>> SaveOnBoardEmployee(OnboardPersonalDetail onboarding)
        {
            try
            {
                var result = _onBoardService.SaveOnBoardEmployee(onboarding);
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
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }       
        [HttpDelete("RemoveHROnboard/{EmpOnboardingID}")]
        public ActionResult<Response<OnBoardRequestModelMsg>> DeleteHROnboard(int EmpOnboardingID)
        {
            Response<OnBoardRequestModelMsg> response = new Response<OnBoardRequestModelMsg>();
            try
            {
                var retobj = _onBoardRepository.DeleteHROnboard(EmpOnboardingID);                
                if (retobj.StatusCode == 200)
                {
                    return Ok(retobj);
                }
                else
                {
                    return StatusCode(retobj.StatusCode, retobj);
                }  
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<OnBoardRequestModelMsg>(ex.Message, 500));
            }
        } 
        [HttpPost("GetOnboardManager")]
        public ActionResult<Response<CountryListBO>> GetOnboardManager(OnboardManagerInput manager)
        {
            try
            {
                var result = _onBoardRepository.GetOnboardManager(manager.LocationID, manager.DeptID, manager.RoleID, manager.CategoryName);
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
                return StatusCode(500, new Response<List<CountryListBO>>(ex.Message, 500));
            }
        }
        [HttpGet("GetOnboardHR/{TenantID}/{ProductID}")]
        public ActionResult<Response<CountryListBO>> GetOnboardHR(int TenantID, int ProductID)
        {
            try
            {
                var result = _onBoardRepository.GetOnboardHR(TenantID, ProductID);
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
                return StatusCode(500, new Response<List<CountryListBO>>(ex.Message, 500));
            }
        }
        [AllowAnonymous]
        [HttpPost("SaveHRDocuments")]
        public ActionResult<Response<int>> SaveHRDocuments([FromForm] DocProofFileBo objdoc)
        {
            Response<int> response = new Response<int>();
            try
            {
                var inputJson = JsonConvert.DeserializeObject<DocProofInputBo>(Request.Form["InputJson"]);
                var retobj = _onBoardService.SaveAzureDocuments(objdoc, inputJson);                
                if (retobj.StatusCode == 200)
                {
                    return Ok(retobj);
                }
                else
                {
                    return StatusCode(retobj.StatusCode, retobj);
                }  
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("SaveIDcardDocuments")]
        public ActionResult<Response<int>> SaveIDcardDocuments(InputDocIDCardInputBo _objdoc)
        {
            Response<int> response = new Response<int>();
            try
            {
                var retobj = _onBoardService.SaveIDcardDocuments(_objdoc);                
                if (retobj.StatusCode == 200)
                {
                    return Ok(retobj);
                }
                else
                {
                    return StatusCode(retobj.StatusCode, retobj);
                }  
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpGet("GetDocInfo/{tenantID}/{LocationID}/{EmpID}")]
        public ActionResult<Response<List<GenDocument>>> GetDocInfo(int tenantID, int LocationID, int EmpID)
        {
            try
            {
                var result = _onBoardService.GetDocInfo(tenantID, LocationID, EmpID);
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
                return StatusCode(500, new Response<List<GenDocument>>(ex.Message, 500));
            }
        }
        [HttpGet("downloadDocs/{docId}/{productId}")]
        public ActionResult<Response<AzureDocURLBO>> DownloadDocs(int docId, int productId)
        {
            try
            {
                var result = _onBoardService.DownloadDocs(docId,productId);
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
                return StatusCode(500, new Response<AzureDocURLBO>(ex.Message, 500));
            }
        }
        [HttpDelete("deleteDocs/{docId}/{productId}")]
        public ActionResult<Response<AzureDocURLBO>> DeleteDocs(int docId, int productId)
        {
            try
            {
                var result = _onBoardService.DeleteDocs(docId, productId);
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
                return StatusCode(500, new Response<AzureDocURLBO>(ex.Message, 500));
            }
        }
        [HttpGet("GetIDCardInfo/{EmpID}/{LocationID}/{tenantID}/{EmpIDCardID}")]
        public ActionResult<Response<List<DocIDCardInputBo>>> GetIDCardInfo(int EmpID, int LocationID, int tenantID, int EmpIDCardID)
        {
            try
            {
                var result = _onBoardService.GetIDCardInfo(EmpID, LocationID, tenantID, EmpIDCardID);
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
                return StatusCode(500, new Response<List<DocIDCardInputBo>>(ex.Message, 500));
            }
        }
        [HttpPost("SaveCheckListDetails")]
        public ActionResult<Response<int>> SaveCheckListDetails(ChecklistBO chkList)
        {
            Response<int> response = new Response<int>();
            try
            {
                var retobj = _onBoardService.SaveHRCraeteCheckList(chkList);                
                if (retobj.StatusCode == 200)
                {
                    return Ok(retobj);
                }
                else
                {
                    return StatusCode(retobj.StatusCode, retobj);
                }  
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("SaveWorkplace")]
        public ActionResult<Response<int>> SaveWorkplace(WorkPlaceBO chkList)
        {
            Response<int> response = new Response<int>();
            try
            {
                var retobj = _onBoardService.SaveWorkplace(chkList);                
                if (retobj.StatusCode == 200)
                {
                    return Ok(retobj);
                }
                else
                {
                    return StatusCode(retobj.StatusCode, retobj);
                }  
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("SaveNetwork")]
        public ActionResult<Response<int>> SaveNetwork(NetworkSetupBO chkList)
        {
            Response<int> response = new Response<int>();
            try
            {
                var retobj = _onBoardService.SaveNetworkSetUp(chkList);                
                if (retobj.StatusCode == 200)
                {
                    return Ok(retobj);
                }
                else
                {
                    return StatusCode(retobj.StatusCode, retobj);
                }  
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("GetNetworkDetails")]
        public ActionResult<Response<NetworkSetupBO>> GetNetworkDetails(NetworkSetupBO chkList)
        {
            Response<NetworkSetupBO> response = new Response<NetworkSetupBO>();
            try
            {
                var retobj = _onBoardService.GetNetworkDetails(chkList);                
                if (retobj.StatusCode == 200)
                {
                    return Ok(retobj);
                }
                else
                {
                    return StatusCode(retobj.StatusCode, retobj);
                }  
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<NetworkSetupBO>(ex.Message, 500));
            }
        }
        [HttpPost("GetWorkPlace")]
        public ActionResult<Response<WorkPlaceBO>> GetWorkPlace(WorkPlaceBO chkList)
        {
            Response<WorkPlaceBO> response = new Response<WorkPlaceBO>();
            try
            {
                var retobj = _onBoardService.GetWorkPlace(chkList);                
                if (retobj.StatusCode == 200)
                {
                    return Ok(retobj);
                }
                else
                {
                    return StatusCode(retobj.StatusCode, retobj);
                }  
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<WorkPlaceBO>(ex.Message, 500));
            }
        }
        [HttpPost("GetHRCheckList")]
        public ActionResult<Response<List<BPchecklist>>> GetHRCheckList(BPcheckInputBO chkList)
        {
            Response<int> response = new Response<int>();
            try
            {
                var retobj = _onBoardService.GetHRCheckList(chkList);                
                if (retobj.StatusCode == 200)
                {
                    return Ok(retobj);
                }
                else
                {
                    return StatusCode(retobj.StatusCode, retobj);
                }  
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("SaveOrientation")]
        public ActionResult<Response<int>> SaveOrientation(OrientationBO chkList)
        {
            Response<int> response = new Response<int>();
            try
            {
                var retobj = _onBoardService.SaveHRCheckList(chkList);                
                if (retobj.StatusCode == 200)
                {
                    return Ok(retobj);
                }
                else
                {
                    return StatusCode(retobj.StatusCode, retobj);
                }  
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("GetOrientation")]
        public ActionResult<Response<OrientationBO>> GetOrientation(OrientationBO _work)
        {
            Response<OrientationBO> response = new Response<OrientationBO>();
            try
            {
                var retobj = _onBoardService.GetOrientation(_work);                
                if (retobj.StatusCode == 200)
                {
                    return Ok(retobj);
                }
                else
                {
                    return StatusCode(retobj.StatusCode, retobj);
                }  
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<OrientationBO>(ex.Message, 500));
            }
        }
        [HttpPost("SaveHRAttendnceConfig")]
        public ActionResult<Response<int>> SaveHRAttendnceConfig(AttendanceConfigBO attend)
        {
            Response<int> response = new Response<int>();
            try
            {
                var retobj = _onBoardService.SaveHRAttendnceConfig(attend);                
                if (retobj.StatusCode == 200)
                {
                    return Ok(retobj);
                }
                else
                {
                    return StatusCode(retobj.StatusCode, retobj);
                }  
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpGet("GetHRAttendnceConfig/{EmpID}/{LocationID}/{AttendConfigID}")]
        public ActionResult<Response<List<AttendanceConfigBO>>> GetHRAttendnceConfig(int EmpID, int LocationID,int AttendConfigID)
        {
            try
            {
                var result = _onBoardService.GetHRAttendnceConfig(EmpID, LocationID, AttendConfigID);
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
                return StatusCode(500, new Response<List<AttendanceConfigBO>>(ex.Message, 500));
            }
        }
        [HttpPost("SaveOnboardExperience")]
        public ActionResult<Response<OnBoardRequestModelMsg>> SaveOnboardExperience(HRInputEmpExperience onExp)
        {
            try
            {
                var result = _onBoardService.SaveOnboardExperience(onExp);
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
                return StatusCode(500, new Response<OnBoardRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpGet("GetOnboardEmpExperience/{EmpID}")]
        public ActionResult<Response<List<HREmpExperience>>> GetOnboardEmpExperience(int EmpID)
        {
            try
            {
                var result = _onBoardService.GetOnboardEmpExperience(EmpID);
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
                return StatusCode(500, new Response<List<HREmpExperience>>(ex.Message, 500));
            }
        }
        [HttpDelete("RemoveEmpExperience/{EmpExpID}")]
        public ActionResult<Response<OnBoardRequestModelMsg>> RemoveEmpExperience(int EmpExpID)
        {
            try
            {
                var result = _onBoardRepository.DeleteEmpExperience(EmpExpID);
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
                return StatusCode(500, new Response<OnBoardRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpGet("GetBenefitGroupByGroupType/{TenantID}/{GroupTypeID}")]
        public ActionResult<Response<List<BenefitGroupBO>>> GetBenefitGroupByGroupType(int TenantID,int GroupTypeID)
        {
            try
            {
                var result = _onBoardService.GetBenefitGroupByGroupType(TenantID,GroupTypeID);
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
                return StatusCode(500, new Response<List<BenefitGroupBO>>(ex.Message, 500));
            }
        }
        [HttpGet("GetBenefits/{BenefitGroupID}/{TenantID}/{LocationID}")]
        public ActionResult<Response<OnboardingBenefitsBO>> GetBenefits(int BenefitGroupID,int TenantID,int LocationID)
        {
            try
            {
                var result = _onBoardService.GetBenefits(BenefitGroupID,TenantID,LocationID);
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
        [HttpGet("GetEmployeeBenefits/{EmpID}/{TenantID}/{LocationID}")]
        public ActionResult<Response<EmployeeBenefitsBO>> GetEmployeeBenefits(int EmpID,int TenantID,int LocationID)
        {
            try
            {
                var result = _onBoardService.GetEmployeeBenefits(EmpID,TenantID,LocationID);
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
                return StatusCode(500, new Response<EmployeeBenefitsBO>(ex.Message, 500));
            }
        }
        [HttpPost("SaveOnBoardBenefits")]
        public ActionResult<Response<int>> SaveOnBoardBenefits(OnBoardBenefitGroupBO objgroup)
        {
            try
            {
                var result = _onBoardService.SaveOnBoardBenefits(objgroup);
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
                return StatusCode(500, new Response<int>(ex.Message));
            }
        }
        [HttpPost("GetEmployeePayrollCycle")]
        public ActionResult<Response<OnboardingEmpBenefitsBO>> GetEmployeePayrollCycle(SalaryonboardPaycycle objPaycycle)
        {
            try
            {
                var result = _onBoardService.GetEmployeePayrollCycle(objPaycycle);
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
                return StatusCode(500, new Response<OnboardingEmpBenefitsBO>(ex.Message, 500));
            }            
        }
        [HttpPost("SaveEmployeeCTC")]
        public ActionResult<Response<int>> SaveEmployeeCTC(EmpSalaryStructureCTC objctc)
        {
            try
            {
                var result = _onBoardService.SaveEmpPayrollCTC(objctc);
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
                return StatusCode(500, new Response<int>(ex.Message));
            }
        }
        [HttpPost("SaveEmployeePayRollBenfits")]
        public ActionResult<Response<int>> SaveEmployeePayRollBenfits(PayrollCTCBO objgroup)
        {
            try
            {
                var result = _onBoardService.SaveEmployeePayRollBenfits(objgroup);
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
                return StatusCode(500, new Response<int>(ex.Message));
            }
        }
        [HttpPost("SaveAccountDetails")]
        public ActionResult<Response<int>> SaveAccountDetails(AccountDetailsBO objaccount)
        {
            try
            {
                var result = _onBoardService.SaveAccountDetails(objaccount);
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
                return StatusCode(500, new Response<int>(ex.Message));
            }
        }
        [HttpGet("GetAccountDetails/{EmpID}")]
        public ActionResult<Response<AccountDetailsBO>> GetAccountDetails(int EmpID)
        {
            try
            {
                var result = _onBoardService.GetAccountDetails(EmpID);
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
                return StatusCode(500, new Response<int>(ex.Message));
            }
        }
        [HttpPost("SaveOnBoardSettingDragDrop")]
        public ActionResult<Response<SaveOut>> SaveOnBoardSettingDragDrop(List<BPTransaction> process)
        {
            try
            {
                var result = _onBoardService.SaveOnBoardSettingDragDrop(process);
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
        [HttpPost("SaveEmpDynamicPayrollCTC")]
        public ActionResult<Response<SaveOut>> SaveEmpDynamicPayrollCTC(EmpSalaryStructureCTC objctc)
        {
            try
            {
                var result = _onBoardService.SaveEmpDynamicPayrollCTC(objctc);
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
        [HttpPost("UpdateHRDirEmployee")]
        public ActionResult<Response<int>> UpdateHRDirEmployee(OnboardPersonalDetail onboarding)
        {
            try
            {
                var result = _onBoardService.UpdateHRDirEmployee(onboarding);
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
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }  
    }
}