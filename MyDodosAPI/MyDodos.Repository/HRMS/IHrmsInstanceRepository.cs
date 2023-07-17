using System;
using System.Collections.Generic;
using MyDodos.ViewModel.Common;
using MyDodos.ViewModel.Entitlement;
using MyDodos.ViewModel.HR;
using MyDodos.ViewModel.HRMS;

namespace MyDodos.Repository.HRMS
{
    public interface IHrmsInstanceRepository
    {
        SaveOut SaveLocation(HRMSLocationBO objlocation);
        SaveOut SaveMasDepartment(HRMSDepartmentBO department);
        List<HREmployeeBO> initGetEmployee(int EmpID, int LocationID);
        SaveOut SaveConsoleLeaveJournal(LeaveJournalBO objJournal);
        // Tuple<OnBoardingGenralBO, AppUserDetailsBO> GetToHRMS(OnBoardingGenralBO genral, AppUserDetailsBO AppUser);
        // void GetonbordToHRMS(OnBoardingGenralBO genral, AppUserDetailsBO AppUser);
    }
}