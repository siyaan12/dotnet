using System.Collections.Generic;
using MyDodos.Domain.HR;
using MyDodos.Domain.AuthBO;
using MyDodos.Domain.AzureStorage;
using MyDodos.Domain.Document;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.HR;
using MyDodos.ViewModel.Business;
using MyDodos.ViewModel.Document;
using KoSoft.DocRepo;
using MyDodos.ViewModel.Entitlement;
using MyDodos.Domain.Payroll;
using MyDodos.Domain.BenefitManagement;
using MyDodos.ViewModel.Common;
using MyDodos.ViewModel.Employee;
using System;

namespace MyDodos.Service.HR
{
    public interface IOnBoardService
    {
        Response<int> UpdateOnBoardSetting(BPProcessBO bpBo);
        Response<List<RoleBO>> GetEntRoles(int ProductID, string GroupType);
        Response<List<EntRolesBO>> GetEntTenantRoles(InpurtEntRolesBO roles);
        Response<List<RtnUserGroupBO>> GetAccountTypes(int ProductID, int TenantID);
        public Response<int> SaveOnboardEducation(HRInputEmpEducation Edu);
        Response<OnBoardRequestModelMsg> AddOnBoardingForm(EmpOnboardingModBO onboarding);
        Response<List<OnBoardingResourceBO>> GetHROnboardResource(int productId, int TenantID, int LocationID);
        Response<Tuple<OnBoardingGenralBO, AppUserDetailsBO, OnBoardRequestModelMsg>> SaveHROnboardGenral(OnBoardingGenralBO genral);
        Response<OnBoardingRequestBO> GetOnboard(int EmpOnboardingID);
        Response<OnBoardingRequest> GetOnboardTrack(int EmpOnboardingID, int LocationID);
        Response<int> SaveOnBoardEmployee(OnboardPersonalDetail onboarding);
        Response<int> SaveAzureDocuments(DocProofFileBo _objdoc, DocProofInputBo _obj);
        Response<int> SaveIDcardDocuments(InputDocIDCardInputBo _objdoc);
        Response<List<GenDocument>> GetDocInfo(int TenantID, int LocationID, int EmpID);
        Response<AzureDocURLBO> DownloadDocs(int docId, int productId);
        Response<AzureDocURLBO> DeleteDocs(int docId, int productId);
        Response<List<DocIDCardInputBo>> GetIDCardInfo(int EmpID, int LocationID, int tenantID, int EmpIDCardID);
        Response<int> SaveHRCraeteCheckList(ChecklistBO chkList);
        Response<List<BPchecklist>> GetHRCheckList(BPcheckInputBO chkList);
        Response<int> SaveWorkplace(WorkPlaceBO _work);
        Response<int> SaveNetworkSetUp(NetworkSetupBO _network);
        Response<NetworkSetupBO> GetNetworkDetails(NetworkSetupBO chkList);
        Response<WorkPlaceBO> GetWorkPlace(WorkPlaceBO chkList);
        Response<int> SaveHRCheckList(OrientationBO chkList);
        Response<OrientationBO> GetOrientation(OrientationBO _work);
        Response<int> SaveHRAttendnceConfig(AttendanceConfigBO attend);
        Response<List<AttendanceConfigBO>> GetHRAttendnceConfig(int EmpID, int LocationID, int AttendConfigID);
        Response<OnBoardRequestModelMsg> SaveOnboardExperience(HRInputEmpExperience onInpExp);
        Response<List<HREmpExperience>> GetOnboardEmpExperience(int EmpID);
        Response<List<HREmpEducation>> GetOnboardEducation(int EmpID);
        Response<List<BenefitGroupBO>> GetBenefitGroupByGroupType(int TenantID,int GroupTypeID);
        Response<OnboardingBenefitsBO> GetBenefits(int BenefitGroupID,int TenantID,int LocationID);
        Response<int> SaveOnBoardBenefits(OnBoardBenefitGroupBO objgroup);
        Response<List<EmployeeBenefitsBO>> GetEmployeeBenefits(int EmpID,int TenantID,int LocationID);
        Response<OnboardingEmpBenefitsBO> GetEmployeePayrollCycle(SalaryonboardPaycycle objPaycycle);
        Response<int> SaveEmployeeCTC(PayrollCTCBO objctc);
        Response<int> SaveEmployeePayRollBenfits(PayrollCTCBO objctc);
        Response<SaveOut> SaveEmpPayrollCTC(EmpSalaryStructureCTC objctc);
        Response<int> SaveAccountDetails(AccountDetailsBO objaccount);
        Response<AccountDetailsBO> GetAccountDetails(int EmpID);
        Response<SaveOut> SaveOnBoardSettingDragDrop(List<BPTransaction> process);
        Response<SaveOut> SaveEmpDynamicPayrollCTC(EmpSalaryStructureCTC objctc);
        Response<int> UpdateHRDirEmployee(OnboardPersonalDetail onboarding);
    }
}