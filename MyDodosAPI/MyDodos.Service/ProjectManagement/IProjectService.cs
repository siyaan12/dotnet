using System.Collections.Generic;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.ProjectManagement;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.ProjectManagement;

namespace MyDodos.Service.ProjectManagement
{
    public interface IProjectService
    {
        Response<LeaveRequestModelMsg> SavePPSponsor(PPSponsorBO sponsor);
        Response<List<PPSponsorBO>> GetPPSponsor(int TenantID, int LocationID, int SponsorID);
        Response<LeaveRequestModelMsg> DeletePPSponsor(int SponsorID);
        Response<LeaveRequestModelMsg> SavePPInitiativeType(PPInitiativeTypeBO type);
        Response<List<PPInitiativeTypeBO>> GetPPInitiativeType(int TenantID, int LocationID, int InitiativeTypeID);
        Response<LeaveRequestModelMsg> DeletePPInitiativeType(int InitiativeTypeID);
        Response<LeaveRequestModelMsg> SaveNewProject(PPProjectBO project);
        Response<LeaveRequestModelMsg> DeleteProject(int ProjectID);
        Response<LeaveRequestModelMsg> SaveProject(PPProjectBO project);
        Response<LeaveRequestModelMsg> SaveProjectTeamMember(PPProjectTeam project);
        Response<List<GetDepartmentdropdowm>> GetProjectMemberDropDown(int TenantID,int LocationID);
        Response<List<ProjectMemberDetailBO>> GetProjectMemberDetails(TimesheetInputBO timeSheet);
        Response<GetProjectList> GetProjectData(GetProjectList project);
        Response<List<ProjectManagerDropDownBO>> GetProjectManager(int TenantID,int LocationID,int ProjectID);
        Response<List<vwProjectDetails>> GetProjectDetails(int TenantID,int LocationID,int ProjectID);
        Response<LeaveRequestModelMsg> SaveProjectTask(PPProjectTaskBO task);
        Response<List<PPProjectTaskBO>> GetProjectTask(int TenantID,int LocationID,int ProjectID,int MileStoneID);
        Response<LeaveRequestModelMsg> SaveMileStone(MileStoneBO milestone);
        Response<List<MileStoneBO>> GetMileStone(int TenantID,int LocationID,int MileStoneID);
        Response<LeaveRequestModelMsg> SaveProjectTaskHistory(PPProjectTaskHistoryBO taskHistory);
        Response<List<vwProjectDetailList>> GetProjectDetailList(int TenantID,int LocationID,int ProjectID);
        Response<LeaveRequestModelMsg> SaveProjectRole(PPProjectRoleBO role);
        Response<List<PPProjectRoleBO>> GetProjectRole(int TenantID,int LocationID,int ProjectRoleID);
        Response<LeaveRequestModelMsg> DeleteProjectRole(int ProjectRoleID);
        Response<List<FinancialByProjectRole>> GetFinancialByProjectRole(int ProjectID, int TenantID);
        Response<List<FinancialByProjectRole>> GetFinancialByProjectMemberRole(int ProjectID, int TenantID);
        Response<LeaveRequestModelMsg> UpdateProjectRoleAmount(PPProjectTeam project);
        Response<List<PPProjectListBO>> GetProjectLists(int TenantID, int LocationID,int ProjectID);
        Response<LeaveRequestModelMsg> SaveProjectDocumentType(PPProjectDocumentTypeBO type);
        Response<List<PPProjectDocumentTypeBO>> GetProjectDocumentType(int TenantID, int LocationID, int DocumentTypeID,int ProjectID);
        Response<LeaveRequestModelMsg> DeletePPProjectDocumentType(int DocumentTypeID);
        Response<int> SaveDocumentType(InputDocType objtype, ProjectDocumentTypeBO objpro);
        Response<PPProjectDocumentTypeBO> GetPPProjectDocument(int TenantID,int LocationID,int DocumentTypeID,int ProjectID,int ProductID);
        Response<string> DownloadDoc(int ProductID,int TenantID,string docuniqueId);
        Response<LeaveRequestModelMsg> SaveEntityProjectTask(PPProjectTaskBO task);
        Response<LeaveRequestModelMsg> UpdateRemoveTaskMembers(PPTaskResourceBO task);
        Response<LeaveRequestModelMsg> DeleteProjectMileStone(int MileStoneID);
        Response<LeaveRequestModelMsg> DeleteProjectTask(int PTaskID);
        Response<List<ProjectTaskReportBO>> GetProjectReport(int TenantID,int LocationID,int ProjectID);
        Response<LeaveRequestModelMsg> SavePPProjectPaymentHistory(PPProjectPaymentHistoryBO project);
        Response<LeaveRequestModelMsg> SaveNewWorkGroupProject(PPProjectBO project);
        Response<List<ProjectWorkGroupSummaryBO>> GetProjectWorkGroupSummary(int TenantID, int LocationID);
        Response<List<PPProjectListBO>> GetProjectWorkGroupLists(PPProjectListBO project);
        Response<List<PPProjectPaymentHistoryBO>> GetPPProjectPaymentHistory(int TenantID,int LocationID,int EmpID);
    }
}