using System;
using System.Collections.Generic;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.Common;
using MyDodos.ViewModel.Entitlement;
using MyDodos.ViewModel.HR;
using MyDodos.ViewModel.HRMS;

namespace MyDodos.Service.HRMS
{
    public interface IHrmsInstanceService
    {
        Response<SaveOut> SaveLocation(HRMSLocationBO objlocation);
        Response<SaveOut> SaveMasDepartment(HRMSDepartmentBO objlocation);
       Response<string> GetToHRMS(OnBoardingGenralBO genral, AppUserDetailsBO AppUser);
       Response<SaveOut> SaveConsoleLeaveJournal(LeaveJournalBO objJournal);
    }
}