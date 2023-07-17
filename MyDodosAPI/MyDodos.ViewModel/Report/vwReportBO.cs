using MyDodos.Domain.Report;
using MyDodos.ViewModel.ServerSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDodos.ViewModel.Report
{
    public class ReportsDataSearch
    {
        public ReportInputBO InputParms { get; set; }
        public ServerSearchable ServerSearchables { get; set; }
        public List<MapValues> MapValues { get; set;}
        public List<ReportDetailsBO> sheetData { get; set; }
    }
}