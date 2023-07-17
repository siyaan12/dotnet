using System.Collections.Generic;
using MyDodos.Domain.HR;
using MyDodos.Domain.AuthBO;
using MyDodos.Domain.AzureStorage;
using MyDodos.Domain.Document;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.HR;
using MyDodos.ViewModel.Business;

namespace MyDodos.Service.HR
{
    public interface IOffBoardService
    {
        Response<OffBoardSearchBO> SearchOffboardingList(OffBoardSearchBO objresult);
        Response<List<BPCheckListDetail>> SearchOffboardList(string FirstName,int TenantID);
        Response<BPCheckListDetails> ViewOffboardListTrack(int ChkListInstanceID);
        Response<List<RecentExitOffBoardBO>> RecentExitOffBoarding(int TenantID);
        Response<OnBoardRequestModelMsg> CompleteOffBoarding(CompleteOffBoardingBO objresult);
        Response<List<BPCheckListDetail>> GetOffboardRequest(int TenantID,int ChkListInstanceID, string RequestStatus);
        Response<List<BPTransInstance>> GetOffBoardTrack(int BProcessID, int ReqInitID);
        Response<OnBoardRequestModelMsg> DeleteoffboardingRequest(int ChkListInstanceID);
        Response<ReqDetails> GetReqDetails(int ChkListInstanceID);
        Response<GenCheckListInstance> GetResignLetter(int ChkListInstanceID,int ProductID);
        Response<List<BPCheckListDetail>> GetOffBoardingRequest(int TenantID);
        Response<OnBoardRequestModelMsg> CreateCheckListInstance(GenCheckListInstance inputJson,DocDetailBO objfile);
        Response<List<BPCheckListDetail>> GetOffBoardReqChkLists(int ChkListInstanceID,int TenantID);
        Response<List<BPchecklistDetBO>> GetOffBoardReqCheckList(int ChkListInstanceID);
        Response<OnBoardRequestModelMsg> UpdateCheckListItem(List<UpdateCheckList> list);
        Response<EmployeeInfoBO> GetEmpOffBoardInfo(int TenantID, int LocationID, int EmpID);
        Response<OnBoardRequestModelMsg> AddGenCheckListDetail(int EmpID,int LocationID,int TenantID);
        Response<OffboardRequestSearchBO> GetOffboardRequestSearch(OffboardRequestSearchBO objresult);
    }
}