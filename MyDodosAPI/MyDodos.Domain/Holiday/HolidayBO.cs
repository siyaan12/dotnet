using System;
using System.Collections.Generic;
namespace MyDodos.Domain.Holiday
{
    public class HolidayBO
    {
        public int HolidayID { get; set; }
        public DateTime HDate { get; set; }
        public string HName { get; set; }
        public string HDay { get; set; }
        public string HType { get; set; }
        public string HStatus { get; set; }
        public int YearID { get; set; }
        public int EmpID { get; set; }
        public bool isOptional { get; set; }
    }
    public class EmployeeHolidayBO {
        public int EmpID { get; set; }
        public int YearID { get; set; }
        public int HolidayID { get; set; }
        public string HStatus { get; set; }
        public string YearName { get; set; }
        public string Holidayoptinal { get; set; }
        public int CreatedBy { get; set; }
    }
    public class MasYearBO
    {
        public int YearID { get; set; }
        public int StartYear { get; set; }
        public string YearName { get; set; }
        public int OptionalHoliday { get; set; }
        public DateTime DueDate { get; set; }
        public int TenantID { get; set; }
        public List<HolidayBO> Holiday { get; set; }
    }
    public class MasYear
    {
        public int YearID { get; set; }
        public int StartYear { get; set; }
        public string YearName { get; set; }
        public int OptionalHoliday { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsNotify { get; set; }
        public string NotifyContent { get; set; }
        public string YearStatus { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
    }
}