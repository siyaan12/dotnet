using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Holiday;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.Holiday;
using MyDodos.Repository.LeaveManagement;
using MyDodos.ViewModel.LeaveManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyDodos.Service.HolidayManagement
{
    public class HolidayService : IHolidayService
    {
        private readonly IConfiguration _configuration;
        private readonly ILeaveRepository _leaveRepository;
        private readonly IHolidayRepository _holidayrepository;
        public HolidayService(IConfiguration configuration, ILeaveRepository leaveRepository, IHolidayRepository holidayrepository)
        {
            _configuration = configuration;
            _leaveRepository =  leaveRepository;
            _holidayrepository = holidayrepository;
        }
        public Response<List<MasYearBO>> GetYearHolidayDetails(int TenantID, int LocationID, int YearID)
        {
            Response<List<MasYearBO>> response;
            try
            {
                var result = _holidayrepository.GetYearDetails(YearID, TenantID, LocationID);         
                if (result.Count() == 0)
                {      
                    response = new Response<List<MasYearBO>>(result, 200, "Data Not Found");
                }
                else
                {      
                    foreach(var item in result)
                    {
                        var holiday = _holidayrepository.GetHolidayList(item.TenantID,item.YearID);
                        item.Holiday = holiday;
                    }
                    response = new Response<List<MasYearBO>>(result, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<MasYearBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> SaveMasYear(MasYear year)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                var result = _holidayrepository.SaveMasYear(year);
                if (result.RequestID == 0)
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"MasYear Data Creation or Updation is Failed");
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
        public Response<List<HolidayBO>> GetEmployeeHoliday(int EmpID, int YearID)
        {
            Response<List<HolidayBO>> response;
            try
            {
                //int result = 0;               
               var result = _holidayrepository.GetEmployeeHoliday(EmpID, YearID);               
                response = new Response<List<HolidayBO>>(result, 200, "Data Retraived");
            }
            catch (Exception ex)
            {
                response = new Response<List<HolidayBO>>(ex.Message, 500);
            }
            return response;
        }
    }
}