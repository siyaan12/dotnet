using System;
using System.Collections.Generic;
using MyDodos.Domain.AuthBO;
using MyDodos.Domain.HR;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.HR;
using MyDodos.ViewModel.Business;
using MyDodos.ViewModel.Document;
using MyDodos.ViewModel.Common;

namespace MyDodos.Repository.HR
{
    public interface IOnBoardRepository
    {
        int UpdateOnBoardSetting(BPProcessBO bpBo);
        Response<List<BPTransaction>> GetOnBoardSetting(int TenantID, int LocationID, int TransOrder, string ProcessCategory);
        Response<OnBoardRequestModelMsg> SaveBusinessOnboard(BPTransInstance bprocess);
        Response<List<IDProofDocumnent>> GetMasDocProof(int CountryId);
        int SaveOnboardEducation(HREmpEducation onEdu);
        List<HREmpEducation> GetOnboardEducation(int EmpID);
        Response<OnBoardRequestModelMsg> DeleteOnboardEducation(int EmpEduID);
        OnBoardRequestModelMsg AddOnBoardingForm(EmpOnboardingModBO onboarding);
        int InsertOnBoardPersonal(OnboardPersonalDetail onboarding);
        List<EmployeeAddress> SaveEmployeeAddress(List<EmployeeAddress> EmpAdd);
        List<OnboardPersonalDetail> GetEmployee(int EmpID,int TenantID,int LocationID);
        Response<OnboardSearchBO> GetOnboardList(OnboardSearchBO search);
        List<OnBoardingResourceBO> GetHROnboardResource(int TenantID, int LocationID);
        OnBoardRequestModelMsg SaveHROnboardGenral(OnBoardingGenralBO genral);
        int SaveEmployeeAppuser(InputAppUserBO EmpAdd);
        OnBoardingRequestBO GetOnboardIndividual(int EmpOnboardingID, int LocationID);
        List<EmployeeAddress> GetAddress(int EmpID);
        OnBoardingRequest GetOnboardTrack(int EmpOnboardingID, int LocationID);
        List<BPTransInstance> GetTracking(int TenantID, int LocationID, int EmpOnboardingID);
        //int SaveHRCraeteCheckList(ChecklistBO chkList);
        Response<OnBoardRequestModelMsg> DeleteHROnboard(int EmpOnboardingID);
        Response<List<GetReportManagerBO>> GetOnboardManager(int LocationID, int DeptID, int RoleID, string CategoryName);
        Response<List<HRRoleBO>> GetOnboardHR(int TenantID, int ProductID);
        int SaveHRIDCard(DocIDCardInputBo _objdoc);
        List<DocIDCardInputBo> GetIDCardInfo(int EmpID, int LocationID,int EmpIDCardID);
        List<DocRepositoryBO> GetDocRepository(int tenantId, int productId, DocRepoNameBO typeName);
        int SaveHRCraeteCheckList(ChecklistBO chkList);
        List<BPchecklist> GetHRCheckList(BPcheckInputBO chkList);
        /*other Steps*/
        int SaveWorkplace(WorkPlaceBO _work);
        int SaveNetworkSetUp(NetworkSetupBO _network);
        int SaveHRCheckList(BPchecklist chkList);
        OnBoardRequestModelMsg SaveHRAttendnceConfig(AttendanceConfigBO attend);
        List<AttendanceConfigBO> GetHRAttendnceConfig(int EmpID, int LocationID,int AttendConfigID);
        OnBoardRequestModelMsg SaveOnboardExperience(HREmpExperience onExp);
        List<HREmpExperience> GetOnboardEmpExperience(int EmpID);
        Response<OnBoardRequestModelMsg> DeleteEmpExperience(int EmpExpID);
        OnBoardRequestModelMsg SaveBPTransInstance(int TenantID, int LocationID,int Onboardingid);
        SaveOut SaveOnBoardSettingDragDrop(BPTransaction process);
        int HRDirEmpPersonal(OnboardPersonalDetail onboarding);
        //GetToHRMS(OnBoardingGenralBO genral, List<AppUserDetailsBO> AppUser);
    }
}