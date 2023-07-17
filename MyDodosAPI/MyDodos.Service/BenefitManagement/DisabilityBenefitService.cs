using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Wrapper;
using MyDodos.Domain.BenefitManagement;
using MyDodos.Repository.BenefitManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace MyDodos.Service.BenefitManagement
{
    public class DisabilityBenefitService : IDisabilityBenefitService
    {
        private readonly IConfiguration _configuration;
        private readonly IDisabilityBenefitRepository _disabilityBenefitRepository;
        public DisabilityBenefitService(IConfiguration configuration, IDisabilityBenefitRepository disabilityBenefitRepository)
        {
            _configuration = configuration;
            _disabilityBenefitRepository = disabilityBenefitRepository;
        }
        public Response<int> SaveMasDisabilityBenefit(MasDisabilityBenefitBO objben)
        {
            Response<int> response;
            try
            {
                var result = _disabilityBenefitRepository.SaveMasDisabilityBenefit(objben);
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
        public Response<List<MasDisabilityBenefitBO>> GetMasDisabilityBenefit(int DisabilityBenefitID, int TenantID, int LocationID)
        {
            Response<List<MasDisabilityBenefitBO>> response;
            try
            {
                var result = _disabilityBenefitRepository.GetMasDisabilityBenefit(DisabilityBenefitID, TenantID, LocationID);
                if (result.Count == 0)
                {
                    response = new Response<List<MasDisabilityBenefitBO>>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<List<MasDisabilityBenefitBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<MasDisabilityBenefitBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> DeleteMasDisabilityBenefit(int DisabilityBenefitID, int TenantID, int LocationID)
        {
            Response<int> response;
            try
            {
                var result = _disabilityBenefitRepository.DeleteMasDisabilityBenefit(DisabilityBenefitID, TenantID, LocationID);
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
    }
}