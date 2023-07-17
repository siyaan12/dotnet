using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Master
{
    public class MasterDA
    {
        public string _connectionString { get; set; }
        public DBType _dBType { get; set; }
        public MasterDA(string connectionString, DBType dBType)
        {
            _connectionString = connectionString;
            _dBType = dBType;
        }
        public IDbConnection GetConnection(string connectionString, DBType dBType)
        {
            if (Convert.ToString(dBType) == "SQL")
            {
                var conn = new SqlConnection(_connectionString);
                return conn;
            }
            else if (Convert.ToString(dBType) == "MYSQL")
            {
                var conn = new MySqlConnection(_connectionString);
                return conn;
            }
            else
            {
                return null;
            }
        }
        public List<TenantProfileBO> GetTenantDetails(MasterInputBO master)
        {
            List<TenantProfileBO> objrange = new List<TenantProfileBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Product_ID", master.ProductID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Entity_Name", master.EntityName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Tenant_ID", master.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", master.LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection(_connectionString, _dBType);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetMasterDetails";
                objrange = SqlMapper.Query<TenantProfileBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objrange;
        }
        public List<LoginLocationBO> GetLocationDetails(MasterInputBO master)
        {
            List<LoginLocationBO> objrange = new List<LoginLocationBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Product_ID", master.ProductID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Entity_Name", master.EntityName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Tenant_ID", master.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", master.LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection(_connectionString, _dBType);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetMasterDetails";
                objrange = SqlMapper.Query<LoginLocationBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objrange;
        }
        public List<LoginYearBO> GetYearDetails(MasterInputBO master)
        {
            List<LoginYearBO> objrange = new List<LoginYearBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Product_ID", master.ProductID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Entity_Name", master.EntityName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Tenant_ID", master.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", master.LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection(_connectionString, _dBType);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetMasterDetails";
                objrange = SqlMapper.Query<LoginYearBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objrange;
        }
        public int SaveMasYear(MasYear year)
        {
            int rtnval = 0;
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Tenant_ID", year.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", year.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Year_ID", year.YearID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Optional_Holiday", year.OptionalHoliday, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Is_Employee", year.IsEmployee, DbType.Boolean, ParameterDirection.Input);
            dyParam.Add("@Due_Date", year.DueDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@Is_Notify", year.IsNotify, DbType.Boolean, ParameterDirection.Input);
            dyParam.Add("@Notify_Content", year.NotifyContent, DbType.String, ParameterDirection.Input);
            dyParam.Add("@year_Status", year.YearStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Created_By", year.CreatedBy, DbType.Int32, ParameterDirection.Input);


            var conn = this.GetConnection(_connectionString, _dBType);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveYear";
                rtnval = SqlMapper.Query<int>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<DeviceMasterBO> GetDeviceID(int TenantID, int EntityID)
        {
            List<DeviceMasterBO> result = new List<DeviceMasterBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@entityId", EntityID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection(_connectionString, _dBType);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetDeviceID";
                result = SqlMapper.Query<DeviceMasterBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return result;
        }
        public string UpdateDeviceExpiryDate(int DeviceID)
        {
            string result = string.Empty;
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@deviceId", DeviceID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection(_connectionString, _dBType);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_UpdateInstruDeviceMasterExpDate";
                result = SqlMapper.Query<string>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return result;
        }
        public List<EntSubscribeProdBO> GetProduct()
        {
            List<EntSubscribeProdBO> objsub = new List<EntSubscribeProdBO>();
            var conn = this.GetConnection(_connectionString, _dBType);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "SELECT * FROM tblEntSubscribedProd where SubscriptionStatus = 'Active'";
                objsub = SqlMapper.Query<EntSubscribeProdBO>(conn, query).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objsub;
        }
        public List<EntSubscribeProdBO> GetProductDetails(int KProductID)
        {
            List<EntSubscribeProdBO> objsub = new List<EntSubscribeProdBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@productId", KProductID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection(_connectionString, _dBType);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetProductDetails";
                objsub = SqlMapper.Query<EntSubscribeProdBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objsub;
        }
        public List<LocationBO> GetLocation(int TenantID)
        {
            List<LocationBO> objsub = new List<LocationBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection(_connectionString, _dBType);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetLocationDetails";
                objsub = SqlMapper.Query<LocationBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objsub;
        }
        public List<YearBO> GetYear(int TenantID,int LocationID)
        {
            List<YearBO> objsub = new List<YearBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection(_connectionString, _dBType);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetYear";
                objsub = SqlMapper.Query<YearBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objsub;
        }
        public void SaveLeaveReport (int TenantID,int LocationID)
        {
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection(_connectionString, _dBType);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State ==  ConnectionState.Open)
            {
                var query = "sp_SaveLeaveRequestReport";
                var objleave = SqlMapper.Query<LeaveReportBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
        }
        public List<TimesheetBO> GetTimesheet(int TenantID,int YearID)
        {
            List<TimesheetBO> objsub = new List<TimesheetBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@yearId", YearID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection(_connectionString, _dBType);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetTimeSheetBillableReports";
                objsub = SqlMapper.Query<TimesheetBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objsub;
        }
        public void SaveHRSalaryPeriod (int TenantID,int LocationID)
        {
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection(_connectionString, _dBType);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State ==  ConnectionState.Open)
            {
                var query = "sp_SaveHRSalaryPeriod";
                var objleave = SqlMapper.Query<SalaryPeriodBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
        }
        public void SaveTimesheetReport (TimesheetBO objtime)
        {
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@timesheetId", objtime.TimesheetID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@weekNo", objtime.WeekNo,DbType.String,ParameterDirection.Input);
            dyParam.Add("@projectId", objtime.ProjectID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@projectName", objtime.ProjectName,DbType.String,ParameterDirection.Input);
            dyParam.Add("@resourceName", objtime.ResourceName,DbType.String,ParameterDirection.Input);
            dyParam.Add("@hoursReported", objtime.HoursReported,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@billedHours", objtime.BilledHours,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@symbol", objtime.Symbol,DbType.String,ParameterDirection.Input);
            dyParam.Add("@rate", objtime.Rate,DbType.String,ParameterDirection.Input);
            dyParam.Add("@billedAmount", objtime.BilledAmount,DbType.String,ParameterDirection.Input);
            dyParam.Add("@paidAmount", objtime.PaidAmount,DbType.String,ParameterDirection.Input);
            dyParam.Add("@balanceAmount", objtime.BalanceAmount,DbType.String,ParameterDirection.Input);
            dyParam.Add("@assignStart", objtime.AssignStart,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@assignEnd", objtime.AssignEnd,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@tenantId", objtime.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId", objtime.LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@createdBy", objtime.CreatedBy,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@createdOn", objtime.CreatedOn,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@modifiedBy", objtime.ModifiedBy,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@modifiedOn", objtime.ModifiedOn,DbType.DateTime,ParameterDirection.Input);

            var conn = this.GetConnection(_connectionString, _dBType);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State ==  ConnectionState.Open)
            {
                var query = "sp_SaveTimesheetReport";
                var objleave = SqlMapper.Query<TimesheetBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
        }
        public List<SalaryPeriodBO> GetSalaryPeriod(int TenantID,int LocationID, int YearID)
        {
            List<SalaryPeriodBO> objsub = new List<SalaryPeriodBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@yearId", YearID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection(_connectionString, _dBType);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetSalaryPeriod";
                objsub = SqlMapper.Query<SalaryPeriodBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objsub;
        }
    }
}
