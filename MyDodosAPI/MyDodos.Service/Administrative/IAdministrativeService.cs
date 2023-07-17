using System.Collections.Generic;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.Administrative;
using MyDodos.ViewModel.Administrative;
using MyDodos.Domain.Wrapper;

namespace MyDodos.Service.Administrative
{
    public interface IAdministrativeService
    {
        Response<LeaveRequestModelMsg> SaveMasDepartment(MasDepartmentBO department);
        Response<List<MasDepartmentBO>> GetMasDepartment(int TenantID,int LocationID,int DeptID);
        Response<LeaveRequestModelMsg> DeleteMasDepartment(int DeptID);
        Response<DepartmentList> GetDepartmentList(DepartmentList objresult);
    }
}