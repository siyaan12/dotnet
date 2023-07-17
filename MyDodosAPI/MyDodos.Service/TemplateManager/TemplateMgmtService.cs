using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.TemplateManager;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace MyDodos.Service.TemplateManager
{
    public class TemplateMgmtService : ITemplateMgmtService
    {
        private readonly IConfiguration _configuration;
        
        public TemplateMgmtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Response<AllTemplates> GetAllTemplate(AllTemplates allTemplates)
        {
            Response<AllTemplates> response;
            AllTemplates loginReturn = new AllTemplates();
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), _configuration.GetSection("TemplateMgmtUrl").Value + "/api/Template/GetAllTemplate"))
                        {
                            //inputLogin.ProductKey = _configuration.GetSection("ProductKey").Value;
                            string SendResult = JsonConvert.SerializeObject(allTemplates);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            //var data = jsondata["data"];
                            loginReturn = JsonConvert.DeserializeObject<AllTemplates>(jsondata.ToString());
                            if (loginReturn != null)
                            {
                                response = new Response<AllTemplates>(loginReturn, 200, "Data Retrieval");
                            }
                            else
                            {
                                response = new Response<AllTemplates>(null, 500, "Data Not Retrieval");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<AllTemplates>(ex.Message, 500);
            }
            return response;
        }
        public Response<tblTemplateManagement> CreateTemplates(tblTemplateManagement template)
        {
            Response<tblTemplateManagement> response;
            tblTemplateManagement loginReturn = new tblTemplateManagement();
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), _configuration.GetSection("TemplateMgmtUrl").Value + "/api/Template/CreateTemplates"))
                        {
                            //inputLogin.ProductKey = _configuration.GetSection("ProductKey").Value;
                            string SendResult = JsonConvert.SerializeObject(template);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            loginReturn = JsonConvert.DeserializeObject<tblTemplateManagement>(data.ToString());
                            if (loginReturn != null)
                            {
                                response = new Response<tblTemplateManagement>(loginReturn, 200, "Data Retrieval");
                            }
                            else
                            {
                                response = new Response<tblTemplateManagement>(null, 500, "Data Not Retrieval");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<tblTemplateManagement>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<TemplateMetaTag>> GetMetaTagByTemplateId(int RepoId)
        {
            Response<List<TemplateMetaTag>> response;
            List<TemplateMetaTag> loginReturn = new List<TemplateMetaTag>();
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("GET"), _configuration.GetSection("TemplateMgmtUrl").Value + "/api/Template/GetMetaTagByTemplateId/" + RepoId))
                        {
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            loginReturn = JsonConvert.DeserializeObject<List<TemplateMetaTag>>(data.ToString());
                            if (loginReturn != null)
                            {
                                response = new Response<List<TemplateMetaTag>>(loginReturn, 200);
                            }
                            else
                            {
                                response = new Response<List<TemplateMetaTag>>(null, 500, "Data Not Found");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<TemplateMetaTag>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<TemplateCategory>> GetCategoryList(GetCategoryList categoryList)
        {
            Response<List<TemplateCategory>> response;
            List<TemplateCategory> loginReturn = new List<TemplateCategory>();
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), _configuration.GetSection("TemplateMgmtUrl").Value + "/api/Template/GetCategoryList"))
                        {
                            //inputLogin.ProductKey = _configuration.GetSection("ProductKey").Value;
                            string SendResult = JsonConvert.SerializeObject(categoryList);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            loginReturn = JsonConvert.DeserializeObject<List<TemplateCategory>>(data.ToString());
                            if (loginReturn != null)
                            {
                                response = new Response<List<TemplateCategory>>(loginReturn, 200, "Data Retrieval");
                            }
                            else
                            {
                                response = new Response<List<TemplateCategory>>(null, 500, "Data Not Retrieval");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<TemplateCategory>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<TemplateTypeVW>> GetTemplateType(int ProductId, int TenantId)
        {
            Response<List<TemplateTypeVW>> response;
            List<TemplateTypeVW> loginReturn = new List<TemplateTypeVW>();
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("GET"), _configuration.GetSection("TemplateMgmtUrl").Value + "/api/Template/GetTemplateType/" + ProductId +"/"+TenantId))
                        {
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            loginReturn = JsonConvert.DeserializeObject<List<TemplateTypeVW>>(data.ToString());
                            if (loginReturn != null)
                            {
                                response = new Response<List<TemplateTypeVW>>(loginReturn, 200);
                            }
                            else
                            {
                                response = new Response<List<TemplateTypeVW>>(null, 500, "Data Not Found");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<TemplateTypeVW>>(ex.Message, 500);
            }
            return response;
        }
        public Response<TemplatePath> GetTemplateFile(int TemplateId, int ProductId)
        {
            Response<TemplatePath> response;
            TemplatePath loginReturn = new TemplatePath();
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("GET"), _configuration.GetSection("TemplateMgmtUrl").Value + "/api/Template/GetTemplateFile/" + TemplateId + "/" + ProductId))
                        {
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            loginReturn = JsonConvert.DeserializeObject<TemplatePath>(data.ToString());
                            if (loginReturn != null)
                            {
                                response = new Response<TemplatePath>(loginReturn, 200);
                            }
                            else
                            {
                                response = new Response<TemplatePath>(null, 500, "Data Not Found");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<TemplatePath>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<TemplateAttribute>> GetAttributeList(int ProductId)
        {
            Response<List<TemplateAttribute>> response;
            List<TemplateAttribute> loginReturn = new List<TemplateAttribute>();
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("GET"), _configuration.GetSection("TemplateMgmtUrl").Value + "/api/Template/GetAttributeList/" + ProductId))
                        {
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            loginReturn = JsonConvert.DeserializeObject<List<TemplateAttribute>>(data.ToString());
                            if (loginReturn != null)
                            {
                                response = new Response<List<TemplateAttribute>>(loginReturn, 200);
                            }
                            else
                            {
                                response = new Response<List<TemplateAttribute>>(null, 500, "Data Not Found");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<TemplateAttribute>>(ex.Message, 500);
            }
            return response;
        }
        public Response<string> DeleteTemplate(int TemplateId)
        {
            Response<string> response;
            string loginReturn = string.Empty;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("DELETE"), _configuration.GetSection("TemplateMgmtUrl").Value + "/api/Template/DeleteTemplate/" + TemplateId))
                        {
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            loginReturn = JsonConvert.DeserializeObject<string>(data.ToString());
                            if (loginReturn != null)
                            {
                                response = new Response<string>(loginReturn, 200);
                            }
                            else
                            {
                                response = new Response<string>(null, 500, "Data Not Found");
                            }
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
        public Response<string> UpdateTemplateStatus(vwInputTemp objtemp)
        {
            Response<string> response;
            string loginReturn = string.Empty;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("PUT"), _configuration.GetSection("TemplateMgmtUrl").Value + "/api/Template/UpdateTemplateStatus"))
                        {
                            string SendResult = JsonConvert.SerializeObject(objtemp);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            var data = jsondata["data"];
                            loginReturn = JsonConvert.DeserializeObject<string>(data.ToString());
                            if (loginReturn != null)
                            {
                                response = new Response<string>(loginReturn, 200);
                            }
                            else
                            {
                                response = new Response<string>(null, 500, "Data Not Found");
                            }
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
    }
}
