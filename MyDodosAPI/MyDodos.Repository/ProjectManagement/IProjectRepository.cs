using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.ProjectManagement;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.ProjectManagement;

namespace MyDodos.Repository.ProjectManagement
{
    public interface IProjectRepository
    {
        LeaveRequestModelMsg SavePPSponsor(PPSponsorBO sponsor);
        List<PPSponsorBO> GetPPSponsor(int TenantID,int LocationID,int SponsorID);
        LeaveRequestModelMsg DeletePPSponsor(int SponsorID);
        LeaveRequestModelMsg SavePPInitiativeType(PPInitiativeTypeBO type);
        List<PPInitiativeTypeBO> GetPPInitiativeType(int TenantID,int LocationID,int InitiativeTypeID);
        LeaveRequestModelMsg DeletePPInitiativeType(int InitiativeTypeID);
        LeaveRequestModelMsg SaveNewProject(PPProjectBO project);
        LeaveRequestModelMsg DeleteProject(int ProjectID);
        LeaveRequestModelMsg SaveProject(PPProjectBO project);
        LeaveRequestModelMsg SaveProjectTeamMember(PPProjectTeam project);
        List<GetDepartmentdropdowm> GetProjectMemberDropDown(int TenantID,int LocationID,int DeptID,int EmpID);
        List<GetResourceList> GetProjectResourceDropDown(int TenantID,int LocationID,int DeptID,int EmpID);
        //List<GetRoleList> GetRoleDropDown(int TenantID,int LocationID,int DeptID,int EmpID);
        List<ProjectMemberDetailBO> GetProjectMemberDetails(int TenantID,int LocationID,int ProjectID,DateTime GetDateTime);
        GetProjectList GetProjectData(GetProjectList inputParam);
        Percentage GetProjectPercentage(int ProjectID);
        List<ProjectManagerDropDownBO> GetProjectManager(int TenantID,int LocationID,int ProjectID);
        List<vwProjectDetails> GetProjectDetails(int TenantID,int LocationID,int ProjectID);
        LeaveRequestModelMsg SaveProjectTask(PPProjectTaskBO task);
        List<PPProjectTaskBO> GetProjectTask(int TenantID,int LocationID,int ProjectID,int MileStoneID);
        LeaveRequestModelMsg SaveMileStone(MileStoneBO milestone);
        List<MileStoneBO> GetMileStone(int TenantID,int LocationID,int ProjectID);
        LeaveRequestModelMsg SaveProjectTaskHistory(PPProjectTaskHistoryBO taskHistory);
        List<vwProjectDetailList> GetProjectDetailList(int TenantID,int LocationID,int ProjectID);
        LeaveRequestModelMsg SaveProjectRole(PPProjectRoleBO role);
        List<PPProjectRoleBO> GetProjectRole(int TenantID,int LocationID,int ProjectRoleID);
        LeaveRequestModelMsg DeleteProjectRole(int ProjectRoleID);
        LeaveRequestModelMsg SaveProjectTaskResource(PPTaskResourceBO task);
        List<ProjectAssignedMember> GetPTaskAssignedMembers(int TenantID,int LocationID,int PTaskID);
        List<TaskExtenDate> GetTaskExtendate(int TenantID,int ProjectID);
        List<FinancialByProjectRole> GetFinancialByProjectRole(int ProjectID, int TenantID);
        List<FinancialByProjectRole> GetFinancialByProjectMemberRole(int ProjectID, int TenantID);
        LeaveRequestModelMsg UpdateProjectRoleAmount(PPProjectTeam project);
        List<ManagerList> GetProjectManagerList(int TenantID,int ProjectID);
        List<PPProjectListBO> GetProjectLists(int TenantID, int LocationID,int ProjectID);
        LeaveRequestModelMsg SaveProjectDocumentType(PPProjectDocumentTypeBO type);
        List<PPProjectDocumentTypeBO> GetProjectDocumentType(int TenantID,int LocationID,int DocumentTypeID,int ProjectID);
        PPProjectDocumentTypeBO GetProjectDocument(int TenantID,int LocationID,int DocumentTypeID,int ProjectID,int ProductID);
        LeaveRequestModelMsg DeletePPProjectDocumentType(int DocumentTypeID);
        MileStonePercentage GetMileSoneTaskPercentage(int ProjectID,int MileStoneID);
        LeaveRequestModelMsg UpdateProjectEstEndDate(vwProjectDetailList project);
        LeaveRequestModelMsg UpdateRemoveTaskMembers(PPTaskResourceBO task);
        LeaveRequestModelMsg DeleteProjectMileStone(int MileStone);
        LeaveRequestModelMsg DeleteProjectTask(int PTaskID);
        List<ProjectReportBO> GetProjectReport(int TenantID, int LocationID,int ProjectID);
        List<ProjectTaskReportBO> GetProjectTaskReport(int TenantID, int LocationID,int ProjectID);
        LeaveRequestModelMsg SaveProjectPaymentHistory(PPProjectPaymentHistoryBO project);
        LeaveRequestModelMsg SaveNewWorkGroupProject(PPProjectBO project);
        List<ProjectWorkGroupSummaryBO> GetProjectWorkGroupSummary(int TenantID,int LocationID);
        List<PPProjectListBO> GetProjectWorkGroupLists(PPProjectListBO project);
        List<PPProjectPaymentHistoryBO> GetPPProjectPaymentHistory(int TenantID, int LocationID,int EmpID);
    }
}