using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDodos.Domain.Document;
using MyDodos.Domain.AuthBO;
using MyDodos.Domain.HR;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.HR;
using MyDodos.ViewModel.Business;

namespace MyDodos.Repository.HR
{
    public interface IOffBoardRepository
    {
        OffBoardSearchBO SearchOffboardingList(OffBoardSearchBO inputParam);
        List<BPCheckListDetail> SearchOffboardList(string FirstName,int TenantID);
        BPCheckListDetails ViewOffboardListTrack(int ChkListInstanceID);
        List<RecentExitOffBoardBO> RecentExitOffBoarding(int TenantID);
        OnBoardRequestModelMsg CompleteOffBoarding(CompleteOffBoardingBO complete);
        List<BPCheckListDetail> GetOffboardRequest(int TenantID,int ChkListInstanceID, string RequestStatus);
        List<BPTransInstance> GetOffBoardTrack(int BProcessID,int ReqInitID);
        OnBoardRequestModelMsg DeleteoffboardingRequest(int ChkListInstanceID);
        ReqDetails GetReqDetails(int ChkListInstanceID);
        GenCheckListInstance GetRequest(int ChkListInstanceID,int ProductID);
        public List<BPCheckListDetail> GetOffBoardingRequest(int TenantID);
        OnBoardRequestModelMsg CreateCheckListInstance(GenCheckListInstance instance);
        OnBoardRequestModelMsg UploadOffboardLetter(int ChkListInstanceID, int TenantID, string ResignFile);
        List<BPCheckListDetail> GetOffBoardReqChkLists(int ChkListInstanceID,int TenantID);
        List<BPchecklistDetBO> GetOffBoardReqCheckList(int ChkListInstanceID);
        OnBoardRequestModelMsg UpdateCheckListItem(UpdateCheckList list);
        EmployeeInfoBO GetEmpOffBoardInfo(int TenantID, int LocationID,int EmpID);
        List<BPCheckList> GetOffBoardReqChkList(int ChkListInstanceID, string ChkListGroup);
        OnBoardRequestModelMsg AddGenCheckListDetail(int RefEntityID,int LocationID,int TenantID);
        OffboardRequestSearchBO GetOffboardRequestSearch(OffboardRequestSearchBO inputParam);
        OnBoardRequestModelMsg SaveBPTransInstance(int TenantID, int LocationID,int EmpID);
        List<BPTransInstance> GetTracking(int TenantID, int LocationID, int EmpID);
    }
}