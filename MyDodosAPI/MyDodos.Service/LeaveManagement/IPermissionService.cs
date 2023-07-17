using System.Collections.Generic;
using MyDodos.Domain.PermissionBO;
using MyDodos.Domain.Holiday;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.LeaveManagement;

namespace MyDodos.Service.LeaveManagement
{
    public interface IPermissionService
    {
        Response<PermissionRequestModelMsg> AddNewPermissionRequest(PermissionModel permission);
        Response<GetMyPermissionList> GetMyPermissionList(GetMyPermissionList objresult);
        Response<List<PermissionModel>> GetMyPermission(int TenantID,int LocationID,int PermID);
        //Mobile API
        Response<PermissionRequestModelMsg> MAddNewPermissionRequest(PermissionModel permission);
        Response<List<PermissionModel>> MGetMyPermission(int TenantID,int LocationID,int PermID);
        Response<List<PermissionModel>> MGetMyPermissionList(GetMyPermissionListInputs objresult);
    }
}