using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using MyDodos.Domain.HR;
using MyDodos.ViewModel.Business;
using MyDodos.ViewModel.ServerSearch;

namespace MyDodos.ViewModel.HR
{
    public class OffBoardSearchBO
    {
        public OffBoardInputBO objOffBoardInput { get; set; }
        public ServerSearchable ServerSearchables { get; set; }
        public List<BPCheckListDetail> objOffBoardList { get; set; }
    }
    public class OffBoardInputBO
    {
        public string CommonSearch { get; set; }
        public string RequestStatus { get; set; }
        public int TenantID { get; set; }
    }
    public class RecentExitOffBoardBO 
    {
        
        public string ProfileImage { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }
        public string EmpNumber { get; set; }
        public string Gender { get; set; }
        public DateTime? ExitDate { get; set; }
        public bool? IsNotice { get; set; }
        public DateTime? NoticeDate { get; set; }
        public int NoticePeriod { get; set; }
        public string NoticePeriodType { get; set; }
        public string ExitReason { get; set; }
        public string base64Images { get; set; }
        public int TenantID { get; set; }
    }
    public class CompleteOffBoardingBO
    {
        public int ChkListInstanceID { get; set; }
        public int EmpID { get; set; }
        public DateTime ExitDate { get; set; }
        public string ExitReason { get; set; }
        public string Comments { get; set; }
        public int ActionBy { get; set; }
        public int TenantID { get; set; }
    }
    public class ReqDetails 
    {
        public int ChkListInstanceID { get; set; }
        public string RequestDescription { get; set; }
        public string EmpNumber { get; set; }
        public string ResignFile { get; set; }
        public DateTime? InitiatedOn { get; set; }
        public string RequestStatus { get; set; }
        public int RefEntityID { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string NoticePeriodType { get; set; }
        public int NoticePeriod { get; set; }
        public bool IsNotice { get; set; }
        public bool? IsStartProcess { get; set; }
        public DateTime? NoticeDate { get; set; }
        public string MangerName { get; set; }
        public string EmployedSeen { get; set; }
        public string NumberofServices { get; set; }
        public string Comments { get; set; }
    }
    public class  ReturnDocDetailBO
    {
        public int ChkListInstanceID { get; set; }
        public int EmpID { get; set; }
        public int DocID { get; set; }        
        public string File { get; set; }
        public int TenantID { get; set; }
        public int ProductID { get; set; }
        public string base64images { get; set; }
    }
    public class GenCheckListInstance 
    {
        public int ChkListInstanceID { get; set; }
        public int ChkListID { get; set; }
        public string ChkProcessName { get; set; }
        public int TenantID { get; set; }
        public bool PlatformNative { get; set; }
        public string RequestStatus { get; set; }
        public string RefEntity { get; set; }
        public int RefEntityID { get; set; }
        public int EmpID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string RequestDescription { get; set; }
        public DateTime? InitiatedOn { get; set; }
        public bool IsNotice { get; set; }
        public bool IsStartProcess { get; set; }
        public string ResignFile { get; set; }
        public int LocationID { get; set; }
        public DateTime? NoticeDate { get; set; }
        public string NoticePeriodType { get; set; }
        public int NoticePeriod { get; set; }
        public int ProductID { get; set; }
        public int DocID { get; set; }
        public int DocTypeID { get; set; }
        public string base64Images { get; set; }
        public string Gender { get; set; }
    }
    public class  DocDetailBO
    {
        // public int ChkListInstanceID { get; set; }
        // public int EmpID { get; set; }
        // public int TenantID { get; set; }
        // public int CreatedBy { get; set; }        
        public string InputJson { get; set; }
        public IFormFile File { get; set; }
    }
    public class BPchecklistDetBO
    {
        public int ChkListInstanceID { get; set; }
        public string ChkListGroup { get; set; }
        public List<BPCheckList> BPCheckList { get; set; }
    }
    public class UpdateCheckList
    {
        public int ChkListInsID { get; set; }
        public int UpdatedBy { get; set; }
        public string ItemComments { get; set; }
        public bool IsCompleted { get; set; }
        public int TenantID { get; set; }
    }
    public class EmployeeInfoBO
    {
        public int EmpID { get; set; }
        public string EmpNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string ReportName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string NumberofServices { get; set; }
        public string EmployedSince { get; set; }
        public int FuncRoleID { get; set; }
        public int ManagerID { get; set; }
        public int DepartmentID { get; set; }
        public int BenefitGroupID { get; set; }
        public string Gender { get; set; }
        public DateTime? DOJ { get; set; }
        public DateTime? DOB { get; set; }
        public string OfficeEmail { get; set; }
        public string PersonalMail { get; set; }
        public string MobileNo { get; set; }
        public string EmpType { get; set; }
        public string BloodGroup { get; set; }
        public string Nationality { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public bool IsCompensate { get; set; }
        public bool IsTimeSheet { get; set; }
        public bool IsAttendance { get; set; }
        public int HRInchargeID { get; set; }
        public string EmpStatus { get; set; }
        public int EmpLocID { get; set; }
        public int ModifedBy { get; set; }
        public string ProfileImage { get; set; }
        public int AppUserID { get; set; }
        public string AppUserName { get; set; }
        public bool IsCompleted { get; set; }
        public string base64images { get; set;}
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string LocationAddress1 { get; set; }
        public string LocationAddress2 { get; set; }
        public string LocationCity { get; set; }
        public string LocationState { get; set; }
        public string LocationZip { get; set; }
        public string LocationCountry { get; set; }
        public string LocationStatus { get; set; }
        public string LocationGmt { get; set; }
        public string LocationCurrency { get; set; }
        public string LocationCurrencySymbol { get; set; }
    }
    public class BPCheckList 
    {
        public int ChkListInstanceID { get; set; }
        public int ChkListID { get; set; }
        public string ChkProcessName { get; set; }
        public int TenantID { get; set; }
        public bool PlatformNative { get; set; }
        public string RequestStatus { get; set; }
        public string RefEntity { get; set; }
        public int RefEntityID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string RequestDescription { get; set; }
        public DateTime? InitiatedOn { get; set; }
        public string FullName { get; set; }
        public int EmpID { get; set; }
        public string EmpNumber { get; set; }
        public string Designation { get; set; }
        public string Gender { get; set; }
        public DateTime? ExitDate { get; set; }
        public bool IsNotice { get; set; }
        public DateTime? NoticeDate { get; set; }
        public Int32 NoticePeriod { get; set; }
        public string NoticePeriodType { get; set; }
        public string ExitReason { get; set; }
        public int ChkListInsID { get; set; }
        public string ChkListGroup { get; set; }
        public string LineItemName { get; set; }
        public string ItemComments { get; set; }
        public int ListOrder { get; set; }
        public int CheckItemUpdatedBy { get; set; }
        public DateTime? CheckItemUpdatedOn { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsNotify { get; set; }
        public string Department { get; set; }
        public bool IsMandatory { get; set; }
        public int LocationID { get; set; }
    }
    public class OffboardRequestSearchBO
    {
        public OffboardRequestInputBO objOffBoardRequestInput { get; set; }
        public ServerSearchable ServerSearchables { get; set; }
        public List<BPCheckListDetail> objOffBoardRequestList { get; set; }
    }
    public class OffboardRequestInputBO
    {
        public int TenantID { get; set; }
        public string RequestStatus { get; set; }
    }
}