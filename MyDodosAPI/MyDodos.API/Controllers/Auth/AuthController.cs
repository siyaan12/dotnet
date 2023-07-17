using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MyDodos.Domain.AuthBO;
using MyDodos.Domain.Wrapper;
using MyDodos.Service.Auth;
using MyDodos.Service.Logger;
using System;
using System.Collections.Generic;

namespace MyDodos.API.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILoggerManager _logger;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("userlogin")]
        public ActionResult<Response<AuthLoginBO>> userlogin(InputLogin inputLogin)
        {
            try
            {
                var result = _authService.GetLogin(inputLogin);
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

                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<AuthLoginBO>(ex.Message, 500));
            }

        }
        [HttpPost("Muserlogin")]
        public ActionResult<Response<AuthLoginBO>> Muserlogin(InputLogin inputLogin)
        {
            try
            {
                var result = _authService.GetLogin(inputLogin);
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

                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<AuthLoginBO>(ex.Message, 500));
            }

        }
        [Authorize]
        [HttpGet("userprofile/{appuserid}")]
        public ActionResult<Response<UserProfileBO>> userprofile(int appuserid)
        {
            try
            {
                var result = _authService.GetProfile(appuserid);
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

                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<UserProfileBO>(ex.Message, 500));
            }

        }
        [Authorize]
        [HttpGet("Muserprofile/{appuserid}")]
        public ActionResult<Response<MUserProfileBO>> Muserprofile(int appuserid)
        {
            try
            {
                var result = _authService.MGetProfile(appuserid);
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

                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<MUserProfileBO>(ex.Message, 500));
            }

        }
        [HttpPost("refreshToken")]
        public ActionResult<Response<AuthLoginBO>> GetRefreshToken(InputRefresh inputLogin)
        {
            try
            {
                var result = _authService.GetRefreshToken(inputLogin);
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

                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<AuthLoginBO>(ex.Message, 500));
            }

        }
        [HttpPost("MrefreshToken")]
        public ActionResult<Response<AuthLoginBO>> MGetRefreshToken(InputRefresh inputLogin)
        {
            try
            {
                var result = _authService.GetRefreshToken(inputLogin);
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

                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<AuthLoginBO>(ex.Message, 500));
            }

        }
        [Authorize]
        [HttpGet("logout/{userid}")]
        public ActionResult<Response<LogOutBO>> UserLogOut(int userid)
        {
            string accessTokenWithBearerPrefix = Request.Headers[HeaderNames.Authorization];
            string accessTokenWithoutBearerPrefix = accessTokenWithBearerPrefix.Substring("Bearer ".Length);
            var inputLogin = new InputLogOut();
            inputLogin.AccessToken = accessTokenWithoutBearerPrefix;
            inputLogin.AppUserID=userid;
            try
            {
                var result = _authService.UserLogOut(inputLogin);
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

                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<LogOutBO>(ex.Message, 500));
            }

        }
        [Authorize]
        [HttpGet("Mlogout/{userid}")]
        public ActionResult<Response<LogOutBO>> MUserLogOut(int userid)
        {
            string accessTokenWithBearerPrefix = Request.Headers[HeaderNames.Authorization];
            string accessTokenWithoutBearerPrefix = accessTokenWithBearerPrefix.Substring("Bearer ".Length);
            var inputLogin = new InputLogOut();
            inputLogin.AccessToken = accessTokenWithoutBearerPrefix;
            inputLogin.AppUserID=userid;
            try
            {
                var result = _authService.UserLogOut(inputLogin);
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

                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<LogOutBO>(ex.Message, 500));
            }

        }
        [Authorize]
        [HttpGet("authcheck/{appuserid}")]
        public ActionResult<Response<string>> GetAuthCheck(int appuserid)
        {
            try
            {
                return Ok(new Response<string>("success", 200));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<string>(ex.Message, 500));
            }

        }
        [Authorize]
        [HttpGet("Mauthcheck/{appuserid}")]
        public ActionResult<Response<string>> MGetAuthCheck(int appuserid)
        {
            try
            {
                return Ok(new Response<string>("success", 200));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<string>(ex.Message, 500));
            }

        }
        [HttpPost("terminallogin")]
        public ActionResult<Response<int>> CheckAppUserID(TAppUser AppUser)
        {
            try
            {
                var result = _authService.CheckAppUserID(AppUser);
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

                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpGet("GetSwitchUsers/{AppUserID}")]
        public ActionResult<Response<List<AssociateUserBO>>> GetSwitchUsers(int AppUserID)
        {
            try
            {
                var result = _authService.GetSwitchUsers(AppUserID);
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
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<string>(ex.Message, 500));
            }
        }
        [HttpPost("SentOtpMail")]
        public ActionResult<Response<OTPOutputSentBO>> SentOtpMail(OTPSentBO _appuser)
        {
            try
            {
                var result = _authService.SentOtpMail(_appuser);
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
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<OTPOutputSentBO>(ex.Message, 500));
            }
        }
        [HttpPost("CheckOTP")]
        public ActionResult<Response<OTPOutputSentBO>> CheckOTP(PasswordBO _appuser)
        {
            try
            {
                var result = _authService.CheckOTP(_appuser);
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
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<OTPOutputSentBO>(ex.Message, 500));
            }
        }        
        // [HttpPost("ResetPassword")]
        // public ActionResult<Response<OTPOutputSentBO>> ResetPassword(PasswordBO _appuser)
        // {
        //     try
        //     {
        //         var result = _authService.ResetPassword(_appuser);
        //         if (result.StatusCode == 200)
        //         {
        //             return Ok(result);
        //         }
        //         else
        //         {
        //             return StatusCode(result.StatusCode, result);
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError($"Internal Server Error{ex.Message}");
        //         return StatusCode(500, new Response<OTPOutputSentBO>(ex.Message, 500));
        //     }
        // }
        [HttpPost("ForgetPassword")]
        public ActionResult<Response<CognitoReturn>> ForgetPassword(ResetPassword _appuser)
        {
            try
            {
                var result = _authService.ForgetPassword(_appuser);
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
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<CognitoReturn>(ex.Message, 500));
            }
        }
        [HttpPost("ResetPassword")]
        public ActionResult<Response<CognitoReturn>> ResetPassword(ResetPassword _appuser)
        {
            try
            {
                var result = _authService.NewResetPassword(_appuser);
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
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<CognitoReturn>(ex.Message, 500));
            }
        }
        [HttpPost("saveDemoRequest")]
        public ActionResult<Response<int>> SaveDemoRequest(MDemoRequest request)
        {
            try
            {
                var result = _authService.SaveDemoRequest(request);
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

                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("saveDetailRequest")]
        public ActionResult<Response<int>> SaveDetailRequest(MDetailRequest request)
        {
            try
            {
                var result = _authService.SaveDetailRequest(request);
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

                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<int>(ex.Message, 500));
            }
        }
        [HttpPost("MForgetPassword")]
        public ActionResult<Response<CognitoReturn>> MForgetPassword(ResetPassword _appuser)
        {
            try
            {
                var result = _authService.MForgetPassword(_appuser);
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
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<CognitoReturn>(ex.Message, 500));
            }
        }
        [HttpPost("MResetPassword")]
        public ActionResult<Response<CognitoReturn>> MResetPassword(ResetPassword _appuser)
        {
            try
            {
                var result = _authService.MNewResetPassword(_appuser);
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
                _logger.LogError($"Internal Server Error{ex.Message}");
                return StatusCode(500, new Response<CognitoReturn>(ex.Message, 500));
            }
        }
    }
}
