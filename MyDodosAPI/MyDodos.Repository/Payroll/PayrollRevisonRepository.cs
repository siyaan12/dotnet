using System;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.ViewModel.Employee;
using MySql.Data.MySqlClient;

namespace MyDodos.Repository.Payroll
{
    public class PayrollRevisonRepository : IPayrollRevisonRepository
    {
        private readonly IConfiguration _configuration;
        public PayrollRevisonRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public GetHRDirectoryList GetPayrollEmployeeSearch(GetHRDirectoryList inputParam)
        {
            GetHRDirectoryList objLeave = new GetHRDirectoryList();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", inputParam.objHRDirectoryInput.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", inputParam.objHRDirectoryInput.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@yearId", inputParam.objHRDirectoryInput.YearID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@managerId", inputParam.objHRDirectoryInput.ManagerID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@search_data", inputParam.ServerSearchables.search_data, DbType.String, ParameterDirection.Input);
            dyParam.Add("@page_No", inputParam.ServerSearchables.page_No, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@page_Size", inputParam.ServerSearchables.page_Size, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@orderBy_Column", inputParam.ServerSearchables.orderBy_Column, DbType.String, ParameterDirection.Input);
            dyParam.Add("@order_By", inputParam.ServerSearchables.order_By, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPayrollEmployeeSearch";
                var data= SqlMapper.Query<GetHRDirectoryBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                objLeave.objHRDirectoryList = data;
                //objLeave.objMyLeaveListInput = inputParam.objMyLeaveListInput;
                objLeave.ServerSearchables = inputParam.ServerSearchables;
                if(objLeave.objHRDirectoryList.Count>0)
                {
                    objLeave.ServerSearchables.RecordsTotal = objLeave.objHRDirectoryList[0].TotalCount;
                    objLeave.ServerSearchables.RecordsFiltered = objLeave.objHRDirectoryList.Count();
                }
            }
            conn.Close();
            conn.Dispose();
            return objLeave;
        }        
    }
}