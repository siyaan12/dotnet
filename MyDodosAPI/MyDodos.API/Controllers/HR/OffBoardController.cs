using Microsoft.AspNetCore.Mvc;
using MyDodos.Domain.HR;
using MyDodos.Domain.AuthBO;
using MyDodos.Domain.Document;
using MyDodos.Domain.AzureStorage;
using MyDodos.Domain.Wrapper;
using MyDodos.Service.HR;
using MyDodos.Service.Logger;
using MyDodos.Repository.HR;
using MyDodos.ViewModel.HR;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using MyDodos.ViewModel.Business;

namespace MyDodos.API.Controllers.HR
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OffBoardController : ControllerBase
    {
        private readonly IOffBoardService _offBoardService;
        private readonly IOffBoardRepository _offBoardRepository;
        private readonly ILoggerManager _logger;
        public OffBoardController(IOffBoardService offBoardService,IOffBoardRepository offBoardRepository)
        {
            _offBoardRepository = offBoardRepository;
            _offBoardService = offBoardService;
        }
        [HttpPost("SearchOffboardList")]
        public ActionResult<Response<OffBoardSearchBO>> SearchOffboardingList(OffBoardSearchBO search)
        {
            try
            {
                var result = _offBoardService.SearchOffboardingList(search);
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
        [HttpPost("SearchOffboardingList")]
        public ActionResult<Response<List<BPCheckListDetail>>> SearchOffboardList(BPCheckListDetail list)
        {
            try
            {
                var result = _offBoardService.SearchOffboardList(list.FirstName,list.TenantID);
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
                return StatusCode(500, new Response<List<int>>(ex.Message, 500));
            }
        }
        [HttpGet("ViewOffboardListTrack/{ChkListInstanceID}")]
        public ActionResult<Response<BPCheckListDetails>> ViewOffboardListTrack(int ChkListInstanceID)
        {
            try
            {
                var result = _offBoardService.ViewOffboardListTrack(ChkListInstanceID);
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
        [HttpGet("RecentExitOffBoarding/{TenantID}")]
        public ActionResult<Response<List<RecentExitOffBoardBO>>> RecentExitOffBoarding(int TenantID)
        {
            try
            {
                var result = _offBoardService.RecentExitOffBoarding(TenantID);
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
                return StatusCode(500, new Response<List<int>>(ex.Message, 500));
            }
        }
        [HttpPost("CompleteOffBoarding")]
        public ActionResult<Response<CompleteOffBoardingBO>> CompleteOffBoarding(CompleteOffBoardingBO complete)
        {
            try
            {
                var result = _offBoardService.CompleteOffBoarding(complete);
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
        [HttpPost("GetOffboardRequest")]
        public ActionResult<Response<List<BPCheckListDetail>>> GetOffboardRequest(BPCheckListDetail list)
        {
            try
            {
                var result = _offBoardService.GetOffboardRequest(list.TenantID,list.ChkListInstanceID,list.RequestStatus);
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
                return StatusCode(500, new Response<List<int>>(ex.Message, 500));
            }
        }
        [HttpGet("GetOffBoardTrack/{BProcessID}/{ReqInitID}")]
        public ActionResult<Response<List<BPTransInstance>>> GetOffBoardTrack(int BProcessID, int ReqInitID)
        {
            try
            {
                var result = _offBoardService.GetOffBoardTrack(BProcessID,ReqInitID);
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
                return StatusCode(500, new Response<List<int>>(ex.Message, 500));
            }
        }
        [HttpDelete("DeleteoffboardingRequest/{ChkListInstanceID}")]
        public ActionResult<Response<OnBoardRequestModelMsg>> DeleteoffboardingRequest(int ChkListInstanceID)
        {
            try
            {
                var result = _offBoardService.DeleteoffboardingRequest(ChkListInstanceID);
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
        [HttpGet("GetReqDetails/{ChkListInstanceID}")]
        public ActionResult<Response<ReqDetails>> GetReqDetails(int ChkListInstanceID)
        {
            try
            {
                var result = _offBoardService.GetReqDetails(ChkListInstanceID);
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
                return StatusCode(500, new Response<ReqDetails>(ex.Message, 500));
            }
        }
        [HttpGet("GetResignLetter/{ChkListInstanceID}/{ProductID}")]
        public ActionResult<Response<ReturnDocDetailBO>> GetResignLetter(int ChkListInstanceID,int ProductID)
        {
            try
            {
                var result = _offBoardService.GetResignLetter(ChkListInstanceID,ProductID);
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
                return StatusCode(500, new Response<ReturnDocDetailBO>(ex.Message, 500));
            }
        }
        [HttpGet("GetOffBoardingRequest/{TenantID}")]
        public ActionResult<Response<List<BPCheckListDetail>>> GetOffBoardingRequest(int TenantID)
        {
            try
            {
                var result = _offBoardService.GetOffBoardingRequest(TenantID);
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
                return StatusCode(500, new Response<List<BPCheckListDetail>>(ex.Message, 500));
            }
        }
        [AllowAnonymous]
        [HttpPost("CreateCheckListInstance")]
        public ActionResult<Response<OnBoardRequestModelMsg>> CreateCheckListInstance([FromForm] DocDetailBO docBO)
        {
            try
            {
                var inputJson = JsonConvert.DeserializeObject<GenCheckListInstance>(Request.Form["InputJson"]);
                var result = _offBoardService.CreateCheckListInstance(inputJson,docBO);
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
                //_logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<OnBoardRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpGet("GetOffBoardReqChkLists/{ChkListInstanceID}/{TenantID}")]
        public ActionResult<Response<List<BPCheckListDetail>>> GetOffBoardReqChkLists(int ChkListInstanceID,int TenantID)
        {
            try
            {
                var result = _offBoardService.GetOffBoardReqChkLists(ChkListInstanceID,TenantID);
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
                return StatusCode(500, new Response<List<BPCheckListDetail>>(ex.Message, 500));
            }
        }
        [HttpGet("GetOffBoardReqCheckList/{ChkListInstanceID}")]
        public ActionResult<Response<List<BPchecklistDetBO>>> GetOffBoardReqCheckList(int ChkListInstanceID)
        {
            try
            {
                var result = _offBoardService.GetOffBoardReqCheckList(ChkListInstanceID);
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
                return StatusCode(500, new Response<List<BPchecklistDetBO>>(ex.Message, 500));
            }
        }
        [HttpPost("UpdateCheckListItem")]
        public ActionResult<Response<OnBoardRequestModelMsg>> UpdateCheckListItem(List<UpdateCheckList> list)
        {
            try
            {
                var result = _offBoardService.UpdateCheckListItem(list);
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
        [HttpGet("GetEmployee/{TenantID}/{LocationID}/{EmpID}")]
        public ActionResult<Response<EmployeeInfoBO>> GetEmpOffBoardInfo(int TenantID, int LocationID,int EmpID)
        {
            try
            {
                var result = _offBoardService.GetEmpOffBoardInfo(TenantID,LocationID,EmpID);
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
                return StatusCode(500, new Response<EmployeeInfoBO>(ex.Message, 500));
            }
        }
        [HttpPost("AddGenCheckListDetail")]
        public ActionResult<Response<OnBoardRequestModelMsg>> AddGenCheckListDetail(BPCheckList list)
        {
            try
            {
                var result = _offBoardService.AddGenCheckListDetail(list.EmpID,list.LocationID,list.TenantID);
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
        [HttpPost("GetOffboardRequestSearch")]
        public ActionResult<Response<OffboardRequestSearchBO>> GetOffboardRequestSearch(OffboardRequestSearchBO search)
        {
            try
            {
                var result = _offBoardService.GetOffboardRequestSearch(search);
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