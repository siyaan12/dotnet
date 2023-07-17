using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Wrapper;
using MyDodos.Service.Entitlement;
using MyDodos.Service.Logger;
using MyDodos.ViewModel.Entitlement;
using System.Collections.Generic;
using System;

namespace MyDodos.API.Controllers.Entitlement
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly ILoggerManager _logger;
        private string _JwtKey;
        private string _JwtIssuer;
        public SecurityController(ISecurityService securityService, IConfiguration configuration)
        {
            _securityService = securityService;
            //_logger = logger;
            //_JwtKey = Convert.ToString(configuration.GetSection("JwtKey").Value);
            //_JwtIssuer = Convert.ToString(configuration.GetSection("JwtIssuer").Value);
        }
        [HttpGet("GetUserTypeRoles/{TenantID}")]
        public ActionResult<Response<List<UserTyepBO>>> GetUserTypeRoles(int TenantID)
        {
            try
            {
                var result = _securityService.GetUserTypeRoles(TenantID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpGet("GetRoleToMenuServices/{RoleID}")]
        public ActionResult<Response<List<EntServicesBO>>> GetRoleToMenuServices(int RoleID)
        {
            try
            {
                var result = _securityService.GetRoleToMenuServices(RoleID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpGet("GetUserTypeServices/{UserTypeID}")]
        public ActionResult<Response<List<EntServicesBO>>> GetUserTypeToServices(int UserTypeID)
        {
            try
            {
                var result = _securityService.GetUserTypeToServices(UserTypeID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("AddRoleServices")]
        public ActionResult<Response<string>> AddRoleServices(UserRoleBO input)
        {
            try
            {
                var result = _securityService.AddRoleServices(input);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpGet("ExceptUserTypeServices/{UserTypeID}/{RoleID}")]
        public ActionResult<Response<List<EntServicesBO>>> GetExceptUserTypeServices(int UserTypeID, int RoleID)
        {
            try
            {
                var result = _securityService.GetExceptUserTypeServices(UserTypeID, RoleID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpGet("GetUserTypeList/{ProductID}/{TenantID}/{AppUserID}")]
        [HttpGet("GetUserTypeList/{ProductID}/{TenantID}")]
        public ActionResult<Response<List<UserTyepBO>>> GetUserTypeList(int ProductID,int TenantID, int AppUserID)
        {
            try
            {
                var result = _securityService.GetUserTypeList(ProductID ,TenantID, AppUserID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("GetUserTypeServiceSearch")]
        public ActionResult<UserTypeServiceListSearch> GetUserTypeServiceSearch(UserTypeServiceListSearch input)
        {
            try
            {
                var result = _securityService.GetUserTypeServiceSearch(input);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("GetEntUserGroupList")]
        public ActionResult<Response<vwUserGroupListSearch>> GetEntUserGroupList(vwUserGroupListSearch input)
        {
            try
            {
                var result = _securityService.GetEntUserGroupList(input);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("CreateUserGroup")]
        public ActionResult<Response<string>> CreateUserGroup(UserGroupInput input)
        {
            try
            {
                var result = _securityService.CreateUserGroup(input);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("GetUserGroupServices")]
        public ActionResult<Response<List<EntServicesBO>>> GetUserGroupServices(EntUserGroupMembersBO input)
        {
            try
            {
                var result = _securityService.GetUserGroupServices(input);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("GetEntUserGroupMembers")]
        public ActionResult<Response<List<VwEntAppUserBO>>> GetEntUserGroupMembers(EntUserGroupMembersBO input)
        {
            try
            {
                var result = _securityService.GetEntUserGroupMembers(input);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("AddUserGroupMember")]
        public ActionResult<Response<string>> AddUserGroupMember(EntUserGroupMembers input)
        {
            try
            {
                var result = _securityService.AddUserGroupMember(input);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpGet("DeleteUserGroupMember/{UserGroupID}/{AppUserID}")]
        public ActionResult<Response<string>> DeleteUserGroupMember(int UserGroupID, int AppUserID)
        {
            try
            {
                var result = _securityService.DeleteUserGroupMember(UserGroupID, AppUserID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("CreateUser")]
        public ActionResult<Response<List<AppUserDetailsBO>>> CreateUser(List<ActivateUserBO> input)
        {
            try
            {
                var result = _securityService.CreateUser(input);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<List<AppUserDetailsBO>>(ex.Message, 500));
            }
        }
        [HttpPost("GetAppUserList")]
        public ActionResult<Response<VwAppUserListSearch>> GetEntAppUserList(VwAppUserListSearch input)
        {
            try
            {
                var result = _securityService.GetEntAppUserList(input);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("GetAssociateRoleServices")]
        public ActionResult<Response<List<EntServicesBO>>> GetAssocUserTypeServices(UserTypeInput input)
        {
            try
            {
                var result = _securityService.GetAssocUserTypeServices(input);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpGet("GetAssocUserGroupServices/{UserGroupID}")]
        public ActionResult<Response<List<EntServicesBO>>> GetAssocUserGroupServices(int UserGroupID)
        {
            try
            {
                var result = _securityService.GetAssocUserGroupServices(UserGroupID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("UpdateUserPassword")]
        public ActionResult<Response<string>> UpdateUserPassword(ActivateUserBO input)
        {
            try
            {
                var result = _securityService.UpdateUserPassword(input);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("SwitchUserStatus")]
        public ActionResult<Response<int>> SwitchUserStatus(ActivateUserBO input)
        {
            try
            {
                var result = _securityService.SwitchUserStatus(input);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("RemoveRoleService")]
        public ActionResult<Response<string>> RemoveRoleService(UserTypeInput input)
        {
            try
            {
                var result = _securityService.RemoveRoleService(input);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpGet("GetAppUserServiceList/{UserID}")]
        public ActionResult<Response<List<VwTblEntServices>>> GetAppUserServiceList(int UserID)
        {
            try
            {
                var result = _securityService.GetAppUserServiceList(UserID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpGet("GetUserServiceException/{UserID}")]
        public ActionResult<Response<List<UserServiceExceptionBO>>> GetUserServiceException(int UserID)
        {
            try
            {
                var result = _securityService.GetUserServiceException(UserID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("AddUserServiceException")]
        public ActionResult<Response<string>> AddUserServiceException(ExceptionBO input)
        {
            try
            {
                var result = _securityService.AddUserServiceException(input);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpGet("DeleteUserServiceException/{ExceptionID}")]
        public ActionResult<Response<string>> DeleteUserServiceException(int ExceptionID)
        {
            try
            {
                var result = _securityService.DeleteUserServiceException(ExceptionID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpGet("DeleteUserRole/{TenantID}/{RoleID}")]
        public ActionResult<Response<string>> DeleteUserRole(int RoleID, int TenantID)
        {
            try
            {
                var result = _securityService.DeleteUserRole(RoleID, TenantID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("SwitchUserGroupStatus")]
        public ActionResult<Response<string>> ChangeUserGroupStatus(UserGroupInput input)
        {
            try
            {
                var result = _securityService.ChangeUserGroupStatus(input);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpGet("GetAppUserInfo/{AppUserID}")]
        public ActionResult<Response<List<VwEntAppUserList>>> GetAppUserInfo(int AppUserID)
        {
            try
            {
                var result = _securityService.GetAppUserInfo(AppUserID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpGet("GetEntSubscribedProdMap/{TenantID}")]
        public ActionResult<Response<List<EntSubscribedProdMap>>> GetEntSubscribedProdMap(int TenantID, int ProductID)
        {
            try
            {
                var result = _securityService.GetEntSubscribedProdMap(TenantID, ProductID);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("DeleteUserGroup")]
        public ActionResult<Response<string>> DeleteUserGroup(UserGroupInput input)
        {
            try
            {
                var result = _securityService.DeleteUserGroup(input);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpGet("GetSubscribedUserList/{AssocTenantID}/{AppUserID}")]
        [HttpGet("GetSubscribedUserList/{AssocTenantID}/{AppUserID}/{ProductID}")]
        public ActionResult<Response<List<EntSubscribedProdMap>>> GetEntSubscribedList(int AssocTenantID , int AppUserID , int ProductID)
        {
            try
            {
                return Ok(_securityService.GetEntSubscribedList(AssocTenantID , AppUserID , ProductID));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server Error{ex.Message}");
                return StatusCode(500, $"Internal server Error{ex.Message}");
            }
        }
    }
}
