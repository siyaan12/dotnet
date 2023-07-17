using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Dashboard;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.Dashboard;

namespace MyDodos.Service.Dashboard
{
    public class DashboardService : IDashboardService
    {
        private readonly IConfiguration _configuration;
        private readonly IDashboardRepository _dashboardRepository;
        public DashboardService(IConfiguration configuration, IDashboardRepository dashboardRepository)
        {
            _configuration = configuration;
            _dashboardRepository = dashboardRepository;
        }
        public Response<DashboardCountBO> GetDashboardCount(DashboardInputBO objinp)
        {
            Response<DashboardCountBO> response;
            try
            {
                var result = _dashboardRepository.GetDashboardCount(objinp);
                if (result == null)
                {
                    response = new Response<DashboardCountBO>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<DashboardCountBO>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<DashboardCountBO>(ex.Message, 500);
            }
            return response;
        }
    }
}