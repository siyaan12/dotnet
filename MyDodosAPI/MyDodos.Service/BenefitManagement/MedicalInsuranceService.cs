using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Wrapper;
using MyDodos.Domain.BenefitManagement;
using MyDodos.Repository.BenefitManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyDodos.ViewModel.Common;
using MyDodos.ViewModel.BenefitManagement;

namespace MyDodos.Service.BenefitManagement
{
    public class MedicalInsuranceService : IMedicalInsuranceService
    {
        private readonly IConfiguration _configuration;
        private readonly IMedicalInsuranceRepository _medicalInsuranceRepository;
        public MedicalInsuranceService(IConfiguration configuration, IMedicalInsuranceRepository medicalInsuranceRepository)
        {
            _configuration = configuration;
            _medicalInsuranceRepository = medicalInsuranceRepository;
        }
        public Response<List<PlanTypeCategoryBO>> GetPlanTypeCategory (int PlanTypeID,int TenantID,int LocationID)
        {
            Response<List<PlanTypeCategoryBO>> response;
            try
            {
                var result = _medicalInsuranceRepository.GetPlanTypeCategory(PlanTypeID,TenantID, LocationID);
                if (result.Count == 0)
                {
                    response = new Response<List<PlanTypeCategoryBO>>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<List<PlanTypeCategoryBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<PlanTypeCategoryBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> SaveHRBenefitPlans(HRBenefitPlansBO objplan)
        {
            Response<int> response;
            try
            {
                var result = _medicalInsuranceRepository.SaveHRBenefitPlans(objplan);
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
        public Response<List<HRBenefitPlansBO>> GetHRBenefitPlans(HRBenefitPlansBO objplan)
        {
            Response<List<HRBenefitPlansBO>> response;
            try
            {
                var result = _medicalInsuranceRepository.GetHRBenefitPlans(objplan);
                if (result.Count == 0)
                {
                    response = new Response<List<HRBenefitPlansBO>>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<List<HRBenefitPlansBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<HRBenefitPlansBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> DeleteHRBenefitPlans(HRBenefitPlansBO objplan)
        {
            Response<int> response;
            try
            {
                var result = _medicalInsuranceRepository.DeleteHRBenefitPlans(objplan);
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
        public Response<HRBenefitPlansSearchBO> GetHRBenefitPlansSearch(HRBenefitPlansSearchBO objplan)
        {
            Response<HRBenefitPlansSearchBO> response;
            try
            {
                var result = _medicalInsuranceRepository.GetHRBenefitPlansSearch(objplan);
                if (result.objplanlist.Count() == 0)
                {
                    response = new Response<HRBenefitPlansSearchBO>(result, 200, "Data Not Found");

                }
                else
                { 
                    response = new Response<HRBenefitPlansSearchBO>(result,200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {

                response = new Response<HRBenefitPlansSearchBO>(ex.Message, 500);
            }
            return response;
        }
    }
}