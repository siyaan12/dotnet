using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.ProjectManagement;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.ProjectManagement;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace MyDodos.Repository.ProjectManagement
{
    public class TimeSheetRepository : ITimeSheetRepository
    {
        private readonly IConfiguration _configuration;
        public TimeSheetRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public LeaveRequestModelMsg SaveTimeSheet(PPTimeSheetBO timeSheet)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@timeSheetId",timeSheet.TimeSheetID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@weekNo",timeSheet.WeekNo,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@yearId",timeSheet.YearID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@empId",timeSheet.EmpID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@comments",timeSheet.Comments,DbType.String,ParameterDirection.Input);
            dyParam.Add("@timeSheetStatus",timeSheet.TimeSheetStatus,DbType.String,ParameterDirection.Input);
            dyParam.Add("@submitedDate",timeSheet.SubmitedDate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@startDate",timeSheet.StartDate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@endDate",timeSheet.EndDate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@isLocked",timeSheet.IsLocked,DbType.Boolean,ParameterDirection.Input);
            dyParam.Add("@lockTime",timeSheet.Locktime,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@dueDate",timeSheet.DueDate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@isTSException",timeSheet.IsTSException,DbType.Boolean,ParameterDirection.Input);
            dyParam.Add("@isCurrException",timeSheet.IsCurrException,DbType.Boolean,ParameterDirection.Input);
            dyParam.Add("@exceptionCleardate",timeSheet.ExceptionCleardate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@tSHours",timeSheet.TSHours,DbType.Decimal,ParameterDirection.Input);
            dyParam.Add("@tSNonBillHours",timeSheet.TSNonBillHours,DbType.Decimal,ParameterDirection.Input);
            dyParam.Add("@releaseDate",timeSheet.ReleaseDate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@approvedBy",timeSheet.ApprovedBy,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@managerComment",timeSheet.ManagerComment,DbType.String,ParameterDirection.Input);
            dyParam.Add("@tenantId",timeSheet.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",timeSheet.LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@createdBy",timeSheet.CreatedBy,DbType.Int32,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SavePPTimeSheet";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<TaskResourceListModel> GetTimeSheetTasks(TimesheetInputBO task)
        {
            List<TaskResourceListModel> objtask = new List<TaskResourceListModel>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@estStartDate", task.StartDate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@empId", task.EmpID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@estEndDate", task.EndDate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@timeSheetId", task.TimeSheetID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetTimeSheetTaskModal";
                objtask = SqlMapper.Query<TaskResourceListModel>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objtask;
        }
        public LeaveRequestModelMsg SaveWeekTSNonBillable(PPWeekTSNonBillableBO timeSheet)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@timeSheetTaskId",timeSheet.TimeSheetTaskID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@pTaskId",timeSheet.PTaskID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@day1",timeSheet.Day1,DbType.Decimal,ParameterDirection.Input);
            dyParam.Add("@day2",timeSheet.Day2,DbType.Decimal,ParameterDirection.Input);
            dyParam.Add("@day3",timeSheet.Day3,DbType.Decimal,ParameterDirection.Input);
            dyParam.Add("@day4",timeSheet.Day4,DbType.Decimal,ParameterDirection.Input);
            dyParam.Add("@day5",timeSheet.Day5,DbType.Decimal,ParameterDirection.Input);
            dyParam.Add("@day6",timeSheet.Day6,DbType.Decimal,ParameterDirection.Input);
            dyParam.Add("@day7",timeSheet.Day7,DbType.Decimal,ParameterDirection.Input);
            dyParam.Add("@weekNo",timeSheet.WeekNo,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@yearId",timeSheet.YearID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@empId",timeSheet.EmpID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@timeSheetId",timeSheet.TimeSheetID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@timeSheetNBStatus",timeSheet.TimeSheetNBStatus,DbType.String,ParameterDirection.Input);
            dyParam.Add("@comments",timeSheet.Comments,DbType.String,ParameterDirection.Input);
            dyParam.Add("@tenantId",timeSheet.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",timeSheet.LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@createdBy",timeSheet.CreatedBy,DbType.Int32,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveWeekTSNonBillable";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }       
        public List<PPTimeSheetBO> GetTimeSheetList(TimesheetInputBO list)
        {
            List<PPTimeSheetBO> objtask = new List<PPTimeSheetBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", list.EmpID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@yearId", list.YearID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@timeSheetStatus", list.TimeSheetStatus,DbType.String,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetTimeSheetList";
                objtask = SqlMapper.Query<PPTimeSheetBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objtask;
        }
        public LeaveRequestModelMsg SaveWeekTimeSheet(PPWeekTimeSheetBO timeSheet)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@timeSheetTaskId",timeSheet.TimeSheetTaskID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@pTaskId",timeSheet.PTaskID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@day1",timeSheet.Day1,DbType.Decimal,ParameterDirection.Input);
            dyParam.Add("@day2",timeSheet.Day2,DbType.Decimal,ParameterDirection.Input);
            dyParam.Add("@day3",timeSheet.Day3,DbType.Decimal,ParameterDirection.Input);
            dyParam.Add("@day4",timeSheet.Day4,DbType.Decimal,ParameterDirection.Input);
            dyParam.Add("@day5",timeSheet.Day5,DbType.Decimal,ParameterDirection.Input);
            dyParam.Add("@day6",timeSheet.Day6,DbType.Decimal,ParameterDirection.Input);
            dyParam.Add("@day7",timeSheet.Day7,DbType.Decimal,ParameterDirection.Input);
            dyParam.Add("@weekNo",timeSheet.WeekNo,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@yearId",timeSheet.YearID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@empId",timeSheet.EmpID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@timeSheetId",timeSheet.TimeSheetID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@wtimeSheetStatus",timeSheet.WTimeSheetStatus,DbType.String,ParameterDirection.Input);
            dyParam.Add("@comments",timeSheet.Comments,DbType.String,ParameterDirection.Input);
            dyParam.Add("@tenantId",timeSheet.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",timeSheet.LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@createdBy",timeSheet.CreatedBy,DbType.Int32,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SavePPWeekTimeSheet";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public LeaveRequestModelMsg SaveMasterWeekTSNonBillable(PPWeekTSNonBillableBO timeSheet)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@timeSheetId",timeSheet.TimeSheetID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@empId",timeSheet.EmpID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@yearId",timeSheet.YearID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@timeSheetNBStatus",timeSheet.TimeSheetNBStatus,DbType.String,ParameterDirection.Input);
            dyParam.Add("@estStartDate",timeSheet.EstStartDate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@tenantId",timeSheet.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",timeSheet.LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@createdBy",timeSheet.CreatedBy,DbType.Int32,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveMasterTSNonBillable";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public PPTimeSheetBO GetTimeSheetDataList(TimesheetInputBO list)
        {
            PPTimeSheetBO objtask = new PPTimeSheetBO();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@startDate",list.StartDate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@endDate",list.EndDate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@empId",list.EmpID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@timeSheetId",list.TimeSheetID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetTimeSheetData";
                objtask = SqlMapper.Query<PPTimeSheetBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return objtask;
        }
        public List<PPWeekTimeSheetBO> GetTimeSheetBillableData(int EmpID,int TimeSheetID)
        {
            List<PPWeekTimeSheetBO> objtask = new List<PPWeekTimeSheetBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId",EmpID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@timeSheetId",TimeSheetID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetTimeSheetBillableData";
                objtask = SqlMapper.Query<PPWeekTimeSheetBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objtask;
        }
        public List<PPWeekTSNonBillableBO> GetTimeSheetNonBillableData(int EmpID,int TimeSheetID)
        {
            List<PPWeekTSNonBillableBO> objtask = new List<PPWeekTSNonBillableBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId",EmpID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@timeSheetId",TimeSheetID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetTimeSheetNonBillableData";
                objtask = SqlMapper.Query<PPWeekTSNonBillableBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objtask;
        }
        public GetTimeSheetList GetTimeSheetData(GetTimeSheetList inputParam)
        {
            GetTimeSheetList objtimesheet = new GetTimeSheetList();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@managerId", inputParam.objTimeSheetInput.ManagerID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@yearId", inputParam.objTimeSheetInput.YearID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@empId", inputParam.objTimeSheetInput.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@startDate", inputParam.objTimeSheetInput.StartDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@endDate", inputParam.objTimeSheetInput.EndDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@timeSheetStatus", inputParam.objTimeSheetInput.TimeSheetStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@tenantId", inputParam.objTimeSheetInput.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", inputParam.objTimeSheetInput.LocationID, DbType.Int32, ParameterDirection.Input);
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
                var query = "sp_GetTimeSheetExceptionDataSearch";
                var data= SqlMapper.Query<TimeSheetException>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                objtimesheet.objTimeSheetList = data;
                objtimesheet.objTimeSheetInput = inputParam.objTimeSheetInput;
                objtimesheet.ServerSearchables = inputParam.ServerSearchables;
                if(objtimesheet.objTimeSheetList.Count>0)
                {
                    objtimesheet.ServerSearchables.RecordsTotal = objtimesheet.objTimeSheetList[0].TotalCount;
                    objtimesheet.ServerSearchables.RecordsFiltered = objtimesheet.objTimeSheetList.Count();
                }
            }
            conn.Close();
            conn.Dispose();
            return objtimesheet;
        }
        public TSBillNonBillHoursBO GetTSBillNonBillHours(TimeSheetException timeSheet)
        {
            TSBillNonBillHoursBO rtnval = new TSBillNonBillHoursBO();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",timeSheet.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@yearId",timeSheet.YearID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@empId",timeSheet.EmpID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@timeSheetStatus",timeSheet.TimeSheetStatus,DbType.String,ParameterDirection.Input);
            dyParam.Add("@weekNo",timeSheet.WeekNo,DbType.Int32,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetTSBillableNonBillableHours";
                rtnval = SqlMapper.Query<TSBillNonBillHoursBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public int GetWeekcount()
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }
        public List<WeekDateRange> GetWeekDateRange(int TenantID,int LocationID,DateTime AttendanceDate)
        {
            List<WeekDateRange> objrange = new List<WeekDateRange>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@attendanceDate",AttendanceDate,DbType.DateTime,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetWeekDateRange";
                objrange = SqlMapper.Query<WeekDateRange>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objrange;
        }
        public LeaveRequestModelMsg SaveTimeSheetTaskApply(PPWeekTimeSheetBO timeSheet)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@timeSheetId",timeSheet.TimeSheetID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@weekNo",timeSheet.WeekNo,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@yearId",timeSheet.YearID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@empId",timeSheet.EmpID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@wTimeSheetStatus",timeSheet.WTimeSheetStatus,DbType.String,ParameterDirection.Input);
            dyParam.Add("@pTaskId",timeSheet.PTaskID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@tenantId",timeSheet.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",timeSheet.LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@createdBy",timeSheet.CreatedBy,DbType.Int32,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveApplyTimeSheet";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public LeaveRequestModelMsg SaveTimesheetOverAll(vwStatusWeekTimeSheetBO timeSheet)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@timeSheetId",timeSheet.TimeSheetID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@yearId",timeSheet.YearID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@empId",timeSheet.EmpID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@timeSheetStatus",timeSheet.TimeSheetStatus,DbType.String,ParameterDirection.Input);
            dyParam.Add("@pTaskId",timeSheet.PTaskID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@tenantId",timeSheet.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",timeSheet.LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@createdBy",timeSheet.CreatedBy,DbType.Int32,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveTimesheetOverAll";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<PPTimeSheetBO> SaveTimeSheetFlagged(TimesheetInputBO timeSheet)
        {
            List<PPTimeSheetBO> rtnval = new List<PPTimeSheetBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@timeSheetId",timeSheet.TimeSheetID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@yearId",timeSheet.YearID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@empId",timeSheet.EmpID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@timeSheetStatus",timeSheet.TimeSheetStatus,DbType.String,ParameterDirection.Input);
            dyParam.Add("@tenantId",timeSheet.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",timeSheet.LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@gmtdateTime",timeSheet.GetDateTime,DbType.DateTime,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveTimeSheetflagged";
                rtnval = SqlMapper.Query<PPTimeSheetBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public TimeSheetEmpReportList GetTimeSheetEmpReportData(TimeSheetEmpReportList inputParam)
        {
            TimeSheetEmpReportList objtimesheet = new TimeSheetEmpReportList();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@managerId", inputParam.objTSEmpReportInput.ManagerID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@empId", inputParam.objTSEmpReportInput.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@firstName", inputParam.objTSEmpReportInput.FirstName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@tenantId", inputParam.objTSEmpReportInput.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", inputParam.objTSEmpReportInput.LocationID, DbType.Int32, ParameterDirection.Input);
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
                var query = "sp_GetTimeSheetEmpReportData";
                var data= SqlMapper.Query<TSExceptionEmpReport>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                objtimesheet.objTSEmpReportList = data;
                objtimesheet.objTSEmpReportInput = inputParam.objTSEmpReportInput;
                objtimesheet.ServerSearchables = inputParam.ServerSearchables;
                if(objtimesheet.objTSEmpReportList.Count>0)
                {
                    objtimesheet.ServerSearchables.RecordsTotal = objtimesheet.objTSEmpReportList[0].TotalCount;
                    objtimesheet.ServerSearchables.RecordsFiltered = objtimesheet.objTSEmpReportList.Count();
                }
            }
            conn.Close();
            conn.Dispose();
            return objtimesheet;
        }
        public TSExcReportResultList GetTSExcReportResult(TSExcReportResultList inputParam)
        {
            TSExcReportResultList objtimesheet = new TSExcReportResultList();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@projectId", inputParam.objTSReportResultInput.ProjectID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@empId", inputParam.objTSReportResultInput.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@startDate", inputParam.objTSReportResultInput.StartDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@endDate", inputParam.objTSReportResultInput.EndDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@tenantId", inputParam.objTSReportResultInput.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", inputParam.objTSReportResultInput.LocationID, DbType.Int32, ParameterDirection.Input);
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
                var query = "sp_GetTimeSheetReportResultData";
                var data= SqlMapper.Query<TSExcReportResult>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                objtimesheet.objTSReportResultList = data;
                objtimesheet.objTSReportResultInput = inputParam.objTSReportResultInput;
                objtimesheet.ServerSearchables = inputParam.ServerSearchables;
                if(objtimesheet.objTSReportResultList.Count>0)
                {
                    objtimesheet.ServerSearchables.RecordsTotal = objtimesheet.objTSReportResultList[0].TotalCount;
                    objtimesheet.ServerSearchables.RecordsFiltered = objtimesheet.objTSReportResultList.Count();
                }
            }
            conn.Close();
            conn.Dispose();
            return objtimesheet;
        }
        public List<DayWeekMonthRange> GetDayWeekMonthRange(int TenantID,int LocationID,int EmpID,DateTime AttendanceDate)
        {
            List<DayWeekMonthRange> objrange = new List<DayWeekMonthRange>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@empId",EmpID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@attendanceDate",AttendanceDate,DbType.DateTime,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetDayWeekMonthRange";
                objrange = SqlMapper.Query<DayWeekMonthRange>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objrange;
        }
        public List<TimeSheetWeek> GetWeekDropdown()
        {
            var d = DateTime.Now.Date;
            CultureInfo cInfo = CultureInfo.CurrentCulture;
            List<TimeSheetWeek> rtn = new List<TimeSheetWeek> ();
            int WkNo = cInfo.Calendar.GetWeekOfYear(d, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
            for (int idx = 1; idx <= WkNo; idx++) {
                var dt = (GetWeekDateSpan (idx, DateTime.Now.Year)); 
                var obj = (GetWeekDates (idx, DateTime.Now.Year)); 
                {
                    rtn.Add (new TimeSheetWeek { Week = dt, WeekNo = idx, WeekStart = obj.WeekStart,WeekEnd = obj.WeekEnd });
                }
            }
            var newList = rtn.OrderByDescending(x => x.WeekNo).ToList();
            return newList;
        }
        public string GetWeekDateSpan (int WeekNo, int dtYear) 
        {
            DateTime dt1 = FirstDateOfWeek (dtYear, WeekNo, CultureInfo.CurrentCulture);
            DateTime dt2 = dt1.AddDays (6);
            return dt1.ToString ("MMM dd, yyyy") + " - " + dt2.ToString ("MMM dd, yyyy");
        }
        public DateTime FirstDateOfWeek (int year, int weekOfYear, System.Globalization.CultureInfo ci)
         {
            DateTime jan1 = new DateTime (year, 1, 1);
            int daysOffset = (int) ci.DateTimeFormat.FirstDayOfWeek - (int) jan1.DayOfWeek;
            DateTime firstWeekDay = jan1.AddDays (daysOffset);
            int firstWeek = ci.Calendar.GetWeekOfYear (jan1, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
            if ((firstWeek <= 1 || firstWeek >= 52) && daysOffset >= -3) {
                weekOfYear -= 1;
            }
            return firstWeekDay.AddDays (weekOfYear * 7);
        }
        public TimeSheetWeek GetWeekDates (int WeekNo, int dtYear) 
        {
            TimeSheetWeek objtime = new TimeSheetWeek();
            DateTime dt1 = FirstDateOfWeek (dtYear, WeekNo, CultureInfo.CurrentCulture);
            DateTime dt2 = dt1.AddDays (6);
            objtime.WeekStart = dt1;
            objtime.WeekEnd = dt2;
            return objtime;
        }
        public List<TimeSheetException> GetConsoleTimesheet(int TenantID,int LocationID)
        {
            List<TimeSheetException> objrange = new List<TimeSheetException>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetConsoleTimesheet";
                objrange = SqlMapper.Query<TimeSheetException>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objrange;
        }
        public LeaveRequestModelMsg UpdateTimeSheetFlagRelease(PPWeekTSNonBillableBO timeSheet)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@timeSheetId",timeSheet.TimeSheetID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@tstatus",timeSheet.TimeSheetNBStatus,DbType.String,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_UpdateTimeSheetFlagRelease";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<PPWeekTSNonBillableBO> GetTimeSheetTasksNBData(PPWeekTSNonBillableBO nbtask)
        {
            List<PPWeekTSNonBillableBO> objtask = new List<PPWeekTSNonBillableBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId",nbtask.EmpID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@yearId",nbtask.YearID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@weekno",nbtask.WeekNo,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@tenantId",nbtask.TenantID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_TimeSheetTasksNB";
                objtask = SqlMapper.Query<PPWeekTSNonBillableBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objtask;
        }
        public List<TimeSheetSummaryBO> GetTimeSheetSummary(int TenantID,int LocationID,int EmpID,int ManagerID)
        {
            List<TimeSheetSummaryBO> rtnval = new List<TimeSheetSummaryBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@locationId",LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@empId",EmpID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@managerId",ManagerID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetTimeSheetSummary";
                rtnval = SqlMapper.Query<TimeSheetSummaryBO>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public ProjectPaymentStatus GetTimeSheetPaidStatus(int TenantID,int YearID,int WeekNo,int EmpID)
        {
            ProjectPaymentStatus objtask = new ProjectPaymentStatus();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId",EmpID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@yearId",YearID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@weekno",WeekNo,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@tenantId",TenantID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetTimeSheetPaidStatus";
                objtask = SqlMapper.Query<ProjectPaymentStatus>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return objtask;
        }
        public LeaveRequestModelMsg UpdateTimeSheetPaidStatus(UpdateTimeSheetPaidStatusBO timeSheet)
        {
            LeaveRequestModelMsg rtnval = new LeaveRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@timeSheetId",timeSheet.TimeSheetID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@timeSheetStatus",timeSheet.TimeSheetStatus,DbType.String,ParameterDirection.Input);
            dyParam.Add("@tenantId",timeSheet.TenantID,DbType.Int32,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_UpdateTimeSheetPaidStatus";
                rtnval = SqlMapper.Query<LeaveRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
    }
}