using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Wrapper;
using MyDodos.Domain.BenefitManagement;
using MyDodos.Repository.BenefitManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyDodos.Repository.Auth;
using MyDodos.ViewModel.Entitlement;
using MyDodos.ViewModel.Common;

namespace MyDodos.Service.BenefitManagement
{
    public class GradeService : IGradeService
    {
        private readonly IConfiguration _configuration;
        private readonly IGradeRepository _gradeRepository;
        private readonly IAuthRepository _authRepository;
        public GradeService(IConfiguration configuration, IGradeRepository gradeRepository, IAuthRepository authRepository)
        {
            _configuration = configuration;
            _gradeRepository = gradeRepository;
            _authRepository = authRepository; 
        }
        public Response<int> SaveGrade(Grade objgrade)
        {
            Response<int> response;
            try
            {
                var result = _gradeRepository.SaveGrade(objgrade);
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
        public Response<int> SaveGroupTypeGrade(RoleGrade objrolegrade)
        {
            Response<int> response;
            try
            {
                Grade objgrade = new Grade();
                SaveOut objout = new SaveOut();
                var objres = _authRepository.GetAccountTypes(objrolegrade.ProductID, objrolegrade.TenantID);
                var rtnobj = objres.Data;
                
                if (rtnobj.Count == 0)
                {
                    response = new Response<int>(rtnobj.Count, 200, "Data Not Retrived");
                }
                else
                {
                    foreach(var item in rtnobj)
                    {
                    objgrade.TenantID = item.TenantID;
                    objgrade.LocationID = objrolegrade.LocationID;
                    objgrade.GroupTypeID = item.GroupTypeID;
                    objout = _gradeRepository.SaveGrade(objgrade);
                    }
                    response = new Response<int>(objout.Id, 200, objout.Msg);
                }
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<Grade>> GetGrade(int ProductID, int TenantID, int LocationID)
        {
            Response<List<Grade>> response;
            try
            {
                var result = _gradeRepository.GetGrade(TenantID, LocationID);                
                if (result.Count == 0)
                {
                    response = new Response<List<Grade>>(result, 200, "Data not Found");
                }
                else
                {
                    foreach (var item in result)
                    {
                    var objres = _authRepository.GetAccountTypes(ProductID, TenantID);
                    var rtnobj = objres.Data;
                    var objcatgory = rtnobj.Where(s => s.GroupTypeID == item.GroupTypeID).ToList();
                     if(objcatgory.Count > 0)
                     {
                        item.RoleCategory = objcatgory[0].GroupType;
                     }   
                    }
                    response = new Response<List<Grade>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<Grade>>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> DeleteGrade(int GradeID,int TenantID,int LocationID)
        {
            Response<int> response;
            try
            {
                var result = _gradeRepository.DeleteGrade(GradeID,TenantID,LocationID);
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