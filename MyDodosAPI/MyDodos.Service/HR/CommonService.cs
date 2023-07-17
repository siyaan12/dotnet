using KoSoft.DocRepo;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Administrative;
using MyDodos.Domain.AzureStorage;
using MyDodos.Domain.HR;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.AzureStorage;
using MyDodos.Repository.HR;
using MyDodos.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyDodos.Service.HR
{
    public class CommonService : ICommonService
    {
        private readonly IConfiguration _configuration;
        private readonly ICommonRepository _commonRepository;
        private readonly IStorageConnect _storage;
        public CommonService(IConfiguration configuration, ICommonRepository commonRepository, IStorageConnect storage)
        {
            _configuration = configuration;
            _commonRepository = commonRepository;
            _storage = storage;
        }
        public Response<CountryListBO> GetCountryList(int ProductID, int CountryID)
        {
            Response<CountryListBO> response;
            try
            {
                CountryListBO obj = new CountryListBO();
                List<CountryBO> objdata = new List<CountryBO>();
                obj.Country = _commonRepository.GetCountryList(ProductID, CountryID);
                if (CountryID > 0)
                {
                    foreach (var item in obj.Country)
                    {
                        item.States = _commonRepository.GetStateList(ProductID, CountryID).Data;
                        obj.Country[0].States = item.States;
                    }
                }
                else
                {
                    obj.States = _commonRepository.GetStateList(ProductID, CountryID).Data;
                }
                response = new Response<CountryListBO>(obj, 200, null);
            }
            catch (Exception ex)
            {
                response = new Response<CountryListBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> SaveMultiSelectCountry(ConfigSettingsBO country)
        {
            Response<SaveOut> response;
            try
            {
                if (country.IncrementNos != "")
                {
                    string[] split = country.IncrementNos.Split(",");
                    foreach (var str in split)
                    {
                        country.IncrementNo = Convert.ToInt32(str);
                        _commonRepository.SaveMultiSelectCountry(country);
                    }
                    response = new Response<SaveOut>(null, 200, "Saved Successfully");
                }
                else
                {
                    response = new Response<SaveOut>(null, 200, "Could not Updated");
                }
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<ConfigSettingsBO>> GetCountryByTenant(int TenantID, int LocationID)
        {
            Response<List<ConfigSettingsBO>> response;
            try
            {
                var result = _commonRepository.GetCountryByTenant(TenantID, LocationID);
                if (result.Count == 0)
                {
                    response = new Response<List<ConfigSettingsBO>>(null, 200, "Data Not Found");
                }
                else
                {
                    response = new Response<List<ConfigSettingsBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<ConfigSettingsBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<TimeZoneBO>> GetTimeZoneDetails()
        {
            Response<List<TimeZoneBO>> response;
            try
            {
                var result = _commonRepository.GetTimeZoneDetails();
                if (result.Count == 0)
                {
                    response = new Response<List<TimeZoneBO>>(null, 200, "Data Not Found");
                }
                else
                {
                    response = new Response<List<TimeZoneBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<TimeZoneBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<CurrencyBO>> GetCurrencyDetails()
        {
            Response<List<CurrencyBO>> response;
            try
            {
                var result = _commonRepository.GetCurrencyDetails();
                if (result.Count == 0)
                {
                    response = new Response<List<CurrencyBO>>(null, 200, "Data Not Found");
                }
                else
                {
                    response = new Response<List<CurrencyBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<CurrencyBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<GenDocument>> GetDocInfo(int TenantID, int LocationID, int EmpID)
        {
            Response<List<GenDocument>> response;
            try
            {
                DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                var docs = objdoc.GetDocument(0, EmpID, "", "IDProof", TenantID);
                if (docs.Count() == 0)
                {
                    response = new Response<List<GenDocument>>(docs, 200, "Data Not Found");
                }
                else
                {
                    response = new Response<List<GenDocument>>(docs, 200, "Data Reterived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<GenDocument>>(ex.Message, 500);
            }
            return response;
        }
        public Response<AzureDocURLBO> DownloadDocs(int docId, int productId)
        {
            Response<AzureDocURLBO> response;
            AzureDocURLBO docURL = new AzureDocURLBO();
            DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
            try
            {
                var result = objdoc.GetDocument(docId, 0, "", "", 0);
                if (result.Count() == 0)
                {
                    response = new Response<AzureDocURLBO>(null, 200, "Data Not Found");
                }
                else
                {
                    foreach (var s in result)
                    {
                        var objcont = objdoc.GetDocContainer(s.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                        string TenantName = objcont[0].ContainerName.ToLower();
                        var docs = _storage.DownloadDocument(new SaveDocCloudBO
                        {
                            CloudType = _configuration.GetSection("CloudType").Value,
                            Container = TenantName,
                            fileName = s.GenDocName,
                            folderPath = s.DirectionPath,
                            ProductCode = Convert.ToString(productId)
                        }).Result;
                        docURL = docs;
                        docURL.DocID = s.DocID;
                        docURL.docsSize = s.DocSize;
                        docURL.docName = s.GenDocName;
                        docURL.DocumentURL = docs.DocumentURL;
                    }
                    response = new Response<AzureDocURLBO>(docURL, 200);
                }
            }
            catch (Exception ex)
            {

                response = new Response<AzureDocURLBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<AzureDocURLBO> DeleteDocs(int docId, int productId)
        {
            Response<AzureDocURLBO> response;
            AzureDocURLBO docURL = new AzureDocURLBO();
            DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
            try
            {
                var result = objdoc.GetDocument(docId, 0, "", "", 0);
                if (result.Count() == 0)
                {
                    response = new Response<AzureDocURLBO>(null, 200, "Data Not Found");
                }
                else
                {
                    foreach (var s in result)
                    {
                        var objcont = objdoc.GetDocContainer(s.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                        string TenantName = objcont[0].ContainerName.ToLower();
                        var docs = _storage.DeleteDocument(new SaveDocCloudBO
                        {
                            CloudType = _configuration.GetSection("CloudType").Value,
                            Container = TenantName,
                            fileName = s.GenDocName,
                            folderPath = s.DirectionPath,
                            ProductCode = Convert.ToString(productId)

                        }).Result;
                        docURL = docs;
                    }
                    if (docURL.Message == "deleted Successfully")
                    {
                        objdoc.DeleteDocument(docId);
                    }
                    response = new Response<AzureDocURLBO>(docURL, 200);
                }
            }
            catch (Exception ex)
            {
                response = new Response<AzureDocURLBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<AzureDocURLBO> Base64DownloadDocs(int docId, int productId)
        {
            Response<AzureDocURLBO> response;
            AzureDocURLBO docURL = new AzureDocURLBO();
            DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
            try
            {
                var result = objdoc.GetDocument(docId, 0, "", "", 0);
                if (result.Count() == 0)
                {
                    response = new Response<AzureDocURLBO>(null, 200, "Data Not Found");
                }
                else
                {
                    foreach (var s in result)
                    {
                        var objcont = objdoc.GetDocContainer(s.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                        string TenantName = objcont[0].ContainerName.ToLower();
                        var docs = _storage.DownloadDocument(new SaveDocCloudBO
                        {
                            CloudType = _configuration.GetSection("CloudType").Value,
                            Container = TenantName,
                            fileName = s.GenDocName,
                            folderPath = s.DirectionPath,
                            ProductCode = Convert.ToString(productId)
                        }).Result;
                        docURL = docs;
                        docURL.DocID = s.DocID;
                        docURL.docsSize = s.DocSize;
                        docURL.docName = s.GenDocName;
                        docURL.DocumentURL = "https://productsteststorage.blob.core.windows.net/tenants/do00101/payroll/2023_5/Payroll_f081fc02132d4efb9144f7b3689bf7b1.txt";
                        if(!string.IsNullOrEmpty(docURL.DocumentURL))
                        {
                          docURL.DocumentURL =  _storage.ReadDataInUrl(docURL.DocumentURL);
                        }
                    }
                    response = new Response<AzureDocURLBO>(docURL, 200);
                }
            }
            catch (Exception ex)
            {

                response = new Response<AzureDocURLBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<WeekDayBO>> GetMasterWeekDays(int TenantID, int LocationID)
        {
            Response<List<WeekDayBO>> response;
            try
            {
                var result = _commonRepository.GetMasterWeekDays(TenantID, LocationID);
                if (result.Count == 0)
                {
                    response = new Response<List<WeekDayBO>>(null, 200, "Data Not Found");
                }
                else
                {
                    response = new Response<List<WeekDayBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<WeekDayBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> SaveStgDownloadDoc(StgDownloadDocBO stgdown)
        {
            Response<SaveOut> response;
            try
            {
                var result = _commonRepository.SaveStgDownloadDoc(stgdown);
                if (result.Id == 0)
                {
                    response = new Response<SaveOut>(result,200,"Could not Saved");
                }
                else
                {
                    response = new Response<SaveOut>(result,200,result.Msg);
                }
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message,500);
            }
            return response;
        }
        public Response<List<StgDownloadDocBO>> GetStgDownloadDoc(int TenantID, int DownloadDocID,int EntityID,string Entity)
        {
            Response<List<StgDownloadDocBO>> response;
            try
            {
                var result = _commonRepository.GetStgDownloadDoc(TenantID, DownloadDocID, EntityID, Entity);
                if (result.Count == 0)
                {
                    response = new Response<List<StgDownloadDocBO>>(null, 200, "Data Not Found");
                }
                else
                {
                    response = new Response<List<StgDownloadDocBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<StgDownloadDocBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<string> ExecutiveScript(ExecutiveScript script)
        {
            Response<string> response;
            try
            {
                var result = _commonRepository.ExecutiveScript(script);
                response = new Response<string>(result,200,"Saved Successfully");
            }
            catch (Exception ex)
            {
                response = new Response<string>(ex.Message,500);
            }
            return response;
        }
    }
}

