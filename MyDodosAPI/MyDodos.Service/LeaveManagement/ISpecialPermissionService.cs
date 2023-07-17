using System.Collections.Generic;
using MyDodos.Domain.PermissionBO;
using MyDodos.Domain.Wrapper;

namespace MyDodos.Service.LeaveManagement
{
    public interface ISpecialPermissionService
    {
        Response<int> SaveSpecialPermission (SpecialPermissionBO objperm);
        Response<List<SpecialPermissionBO>> GetSpecialPermission(int PermissionID, int TenantID, int LocationID);
        Response<int> DeleteSpecialPermission(int PermissionID, int TenantID, int LocationID);
    }
}