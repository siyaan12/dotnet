using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyDodos.Domain.Dashboard;

namespace MyDodos.Repository.Dashboard
{
    public interface IDashboardRepository
    {
        DashboardCountBO GetDashboardCount(DashboardInputBO objinp);
    }
}