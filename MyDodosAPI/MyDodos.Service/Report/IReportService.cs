using System.Collections.Generic;
using MyDodos.Domain.Report;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.Report;

namespace MyDodos.Service.Report
{
    public interface IReportService
    {
        Response<List<ReportTypesBO>> GetReportTypes(int TenantID, int LocationID);
        Response<ReportsDataSearch> GetReports(ReportsDataSearch objReportInput);
    }
}