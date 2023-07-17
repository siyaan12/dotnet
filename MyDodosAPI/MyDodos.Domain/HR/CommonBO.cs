using System;
using System.Collections.Generic;
namespace MyDodos.Domain.HR
{
    public class CountryBO
    {
        public int CountryID { get; set; }
        public string Code { get; set; }
        public string CountryName { get; set; }
        public int ProductId { get; set; }
        public int EntityId { get; set; }
        public bool IsRequired { get; set; }
        public List<StatesBO> States { get; set; }
    }
    public class CountryListBO
    {
        public List<CountryBO> Country { get; set; }
        public List<StatesBO> States { get; set; }
    }
    public class StatesBO
    {
        public int StateID { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public int CountryID { get; set; }
        public bool StateStatus { get; set; }
    }
    public class TimeZoneBO
    {
        public int TimeZoneID { get; set; }
        public string TimeDifference { get; set; }
        public string TimeZoneName { get; set; }
        public string ShortTimeZoneName { get; set; }
        public string TZCode { get; set; }
        public decimal RelativeToGMT { get; set; }
    }
    public class CurrencyBO
    {
        public int CurrencyID { get; set; }
        public string CountryName { get; set; }
        public int CountryCode { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
        public decimal ExchangeRate { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public bool IsbaseCurrency { get; set; }
        public bool Iseditable { get; set; }
        public bool Issystem { get; set; }
        public string Status { get; set; }
    }
    public class StgDownloadDocBO
    {
        public int DownloadDocID { get; set; }
        public string DownloadDocName { get; set; }
        public int DocID { get; set; }
        public int RepositoryID { get; set; }
        public string DocType { get; set; }
        public string DocNumber { get; set; }
        public string GenDocName { get; set; }
        public bool IsDownload { get; set; }
        public int DownloadBy { get; set; }
        public DateTime DownloadOn { get; set; }
        public string DownloadDocStatus { get; set; }
        public string DownloadComments { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public string Entity { get; set; }
        public int EntityID { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string FullName { get; set; }
    }
    public class ExecutiveScript
    {
        public string queryText { get; set; }
    }
}