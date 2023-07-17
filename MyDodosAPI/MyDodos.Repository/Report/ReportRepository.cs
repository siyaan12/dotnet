using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Report;
using MyDodos.ViewModel.Report;
using MySql.Data.MySqlClient;

namespace MyDodos.Repository.Report
{
    public class ReportRepository : IReportRepository
    {
        private readonly IConfiguration _configuration;
        public ReportRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSREPORTDBConnection"));
            return conn;
        }
        public List<ReportTypeBO> GetReportTypes(int TenantID, int LocationID)
        {
            List<ReportTypeBO> output = new List<ReportTypeBO>();
            DynamicParameters dyParam = new DynamicParameters();
            
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetDynReportTypes";
                output = SqlMapper.Query<ReportTypeBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public ReportsDataSearch GetReports(ReportsDataSearch objReportInput)
        {
            ReportsDataSearch result = new ReportsDataSearch();
            List<ReportDetailsBO> obj = new List<ReportDetailsBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@reportTypeId", objReportInput.InputParms.ReportTypeID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@reportId", objReportInput.InputParms.ReportID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objReportInput.InputParms.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objReportInput.InputParms.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@fromDate", objReportInput.InputParms.FromDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@toDate", objReportInput.InputParms.ToDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@salaryPeriodId", objReportInput.InputParms.SalaryPeriodID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@pageNo", objReportInput.ServerSearchables.page_No, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@pageSize", objReportInput.ServerSearchables.page_Size, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@orderByColumn", objReportInput.ServerSearchables.orderBy_Column, DbType.String, ParameterDirection.Input);
            dyParam.Add("@orderBy", objReportInput.ServerSearchables.order_By, DbType.String, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetReportDetails";
                obj = SqlMapper.Query<ReportDetailsBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                result.InputParms = objReportInput.InputParms;
                result.sheetData = obj;
                result.ServerSearchables = objReportInput.ServerSearchables;
                if (result.sheetData.Count > 0)
                {
                    result.ServerSearchables.RecordsTotal = result.sheetData[0].TotalCount;
                }
            }
            conn.Close();
            conn.Dispose();
            return result;
        }
        public MasReportBO GetReportFields(ReportInputBO objReportInput)
        {
            MasReportBO obj = new MasReportBO();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@reportTypeId", objReportInput.ReportTypeID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@reportId", objReportInput.ReportID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objReportInput.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objReportInput.LocationID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetReportFields";
                obj = SqlMapper.Query<MasReportBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return obj;
        }
    }
}