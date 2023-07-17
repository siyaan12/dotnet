using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.HR;
using MyDodos.Domain.Wrapper;
using MyDodos.Domain.AuthBO;
using MyDodos.ViewModel.HR;
using MySql.Data.MySqlClient;
using System.Reflection;
using System.ComponentModel;
using MyDodos.ViewModel.Business;
using MyDodos.ViewModel.Document;
using MyDodos.ViewModel.Common;

namespace MyDodos.Repository.HR
{
    public class OnBoardRepository : IOnBoardRepository
    {
        private readonly IConfiguration _configuration;
        public OnBoardRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public int UpdateOnBoardSetting(BPProcessBO bpBo)
        {
            int rtnval = 0;
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@bPids", bpBo.bpids, DbType.String, ParameterDirection.Input);
            dyParam.Add("@transOrder", bpBo.TransOrder, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", bpBo.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", bpBo.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@bprocessId", bpBo.BProcessID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_HRSaveSetting";
                rtnval = SqlMapper.Query<int>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
                
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
       public Response<List<BPTransaction>> GetOnBoardSetting(int TenantID, int LocationID, int TransOrder, string ProcessCategory)
        {
            Response<List<BPTransaction>> response;
            try
            {
            List<BPTransaction> obj = new List<BPTransaction>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Tenant_ID", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@transOrder", TransOrder, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Process_Category", ProcessCategory, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetHROnboardSetting";
                obj = SqlMapper.Query<BPTransaction>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                
            }
            conn.Close();
            conn.Dispose();
            response = new Response<List<BPTransaction>>(obj, 200);
            }
            catch(Exception ex)
            {
                response = new Response<List<BPTransaction>>(ex.Message, 500);
            }
            return response;
        }
        public Response<OnBoardRequestModelMsg> SaveBusinessOnboard(BPTransInstance bprocess)
        {
            Response<OnBoardRequestModelMsg> response;
            try
            {
            OnBoardRequestModelMsg objBp = new OnBoardRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@BPTrans_ID", bprocess.BPTransInstanceID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Begin_When", bprocess.BeginWhen, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Ends_When", bprocess.EndsWhen, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Pre_Condition", bprocess.PreCondition, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Post_Condition", bprocess.PostCondition, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Trans_Status", bprocess.TransStatus, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_HRSaveBusinessOnboard";
                objBp = SqlMapper.Query<OnBoardRequestModelMsg>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            response = new Response<OnBoardRequestModelMsg>(objBp, 200);
            }
            catch(Exception ex)
            {
                response = new Response<OnBoardRequestModelMsg>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<IDProofDocumnent>> GetMasDocProof(int CountryId)
        {
            Response<List<IDProofDocumnent>> response;
            try
            {
            List<IDProofDocumnent> objMasProof = new List<IDProofDocumnent>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Country_Id", CountryId, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetCoutryDocType";
                objMasProof = SqlMapper.Query<IDProofDocumnent>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();

            }
            conn.Close();
            conn.Dispose();
             response = new Response<List<IDProofDocumnent>>(objMasProof, 200);
            }
            catch(Exception ex)
            {
                response = new Response<List<IDProofDocumnent>>(ex.Message, 500);
            }
            return response;
        }
        public int SaveOnboardEducation(HREmpEducation onEdu)
        {
            int rtnval = 0;
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Edu_ID", onEdu.EmpEduID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Emp_ID", onEdu.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Course_Name", onEdu.CourseName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Level_Name", onEdu.LevelName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Institute_Name", onEdu.InstituteName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Duration_From", onEdu.DurationFrom, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Duration_To", onEdu.DurationTo, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Created_By", onEdu.CreatedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Modified_By", onEdu.ModifiedBy, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_HRSaveOnboardEducation";
                rtnval = SqlMapper.Query<int>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<HREmpEducation> GetOnboardEducation(int EmpID)
        {
            List<HREmpEducation> obj = new List<HREmpEducation>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Emp_ID", EmpID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetHREducation";
                obj = SqlMapper.Query<HREmpEducation>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();

            }
            conn.Close();
            conn.Dispose();
            return obj;
        }
        public Response<OnBoardRequestModelMsg> DeleteOnboardEducation(int EmpEduID)
        {
            Response<OnBoardRequestModelMsg> response;
            try
            {
            OnBoardRequestModelMsg obj = new OnBoardRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@EmpEdu_ID", EmpEduID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_DeleteHREducation";
                obj = SqlMapper.Query<OnBoardRequestModelMsg>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            response = new Response<OnBoardRequestModelMsg>(obj, 200);
            }
            catch(Exception ex)
            {
                response = new Response<OnBoardRequestModelMsg>(ex.Message, 500);
            }
            return response;
        }
        public OnBoardRequestModelMsg AddOnBoardingForm(EmpOnboardingModBO onboarding)
        {
            OnBoardRequestModelMsg rtnval = new OnBoardRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Onboardingid", onboarding.EmpOnboardingID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Requestsource", onboarding.RequestSource, DbType.String, ParameterDirection.Input);
            dyParam.Add("@GroupType_ID", onboarding.GroupTypeID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Group_Type", onboarding.GroupType, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Sourcerefnumber", onboarding.SourceRefNumber, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Requestdesc", onboarding.RequestDesc, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Initiationdate", onboarding.InitiationDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@Firstname", onboarding.FirstName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Middlename", onboarding.MiddleName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Lastname", onboarding.LastName, DbType.String, ParameterDirection.Input);            
            dyParam.Add("@Location_ID", onboarding.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Country_ID", onboarding.CountryID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@HRInchargeid", onboarding.HRInchargeID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@RequestDepid", onboarding.RequestDepID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Roleid", onboarding.RoleID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Tenant_id", onboarding.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@ReqEmpid", onboarding.ReqEmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Requeststatus", onboarding.RequestStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@offerDate", onboarding.OfferDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@acceptanceDate", onboarding.AcceptanceDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@Createdby", onboarding.CreatedBy, DbType.Int32, ParameterDirection.Input);            
            dyParam.Add("@Modifiedby", onboarding.ModifiedBy, DbType.Int32, ParameterDirection.Input);
            
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_HRSaveOnboard";
                
                rtnval = SqlMapper.Query<OnBoardRequestModelMsg>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();            
            return rtnval;
        }
        public int InsertOnBoardPersonal(OnboardPersonalDetail onboarding)
        {
            int rtnval = 0;
                DynamicParameters dyParam = new DynamicParameters();
                dyParam.Add("@Onboardingid", onboarding.EmpOnboardingID, DbType.Int32, ParameterDirection.Input);
                //dyParam.Add("@EmpID", onboarding.EmpID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@Tenantid", onboarding.TenantID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@Firstname", onboarding.FirstName, DbType.String, ParameterDirection.Input);
                dyParam.Add("@Middlename", onboarding.MiddleName, DbType.String, ParameterDirection.Input);
                dyParam.Add("@Lastname", onboarding.LastName, DbType.String, ParameterDirection.Input);
                dyParam.Add("@Suffix", onboarding.Suffix, DbType.String, ParameterDirection.Input);
                dyParam.Add("@Gender", onboarding.Gender, DbType.String, ParameterDirection.Input);
                dyParam.Add("@Dob", onboarding.DOB, DbType.DateTime, ParameterDirection.Input);
                dyParam.Add("@Emp_Status", onboarding.EmpStatus, DbType.String, ParameterDirection.Input);
                dyParam.Add("@Personal_mail", onboarding.PersonalMail, DbType.String, ParameterDirection.Input);
                dyParam.Add("@Mobileno", onboarding.MobileNo, DbType.String, ParameterDirection.Input);
                dyParam.Add("@Alternatephoneno", onboarding.AlternatePhoneNo, DbType.String, ParameterDirection.Input);
                dyParam.Add("@AppUser_ID", onboarding.AppUserID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@Location_ID", onboarding.LocationID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@Officecontactnumber", onboarding.OfficeContactNumber, DbType.String, ParameterDirection.Input);
                dyParam.Add("@Emercontactnumber", onboarding.EmerContactNumber, DbType.String, ParameterDirection.Input);
                dyParam.Add("@Emerrelationship", onboarding.EmerRelationShip, DbType.String, ParameterDirection.Input);
                dyParam.Add("@Bloodgroup", onboarding.BloodGroup, DbType.String, ParameterDirection.Input);
                dyParam.Add("@Maritalstatus", onboarding.MaritalStatus, DbType.String, ParameterDirection.Input);
                dyParam.Add("@Nationality", onboarding.Nationality, DbType.String, ParameterDirection.Input);
                dyParam.Add("@Phonesuffix", onboarding.PhoneSuffix, DbType.String, ParameterDirection.Input);
                dyParam.Add("@Alternatephonesuffix", onboarding.AlternatePhoneSuffix, DbType.String, ParameterDirection.Input);
                dyParam.Add("@Iscompensate", onboarding.IsCompensate, DbType.Boolean, ParameterDirection.Input);
                dyParam.Add("@Istimesheet", onboarding.IsTimeSheet, DbType.Boolean, ParameterDirection.Input);
                dyParam.Add("@Isattendance", onboarding.IsAttendance, DbType.Boolean, ParameterDirection.Input);
                dyParam.Add("@Country_ID", onboarding.CountryID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@Createdby", onboarding.CreatedBy, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@Modifiedby", onboarding.ModifiedBy, DbType.Int32, ParameterDirection.Input);
                var conn = this.GetConnection();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (conn.State == ConnectionState.Open)
                {
                    var query = "sp_HRsaveEmployee";
                    rtnval = SqlMapper.Query<int>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
                conn.Close();
                conn.Dispose();
            return rtnval;
        }
        public List<EmployeeAddress> SaveEmployeeAddress(List<EmployeeAddress> EmpAdd)
        {
            List<EmployeeAddress> role = new List<EmployeeAddress>();
            DynamicParameters dyParam = new DynamicParameters();
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)

            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                foreach (var save in EmpAdd)
                {
                    save.EmpRefID = EmpAdd[0].EmpRefID;
                    dyParam.Add("@Reference_ID", save.EmpRefID, DbType.Int32, ParameterDirection.Input);
                    dyParam.Add("@Address_1", save.Address1, DbType.String, ParameterDirection.Input);
                    dyParam.Add("@Address_2", save.Address2, DbType.String, ParameterDirection.Input);
                    dyParam.Add("@C_ity", save.City, DbType.String, ParameterDirection.Input);
                    dyParam.Add("@S_tate", save.State, DbType.String, ParameterDirection.Input);
                    dyParam.Add("@Z_ip", save.Zip, DbType.String, ParameterDirection.Input);
                    dyParam.Add("@C_ountry", save.Country, DbType.String, ParameterDirection.Input);
                    dyParam.Add("@P_hone", save.Phone, DbType.String, ParameterDirection.Input);
                    dyParam.Add("@F_ax", save.Fax, DbType.String, ParameterDirection.Input);
                    dyParam.Add("@Address_Type", save.AddressType, DbType.String, ParameterDirection.Input);
                    dyParam.Add("@Country_Code", save.CountryCode, DbType.String, ParameterDirection.Input);
                    dyParam.Add("@Is_Same", save.IsSame, DbType.Boolean, ParameterDirection.Input);
                    dyParam.Add("@Address_ID", save.AddressID, DbType.Int32, ParameterDirection.Input);

                    var query = "sp_HRSaveAddress";
                    role = SqlMapper.Query<EmployeeAddress>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                }
            }

            conn.Close();
            conn.Dispose();
            return role;
        }
        public List<OnboardPersonalDetail> GetEmployee(int EmpID,int TenantID,int LocationID)
        {
            List<OnboardPersonalDetail> obj = new List<OnboardPersonalDetail>();
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
                var query = "sp_GetEmployee";
                obj = SqlMapper.Query<OnboardPersonalDetail>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return obj;
        }
        public Response<OnboardSearchBO> GetOnboardList(OnboardSearchBO search)
        {
            Response<OnboardSearchBO> response;
            try
            {
            OnboardSearchBO rtnval = new OnboardSearchBO();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Tenant_Id", search.objOnboardInput.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", search.objOnboardInput.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@search_data", search.ServerSearchables.search_data, DbType.String, ParameterDirection.Input);
            dyParam.Add("@page_No", search.ServerSearchables.page_No, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@page_Size", search.ServerSearchables.page_Size, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@orderBy_Column", search.ServerSearchables.orderBy_Column, DbType.String, ParameterDirection.Input);
            dyParam.Add("@order_By", search.ServerSearchables.order_By, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetOnboardDataSearch";
                 var data = SqlMapper.Query<OnBoardingSerachBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                rtnval.objOnboardList = data;
                //employee.objStageInput = input.objStageInput;
                rtnval.ServerSearchables = search.ServerSearchables;
                if(rtnval.objOnboardList.Count > 0)
                {
                    rtnval.ServerSearchables.RecordsTotal = rtnval.objOnboardList[0].TotalCount;
                    // foreach (var item in rtnval.objOnboardList)
                    // {
                    //     var trans = GetBPTransName(item.EmpOnboardingID);       
                    //     item.TransStatus = trans;         
                    // } 
                }
            }
            conn.Close();
            conn.Dispose();            
            response = new Response<OnboardSearchBO>(rtnval, 200);
            }
            catch(Exception ex)
            {
                response = new Response<OnboardSearchBO>(ex.Message, 500);
            }
            return response;
        }
        public List<OnBoardingResourceBO> GetHROnboardResource(int TenantID, int LocationID)
        {
            // Response<List<OnBoardingResourceBO>> response;
            // try
            // {
            List<OnBoardingResourceBO> rtnval = new List<OnBoardingResourceBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Tenant_ID", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetHROnboardResource";
                rtnval = SqlMapper.Query<OnBoardingResourceBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            // response = new Response<List<OnBoardingResourceBO>>(rtnval, 200, "");
            // }
            // catch(Exception ex)
            // {
            //     response = new Response<List<OnBoardingResourceBO>>(ex.Message, 500);
            // }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public OnBoardRequestModelMsg SaveHROnboardGenral(OnBoardingGenralBO genral)
        {
            OnBoardRequestModelMsg rtnval = new OnBoardRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@onBoardId", genral.EmpOnboardingID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@offerDate", genral.OfferDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@acceptanceDate", genral.AcceptanceDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@joiningDate", genral.JoiningDate, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@reqStatus", genral.ReqStatus, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveHROnboardGenral";
                rtnval = SqlMapper.Query<OnBoardRequestModelMsg>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public int SaveEmployeeAppuser(InputAppUserBO EmpAdd)
        {
            int role = 0;
            DynamicParameters dyParam = new DynamicParameters();
                    dyParam.Add("@appUserId", EmpAdd.AppUserID, DbType.Int32, ParameterDirection.Input);
                    dyParam.Add("@empId", EmpAdd.EmpID, DbType.Int32, ParameterDirection.Input);
                    dyParam.Add("@appUserName", EmpAdd.UserName, DbType.String, ParameterDirection.Input);
                    dyParam.Add("@appUserPassword", EmpAdd.Password, DbType.String, ParameterDirection.Input);
                    dyParam.Add("@tenantId", EmpAdd.TenantID, DbType.Int32, ParameterDirection.Input);
                    dyParam.Add("@firstName", EmpAdd.FirstName, DbType.String, ParameterDirection.Input);
                    dyParam.Add("@middleName", EmpAdd.MiddleName, DbType.String, ParameterDirection.Input);
                    dyParam.Add("@lastName", EmpAdd.LastName, DbType.String, ParameterDirection.Input);
                    dyParam.Add("@createdBy", EmpAdd.CreatedBy, DbType.Int32, ParameterDirection.Input);
                    dyParam.Add("@userType", EmpAdd.UserAccessType, DbType.String, ParameterDirection.Input);
                    dyParam.Add("@appUserStatus", EmpAdd.AppUserStatus, DbType.String, ParameterDirection.Input);
                    var query = "sp_HRSaveEntAppUser";
                    var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                    role = SqlMapper.Query<int>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return role;
        }
        public OnBoardingRequestBO GetOnboardIndividual(int EmpOnboardingID, int LocationID)
        {
            OnBoardingRequestBO obj = new OnBoardingRequestBO();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@EmpOnboarding_ID", EmpOnboardingID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)

            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {

                var query = "sp_GetHROnboard";
                obj = SqlMapper.Query<OnBoardingRequestBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();

            }
            conn.Close();
            conn.Dispose();
            return obj;
        }
        public List<EmployeeAddress> GetAddress(int EmpID)
        {
            List<EmployeeAddress> obj = new List<EmployeeAddress>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Employee_ID", EmpID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetHRAddress";
                obj = SqlMapper.Query<EmployeeAddress>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return obj;
        }
        public OnBoardingRequest GetOnboardTrack(int EmpOnboardingID, int LocationID)
        {
            // Response<OnBoardingRequest> response;
            // try
            // {
            OnBoardingRequest obj = new OnBoardingRequest();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@EmpOnboarding_ID", EmpOnboardingID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)

            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {

                var query = "sp_GetHROnboard";
                obj = SqlMapper.Query<OnBoardingRequest>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();

            }
            conn.Close();
            conn.Dispose();
            // if(obj != null)
            // {
            // var track =   GetTracking(obj.EmpOnboardingID);
            // obj.OnboardTrack =  track;
            // }
            // response = new Response<OnBoardingRequest>(obj, 200);
            // }
            // catch(Exception ex)
            // {
            //     response = new Response<OnBoardingRequest>(ex.Message, 500);
            // }
            return obj;
        }
        public List<BPTransInstance> GetTracking(int TenantID, int LocationID, int EmpOnboardingID)
        {
            List<BPTransInstance> objpersonal = new List<BPTransInstance>();
            OnBoardingRequest obj = new OnBoardingRequest();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@EmpOnboarding_ID", EmpOnboardingID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Tenant_ID", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetHROnboardTrack";
                objpersonal = SqlMapper.Query<BPTransInstance>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objpersonal;
        }        
        public Response<OnBoardRequestModelMsg> DeleteHROnboard(int EmpOnboardingID)
        {
            Response<OnBoardRequestModelMsg> response;
            try
            {
            OnBoardRequestModelMsg obj = new OnBoardRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@EmpOnboarding_ID", EmpOnboardingID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {

                var query = "sp_DeleteHROnboard";
                obj = SqlMapper.Query<OnBoardRequestModelMsg>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            response = new Response<OnBoardRequestModelMsg>(obj, 200);
            }
            catch(Exception ex)
            {
                response = new Response<OnBoardRequestModelMsg>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<GetReportManagerBO>> GetOnboardManager(int LocationID, int DeptID, int RoleID, string CategoryName)
        {
            Response<List<GetReportManagerBO>> response;
            try
            {
            List<GetReportManagerBO> obj = new List<GetReportManagerBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Location_ID", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Dept_id", DeptID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Role_ID", RoleID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Category_Name", CategoryName, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)

            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_HRGetOnboardManager";
                obj = SqlMapper.Query<GetReportManagerBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                if(obj.Count == 0)
                {
                    DynamicParameters dyParam1 = new DynamicParameters();
                    dyParam1.Add("@Location_ID", LocationID, DbType.Int32, ParameterDirection.Input);
                    dyParam1.Add("@Dept_id", DeptID, DbType.Int32, ParameterDirection.Input);
                    dyParam1.Add("@Role_ID", RoleID, DbType.Int32, ParameterDirection.Input);
                    dyParam1.Add("@Category_Name", CategoryName, DbType.String, ParameterDirection.Input);
                    var query1 = "sp_HRGetMangerMessage";
                    obj = SqlMapper.Query<GetReportManagerBO>(conn, query1, param: dyParam1, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            conn.Close();
            conn.Dispose();
            
            response = new Response<List<GetReportManagerBO>>(obj, 200);
            }
            catch(Exception ex)
            {
                response = new Response<List<GetReportManagerBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<HRRoleBO>> GetOnboardHR(int TenantID, int ProductID)
        {
            Response<List<HRRoleBO>> response;
            try
            {
            List<HRRoleBO> obj = new List<HRRoleBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@productId", ProductID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)

            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {

                var query = "sp_HRGetOnboardHR";
                obj = SqlMapper.Query<HRRoleBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();

            }
            conn.Close();
            conn.Dispose();
            response = new Response<List<HRRoleBO>>(obj, 200);
            }
            catch(Exception ex)
            {
                response = new Response<List<HRRoleBO>>(ex.Message, 500);
            }
            return response;
        }
        public int SaveHRIDCard(DocIDCardInputBo _objdoc)
        {
            int objUpload = 0;
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Emp_ID", _objdoc.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@EmpIDCard_ID", _objdoc.EmpIDCardID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", _objdoc.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Card_Number", _objdoc.CardNumber, DbType.String, ParameterDirection.Input);           
            dyParam.Add("@Activated_On", _objdoc.ActivatedOn, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@Activated_By", _objdoc.ActivatedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@IDCard_Status", _objdoc.IDCardStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@IDCard_Image", _objdoc.IDCardImage, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveHRIDCard";
                objUpload = SqlMapper.Query<int>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return objUpload;
        }
        public List<DocIDCardInputBo> GetIDCardInfo(int EmpID, int LocationID,int EmpIDCardID)
        {
            List<DocIDCardInputBo> objUpload = new List<DocIDCardInputBo>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Emp_ID", EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@EmpIDCard_ID", EmpIDCardID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetHRIDCard";
                objUpload = SqlMapper.Query<DocIDCardInputBo>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objUpload;
        }
        public List<DocRepositoryBO> GetDocRepository(int tenantId, int productId, DocRepoNameBO typeName)
        {
            List<DocRepositoryBO> rtnval = new List<DocRepositoryBO>();
            DynamicParameters dyParam = new DynamicParameters();
            var descattt =  Description(typeName);
            dyParam.Add("@tenantId", tenantId, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@productId", productId, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@typeName", descattt, DbType.String, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetDocRepository";
                rtnval = SqlMapper.Query<DocRepositoryBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public string Description(Enum typeName)
        {
            var retn = typeName.GetType()
                .GetMember(typeName.ToString())
                .First()
                .GetCustomAttribute<DescriptionAttribute>().Description;
            return retn;
        }
        /* Check List Common Table */
        public int SaveHRCraeteCheckList(ChecklistBO chkList)
        {
            int rtnval = 0;
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Emp_ID", chkList.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Tenant_ID", chkList.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", chkList.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Process_Category", chkList.ProcessCategory, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Entity_Name", chkList.EntityName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Request_Description", chkList.RequestDescription, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Created_By", chkList.CreatedBy, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveHRCraeteCheckList";
                rtnval = SqlMapper.Query<int>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<BPchecklist> GetHRCheckList(BPcheckInputBO chkList)
        {
            List<BPchecklist> rtnval = new List<BPchecklist>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@RefEntity_ID", chkList.RefEntityID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", chkList.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Ref_Entity", chkList.RefEntity, DbType.String, ParameterDirection.Input);
            if(!string.IsNullOrEmpty(chkList.ChkListGroup))
                dyParam.Add("@ChkList_Group", chkList.ChkListGroup, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetHRCheckList";
                rtnval = SqlMapper.Query<BPchecklist>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;

        }
        public int SaveHRCheckList(BPchecklist chkList)
        {
            int rtnval = 0;
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@ChkListIns_ID", chkList.ChkListInsID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Item_Comment", chkList.ItemComments, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Is_Completed", chkList.IsCompleted, DbType.Boolean, ParameterDirection.Input);
            dyParam.Add("@Modified_By", chkList.CreatedBy, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveHRCheckList";
                rtnval = SqlMapper.Query<int>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        /* Work Place Arrangement */
        public int SaveWorkplace(WorkPlaceBO _work)
        {
            int rtnval = 0;
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Emp_ID", _work.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", _work.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Cubicle_No", _work.CubicleNo, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveHRWorkPlace";
                rtnval = SqlMapper.Query<int>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        /* Network Set Up */
        public int SaveNetworkSetUp(NetworkSetupBO _network)
        {
            int rtnval = 0;
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Emp_ID", _network.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", _network.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Network_Domain", _network.NetworkDomain, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Network_UserName", _network.NetworkUserName, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveHRNetworkSetUp";
                rtnval = SqlMapper.Query<int>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }  
        public OnBoardRequestModelMsg SaveHRAttendnceConfig(AttendanceConfigBO attend)
        {
            OnBoardRequestModelMsg rtnval = new OnBoardRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@AttendConfig_ID", attend.AttendConfigID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Emp_ID", attend.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", attend.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Clocking_Mode", attend.ClockingMode, DbType.String, ParameterDirection.Input);
            dyParam.Add("@AttendCard_No", attend.AttendCardNo, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Is_Roaster", attend.IsRoster, DbType.Boolean, ParameterDirection.Input);
            dyParam.Add("@IsIDCard_Linked", attend.IsIDCardLinked, DbType.Boolean, ParameterDirection.Input);
            dyParam.Add("@Created_By", attend.CreatedBy, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveHRAttendnceConfig";
                rtnval = SqlMapper.Query<OnBoardRequestModelMsg>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<AttendanceConfigBO> GetHRAttendnceConfig(int EmpID, int LocationID,int AttendConfigID)
        {
            List<AttendanceConfigBO> rtnval = new List<AttendanceConfigBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@AttendConfig_ID", AttendConfigID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Emp_ID", EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Location_ID", LocationID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetHRAttendnceConfig";
                rtnval = SqlMapper.Query<AttendanceConfigBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public OnBoardRequestModelMsg SaveOnboardExperience(HREmpExperience onExp)
        {
            OnBoardRequestModelMsg rtnval = new OnBoardRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empExpId", onExp.EmpExpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@empId", onExp.EmpID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@companyName", onExp.CompanyName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@designation", onExp.Designation, DbType.String, ParameterDirection.Input);
            dyParam.Add("@durationFrom", onExp.DurationFrom, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@durationTo", onExp.DurationTo, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@yearOfExp", onExp.YearOfExp, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@cTc", onExp.CTC, DbType.Decimal, ParameterDirection.Input);
            dyParam.Add("@workLocation", onExp.WorkLocation, DbType.String, ParameterDirection.Input);
            dyParam.Add("@tenantId", onExp.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@createdBy", onExp.CreatedBy, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveHREmpExperience";
                rtnval = SqlMapper.Query<OnBoardRequestModelMsg>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<HREmpExperience> GetOnboardEmpExperience(int EmpID)
        {
            List<HREmpExperience> obj = new List<HREmpExperience>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empId", EmpID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetHREmpExperience";
                obj = SqlMapper.Query<HREmpExperience>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();

            }
            conn.Close();
            conn.Dispose();
            return obj;
        }
        public Response<OnBoardRequestModelMsg> DeleteEmpExperience(int EmpExpID)
        {
            Response<OnBoardRequestModelMsg> response;
            try
            {
            OnBoardRequestModelMsg obj = new OnBoardRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empExpId", EmpExpID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)

            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_DeleteHREmpExperience";
                obj = SqlMapper.Query<OnBoardRequestModelMsg>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            response = new Response<OnBoardRequestModelMsg>(obj, 200);
            }
            catch(Exception ex)
            {
                response = new Response<OnBoardRequestModelMsg>(ex.Message, 500);
            }
            return response;
        }
        public OnBoardRequestModelMsg SaveBPTransInstance(int TenantID, int LocationID,int Onboardingid)
        {
            OnBoardRequestModelMsg rtnval = new OnBoardRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Onboardingid", Onboardingid, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_HRSaveBPTransInstance";
                rtnval = SqlMapper.Query<OnBoardRequestModelMsg>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public SaveOut SaveOnBoardSettingDragDrop(BPTransaction process)
        {
            SaveOut output = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@bPTransId", process.BPTransID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@bProcessId", process.BProcessID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@bPTransName", process.BPTransName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@beginWhen", process.BeginWhen, DbType.String, ParameterDirection.Input);
            dyParam.Add("@endsWhen", process.EndsWhen, DbType.String, ParameterDirection.Input);
            dyParam.Add("@preCondition", process.PreCondition, DbType.String, ParameterDirection.Input);
            dyParam.Add("@postCondition", process.PostCondition, DbType.String, ParameterDirection.Input);
            dyParam.Add("@businessUnit", process.BusinessUnit, DbType.String, ParameterDirection.Input);
            dyParam.Add("@transStatus", process.TransStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@transOrder", process.TransOrder, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@isMandatory", process.IsMandatory, DbType.Boolean, ParameterDirection.Input);
            dyParam.Add("@businessURL", process.BusinessURL, DbType.String, ParameterDirection.Input);
            dyParam.Add("@prevURL", process.PrevURL, DbType.String, ParameterDirection.Input);
            dyParam.Add("@nxtURL", process.NxtURL, DbType.String, ParameterDirection.Input);
            dyParam.Add("@locationId", process.LocationID, DbType.Int32, ParameterDirection.Input);
            
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveOnBoardSettingsDragDrop";
                output = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return output;
        }
        public int HRDirEmpPersonal(OnboardPersonalDetail onboarding)
        {
            int rtnval = 0;
                DynamicParameters dyParam = new DynamicParameters();
                dyParam.Add("@empId", onboarding.EmpID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@tenantId", onboarding.TenantID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@firstname", onboarding.FirstName, DbType.String, ParameterDirection.Input);
                dyParam.Add("@middlename", onboarding.MiddleName, DbType.String, ParameterDirection.Input);
                dyParam.Add("@lastname", onboarding.LastName, DbType.String, ParameterDirection.Input);
                dyParam.Add("@suffix", onboarding.Suffix, DbType.String, ParameterDirection.Input);
                dyParam.Add("@gender", onboarding.Gender, DbType.String, ParameterDirection.Input);
                dyParam.Add("@dob", onboarding.DOB, DbType.DateTime, ParameterDirection.Input);
                dyParam.Add("@empStatus", onboarding.EmpStatus, DbType.String, ParameterDirection.Input);
                dyParam.Add("@personalmail", onboarding.PersonalMail, DbType.String, ParameterDirection.Input);
                dyParam.Add("@mobileno", onboarding.MobileNo, DbType.String, ParameterDirection.Input);
                dyParam.Add("@alternatephoneno", onboarding.AlternatePhoneNo, DbType.String, ParameterDirection.Input);
                //dyParam.Add("@AppUser_ID", onboarding.AppUserID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@locationId", onboarding.LocationID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@officecontactnumber", onboarding.OfficeContactNumber, DbType.String, ParameterDirection.Input);
                dyParam.Add("@emercontactnumber", onboarding.EmerContactNumber, DbType.String, ParameterDirection.Input);
                dyParam.Add("@emerrelationship", onboarding.EmerRelationShip, DbType.String, ParameterDirection.Input);
                dyParam.Add("@bloodgroup", onboarding.BloodGroup, DbType.String, ParameterDirection.Input);
                dyParam.Add("@maritalstatus", onboarding.MaritalStatus, DbType.String, ParameterDirection.Input);
                dyParam.Add("@nationality", onboarding.Nationality, DbType.String, ParameterDirection.Input);
                dyParam.Add("@phonesuffix", onboarding.PhoneSuffix, DbType.String, ParameterDirection.Input);
                dyParam.Add("@alternatephonesuffix", onboarding.AlternatePhoneSuffix, DbType.String, ParameterDirection.Input);
                dyParam.Add("@iscompensate", onboarding.IsCompensate, DbType.Boolean, ParameterDirection.Input);
                dyParam.Add("@istimesheet", onboarding.IsTimeSheet, DbType.Boolean, ParameterDirection.Input);
                dyParam.Add("@isattendance", onboarding.IsAttendance, DbType.Boolean, ParameterDirection.Input);
                dyParam.Add("@countryId", onboarding.CountryID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@createdby", onboarding.CreatedBy, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@modifiedby", onboarding.ModifiedBy, DbType.Int32, ParameterDirection.Input);
                var conn = this.GetConnection();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (conn.State == ConnectionState.Open)
                {
                    var query = "sp_UpdateHRDirEmployee";
                    rtnval = SqlMapper.Query<int>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
                conn.Close();
                conn.Dispose();
            return rtnval;
        }
    }
}