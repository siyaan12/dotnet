using System;

namespace MyDodos.Domain.Mail
{
    public class MailModel
    {
        public Int32 NotifyID { get; set; }
        public int SubscriberID { get; set; }
        public string NotifyFrom { get; set; }
        public string NotifyTo { get; set; }
        public string NotifyCC { get; set; }
        public string NotifyBCC { get; set; }
        public string NotifySubject { get; set; }
        public string NotifyBody { get; set; }
        public bool IsAttachment { get; set; }
        public string NotifyStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Entity { get; set; }
        public int EntityId { get; set; }
        public int TenantID { get; set; }
    }
    public class MailModeldetBO
    {
        public int NotifyID { get; set; }
        public string NotifyFrom { get; set; }
        public string NotifyTo { get; set; }
        public string NotifyCC { get; set; }
        public string NotifyBCC { get; set; }
        public string NotifyBody { get; set; }
        public string NotifySubject { get; set; }
        public string NotifyStatus { get; set; }
        public DateTime CreatedOn { get; set; }
    }
    public class MailNotifyBO
    {
        public int EmpID { get; set; }
        public int EntityID { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmpName { get; set; }
        public string HRName { get; set; }
        public string ReportingEmail { get; set; }
        public string ReportName { get; set; }
        public string HREmail { get; set; }
        public int ProductID { get; set; }
        public int TenantID { get; set; }
        public string Msg { get; set; }
    }
    public class MailInputModel
    {
        public int EntityID { get; set; }
        public int EmpID { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public int YearID { get; set; }
        public int ProductID { get; set; }
        public string EntitySubject { get; set; }
        public string EntityStatus { get; set; }
        public string EntityName { get; set; }
        public string EntityType { get; set; }
    }
}