using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDodos.Domain.AuthBO;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.Common;
using MyDodos.ViewModel.Entitlement;

namespace MyDodos.Repository.Entitlement
{
    public interface IEntitlementRepository
    {
        SaveOut SaveEntAppUserData(AppUser _objEntAppUser);
        SaveOut SaveEntEmployee(AppUser _objEntAppUser);        
    }
}