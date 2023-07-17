using System;
using System.Collections.Generic;
using MyDodos.Domain.Administrative;
using MyDodos.Domain.LoginBO;
using MyDodos.Domain.Employee;

namespace MyDodos.Domain.AuthBO
{
    public class AuthLoginBO
    {
        public string AccessToken { get; set; }
        public string UserId { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expires { get; set; }
        public int AppUserID { get; set; }
    }
    public class InputLogin
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public int AppUserID { get; set; }
        public string ProductKey { get; set; }
        public string ProdKey { get; set; }
    }
    public class InputRefresh
    {
        public string RefreshToken { get; set; }
        public int AppUserID { get; set; }
        public string ProductKey { get; set; }

    }
    public class UserProfileBO
    {
        public AppUserBO AppUser { get; set; }
        public TenantBO TenantDetail { get; set; }
        public TenantProfileImageBO CompanyProfile { get; set; }
        public List<AssocUserProductBO> AssociatedProducts { get; set; }
        public List<AssociatedUserDashBoardBO> ThemeDetails { get; set; }
        public RoleBO Role { get; set; }
        public List<RtnUserGroupBO> Group { get; set; }
        public List<MenuServiceViewModel> entService { get; set; }
        public List<LoginEmployeeBO> EmployeeDetail { get; set; }
        public List<LoginLocationBO> Location { get; set; }
        public List<GeneralConfigSettings> Settings { get; set; }
        public TenantPaymentModeBO TenantPaymentMode { get; set; }
        public List<LoginYearBO> Year { get; set; }
    }
    public class InputLogOut
    {
        public int AppUserID { get; set; }
        public string ProductKey { get; set; }
        public string AccessToken { get; set; }

    }
    public class LogOutBO
    {
        public string EmailAddress { get; set; }
        public string StatusMessage { get; set; }

    }
    public class AssocUserProductBO
    {
        public int AppUserID { get; set; }
        public string AppUserName { get; set; }
        public int TenantID { get; set; }
        public string TenantName { get; set; }
        public int KProductID { get; set; }
        public string ProdKey { get; set; }
        public string ProdName { get; set; }
        public string ProdURL { get; set; }
        public string ProdLogoPath { get; set; }
        public bool IsPrimaryProd { get; set; }
    }
    public class MenuServiceViewModel
    {
        public string MenuIcon { get; set; }
        public string MenuIconV2 { get; set; }
        public int globalOrderBy { get; set; }
        public int ParentServiceID { get; set; }
        public string ResourceType { get; set; }
        public string ResourceAction { get; set; }
        public string ResourceActionV2 { get; set; }
        public string ServiceName { get; set; }
        public int EntServiceID { get; set; }
        public int ServiceOrder { get; set; }
        public List<SubMenuService> SubMenu { get; set; }
    }
    public class SubMenuService
    {
        public string MenuIcon { get; set; }
        public string MenuIconV2 { get; set; }
        public int globalOrderBy { get; set; }
        public int ParentServiceID { get; set; }
        public string ResourceType { get; set; }
        public string ResourceAction { get; set; }
        public string ResourceActionV2 { get; set; }
        public string ServiceName { get; set; }
        public int ServiceID { get; set; }
        public int ServiceOrder { get; set; }
        public int entServiceID { get; set; }
        public List<SubMenuServiceV1> SubMenu { get; set; }

    }
    public class SubMenuServiceV1
    {
        public string MenuIcon { get; set; }
        public string MenuIconV2 { get; set; }
        public int globalOrderBy { get; set; }
        public int ParentServiceID { get; set; }
        public string ResourceType { get; set; }
        public string ResourceAction { get; set; }
        public string ResourceActionV2 { get; set; }
        public string ServiceName { get; set; }
        public int ServiceID { get; set; }
        public int ServiceOrder { get; set; }
        public int entServiceID { get; set; }
        public List<SubMenuServiceV2> SubMenu { get; set; }
    }
    public class SubMenuServiceV2
    {
        public string MenuIcon { get; set; }
        public string MenuIconV2 { get; set; }
        public int globalOrderBy { get; set; }
        public int ParentServiceID { get; set; }
        public string ResourceType { get; set; }
        public string ResourceAction { get; set; }
        public string ResourceActionV2 { get; set; }
        public string ServiceName { get; set; }
        public int ServiceID { get; set; }
        public int ServiceOrder { get; set; }
        public int entServiceID { get; set; }
    }
    public class AppUserBO
    {

