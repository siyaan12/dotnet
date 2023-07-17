using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Wrapper;
using MyDodos.Domain.Administrative;
using MyDodos.Repository.Administrative;
using MyDodos.Domain.LeaveBO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyDodos.ViewModel.Common;

namespace MyDodos.Service.Administrative
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationRepository _configurationRepository;        
        public ConfigurationService(IConfiguration configuration, IConfigurationRepository configurationRepository)
        {
            _configuration = configuration;
            _configurationRepository =  configurationRepository;
        }
        public Response<int> SaveConfigSettings(List<ConfigSettingsBO> objconfig)
        {
            Response<int> response;
            SaveOut result = new SaveOut();
            try
            {
                foreach (var item in objconfig)
                {
                result = _configurationRepository.SaveConfigSettings(item);
                }
                response = new Response<int>(result.Id, 200, result.Msg);
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<ConfigSettingsBO>> GetConfigSettings(ConfigSettingsBO objconfig)
        {
            Response<List<ConfigSettingsBO>> response;
            try
            {
                var result = _configurationRepository.GetConfigSettings(objconfig);
                if (result.Count == 0)
                {
                    response = new Response<List<ConfigSettingsBO>>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<List<ConfigSettingsBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<ConfigSettingsBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> DeleteConfigSettings(int ConfigID,int TenantID,int LocationID)
        {
            Response<int> response;
            try
            {
                var result = _configurationRepository.DeleteConfigSettings(ConfigID,TenantID,LocationID);
                if (result.Id == 0)
                {
                    response = new Response<int>(result.Id, 200, result.Msg);
                }
                else
                {
                    response = new Response<int>(result.Id, 200, result.Msg);
                }
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> SaveGeneralConfigSettings(GeneralConfigSettingsBO objconfig)
        {
            Response<int> response;
            try
            {
                var result = _configurationRepository.SaveGeneralConfigSettings(objconfig);
                if (result.Id == 0)
                {
                    response = new Response<int>(result.Id, 200, result.Msg);
                }
                else
                {
                    response = new Response<int>(result.Id, 200, result.Msg);
                }
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<GeneralConfigSettingsBO>> GetGeneralConfigSettings(GeneralConfigSettingsBO objconfig)
        {
            Response<List<GeneralConfigSettingsBO>> response;
            try
            {
                var result = _configurationRepository.GetGeneralConfigSettings(objconfig);
                if (result.Count == 0)
                {
                    response = new Response<List<GeneralConfigSettingsBO>>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<List<GeneralConfigSettingsBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<GeneralConfigSettingsBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<ProofDetailsBO>> GetProofDetails(int CountryID)
        {
            Response<List<ProofDetailsBO>> response;
            try
            {
                var result = _configurationRepository.GetProofDetails(CountryID);
                if (result.Count == 0)
                {
                    response = new Response<List<ProofDetailsBO>>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<List<ProofDetailsBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<ProofDetailsBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> SaveTimesheetPaymentModeSettings(TSPaymentModeSettingsBO mode)
        {
            Response<SaveOut> response;
            try
            {
                var result = _configurationRepository.SaveTimesheetPaymentModeSettings(mode);
                if (result.Id == 0)
                {
                    response = new Response<SaveOut>(result,200,"Timesheet Payment Mode Data Creation or Updation is Failed");
                }
                else
                {
                    response = new Response<SaveOut>(result,200,result.Msg);
                }
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message,500);
            }
            return response;
        }
        public Response<TSPaymentModeSettingsBO> GetTimesheetPaymentModeSettings(TSPaymentModeSettingsBO mode)
        {
            Response<TSPaymentModeSettingsBO> response;
            try
            {
                var result = _configurationRepository.GetTimesheetPaymentModeSettings(mode);
                if (result == null)
                {
                    response = new Response<TSPaymentModeSettingsBO>(result,200,"Data Not Found");
                }
                else
                {
                    response = new Response<TSPaymentModeSettingsBO>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<TSPaymentModeSettingsBO>(ex.Message,500);
            }
            return response;
        }
    }
}