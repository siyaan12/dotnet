using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.Administrative;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.Administrative;

namespace MyDodos.Repository.Administrative
{
    public interface IAdministrativeRepository
    {
        GenSeqNum GenSequenceNo(int TenantID, int LocationID, string ServiceName);
        LeaveRequestModelMsg SaveMasDepartment(MasDepartmentBO department);
        List<MasDepartmentBO> GetMasDepartment(int TenantID,int LocationID,int DeptID);
        LeaveRequestModelMsg DeleteMasDepartment(int DeptID);
        DepartmentList GetDepartmentList(DepartmentList inputParam);
    }
}