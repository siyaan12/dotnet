using Microsoft.Extensions.Configuration;
using MyDodos.Domain.AuthBO;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.Auth;
using MyDodos.Repository.Employee;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using KoSoft.Utility;
using System.Collections.Generic;
using MyDodos.ViewModel.Common;
using MyDodos.Domain.AzureStorage;
using KoSoft.DocRepo;
using MyDodos.Repository.TemplateManager;
using MyDodos.Repository.AzureStorage;

namespace MyDodos.Service.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthRepository _authRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDocRepository _docRepository;
        private readonly IStorageConnect _storage;
        public AuthService(IConfiguration configuration, IAuthRepository authRepository,IEmployeeRepository employeeRepository,IDocRepository docRepository,IStorageConnect storage)
        {
            _configuration = configuration;
            _authRepository = authRepository;
            _employeeRepository = employeeRepository;
            _docRepository = docRepository;
            _storage = storage;
        }
        public Response<AuthLoginBO> GetLogin(InputLogin inputLogin)
        {
            Response<AuthLoginBO> response;
            AuthLoginBO loginReturn = new AuthLoginBO();
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), _configuration.GetSection("CentralHubUrl").Value + "/api/Tenant/GetLoginUser"))
                        {
                            inputLogin.ProductKey = _configuration.GetSection("ProductKey").Value;
                            string SendResult = JsonConvert.SerializeObject(inputLogin);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            loginReturn = JsonConvert.DeserializeObject<AuthLoginBO>(data.ToString());

                            if (loginReturn != null)
                            {
                                response = new Response<AuthLoginBO>(loginReturn, 200, "Data Retrieval");
                            }
                            else
                            {
                                response = new Response<AuthLoginBO>(null, 500, "Data Not Retrieval");
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                response = new Response<AuthLoginBO>(ex.Message, 500);
            }
            return response;
        }        
        public Response<UserProfileBO> GetProfile(int appuserid)
        {
            Response<UserProfileBO> response;
            UserProfileBO loginReturn = new UserProfileBO();
            string path = string.Empty;
            string fileName = string.Empty;  
            AzureDocURLBO docURL = new AzureDocURLBO();
            InputLogin inputLogin = new InputLogin();
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), _configuration.GetSection("CentralHubUrl").Value + "/api/Tenant/GetProductUserDetails"))
                        {
                            inputLogin.AppUserID = appuserid;
                            inputLogin.ProdKey = _configuration.GetSection("ProductKey").Value;
                            string SendResult = JsonConvert.SerializeObject(inputLogin);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            loginReturn = JsonConvert.DeserializeObject<UserProfileBO>(data.ToString());
                            if (loginReturn != null)
                            {
                                var objemp = _authRepository.GetLoginEmployee(loginReturn.AppUser.AppuserID, "Employee");
                                if(objemp.Count > 0)
                                {
                                loginReturn.EmployeeDetail =  objemp;
                                var objloc = _authRepository.GetLoginLocation(loginReturn.AppUser.AppuserID, "Location");
                                loginReturn.Location =  objloc; 
                                // var objprofile = _employeeService.GetTenantProfile(loginReturn.TenantDetail.TenantID,loginReturn.TenantDetail.KProductID);
                                // loginReturn.CompanyProfileLogo = objprofile;
                                    var objreturn = _employeeRepository.GetTenantProfileImage(loginReturn.TenantDetail.TenantID,loginReturn.TenantDetail.KProductID);
                                    objreturn.RegularLogobase64Images = "";
                                    objreturn.SmallLogobase64Images = "";
                                    DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                                    var docs = objdoc.GetDocument(0, objreturn.TenantID, "", "TenantImage", objreturn.TenantID);
                                    if (docs.Count > 0)
                                    {
                                        var objcont = objdoc.GetDocContainer(objreturn.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                                        
                                        var doc = _storage.DownloadDocument(new SaveDocCloudBO
                                        {
                                            CloudType = _configuration.GetSection("CloudType").Value,
                                            Container = objcont[0].ContainerName.ToLower(),
                                            fileName = docs[0].GenDocName,
                                            folderPath = docs[0].DirectionPath,
                                            ProductCode = Convert.ToString(loginReturn.TenantDetail.KProductID)
                                        }).Result;
                                        objreturn.RegularLogobase64Images = _storage.ReadDataInUrl(doc.DocumentURL);
                                    }
                                    var documents = objdoc.GetDocument(0, objreturn.TenantID, "", "TenantLogo", objreturn.TenantID);
                                    if (documents.Count > 0)
                                    {
                                        var objconta = objdoc.GetDocContainer(objreturn.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                                        
                                        var docu = _storage.DownloadDocument(new SaveDocCloudBO
                                        {
                                            CloudType = _configuration.GetSection("CloudType").Value,
                                            Container = objconta[0].ContainerName.ToLower(),
                                            fileName = documents[0].GenDocName,
                                            folderPath = documents[0].DirectionPath,
                                            ProductCode = Convert.ToString(loginReturn.TenantDetail.KProductID)
                                        }).Result;
                                        objreturn.SmallLogobase64Images = _storage.ReadDataInUrl(docu.DocumentURL);
                                    }
                                    loginReturn.CompanyProfile = objreturn;
                                foreach(var cur in loginReturn.EmployeeDetail)   
                                {
                                    var currency = _authRepository.GetTenantCurrency(loginReturn.TenantDetail.TenantID,cur.LocationID,true);
                                    loginReturn.Settings = currency;
                                }
                                var mode = _authRepository.GetTenantPaymentMode(loginReturn.TenantDetail.TenantID);
                                loginReturn.TenantPaymentMode = mode;
                                var objyear = _authRepository.GetLoginYear(loginReturn.AppUser.AppuserID, "Year");
                                loginReturn.Year =  objyear;
                                }
                                response = new Response<UserProfileBO>(loginReturn, 200, "Data Retrieval");
                            }
                            else
                            {
                                response = new Response<UserProfileBO>(null, 500, "Data Not Retrieval");
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                response = new Response<UserProfileBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<MUserProfileBO> MGetProfile(int appuserid)
        {
            Response<MUserProfileBO> response;
            MUserProfileBO loginReturn = new MUserProfileBO();
            string path = string.Empty;
            string fileName = string.Empty;  
            AzureDocURLBO docURL = new AzureDocURLBO();
            InputLogin inputLogin = new InputLogin();
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), _configuration.GetSection("CentralHubUrl").Value + "/api/Tenant/GetProductUserDetails"))
                        {
                            inputLogin.AppUserID = appuserid;
                            inputLogin.ProdKey = _configuration.GetSection("ProductKey").Value;
                            string SendResult = JsonConvert.SerializeObject(inputLogin);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            loginReturn = JsonConvert.DeserializeObject<MUserProfileBO>(data.ToString());
                            if (loginReturn != null)
                            {
                                var objemp = _authRepository.GetLoginEmployee(loginReturn.AppUser.AppuserID, "Employee");
                                if(objemp.Count > 0)
                                {
                                loginReturn.EmployeeDetail =  objemp;
                                var objloc = _authRepository.GetLoginLocation(loginReturn.AppUser.AppuserID, "Location");
                                loginReturn.Location =  objloc; 
                                // var objprofile = _employeeService.GetTenantProfile(loginReturn.TenantDetail.TenantID,loginReturn.TenantDetail.KProductID);
                                // loginReturn.CompanyProfileLogo = objprofile;
                                //     var objreturn = _employeeRepository.GetTenantProfileImage(loginReturn.TenantDetail.TenantID,loginReturn.TenantDetail.KProductID);
                                //     objreturn.RegularLogobase64Images = "";
                                //     objreturn.SmallLogobase64Images = "";
                                //     DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                                //     var docs = objdoc.GetDocument(0, objreturn.TenantID, "", "TenantImage", objreturn.TenantID);
                                //     if (docs.Count > 0)
                                //     {
                                //         var objcont = objdoc.GetDocContainer(objreturn.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                                        
                                //         var doc = _storage.DownloadDocument(new SaveDocCloudBO
                                //         {
                                //             CloudType = _configuration.GetSection("CloudType").Value,
                                //             Container = objcont[0].ContainerName.ToLower(),
                                //             fileName = docs[0].GenDocName,
                                //             folderPath = docs[0].DirectionPath,
                                //             ProductCode = Convert.ToString(loginReturn.TenantDetail.KProductID)
                                //         }).Result;
                                //         objreturn.RegularLogobase64Images = _storage.ReadDataInUrl(doc.DocumentURL);
                                //     }
                                //     var documents = objdoc.GetDocument(0, objreturn.TenantID, "", "TenantLogo", objreturn.TenantID);
                                //     if (documents.Count > 0)
                                //     {
                                //         var objconta = objdoc.GetDocContainer(objreturn.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                                        
                                //         var docu = _storage.DownloadDocument(new SaveDocCloudBO
                                //         {
                                //             CloudType = _configuration.GetSection("CloudType").Value,
                                //             Container = objconta[0].ContainerName.ToLower(),
                                //             fileName = documents[0].GenDocName,
                                //             folderPath = documents[0].DirectionPath,
                                //             ProductCode = Convert.ToString(loginReturn.TenantDetail.KProductID)
                                //         }).Result;
                                //         objreturn.SmallLogobase64Images = _storage.ReadDataInUrl(docu.DocumentURL);
                                //     }
                                //     loginReturn.CompanyProfile = objreturn;
                                // foreach(var cur in loginReturn.EmployeeDetail)   
                                // {
                                //     var currency = _authRepository.GetTenantCurrency(loginReturn.TenantDetail.TenantID,cur.LocationID,true);
                                //     loginReturn.Settings = currency;
                                // }
                                // var mode = _authRepository.GetTenantPaymentMode(loginReturn.TenantDetail.TenantID);
                                // loginReturn.TenantPaymentMode = mode;
                                var objyear = _authRepository.GetLoginYear(loginReturn.AppUser.AppuserID, "Year");
                                loginReturn.Year =  objyear;
                                }
                                response = new Response<MUserProfileBO>(loginReturn, 200, "Data Retrieval");
                            }
                            else
                            {
                                response = new Response<MUserProfileBO>(null, 500, "Data Not Retrieval");
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                response = new Response<MUserProfileBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<AuthLoginBO> GetRefreshToken(InputRefresh inputLogin)
        {
            Response<AuthLoginBO> response;
            AuthLoginBO loginReturn = new AuthLoginBO();
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), _configuration.GetSection("CentralHubUrl").Value + "/api/Tenant/RefreshToken"))
                        {
                            inputLogin.ProductKey = _configuration.GetSection("ProductKey").Value;
                            string SendResult = JsonConvert.SerializeObject(inputLogin);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            loginReturn = JsonConvert.DeserializeObject<AuthLoginBO>(data.ToString());
                            if(loginReturn != null)
                            {
                                response = new Response<AuthLoginBO>(loginReturn, 200, "Data Retrieval");
                            }
                            else
                            {
                                response = new Response<AuthLoginBO>(null, 500,"Data Not Retrieval");
                            }
                            
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                response = new Response<AuthLoginBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<LogOutBO> UserLogOut(InputLogOut inputLogin)
        {
            Response<LogOutBO> response;
            LogOutBO loginReturn = new LogOutBO();
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), _configuration.GetSection("CentralHubUrl").Value + "/api/Tenant/SignOut"))
                        {
                            inputLogin.ProductKey = _configuration.GetSection("ProductKey").Value;
                            string SendResult = JsonConvert.SerializeObject(inputLogin);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            loginReturn = JsonConvert.DeserializeObject<LogOutBO>(data.ToString());
                            if (loginReturn != null)
                            {
                                response = new Response<LogOutBO>(loginReturn, 200, "Data Retrieval");
                            }
                            else
                            {
                                response = new Response<LogOutBO>(null, 500, "Data Not Retrieval");
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                response = new Response<LogOutBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> CheckAppUserID(TAppUser AppUser)
        {
            Response<int> response;
            try
            {
                Password_Encrypt_Decrypt objEncDec = new Password_Encrypt_Decrypt();
                string Pass = objEncDec.Encrypt(AppUser.AppUserPassword);
                AppUser.AppUserPassword = Pass;
                var result = _authRepository.CheckAppUserID(AppUser);
                if (result == 0)
                {
                    response = new Response<int>(0, 500, "No User");
                }
                else
                {
                    response = new Response<int>(result, 200);
                }
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<AssociateUserBO>> GetSwitchUsers(int AppUserID)
        {
            Response<List<AssociateUserBO>> response;
            List<AssociateUserBO> loginReturn = new List<AssociateUserBO>();
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("GET"), _configuration.GetSection("CentralHubUrl").Value + "/api/Tenant/GetSwitchUsers/" +AppUserID ))
                        {
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            loginReturn = JsonConvert.DeserializeObject<List<AssociateUserBO>>(data.ToString());
                            if (loginReturn != null)
                            {
                                response = new Response<List<AssociateUserBO>>(loginReturn, 200);
                            }
                            else
                            {
                                response = new Response<List<AssociateUserBO>>(null, 500, "Data Not Found");
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                response = new Response<List<AssociateUserBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<OTPOutputSentBO> SentOtpMail(OTPSentBO _appuser)
        {
            Response<OTPOutputSentBO> response;
            OTPOutputSentBO loginReturn = new OTPOutputSentBO();
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), _configuration.GetSection("CentralHubUrl").Value + "/api/Mail/SentOtpMail"))
                        {
                            _appuser.ProductKey = _configuration.GetSection("ProductKey").Value;
                            string SendResult = JsonConvert.SerializeObject(_appuser);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            loginReturn = JsonConvert.DeserializeObject<OTPOutputSentBO>(data.ToString());
                            if (loginReturn != null)
                            {
                                response = new Response<OTPOutputSentBO>(loginReturn, 200);
                            }
                            else
                            {
                                response = new Response<OTPOutputSentBO>(null, 500, "Data Not Found");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<OTPOutputSentBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<OTPOutputSentBO> CheckOTP(PasswordBO _appuser)
        {
            Response<OTPOutputSentBO> response;
            OTPOutputSentBO loginReturn = new OTPOutputSentBO();
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), _configuration.GetSection("CentralHubUrl").Value + "/api/Tenant/CheckOTP"))
                        {
                           _appuser.ProductKey = _configuration.GetSection("ProductKey").Value;
                            string SendResult = JsonConvert.SerializeObject(_appuser);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            loginReturn = JsonConvert.DeserializeObject<OTPOutputSentBO>(data.ToString());
                            if (loginReturn != null)
                            {
                                response = new Response<OTPOutputSentBO>(loginReturn, 200);
                            }
                            else
                            {
                                response = new Response<OTPOutputSentBO>(null, 500, "Data Not Found");
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                response = new Response<OTPOutputSentBO>(ex.Message, 500);
            }
            return response;
        }        
        // public Response<OTPOutputSentBO> ResetPassword(PasswordBO _appuser)
        // {
        //     Response<OTPOutputSentBO> response;
        //     OTPOutputSentBO loginReturn = new OTPOutputSentBO();
        //     try
        //     {
        //         using (HttpClientHandler clientHandler = new HttpClientHandler())
        //         {
        //             clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        //             using (var httpClient = new HttpClient(clientHandler))
        //             {
        //                 using (var request = new HttpRequestMessage(new HttpMethod("POST"), _configuration.GetSection("CentralHubUrl").Value + "/api/Tenant/ResetPassword"))
        //                 {
        //                     //_appuser.UserName = _configuration.GetSection("ProductKey").Value;
        //                     string SendResult = JsonConvert.SerializeObject(_appuser);
        //                     request.Content = new StringContent(SendResult);
        //                     request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
        //                     var logresponse = httpClient.SendAsync(request).Result;
        //                     var json = logresponse.Content.ReadAsStringAsync().Result;
        //                     var jsondata = JObject.Parse(json);
        //                     var data = jsondata["data"];
        //                     loginReturn = JsonConvert.DeserializeObject<OTPOutputSentBO>(data.ToString());
        //                     if (loginReturn != null)
        //                     {
        //                         response = new Response<OTPOutputSentBO>(loginReturn, 200);
        //                     }
        //                     else
        //                     {
        //                         response = new Response<OTPOutputSentBO>(null, 500, "Data Not Found");
        //                     }
        //                 }
        //             }

        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         response = new Response<OTPOutputSentBO>(ex.Message, 500);
        //     }
        //     return response;
        // }
        public Response<CognitoReturn> ForgetPassword(ResetPassword _appuser)
        {
            Response<CognitoReturn> response;
            CognitoReturn loginReturn = new CognitoReturn();
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), _configuration.GetSection("CentralHubUrl").Value + "/api/Mail/ForgetPassword"))
                        {
                            _appuser.ProductKey = _configuration.GetSection("ProductKey").Value;
                            string SendResult = JsonConvert.SerializeObject(_appuser);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            loginReturn = JsonConvert.DeserializeObject<CognitoReturn>(data.ToString());
                            if (loginReturn != null)
                            {
                                response = new Response<CognitoReturn>(loginReturn, 200);
                            }
                            else
                            {
                                response = new Response<CognitoReturn>(null, 500, "Data Not Found");
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                response = new Response<CognitoReturn>(ex.Message, 500);
            }
            return response;
        }
        public Response<CognitoReturn> NewResetPassword(ResetPassword _appuser)
        {
            Response<CognitoReturn> response;
            CognitoReturn loginReturn = new CognitoReturn();
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), _configuration.GetSection("CentralHubUrl").Value + "/api/Mail/ResetPassword"))
                        {
                            _appuser.ProductKey = _configuration.GetSection("ProductKey").Value;
                            string SendResult = JsonConvert.SerializeObject(_appuser);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            loginReturn = JsonConvert.DeserializeObject<CognitoReturn>(data.ToString());
                            if (loginReturn != null)
                            {
                                response = new Response<CognitoReturn>(loginReturn, 200);
                            }
                            else
                            {
                                response = new Response<CognitoReturn>(null, 500, "Data Not Found");
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                response = new Response<CognitoReturn>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> SaveDemoRequest(MDemoRequest request)
        {
            Response<SaveOut> response;
            try
            {
                var result = _authRepository.SaveDemoRequest(request);
                if (result.Id == 0)
                {
                    response = new Response<SaveOut>(null, 200, "Save Falied");
                }
                else
                {
                    response = new Response<SaveOut>(result, 200);
                }
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> SaveDetailRequest(MDetailRequest request)
        {
            Response<SaveOut> response;
            try
            {
                var result = _authRepository.SaveDetailRequest(request);
                if (result.Id == 0)
                {
                    response = new Response<SaveOut>(null, 200, "Save Falied");
                }
                else
                {
                    response = new Response<SaveOut>(result, 200);
                }
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message, 500);
            }
            return response;
        }
        public Response<CognitoReturn> MForgetPassword(ResetPassword _appuser)
        {
            Response<CognitoReturn> response;
            CognitoReturn loginReturn = new CognitoReturn();
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), _configuration.GetSection("CentralHubUrl").Value + "/api/Mail/ForgetPassword"))
                        {
                            _appuser.ProductKey = _configuration.GetSection("ProductKey").Value;
                            string SendResult = JsonConvert.SerializeObject(_appuser);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            loginReturn = JsonConvert.DeserializeObject<CognitoReturn>(data.ToString());
                            if (loginReturn != null)
                            {
                                response = new Response<CognitoReturn>(loginReturn, 200);
                            }
                            else
                            {
                                response = new Response<CognitoReturn>(null, 500, "Data Not Found");
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                response = new Response<CognitoReturn>(ex.Message, 500);
            }
            return response;
        }
        public Response<CognitoReturn> MNewResetPassword(ResetPassword _appuser)
        {
            Response<CognitoReturn> response;
            CognitoReturn loginReturn = new CognitoReturn();
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), _configuration.GetSection("CentralHubUrl").Value + "/api/Mail/ResetPassword"))
                        {
                            _appuser.ProductKey = _configuration.GetSection("ProductKey").Value;
                            string SendResult = JsonConvert.SerializeObject(_appuser);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            loginReturn = JsonConvert.DeserializeObject<CognitoReturn>(data.ToString());
                            if (loginReturn != null)
                            {
                                response = new Response<CognitoReturn>(loginReturn, 200);
                            }
                            else
                            {
                                response = new Response<CognitoReturn>(null, 500, "Data Not Found");
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                response = new Response<CognitoReturn>(ex.Message, 500);
            }
            return response;
        }
    }
}
