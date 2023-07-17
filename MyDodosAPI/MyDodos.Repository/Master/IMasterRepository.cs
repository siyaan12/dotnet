using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.Master;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.ProjectManagement;

namespace MyDodos.Repository.Master
{
    public interface IMasterRepository
    {
        List<TenantProfileBO> GetTenantDetails(MasterInputBO master);
        List<LocationBO> GetLocationDetails(MasterInputBO master);
        List<YearBO> GetYearDetails(MasterInputBO master);
        Response<List<ShiftConfigSettingBO>> GetShiftConfigSettings(int TenantID,int LocationID);
        List<HRLeaveAllocReferenceBO> GetLeaveAllocReference(int LeaveGroupID, int LocationID, int TenantID);
        List<HRMasLeaveCategoryBO> GetMasLeaveCategory(int CategoryID, int LocationID, int TenantID);
        LeaveRequestModelMsg DeleteLeaveAllocReference(int LeaveAllocationID);
        LeaveRequestModelMsg DeleteMasLeaveCategory(int CategoryID);
        LeaveRequestModelMsg SaveOptionalSet(OptionalSetBO optional);
        List<OptionalSetBO> GetOptionalSet(int TenantID, int FieldId,int OptionalSetValue);
        LeaveRequestModelMsg DeleteOptionalSet(int FormId);
    }
}