using System.Collections.Generic;
using MyDodos.Domain.BenefitManagement;
using MyDodos.Domain.Wrapper;

namespace MyDodos.Service.BenefitManagement
{
    public interface IGradeService
    {
        Response<int> SaveGrade(Grade objgrade);
        Response<List<Grade>> GetGrade(int ProductID, int TenantID, int LocationID);
        Response<int> DeleteGrade(int GradeID,int TenantID,int LocationID);
        Response<int> SaveGroupTypeGrade(RoleGrade objrolegrade);
    }
}