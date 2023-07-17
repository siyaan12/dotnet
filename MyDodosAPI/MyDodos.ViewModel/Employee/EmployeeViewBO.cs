using System;
using System.Collections.Generic;
using MyDodos.Domain.Employee;
using MyDodos.Domain.HR;
using MyDodos.ViewModel.HR;
using MyDodos.ViewModel.ServerSearch;

namespace MyDodos.ViewModel.Employee
{
    public class GetHRDirectoryBO
    {
        public int EmpID { get; set; }
        public string DetEncrpt { get; set; }
        public string DetDecrypt { get; set; }
        public int FuncRoleID { get; set; }
        public int DepartmentID { get; set; }
        public int HRInchargeID { get; set; }
        public int LocationID { get; set; }
        public string LocationName { get; set; }
        public string EmpNumber { get; set; }
        public string FullName { get; set; }
        public string ReportName { get; set; }
        public string ProfileImage { get; set; }
        public string MobileNo { get; set; }
        public string PersonalMail { get; set; }
        public string Gender { get; set; }
        public string EmpStatus { get; set; }
        public string RoleName { get; set; }
        public DateTime? DOB { get; set; }
        public string Department { get; set; }
        public int TenantID { get; set; }
        public bool IsCompleted { get; set; }
        public int TotalCount { get; set; }
        public string base64Images { get; set; }
        public string HRNotification { get; set; }
        public int? BenefitGroupID { get; set; }
        public int StructureID { get; set; }
        public List<string> ListHRNotification { get; set; }
        //public int InCompletedCount { get; set; }
        public List<BPTransNameBO> OnBoardTrackStatus { get; set; }
    }
    public class GetHRDirectoryList
    {
        public GetHRDirectoryListInputs objHRDirectoryInput { get; set; }
        public ServerSearchable ServerSearchables { get; set; }
        public List<HRDirectorySummeryBO> Summary { get; set; }
        public UserViewBO UserView { get; set; }
        public InCompleteCountBO InCompletedCounts { get; set; }
        public List<GetHRDirectoryBO> objHRDirectoryList { get; set; }
    }
    public class GetHRDirectoryListInputs
    {
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int DepartmentID { get; set; }
        public bool IsCompleted { get; set; }
        public int EmpID { get; set; }
        public int ManagerID { get; set; }
        public int YearID { get; set; }
        public int productId { get; set; }
    }
    public class HRDirectorySummeryBO
    {
        public int TotalEmployees { get; set; }
        public int Male { get; set; }
        public int Female { get; set; }
        public int Others { get; set; }
        public int NewEmployee { get; set; }        
        public int NumberofLeave { get; set; }
    }
    public class ManagerBO
    {
        public int EmpID { get; set; }
        public int LocationID { get; set; }
        public string EmpNumber { get; set; }
        public string FullName { get; set; }
        public int TenantID { get; set; }
        public string HStatus { get; set; }
    }
    public class UserViewBO
    {
        public string CommonName { get; set; }
        //public List<EmployeeView> EmployeeViewList { get; set; }
    }
    public class InCompleteCountBO
    {
        public bool IsCompleted { get; set; }
        public int InCompletedCount { get; set; }
    }
    public class EmployeeView
    {
        public int EmpID { get; set; }
        public string EmpNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int RoleID { get; set; }
        public int ManagerID { get; set; }
        public int DepartmentID { get; set; }
        public int BenefitGroupID { get; set; }
        public string Gender { get; set; }
        public DateTime? DOJ { get; set; }
        public DateTime? DOB { get; set; }
        public string OfficeEmail { get; set; }
        public string PersonalMail { get; set; }
        public string MobileNo { get; set; }
        public string EmpType { get; set; }
        public string BloodGroup { get; set; }
        public string Nationality { get; set; }
        // public DateTime? OfferDate { get; set; }
        // public DateTime? AcceptanceDate { get; set; }
        // public DateTime? JoiningDate { get; set; }
        public int TenantID { get; set; }
        public bool IsCompensate { get; set; }
        public bool IsTimeSheet { get; set; }
        public bool IsAttendance { get; set; }
        public int HRInchargeID { get; set; }
        public string EmpStatus { get; set; }
        public string ProfileImage { get; set; }
        public int AppUserID { get; set; }
        //public string AppUserName { get; set; }
        public bool IsCompleted { get; set; }
        public string base64images { get; set;}
        public string CommonName { get; set; }
    }
    public class HRDirectoryEmpView
    {
        public int EmpID { get; set; }
        public string EmpNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public DateTime? OfferDate { get; set; }
        public DateTime? AcceptanceDate { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime? DateOfHire { get; set; }
        public string EmpType { get; set; }
        public string Designation { get; set; }
        public string EmpStatus { get; set; }
        public string ProfileImage { get; set; }
        public string base64images { get; set;}
        public string Gender { get; set; }
        public string AadharNo { get; set; }
        public int AppUserID { get; set; }
        public string AppUserName { get; set; }
        public int DeptID { get; set; }
        public string Department { get; set; }
        public int ManagerID { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; } 
        public int ProductID { get; set; }
        // public HREmpPersonalInfo PersonalInformation { get; set; }
        // public List<EmployeeAddress> Address { get; set; }
        public HREmpPersonalInfo PersonalInformation { get; set; }
        public Employment Employment { get; set; }
        //public List<HREmpEducation> Education { get; set; }
        //public List<HREmpExperience> Experience { get; set; }
    }
    public class Employment
    {
        public List<HREmpEducation> Education { get; set; }
        public List<HREmpExperience> Experience { get; set; }
    }
    public class HREmpPersonalInfo
    {
        public int EmpID { get; set; }
        //public string EmpNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Suffix { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string BloodGroup { get; set; }
        public string MaritalStatus { get; set; }
        public string Nationality { get; set; }
        public string EmerContactNumber { get; set; }
        public string EmerRelationShip { get; set; }
        public string OfficeContactNumber { get; set; }
        public string PersonalMail { get; set; }
        public string MobileNo { get; set; }
        public string AlternatePhoneNo { get; set; }
        public bool IsCompensate { get; set; }
        public bool IsTimeSheet { get; set; }
        public bool IsAttendance { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; } 
        public List<EmployeeAddress> Address { get; set; }
    }
    public class HREmpAddress
    {
        public int AddressID { get; set; }
        public int EmpRefID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string AddressStatus { get; set; }
        public string Phone { get; set; }
        public string EmerContactNumber { get; set; }
        public string PersonalMail { get; set; }
        public string EmerRelationShip { get; set; }
    }
    public class OrgChartBO
    {
        public List<EmpReportingBO> Reporting { get; set; }
        public List<EmpDirectorBO> Directors { get; set; }
        public List<EmpColleaguesBO> Colleagues { get; set; }
    }
    public class EmpReportingBO
    {
        public int EmpID { get; set; }
        public string FullName { get; set; }
        public string EmpStatus { get; set; }
        public string ProfileImage { get; set; }
        public int FuncRoleID { get; set; }
        public string Gender { get; set; }
        public string Designation { get; set; }
        public string base64images { get; set;}
        public string MobileNo { get; set; }
        public string PersonalMail { get; set; }
        public string OfficeEmail { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int DepartmentID { get; set; }
        public string Department { get; set; }
        //public List<EmpDirectorBO> Childs { get; set; }
    }
    public class EmpDirectorBO
    {
        public int EmpID { get; set; }
        public string FullName { get; set; }
        public string EmpStatus { get; set; }
        public string ProfileImage { get; set; }
        public int FuncRoleID { get; set; }
        public string Gender { get; set; }
        public string Designation { get; set; }
        public string base64images { get; set;}
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        //public List<EmpColleaguesBO> Childs { get; set; }
    }
    public class EmpColleaguesBO
    {
        public int EmpID { get; set; }
        public string FullName { get; set; }
        public string EmpStatus { get; set; }
        public string ProfileImage { get; set; }
        public int FuncRoleID { get; set; }
        public string Gender { get; set; }
        public string Designation { get; set; }
        public string base64images { get; set;}
        public int TenantID { get; set; }
        public int LocationID { get; set; }
    }
    public class EmployeeProfileBO
    {
        public EmployeeProfileInputBO EmpProfile { get; set; }  
        public InputDocsBO inputDocs { get; set; }
    }
    public class EmployeeProfileInputBO
    {
        public int AppUserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string PrimaryEmail { get; set; }
        public string SecondaryEmail { get; set; }
        public string ContactPhone { get; set; }
        public int PrimaryCompanyID { get; set; }
        public string base64Images { get; set; }
        public string FileName { get; set; }
        public int DocID { get; set; }
        public int ProductID { get; set; }
        public int EmpID { get; set; }
        public int ModifiedBy { get; set; }
    }
    public class EmpReportingOrgBO
    {   

