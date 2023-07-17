using Microsoft.Extensions.Configuration;
using MyDodos.Repository.Employee;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using MyDodos.Repository.TemplateManager;
using MyDodos.Repository.AzureStorage;
using MyDodos.ViewModel.TicketingSystem;
using MyDodos.Domain.Wrapper;
using System.Collections.Generic;

namespace MyDodos.Service.TicketingSystem
{
    public class TicketingSystemService : ITicketingSystemService
    {
        private readonly IConfiguration _configuration;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDocRepository _docRepository;
        private readonly IStorageConnect _storage;
        public TicketingSystemService(IConfiguration configuration, IEmployeeRepository employeeRepository,IDocRepository docRepository,IStorageConnect storage)
        {
            _configuration = configuration;
            _employeeRepository = employeeRepository;
            _docRepository = docRepository;
            _storage = storage;
        }
        public Response<SaveTicket> RiseTicket(RiseTicket inputobj)
        {
            Response<SaveTicket> response;
            SaveTicket loginReturn = new SaveTicket();
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), _configuration.GetSection("CentralHubUrl").Value + "/api/Ticketing/RiseTicket"))
                        {
                            string SendResult = JsonConvert.SerializeObject(inputobj);
                            request.Content = new StringContent(SendResult);
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JObject.Parse(json);
                            //var data = jsondata["data"];
                            loginReturn = JsonConvert.DeserializeObject<SaveTicket>(jsondata.ToString());

                            if (loginReturn != null)
                            {
                                response = new Response<SaveTicket>(loginReturn, 200, "Data Retrieval");
                            }
                            else
                            {
                                response = new Response<SaveTicket>(null, 500, "Data Not Retrieval");
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                response = new Response<SaveTicket>(ex.Message, 500);
            }
            return response;
        }        
        public Response<List<Ticket>> TicketsList(TicketingInputBO inputobj)
        {
            Response<List<Ticket>> response;
            List<Ticket> loginReturn = new List<Ticket>();
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("GET"), _configuration.GetSection("CentralHubUrl").Value + "/api/Ticketing/TicketsList/"+ inputobj.email +"/" + inputobj.name))
                        {
                            var logresponse = httpClient.SendAsync(request).Result;
                            var json = logresponse.Content.ReadAsStringAsync().Result;
                            var jsondata = JArray.Parse(json);
                            //var data = jsondata["data"];
                            loginReturn = JsonConvert.DeserializeObject<List<Ticket>>(jsondata.ToString());
                            if (loginReturn != null)
                            {
                                response = new Response<List<Ticket>>(loginReturn, 200, "Data Retrieval");
                            }
                            else
                            {
                                response = new Response<List<Ticket>>(null, 500, "Data Not Retrieval");
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                response = new Response<List<Ticket>>(ex.Message, 500);
            }
            return response;
        }
    }
}
