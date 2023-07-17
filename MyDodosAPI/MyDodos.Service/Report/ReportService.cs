using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Report;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.Report;
using MyDodos.ViewModel.Report;

namespace MyDodos.Service.Report
{
    public class ReportService : IReportService
    {
        private readonly IConfiguration _configuration;
        private readonly IReportRepository _reportRepository;
        public ReportService(IConfiguration configuration, IReportRepository reportRepository)
        {
            _configuration = configuration;
            _reportRepository = reportRepository;
        }
        public Response<List<ReportTypesBO>> GetReportTypes(int TenantID, int LocationID)
        {
            Response<List<ReportTypesBO>> response;
            ReportTypesBO objrep = new ReportTypesBO();
            List<ReportTypesBO> objtype = new List<ReportTypesBO>();
            DynReportsBO objdynreport = new DynReportsBO();
            List<DynReportsBO> objdynrepo = new List<DynReportsBO>();
            try
            {
                var result = _reportRepository.GetReportTypes(TenantID, LocationID);
                if (result.Count == 0)
                {
                    response = new Response<List<ReportTypesBO>>(objtype, 200, "Data Not Found");
                }
                else
                {
                    List<int> reporttypeIDs = result.Select(s => s.ReportTypeID).Distinct().ToList();

                    foreach (var item in reporttypeIDs)
                    {
                        List<int> reportIDs = result.Where(s => s.ReportTypeID == item).Select(s => s.ReportID).Distinct().ToList();
                        objrep.ReportTypeID = item;
                        objrep.ReportTypeName = result.Where(s => s.ReportTypeID == item).Select(s => s.ReportTypeName).FirstOrDefault();
                        objrep.ReportTypeDescription = result.Where(s => s.ReportTypeID == item).Select(s => s.ReportDescription).FirstOrDefault();
                        objrep.TenantID = TenantID;
                        objrep.LocationID = LocationID;

                        foreach (var items in reportIDs)
                        {
                            objdynreport.ReportID = items;
                            objdynreport.ReportName = result.Where(s => s.ReportID == items).Select(s => s.ReportName).FirstOrDefault();
                            objdynreport.ReportDescription = result.Where(s => s.ReportID == items).Select(s => s.ReportDescription).FirstOrDefault();
                            objdynrepo.Add(objdynreport);
                            objdynreport = new DynReportsBO();
                        }
                        objrep.objreport = new List<DynReportsBO>(objdynrepo);
                        objdynrepo.Clear();
                        objtype.Add(objrep);
                        objrep = new ReportTypesBO();
                    }
                    response = new Response<List<ReportTypesBO>>(objtype, 200, "Data Retrived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<ReportTypesBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<ReportsDataSearch> GetReports(ReportsDataSearch objReportInput)
        {
            Response<ReportsDataSearch> response;
            ReportsDataSearch result = new ReportsDataSearch();
            List<MapValues> mapValues = new List<MapValues>(); 
            try
            {
                result = _reportRepository.GetReports(objReportInput);
                var fields = _reportRepository.GetReportFields(objReportInput.InputParms);
                
                var clmHeader = this.SplitData(fields.DisplayColumns);
                var fieldname = this.SplitData(fields.ColumnName);
                List<string> dfltField = fields.DefaultFieldNames != null ? fields.DefaultFieldNames.Split(",").ToList() : null;

                foreach (var item in clmHeader)
                {
                    mapValues.Add(new MapValues{
                        MapID = item.MapID,
                        FieldName = item.FieldName,
                        MapValue = fieldname.Count() != 0 ? fieldname.Where(x=> x.MapID == item.MapID).Select(x => string.Concat(x.FieldName.Substring(0,1).ToLower(), x.FieldName.Substring(1))).FirstOrDefault() : "",
                        IsDefaultField = dfltField != null ? dfltField.Contains(fieldname.Where(x=> x.MapID == item.MapID).Select(x => x.FieldName).FirstOrDefault()) : false
                    });
                }
                result.MapValues = mapValues;

                response = new Response<ReportsDataSearch>(result, 200);
            }
            catch (Exception ex)
            {
                response = new Response<ReportsDataSearch>(ex.Message, 500);
            }
            return response;
        }

        private List<MapValues> SplitData(string Values)
        {
            List<MapValues> mapValues = new List<MapValues>();
            int i = 1;
            if (!string.IsNullOrEmpty(Values) && !string.IsNullOrWhiteSpace(Values))
            {
                var lstData = Values.Split(',').ToList();

                foreach (var item in lstData)
                {
                    mapValues.Add(new MapValues
                    {
                        MapID = i++,
                        FieldName = item
                    });
                }
            }
            return mapValues;
        }
    }
}