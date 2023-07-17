using MyDodos.ViewModel.ServerSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDodos.ViewModel.Entitlement
{
    public class TenantProfileBO
    {
        public int TenantID { get; set; }
        public string TenantCode { get; set; }
        public string TenantName { get; set; }
        public string TenantType { get; set; }
        public string DomainPrefix { get; set; }
        public string ShortName { get; set; }
        public int TimeZoneID { get; set; }
        public string inCorpState { get; set; }
        public string TaxID { get; set; }
        public string TenantStatus { get; set; }
        public string AdministratorName { get; set; }
        public string AdministratorEmail { get; set; }
        public string AdministratorPhone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public int KProductID { get; set; }
        public string PrimaryPhone { get; set; }
        public string AlternatePhone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public int ParentTenantID { get; set; }
        public string CreatedFormIP { get; set; }
        public int DataPartitionID { get; set; }
    }
    public class AddTenantDataBO
    {
        public TenantProfileBO tenantdetails { get; set; }
        public List<CreateGroupBO> usergroupdetails { get; set; }


    }
    public class CreateGroupBO
    {
        public int GroupTypeID { get; set; }
        public string GroupName { get; set; }
        public int UserGroupID { get; set; }
        public int TenantID { get; set; }
        public string TenantCode { get; set; }
        public int SubscriptionID { get; set; }
        public int ProductID { get; set; }
    }
    public class ReturnTenantDetails
    {
        public int TenantID { get; set; }
        public string TenantCode { get; set; }
        public string UserName { get; set; }
        public string TenantPassword { get; set; }

    }
    public class UserTyepBO
    {
        public int UserTypeID { get; set; }
        public string UserType { get; set; }
        public string CommonName { get; set; }
        public string TenantType { get; set; }
        public string EntityLevel { get; set; }
        public string Description { get; set; }
        public int ProductID { get; set; }
        public List<EntRolesBO> Roles { get; set; }
    }
    public class EntRolesBO
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string CommonName { get; set; }
        public string RoleDescription { get; set; }
        public int ReportToRoleId { get; set; }
        public string RoleCategory { get; set; }
        public int KproductId { get; set; }
        public string DefaultPage { get; set; }
        public string DefaultPageV2 { get; set; }
        public bool IsSeedRole { get; set; }
        public bool IsSignatory { get; set; }
        public int UserTypeID { get; set; }
        public int TenantID { get; set; }
        public int CreatedBy { get; set; }
        public List<EntRolesBO> Roles { get; set; }
    }
    public class UserTypeInput
    {
        public int AppUserID { get; set; }
        public int ProductID { get; set; }
        public int TenantID { get; set; }
        public string TenantType { get; set; }
        public string EntityType { get; set; }
        public int UserTypeID { get; set; }
        public int RoleId { get; set; }
        public int SubRoleID { get; set; }
        public int EntServiceID { get; set; }
    }
    public class ExceptionBO
    {
        public int AppUserID { get; set; }
        public int ServiceId { get; set; }

    }
    public class EntRole
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public int ReportToRoleID { get; set; }
        public string RoleCategory { get; set; }
        public int KProductID { get; set; }
        public string DefaultPage { get; set; }
        public string DefaultPagev2 { get; set; }
        public bool IsSeedRole { get; set; }
    }
    public class VwTblEntServices
    {
        public int EntServiceId { get; set; }
        public string ServiceName { get; set; }
        public int? ServiceOrder { get; set; }
        public int? GlobalOrderBy { get; set; }
        public string ResourceAction { get; set; }
        public string ResourceActionV2 { get; set; }
        public string MenuIcon { get; set; }
        public string MenuIconV2 { get; set; }
        public string ResourceType { get; set; }
        public int? ParentServiceId { get; set; }
        public string ServiceDesc { get; set; }
        public string ServiceGroup { get; set; }
        public bool? Read_Attri { get; set; }
        public bool? Write_Attri { get; set; }
        public bool? Delete_Attri { get; set; }
        public bool? Approve_Attri { get; set; }
        public bool? Export_Attri { get; set; }
        public bool? IsEntitlable { get; set; }
        public string DisplayIndicator { get; set; }
        public bool? IsChild { get; set; }
        //public string MenuIcon { get; set; }
        public bool? IsRoleRestricted { get; set; }
        public string AllowedRoles { get; set; }
        public int? KproductId { get; set; }
        public string ServiceCategory { get; set; }
        public string SrvCode { get; set; }
    }
    public class EntServicesBO
    {
        public int EntServiceId { get; set; }
        public string ServiceName { get; set; }
        public int ServiceOrder { get; set; }
        public int GlobalOrderBy { get; set; }
        public string ResourceAction { get; set; }
        public string ResourceActionV2 { get; set; }
        public string MenuIcon { get; set; }
        public string MenuIconV2 { get; set; }
        public string ResourceType { get; set; }
        public int ParentServiceId { get; set; }
        public string ServiceDesc { get; set; }
        public string ServiceGroup { get; set; }
        public bool Read_Attri { get; set; }
        public bool Write_Attri { get; set; }
        public bool Delete_Attri { get; set; }
        public bool Approve_Attri { get; set; }
        public bool Export_Attri { get; set; }
        public bool IsEntitlable { get; set; }
        public string DisplayIndicator { get; set; }
        public bool IsChild { get; set; }
        public bool IsRoleRestricted { get; set; }
        public string AllowedRoles { get; set; }
        public int KproductId { get; set; }
        public string ServiceCategory { get; set; }
        public string SrvCode { get; set; }
        public bool IsAssociate { get; set; }
        public List<EntServicesBO> SubMenu { get; set; }
    }
    public class UserServiceExceptionBO
    {
        public string ServiceName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AppUserName { get; set; }
        public DateTime CreatedOn { get; set; }
        public int EntServiceID { get; set; }
        public int EntServiceExpID { get; set; }
        public int AppUserID { get; set; }

    }
    public class UserTypeServiceListSearch
    {
        public UserTypeInput UserTypeParams { get; set; }
        public ServerSearchable ServerSearchables { get; set; }
        public List<EntServicesBO> UserTypeServiceLists { get; set; }
    }
    public class vwUserGroupListSearch
    {
        public UserTypeInput UserGroupParams { get; set; }
        public ServerSearchable ServerSearchables { get; set; }
        public List<VwUserGroupBO> userGrpOrderLists { get; set; }
    }
    public class VwAppUserListSearch
    {
        public UserInput AppUserParams { get; set; }
        public ServerSearchable ServerSearchables { get; set; }
        public List<VwEntAppUserList> AppUserLists { get; set; }
    }
    public class VwEntAppUserList
    {
        public long AppUserId { get; set; }
        public string AppUserName { get; set; }
        public string AppUserPassword { get; set; }
        public string AppUserStatus { get; set; }
        public int PrimaryCompanyId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string ActivationKey { get; set; }
        public bool IsActivated { get; set; }
        public DateTime? ActivatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? FailedAttempts { get; set; }
        public int SubscriptionId { get; set; }
        public DateTime? AssociatedOn { get; set; }
        public string TenantCode { get; set; }
        public string TenantName { get; set; }
        public string TenantStatus { get; set; }
        public string TenantType { get; set; }
        public string EntityLevel { get; set; }
        public int UserTypeID { get; set; }
        public string UserType { get; set; }
        public string ProdKey { get; set; }
        public string ProdName { get; set; }
        public int KproductId { get; set; }
        public int TenantId { get; set; }
        public bool IsPrimaryProd { get; set; }
        public string SubscriptionStatus { get; set; }
        public string ProdStatus { get; set; }
        public string SubscriptionAssoStatus { get; set; }
        public string PrimaryEmail { get; set; }
        public string SecondaryEmail { get; set; }
        public string ContactPhone { get; set; }
        public string ReturnCode { get; set; }
        public bool ChangePassword { get; set; }
        public string Status { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int ReportToRoleID { get; set; }
        public string RoleCategory { get; set; }
        public string DefaultPage { get; set; }
        public string DefaultPageV2 { get; set; }
    }
    public class UserInput
    {
        public int TenantId { get; set; }
        public int ProductId { get; set; }
        public int GroupId { get; set; }
        public string Status { get; set; }
        public string Category { get; set; }
        public int AppUserId { get; set; }
        public bool IsEmpSearch { get; set; }
        public int UserTypeID { get; set; }
        public int RoleID { get; set; }
    }
    public class VwUserGroupBO
    {
        public int UserGroupID { get; set; }
        public string UserGroup { get; set; }
        public string UserGroupDesc { get; set; }
        public bool IsStandard { get; set; }
        public string UserGroupStatus { get; set; }
        public int SubscriptionID { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ProductID { get; set; }
        public int TenantID { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleStatus { get; set; }
        public int UserTypeID { get; set; }
        public string UserType { get; set; }
        public string TenantType { get; set; }
        public string EntityLevel { get; set; }
    }
    public class UserGroupInput
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
    public class EntSubscribedProdMap
    {
        public int SubscriptionMapID { get; set; }
        public int AssocTenantID { get; set; }
        public int TenantID { get; set; }
        public int SubscriptionID { get; set; }
        public int ProductID { get; set; }
        public string TenantName { get; set; }
        public string TenantCode { get; set; }
        public string TenantType { get; set; }
        public string ProdName { get; set; }
        public string ProdKey { get; set; }
        public List<UserTyepBO> UserType { get; set; }
        public VwEntAppUserList AppUser { get; set; }
    }
    public class UserRoleBO
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleCategory { get; set; }
        public string RoleDescription { get; set; }
        public int ReportToRoleID { get; set; }
        public string RoportingToRole { get; set; }
        public int TenantId { get; set; }
        public int ProductID { get; set; }
        public string RoleStatus { get; set; }
        public string UserRoleStatus { get; set; }
        public bool IsPrimary { get; set; }
        public int UserID { get; set; }
        public int UserTypeID { get; set; }
        public string EntServices { get; set; }
    }
    public class EntUserGroupMembersBO
    {
        public int grpMemID { get; set; }
        public int AppUserID { get; set; }
        public int TenantID { get; set; }
        public int ProductID { get; set; }
        public int UserGroupID { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsMembersList { get; set; }
        public bool IsAvailable { get; set; }

    }
    public class VwEntAppUserBO
    {
        public int AppUserID { get; set; }
        public string AppUserName { get; set; }
        public string AppUserStatus { get; set; }
        public int TenantID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FullName { get; set; }
        public string UserType { get; set; }
        public string SecondaryEmail { get; set; }
        public string PrimaryEmail { get; set; }
        public bool ChangePassword { get; set; }
        public string LastSigninAt { get; set; }
        public string LastSigninIP { get; set; }
    }
    public class EntUserGroupMembers
    {
        public int grpMemID { get; set; }
        public int AppUserID { get; set; }
        public int TenantID { get; set; }
        public int ProductID { get; set; }
        public int UserGroupID { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
    public class ActivateUserBO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int TenantID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int CreatedBy { get; set; }
        public int RoleID { get; set; }
        public string UserAccessType { get; set; }
        public int ProductID { get; set; }
        public int DefaultUserGroupID { get; set; }
        public string PrimaryEmail { get; set; }
        public int AppUserID { get; set; }
        public bool IsSSoUser { get; set; }
        public bool SendNotify { get; set; }
        public string Status { get; set; }
        public int UserTypeID { get; set; }
    }
    public class AppUserDetailsBO
    {
        public AppUser AppUser { get; set; }
        public EntMyRolesBO MyRole { get; set; }
        public EntUserRole UserRole { get; set; }
    }
    public class AppUser
    {
        public int AppUserID { get; set; }
        public string AppUserName { get; set; }
        public string AppUserPassword { get; set; }
        public string AppUserStatus { get; set; }
        public int PrimaryCompanyID { get; set; }
        public string ProfilePhoto { get; set; }
        public string Prefix { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public bool IsActivated { get; set; }
        public string ActivatedOn { get; set; }
        public string ProfileImageID { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public int FailedAttempts { get; set; }
        public string CurrentSigninAt { get; set; }
        public string CurrentSigninIP { get; set; }
        public string LastSigninAt { get; set; }
        public string LastSigninIP { get; set; }
        public string PrimaryEmail { get; set; }
        public string SecondaryEmail { get; set; }
        public string ContactPhone { get; set; }
        public string UserType { get; set; }
        public bool ChangePassword { get; set; }
        public string LoginPIN { get; set; }
        public bool IsSSoUser { get; set; }
        public int LocationID { get; set; }
        public int RoleID { get; set; }
        public string Department { get; set; }
        public string ActivationKey { get; set; }
        public string DefaultPage { get; set; }
        public int ReportToRoleID { get; set; }
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
    public partial class VwEntAppUser
    {
        public long AppUserId { get; set; }
        public string AppUserName { get; set; }
        public string AppUserPassword { get; set; }
        public string AppUserStatus { get; set; }
        public int? PrimaryCompanyId { get; set; }
        public string ProfilePhoto { get; set; }
        public string Prefix { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string ActivationKey { get; set; }
        public bool? IsActivated { get; set; }
        public DateTime? ActivatedOn { get; set; }
        public int? ProfileImageId { get; set; }
        public int? ReportingToId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? FailedAttempts { get; set; }
        public DateTime? CurrentSigninAt { get; set; }
        public string CurrentSigninIp { get; set; }
        public DateTime? LastSigninAt { get; set; }
        public string LastSigninIp { get; set; }
        public int SubscriptionId { get; set; }
        public DateTime? AssociatedOn { get; set; }
        public string TenantCode { get; set; }
        public string TenantName { get; set; }
        public string TenantType { get; set; }
        public string TenantStatus { get; set; }
        public string ProdKey { get; set; }
        public string ProdName { get; set; }
        public int KproductId { get; set; }
        public int TenantId { get; set; }
        public bool? IsPrimaryProd { get; set; }
        public string SubscriptionStatus { get; set; }
        public string ProdStatus { get; set; }
        public string SubscriptionAssoStatus { get; set; }
        public string DefaultPage { get; set; }
        public string PrimaryEmail { get; set; }
        public string SecondaryEmail { get; set; }
        public string ContactPhone { get; set; }
        public string ReturnCode { get; set; }
        public string UserType { get; set; }
        public bool? IsExclTenantUser { get; set; }
        public bool? ChangePassword { get; set; }
        public int? AcctId { get; set; }
        public int? SignatureId { get; set; }
        public string Status { get; set; }
        public string CategoryType { get; set; }
        public int RoleId { get; set; }
        public int UserGroupID { get; set; }
        public string UserGroup { get; set; }
        public int OldUserGroupID { get; set; }
    }
    public class ServerSearchable
    {
        public int Draw { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public string SearchData { get; set; }
        public string OrderByColumn { get; set; }
        public string OrderBy { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
