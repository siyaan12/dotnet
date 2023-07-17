using System;
using System.Collections.Generic;
using MyDodos.Domain.Holiday;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.TimeOff;
using MyDodos.ViewModel.ServerSearch;

namespace MyDodos.ViewModel.TimeOff
{
    public class GetTimeoffRequestBO 
    {
        public List<TimeoffRequestModel> OutStandLeave { get; set; }
        public List<TimeoffRequestModel> OutStandRequest { get; set; }
        public List<TimeoffRequestModel> TimeOffTeamMember { get; set; }
        public List<HolidayBO> EmpHoliday { get; set; }
        public HolidayBO UpcomingHoliday { get; set; }
        public List<MasYearBO> YearDetails { get; set; }
        public List<MasLeaveCategoryBO> LeaveJourney { get; set; }
        public List<HolidayBO> Holiday { get; set; }
    }
    public class GetMyTimeoffList
    {
        public GetMyTimeoffListInputs objMyTimeoffListInput { get; set; }
        public ServerSearchable ServerSearchables { get; set; }
        public List<TimeoffRequestModel> objMyTimeoffList { get; set; }

    }
    public class GetMyTimeoffListInputs
    {
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int YearID { get; set; }
        public int EmpID { get; set; }
        public int ManagerID { get; set; }
        public string LeaveStatus { get; set; }
    }
}
