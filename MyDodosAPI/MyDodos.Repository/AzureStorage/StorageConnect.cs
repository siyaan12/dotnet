
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.AzureStorage;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MyDodos.Repository.AzureStorage
{
    public class StorageConnect : IStorageConnect
    {
        private readonly IConfiguration _configuration;
        public StorageConnect(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<AzureDocURLBO> SaveBulkDocumentCloud(SaveDocCloudBO docCloud)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), _configuration.GetSection("AzureUrl").Value + "/api/AzureStorage/BulkGenPDF"))
                {
                    string SendResult = JsonConvert.SerializeObject(docCloud);
                    request.Content = new StringContent(SendResult);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    var response = httpClient.SendAsync(request).Result;
                    var json = await response.Content.ReadAsStringAsync();
                    var jsons = JsonConvert.DeserializeObject<AzureDocURLBO>(json);
                    return jsons;
                }
            }
        }
        public async Task<AzureDocURLBO> DownloadDocument(SaveDocCloudBO getDoc)
        {
            AzureDocURLBO azureReturn = new AzureDocURLBO();
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), _configuration.GetSection("AzureUrl").Value + "/api/AzureStorage/DownloadFile"))
                        {
                            string SendResult = JsonConvert.SerializeObject(getDoc);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var response = httpClient.SendAsync(request).Result;
                            var json = await response.Content.ReadAsStringAsync();
                            azureReturn = JsonConvert.DeserializeObject<AzureDocURLBO>(json);
                            azureReturn.Message = "Success";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                azureReturn.Message = ex.Message;
            }
            return azureReturn;
        }
        public async Task<AzureDocURLBO> DeleteDocument(SaveDocCloudBO getDoc)
        {
            AzureDocURLBO azureReturn = new AzureDocURLBO();
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), _configuration.GetSection("AzureUrl").Value + "/api/AzureStorage/DeleteFile"))
                        {
                            string SendResult = JsonConvert.SerializeObject(getDoc);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var response = httpClient.SendAsync(request).Result;
                            var json = await response.Content.ReadAsStringAsync();
                            azureReturn = JsonConvert.DeserializeObject<AzureDocURLBO>(json);
                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                azureReturn.Message = ex.Message;
            }
            return azureReturn;
        }
        public string ReadDataInUrl(string url)
        {
            string data = string.Empty;
            var owners = (new WebClient()).DownloadData(url);
            data = Convert.ToBase64String(owners);
            return data;
        }
    }
}