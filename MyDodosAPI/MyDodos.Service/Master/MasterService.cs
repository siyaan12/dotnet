using Microsoft.Extensions.Configuration;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.Master;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.Master;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace MyDodos.Service.Master
{
    public class MasterService : IMasterService
    {
        private readonly IConfiguration _configuration;
        private readonly IMasterRepository _masterRepository;
        public MasterService(IConfiguration configuration, IMasterRepository masterRepository)
        {
            _configuration = configuration;
            _masterRepository =  masterRepository;
        }
        public Response<List<TenantProfileBO>> GetTenantDetails(MasterInputBO master)
        {
            Response<List<TenantProfileBO>> response;
            try
            {
                var result = _masterRepository.GetTenantDetails(master);
                if (result.Count() == 0)
                {
                    response = new Response<List<TenantProfileBO>>(result,200,"Data Not Found");
                }
                else
                {
                    response = new Response<List<TenantProfileBO>>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<TenantProfileBO>>(ex.Message,500);
            }
            return response;
        }
        public Response<List<LocationBO>> GetLocationDetails(MasterInputBO master)
        {
            Response<List<LocationBO>> response;
            try
            {
                var result = _masterRepository.GetLocationDetails(master);
                if (result.Count() == 0)
                {
                    response = new Response<List<LocationBO>>(result,200,"Data Not Found");
                }
                else
                {
                    response = new Response<List<LocationBO>>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<LocationBO>>(ex.Message,500);
            }
            return response;
        }
        public Response<List<YearBO>> GetYearDetails(MasterInputBO master)
        {
            Response<List<YearBO>> response;
            try
            {
                var result = _masterRepository.GetYearDetails(master);
                if (result.Count() == 0)
                {
                    response = new Response<List<YearBO>>(result,200,"Data Not Found");
                }
                else
                {
                    response = new Response<List<YearBO>>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<YearBO>>(ex.Message,500);
            }
            return response;
        }
        public Response<List<HRLeaveAllocReferenceBO>> GetLeaveAllocReference(int LeaveGroupID, int LocationID, int TenantID)
        {
            Response<List<HRLeaveAllocReferenceBO>> response;
            try
            {
                var result = _masterRepository.GetLeaveAllocReference(LeaveGroupID, LocationID, TenantID);
                if (result.Count() == 0)
                {
                    response = new Response<List<HRLeaveAllocReferenceBO>>(result,200,"Data Not Found");
                }
                else
                {
                    response = new Response<List<HRLeaveAllocReferenceBO>>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<HRLeaveAllocReferenceBO>>(ex.Message,500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> DeleteLeaveAllocReference(int LeaveAllocationID)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                var result = _masterRepository.DeleteLeaveAllocReference(LeaveAllocationID);
                if(result.RequestID == 0)
                {
                response = new Response<LeaveRequestModelMsg>(result,200,"Cannot Deleted");
                }
                else
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"Deleted Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<List<HRMasLeaveCategoryBO>> GetMasLeaveCategory(int CategoryID, int LocationID, int TenantID)
        {
            Response<List<HRMasLeaveCategoryBO>> response;
            try
            {
                var result = _masterRepository.GetMasLeaveCategory(CategoryID, LocationID, TenantID);
                if (result.Count() == 0)
                {
                    response = new Response<List<HRMasLeaveCategoryBO>>(result,200,"Data Not Found");
                }
                else
                {
                    response = new Response<List<HRMasLeaveCategoryBO>>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<HRMasLeaveCategoryBO>>(ex.Message,500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> DeleteMasLeaveCategory(int CategoryID)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                var result = _masterRepository.DeleteMasLeaveCategory(CategoryID);
                if(result.RequestID == 0)
                {
                response = new Response<LeaveRequestModelMsg>(result,200,"Cannot Deleted");
                }
                else
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"Deleted Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> SaveOptionalSet(OptionalSetBO optional)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                var result = _masterRepository.SaveOptionalSet(optional);
                if (result.RequestID == 0)
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"OptionalSet Data Creation or Updation is Failed");
                }
                else
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"Saved Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<List<OptionalSetBO>> GetOptionalSet(int TenantID, int FieldId,int OptionalSetValue)
        {
            Response<List<OptionalSetBO>> response;
            try
            {
                var result = _masterRepository.GetOptionalSet(TenantID,FieldId,OptionalSetValue);
                if (result.Count() == 0)
                {
                    response = new Response<List<OptionalSetBO>>(result,200,"Data Not Found");
                }
                else
                {
                    response = new Response<List<OptionalSetBO>>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<OptionalSetBO>>(ex.Message,500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> DeleteOptionalSet(int FormId)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                var result = _masterRepository.DeleteOptionalSet(FormId);
                if(result.RequestID == 0)
                {
                response = new Response<LeaveRequestModelMsg>(result,200,"Cannot Deleted");
                }
                else
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"Deleted Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
    }
}