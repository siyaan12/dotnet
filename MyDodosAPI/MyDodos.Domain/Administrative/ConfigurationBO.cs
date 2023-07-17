using System;
using System.Collections.Generic;
namespace MyDodos.Domain.Administrative
{
    public class ConfigSettingsBO
    {
        public int ConfigID { get; set; }
        public string ServiceName { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public string GenerateMode { get; set; }
        public string PrefixValue { get; set; }
        public int StartNo { get; set; }
        public int IncrementNo { get; set; }
        public string IncrementNos { get; set; }
        public int FixedWidth { get; set; }
        public string IdentifierType { get; set; }
        public string IdentifierColumnName { get; set; }
        public string timelimit { get; set; }
        public string timePeriod { get; set; }
        public bool IsActive { get; set; }
    }
    public class GeneralConfigSettingsBO
    {
        public int GenConfigID { get; set; }
        public string DateFormat { get; set; }
        public string TimeZone { get; set; }
        public string Currency { get; set; }
        public string Symbol { get; set; }
        public string Countrytype { get; set; }
        public string CountryValues { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
    public class GeneralConfigSettings
    {
        public int GenConfigID { get; set; }
        public string DateFormat { get; set; }
        public string TimeZone { get; set; }
        public string Currency { get; set; }
        public string Symbol { get; set; }
        public string Countrytype { get; set; }
        public string CountryValues { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public DateTime GmtDate { get; set; }
    }
    public class ProofDetailsBO
    {
        public int DocTypeID { get; set; }
        public string DocType { get; set; }
    }
    public class WeekDayBO
    {
        public int DayID { get; set; }
        public string DayName { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public string ShortName { get; set; }
        public bool IsHoliday { get; set; }
    }
    public class TSPaymentModeSettingsBO
    {
        public int ConfigID { get; set; }
        public string ServiceName { get; set; }
        public string GenerateMode { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public bool IsActive { get; set; }
    }
}