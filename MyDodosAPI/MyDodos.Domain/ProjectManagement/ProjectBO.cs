using System;
using System.Collections.Generic;
namespace MyDodos.Domain.ProjectManagement
{
    public class PPSponsorBO
    {
        public int SponsorID { get; set; }
        public string SponsorName { get; set; }
        public string Description { get; set; }
        public string ShortName { get; set; }
        public string ClientStatus { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
    }
    public class PPInitiativeTypeBO
    {
        public int InitiativeTypeID { get; set; }
        public string InitiativeType { get; set; }
        public string Description { get; set; }
        public string InitiativeTypeStatus { get; set; }
        public bool IsBillable  { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
    }
    public class PPProjectBO 
    {
        public int ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public string ProjShortName { get; set; }
        public string Description { get; set; }
        public int InitiativeTypeID { get; set; }
        public int YearID { get; set; }
        public string ManagerID { get; set; }
        public int SponsorID { get; set; }
        public string Sponsor { get; set; }
        public string ExecutiveSponsor { get; set; }
        public int ExecutiveSponsorID { get; set; }
        public string ProjStatus { get; set; }
        public string IsActive { get; set; }
        public DateTime? EstStartDate { get; set; }
        public DateTime? EstEndDate { get; set; }
        public DateTime? ActStartDate { get; set; }
        public DateTime? ActEndDate { get; set; }
        public bool IsBillable { get; set; }
        public string BillableName { get; set; }
        public bool IsClientProject { get; set; }
        public decimal Effort { get; set; }
        public string Priority { get; set; }
        public decimal CompPercent { get; set; }
        public string CompletionMode { get; set; }
        public string ProjectFrom { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
    }
    public class PPProjectTeam
    {
        public int ProjTeamMemID { get; set; }  
        public int ProjectID { get; set; }
        public int EmpID { get; set; }
        public int DeptID { get; set; }
        public int RoleID { get; set; }
        public bool IsProjectManager { get; set; }
        public decimal RoleRate { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime? AssignedOn { get; set; }
        public string AssignedStatus { get; set; }
        public DateTime? AssignStart { get; set; }
        public DateTime? AssignEnd { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
    }
    public class PPProjectTaskBO
    {
        public int PTaskID { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public bool IsTimeTrack { get; set; }
        public bool IsBillable { get; set; }
        public string EmpID { get; set; }
        public int ProjectID { get; set; }
        public int MileStoneID { get; set; }
        public DateTime? EstStartDate { get; set; }
        public DateTime? EstEndDate { get; set; }
        public DateTime? ActStartDate { get; set; }
        public DateTime? ActEndDate { get; set; }
        public decimal EstEffort { get; set; }
        public decimal ActEffort { get; set; }
        public string TaskStatus { get; set; }
        public string MileStoneStatus { get; set; }
        public string TaskPriority { get; set; }
        public bool IsProjEstDateExten { get; set; }
        public DateTime? ProjEstEndDate { get; set; }
        public string ReferenceNo { get; set; }
        public string EmpIDs { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int YearID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime GmtDateTime { get; set; }
        public List<ProjectAssignedMember> AssignedMemberLists { get; set; }
    }
    public class MileStoneBO
    {
        public int MileStoneID { get; set; }
        public string MileStoneName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Percentage { get; set; }
        public string MileStoneComments { get; set; }
        public string MileStoneStatus { get; set; }
        public int ProjectID { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
        public MileStonePercentage CompletePercentage { get; set; }
        public List<PPProjectTaskBO> ProjectTaskList { get; set; }
    }
    public class MileStonePercentage
    {
        public int CompPercent { get; set; }
    }
    public class PPProjectTaskHistoryBO
    {
        public int PTaskHisID { get; set; }
        public int PTaskID { get; set; }
        public DateTime? EstStartDate { get; set; }
        public DateTime? EstEndDate { get; set; }
        public DateTime? ActStartDate { get; set; }
        public DateTime? ActEndDate { get; set; }
        public decimal EstEffort { get; set; }
        public decimal ActEffort { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
    }
    public class PPProjectRoleBO
    {
        public int ProjectRoleID { get; set; }
        public string ProjectRole { get; set; }
        public string Description { get; set; }
        public string RoleStatus { get; set; }
        public bool IsManagerRole { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
    }
    public class PPTaskResourceBO
    {
        public int ProjTaskResID { get; set; }
        public int PTaskID { get; set; }
        public int EmpID { get; set; }
        public int ProjectID { get; set; }
        public bool IsProjTask { get; set; }
        public string TaskStatus { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
        public string EmpIDs { get; set; }
    }
    public class ProjectAssignedMember
    {
        public int EmpID { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public int PTaskID { get; set; }
    }
    public class FinancialByProjectRole
    {
        public int ProjectRoleID { get; set; }
        public string ProjectRole { get; set; }
        public int EmpID { get; set; }
        public string FirstName { get; set; }
        public decimal RoleRate { get; set; }
        public int FTE { get; set; }
        public decimal ActualEffort { get; set; }
        public decimal ActualIncurredCost { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal Balance { get; set; }
    }
    public class PPProjectDocumentTypeBO
    {
        public int DocumentTypeID { get; set; }
        public string DocumentType { get; set; }
        public string Description { get; set; }
        public string DocumentTypeStatus { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int ProductID { get; set; }
        public int ProjectID { get; set; }
        public string base64Images { get; set; }
        public int CreatedBy { get; set; }
    }
    public class PPProjectPaymentHistoryBO
    {
        public int PaymentID { get; set; }
        public int TimeSheetID { get; set; }
        public int WeekNo { get; set; }
        public int EmpID { get; set; }
        public decimal PaymentAmount { get; set; }  
        public string PaymentStatus { get; set; }
        public int YearID { get; set; }
        public int TenantID { get; set; }
        public bool IsPaidStatus { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
    }
    public class ProjectPaymentStatus
    {
        public int WeekNo { get; set; }
        public int EmpID { get; set; }
        public int YearID { get; set; }
        public int TenantID { get; set; }
        public bool IsPaidStatus { get; set; }
    }
}