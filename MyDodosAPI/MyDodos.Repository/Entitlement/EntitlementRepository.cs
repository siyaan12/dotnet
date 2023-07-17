using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.AuthBO;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.Common;
using MyDodos.ViewModel.Entitlement;
using MySql.Data.MySqlClient;

namespace MyDodos.Repository.Entitlement
{
    public class EntitlementRepository : IEntitlementRepository
    {
        private readonly IConfiguration _configuration;
        public EntitlementRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public SaveOut SaveEntAppUserData(AppUser _objEntAppUser)
        {
            SaveOut obj = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@appUserId", _objEntAppUser.AppUserID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@appUserName", _objEntAppUser.AppUserName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@appUserpassword", _objEntAppUser.AppUserPassword, DbType.String, ParameterDirection.Input);
            dyParam.Add("@appUserStatus", _objEntAppUser.AppUserStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@primaryCompanyId", _objEntAppUser.PrimaryCompanyID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@profilePhoto", _objEntAppUser.ProfilePhoto, DbType.String, ParameterDirection.Input);
            dyParam.Add("@prefix", _objEntAppUser.Prefix, DbType.String, ParameterDirection.Input);
            dyParam.Add("@firstName", _objEntAppUser.FirstName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@middleName", _objEntAppUser.MiddleName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@lastName", _objEntAppUser.LastName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@suffix", _objEntAppUser.Suffix, DbType.String, ParameterDirection.Input);
            dyParam.Add("@department", _objEntAppUser.Department, DbType.String, ParameterDirection.Input);
            dyParam.Add("@activationKey", _objEntAppUser.ActivationKey, DbType.String, ParameterDirection.Input);
            dyParam.Add("@isActivated", _objEntAppUser.IsActivated, DbType.Boolean, ParameterDirection.Input);
            dyParam.Add("@changePassword", _objEntAppUser.ChangePassword, DbType.Boolean, ParameterDirection.Input);
            dyParam.Add("@reportingToId", _objEntAppUser.ReportToRoleID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@profileImageId", _objEntAppUser.ProfileImageID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@defaultPage", _objEntAppUser.DefaultPage, DbType.String, ParameterDirection.Input);
            dyParam.Add("@createdBy", _objEntAppUser.CreatedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@modifiedBy", _objEntAppUser.ModifiedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@failedAttempts", _objEntAppUser.FailedAttempts, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@currentSigninIp", _objEntAppUser.CurrentSigninIP, DbType.String, ParameterDirection.Input);
            dyParam.Add("@lastSigninIp", _objEntAppUser.LastSigninIP, DbType.String, ParameterDirection.Input);
            dyParam.Add("@primaryEmail", _objEntAppUser.PrimaryEmail, DbType.String, ParameterDirection.Input);
            dyParam.Add("@secondaryEmail", _objEntAppUser.SecondaryEmail, DbType.String, ParameterDirection.Input);
            dyParam.Add("@contactPhone", _objEntAppUser.ContactPhone, DbType.String, ParameterDirection.Input);
            dyParam.Add("@loginPIn", _objEntAppUser.LoginPIN, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@userType", _objEntAppUser.UserType, DbType.String, ParameterDirection.Input);
            dyParam.Add("@isSsoUser", _objEntAppUser.IsSSoUser, DbType.Boolean, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveEntAppUserData";
                obj = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return obj;
        }
        public SaveOut SaveEntEmployee(AppUser _objEntAppUser)
        {
            SaveOut obj = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", 0, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@firstName", _objEntAppUser.FirstName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@middleName", _objEntAppUser.MiddleName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@lastName", _objEntAppUser.LastName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@suffix", _objEntAppUser.Suffix, DbType.String, ParameterDirection.Input);
            dyParam.Add("@funcRoleId", _objEntAppUser.RoleID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@empLocId", _objEntAppUser.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@departmentId", 0, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@gender", "", DbType.String, ParameterDirection.Input);
            dyParam.Add("@officeEmail", _objEntAppUser.AppUserName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@personalMail", _objEntAppUser.PrimaryEmail, DbType.String, ParameterDirection.Input);
            dyParam.Add("@tenantId", _objEntAppUser.PrimaryCompanyID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@empType", "", DbType.String, ParameterDirection.Input);
            dyParam.Add("@empStatus", _objEntAppUser.AppUserStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@createdBy", _objEntAppUser.CreatedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@createdOn", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@appUserId", _objEntAppUser.AppUserID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveEntEmployee";
                obj = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return obj;
        }
    }
}