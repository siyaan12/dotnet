using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Holiday;
using MyDodos.Domain.LeaveBO;
using MySql.Data.MySqlClient;
namespace MyDodos.Repository.Holiday
{
    public class HolidayRepository : IHolidayRepository
    {
        private readonly IConfiguration _configuration;
        public HolidayRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public List<HolidayBO> GetHolidayList(int TeantID, int YearID)
        {
            List<HolidayBO> obj = new List<HolidayBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Tenant_ID", TeantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Year_ID", YearID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetHolidayList";
                obj = SqlMapper.Query<HolidayBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return obj;
        }
        public List<HolidayBO> GetEmployeeHoliday(int EmpID, int YearID)
        {
            List<HolidayBO> obj = new List<HolidayBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Emp_ID", EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Year_ID", YearID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEmployeeHoliday";
                obj = SqlMapper.Query<HolidayBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return obj;
        }
        public int SaveEmployeeHoliday(EmployeeHolidayBO _holiday)
        {
            int obj = 0;
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Emp_ID", _holiday.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Year_ID", _holiday.YearID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@H_Status", _holiday.HStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Year_Name", _holiday.YearName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Holiday_optinal", _holiday.Holidayoptinal, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Created_By", _holiday.CreatedBy, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_HRSaveEmployeeHoliday";
                obj = SqlMapper.Query<int>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return obj;
        }
        public List<MasYearBO> GetYearDetails(int YearID ,int TeantID, int LocationID)
        {
            List<MasYearBO> rtnval = new List<MasYearBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Year_ID",YearID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Tenant_ID",TeantID, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Location_ID",LocationID, DbType.String, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetYearDetails";
                rtnval = SqlMapper.Query<MasYearBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();            
            return rtnval;
        }
        public LeaveRequestModelMsg SaveMasYear(MasYear year)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Tenant_ID",year.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@Location_ID",year.LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@Year_ID",year.YearID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@Optional_Holiday",year.OptionalHoliday,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@Is_Employee",year.IsEmployee,DbType.Boolean,ParameterDirection.Input);
            dyParam.Add("@Due_Date",year.DueDate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@Is_Notify",year.IsNotify,DbType.Boolean,ParameterDirection.Input);
            dyParam.Add("@Notify_Content",year.NotifyContent,DbType.String,ParameterDirection.Input);
            dyParam.Add("@year_Status",year.YearStatus,DbType.String,ParameterDirection.Input);
            dyParam.Add("@Created_By",year.CreatedBy,DbType.Int32,ParameterDirection.Input);


            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveYear";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
    }
}