        public string Returnmsg { get; set; }
        public int AppuserID { get; set; }
        public string AppuserName { get; set; }
        public string AppUserPassword { get; set; }
        public string AppUserStatus { get; set; }
        public string UserName { get; set; }
        public bool IsSSoUser { get; set; }
        public int TenantID { get; set; }
    }
    public class TenantBO
    {
        public int TenantID { get; set; }
        public string TenantName { get; set; }
        public string AdministratorName { get; set; }
        public string AdministratorEmail { get; set; }
        public int SubscriptionID { get; set; }
        public int KProductID { get; set; }
        public string base64Images { get; set; }
    }
    public class RoleBO
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleCategory { get; set; }
        public string Status { get; set; }
    }
    public class SaveGroupMemberBO
    {
        public int UserGroupID { get; set; }
        public string UserGroup { get; set; }
        public int GroupTypeID { get; set; }
        public string GroupType { get; set; }
        public int TenantID { get; set; }
        public int ProductID { get; set; }
    }
    public class UserGroupBO
    {
        public int UserGroupID { get; set; }
        public int CategoryId { get; set; }
        public string UserGroup { get; set; }
        public string UserGroupDesc { get; set; }
        public bool IsStandard { get; set; }
        public string UserGroupStatus { get; set; }
        public string UserGroupcode { get; set; }
        public int Usercount { get; set; }
        public int CreatedBy { get; set; }
        public int TenantID { get; set; }
        public int ProductID { get; set; }
        public int AppUserID { get; set; }
        public bool IsAvailable { get; set; }
        public int UserTypeID { get; set; }
        public int RoleID { get; set; }
        public string EntServices { get; set; }
    }
    public class RtnUserGroupBO
    {
        public int UserGroupID { get; set; }
        public string UserGroup { get; set; }
        public int GroupTypeID { get; set; }
        public string GroupType { get; set; }
        public int TenantID { get; set; }
        public int ProductID { get; set; }        
    }    
    public class DashBoardBO
    {
        public int DashID { get; set; }
        public string DashName { get; set; }
        public int TenantID { get; set; }
        public int InstitutionID { get; set; }
        public List<DashBoardWidgetBO> WidgetList { get; set; }
    }
    public class DashBoardWidgetBO
    {
        public int WidgetID { get; set; }
        public string WidgetName { get; set; }
        public int DashID { get; set; }
        public int TenantID { get; set; }
        public int InstitutionID { get; set; }
        public List<DashBoardGraphBO> GraphList { get; set; }
    }
    public class DashBoardGraphBO
    {
        public int GraphID { get; set; }
        public string GraphName { get; set; }
        public int WidgetID { get; set; }
        public int TenantID { get; set; }
        public int InstitutionID { get; set; }
    }
    public class AssociatedUserDashBoardBO
    {
        public int AssoDashID { get; set; }
        public int AppUserID { get; set; }
        public string DashWidgets { get; set; }
        public string ThemeSettings { get; set; }
        public string GridSettings { get; set; }
        public int InstitutionID { get; set; }
        public int TenantID { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
    public class UserLogoUpload
    {
        public int TenantID { get; set; }
        public int InstitutionID { get; set; }
        public int ProductID { get; set; }
    }

    public class ReturnAppUserBO
    {
        public int AppUserID { get; set; }
        public string AppUserName { get; set; }
        public string AppUserPassword { get; set; }
        public string AppUserStatus { get; set; }
        public string UserType { get; set; }
    }
    public class InputAppUserBO
    {
        public int AppUserID { get; set; }
        public int EmpID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int TenantID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int CreatedBy { get; set; }
        public int RoleID { get; set; }
        public int EntityID { get; set; }
        public string UserAccessType { get; set; }
        public string AppUserStatus { get; set; }
        public int ProductID { get; set; }
        public int DefaultUserGroupID { get; set; }
        public string PrimaryEmail { get; set; }
    }
    // public class loginBO
    // {
    //     public int EmpID { get; set; }
    //     public long AppUserID { get; set; }
    //     public int FuncRoleID { get; set; }
    //     public int TenantID { get; set; }
    //     public string EmpNumber { get; set; }
    //     public string FullName { get; set; }
    //     public string OfficeEmail { get; set; }
    //     public string RoleName { get; set; }
    //     public string ProfileImage { get; set; }
    //     public int EmpLocID { get; set; } 
    //     public string ReportingMail { get; set; }
    //     public string Gender { get; set; }
    //     public string AppUserStatus { get; set; }
    //     public int YearID { get; set; }
    //     public bool IsActivated { get; set; }
    //     public string IsReptManager { get; set; }
    // }  
    // public class GenDocumentBO
    // {
    //     public int DocID { get; set; }
    //     public string DocumentName { get; set; }
    //     public int RepositoryID { get; set; }
    //     public string DocType { get; set; }
    //     public int Rentention { get; set; }
    //     public string DocNumber { get; set; }
    //     public DateTime CreatedOn { get; set; }
    //     public int CreatedBy { get; set; }
    //     public string DocStatus { get; set; }
    //     public string OrgDocName { get; set; }
    //     public string GenDocName { get; set; }
    //     public string TokenID { get; set; }
    //     public DateTime ValidUpto { get; set; }
    //     public int TenantID { get; set; }
    //     public string Entity { get; set; }
    //     public int EntityID { get; set; }
    //     public int DocTypeID { get; set; }
    //     public int InstitutionID { get; set; }
    //     public string DocKey { get; set; }
    //     public decimal DocSize { get; set; }
    //     public string DirectionPath { get; set; }
    // }
    // public class DocContainerBO
    // {
    //     public int ContainerID { get; set; }
    //     public string ContainerName { get; set; }
    //     public string ContainerStatus { get; set; }
    //     public int TenantID { get; set; }
    //     public int ProductID { get; set; }
    // }
    public class TAppUser
    {
        public string AppUserName { get; set; }
        public string AppUserPassword { get; set; }
        public string AuthPIN { get; set; }
        public int TenantID { get; set; }
    }
    //public class AppUserDetailsBO
    //{
    //    public AppUser AppUser { get; set; }
    //    public EntMyRolesBO MyRole { get; set; }
    //    public EntUserRole UserRole { get; set; }
    //}
    //public class AppUser
    //{
    //    public int AppUserID { get; set; }
    //    public string AppUserName { get; set; }
    //    public string AppUserPassword { get; set; }
    //    public string AppUserStatus { get; set; }        
    //    public int PrimaryCompanyID { get; set; }
    //    public string ProfilePhoto { get; set; }
    //    public string Prefix { get; set; }
    //    public string FirstName { get; set; }
    //    public string MiddleName { get; set; }
    //    public string LastName { get; set; }
    //    public string Suffix { get; set; }
    //    public bool IsActivated { get; set; }
    //    public string ActivatedOn { get; set; }
    //    public string ProfileImageID { get; set; }
    //    public int CreatedBy { get; set; }
    //    public string CreatedOn { get; set; }
    //    public int ModifiedBy { get; set; }
    //    public string ModifiedOn { get; set; }
    //    public int FailedAttempts { get; set; }
    //    //public string CurrentSigninAt { get; set; }
    //    public string CurrentSigninIP { get; set; }
    //    //public string LastSigninAt { get; set; }
    //    public string LastSigninIP { get; set; }
    //    public string PrimaryEmail { get; set; }
    //    public string SecondaryEmail { get; set; }
    //    public string ContactPhone { get; set; }
    //    public string UserType { get; set; }
    //    public bool ChangePassword { get; set; }
    //    public string LoginPIN { get; set; }
    //    public bool IsSSoUser { get; set; }
    //    public int LocationID { get; set; }
    //    public int RoleID { get; set; }
    //    public string Department { get; set; }
    //    public string ActivationKey { get; set; }
    //    public string DefaultPage { get; set; }
    //    public int ReportToRoleID { get; set; }
    //}
    public class inputAppUserModel
    {
        public int AppUserID { get; set; }
        public string Status { get; set; }
    }
    public class EntMyRolesBO
    {
        public int MyRoleId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string RoleStatus { get; set; }
        public int ProductId { get; set; }
    }
    public class EntUserRole
    {
        public int RoleUserId { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public bool IsPrimaryRole { get; set; }
        public string UserRoleStatus { get; set; }
    }
    public class AssociateUserBO
    {
        public int ID { get; set; }
        public int AssociateUserID { get; set;}
        public int AppuserID { get; set;}
        public string RelationshipStatus { get; set; }
        public bool IsPrimary { get; set; }
        public string AppUserName { get; set; }
        public string AppUserPassword { get; set; }
        public string FirstName { get; set;}
        public string LastName { get; set;}
        public string MiddleName { get; set;}
        public int TenantID { get; set;}
        public string SubscriptionStatus { get; set;}
        public string TenantType { get; set;}
        public string TenantName { get; set; }
        public string TenantCode { get; set; }
        public string TenantStatus { get; set; }
        public int KProductID { get; set; }
        public bool isExclTenantUser { get; set; }
        public bool ChangePassword { get; set; }
        public int AcctID { get; set; }
        public string UserType { get; set; }
        public string AppUserStatus { get; set; }
        public string DefaultPage { get; set; }
        public string DefaultPageV2 { get; set; }
        public string SecondaryEmail { get; set;}
        public string PrimaryEmail { get; set;}
        public string ProdStatus { get; set;}
        public string ProdName { get; set;}
        public string ProdKey { get; set;}
    }
    public class OTPSentBO
    {
        public string AppUserName { get; set; }
        public int OTP { get; set;}
        public string ProductKey { get; set; }
        public string EntityType { get; set; }
    }
    public class OTPOutputSentBO
    {
        public int AppUserID { get; set; }
        public int OTP { get; set;}
        public string msg { get; set; }
    }
    public class PasswordBO
    {
        public int AppUserID { get; set; }
        public string AppUserName { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string CurrentPassword  { get; set; }
        public bool IsReset { get ; set; }
        public string ProductKey { get; set; }
        public int ProductID { get; set; }
    }
public class CognitoReturn
    {
        public string userId { get; set; }
        public string emailAddress { get; set; }
        public string statusMessage { get; set; }
    }
    public class CognitoSignUp
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string Website { get; set; }
        public string Gender { get; set; }
        public string ProductKey { get; set; }
    }
    public class ResetPassword
    {
        public string EmailAddress { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmationCode { get; set; }
        public string ProductKey { get; set; }
    }
    public class MDemoRequest
    {
        public int RequestID { get; set; }
        public string RequesterName { get; set; }
        public string Designation { get; set; }
        public string CompanyName { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public string InterestModules { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int ProductID { get; set; }
        public string ProductKey { get; set; }

    }
    public class MDetailRequest
    {
        public int DetailRequestID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int ProductID { get; set; }
        public string ProductKey { get; set; }
    }
    public class MUserProfileBO
    {
        public AppUserBO AppUser { get; set; }
        public TenantBO TenantDetail { get; set; }
        //public TenantProfileImageBO CompanyProfile { get; set; }
        public List<AssocUserProductBO> AssociatedProducts { get; set; }
        //public List<AssociatedUserDashBoardBO> ThemeDetails { get; set; }
        public RoleBO Role { get; set; }
        public List<RtnUserGroupBO> Group { get; set; }
        public List<MenuServiceViewModel> entService { get; set; }
        public List<LoginEmployeeBO> EmployeeDetail { get; set; }
        public List<LoginLocationBO> Location { get; set; }
        //public List<GeneralConfigSettings> Settings { get; set; }
        //public TenantPaymentModeBO TenantPaymentMode { get; set; }
        public List<LoginYearBO> Year { get; set; }
    }
}
