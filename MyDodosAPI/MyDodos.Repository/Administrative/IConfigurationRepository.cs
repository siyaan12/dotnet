using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDodos.Domain.Administrative;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.Common;

namespace MyDodos.Repository.Administrative
{
    public interface IConfigurationRepository
    {
        SaveOut SaveConfigSettings(ConfigSettingsBO objconfig);
        List<ConfigSettingsBO> GetConfigSettings(ConfigSettingsBO objconfig);
        SaveOut DeleteConfigSettings(int ConfigID,int TenantID,int LocationID);
        SaveOut SaveGeneralConfigSettings(GeneralConfigSettingsBO objconfig);
        List<GeneralConfigSettingsBO> GetGeneralConfigSettings(GeneralConfigSettingsBO objconfig);
        List<ProofDetailsBO> GetProofDetails(int CountryID);
        SaveOut SaveTimesheetPaymentModeSettings(TSPaymentModeSettingsBO mode);
        TSPaymentModeSettingsBO GetTimesheetPaymentModeSettings(TSPaymentModeSettingsBO mode);
    }
}