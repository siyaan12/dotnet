using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.Entitlement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDodos.Service.Entitlement
{
    public interface ISecurityService
    {
        //public Response<AppUserBO> GetAppUser(string UserName, string sPass);
        Response<List<UserTyepBO>> GetUserTypeRoles(int TenantID);
        Response<List<EntServicesBO>> GetRoleToMenuServices(int RoleID);
        Response<List<EntServicesBO>> GetUserTypeToServices(int UserTypeID);
        Response<string> AddRoleServices(UserRoleBO input);
        Response<List<EntServicesBO>> GetExceptUserTypeServices(int UserTypeID, int RoleID);
        Response<List<UserTyepBO>> GetUserTypeList(int ProductID, int TenantID, int AppUserID);
        Response<UserTypeServiceListSearch> GetUserTypeServiceSearch(UserTypeServiceListSearch input);
        Response<vwUserGroupListSearch> GetEntUserGroupList(vwUserGroupListSearch input);
        Response<string> CreateUserGroup(UserGroupInput input);
        Response<List<EntServicesBO>> GetUserGroupServices(EntUserGroupMembersBO input);
        Response<List<VwEntAppUserBO>> GetEntUserGroupMembers(EntUserGroupMembersBO input);
        Response<string> AddUserGroupMember(EntUserGroupMembers input);
        Response<string> DeleteUserGroupMember(int UserGroupID, int AppUserID);
        Response<List<AppUserDetailsBO>> CreateUser(List<ActivateUserBO> input);
        Response<VwAppUserListSearch> GetEntAppUserList(VwAppUserListSearch input);
        Response<List<EntServicesBO>> GetAssocUserTypeServices(UserTypeInput input);
        Response<List<EntServicesBO>> GetAssocUserGroupServices(int UserGroupID);
        Response<string> UpdateUserPassword(ActivateUserBO input);
        Response<int> SwitchUserStatus(ActivateUserBO input);
        Response<string> RemoveRoleService(UserTypeInput input);
        Response<List<VwTblEntServices>> GetAppUserServiceList(int UserID);
        Response<List<UserServiceExceptionBO>> GetUserServiceException(int UserID);
        Response<string> AddUserServiceException(ExceptionBO input);
        Response<string> DeleteUserServiceException(int ExceptionID);
        Response<string> DeleteUserRole(int RoleID, int TenantID);
        Response<string> ChangeUserGroupStatus(UserGroupInput input);
        Response<List<VwEntAppUserList>> GetAppUserInfo(int AppUserID);
        Response<List<EntSubscribedProdMap>> GetEntSubscribedProdMap(int TenantID, int ProductID);
        Response<string> DeleteUserGroup(UserGroupInput input);
        Response<List<EntSubscribedProdMap>> GetEntSubscribedList(int AssocTenantID , int AppUserID , int ProductID);
    }
}
