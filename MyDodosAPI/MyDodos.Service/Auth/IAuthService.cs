using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyDodos.Domain.AuthBO;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.Common;

namespace MyDodos.Service.Auth
{
    public interface IAuthService
    {
        Response<AuthLoginBO> GetLogin(InputLogin inputLogin);
        Response<UserProfileBO> GetProfile(int appuserid);
        Response<AuthLoginBO> GetRefreshToken(InputRefresh inputLogin);
        Response<LogOutBO> UserLogOut(InputLogOut inputLogin);
        Response<MUserProfileBO> MGetProfile(int appuserid);
        Response<int> CheckAppUserID(TAppUser AppUser);
        Response<List<AssociateUserBO>> GetSwitchUsers(int AppUserID);
        Response<OTPOutputSentBO> SentOtpMail(OTPSentBO _appuser);
        Response<OTPOutputSentBO> CheckOTP(PasswordBO _appuser);
        //Response<OTPOutputSentBO> ResetPassword(PasswordBO _appuser);
        Response<CognitoReturn> ForgetPassword(ResetPassword _appuser);
        Response<CognitoReturn> NewResetPassword(ResetPassword _appuser);
        Response<SaveOut> SaveDemoRequest(MDemoRequest request);
        Response<SaveOut> SaveDetailRequest(MDetailRequest request);
        Response<CognitoReturn> MForgetPassword(ResetPassword _appuser);
        Response<CognitoReturn> MNewResetPassword(ResetPassword _appuser);
    }
}
