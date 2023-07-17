using Microsoft.AspNetCore.Mvc;
using MyDodos.Domain.AuthBO;
using MyDodos.Domain.ProjectManagement;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.Wrapper;
using MyDodos.Domain.Holiday;
using MyDodos.Repository.ProjectManagement;
using MyDodos.Service.Auth;
using MyDodos.Service.ProjectManagement;
using MyDodos.Service.Logger;
using MyDodos.ViewModel.ProjectManagement;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace MyDodos.API.Controllers.ProjectManagement
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectService _projectService;
        private readonly ILoggerManager _logger;
        public ProjectController(IProjectRepository projectRepository, IProjectService projectService)
        {
            _projectRepository = projectRepository;
            _projectService = projectService;
        }
        [HttpPost("SavePPSponsor")]
        public ActionResult<Response<LeaveRequestModelMsg>> SavePPSponsor(PPSponsorBO sponsor)
        {
            try
            {
                var result = _projectService.SavePPSponsor(sponsor);
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
                return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpGet("getPPSponsor/{TenantID}/{LocationID}/{SponsorID}")]
        public ActionResult<Response<List<PPSponsorBO>>> GetPPSponsor(int TenantID, int LocationID, int SponsorID)
        {
            try
            {
                var result = _projectService.GetPPSponsor(TenantID, LocationID, SponsorID);
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
                return StatusCode(500, new Response<List<PPSponsorBO>>(ex.Message, 500));
            }
        }
        [HttpDelete("DeletePPSponsor/{SponsorID}")]
        public ActionResult<Response<LeaveRequestModelMsg>> DeletePPSponsor(int SponsorID)
        {
            try
            {
                var result = _projectService.DeletePPSponsor(SponsorID);
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
                return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpPost("SavePPInitiativeType")]
        public ActionResult<Response<LeaveRequestModelMsg>> SavePPInitiativeType(PPInitiativeTypeBO type)
        {
            try
            {
                var result = _projectService.SavePPInitiativeType(type);
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
                return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpGet("getPPInitiativeType/{TenantID}/{LocationID}/{InitiativeTypeID}")]
        public ActionResult<Response<List<PPInitiativeTypeBO>>> GetPPInitiativeType(int TenantID, int LocationID, int InitiativeTypeID)
        {
            try
            {
                var result = _projectService.GetPPInitiativeType(TenantID, LocationID, InitiativeTypeID);
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
                return StatusCode(500, new Response<List<PPSponsorBO>>(ex.Message, 500));
            }
        }
        [HttpDelete("DeletePPInitiativeType/{InitiativeTypeID}")]
        public ActionResult<Response<LeaveRequestModelMsg>> DeletePPInitiativeType(int InitiativeTypeID)
        {
            try
            {
                var result = _projectService.DeletePPInitiativeType(InitiativeTypeID);
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
                return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpPost("SaveNewProject")]
        public ActionResult<Response<LeaveRequestModelMsg>> SaveNewProject(PPProjectBO project)
        {
            try
            {
                var result = _projectService.SaveNewProject(project);
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
                return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpDelete("DeleteProject/{ProjectID}")]
        public ActionResult<Response<LeaveRequestModelMsg>> DeleteProject(int ProjectID)
        {
            try
            {
                var result = _projectService.DeleteProject(ProjectID);
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
                return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpPost("SaveProjectDetail")]
        public ActionResult<Response<LeaveRequestModelMsg>> SaveProject(PPProjectBO project)
        {
            try
            {
                var result = _projectService.SaveProject(project);
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
                return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpGet("GetProjectDetails/{TenantID}/{LocationID}/{ProjectID}")]
        public ActionResult<Response<List<vwProjectDetails>>> GetProjectDetails(int TenantID, int LocationID,int ProjectID)
        {
            try
            {
                var result = _projectService.GetProjectDetails(TenantID, LocationID,ProjectID);
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
        [HttpGet("GetProjectDetailList/{TenantID}/{LocationID}/{ProjectID}")]
        public ActionResult<Response<List<vwProjectDetailList>>> GetProjectDetailList(int TenantID, int LocationID,int ProjectID)
        {
            try
            {
                var result = _projectService.GetProjectDetailList(TenantID, LocationID,ProjectID);
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
        [HttpPost("GetProjectData")]
        public ActionResult<Response<GetProjectList>> GetProjectData(GetProjectList project)
        {
            try
            {
                var result = _projectService.GetProjectData(project);
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
                return StatusCode(500, new Response<GetProjectList>(ex.Message, 500));
            }
        }
        [HttpPost("SavePPProjectTeam")]
        public ActionResult<Response<LeaveRequestModelMsg>> SaveProjectTeamMember(PPProjectTeam project)
        {
            try
            {
                var result = _projectService.SaveProjectTeamMember(project);
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
                return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpGet("GetProjectMemberDropDown/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<GetDepartmentdropdowm>>> GetProjectMemberDropDown(int TenantID, int LocationID)
        {
            try
            {
                var result = _projectService.GetProjectMemberDropDown(TenantID, LocationID);
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
        [HttpPost("GetProjectMemberDetails")]
        public ActionResult<Response<List<ProjectMemberDetailBO>>> GetProjectMemberDetails(TimesheetInputBO timeSheet)
        {
            try
            {
                var result = _projectService.GetProjectMemberDetails(timeSheet);
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
        [HttpGet("GetProjectManagerDropDown/{TenantID}/{LocationID}/{ProjectID}")]
        public ActionResult<Response<List<ProjectManagerDropDownBO>>> GetProjectManager(int TenantID, int LocationID,int ProjectID)
        {
            try
            {
                var result = _projectService.GetProjectManager(TenantID, LocationID,ProjectID);
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
        [HttpPost("SaveProjectTask")]
        public ActionResult<Response<LeaveRequestModelMsg>> SaveProjectTask(PPProjectTaskBO task)
        {
            try
            {
                var result = _projectService.SaveProjectTask(task);
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
                return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpPost("GetProjectTaskDetails")]
        public ActionResult<Response<List<PPProjectTaskBO>>> GetProjectTask(PPProjectTaskBO task)
        {
            try
            {
                var result = _projectService.GetProjectTask(task.TenantID, task.LocationID, task.ProjectID, task.MileStoneID);
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
        [HttpPost("SaveMileStone")]
        public ActionResult<Response<LeaveRequestModelMsg>> SaveMileStone(MileStoneBO milestone)
        {
            try
            {
                var result = _projectService.SaveMileStone(milestone);
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
                return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpGet("GetMileStone/{TenantID}/{LocationID}/{ProjectID}")]
        public ActionResult<Response<List<MileStoneBO>>> GetMileStone(int TenantID, int LocationID,int ProjectID)
        {
            try
            {
                var result = _projectService.GetMileStone(TenantID, LocationID,ProjectID);
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
        [HttpPost("SaveProjectTaskHistory")]
        public ActionResult<Response<LeaveRequestModelMsg>> SaveProjectTaskHistory(PPProjectTaskHistoryBO taskHistory)
        {
            try
            {
                var result = _projectService.SaveProjectTaskHistory(taskHistory);
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
                return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpPost("SaveProjectRole")]
        public ActionResult<Response<LeaveRequestModelMsg>> SaveProjectRole(PPProjectRoleBO role)
        {
            try
            {
                var result = _projectService.SaveProjectRole(role);
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
                return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpGet("GetProjectRoleDetails/{TenantID}/{LocationID}/{ProjectRoleID}")]
        public ActionResult<Response<List<PPProjectRoleBO>>> GetProjectRole(int TenantID, int LocationID,int ProjectRoleID)
        {
            try
            {
                var result = _projectService.GetProjectRole(TenantID, LocationID,ProjectRoleID);
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
        [HttpDelete("DeleteProjectRole/{ProjectRoleID}")]
        public ActionResult<Response<LeaveRequestModelMsg>> DeleteProjectRole(int ProjectRoleID)
        {
            try
            {
                var result = _projectService.DeleteProjectRole(ProjectRoleID);
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
                return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpGet("GetFinancialByProjectRole/{ProjectID}/{TenantID}")]
        public ActionResult<Response<List<FinancialByProjectRole>>> GetFinancialByProjectRole(int ProjectID, int TenantID)
        {
            try
            {
                var result = _projectService.GetFinancialByProjectRole(ProjectID, TenantID);
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
        [HttpGet("GetFinancialByProjectMemberRole/{ProjectID}/{TenantID}")]
        public ActionResult<Response<List<FinancialByProjectRole>>> GetFinancialByProjectMemberRole(int ProjectID, int TenantID)
        {
            try
            {
                var result = _projectService.GetFinancialByProjectMemberRole(ProjectID, TenantID);
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
        [HttpPost("UpdateProjectRoleAmount")]
        public ActionResult<Response<LeaveRequestModelMsg>> UpdateProjectRoleAmount(PPProjectTeam project)
        {
            try
            {
                var result = _projectService.UpdateProjectRoleAmount(project);
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
                return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpGet("GetProjectLists/{TenantID}/{LocationID}/{ProjectID}")]
        public ActionResult<Response<List<PPProjectListBO>>> GetProjectLists(int TenantID, int LocationID ,int ProjectID)
        {
            try
            {
                var result = _projectService.GetProjectLists(TenantID, LocationID, ProjectID);
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
        [HttpPost("SavePPProjectDocumentType")]
        public ActionResult<Response<LeaveRequestModelMsg>> SaveProjectDocumentType(PPProjectDocumentTypeBO type)
        {
            try
            {
                var result = _projectService.SaveProjectDocumentType(type);
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
                return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpGet("GetPPProjectDocumentType/{TenantID}/{LocationID}/{DocumentTypeID}/{ProjectID}")]
        public ActionResult<Response<List<PPProjectDocumentTypeBO>>> GetProjectDocumentType(int TenantID, int LocationID, int DocumentTypeID,int ProjectID)
        {
            try
            {
                var result = _projectService.GetProjectDocumentType(TenantID, LocationID, DocumentTypeID,ProjectID);
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
                return StatusCode(500, new Response<List<PPProjectDocumentTypeBO>>(ex.Message, 500));
            }
        }
        [HttpDelete("DeletePPProjectDocumentType/{DocumentTypeID}")]
        public ActionResult<Response<LeaveRequestModelMsg>> DeletePPProjectDocumentType(int DocumentTypeID)
        {
            try
            {
                var result = _projectService.DeletePPProjectDocumentType(DocumentTypeID);
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
                return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
            }
        }
        [AllowAnonymous]
        [HttpPost("UploadDocumentType")]
        public ActionResult<Response<int>> SaveDocumentType([FromForm] InputDocType objdoc)
        {
            Response<int> response = new Response<int>();
            try
            {
                var inputJson = JsonConvert.DeserializeObject<ProjectDocumentTypeBO>(Request.Form["InputJson"]);
                var retobj = _projectService.SaveDocumentType(objdoc, inputJson);                
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
        [HttpPost("GetPPProjectDocument")]
        public ActionResult<Response<PPProjectDocumentTypeBO>> GetPPProjectDocument(PPProjectDocumentTypeBO type)
        {
            try
            {
                var result = _projectService.GetPPProjectDocument(type.TenantID,type.LocationID,type.DocumentTypeID,type.ProjectID,type.ProductID);
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
                return StatusCode(500, new Response<Response<PPProjectDocumentTypeBO>>(ex.Message, 500));
            }
        }
        [HttpGet("downloadDoc/{ProductID}/{TenantID}/{docKey}")]
        public ActionResult<Response<int>> DownloadDoc(int ProductID,int TenantID,string docKey)
        {
            try
            {
                var result = _projectService.DownloadDoc(ProductID,TenantID, docKey);
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
        [HttpPost("SaveEntityProjectTask")]
        public ActionResult<Response<LeaveRequestModelMsg>> SaveEntityProjectTask(PPProjectTaskBO task)
        {
            try
            {
                var result = _projectService.SaveEntityProjectTask(task);
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
                return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpPost("UpdateRemoveTaskMembers")]
        public ActionResult<Response<LeaveRequestModelMsg>> UpdateRemoveTaskMembers(PPTaskResourceBO task)
        {
            try
            {
                var result = _projectService.UpdateRemoveTaskMembers(task);
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
                return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpDelete("DeleteProjectMileStone/{MileStoneID}")]
        public ActionResult<Response<LeaveRequestModelMsg>> DeleteProjectMileStone(int MileStoneID)
        {
            try
            {
                var result = _projectService.DeleteProjectMileStone(MileStoneID);
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
                return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpDelete("DeleteProjectTask/{PTaskID}")]
        public ActionResult<Response<LeaveRequestModelMsg>> DeleteProjectTask(int PTaskID)
        {
            try
            {
                var result = _projectService.DeleteProjectTask(PTaskID);
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
                return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpGet("GetProjectReport/{TenantID}/{LocationID}/{ProjectID}")]
        public ActionResult<Response<List<ProjectTaskReportBO>>> GetProjectReport(int TenantID, int LocationID,int ProjectID)
        {
            try
            {
                var result = _projectService.GetProjectReport(TenantID, LocationID,ProjectID);
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
        [HttpPost("SavePPProjectPaymentHistory")]
        public ActionResult<Response<LeaveRequestModelMsg>> SavePPProjectPaymentHistory(PPProjectPaymentHistoryBO project)
        {
            try
            {
                var result = _projectService.SavePPProjectPaymentHistory(project);
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
                return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpGet("GetPPProjectPaymentHistory/{TenantID}/{LocationID}/{EmpID}")]
        public ActionResult<Response<List<PPProjectPaymentHistoryBO>>> GetPPProjectPaymentHistory(int TenantID, int LocationID,int EmpID)
        {
            try
            {
                var result = _projectService.GetPPProjectPaymentHistory(TenantID, LocationID,EmpID);
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
        [HttpPost("SaveNewWorkGroupProject")]
        public ActionResult<Response<LeaveRequestModelMsg>> SaveNewWorkGroupProject(PPProjectBO project)
        {
            try
            {
                var result = _projectService.SaveNewWorkGroupProject(project);
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
                return StatusCode(500, new Response<LeaveRequestModelMsg>(ex.Message, 500));
            }
        }
        [HttpGet("GetProjectWorkGroupSummary/{TenantID}/{LocationID}")]
        public ActionResult<Response<List<ProjectWorkGroupSummaryBO>>> GetProjectWorkGroupSummary(int TenantID, int LocationID)
        {
            try
            {
                var result = _projectService.GetProjectWorkGroupSummary(TenantID, LocationID);
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
                return StatusCode(500, new Response<List<ProjectWorkGroupSummaryBO>>(ex.Message, 500));
            }
        }
        [HttpPost("GetProjectWorkGroupLists")]
        public ActionResult<Response<List<PPProjectListBO>>> GetProjectWorkGroupLists(PPProjectListBO project)
        {
            try
            {
                var result = _projectService.GetProjectWorkGroupLists(project);
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
    }
}