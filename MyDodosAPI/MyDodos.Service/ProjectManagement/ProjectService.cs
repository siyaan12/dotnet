using Microsoft.Extensions.Configuration;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.Wrapper;
using MyDodos.Domain.ProjectManagement;
using MyDodos.Repository.Administrative;
using MyDodos.Repository.ProjectManagement;
using MyDodos.ViewModel.ProjectManagement;
using MyDodos.Repository.AzureStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KoSoft.DocRepo;
using MyDodos.Domain.AzureStorage;

namespace MyDodos.Service.ProjectManagement
{
    public class ProjectService : IProjectService
    {
        private readonly IConfiguration _configuration;
        private readonly IProjectRepository _projectRepository;
        private readonly ITimeSheetRepository _timeSheetRepository;
        private readonly IStorageConnect _storage;
        private readonly IAdministrativeRepository _administrativeRepository;
        public ProjectService(IConfiguration configuration, IProjectRepository projectRepository,IAdministrativeRepository administrativeRepository,ITimeSheetRepository timeSheetRepository,IStorageConnect storage)
        {
            _configuration = configuration;
            _storage = storage;
            _projectRepository =  projectRepository;
            _administrativeRepository = administrativeRepository;
            _timeSheetRepository = timeSheetRepository;
        }
        public Response<LeaveRequestModelMsg> SavePPSponsor(PPSponsorBO sponsor)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                var result = _projectRepository.SavePPSponsor(sponsor);
                if (result.RequestID == 0)
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"Sponsor Data Creation or Updation is Failed");
                }
                else
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"Saved Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<List<PPSponsorBO>> GetPPSponsor(int TenantID, int LocationID, int SponsorID)
        {
            Response<List<PPSponsorBO>> response;
            try
            {
                var result = _projectRepository.GetPPSponsor(TenantID,LocationID,SponsorID);
                if (result.Count() == 0)
                {
                    response = new Response<List<PPSponsorBO>>(result,200,"Data Not Found");
                }
                else
                {
                    response = new Response<List<PPSponsorBO>>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<PPSponsorBO>>(ex.Message,500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> DeletePPSponsor(int SponsorID)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                var result = _projectRepository.DeletePPSponsor(SponsorID);
                if(result.RequestID == 0)
                {
                response = new Response<LeaveRequestModelMsg>(result,200,"Cannot Deleted");
                }
                else
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"Deleted Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> SavePPInitiativeType(PPInitiativeTypeBO type)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                var result = _projectRepository.SavePPInitiativeType(type);
                if (result.RequestID == 0)
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"PPInitiativeType Data Creation or Updation is Failed");
                }
                else
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"Saved Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<List<PPInitiativeTypeBO>> GetPPInitiativeType(int TenantID, int LocationID, int InitiativeTypeID)
        {
            Response<List<PPInitiativeTypeBO>> response;
            try
            {
                var result = _projectRepository.GetPPInitiativeType(TenantID,LocationID,InitiativeTypeID);
                if (result.Count() == 0)
                {
                    response = new Response<List<PPInitiativeTypeBO>>(result,200,"Data Not Found");
                }
                else
                {
                    response = new Response<List<PPInitiativeTypeBO>>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<PPInitiativeTypeBO>>(ex.Message,500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> DeletePPInitiativeType(int InitiativeTypeID)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                var result = _projectRepository.DeletePPInitiativeType(InitiativeTypeID);
                if(result.RequestID == 0)
                {
                response = new Response<LeaveRequestModelMsg>(result,200,"Cannot Deleted");
                }
                else
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"Deleted Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> SaveNewProject(PPProjectBO project)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                if(project.ProjectID==0)
                {
                    var num =_administrativeRepository.GenSequenceNo(project.TenantID,project.LocationID,"Project");
                    project.ProjectNo=num.Id;
                }
                var result = _projectRepository.SaveNewProject(project);
                if (result.RequestID == 0)
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"Project Data Creation or Updation is Failed");
                }
                else
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"Saved Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> DeleteProject(int ProjectID)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                var result = _projectRepository.DeleteProject(ProjectID);
                if(result.RequestID == 0)
                {
                response = new Response<LeaveRequestModelMsg>(result,200,"Cannot Deleted");
                }
                else
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"Moved To Archieve List");
                }
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> SaveProject(PPProjectBO project)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                var result = _projectRepository.SaveProject(project);
                if (result.RequestID == 0)
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"Project Data Creation or Updation is Failed");
                }
                else
                {
                    // if(project.ManagerID !=null && project.ManagerID !="")
                    // {
                    //     string[] split = project.ManagerID.Split(',');
                    // }
                    response = new Response<LeaveRequestModelMsg>(result,200,"Saved Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<List<vwProjectDetails>> GetProjectDetails(int TenantID,int LocationID,int ProjectID)
        {
            Response<List<vwProjectDetails>> response;
            try
            {
                var result = _projectRepository.GetProjectDetails(TenantID,LocationID,ProjectID);
                if (result.Count() == 0)
                {
                    response = new Response<List<vwProjectDetails>>(result,200,"Data Not Found");
                }
                else
                {
                    response = new Response<List<vwProjectDetails>>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<vwProjectDetails>>(ex.Message,500);
            }
            return response;
        }
        public Response<List<vwProjectDetailList>> GetProjectDetailList(int TenantID,int LocationID,int ProjectID)
        {
            Response<List<vwProjectDetailList>> response;
            try
            {
                var result = _projectRepository.GetProjectDetailList(TenantID,LocationID,ProjectID);
                if (result.Count() == 0)
                {
                    response = new Response<List<vwProjectDetailList>>(result,200,"Data Not Found");
                }
                else
                {
                    foreach(var items in result)
                    {
                        var date = _projectRepository.GetTaskExtendate(items.TenantID,items.ProjectID);
                        items.TaskExtenDate = date;
                        vwProjectDetailList data = new vwProjectDetailList();
                        if(date.Count > 0)
                        {
                            data.EstEndDate = date[0].NewTaskEstEndDate;
                            data.ProjectID = items.ProjectID;
                            data.TenantID = items.TenantID;
                            _projectRepository.UpdateProjectEstEndDate(data);
                        }
                    }
                    var results = _projectRepository.GetProjectDetailList(TenantID,LocationID,ProjectID);
                    foreach(var items in results)
                    {
                        var date = _projectRepository.GetTaskExtendate(items.TenantID,items.ProjectID);
                        items.TaskExtenDate = date;
                    }
                    response = new Response<List<vwProjectDetailList>>(results,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<vwProjectDetailList>>(ex.Message,500);
            }
            return response;
        }
        public Response<GetProjectList> GetProjectData(GetProjectList project)
        {
            Response<GetProjectList> response;
            try
            {
                var result = _projectRepository.GetProjectData(project);         
                if (result.objProjectList.Count() == 0)
                {      
                    response = new Response<GetProjectList>(result, 200, "Data Not Found");
                }
                else
                {   
                    foreach(var item in result.objProjectList)
                    {
                        item.ProjectPercentage = _projectRepository.GetProjectPercentage(item.ProjectID);
                    }
                    response = new Response<GetProjectList>(result, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<GetProjectList>(ex.Message, 500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> SaveProjectTeamMember(PPProjectTeam project)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                var result = _projectRepository.SaveProjectTeamMember(project);
                if (result.RequestID == 0)
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"ProjectTeamMember Data Creation or Updation is Failed");
                }
                else
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"Saved Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<List<GetDepartmentdropdowm>> GetProjectMemberDropDown(int TenantID,int LocationID)
        {
            Response<List<GetDepartmentdropdowm>> response;
            try
            {
                var result = _projectRepository.GetProjectMemberDropDown(TenantID,LocationID,0,0);
                if (result.Count() == 0)
                {
                    response = new Response<List<GetDepartmentdropdowm>>(result,200,"Data Not Found");
                }
                else
                {
                    foreach(var item in result)
                    {
                        var resource = _projectRepository.GetProjectResourceDropDown(item.TenantID,item.LocationID,item.DeptID,0);
                        item.ResourceList = resource;
                        // foreach(var items in resource)
                        // {
                        //     var role = _projectRepository.GetRoleDropDown(TenantID,LocationID,item.DeptID,items.EmpID);
                        //     items.RoleList = role;
                        // }
                    }
                    response = new Response<List<GetDepartmentdropdowm>>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<GetDepartmentdropdowm>>(ex.Message,500);
            }
            return response;
        }
        public Response<List<ProjectMemberDetailBO>> GetProjectMemberDetails(TimesheetInputBO timeSheet)
        {
            Response<List<ProjectMemberDetailBO>> response;
            try
            {
                var result = _projectRepository.GetProjectMemberDetails(timeSheet.TenantID,timeSheet.LocationID,timeSheet.ProjectID,timeSheet.GetDateTime);
                if(result.Count() > 0)
                {
                if (string.IsNullOrEmpty(timeSheet.TimeSheetStatus))
                {
                    timeSheet.TimeSheetStatus = "Flagged";
                }
                foreach(var objflagged in result)
                {
                    timeSheet.EmpID = objflagged.EmpID;                    
                    var result1 = _timeSheetRepository.SaveTimeSheetFlagged(timeSheet);
                    foreach(var objflagg in result1)
                    {
                        if(objflagg.EmpID == objflagged.EmpID)
                        {
                            objflagged.FlaggedCount = 1;  // objflagged.FlaggedCount = result1.Count();
                        }
                        else
                        {
                            objflagged.FlaggedCount = 0;
                        }
                    }
                }
                }
                if (result.Count() == 0)
                {
                    response = new Response<List<ProjectMemberDetailBO>>(result,200,"Data Not Found");
                }
                else
                {
                    response = new Response<List<ProjectMemberDetailBO>>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<ProjectMemberDetailBO>>(ex.Message,500);
            }
            return response;
        }
        public Response<List<ProjectManagerDropDownBO>> GetProjectManager(int TenantID,int LocationID,int ProjectID)
        {
            Response<List<ProjectManagerDropDownBO>> response;
            try
            {
                var result = _projectRepository.GetProjectManager(TenantID,LocationID,ProjectID);
                if (result.Count() == 0)
                {
                    response = new Response<List<ProjectManagerDropDownBO>>(result,200,"Data Not Found");
                }
                else
                {
                    response = new Response<List<ProjectManagerDropDownBO>>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<ProjectManagerDropDownBO>>(ex.Message,500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> SaveProjectTask(PPProjectTaskBO task)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                var result = _projectRepository.SaveProjectTask(task);
                if (result.RequestID == 0)
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"ProjectTask Data Creation or Updation is Failed");
                }
                else
                {
                    if(task.EmpIDs!=null && task.EmpIDs!="")
                    {
                        string[] split = task.EmpIDs.Split(',');
                        foreach (var str in split)
                        {
                            _projectRepository.SaveProjectTaskResource(new PPTaskResourceBO
                            {
                                PTaskID = result.RequestID,
                                EmpID = Convert.ToInt32(str),
                                IsProjTask = task.IsTimeTrack,
                                TaskStatus = task.TaskStatus,
                                ProjectID = task.ProjectID,
                                TenantID = task.TenantID,
                                LocationID = task.LocationID,
                                CreatedBy = task.CreatedBy
                            });
                            var timesheet = _timeSheetRepository.SaveTimeSheet(new PPTimeSheetBO
                            {
                                EmpID = Convert.ToInt32(str),
                                TimeSheetStatus = task.TaskStatus,
                                TenantID = task.TenantID,
                                LocationID = task.LocationID,
                                YearID = task.YearID,
                                CreatedBy = task.CreatedBy
                            });
                            _timeSheetRepository.SaveMasterWeekTSNonBillable(new PPWeekTSNonBillableBO
                            {
                                EmpID = Convert.ToInt32(str),
                                YearID = task.YearID,
                                TimeSheetID = timesheet.RequestID,
                                TimeSheetNBStatus = task.TaskStatus,
                                EstStartDate = task.EstStartDate,
                                TenantID = task.TenantID,
                                LocationID = task.LocationID,
                                CreatedBy = task.CreatedBy
                            });
                        }
                    }
                    var project = _projectRepository.GetProjectDetailList(task.TenantID,task.LocationID,task.ProjectID);
                    foreach(var item in project)
                    {
                        vwProjectDetailList data = new vwProjectDetailList();
                        if(item.EstEndDate < task.EstEndDate && task.IsProjEstDateExten == true)
                        {
                            data.EstEndDate = task.EstEndDate;
                            data.ProjectID = task.ProjectID;
                            data.TenantID = task.TenantID;
                            _projectRepository.UpdateProjectEstEndDate(data);
                        }
                    }
                    response = new Response<LeaveRequestModelMsg>(result,200,result.Msg);
                }
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<List<PPProjectTaskBO>> GetProjectTask(int TenantID,int LocationID,int ProjectID,int MileStoneID)
        {
            Response<List<PPProjectTaskBO>> response;
            try
            {
                var result = _projectRepository.GetProjectTask(TenantID,LocationID,ProjectID,MileStoneID);
                if (result.Count() == 0)
                {
                    response = new Response<List<PPProjectTaskBO>>(result,200,"Data Not Found");
                }
                else
                {
                    response = new Response<List<PPProjectTaskBO>>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<PPProjectTaskBO>>(ex.Message,500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> SaveMileStone(MileStoneBO milestone)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                var result = _projectRepository.SaveMileStone(milestone);
                if (result.RequestID == 0)
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"MileStone Data Creation or Updation is Failed");
                }
                else
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,result.Msg);
                }
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<List<MileStoneBO>> GetMileStone(int TenantID,int LocationID,int ProjectID)
        {
            Response<List<MileStoneBO>> response;
            try
            {
                var result = _projectRepository.GetMileStone(TenantID,LocationID,ProjectID);
                if (result.Count() == 0)
                {
                    response = new Response<List<MileStoneBO>>(result,200,"Data Not Found");
                }
                else
                {
                    foreach(var item in result)
                    {
                        var percentage = _projectRepository.GetMileSoneTaskPercentage(item.ProjectID,item.MileStoneID);
                        item.CompletePercentage = percentage;
                        var task = _projectRepository.GetProjectTask(item.TenantID,item.LocationID,item.ProjectID,item.MileStoneID);
                        item.ProjectTaskList = task;
                         foreach(var items in item.ProjectTaskList)
                        {
                            var member = _projectRepository.GetPTaskAssignedMembers(items.TenantID,items.LocationID,items.PTaskID);
                            items.AssignedMemberLists = member;
                        }
                    }
                    response = new Response<List<MileStoneBO>>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<MileStoneBO>>(ex.Message,500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> SaveProjectTaskHistory(PPProjectTaskHistoryBO taskHistory)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                var result = _projectRepository.SaveProjectTaskHistory(taskHistory);
                if (result.RequestID == 0)
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"ProjectTaskHistory Data Creation or Updation is Failed");
                }
                else
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"Saved Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> SaveProjectRole(PPProjectRoleBO role)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                var result = _projectRepository.SaveProjectRole(role);
                if (result.RequestID == 0)
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"ProjectRole Data Creation or Updation is Failed");
                }
                else
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"Saved Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<List<PPProjectRoleBO>> GetProjectRole(int TenantID,int LocationID,int ProjectRoleID)
        {
            Response<List<PPProjectRoleBO>> response;
            try
            {
                var result = _projectRepository.GetProjectRole(TenantID,LocationID,ProjectRoleID);
                if (result.Count() == 0)
                {
                    response = new Response<List<PPProjectRoleBO>>(result,200,"Data Not Found");
                }
                else
                {
                    response = new Response<List<PPProjectRoleBO>>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<PPProjectRoleBO>>(ex.Message,500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> DeleteProjectRole(int ProjectRoleID)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                var result = _projectRepository.DeleteProjectRole(ProjectRoleID);
                if(result.RequestID == 0)
                {
                response = new Response<LeaveRequestModelMsg>(result,200,"Cannot Deleted");
                }
                else
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"Deleted Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<List<FinancialByProjectRole>> GetFinancialByProjectRole(int ProjectID, int TenantID)
        {
            Response<List<FinancialByProjectRole>> response;
            try
            {
                var result = _projectRepository.GetFinancialByProjectRole(ProjectID, TenantID);
                if (result.Count() == 0)
                {
                    response = new Response<List<FinancialByProjectRole>>(result,200,"Data Not Found");
                }
                else
                {
                    response = new Response<List<FinancialByProjectRole>>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<FinancialByProjectRole>>(ex.Message,500);
            }
            return response;
        }
        public Response<List<FinancialByProjectRole>> GetFinancialByProjectMemberRole(int ProjectID, int TenantID)
        {
            Response<List<FinancialByProjectRole>> response;
            try
            {
                var result = _projectRepository.GetFinancialByProjectMemberRole(ProjectID, TenantID);
                if (result.Count() == 0)
                {
                    response = new Response<List<FinancialByProjectRole>>(result,200,"Data Not Found");
                }
                else
                {
                    response = new Response<List<FinancialByProjectRole>>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<FinancialByProjectRole>>(ex.Message,500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> UpdateProjectRoleAmount(PPProjectTeam project)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                var result = _projectRepository.UpdateProjectRoleAmount(project);
                if (result.RequestID == 0)
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"ProjectRoleAmount Data Creation or Updation is Failed");
                }
                else
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"Saved Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<List<PPProjectListBO>> GetProjectLists(int TenantID, int LocationID,int ProjectID)
        {
            Response<List<PPProjectListBO>> response;
            try
            {
                var result = _projectRepository.GetProjectLists(TenantID, LocationID ,ProjectID);
                if (result.Count() == 0)
                {
                    response = new Response<List<PPProjectListBO>>(result,200,"Data Not Found");
                }
                else
                {
                    response = new Response<List<PPProjectListBO>>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<PPProjectListBO>>(ex.Message,500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> SaveProjectDocumentType(PPProjectDocumentTypeBO type)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                var result = _projectRepository.SaveProjectDocumentType(type);
                if (result.RequestID == 0)
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"ProjectDocumentType Data Creation or Updation is Failed");
                }
                else
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,result.Msg);
                }
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<List<PPProjectDocumentTypeBO>> GetProjectDocumentType(int TenantID, int LocationID, int DocumentTypeID,int ProjectID)
        {
            Response<List<PPProjectDocumentTypeBO>> response;
            try
            {
                var result = _projectRepository.GetProjectDocumentType(TenantID,LocationID,DocumentTypeID,ProjectID);
                if (result.Count() == 0)
                {
                    response = new Response<List<PPProjectDocumentTypeBO>>(result,200,"Data Not Found");
                }
                else
                {
                    response = new Response<List<PPProjectDocumentTypeBO>>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<PPProjectDocumentTypeBO>>(ex.Message,500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> DeletePPProjectDocumentType(int DocumentTypeID)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                var result = _projectRepository.DeletePPProjectDocumentType(DocumentTypeID);
                if(result.RequestID == 0)
                {
                response = new Response<LeaveRequestModelMsg>(result,200,"Could not be Deleted");
                }
                else
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,result.Msg);
                }
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<int> SaveDocumentType(InputDocType objtype, ProjectDocumentTypeBO objpro)
        {
            Response<int> response;
            try
            {
                int DocID = 0;
                string NewFileName;
                string filExt;
                DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                DocumentDA objdocservice = new DocumentDA(_configuration.GetConnectionString("KOPRODADBConnection"), DBType.MySQL);
                filExt = Path.GetExtension(objtype.File.FileName.ToString());
                if (objpro.DocID > 0)
                {                    
                    var result = objdoc.GetDocument(objpro.DocID, 0, "", "", 0);
                    var name = Path.GetFileNameWithoutExtension(result[0].GenDocName);
                    NewFileName = name + filExt;
                    DocID = result[0].DocID;
                }
                else
                {
                    NewFileName = "ProjectDoc_" + (Guid.NewGuid()).ToString("N") + filExt;
                }
                SaveDocCloudBO docCloudins = new SaveDocCloudBO();
                var Repo = objdocservice.GetDocRepository(objpro.TenantID,objpro.TenantID,"Tenant",Convert.ToInt32(_configuration.GetSection("ProductID").Value), LocationType.ProjectDocument);
                var docRepo = Repo;
                if (docRepo[0].RepoType == "Storage")
                {

                    var ms = new MemoryStream();
                    objtype.File.OpenReadStream().CopyTo(ms);
                    byte[] contents = ms.ToArray();
                    var fileFormat = Convert.ToBase64String(contents);
                    switch (objtype.File.ContentType)
                    {
                        case "image/png":
                            docCloudins.ContentType = "image/png";
                            break;
                        case "image/jpeg":
                            docCloudins.ContentType = "image/jpeg";
                            break;
                        case "application/pdf":
                            docCloudins.ContentType = "application/pdf";
                            break;
                        case "application/docx":
                            docCloudins.ContentType = "application/msword";
                            break;
                        case "application/doc":
                            docCloudins.ContentType = "application/msword";
                            break;
                        default:
                            docCloudins.ContentType = System.Net.Mime.MediaTypeNames.Application.Pdf;
                            break;
                    }
                    var objcont1 = objdoc.GetDocContainer(objpro.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                    docCloudins.Container = objcont1[0].ContainerName.ToLower();

                    docCloudins.CloudType = _configuration.GetSection("CloudType").Value;
                    docCloudins.folderPath = docRepo[0].RepoPath.ToLower() + "/" + Convert.ToString(objpro.ProjectID);
                    docCloudins.file = fileFormat;
                    docCloudins.fileName = NewFileName;
                    docCloudins.ProductCode = _configuration.GetSection("ProductID").Value;

                    _ = _storage.SaveBulkDocumentCloud(docCloudins);

                    GenDocument d1 = new GenDocument();
                    d1.DocID = DocID;
                    d1.RepositoryID = docRepo[0].RepositoryID;
                    d1.DocumentName = objtype.File.FileName;
                    d1.DocType = docRepo[0].RepoName;
                    d1.CreatedBy = objpro.CreatedBy;
                    d1.OrgDocName = NewFileName;
                    d1.GenDocName = NewFileName;
                    d1.DocKey = (Guid.NewGuid()).ToString("N");
                    d1.DocSize = Convert.ToDecimal(objtype.File.Length);
                    d1.Entity = docRepo[0].LocType;
                    d1.EntityID = objpro.ProjectID;
                    d1.TenantID = objpro.TenantID;
                    d1.DocTypeID = objpro.DocTypeID;
                    d1.DirectionPath = docCloudins.folderPath;
                    var doc = objdoc.SaveDocument(d1);
                    DocID = doc;

                }
                if (DocID == 0)
                {
                    response = new Response<int>(DocID, 400, "please upload Incomplete file");
                }
                else
                {
                    response = new Response<int>(DocID, 200, "Saved Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<PPProjectDocumentTypeBO> GetPPProjectDocument(int TenantID,int LocationID,int DocumentTypeID,int ProjectID,int ProductID)
        {
            Response<PPProjectDocumentTypeBO> responce;
            string path = string.Empty;
            string fileName = string.Empty;
            AzureDocURLBO docURL = new AzureDocURLBO();
            try
            {
                var objreturn = _projectRepository.GetProjectDocument(TenantID,LocationID,0,ProjectID,ProductID);
                if (objreturn.TenantID == 0)
                {
                    responce = new Response<PPProjectDocumentTypeBO>("Data Valid Not Found", 500);
                }
                else
                {
                    objreturn.base64Images = "";
                    DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                    var documents = objdoc.GetDocument(0, DocumentTypeID, "", "ProjectDocument", TenantID);
                    if (documents.Count > 0)
                    {
                        var objconta = objdoc.GetDocContainer(TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));

                        var docu = _storage.DownloadDocument(new SaveDocCloudBO
                        {
                            CloudType = _configuration.GetSection("CloudType").Value,
                            Container = objconta[0].ContainerName.ToLower(),
                            fileName = documents[0].GenDocName,
                            folderPath = documents[0].DirectionPath,
                            ProductCode = Convert.ToString(ProductID)
                        }).Result;
                        objreturn.base64Images = docu.DocumentURL;
                    }
                }
                responce = new Response<PPProjectDocumentTypeBO>(objreturn, 200, "Data Retraived");
            }
            catch (Exception ex)
            {
                responce = new Response<PPProjectDocumentTypeBO>(ex.Message, 500);
            }
            return responce;
        }
        public Response<string> DownloadDoc(int ProductID,int TenantID,string docuniqueId)
        {
            Response<string> response;

            try
            {
                string path = string.Empty;
                string fileName = string.Empty;
                string rtnVal = string.Empty;
                DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                DocumentDA objdocservice = new DocumentDA(_configuration.GetConnectionString("KOPRODADBConnection"), DBType.MySQL);
                //List<GenDocument> gendocs = new List<GenDocument>();
                var gendocs = objdoc.GetDocument(0, 0,docuniqueId, "", 0);
                if (gendocs.Count > 0)
                    {
                        var objconta = objdoc.GetDocContainer(TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));

                        var docu = _storage.DownloadDocument(new SaveDocCloudBO
                        {
                            CloudType = _configuration.GetSection("CloudType").Value,
                            Container = objconta[0].ContainerName.ToLower(),
                            fileName = gendocs[0].GenDocName,
                            folderPath = gendocs[0].DirectionPath,
                            ProductCode = Convert.ToString(ProductID)
                        }).Result;
                        if (docu != null && docu.DocumentURL != "" && docu.DocumentURL != null)
                        {
                            rtnVal = _storage.ReadDataInUrl(docu.DocumentURL);
                        }
                    }
                if (string.IsNullOrEmpty(rtnVal))
                {
                    response = new Response<string>(null, 200, "Data Not Found");
                }
                else
                {
                    response = new Response<string>(rtnVal, 200, "Data Retrived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<string>(ex.Message, 500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> SaveEntityProjectTask(PPProjectTaskBO task)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                var result = _projectRepository.SaveProjectTask(task);
                if (result.RequestID == 0)
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"ProjectTask Data Creation or Updation is Failed");
                }
                else
                {
                    if(task.EmpIDs!=null && task.EmpIDs!="")
                    {
                        string[] split = task.EmpIDs.Split(',');
                        foreach (var str in split)
                        {
                            _projectRepository.SaveProjectTaskResource(new PPTaskResourceBO
                            {
                                PTaskID = result.RequestID,
                                EmpID = Convert.ToInt32(str),
                                IsProjTask = task.IsTimeTrack,
                                TaskStatus = task.TaskStatus,
                                ProjectID = task.ProjectID,
                                TenantID = task.TenantID,
                                LocationID = task.LocationID,
                                CreatedBy = task.CreatedBy
                            });
                            var timesheet = _timeSheetRepository.SaveTimeSheet(new PPTimeSheetBO
                            {
                                EmpID = Convert.ToInt32(str),
                                TimeSheetStatus = task.TaskStatus,
                                TenantID = task.TenantID,
                                LocationID = task.LocationID,
                                YearID = task.YearID,
                                CreatedBy = task.CreatedBy
                            });
                            _timeSheetRepository.SaveMasterWeekTSNonBillable(new PPWeekTSNonBillableBO
                            {
                                EmpID = Convert.ToInt32(str),
                                YearID = task.YearID,
                                TimeSheetID = timesheet.RequestID,
                                TimeSheetNBStatus = task.TaskStatus,
                                EstStartDate = task.EstStartDate,
                                TenantID = task.TenantID,
                                LocationID = task.LocationID,
                                CreatedBy = task.CreatedBy
                            });
                        }
                    }
                    response = new Response<LeaveRequestModelMsg>(result,200,"Saved Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> UpdateRemoveTaskMembers(PPTaskResourceBO task)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                var result = _projectRepository.UpdateRemoveTaskMembers(task);
                if (result.RequestID == 0)
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"Task Could Not Be Updated");
                }
                else
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"Updated Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> DeleteProjectMileStone(int MileStoneID)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                var result = _projectRepository.DeleteProjectMileStone(MileStoneID);
                if(result.RequestID == 0)
                {
                response = new Response<LeaveRequestModelMsg>(result,200,result.Msg);
                }
                else
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,result.Msg);
                }
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> DeleteProjectTask(int PTaskID)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                var result = _projectRepository.DeleteProjectTask(PTaskID);
                if(result.RequestID == 0)
                {
                response = new Response<LeaveRequestModelMsg>(result,200,result.Msg);
                }
                else
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,result.Msg);
                }
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<List<ProjectTaskReportBO>> GetProjectReport(int TenantID,int LocationID,int ProjectID)
        {
            Response<List<ProjectTaskReportBO>> response;
            try
            {
                var results = _projectRepository.GetProjectTaskReport(TenantID,LocationID,ProjectID);
                if (results.Count() == 0)
                {
                    response = new Response<List<ProjectTaskReportBO>>(results,200,"Data Not Found");
                }
                else
                {
                    // foreach(var item in result)
                    // {
                    //     var report = _projectRepository.GetProjectTaskReport(item.TenantID,item.EmpID,item.ProjectID);
                    //     item.ProjectTaskReport = report;
                    // }
                    response = new Response<List<ProjectTaskReportBO>>(results,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<ProjectTaskReportBO>>(ex.Message,500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> SavePPProjectPaymentHistory(PPProjectPaymentHistoryBO project)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                var result = _projectRepository.SaveProjectPaymentHistory(project);
                if (result.RequestID == 0)
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"ProjectPayment Data Creation or Updation is Failed");
                }
                else
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"Saved Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<List<PPProjectPaymentHistoryBO>> GetPPProjectPaymentHistory(int TenantID,int LocationID,int EmpID)
        {
            Response<List<PPProjectPaymentHistoryBO>> response;
            try
            {
                var results = _projectRepository.GetPPProjectPaymentHistory(TenantID,LocationID,EmpID);
                if (results.Count() == 0)
                {
                    response = new Response<List<PPProjectPaymentHistoryBO>>(results,200,"Data Not Found");
                }
                else
                {
                    response = new Response<List<PPProjectPaymentHistoryBO>>(results,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<PPProjectPaymentHistoryBO>>(ex.Message,500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> SaveNewWorkGroupProject(PPProjectBO project)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                if(project.ProjectID==0)
                {
                    var num =_administrativeRepository.GenSequenceNo(project.TenantID,project.LocationID,"Project");
                    project.ProjectNo=num.Id;
                }
                var result = _projectRepository.SaveNewWorkGroupProject(project);
                if (result.RequestID == 0)
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"Project Data Creation or Updation is Failed");
                }
                else
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"Saved Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<List<ProjectWorkGroupSummaryBO>> GetProjectWorkGroupSummary(int TenantID, int LocationID)
        {
            Response<List<ProjectWorkGroupSummaryBO>> response;
            try
            {
                var result = _projectRepository.GetProjectWorkGroupSummary(TenantID,LocationID);
                if (result.Count() == 0)
                {
                    response = new Response<List<ProjectWorkGroupSummaryBO>>(result,200,"Data Not Found");
                }
                else
                {
                    response = new Response<List<ProjectWorkGroupSummaryBO>>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<ProjectWorkGroupSummaryBO>>(ex.Message,500);
            }
            return response;
        }
        public Response<List<PPProjectListBO>> GetProjectWorkGroupLists(PPProjectListBO project)
        {
            Response<List<PPProjectListBO>> response;
            try
            {
                var result = _projectRepository.GetProjectWorkGroupLists(project);
                if (result.Count() == 0)
                {
                    response = new Response<List<PPProjectListBO>>(result,200,"Data Not Found");
                }
                else
                {
                    response = new Response<List<PPProjectListBO>>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<PPProjectListBO>>(ex.Message,500);
            }
            return response;
        }
    }
}