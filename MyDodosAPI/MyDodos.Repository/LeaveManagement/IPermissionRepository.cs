using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDodos.Domain.PermissionBO;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.LeaveManagement;

namespace MyDodos.Repository.LeaveManagement
{
    public interface IPermissionRepository
    {
        PermissionRequestModelMsg AddNewPermissionRequest(PermissionModel permission);
        GetMyPermissionList GetMyPermissionList(GetMyPermissionList inputParam);
        List<PermissionModel> GetPermission(int TenantID, int LocationID, int PermID);
        // Mobile API
        PermissionRequestModelMsg MAddNewPermissionRequest(PermissionModel permission);
        List<PermissionModel> MGetPermission(int TenantID, int LocationID, int PermID);
        List<PermissionModel> MGetMyPermissionList(GetMyPermissionListInputs inputParam);
    }
}