        public int EmpID { get; set; }
        public string Gender { get; set; }
        public int ManagerID { get; set; }
        public int FuncRoleID { get; set; }
        public string EmployeeName { get; set; }
        public string RoleName { get; set; }
        public string base64images { get; set;}
        public string Classtext { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public List<EmpReportingOrgBO> Children { get; set; }
        public EmpReportingOrgBO()
        {
            Children = new List<EmpReportingOrgBO>();
        }
        public void AddChild(EmpReportingOrgBO child)
        {
            Children.Add(child);
        }
    }
    // public class EmpChildOrgBO
    // {
    //     public int EmpID { get; set; }
    //     public string Name { get; set; }
    //     public int ManagerID { get; set; }
    //     public string Image { get; set; }
    //     //public int FuncRoleID { get; set; }
    //     //public string Gender { get; set; }
    //     public string Title { get; set; }
    //     //public string base64images { get; set;}
    //     public string Class { get; set; }
    //     public int TenantID { get; set; }
    //     public int LocationID { get; set; }
    //     public List<OthersChildOrgBO> Childs { get; set; }
    // }
    // public class OthersChildOrgBO
    // {
    //     public int EmpID { get; set; }
    //     public string Name { get; set; }
    //     public int ManagerID { get; set; }
    //     public string Image { get; set; }
    //     //public int FuncRoleID { get; set; }
    //     //public string Gender { get; set; }
    //     public string Title { get; set; }
    //     //public string base64images { get; set;}
    //     public string Class { get; set; }
    //     public int TenantID { get; set; }
    //     public int LocationID { get; set; }
    //     public List<EmpReportingOrgBO> Childs { get; set; }
    // }
    public class EmpDirectorOrgBO
    {
        public int EmpID { get; set; }
        public string Name { get; set; }
        public int ManagerID { get; set; }
        public string Image { get; set; }
        //public int FuncRoleID { get; set; }
        //public string Gender { get; set; }
        public string Title { get; set; }
        //public string base64images { get; set;}
        public string CssClass { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public List<EmpColleaguesOrgBO> Childs { get; set; }
    }
    public class EmpColleaguesOrgBO
    {
        public int EmpID { get; set; }
        public string Name { get; set; }
        //public string EmpStatus { get; set; }
        public string Image { get; set; }
        //public int FuncRoleID { get; set; }
        //public string Gender { get; set; }
        public string Title { get; set; }
        //public string base64images { get; set;}
        public string CssClass { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
    }
    public class CompanyProfileBO
    {
        public TenantProfiledataBO TenantProfile { get; set; }  
        public InputDocBO inputDocs { get; set; }
    }
    public class InputDocBO
    {
        public int RegularDocID { get; set; }
        public string RegularLogodocName { get; set; }
        public string RegularLogodocsFile { get; set; }
        public decimal RegulardocsSize { get; set; }
        public int SmallDocID { get; set; }
        public string SmallLogodocName { get; set; }
        public string SmallLogodocsFile { get; set; }
        public decimal SmalldocsSize { get; set; }
    }
    public class AccountDetailsBO
    {
        public int EmpSalAccID { get; set; }
        public int EmpID { get; set; }
        public string AccNumber { get; set; }
        public string BranchName { get; set; }
        public string BankName { get; set; }
        public string IFSCCode { get; set; }
        public string AccType { get; set; }
        public string AccStatus { get; set; }
        public string PFNO { get; set; }
        public string ESINO { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifedBy { get; set; }
        public DateTime? ModifiedOn { get; set; } 
    }
    public class EmpPersonalDetail
    {
        public int EmpID { get; set; }
        public string EmpNumber { get; set; }
        public string EmpStatus { get; set; }        
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Suffix { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string Nationality { get; set; }
        public string MaritalStatus { get; set; }
        public string MobileNo { get; set; }
        public string AlternatePhoneNo { get; set; }
        public string BloodGroup { get; set; }
        public string PersonalMail { get; set; }
        public string OfficeEmail { get; set; }
        public string Designation { get; set; }
        public string base64images { get; set;}
        public int DepartmentID { get; set; }
        public string Department { get; set; }
        public int AppUserID { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
    }
    public class MyTeamBO
    {
        public List<EmpPersonalDetail> EmpDetails { get; set; }
        public List<EmpReportingBO> ReportingTo { get; set; }
        public List<vwProjectBO> ProjectList { get; set; }
    }
    public class vwProjectBO
    {
        public int ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public int YearID { get; set; }
        public string ManagerID { get; set; }
        public string ProjStatus { get; set; }
        public string IsActive { get; set; }
        public bool IsProjectManager { get; set; }
        public DateTime? EstStartDate { get; set; }
        public DateTime? EstEndDate { get; set; }
        public int TenantID { get; set; }
        public List<EmpPersonalDetail> ManagerDetails { get; set; }
        public List<EmpPersonalDetail> Colleagues { get; set; }
    }
    public class EntRoles
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public int ReportToRoleID { get; set; }
        public string RoleCategory { get; set; }
        public int KProductID { get; set; }
        public string DefaultPage { get; set; }
        public string DefaultPageV2 { get; set; }
        public bool IsSeedRole { get; set; }
        public bool IsSignatory { get; set; }
        public string RoleStatus { get; set; }
        public int UserTypeID { get; set; }
        public int TenantID { get; set; }
    }
    public class EmpManagerDropdown
    {
        public int EmpID { get; set; }
        public string EmpNumber { get; set; }
        public string ManagerName { get; set; }
        public int FuncRoleID { get; set; }
    }
}