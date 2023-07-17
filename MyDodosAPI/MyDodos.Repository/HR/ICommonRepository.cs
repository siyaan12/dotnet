using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.HR;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.LeaveManagement;
using MyDodos.ViewModel.Common;
using MyDodos.Domain.Administrative;

namespace MyDodos.Repository.HR
{
    public interface ICommonRepository
    {
        List<CountryBO> GetCountryList(int ProductID, int CountryID);
        Response<List<StatesBO>> GetStateList(int ProductID, int CountryID);
        SaveOut SaveMultiSelectCountry(ConfigSettingsBO country);
        List<ConfigSettingsBO> GetCountryByTenant (int TenantID,int LocationID);
        List<TimeZoneBO> GetTimeZoneDetails();
        List<CurrencyBO> GetCurrencyDetails();
        List<WeekDayBO> GetMasterWeekDays(int TenantID, int LocationID);
        SaveOut SaveStgDownloadDoc(StgDownloadDocBO stgdown);
        List<StgDownloadDocBO> GetStgDownloadDoc(int TenantID, int DownloadDocID,int EntityID,string Entity);
        string GetExecutiveScript(string Entity);
        string ExecutiveScript(ExecutiveScript script);
    }
}