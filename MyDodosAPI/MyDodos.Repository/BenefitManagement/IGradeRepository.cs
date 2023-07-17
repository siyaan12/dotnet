using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDodos.Domain.BenefitManagement;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.Common;
using MyDodos.ViewModel.Entitlement;

namespace MyDodos.Repository.BenefitManagement
{
    public interface IGradeRepository
    {
        SaveOut SaveGrade(Grade objgrade);
        SaveOut SaveRole(EntRolesBO objrole);
        List<Grade> GetGrade(int TenantID, int LocationID);
        SaveOut DeleteGrade(int GradeID,int TenantID,int LocationID);
    }
}