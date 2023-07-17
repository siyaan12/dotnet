using System;
using System.Collections.Generic;
using MyDodos.Domain.Holiday;
using MyDodos.Domain.LeaveBO;

namespace MyDodos.Repository.Holiday
{
    public interface IHolidayRepository
    {
        List<HolidayBO> GetHolidayList(int TeantID, int YearID);
        List<HolidayBO> GetEmployeeHoliday(int EmpID, int YearID);
        int SaveEmployeeHoliday(EmployeeHolidayBO _holiday);
        List<MasYearBO> GetYearDetails(int YearID ,int TeantID, int LocationID);
        LeaveRequestModelMsg SaveMasYear(MasYear year);
    }
}