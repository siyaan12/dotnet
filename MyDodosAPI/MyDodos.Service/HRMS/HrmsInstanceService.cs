using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.HR;
using MyDodos.Repository.HRMS;
using MyDodos.ViewModel.Common;
using MyDodos.ViewModel.Entitlement;
using MyDodos.ViewModel.HR;
using MyDodos.ViewModel.HRMS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyDodos.Service.HRMS
{
    public class HrmsInstanceService : IHrmsInstanceService
    {
        private readonly IHrmsInstanceRepository _hrmsInstanceRepository;
        private readonly IOnBoardRepository _onBoardRepository;
        private readonly IConfiguration _configuration;
        public HrmsInstanceService(IHrmsInstanceRepository hrmsInstanceRepository, IOnBoardRepository onBoardRepository, IConfiguration configuration)
        {
            _hrmsInstanceRepository = hrmsInstanceRepository;
            _onBoardRepository = onBoardRepository;
            _configuration = configuration;
        }
        public Response<SaveOut> SaveLocation(HRMSLocationBO objlocation)
        {
            Response<SaveOut> response;
            try
            {
                var result = _hrmsInstanceRepository.SaveLocation(objlocation);
                response = new Response<SaveOut>(result, 200, "Saved Successfully");                
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> SaveMasDepartment(HRMSDepartmentBO department)
        {
            Response<SaveOut> response;
            try
            {
                var result = _hrmsInstanceRepository.SaveMasDepartment(department);
                response = new Response<SaveOut>(result, 200, "Saved Successfully");                
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message, 500);
            }
            return response;
        }
        public Response<string> GetToHRMS(OnBoardingGenralBO genral, AppUserDetailsBO AppUser)
        {
            Response<string> response;
            try
            {
                HRrtnOnboardBO objdata = new HRrtnOnboardBO();
                if (AppUser.AppUser != null)
                {
                objdata.Employee = _hrmsInstanceRepository.initGetEmployee(genral.EmpID, genral.LocationID);
                if(objdata.Employee != null)
                {
                    objdata.Employee[0].RellEmpID = objdata.Employee[0].EmpID;
                    objdata.Employee[0].LocationID = objdata.Employee[0].RellID;
                    objdata.Employee[0].EmpID = 0;                    
                    objdata.Address = _onBoardRepository.GetAddress(genral.EmpID);
                    if(objdata.Address.Count > 0){
                        foreach (var item in objdata.Address)
                        {
                            item.RellAddID = item.AddressID;
                            item.AddressID = 0;
                        }
                    }
                }
                
                 //objdata.Objappuser = AppUser;
                 var obj = SaveToHRMS(objdata);
                 response = new Response<string>(obj.Message, 200, "Saved Successfully");
                }
                else
                {
                    response = new Response<string>("", 200, "Saved Not Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<string>(ex.Message, 500);
            }
            return response;
        }
        public Response<string> SaveToHRMS(HRrtnOnboardBO objdata)
        {
            Response<string> response;
            try
            {
                using (HttpClientHandler clientHandler = new HttpClientHandler())
                {
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    using (var httpClient = new HttpClient(clientHandler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), _configuration.GetSection("HamsaUrl").Value + "/api/DodosIntegration/saveHREmployeeOnboard"))
                        {
                            //input.ProductID = Convert.ToInt32(configuration.GetSection("ProductID").Value);
                            string SendResult = JsonConvert.SerializeObject(objdata);
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
        public Response<SaveOut> SaveConsoleLeaveJournal(LeaveJournalBO objJournal)
        {
            Response<SaveOut> response;
            try
            {
                var result = _hrmsInstanceRepository.SaveConsoleLeaveJournal(objJournal);
                response = new Response<SaveOut>(result, 200, "Saved Successfully");                
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message, 500);
            }
            return response;
        }
    }
}