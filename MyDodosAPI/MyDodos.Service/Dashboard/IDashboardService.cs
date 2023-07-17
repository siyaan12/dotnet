using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyDodos.Domain.Dashboard;
using MyDodos.Domain.Wrapper;

namespace MyDodos.Service.Dashboard
{
    public interface IDashboardService
    {
        Response<DashboardCountBO> GetDashboardCount(DashboardInputBO objinp);
    }
}