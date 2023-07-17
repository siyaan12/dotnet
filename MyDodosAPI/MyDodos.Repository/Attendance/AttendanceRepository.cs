using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Attendance;
using MyDodos.Domain.Wrapper;
using MySql.Data.MySqlClient;
using MyDodos.ViewModel.Common;
using MyDodos.Domain.Employee;
using MyDodos.ViewModel.Attendance;

namespace MyDodos.Repository.Attendance
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly IConfiguration _configuration;
        public AttendanceRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public SaveOut SaveAttendanceUserLogData(MachineInfo attend)
        {
            SaveOut objRoster = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@machineId", attend.MachineID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@machineNumber", attend.MachineNumber, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@indRegId", attend.IndRegID, DbType.Int64, ParameterDirection.Input);
            dyParam.Add("@dateTimeRecord", attend.DateTimeRecord, DbType.String, ParameterDirection.Input);
            dyParam.Add("@mode", attend.Mode, DbType.String, ParameterDirection.Input);
            dyParam.Add("@dateOnlyRecord", attend.DateOnlyRecord, DbType.String, ParameterDirection.Input);
            dyParam.Add("@timeOnlyRecord", attend.TimeOnlyRecord, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveAttendanceEpushData";
                objRoster = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return objRoster;
        }
        public EmployeeAttendanceBO SaveSwipeAttendanceData(MachineInfo attend,int EmpID)
        {
            EmployeeAttendanceBO objRoster = new EmployeeAttendanceBO();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@machineNumber", attend.MachineNumber, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@indRegId", attend.IndRegID, DbType.Int64, ParameterDirection.Input);
            dyParam.Add("@dateTimeRecord", attend.DateOnlyRecord, DbType.String, ParameterDirection.Input);
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveAttendanceEpushEmployee";
                objRoster = SqlMapper.Query<EmployeeAttendanceBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return objRoster;
        }
        public SaveOut SaveAttendanceRoster(EmployeeRosterBO objrost)
        {
            SaveOut objRoster = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@rosterId", objrost.RosterID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@shiftId", objrost.ShiftID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@empId", objrost.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objrost.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objrost.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@rosterDate", objrost.RosterDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@isRotated", objrost.IsRotated, DbType.Boolean, ParameterDirection.Input);
            dyParam.Add("@isLastShift", objrost.IsLastShift, DbType.Boolean, ParameterDirection.Input);
            dyParam.Add("@isEnable", objrost.IsEnable, DbType.Boolean, ParameterDirection.Input);
            dyParam.Add("@dayId", objrost.DayID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@rosterStatus", objrost.RosterStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@createdBy", objrost.CreatedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@modifiedBy", objrost.ModifiedBy, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveAttendanceRoster";
                objRoster = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return objRoster;
        }
        public SaveOut SaveEmployeeAttendance(EmployeeAttendanceBO objatt, int RosterID)
        {
            SaveOut objroster = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empAttendanceId", objatt.EmpAttendanceID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@empId", objatt.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@rosterId", RosterID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objatt.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objatt.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@attendanceDate", objatt.AttendanceDate, DbType.String, ParameterDirection.Input);
            dyParam.Add("@timeIn", objatt.TimeIn, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@timeOut", objatt.TimeOut, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@attendanceStatus", objatt.AttendanceStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@createdBy", objatt.CreatedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@modifiedBy", objatt.ModifiedBy, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveEmployeeAttendance";
                objroster = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return objroster;
        }
        public List<EmployeeRosterBO> GetEmpAttendanceRoster(EmpAttendanceInputBO objplan)
        {
            List<EmployeeRosterBO> objros = new List<EmployeeRosterBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", objplan.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objplan.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objplan.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@startDate", objplan.Startdate, DbType.String, ParameterDirection.Input);
            dyParam.Add("@endDate", objplan.EndDate, DbType.String, ParameterDirection.Input);
            dyParam.Add("@departmentId", objplan.DepartmentID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEmpAttendanceRoster";
                objros = SqlMapper.Query<EmployeeRosterBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objros;
        }
        public List<EmployeeAttendanceBO> GetEmpAttendance(int EmpID, DateTime RosterDate,int TenantID,int LocationID)
        {
            List<EmployeeAttendanceBO> objros = new List<EmployeeAttendanceBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@rosterDate", RosterDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEmpAttendanceManual";
                objros = SqlMapper.Query<EmployeeAttendanceBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objros;
        }
        // public List<EmployeeAttendanceBO> GetEmployeeAttendance(int EmpID, DateTime RosterDate,int TenantID,int LocationID)
        // {
        //     List<EmployeeAttendanceBO> objros = new List<EmployeeAttendanceBO>();
        //     DynamicParameters dyParam = new DynamicParameters();
        //     dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
        //     dyParam.Add("@rosterDate", RosterDate, DbType.DateTime, ParameterDirection.Input);
        //     dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
        //     dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
        //     var conn = this.GetConnection();
        //     if (conn.State == ConnectionState.Closed)
        //     {
        //         conn.Open();
        //     }
        //     if (conn.State == ConnectionState.Open)
        //     {
        //         var query = "sp_GetEmployeeAttendanceManual";
        //         objros = SqlMapper.Query<EmployeeAttendanceBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
        //     }
        //     conn.Close();
        //     conn.Dispose();
        //     return objros;
        // }
        public List<EmployeeRosterBO> GetAttendanceRoster(AttendanceInputBO _att)
        {
            List<EmployeeRosterBO> objRoster = new List<EmployeeRosterBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", _att.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@yearId", _att.YearID, DbType.Int32, ParameterDirection.Input);
            if (!string.IsNullOrEmpty(_att.StartDate))
                dyParam.Add("@startDate", _att.StartDate, DbType.String, ParameterDirection.Input);
            if (!string.IsNullOrEmpty(_att.EndDate))
                dyParam.Add("@endDate", _att.EndDate, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_HRAttendceGetInfo";
                objRoster = SqlMapper.Query<EmployeeRosterBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objRoster;
        }
        public List<EmployeeAttendanceBO> GetAttendanceEmp(AttendanceInputBO _att)
        {
            List<EmployeeAttendanceBO> objRoster = new List<EmployeeAttendanceBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", _att.EmpID, DbType.Int32, ParameterDirection.Input);
            if (!string.IsNullOrEmpty(_att.CurrentDate))
                dyParam.Add("@attendanceDate", _att.CurrentDate, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_HRAttendanceGetSwipes";
                objRoster = SqlMapper.Query<EmployeeAttendanceBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objRoster;
        }
        public List<AttendanceTerminalBO> GetTerminalData(TerminalInputBO objinput)
        {
            List<AttendanceTerminalBO> objRoster = new List<AttendanceTerminalBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@terminalNo", objinput.TerminalNo, DbType.String, ParameterDirection.Input);
            dyParam.Add("@deviceIdentifier", objinput.DeviceIdentifier, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetTerminal";
                objRoster = SqlMapper.Query<AttendanceTerminalBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objRoster;
        }
        public bool GetCheckInMsg(int EmpID, int LocationID, int TenantID)
        {
            bool objRoster = false;
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("empId", EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetCheckInData";
                var objdata = SqlMapper.Query<ManualInandOutBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                if (objdata.Count > 0)
                {
                    objRoster = true;
                }
                else
                {
                    objRoster = false;
                }
            }
            conn.Close();
            conn.Dispose();
            return objRoster;
        }
        public StatusCountBO GetCountData(EmployeeAttendanceInputBO objemp)
        {
            StatusCountBO objRoster = new StatusCountBO();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", objemp.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objemp.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objemp.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@attendanceDate", objemp.AttendanceDate, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetAttendanceCount";
                objRoster = SqlMapper.Query<StatusCountBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return objRoster;
        }
        // public List<EmployeeRosterBO> GetGraphData(EmployeeAttendanceInputBO objemp)
        // {
        //     List<EmployeeRosterBO> objRoster = new List<EmployeeRosterBO>();
        //     DynamicParameters dyParam = new DynamicParameters();
        //     dyParam.Add("@empId", objemp.EmpID, DbType.Int32, ParameterDirection.Input);
        //     dyParam.Add("@tenantId", objemp.TenantID, DbType.Int32, ParameterDirection.Input);
        //     dyParam.Add("@locationId", objemp.LocationID, DbType.Int32, ParameterDirection.Input);
        //     dyParam.Add("@attendanceDate", objemp.AttendanceDate, DbType.String, ParameterDirection.Input);
        //     var conn = this.GetConnection();
        //     if (conn.State == ConnectionState.Closed)
        //     {
        //         conn.Open();
        //     }
        //     if (conn.State == ConnectionState.Open)
        //     {
        //         var query = "sp_GetAttendanceGraph";
        //         objRoster = SqlMapper.Query<EmployeeRosterBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
        //     }
        //     conn.Close();
        //     conn.Dispose();
        //     return objRoster;
        // }
        public EmployeeRosterSearchBO GetEmployeeRosterSearch(EmployeeRosterSearchBO objplan)
        {
            EmployeeRosterSearchBO output = new EmployeeRosterSearchBO();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", objplan.objinput.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objplan.objinput.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@empId", objplan.objinput.EmpID, DbType.String, ParameterDirection.Input);
            dyParam.Add("@attendanceDate", objplan.objinput.AttendanceDate, DbType.String, ParameterDirection.Input);
            dyParam.Add("@departmentId", objplan.objinput.DepartmentID, DbType.String, ParameterDirection.Input);
            dyParam.Add("@searchData", objplan.ServerSearchables.search_data, DbType.String, ParameterDirection.Input);
            dyParam.Add("@pageNo", objplan.ServerSearchables.page_No, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@pageSize", objplan.ServerSearchables.page_Size, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@orderByColumn", objplan.ServerSearchables.orderBy_Column, DbType.String, ParameterDirection.Input);
            dyParam.Add("@orderBy", objplan.ServerSearchables.order_By, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetAttendanceGraphSearch";
                var data= SqlMapper.Query<EmployeeRosterOutputBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                output.objoutlist = data;
                output.objinput = objplan.objinput;
                output.ServerSearchables = objplan.ServerSearchables;
                if(output.objoutlist.Count > 0)
                {
                    output.ServerSearchables.RecordsTotal = output.objoutlist[0].TotalCount;
                }
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public List<LeaveRequestBO> GetLeaveRequestData(EmployeeAttendanceInputBO objemp)
        {
            List<LeaveRequestBO> objRoster = new List<LeaveRequestBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", objemp.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objemp.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objemp.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@leaveDate", objemp.AttendanceDate, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetLeaveRequestData";
                objRoster = SqlMapper.Query<LeaveRequestBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objRoster;
        }
        public List<PermissionRequestBO> GetPermissionRequestData(EmployeeAttendanceInputBO objemp)
        {
            List<PermissionRequestBO> objRoster = new List<PermissionRequestBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", objemp.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objemp.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objemp.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@permissionDate", objemp.AttendanceDate, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetPermissionRequestData";
                objRoster = SqlMapper.Query<PermissionRequestBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objRoster;
        }
        // public List<TardiesBO> GetTardiesData(EmployeeAttendanceInputBO objemp)
        // {
        //     List<TardiesBO> objRoster = new List<TardiesBO>();
        //     DynamicParameters dyParam = new DynamicParameters();
        //     dyParam.Add("@empId", objemp.EmpID, DbType.Int32, ParameterDirection.Input);
        //     dyParam.Add("@tenantId", objemp.TenantID, DbType.Int32, ParameterDirection.Input);
        //     dyParam.Add("@locationId", objemp.LocationID, DbType.Int32, ParameterDirection.Input);
        //     dyParam.Add("@attendanceDate", objemp.AttendanceDate, DbType.String, ParameterDirection.Input);
        //     var conn = this.GetConnection();
        //     if (conn.State == ConnectionState.Closed)
        //     {
        //         conn.Open();
        //     }
        //     if (conn.State == ConnectionState.Open)
        //     {
        //         var query = "sp_GetTardiesData";
        //         objRoster = SqlMapper.Query<TardiesBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
        //     }
        //     conn.Close();
        //     conn.Dispose();
        //     return objRoster;
        // }
        public EmployeeBO GetEmployee(EmployeeAttendanceInputBO objemp)
        {
            EmployeeBO objRoster = new EmployeeBO();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", objemp.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", objemp.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objemp.LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEmployee";
                objRoster = SqlMapper.Query<EmployeeBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return objRoster;
        }
        public EmpDetailsBO GetEmpDetails(int EmpID,string AttendanceDate)
        {
            EmpDetailsBO objRoster = new EmpDetailsBO();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@attendanceDate", AttendanceDate, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEmpDetails";
                objRoster = SqlMapper.Query<EmpDetailsBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return objRoster;
        }
        public string UpdateEmployeeStatus(EmpStatusBO objsts)
        {
            string result = string.Empty;
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", objsts.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@attendanceDate", objsts.AttendanceDate, DbType.String, ParameterDirection.Input);
            dyParam.Add("@tenantId", objsts.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objsts.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@startTime", objsts.StartTime, DbType.String, ParameterDirection.Input);
            dyParam.Add("@endTime", objsts.EndTime, DbType.String, ParameterDirection.Input);
            dyParam.Add("@timeIn", objsts.TimeIn, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@timeOut", objsts.TimeOut, DbType.DateTime, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_UpdateEmpAttendanceStatus";
                result = SqlMapper.Query<string>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return result;
        }
        public EmployeeAttendanceBO AttendanceInandOut(AttendanceInandOutBO _shiftin)
        {
            EmployeeAttendanceBO objRoster = new EmployeeAttendanceBO();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Emp_ID", _shiftin.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", _shiftin.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Tenant_ID", _shiftin.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@TimeOut_Msg", _shiftin.TimeOutMsg, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Time_In", _shiftin.TimeIn, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Time_Out", _shiftin.TimeOut, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_HRSaveAttendanceInandOut";
                objRoster = SqlMapper.Query<EmployeeAttendanceBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return objRoster;
        }
        public string AttendanceRosterGenerted(ShifRosterGenertedBO _shift)
        {
            string objRoster = string.Empty;
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Tenant_ID", _shift.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@YEAR_ID", _shift.YearID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", _shift.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Emp_ID", _shift.EmpID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_HRAttendanceRosterGenerted";
                objRoster = SqlMapper.Query<string>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return objRoster;
        }
        public List<EmployeeBO> GetAttendanceEmployeeList(int TenantID, int LocationID)
        {
            List<EmployeeBO> obj = new List<EmployeeBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@LocationID", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@TenantID", TenantID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "SELECT * FROM tblEmployee WHERE TenantID = @TenantID AND EmpStatus = 'Active' AND LocationID = @LocationID AND IsAttendance = 1";
                obj = SqlMapper.Query<EmployeeBO>(conn, query, param: dyParam, commandType: CommandType.Text).ToList();

            }
            conn.Close();
            conn.Dispose();
            return obj;
        }
        public string UpdateDeviceExpiryDate(int DeviceID)
        {
            string result = string.Empty;
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@deviceId", DeviceID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
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
        public List<DeviceMasterBO> GetDeviceID(int TenantID,int EntityID)
        {
            List<DeviceMasterBO>  result = new List<DeviceMasterBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@entityId", EntityID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
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
        public SaveOut SaveAttendacePIN(EmpAttendanceConfigBO objconfig,string AttendCardNo)
        {
            SaveOut result = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@attendConfigId", objconfig.AttendConfigID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@empId", objconfig.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", objconfig.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@clockingMode", objconfig.ClockingMode, DbType.String, ParameterDirection.Input);
            dyParam.Add("@attendCardNo", AttendCardNo, DbType.String, ParameterDirection.Input);
            dyParam.Add("@isRoster", objconfig.IsRoster, DbType.Boolean, ParameterDirection.Input);
            dyParam.Add("@isIDCardLinked", objconfig.IsIDCardLinked, DbType.Boolean, ParameterDirection.Input);
            dyParam.Add("@createdBy", objconfig.CreatedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@modifiedBy", objconfig.ModifiedBy, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveAttendancePIN";
                result = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return result;
        }
        public string GetEmpNumber(int EmpID)
        {
            string result = string.Empty;
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
             var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEmpNumber";
                result = SqlMapper.Query<string>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return result;
        }
        public List<EmployeeListBO> GetEmployeeList(int TenantID,int LocationID)
        {
            List<EmployeeListBO>  result = new List<EmployeeListBO>();
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
                var query = "sp_GetEmployeeList";
                result = SqlMapper.Query<EmployeeListBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return result;
        }
        public SaveOut AddAttendanceProfile(WorkingHours hours)
        {
            SaveOut rtnval = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@dayId",hours.DayID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@days",hours.Day,DbType.String,ParameterDirection.Input);
            dyParam.Add("@startTime",hours.StartTime,DbType.String,ParameterDirection.Input);
            dyParam.Add("@endTime",hours.EndTime,DbType.String,ParameterDirection.Input);
            dyParam.Add("@dayType",hours.DayType,DbType.String,ParameterDirection.Input);
            dyParam.Add("@shiftId",hours.ShiftID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@shiftName",hours.ShiftName,DbType.String,ParameterDirection.Input);
            dyParam.Add("@shiftStatus",hours.ShiftStatus,DbType.String,ParameterDirection.Input);
            dyParam.Add("@locationId",hours.LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@tenantId",hours.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@createdBy",hours.CreatedBy,DbType.Int32,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveAttendenceWorkinghours";
                rtnval = SqlMapper.Query<SaveOut>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<WorkingHours> GetShiftList(int TenantID,int LocationID)
        {
            List<WorkingHours>  result = new List<WorkingHours>();
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
                var query = "sp_GetAttendenceShiftList";
                result = SqlMapper.Query<WorkingHours>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return result;
        }
        public List<ReturnShiftBO> GetShiftName(int TenantID,int LocationID)
        {
            List<ReturnShiftBO>  result = new List<ReturnShiftBO>();
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
                var query = "sp_GetAttendenceShiftList";
                result = SqlMapper.Query<ReturnShiftBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return result;
        }
        public List<ReturnShiftSchdBO> GetAttendanceSchedule(int TenantID,int LocationID)
        {
            List<ReturnShiftSchdBO>  result = new List<ReturnShiftSchdBO>();
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
                var query = "sp_GetAttendanceSchedule";
                result = SqlMapper.Query<ReturnShiftSchdBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return result;
        }
        public SaveOut AttendanceRoster(HRRoasterBO roster)
        {
            SaveOut rtnval = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            //dyParam.Add("@rosterId",roster.RosterID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@shiftId",roster.ShiftID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@empId",roster.EmpID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@rosterStatus",roster.RosterStatus,DbType.String,ParameterDirection.Input);
            //dyParam.Add("@rosterDate",roster.RosterDate,DbType.DateTime,ParameterDirection.Input);
            dyParam.Add("@startDate",roster.StartDate,DbType.String,ParameterDirection.Input);
            dyParam.Add("@endDate",roster.EndDate,DbType.String,ParameterDirection.Input);
            dyParam.Add("@yearId",roster.YearId,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@isRotated",roster.IsRotated,DbType.Boolean,ParameterDirection.Input);
            dyParam.Add("@locationId",roster.LocationID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@tenantId",roster.TenantID,DbType.Int32,ParameterDirection.Input);
            dyParam.Add("@createdBy",roster.CreatedBy,DbType.Int32,ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_HRSaveRoaster";
                rtnval = SqlMapper.Query<SaveOut>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<WorkingHours> GetWorkingHours(int TenantID,int LocationID,string ShiftName)
        {
            List<WorkingHours>  result = new List<WorkingHours>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@shiftName", ShiftName, DbType.String, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetShiftWorkingHours";
                result = SqlMapper.Query<WorkingHours>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return result;
        }
        public List<ReturnEmpShiftNameBO> GetShiftRoasterEmp(int TenantID,int LocationID,int ShiftID)
        {
            List<ReturnEmpShiftNameBO>  result = new List<ReturnEmpShiftNameBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@shiftId", ShiftID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_HRGetShiftRoaster";
                result = SqlMapper.Query<ReturnEmpShiftNameBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return result;
        }
        public List<HRRoasterBO> GetEmpShift(DateTime StartDate,DateTime EndDate,int ShiftID,int EmpID)
        {
            List<HRRoasterBO>  result = new List<HRRoasterBO>();
            DynamicParameters dyParam = new DynamicParameters();
            //DateTime enteredDate = DateTime.Parse(StartDate);
            dyParam.Add("@startDate", StartDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@endDate", EndDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@shiftId", ShiftID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEmpShiftAttendanceRoaster";
                result = SqlMapper.Query<HRRoasterBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return result;
        }
        public List<EmployeeListsBO> GetEmployeeList(int TenantID, int LocationID, int EmpID)
        {
            List<EmployeeListsBO> rtnval = new List<EmployeeListsBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetEmployee";
                rtnval = SqlMapper.Query<EmployeeListsBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
    }
}