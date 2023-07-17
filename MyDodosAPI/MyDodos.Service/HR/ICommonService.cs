using System.Collections.Generic;
using KoSoft.DocRepo;
using MyDodos.Domain.Administrative;
using MyDodos.Domain.AzureStorage;
using MyDodos.Domain.HR;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.Common;
using MyDodos.ViewModel.LeaveManagement;

namespace MyDodos.Service.HR
{
    public interface ICommonService
    {
        Response<CountryListBO> GetCountryList(int ProductID, int CountryID);
        Response<SaveOut> SaveMultiSelectCountry(ConfigSettingsBO country);
        Response<List<ConfigSettingsBO>> GetCountryByTenant(int TenantID, int LocationID);
        Response<List<TimeZoneBO>> GetTimeZoneDetails();
        Response<List<CurrencyBO>> GetCurrencyDetails();
        Response<List<GenDocument>> GetDocInfo(int TenantID, int LocationID, int EmpID);
        Response<AzureDocURLBO> DownloadDocs(int docId, int productId);
        Response<AzureDocURLBO> DeleteDocs(int docId, int productId);
        Response<AzureDocURLBO> Base64DownloadDocs(int docId, int productId);
        Response<List<WeekDayBO>> GetMasterWeekDays(int TenantID, int LocationID);
        Response<SaveOut> SaveStgDownloadDoc(StgDownloadDocBO stgdown);
        Response<List<StgDownloadDocBO>> GetStgDownloadDoc(int TenantID, int DownloadDocID,int EntityID,string Entity);
        Response<string> ExecutiveScript(ExecutiveScript script);
    }
}