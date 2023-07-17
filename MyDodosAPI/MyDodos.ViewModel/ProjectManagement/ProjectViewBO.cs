using System;
using System.Collections.Generic;
using MyDodos.Domain.ProjectManagement;
using MyDodos.Domain.LeaveBO;
using MyDodos.ViewModel.ServerSearch;
using Microsoft.AspNetCore.Http;
using MyDodos.ViewModel.Employee;

namespace MyDodos.ViewModel.ProjectManagement
{
    public class GetProjectList
    {
        public GetProjectListInputs objProjectListInput { get; set; }
        public ServerSearchable ServerSearchables { get; set; }
        public List<ProjectListBO> objProjectList { get; set; }
    }
    public class GetProjectListInputs
    {
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public string ProjStatus { get; set; }
    }
    public class ProjectListBO 
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectNo { get; set; }
        //public string ManagerID { get; set; }
        public string ManagerName { get; set; }
        public DateTime? EstStartDate { get; set; }
        public DateTime? EstEndDate { get; set; }
        public int TotalCount { get; set; }
        public int CompPercent { get; set; }
        public string InitiativeType { get; set; }
        public string ProjStatus { get; set; }
        public int TenantID { get; set; }     
        public int LocationID { get; set; }  
        public DateTime GetDateTime { get; set; } 
        public Percentage ProjectPercentage { get; set; }
        public List<ManagerList> ManagerList { get; set; }
        public List<ProjectMemberDetailBO> TeamList { get; set; }
    }
    public class Percentage
    {
        public int CompPercent { get; set; }
    }
    public class ManagerList
    {
        public int ManagerID { get; set; }
        public string ManagerName { get; set; }
    }
    public class GetDepartmentdropdowm
    {
        public int DeptID { get; set; }
        public string Department { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public List<GetResourceList> ResourceList { get; set; }
    }
    public class GetResourceList
    {
        public int EmpID { get; set; }
        public string FirstName { get; set; }
        //public List<GetRoleList> RoleList { get; set; }
    }
    // public class GetRoleList
    // {
    //     public int RoleID { get; set; }
    //     public string RoleName { get; set; }
    // }
    public class ProjectMemberDetailBO
    {
        public int EmpID { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public int FuncRoleID { get; set; }
        public string RoleName { get; set; }
        public decimal RoleRate { get; set; }
        public bool IsProjectManager { get; set; }
        public int ProjectID { get; set; }
        public DateTime? AssignedOn { get; set; }
        public string AssignedStatus { get; set; }
        public DateTime? AssignStart { get; set; }
        public DateTime? AssignEnd { get; set; }
        public int DeptID { get; set; }
        public int ProjTeamMemID { get; set; }
        public string TimeSheetStatus { get; set; }
        public int FlaggedCount { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
    }
    public class ProjectManagerDropDownBO
    {
        public int EmpID { get; set; }
        public string ManagerName { get; set; }
        public int ManagerID { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int ProjectID { get; set; }
    }
    public class vwProjectDetails
    {
         public int ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public string ProjShortName { get; set; }
        public string Description { get; set; }
        public int InitiativeTypeID { get; set; }
        public string Priority { get; set; }
        public string ProjStatus { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
    }
    public class vwProjectDetailList
    {
        public int ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public int InitiativeTypeID { get; set; }
        public int YearID { get; set; }
        public string Priority { get; set; }
        public string ManagerID { get; set; }
        public int SponsorID { get; set; }
        public string Sponsor { get; set; }
        public string ExecutiveSponsor { get; set; }
        public string ProjStatus { get; set; }
        public DateTime? EstStartDate { get; set; }
        public DateTime? EstEndDate { get; set; }
        public DateTime? ActStartDate { get; set; }
        public DateTime? ActEndDate { get; set; }
        public bool IsBillable { get; set; }
        public string BillableName { get; set; }
        public bool IsClientProject { get; set; }
        public decimal CompPercent { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
        public bool IsProjEstDateExten { get; set; }
        public List<TaskExtenDate> TaskExtenDate { get; set; }
    }
    public class TaskExtenDate
    {
        public bool IsProjEstDateExten { get; set; }
        public DateTime? OldProjEstEndDate { get; set; }
        public DateTime? NewTaskEstEndDate { get; set; }
    }
    public class PPProjectListBO 
    {
        public int ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public string ProjShortName { get; set; }
        public string Description { get; set; }
        public int InitiativeTypeID { get; set; }
        public string InitiativeType { get; set; }
        public string ManagerID { get; set; }
        public int YearID { get; set; }
        public string ManagerName { get; set; }
        public string ProjStatus { get; set; }
        public string ProjectFrom { get; set; }
        public DateTime? EstStartDate { get; set; }
        public DateTime? EstEndDate { get; set; }
        public bool IsProjectManager { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public List<EmpPersonalDetail> ProjectManager { get; set; }
        public List<EmpPersonalDetail> TeamMembers { get; set; }
    }
    public class InputDocType
    {
        public string InputJson { get; set; }
        public IFormFile File { get; set; }
    }
    public class ProjectDocumentTypeBO
    {
        public int DocID { get; set; }
        public string FileName { get; set; }
        public int ProjectID { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int DocumentTypeID { get; set; }
        public int DocTypeID { get; set; }
        public int CreatedBy { get; set; }       
    }
    public class ProjectReportBO
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public int EmpID { get; set; }
        public string FirstName { get; set; }
        public decimal TotalWHours { get; set; }
        public string PReportStatus { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public List<ProjectTaskReportBO> ProjectTaskReport { get; set; }
    }
    public class ProjectTaskReportBO
    {
        public int TimeSheetID { get; set; }
        public int EmpID { get; set; }
        public string EmpNumber { get; set; }
        public string FullName { get; set; }
        public decimal WorkingHours { get; set; }
        public string PaymentStatus { get; set; }
        public int WeekNo { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public int YearID { get; set; }
        public int TenantID { get; set; }
    }
    public class ProjectWorkGroupSummaryBO
    {
        public string fieldName { get; set; }
        public int Count { get; set; }
        public string ProjectFrom { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
    }
}
