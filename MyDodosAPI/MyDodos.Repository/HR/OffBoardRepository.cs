using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.HR;
using MyDodos.Domain.Wrapper;
using MyDodos.Domain.AuthBO;
using MyDodos.Domain.Document;
using MyDodos.ViewModel.HR;
using MySql.Data.MySqlClient;
using System.Reflection;
using System.ComponentModel;
using MyDodos.ViewModel.Business;

namespace MyDodos.Repository.HR
{
    public class OffBoardRepository : IOffBoardRepository
    {
        private readonly IConfiguration _configuration;
        public OffBoardRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public OffBoardSearchBO SearchOffboardingList(OffBoardSearchBO inputParam)
        {
            OffBoardSearchBO objoffboard = new OffBoardSearchBO();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@commonsearch", inputParam.objOffBoardInput.CommonSearch, DbType.String, ParameterDirection.Input);
            dyParam.Add("@empStatus", inputParam.objOffBoardInput.RequestStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@tenantId", inputParam.objOffBoardInput.TenantID, DbType.Int32, ParameterDirection.Input);
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
                var query = "sp_GetOffBoardDataSearch";
                var data= SqlMapper.Query<BPCheckListDetail>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                objoffboard.objOffBoardList = data;
                objoffboard.objOffBoardInput = inputParam.objOffBoardInput;
                objoffboard.ServerSearchables = inputParam.ServerSearchables;
                if(objoffboard.objOffBoardList.Count>0)
                {
                    objoffboard.ServerSearchables.RecordsTotal = objoffboard.objOffBoardList[0].TotalCount;
                    objoffboard.ServerSearchables.RecordsFiltered = objoffboard.objOffBoardList.Count();
                }
            }
            conn.Close();
            conn.Dispose();
            return objoffboard;
        }
        public List<BPCheckListDetail> SearchOffboardList(string FirstName,int TenantID)
        {
            List<BPCheckListDetail> objoffboard = new List<BPCheckListDetail>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@firstName", FirstName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetOffBoardSearch";
                objoffboard= SqlMapper.Query<BPCheckListDetail>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objoffboard;
        }
        public BPCheckListDetails ViewOffboardListTrack(int ChkListInstanceID)
        {
            BPCheckListDetails objoffboard = new BPCheckListDetails();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@chkListInstanceId", ChkListInstanceID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetOffboardListTrack";
                objoffboard= SqlMapper.Query<BPCheckListDetails>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return objoffboard;
        }
        public List<RecentExitOffBoardBO> RecentExitOffBoarding(int TenantID)
        {
            List<RecentExitOffBoardBO> objoffboard = new List<RecentExitOffBoardBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetRecentExitOffBoarding";
                objoffboard= SqlMapper.Query<RecentExitOffBoardBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objoffboard;
        }
        public OnBoardRequestModelMsg CompleteOffBoarding(CompleteOffBoardingBO complete)
        {
            OnBoardRequestModelMsg objoffboard = new OnBoardRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@chkListInstanceId", complete.ChkListInstanceID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@exitDate", complete.ExitDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@exitReason", complete.ExitReason, DbType.String, ParameterDirection.Input);
            dyParam.Add("@comments", complete.Comments, DbType.String, ParameterDirection.Input);
            dyParam.Add("@tenantId", complete.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@actionBy", complete.ActionBy, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_HRCompleteOffBoarding";
                objoffboard = SqlMapper.Query<OnBoardRequestModelMsg>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return objoffboard;
        }
        public List<BPCheckListDetail> GetOffboardRequest(int TenantID,int ChkListInstanceID, string RequestStatus)
        {
            List<BPCheckListDetail> objoffboard = new List<BPCheckListDetail>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@chkListInstanceId", ChkListInstanceID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@requestStatus", RequestStatus, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_HRGetOffBoardingList";
                objoffboard= SqlMapper.Query<BPCheckListDetail>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objoffboard;
        }
        public List<BPTransInstance> GetOffBoardTrack(int BProcessID,int ReqInitID)
        {
            List<BPTransInstance> objoffboard = new List<BPTransInstance>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@bProcessId", BProcessID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@reqInitId", ReqInitID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetOffBoardTrack";
                objoffboard= SqlMapper.Query<BPTransInstance>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objoffboard;
        }
        public OnBoardRequestModelMsg DeleteoffboardingRequest(int ChkListInstanceID)
        {
            OnBoardRequestModelMsg rtnval = new OnBoardRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@chkListInstanceId",ChkListInstanceID,DbType.Int32,ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_DeleteoffboardingRequest";
                rtnval = SqlMapper.Query<OnBoardRequestModelMsg>(conn,query,param:dyParam,commandType:CommandType.StoredProcedure).FirstOrDefault();         
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public ReqDetails GetReqDetails(int ChkListInstanceID)
        {
            ReqDetails objoffboard = new ReqDetails();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@chkListInstanceId", ChkListInstanceID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetoffboardReqDetails";
                objoffboard= SqlMapper.Query<ReqDetails>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return objoffboard;
        }
        public GenCheckListInstance GetRequest(int ChkListInstanceID,int ProductID)
        {
            GenCheckListInstance result = new GenCheckListInstance();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("chkListInstanceId", ChkListInstanceID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query ="sp_GetoffboardRequest";
                result = SqlMapper.Query<GenCheckListInstance>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return result;
        }
        public List<BPCheckListDetail> GetOffBoardingRequest(int TenantID)
        {
            List<BPCheckListDetail> objoffboard = new List<BPCheckListDetail>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetOffBoardingRequest";
                objoffboard= SqlMapper.Query<BPCheckListDetail>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objoffboard;
        }
        public OnBoardRequestModelMsg CreateCheckListInstance(GenCheckListInstance instance)
        {
            OnBoardRequestModelMsg objoffboard = new OnBoardRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@chkListInstanceId", instance.ChkListInstanceID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", instance.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@chkProcessName", instance.ChkProcessName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@refEntityId", instance.RefEntityID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@createdBy", instance.CreatedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@requestDescription", instance.RequestDescription, DbType.String, ParameterDirection.Input);
            dyParam.Add("@isNotice", instance.IsNotice, DbType.Boolean, ParameterDirection.Input);
            dyParam.Add("@noticePeriod", instance.NoticePeriod, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@isStartProcess", instance.IsStartProcess, DbType.Boolean, ParameterDirection.Input);
            dyParam.Add("@noticeDate", instance.NoticeDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@initiatedOn", instance.InitiatedOn, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@noticePeriodType", instance.NoticePeriodType, DbType.String, ParameterDirection.Input);
            dyParam.Add("@locationId", instance.LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_CreateOffBoardCheckListInstance";
                objoffboard = SqlMapper.Query<OnBoardRequestModelMsg>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return objoffboard;
        }
        public OnBoardRequestModelMsg UploadOffboardLetter(int ChkListInstanceID, int TenantID, string ResignFile)
        {
            OnBoardRequestModelMsg objoffboard = new OnBoardRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@chkListInstanceId", ChkListInstanceID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@resignFile", ResignFile, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetUpdateOffBoard";
                objoffboard = SqlMapper.Query<OnBoardRequestModelMsg>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return objoffboard;
        }
        public List<BPCheckListDetail> GetOffBoardReqChkLists(int ChkListInstanceID,int TenantID)
        {
            List<BPCheckListDetail> objoffboard = new List<BPCheckListDetail>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@chkListInstanceId", ChkListInstanceID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetOffBoardReqChkLists";
                objoffboard= SqlMapper.Query<BPCheckListDetail>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objoffboard;
        }
        public List<BPchecklistDetBO> GetOffBoardReqCheckList(int ChkListInstanceID)
        {
            List<BPchecklistDetBO> objoffboard = new List<BPchecklistDetBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@chkListInstanceId", ChkListInstanceID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetOffBoardReqCheckList";
                objoffboard= SqlMapper.Query<BPchecklistDetBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objoffboard;
        }
        public OnBoardRequestModelMsg UpdateCheckListItem(UpdateCheckList list)
        {
            OnBoardRequestModelMsg objoffboard = new OnBoardRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@chkListInsId", list.ChkListInsID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@updatedBy", list.UpdatedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@itemComments", list.ItemComments, DbType.String, ParameterDirection.Input);
            dyParam.Add("@isCompleted", list.IsCompleted, DbType.Boolean, ParameterDirection.Input);
            dyParam.Add("@tenantId", list.TenantID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_UpdateCheckListItem";
                objoffboard = SqlMapper.Query<OnBoardRequestModelMsg>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return objoffboard;
        }
        public EmployeeInfoBO GetEmpOffBoardInfo(int TenantID, int LocationID, int EmpID)
        {
            EmployeeInfoBO objoffboard = new EmployeeInfoBO();
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
                var query = "sp_GetEmpOffBoardInfo";
                objoffboard= SqlMapper.Query<EmployeeInfoBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return objoffboard;
        }
        public List<BPCheckList> GetOffBoardReqChkList(int ChkListInstanceID, string ChkListGroup)
        {
            List<BPCheckList> objoffboard = new List<BPCheckList>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@chkListInstanceId", ChkListInstanceID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@chkListGroup", ChkListGroup, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetOffBoardReqChkList";
                objoffboard= SqlMapper.Query<BPCheckList>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objoffboard;
        }
        public OnBoardRequestModelMsg AddGenCheckListDetail(int RefEntityID,int LocationID,int TenantID)
        {
            OnBoardRequestModelMsg objoffboard = new OnBoardRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", RefEntityID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_UpdateOffboardChecklist";
                objoffboard = SqlMapper.Query<OnBoardRequestModelMsg>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return objoffboard;
        }
        public OffboardRequestSearchBO GetOffboardRequestSearch(OffboardRequestSearchBO inputParam)
        {
            OffboardRequestSearchBO objoffboard = new OffboardRequestSearchBO();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", inputParam.objOffBoardRequestInput.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@requestStatus", inputParam.objOffBoardRequestInput.RequestStatus, DbType.String, ParameterDirection.Input);
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
                var query = "sp_HRGetOffBoardingListSearch";
                var data= SqlMapper.Query<BPCheckListDetail>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                objoffboard.objOffBoardRequestList = data;
                objoffboard.objOffBoardRequestInput = inputParam.objOffBoardRequestInput;
                objoffboard.ServerSearchables = inputParam.ServerSearchables;
                if(objoffboard.objOffBoardRequestList.Count>0)
                {
                    objoffboard.ServerSearchables.RecordsTotal = objoffboard.objOffBoardRequestList[0].TotalCount;
                    objoffboard.ServerSearchables.RecordsFiltered = objoffboard.objOffBoardRequestList.Count();
                }
            }
            conn.Close();
            conn.Dispose();
            return objoffboard;
        }
        public OnBoardRequestModelMsg SaveBPTransInstance(int TenantID, int LocationID,int EmpID)
        {
            OnBoardRequestModelMsg rtnval = new OnBoardRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_HRSaveOffboardBPTransInstance";
                rtnval = SqlMapper.Query<OnBoardRequestModelMsg>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<BPTransInstance> GetTracking(int TenantID, int LocationID, int EmpID)
        {
            List<BPTransInstance> objpersonal = new List<BPTransInstance>();
            OnBoardingRequest obj = new OnBoardingRequest();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetOffBoardHROnboardTrack";
                objpersonal = SqlMapper.Query<BPTransInstance>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objpersonal;
        }       
    }
}