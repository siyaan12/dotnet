using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.AuthBO;
using MyDodos.Domain.Document;
using MyDodos.Domain.LoginBO;
using MyDodos.ViewModel.Document;
using MyDodos.Domain.Wrapper;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MyDodos.Domain.HR;
using MyDodos.Domain.Administrative;
using MyDodos.ViewModel.Entitlement;
using MyDodos.ViewModel.Common;
using MyDodos.ViewModel.Employee;
using MyDodos.Domain.Employee;
using MyDodos.Domain.Master;

namespace MyDodos.Repository.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration _configuration;
        public AuthRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public Response<AppUserDetailsBO> AddProfile(InputAppUserBO inputLogin)
        {
            Response<AppUserDetailsBO> response;
            AppUserDetailsBO loginReturn = new AppUserDetailsBO();
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), _configuration.GetSection("CentralHubUrl").Value + "/api/Tenant/UserActivation"))
                        {
                            //inputLogin.ProductKey = _configuration.GetSection("ProductKey").Value;
                            string SendResult = JsonConvert.SerializeObject(inputLogin);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            loginReturn = JsonConvert.DeserializeObject<AppUserDetailsBO>(data.ToString());

                            if (loginReturn != null)
                            {
                                response = new Response<AppUserDetailsBO>(loginReturn, 200, "Data Retrieval");
                            }
                            else
                            {
                                response = new Response<AppUserDetailsBO>(null, 500, "Data Not Retrieval");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<AppUserDetailsBO>(ex.Message, 500);
            }
            return response;
        }
        public List<LoginEmployeeBO> GetLoginEmployee(int AppUserID, string loginType)
        {
            // Response<List<loginBO>> response;
            // try
            // {
            List<LoginEmployeeBO> obj = new List<LoginEmployeeBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@appUserId", AppUserID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@logType", loginType, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetLoginEmployee";
                obj = SqlMapper.Query<LoginEmployeeBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            // response = new Response<List<loginBO>>(obj, 200, "Data Retrieval");
            // }
            // catch (Exception ex)
            // {
            //     response = new Response<List<loginBO>>(ex.Message, 500);
            // }
            return obj;
        }
        public List<LoginLocationBO> GetLoginLocation(int AppUserID, string loginType)
        {
            // Response<List<loginBO>> response;
            // try
            // {
            List<LoginLocationBO> obj = new List<LoginLocationBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@appUserId", AppUserID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@logType", loginType, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetLoginEmployee";
                obj = SqlMapper.Query<LoginLocationBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            // response = new Response<List<loginBO>>(obj, 200, "Data Retrieval");
            // }
            // catch (Exception ex)
            // {
            //     response = new Response<List<loginBO>>(ex.Message, 500);
            // }
            return obj;
        }
        public List<LoginYearBO> GetLoginYear(int AppUserID, string loginType)
        {
            // Response<List<loginBO>> response;
            // try
            // {
            List<LoginYearBO> obj = new List<LoginYearBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@appUserId", AppUserID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@logType", loginType, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetLoginEmployee";
                obj = SqlMapper.Query<LoginYearBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            // response = new Response<List<loginBO>>(obj, 200, "Data Retrieval");
            // }
            // catch (Exception ex)
            // {
            //     response = new Response<List<loginBO>>(ex.Message, 500);
            // }
            return obj;
        }
        public List<GenDocumentBO> GetDocument(int docId, int entityId, string docKey, string entity, int tenantId)
        {
            List<GenDocumentBO> rtnval = new List<GenDocumentBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@docId", docId, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@entityId", entityId, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@docKey", docKey, DbType.String, ParameterDirection.Input);
            dyParam.Add("@entity", entity, DbType.String, ParameterDirection.Input);
            dyParam.Add("@tenantId", tenantId, DbType.Int32, ParameterDirection.Input);


            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_Getdocinfo";
                rtnval = SqlMapper.Query<GenDocumentBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public List<DocContainerBO> GetDocContainer(int TenantID, int ProductID)
        {
            List<DocContainerBO> rtnval = new List<DocContainerBO>();
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
                var query = "sp_GetDocContanier";
                rtnval = SqlMapper.Query<DocContainerBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public int CheckAppUserID(TAppUser AppUser)
        {
            int rtnval = 0;
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@appUserName", AppUser.AppUserName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@appUserPassword", AppUser.AppUserPassword, DbType.String, ParameterDirection.Input);
            dyParam.Add("@tenantId", AppUser.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@authPIN", AppUser.AuthPIN, DbType.String, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_CheckUserData";
                rtnval = SqlMapper.Query<int>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public Response<List<RoleBO>> GetEntRoles(int ProductID, string GroupType)
        {
            Response<List<RoleBO>> response;
            List<RoleBO> loginReturn = new List<RoleBO>();
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("GET"), _configuration.GetSection("CentralHubUrl").Value + "/api/Tenant/GetEntRoles/" + ProductID + "/" + GroupType))
                        {
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            loginReturn = JsonConvert.DeserializeObject<List<RoleBO>>(data.ToString());
                            if (loginReturn != null)
                            {
                                response = new Response<List<RoleBO>>(loginReturn, 200);
                            }
                            else
                            {
                                response = new Response<List<RoleBO>>(null, 500, "Data Not Found");
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                response = new Response<List<RoleBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<EntRolesBO>> GetEntTenantRoles(InpurtEntRolesBO roles)
        {
            Response<List<EntRolesBO>> response;
            List<EntRolesBO> loginReturn = new List<EntRolesBO>();
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), _configuration.GetSection("CentralHubUrl").Value + "/api/Tenant/GetEntTenantRoles"))
                        {
                            string SendResult = JsonConvert.SerializeObject(roles);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            loginReturn = JsonConvert.DeserializeObject<List<EntRolesBO>>(data.ToString());
                            if (loginReturn != null)
                            {
                                response = new Response<List<EntRolesBO>>(loginReturn, 200);
                            }
                            else
                            {
                                response = new Response<List<EntRolesBO>>(null, 500, "Data Not Found");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<EntRolesBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<RtnUserGroupBO>> GetAccountTypes(int ProductID, int TenantID)
        {
            Response<List<RtnUserGroupBO>> response;
            List<RtnUserGroupBO> loginReturn = new List<RtnUserGroupBO>();
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("GET"), _configuration.GetSection("CentralHubUrl").Value + "/api/Tenant/GetAccountTypes/" + ProductID + "/" + TenantID))
                        {
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            loginReturn = JsonConvert.DeserializeObject<List<RtnUserGroupBO>>(data.ToString());
                            if (loginReturn != null)
                            {
                                response = new Response<List<RtnUserGroupBO>>(loginReturn, 200);
                            }
                            else
                            {
                                response = new Response<List<RtnUserGroupBO>>(null, 500, "Data Not Found");
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                response = new Response<List<RtnUserGroupBO>>(ex.Message, 500);
            }
            return response;
        }
        public List<GeneralConfigSettings> GetTenantCurrency(int TenantID, int LocationID, bool IsActive)
        {
            List<GeneralConfigSettings> objrange = new List<GeneralConfigSettings>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@isActive", IsActive, DbType.Boolean, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetGeneralConfigSettings";
                objrange = SqlMapper.Query<GeneralConfigSettings>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return objrange;
        }
        public SaveOut SaveDemoRequest(MDemoRequest request)
        {
            SaveOut rtnval = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@requestId", request.RequestID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@requesterName", request.RequesterName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@designation", request.Designation, DbType.String, ParameterDirection.Input);
            dyParam.Add("@companyName", request.CompanyName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@mobileNo", request.MobileNo, DbType.String, ParameterDirection.Input);
            dyParam.Add("@emailId", request.EmailID, DbType.String, ParameterDirection.Input);
            dyParam.Add("@interestModules", request.InterestModules, DbType.String, ParameterDirection.Input);
            dyParam.Add("@createdBy", request.CreatedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@modifiedBy", request.ModifiedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@productId", request.ProductID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@productKey", request.ProductKey, DbType.String, ParameterDirection.Input);


            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveDemoRequest";
                rtnval = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }
        public SaveOut SaveDetailRequest(MDetailRequest request)
        {
            SaveOut rtnval = new SaveOut();
            DynamicParameters dyParam = new DynamicParameters();

            dyParam.Add("@detailRequestId", request.DetailRequestID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@firstName", request.FirstName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@lastName", request.LastName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@mobileNo", request.MobileNo, DbType.String, ParameterDirection.Input);
            dyParam.Add("@emailId", request.EmailID, DbType.String, ParameterDirection.Input);
            dyParam.Add("@createdBy", request.CreatedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@modifiedBy", request.ModifiedBy, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@productId", request.ProductID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@productKey", request.ProductKey, DbType.String, ParameterDirection.Input);


            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveDetailRequest";
                rtnval = SqlMapper.Query<SaveOut>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return rtnval;
        }

        public string UpdateCenEntAppUser(EmployeeProfileInputBO objuser)
        {
            string data = "";
            using (HttpClientHandler clientHandler = new HttpClientHandler())
            {
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var httpClient = new HttpClient(clientHandler))
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("POST"), _configuration.GetSection("CentralHubUrl").Value + "/api/Tenant/UpdateAppuserInfo"))
                    {
                        string SendResult = JsonConvert.SerializeObject(objuser);
                        request.Content = new StringContent(SendResult);
                        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                        var logresponse = httpClient.SendAsync(request).Result;
                        var json = logresponse.Content.ReadAsStringAsync().Result;
                        var jsondata = JObject.Parse(json);
                        data = jsondata["data"].ToString();
                    }
                }
            }
            return data;
        }
        public string SaveTenantProfile(TenantProfiledataBO objuser)
        {
            string data = "";
            using (HttpClientHandler clientHandler = new HttpClientHandler())
            {
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var httpClient = new HttpClient(clientHandler))
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("POST"), _configuration.GetSection("CentralHubUrl").Value + "/api/Tenant/SaveTenantProfile"))
                    {
                        string SendResult = JsonConvert.SerializeObject(objuser);
                        request.Content = new StringContent(SendResult);
                        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                        var logresponse = httpClient.SendAsync(request).Result;
                        var json = logresponse.Content.ReadAsStringAsync().Result;
                        var jsondata = JObject.Parse(json);
                        data = jsondata["data"].ToString();
                    }
                }
            }
            return data;
        }
        public TenantPaymentModeBO GetTenantPaymentMode(int TenantID)
        {
            TenantPaymentModeBO objrange = new TenantPaymentModeBO();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);

            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetTenantPaymentMode";
                objrange = SqlMapper.Query<TenantPaymentModeBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();
            return objrange;
        }
    }
}