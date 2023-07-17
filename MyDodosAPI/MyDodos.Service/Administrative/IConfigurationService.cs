using System.Collections.Generic;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.Administrative;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.Common;

namespace MyDodos.Service.Administrative
{
    public interface IConfigurationService
    {
        Response<int> SaveConfigSettings(List<ConfigSettingsBO> objconfig);
        Response<List<ConfigSettingsBO>> GetConfigSettings(ConfigSettingsBO objconfig);
        Response<int> DeleteConfigSettings(int ConfigID,int TenantID,int LocationID);
        Response<int> SaveGeneralConfigSettings(GeneralConfigSettingsBO objconfig);
        Response<List<GeneralConfigSettingsBO>> GetGeneralConfigSettings(GeneralConfigSettingsBO objconfig);
        Response<List<ProofDetailsBO>> GetProofDetails(int CountryID);
        Response<SaveOut> SaveTimesheetPaymentModeSettings(TSPaymentModeSettingsBO mode);
        Response<TSPaymentModeSettingsBO> GetTimesheetPaymentModeSettings(TSPaymentModeSettingsBO mode);
    }
}