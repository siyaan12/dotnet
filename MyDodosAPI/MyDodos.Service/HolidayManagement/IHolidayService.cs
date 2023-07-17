using System.Collections.Generic;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.Holiday;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.LeaveManagement;

namespace MyDodos.Service.HolidayManagement
{
    public interface IHolidayService
    {
        Response<List<MasYearBO>> GetYearHolidayDetails(int TenantID, int LocationID, int YearID);
        Response<LeaveRequestModelMsg> SaveMasYear(MasYear year);
        Response<List<HolidayBO>> GetEmployeeHoliday(int EmpID, int YearID);
    }
}