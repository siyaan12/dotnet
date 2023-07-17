using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDodos.Domain.PermissionBO;
using MyDodos.ViewModel.Common;

namespace MyDodos.Repository.LeaveManagement
{
    public interface ISpecialPermissionRepository
    {
        SaveOut SaveSpecialPermission (SpecialPermissionBO objperm);
        List<SpecialPermissionBO> GetSpecialPermission(int PermissionID, int TenantID, int LocationID);
        SaveOut DeleteSpecialPermission(int PermissionID, int TenantID, int LocationID);
    }
}