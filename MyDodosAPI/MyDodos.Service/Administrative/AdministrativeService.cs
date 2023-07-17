using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Wrapper;
using MyDodos.Domain.Administrative;
using MyDodos.Repository.Administrative;
using MyDodos.Domain.LeaveBO;
using MyDodos.ViewModel.Administrative;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyDodos.Service.Administrative
{
    public class AdministrativeService : IAdministrativeService
    {
        private readonly IConfiguration _configuration;
        private readonly IAdministrativeRepository _administrativeRepository;        
        public AdministrativeService(IConfiguration configuration, IAdministrativeRepository administrativeRepository)
        {
            _configuration = configuration;
            _administrativeRepository =  administrativeRepository;
        }
        public Response<LeaveRequestModelMsg> SaveMasDepartment(MasDepartmentBO department)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                if(department.DeptID==0)
                {
                    var num=_administrativeRepository.GenSequenceNo(department.TenantID,department.LocationID,"Department");
                    department.DepartmentCode=num.Id;
                }
                var result = _administrativeRepository.SaveMasDepartment(department);
                if (result.RequestID == 0)
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"MasDepartment Data Creation or Updation is Failed");
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
        public Response<List<MasDepartmentBO>> GetMasDepartment(int TenantID,int LocationID,int DeptID)
        {
            Response<List<MasDepartmentBO>> response;
            try
            {
                var result = _administrativeRepository.GetMasDepartment(TenantID,LocationID,DeptID);
                if (result.Count() == 0)
                {
                    response = new Response<List<MasDepartmentBO>>(result,200,"Data Not Found");
                }
                else
                {
                    response = new Response<List<MasDepartmentBO>>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<MasDepartmentBO>>(ex.Message,500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> DeleteMasDepartment(int DeptID)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                var result = _administrativeRepository.DeleteMasDepartment(DeptID);
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
        public Response<DepartmentList> GetDepartmentList(DepartmentList objresult)
        {
            Response<DepartmentList> response;
            try
            {
                var result = _administrativeRepository.GetDepartmentList(objresult);         
                if (result.objDepartmentList.Count() == 0)
                {      
                    response = new Response<DepartmentList>(result, 200, "Data Not Found");
                }
                else
                {      
                    response = new Response<DepartmentList>(result, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<DepartmentList>(ex.Message, 500);
            }
            return response;
        }
    }
}