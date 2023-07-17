using System.Collections.Generic;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.Master;
using MyDodos.Domain.Wrapper;

namespace MyDodos.Service.Master
{
    public interface IMasterService
    {
        Response<List<TenantProfileBO>> GetTenantDetails(MasterInputBO master);
        Response<List<LocationBO>> GetLocationDetails(MasterInputBO master);
        Response<List<YearBO>> GetYearDetails(MasterInputBO master);
        Response<List<HRLeaveAllocReferenceBO>> GetLeaveAllocReference(int LeaveGroupID, int LocationID, int TenantID);
        Response<List<HRMasLeaveCategoryBO>> GetMasLeaveCategory(int CategoryID, int LocationID, int TenantID);
        Response<LeaveRequestModelMsg> DeleteLeaveAllocReference(int LeaveAllocationID);
        Response<LeaveRequestModelMsg> DeleteMasLeaveCategory(int CategoryID);
        Response<LeaveRequestModelMsg> SaveOptionalSet(OptionalSetBO optional);
        Response<List<OptionalSetBO>> GetOptionalSet(int TenantID, int FieldId,int OptionalSetValue);
        Response<LeaveRequestModelMsg> DeleteOptionalSet(int FormId);
    }
}