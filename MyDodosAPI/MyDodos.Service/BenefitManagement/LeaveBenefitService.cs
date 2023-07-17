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
using MyDodos.Repository.LeaveManagement;
using MyDodos.Repository.Auth;
using MyDodos.Domain.LeaveBO;

namespace MyDodos.Service.BenefitManagement
{
    public class LeaveBenefitService : ILeaveBenefitService
    {
        private readonly IConfiguration _configuration;
        private readonly ILeaveBenefitRepository _leaveBenefitRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly ILeaveRepository _leaveRepository;
        private readonly IAuthRepository _authRepository;
        public LeaveBenefitService(IConfiguration configuration, ILeaveBenefitRepository leaveBenefitRepository, IGradeRepository gradeRepository, ILeaveRepository leaveRepository,IAuthRepository authRepository)
        {
            _configuration = configuration;
            _leaveBenefitRepository = leaveBenefitRepository;
            _gradeRepository = gradeRepository;
            _leaveRepository = leaveRepository;
            _authRepository = authRepository;
        }
        public Response<List<HRVwBeneftisLeave_BO>> GetBenefitLeaveCategory(int TenantID, int LocationID)
        {
            Response<List<HRVwBeneftisLeave_BO>> response;
            try
            {
                var result = _leaveRepository.GetLeaveCategoryMaster(TenantID, LocationID);
                if (result.Count == 0)
                {
                    response = new Response<List<HRVwBeneftisLeave_BO>>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<List<HRVwBeneftisLeave_BO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<HRVwBeneftisLeave_BO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> SaveLeaveBenefits(MasLeaveGroupBO objgroup)
        {
            Response<int> response;
            try
            {
                var result = _leaveBenefitRepository.SaveMasLeaveGroup(objgroup);
                if (result.Id == 0)
                {
                    response = new Response<int>(result.Id, 200, result.Msg);
                }
                else
                {
                    foreach (var item in objgroup.objalloc)
                    {
                        var res = _leaveBenefitRepository.SaveLeaveAllocReference(item, result.Id);
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
        public Response<List<MasLeaveGroupBO>> GetLeaveBenefits(int LeaveGroupID, int TenantID, int LocationID)
        {
            Response<List<MasLeaveGroupBO>> response;
            List<LeaveAllocReferenceBO> objref = new List<LeaveAllocReferenceBO>();
            try
            {
                var result = _leaveBenefitRepository.GetMasLeaveGroup(LeaveGroupID, TenantID, LocationID);
                if (result.Count == 0)
                {
                    response = new Response<List<MasLeaveGroupBO>>(result, 200, "Data not Found");
                }
                else
                {
                    foreach (var item in result)
                    {
                        var res = _leaveBenefitRepository.GetLeaveAllocReference(item.LeaveGroupID, item.TenantID, item.LocationID);
                        objref = res;
                        item.objalloc = new List<LeaveAllocReferenceBO>(objref);
                        objref = new List<LeaveAllocReferenceBO>();
                    }

                    response = new Response<List<MasLeaveGroupBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<MasLeaveGroupBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> DeleteLeaveBenefits(int LeaveGroupID, int TenantID, int LocationID)
        {
            Response<int> response;
            try
            {
                var result = _leaveBenefitRepository.DeleteMasLeaveGroup(LeaveGroupID, TenantID, LocationID);
                if (result.Id == 0)
                {
                    response = new Response<int>(result.Id, 200, result.Msg);
                }
                else
                {
                    var res = _leaveBenefitRepository.DeleteLeaveAllocReference(result.Id, TenantID, LocationID);
                    response = new Response<int>(result.Id, 200, result.Msg);
                }
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<MasLeaveGroupBO>> GetLeaveBenefitsByGroupType(int GroupTypeID, int TenantID, int LocationID)
        {
            Response<List<MasLeaveGroupBO>> response;
            List<LeaveAllocReferenceBO> objref = new List<LeaveAllocReferenceBO>();
            try
            {
                var result = _leaveBenefitRepository.GetMasLeaveGroupByGroupType(GroupTypeID, TenantID, LocationID);
                if (result.Count == 0)
                {
                    response = new Response<List<MasLeaveGroupBO>>(result, 200, "Data not Found");
                }
                else
                {
                    foreach (var item in result)
                    {
                        var res = _leaveBenefitRepository.GetLeaveAllocReference(item.LeaveGroupID, item.TenantID, item.LocationID);
                        objref = res;
                        item.objalloc = new List<LeaveAllocReferenceBO>(objref);
                        objref = new List<LeaveAllocReferenceBO>();
                    }
                    response = new Response<List<MasLeaveGroupBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<MasLeaveGroupBO>>(ex.Message, 500);
            }
            return response;
        }
    }
}