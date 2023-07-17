using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyDodos.Domain.Report;
using MyDodos.ViewModel.Report;

namespace MyDodos.Repository.Report
{
    public interface IReportRepository
    {
        List<ReportTypeBO> GetReportTypes(int TenantID, int LocationID);
        ReportsDataSearch GetReports(ReportsDataSearch objReportInput);
        MasReportBO GetReportFields(ReportInputBO objReportInput);
    }
}