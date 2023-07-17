using System;
using System.Collections.Generic;
using MyDodos.Domain.HR;
using MyDodos.ViewModel.Business;
using MyDodos.ViewModel.ServerSearch;

namespace MyDodos.ViewModel.HR
{
    public class OnboardSearchBO
    {
        public OnboardInputBO objOnboardInput { get; set; }
        public ServerSearchable ServerSearchables { get; set; }
        public List<OnBoardingSerachBO> objOnboardList { get; set; }
    }
    public class OnboardInputBO
    {
        public int StgDataReferID { get; set; }
        public string EntityName { get; set; }
        public int TenantID { get; set; }
        public int ProductID { get; set; }
        public int DepartmentID { get; set; }
        public int LocationID { get; set; }
        public int TemplateID { get; set; }
        public string UniqueBatchNO { get; set; }    
    }
    public class OnBoardingSerachBO
    {
        public int EmpOnboardingID { get; set; }
        public int EmpID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string RequestNumber { get; set; }
        public string RequestSource { get; set; }
        public string SourceRefNumber { get; set; }
        public int ReqEmpID { get; set; }
        public int HRInchargeID { get; set; }
        public int GroupTypeID { get; set; }
        public string GroupType { get; set; }
        public int DeptID { get; set; }
        public int RoleID { get; set; }
        public int LocationID { get; set; }
        public string ReqDesc { get; set; }
        public DateTime? InitiationDate { get; set; }
        public string RequestStatus { get; set; }
        public int TenantID { get; set; }
        public string EmployeeName { get; set; }
        public string RequesterName { get; set; }
        public string HrName { get; set; }
        public int TotalCount { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        //public List<OnBoardDeptBO> GetDeptList { get; set; }
    }
    public class OnBoardingResourceBO
    {
        public int EmpOnboardingID { get; set; }
        public int LocationID { get; set; }
        public string RequestStatus { get; set; }
        public string Designation { get; set; }
        public DateTime? JoiningDate { get; set; }
        public int EmpID { get; set; }
        public string EmployeeName { get; set; }
        public int TenantID { get; set; }
        public string base64Images { get; set; }
        public string Gender { get; set; }
    }
    public class OnBoardingGenralBO
    {
        public int EmpOnboardingID { get; set; }
        public int LocationID { get; set; }
        public DateTime? OfferDate { get; set; }
        public DateTime? AcceptanceDate { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string ReqStatus { get; set; }
        public int TenantID { get; set; }
        public int CreatedBy { get; set; }
        public int EmpID { get; set; }
        public int ProductID { get; set; }
        public string EntityName { get; set; }
        public string EntityType { get; set; }
    }
    public class OnBoardingRequestBO
    {
        public int EmpOnboardingID { get; set; }
        public int EmpID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string RequestNumber { get; set; }
        public string SourceRefNumber { get; set; }
        public int ReqEmpID { get; set; }
        public int HRInchargeID { get; set; }
        public int DeptID { get; set; }
        public int RoleID { get; set; }
        public int LocationID { get; set; }
        public string ReqDesc { get; set; }
        public DateTime? InitiationDate { get; set; }
        public string RequestStatus { get; set; }
        public int TenantID { get; set; }
        public string EmployeeName { get; set; }
        public string RequesterName { get; set; }
        public string HrName { get; set; }
        public int TotalCount { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public OnboardPersonalDetail employee { get; set; }        
        //public List<EmployeeAddress> Address { get; set; }
    }
    public class OnBoardingRequest
    {
        public int EmpOnboardingID { get; set; }
        public int LocationID { get; set; }
        public string RequestNumber { get; set; }
        public string SourceRefNumber { get; set; }
        //public string MPN { get; set; }
        public string RequestDesc { get; set; }
        public string RequestStatus { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public int? BenefitGroupID { get; set; }
        public int? StructureID { get; set; }
        public int? GroupTypeID { get; set; }
        public DateTime? InitiationDate { get; set; }
        public DateTime? OfferDate { get; set; }
        public DateTime? AcceptanceDate { get; set; }
        public DateTime? JoiningDate { get; set; }
        public int EmpID { get; set; }
        public int RoleID { get; set; }
        public int ReqEmpID { get; set; }
        public int CountryID { get; set; }
        public string LocationName { get; set; }
        public string EmployeeName { get; set; }
        public string RequesterName { get; set; }
        public string HrName { get; set; }
        public string employee_email { get; set; }
        public string Reporting_email { get; set; }
        public string HREmail { get; set; }
        public int TenantID { get; set; }
        public int HRInchargeID { get; set; }
        public int DeptID { get; set; }
        public string base64Images { get; set; }
        public string Gender { get; set; }
        public List<BPTransInstance> OnboardTrack {get; set;}
    }
    public class OnboardManagerInput
    {
        public int LocationID { get; set; }
        public int DeptID { get; set; }
        public int RoleID { get; set; }
        public string CategoryName { get; set; }
    }
    public class GetReportManagerBO
    {
        public int EmpID { get; set; }
        public string EmployeeName { get; set; }
        public string RoleName { get; set; }
    }
    public class HRRoleBO
    {  
        public int EmpID { get; set; }
        public string RoleName { get; set; }
        public string EmployeeName { get; set; }
    }
    public class WorkPlaceBO
    {
        public int EmpID { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public string ProcessName { get; set; }
        public string CubicleNo { get; set; }
        public int CreatedBy { get; set; }
        public List<BPchecklist> checkList { get; set; }

    }
    public class OrientationBO
    {
        public int EmpID { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public string ProcessName { get; set; }
        public List<BPchecklist> checkList { get; set; }

    }
    public class NetworkSetupBO
    {
        public int EmpID { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public string ProcessName { get; set; }
        public string NetworkDomain { get; set; }
        public string NetworkUserName { get; set; }
        public int CreatedBy { get; set; }
        public List<BPchecklist> checkList { get; set; }
    }
    public class BPTransNameBO
    {
        public int BPTransInstanceID { get; set; }
        public int BProcessID { get; set; }
        public int ReqInitID { get; set; }
        public string BPTransName { get; set; }
        public string TransStatus { get; set; }
    }
    public class NotificationBO
    {
        public int SubscriberID { get; set; }
        public string NotifyTo { get; set; }
        public string NotifySubject { get; set; }
        public string NotifyBody { get; set; }
    }
}   