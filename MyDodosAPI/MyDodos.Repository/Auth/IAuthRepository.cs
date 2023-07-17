using System;
using System.Collections.Generic;
using System.Linq;
using MyDodos.Domain.AuthBO;
using MyDodos.Domain.LoginBO;
using MyDodos.ViewModel.Document;
using MyDodos.Domain.Wrapper;
using MyDodos.Domain.HR;
using MyDodos.Domain.Administrative;
using MyDodos.ViewModel.Entitlement;
using MyDodos.ViewModel.Common;
using MyDodos.ViewModel.Employee;
using MyDodos.Domain.Employee;
using MyDodos.Domain.Master;

namespace MyDodos.Repository.Auth
{
    public interface IAuthRepository
    {
        Response<AppUserDetailsBO> AddProfile(InputAppUserBO inputLogin);
        List<LoginEmployeeBO> GetLoginEmployee(int AppUserID, string loginType);
        List<LoginLocationBO> GetLoginLocation(int AppUserID, string loginType);
        List<LoginYearBO> GetLoginYear(int AppUserID, string loginType);
        List<GenDocumentBO> GetDocument(int docId,int entityId,string docKey,string entity,int tenantId);
        List<DocContainerBO> GetDocContainer(int TenantID,int ProductID);
        int CheckAppUserID(TAppUser AppUser);
        Response<List<RoleBO>> GetEntRoles(int ProductID, string GroupType);
        Response<List<RtnUserGroupBO>> GetAccountTypes(int ProductID, int TenantID);
        Response<List<EntRolesBO>> GetEntTenantRoles(InpurtEntRolesBO roles);
        List<GeneralConfigSettings> GetTenantCurrency(int TenantID,int LocationID,bool IsActive = true);
        SaveOut SaveDemoRequest(MDemoRequest request);
        SaveOut SaveDetailRequest(MDetailRequest request);
        string UpdateCenEntAppUser(EmployeeProfileInputBO objuser);
        string SaveTenantProfile(TenantProfiledataBO objuser);
        TenantPaymentModeBO GetTenantPaymentMode(int TenantID);
    }
}
