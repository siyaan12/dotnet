using Microsoft.Extensions.Configuration;
using MyDodos.Domain.PermissionBO;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.LeaveManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyDodos.Service.LeaveManagement
{
    public class SpecialPermissionService : ISpecialPermissionService
    {
        private readonly ISpecialPermissionRepository _specialPermissionRepository;
        private readonly IConfiguration _configuration;
        public SpecialPermissionService(IConfiguration configuration, ISpecialPermissionRepository specialPermissionRepository)
        {
            _configuration = configuration;
            _specialPermissionRepository = specialPermissionRepository;
        }
        public Response<int> SaveSpecialPermission (SpecialPermissionBO objperm)
        {
            Response<int> response;
            try
            {
                var result = _specialPermissionRepository.SaveSpecialPermission(objperm);
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
        public Response<List<SpecialPermissionBO>> GetSpecialPermission(int PermissionID, int TenantID, int LocationID)
        {
            Response<List<SpecialPermissionBO>> response;
            try
            {
                var result = _specialPermissionRepository.GetSpecialPermission(PermissionID,TenantID, LocationID);
                if (result.Count == 0)
                {
                    response = new Response<List<SpecialPermissionBO>>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<List<SpecialPermissionBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<SpecialPermissionBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> DeleteSpecialPermission(int PermissionID, int TenantID, int LocationID)
        {
            Response<int> response;
            try
            {
                var result = _specialPermissionRepository.DeleteSpecialPermission(PermissionID,TenantID,LocationID);
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