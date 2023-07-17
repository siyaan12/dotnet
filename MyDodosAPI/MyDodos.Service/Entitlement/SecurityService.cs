using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.Entitlement;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace MyDodos.Service.Entitlement
{
    public class SecurityService : ISecurityService
    {
        //private ISecurityRepository _securityRepository;
        private readonly IConfiguration configuration;

        public SecurityService(IConfiguration configuration)
        {
            //_securityRepository = securityRepository;
            //_logger = logger;
            this.configuration = configuration;
        }
        /*  public Response<AppUserBO> GetAppUser(string UserName, string sPass)
         {
             Response<AppUserBO> response;
             try
             {
                 var token = _securityRepository.GetAppUser(UserName, sPass);
                 response = new Response<AppUserBO>(token);
             }
             catch (Exception ex)
             {
                 response = new Response<AppUserBO>(ex.Message,500);
             }
             return response;
         } */
        public Response<List<UserTyepBO>> GetUserTypeRoles(int TenantID)
        {

            UserTypeInput input = new UserTypeInput();
            input.TenantID = TenantID;
            Response<List<UserTyepBO>> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), configuration.GetSection("CentralHubUrl").Value + "/api/Usertype/GetUserType"))
                        {
                            input.ProductID = Convert.ToInt32(configuration.GetSection("ProductID").Value);
                            string SendResult = JsonConvert.SerializeObject(input);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            var jsons = JsonConvert.DeserializeObject<List<UserTyepBO>>(data.ToString());
                            response = new Response<List<UserTyepBO>>(jsons, 200);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<UserTyepBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<EntServicesBO>> GetRoleToMenuServices(int RoleID)
        {

            UserRoleBO input = new UserRoleBO();
            input.RoleID = RoleID;
            Response<List<EntServicesBO>> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), configuration.GetSection("CentralHubUrl").Value + "/api/UserService/GetRoleToMenuServices"))
                        {
                            input.ProductID = Convert.ToInt32(configuration.GetSection("ProductID").Value);
                            string SendResult = JsonConvert.SerializeObject(input);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            var jsons = JsonConvert.DeserializeObject<List<EntServicesBO>>(data.ToString());
                            response = new Response<List<EntServicesBO>>(jsons, 200);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<EntServicesBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<EntServicesBO>> GetUserTypeToServices(int UserTypeID)
        {

            UserRoleBO input = new UserRoleBO();
            input.UserTypeID = UserTypeID;
            Response<List<EntServicesBO>> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), configuration.GetSection("CentralHubUrl").Value + "/api/UserService/GetUserTypeServices"))
                        {
                            input.ProductID = Convert.ToInt32(configuration.GetSection("ProductID").Value);
                            string SendResult = JsonConvert.SerializeObject(input);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            var jsons = JsonConvert.DeserializeObject<List<EntServicesBO>>(data.ToString());
                            response = new Response<List<EntServicesBO>>(jsons, 200);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<EntServicesBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<string> AddRoleServices(UserRoleBO input)
        {
            Response<string> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), configuration.GetSection("CentralHubUrl").Value + "/api/UserRole/AddRoleServices"))
                        {
                            input.ProductID = Convert.ToInt32(configuration.GetSection("ProductID").Value);
                            string SendResult = JsonConvert.SerializeObject(input);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            response = new Response<string>(data.ToString(), 200);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<string>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<EntServicesBO>> GetExceptUserTypeServices(int UserTypeID, int RoleID)
        {

            UserTypeInput input = new UserTypeInput();
            input.UserTypeID = UserTypeID;
            input.RoleId = RoleID;
            Response<List<EntServicesBO>> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), configuration.GetSection("CentralHubUrl").Value + "/api/UserService/ExceptUserTypeServices"))
                        {
                            input.ProductID = Convert.ToInt32(configuration.GetSection("ProductID").Value);
                            string SendResult = JsonConvert.SerializeObject(input);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            var jsons = JsonConvert.DeserializeObject<List<EntServicesBO>>(data.ToString());
                            response = new Response<List<EntServicesBO>>(jsons, 200);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<EntServicesBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<UserTyepBO>> GetUserTypeList(int ProductID,int TenantID, int AppUserID)
        {
            UserTypeInput input = new UserTypeInput();
            input.TenantID = TenantID;
            input.AppUserID = AppUserID;
            input.ProductID = ProductID;
            Response<List<UserTyepBO>> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), configuration.GetSection("CentralHubUrl").Value + "/api/Usertype/GetUserTypeList"))
                        {
                            //input.ProductID = Convert.ToInt32(configuration.GetSection("ProductID").Value);
                            string SendResult = JsonConvert.SerializeObject(input);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            var jsons = JsonConvert.DeserializeObject<List<UserTyepBO>>(data.ToString());
                            response = new Response<List<UserTyepBO>>(jsons, 200);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<UserTyepBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<UserTypeServiceListSearch> GetUserTypeServiceSearch(UserTypeServiceListSearch input)
        {
            Response<UserTypeServiceListSearch> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), configuration.GetSection("CentralHubUrl").Value + "/api/UserService/GetUserTypeServiceSearch"))
                        {
                            input.UserTypeParams.ProductID = Convert.ToInt32(configuration.GetSection("ProductID").Value);
                            string SendResult = JsonConvert.SerializeObject(input);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            var Services = JsonConvert.DeserializeObject<UserTypeServiceListSearch>(data.ToString());
                            Services.UserTypeParams = null;
                            response = new Response<UserTypeServiceListSearch>(Services, 200);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<UserTypeServiceListSearch>(ex.Message, 500);
            }
            return response;
        }
        public Response<vwUserGroupListSearch> GetEntUserGroupList(vwUserGroupListSearch input)
        {
            Response<vwUserGroupListSearch> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), configuration.GetSection("CentralHubUrl").Value + "/api/UserGroup/GetEntUserGroupList"))
                        {
                            input.UserGroupParams.ProductID = Convert.ToInt32(configuration.GetSection("ProductID").Value);
                            string SendResult = JsonConvert.SerializeObject(input);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            var Groups = JsonConvert.DeserializeObject<vwUserGroupListSearch>(data.ToString());
                            Groups.UserGroupParams = null;
                            response = new Response<vwUserGroupListSearch>(Groups, 200);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<vwUserGroupListSearch>(ex.Message, 500);
            }
            return response;
        }
        public Response<string> CreateUserGroup(UserGroupInput input)
        {
            Response<string> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), configuration.GetSection("CentralHubUrl").Value + "/api/UserGroup/CreateUserGroup"))
                        {
                            input.ProductID = Convert.ToInt32(configuration.GetSection("ProductID").Value);
                            string SendResult = JsonConvert.SerializeObject(input);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            response = new Response<string>(data.ToString(), 200);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<string>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<EntServicesBO>> GetUserGroupServices(EntUserGroupMembersBO input)
        {
            Response<List<EntServicesBO>> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), configuration.GetSection("CentralHubUrl").Value + "/api/UserService/GetUserGroupServices"))
                        {
                            input.ProductID = Convert.ToInt32(configuration.GetSection("ProductID").Value);
                            string SendResult = JsonConvert.SerializeObject(input);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            var jsons = JsonConvert.DeserializeObject<List<EntServicesBO>>(data.ToString());
                            response = new Response<List<EntServicesBO>>(jsons, 200);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<EntServicesBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<VwEntAppUserBO>> GetEntUserGroupMembers(EntUserGroupMembersBO input)
        {
            Response<List<VwEntAppUserBO>> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), configuration.GetSection("CentralHubUrl").Value + "/api/AppUser/GetEntUserGroupMembers"))
                        {
                            input.ProductID = Convert.ToInt32(configuration.GetSection("ProductID").Value);
                            string SendResult = JsonConvert.SerializeObject(input);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            var jsons = JsonConvert.DeserializeObject<List<VwEntAppUserBO>>(data.ToString());
                            response = new Response<List<VwEntAppUserBO>>(jsons, 200);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<VwEntAppUserBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<string> AddUserGroupMember(EntUserGroupMembers input)
        {
            Response<string> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), configuration.GetSection("CentralHubUrl").Value + "/api/UserGroup/AddUserGroupMember"))
                        {
                            string SendResult = JsonConvert.SerializeObject(input);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            response = new Response<string>(data.ToString(), 200);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<string>(ex.Message, 500);
            }
            return response;
        }
        public Response<string> DeleteUserGroupMember(int UserGroupID, int AppUserID)
        {
            Response<string> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("DELETE"), configuration.GetSection("CentralHubUrl").Value + "/api/UserGroup/DeleteUserGroupMember/" + UserGroupID + "/" + AppUserID))
                        {
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            response = new Response<string>(data.ToString(), 200);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<string>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<AppUserDetailsBO>> CreateUser(List<ActivateUserBO> input)
        {
            //input.ProductID = Convert.ToInt32(configuration.GetSection("ProductID").Value);
            // var CloneUser = _securityRepository.AddAppUser(input);
            Response<List<AppUserDetailsBO>> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), configuration.GetSection("CentralHubUrl").Value + "/api/Tenant/CreateUser"))
                        {
                            string SendResult = JsonConvert.SerializeObject(input);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            var Msg = jsondata["message"];
                            var jsons = JsonConvert.DeserializeObject<List<AppUserDetailsBO>>(data.ToString());
                            response = new Response<List<AppUserDetailsBO>>(jsons, 200, Msg.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<AppUserDetailsBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<VwAppUserListSearch> GetEntAppUserList(VwAppUserListSearch input)
        {
            Response<VwAppUserListSearch> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), configuration.GetSection("CentralHubUrl").Value + "/api/AppUser/GetAppUserList"))
                        {
                            input.AppUserParams.ProductId = Convert.ToInt32(configuration.GetSection("ProductID").Value);
                            string SendResult = JsonConvert.SerializeObject(input);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            var Groups = JsonConvert.DeserializeObject<VwAppUserListSearch>(data.ToString());
                            Groups.AppUserParams = null;
                            response = new Response<VwAppUserListSearch>(Groups, 200);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<VwAppUserListSearch>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<EntServicesBO>> GetAssocUserTypeServices(UserTypeInput input)
        {
            Response<List<EntServicesBO>> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), configuration.GetSection("CentralHubUrl").Value + "/api/UserService/GetAssociateRoleServices"))
                        {
                            input.ProductID = Convert.ToInt32(configuration.GetSection("ProductID").Value);
                            string SendResult = JsonConvert.SerializeObject(input);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            var jsons = JsonConvert.DeserializeObject<List<EntServicesBO>>(data.ToString());
                            response = new Response<List<EntServicesBO>>(jsons, 200);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<EntServicesBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<EntServicesBO>> GetAssocUserGroupServices(int UserGroupID)
        {
            EntUserGroupMembersBO input = new EntUserGroupMembersBO();
            input.UserGroupID = UserGroupID;
            Response<List<EntServicesBO>> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), configuration.GetSection("CentralHubUrl").Value + "/api/UserService/GetAssocateUserGroupServices"))
                        {
                            input.ProductID = Convert.ToInt32(configuration.GetSection("ProductID").Value);
                            string SendResult = JsonConvert.SerializeObject(input);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            var jsons = JsonConvert.DeserializeObject<List<EntServicesBO>>(data.ToString());
                            response = new Response<List<EntServicesBO>>(jsons, 200);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<EntServicesBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<string> UpdateUserPassword(ActivateUserBO input)
        {
            Response<string> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("PUT"), configuration.GetSection("CentralHubUrl").Value + "/api/AppUser/UpdateUserPassword"))
                        {
                            input.ProductID = Convert.ToInt32(configuration.GetSection("ProductID").Value);
                            string SendResult = JsonConvert.SerializeObject(input);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            //var ChangePassword = _securityRepository.UpdateUserPassword(input);
                            response = new Response<string>(data.ToString(), 200);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<string>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> SwitchUserStatus(ActivateUserBO input)
        {
            Response<int> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), configuration.GetSection("CentralHubUrl").Value + "/api/AppUser/SwitchUserStatus"))
                        {
                            input.ProductID = Convert.ToInt32(configuration.GetSection("ProductID").Value);
                            string SendResult = JsonConvert.SerializeObject(input);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            response = new Response<int>(Convert.ToInt32(data.ToString()), 200);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<string> RemoveRoleService(UserTypeInput input)
        {
            Response<string> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), configuration.GetSection("CentralHubUrl").Value + "/api/UserRole/RemoveRoleService"))
                        {
                            input.ProductID = Convert.ToInt32(configuration.GetSection("ProductID").Value);
                            string SendResult = JsonConvert.SerializeObject(input);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            response = new Response<string>(data.ToString(), 200);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<string>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<VwTblEntServices>> GetAppUserServiceList(int UserID)
        {
            int ProductID = Convert.ToInt32(configuration.GetSection("ProductID").Value);
            Response<List<VwTblEntServices>> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("GET"), configuration.GetSection("CentralHubUrl").Value + "/api/AppUser/GetAppUserServiceList/" + UserID + "/" + ProductID))
                        {
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            var jsons = JsonConvert.DeserializeObject<List<VwTblEntServices>>(data.ToString());
                            response = new Response<List<VwTblEntServices>>(jsons, 200);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<VwTblEntServices>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<UserServiceExceptionBO>> GetUserServiceException(int UserID)
        {
            int ProductID = Convert.ToInt32(configuration.GetSection("ProductID").Value);
            Response<List<UserServiceExceptionBO>> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("GET"), configuration.GetSection("CentralHubUrl").Value + "/api/AppUser/GetUserServiceException/" + UserID + "/" + ProductID))
                        {
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            var jsons = JsonConvert.DeserializeObject<List<UserServiceExceptionBO>>(data.ToString());
                            response = new Response<List<UserServiceExceptionBO>>(jsons, 200);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<UserServiceExceptionBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<string> AddUserServiceException(ExceptionBO input)
        {
            Response<string> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), configuration.GetSection("CentralHubUrl").Value + "/api/AppUser/AddUserServiceException"))
                        {
                            string SendResult = JsonConvert.SerializeObject(input);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            response = new Response<string>(data.ToString(), 200);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<string>(ex.Message, 500);
            }
            return response;
        }
        public Response<string> DeleteUserServiceException(int ExceptionID)
        {
            Response<string> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("DELETE"), configuration.GetSection("CentralHubUrl").Value + "/api/AppUser/DeleteUserServiceException/" + ExceptionID))
                        {
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            response = new Response<string>(data.ToString(), 200);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<string>(ex.Message, 500);
            }
            return response;
        }
        public Response<string> DeleteUserRole(int RoleID, int TenantID)
        {
            UserRoleBO input = new UserRoleBO();
            input.RoleID = RoleID;
            input.TenantId = TenantID;
            Response<string> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), configuration.GetSection("CentralHubUrl").Value + "/api/UserRole/DeleteUserRole"))
                        {
                            input.ProductID = Convert.ToInt32(configuration.GetSection("ProductID").Value);
                            string SendResult = JsonConvert.SerializeObject(input);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            var Msg = jsondata["message"];
                            response = new Response<string>(data.ToString(), 200, Msg.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<string>(ex.Message, 500);
            }
            return response;
        }
        public Response<string> ChangeUserGroupStatus(UserGroupInput input)
        {
            Response<string> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), configuration.GetSection("CentralHubUrl").Value + "/api/UserGroup/SwitchUserGroupStatus"))
                        {
                            input.ProductID = Convert.ToInt32(configuration.GetSection("ProductID").Value);
                            string SendResult = JsonConvert.SerializeObject(input);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            var Msg = jsondata["message"];
                            response = new Response<string>(data.ToString(), 200, Msg.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<string>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<VwEntAppUserList>> GetAppUserInfo(int AppUserID)
        {
            Response<List<VwEntAppUserList>> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("GET"), configuration.GetSection("CentralHubUrl").Value + "/api/AppUser/GetAppUserInfo/" + AppUserID))
                        {
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            var jsons = JsonConvert.DeserializeObject<List<VwEntAppUserList>>(data.ToString());
                            response = new Response<List<VwEntAppUserList>>(jsons, 200);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<VwEntAppUserList>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<EntSubscribedProdMap>> GetEntSubscribedProdMap(int TenantID, int ProductID)
        {
            ProductID = Convert.ToInt32(configuration.GetSection("ProductID").Value);
            Response<List<EntSubscribedProdMap>> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("GET"), configuration.GetSection("CentralHubUrl").Value + "/api/Tenant/GetEntSubscribedProdMap/" + TenantID + "/" + ProductID))
                        {
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            var jsons = JsonConvert.DeserializeObject<List<EntSubscribedProdMap>>(data.ToString());
                            response = new Response<List<EntSubscribedProdMap>>(jsons, 200);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<EntSubscribedProdMap>>(ex.Message, 500);
            }
            return response;
        }
        public Response<string> DeleteUserGroup(UserGroupInput input)
        {
            Response<string> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), configuration.GetSection("CentralHubUrl").Value + "/api/UserGroup/DeleteUserGroup"))
                        {
                            input.ProductID = Convert.ToInt32(configuration.GetSection("ProductID").Value);
                            string SendResult = JsonConvert.SerializeObject(input);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            var Msg = jsondata["message"];
                            response = new Response<string>(data.ToString(), 200, Msg.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<string>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<EntSubscribedProdMap>> GetEntSubscribedList(int AssocTenantID , int AppUserID , int ProductID)
        {
            if(ProductID == 0)
            {
                ProductID = Convert.ToInt32(configuration.GetSection("ProductID").Value);
            }
            Response<List<EntSubscribedProdMap>> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("GET"), configuration.GetSection("CentralHubUrl").Value + "/api/Tenant/GetSubscribedUserList/" + AssocTenantID + "/" + AppUserID + "/" + ProductID))
                        {
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            var jsons = JsonConvert.DeserializeObject<List<EntSubscribedProdMap>>(data.ToString());
                            response = new Response<List<EntSubscribedProdMap>>(jsons);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<EntSubscribedProdMap>>(ex.Message);
            }
            return response;
        }
    }
}
