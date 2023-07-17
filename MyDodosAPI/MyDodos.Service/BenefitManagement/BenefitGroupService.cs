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
    public class BenefitGroupService : IBenefitGroupService
    {
        private readonly IConfiguration _configuration;
        private readonly IBenefitGroupRepository _benefitGroupRepository;
        public BenefitGroupService(IConfiguration configuration, IBenefitGroupRepository benefitGroupRepository)
        {
            _configuration = configuration;
            _benefitGroupRepository = benefitGroupRepository;
        }
        public Response<int> SaveBenefitGroup(BenefitGroupBO objgroup)
        {
            Response<int> response;
            try
            {
                var result = _benefitGroupRepository.SaveBenefitGroup(objgroup);
                if (result.Id == 0)
                {
                    response = new Response<int>(result.Id, 200, result.Msg);
                }
                else
                {
                    foreach (var item in objgroup.objplan)
                    {
                        var res = _benefitGroupRepository.SaveBenefitMedPlan(item, result.Id);
                    }
                    response = new Response<int>(result.Id, 200, result.Msg);
                }
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<BenefitGroupBO>> GetBenefitGroup(int BenefitGroupID, int TenantID, int LocationID)
        {
            Response<List<BenefitGroupBO>> response;
            List<BenefitGroupMedPlanBO> objmed = new List<BenefitGroupMedPlanBO>();
            try
            {
                var result = _benefitGroupRepository.GetBenefitGroup(BenefitGroupID, TenantID, LocationID);
                if (result.Count == 0)
                {
                    response = new Response<List<BenefitGroupBO>>(result, 200, "Data not Found");
                }
                else
                {
                    foreach (var item in result)
                    {
                        var res = _benefitGroupRepository.GetBenefitMedPlan(item.BenefitGroupID, item.TenantID, item.LocationID);
                        objmed = res;
                        item.objplan = new List<BenefitGroupMedPlanBO>(objmed);
                        objmed = new List<BenefitGroupMedPlanBO>();
                    }

                    response = new Response<List<BenefitGroupBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<BenefitGroupBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> DeleteBenefitGroup(int BenefitGroupID, int TenantID, int LocationID)
        {
            Response<int> response;
            try
            {
                var result = _benefitGroupRepository.DeleteBenefitMedPlan(BenefitGroupID, TenantID, LocationID);
                if (result.Id == 0)
                {
                    response = new Response<int>(result.Id, 200, result.Msg);
                }
                else
                {
                    var res = _benefitGroupRepository.DeleteBenefitGroup(result.Id, TenantID, LocationID);
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