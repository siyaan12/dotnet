using System;
using System.Collections.Generic;
using MyDodos.Domain.AuthBO;
namespace MyDodos.Domain.HR
{
    public class BPCheckListDetail 
    {
        public int ChkListInstanceID { get; set; }
        public int ChkListID { get; set; }
        public int TenantID { get; set; }
        public string PlatformNative { get; set; }
        public string RequestStatus { get; set; }
        public string RefEntity { get; set; }
        public int RefEntityID { get; set; }
        public string RequestDescription { get; set; }
        public DateTime? InitiatedOn { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string FirstName { get; set; }
        public int EmpID { get; set; } 
        public string EmpNumber { get; set; }
        public string Gender { get; set; }
        public DateTime? ExitDate { get; set; }
        public bool IsNotice { get; set; }
        public DateTime? NoticeDate { get; set; }
        public int NoticePeriod { get; set; } 
        public string NoticePeriodType { get; set; }
        public string ExitReason { get; set; }
        public string ResignFile { get; set; }
        public string ReportName { get; set; }
        public int ChkListDetID { get; set; }
        public string ChkListGroup { get; set; }
        public string LineItemName { get; set; }
        public int ListOrder { get; set; }
        public int CheckItemUpdatedBy { get; set; }
        public DateTime? CheckItemUpdatedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsNotify { get; set; }
        public int LocationID { get; set; }
        public int TotalCount { get; set; }
    }
    public class BPCheckListDetails 
   {
        public DateTime? ActionDate { get; set; }
        public string Comments { get; set; }
        public int EmpID { get; set; } 
        public string EmpNumber { get; set; }
        public string FullName { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? NoticeDate { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public int ChkListInstanceID { get; set; }	
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public List<BPTransInstance> OffboardTrack {get; set;}
    }
    public class GenCheckListDetailInstance 
    {
        public int ChkListInstanceID { get; set; }
        public int TenantID { get; set; }
        public string ProcessName { get; set; }
        public int RefEntityID { get; set; }
        public int CreatedBy { get; set; }
        public string RequestDescription { get; set; }
        public bool IsNotice { get; set; }
        public bool IsProcessStart { get; set; }
        public DateTime? NoticeDate { get; set; }
        public int NoticePeriod { get; set; }
        public string NoticePeriodType { get; set; }
        public DateTime? InitiatedOn { get; set; }
        public int BProcessID { get; set; }
        public int LocationID { get; set; }
        public int DocID { get; set; }
    }
}