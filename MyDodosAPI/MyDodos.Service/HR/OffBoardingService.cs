using Microsoft.Extensions.Configuration;
using MyDodos.Domain.AzureStorage;
using MyDodos.ViewModel.HR;
using MyDodos.Domain.HR;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.AzureStorage;
using MyDodos.Repository.Document;
using MyDodos.Repository.HR;
using MyDodos.Repository.Auth;
using MyDodos.Domain.AuthBO;
using MyDodos.Domain.Document;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyDodos.Repository.TemplateManager;
using Microsoft.AspNetCore.Http;
using KoSoft.DocRepo;

namespace MyDodos.Service.HR
{
    public class OffBoardService : IOffBoardService
    {
        private readonly IConfiguration _configuration;
        private readonly IOffBoardRepository _offBoardRepository;
        private readonly IAuthRepository _authRepository;
        private readonly IStorageConnect _storage;
        private readonly IDocRepository _docRepository;
        public OffBoardService(IConfiguration configuration, IOffBoardRepository offBoardRepository, IStorageConnect storage, IDocRepository docRepository,IAuthRepository authRepository)
        {
            _configuration = configuration;
            _offBoardRepository = offBoardRepository;
            _storage = storage;
            _docRepository = docRepository;
            _authRepository = authRepository;
        }
        public Response<OffBoardSearchBO> SearchOffboardingList(OffBoardSearchBO objresult)
        {
            Response<OffBoardSearchBO> response;
            try
            {
                var result = _offBoardRepository.SearchOffboardingList(objresult);       
                if (result.objOffBoardList.Count() == 0)
                {      
                    response = new Response<OffBoardSearchBO>(result, 200, "Data Not Found");
                }
                else
                {   
                    response = new Response<OffBoardSearchBO>(result, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<OffBoardSearchBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<BPCheckListDetail>> SearchOffboardList(string FirstName,int TenantID)
        {
            Response<List<BPCheckListDetail>> response;
            try
            {
                var result = _offBoardRepository.SearchOffboardList(FirstName,TenantID);       
                if (result.Count() == 0)
                {      
                    response = new Response<List<BPCheckListDetail>>(result, 200, "Data Not Found");
                }
                else
                {   
                    // foreach(var item in result)
                    // {
                    //     var check = _offBoardRepository.UpdateCheckList(item.EmpID,item.LocationID,item.TenantID);
                    // }
                    response = new Response<List<BPCheckListDetail>>(result, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<BPCheckListDetail>>(ex.Message, 500);
            }
            return response;
        }
        public Response<BPCheckListDetails> ViewOffboardListTrack(int ChkListInstanceID)
        {
            Response<BPCheckListDetails> response;
            try
            {
                BPCheckListDetails obj = new BPCheckListDetails();
                obj = _offBoardRepository.ViewOffboardListTrack(ChkListInstanceID);   
                if (obj.EmpID > 0)
                {
                    var track = _offBoardRepository.GetTracking(obj.TenantID, obj.LocationID, obj.EmpID);    
                    obj.OffboardTrack = track; 
                    var trans = _offBoardRepository.SaveBPTransInstance(obj.TenantID,obj.LocationID,obj.EmpID);
                }
                response = new Response<BPCheckListDetails>(obj, 200, "Data Retraived");
            }
            catch (Exception ex)
            {
                response = new Response<BPCheckListDetails>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<RecentExitOffBoardBO>> RecentExitOffBoarding(int TenantID)
        {
            Response<List<RecentExitOffBoardBO>> response;
            try
            {
                var result = _offBoardRepository.RecentExitOffBoarding(TenantID);       
                if (result.Count() == 0)
                {      
                    response = new Response<List<RecentExitOffBoardBO>>(result, 200, "Data Not Found");
                }
                else
                {   
                    response = new Response<List<RecentExitOffBoardBO>>(result, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<RecentExitOffBoardBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<OnBoardRequestModelMsg> CompleteOffBoarding(CompleteOffBoardingBO objresult)
        {
            Response<OnBoardRequestModelMsg> response;
            try
            {
                var result = _offBoardRepository.CompleteOffBoarding(objresult);       
                if (result.ReqID == 0)
                {      
                    response = new Response<OnBoardRequestModelMsg>(result, 200, "Data Not Found");
                }
                else
                {   
                    response = new Response<OnBoardRequestModelMsg>(result, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<OnBoardRequestModelMsg>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<BPCheckListDetail>> GetOffboardRequest(int TenantID,int ChkListInstanceID, string RequestStatus)
        {
            Response<List<BPCheckListDetail>> response;
            try
            {
                var result = _offBoardRepository.GetOffboardRequest(TenantID,ChkListInstanceID,RequestStatus);       
                if (result.Count() == 0)
                {      
                    response = new Response<List<BPCheckListDetail>>(result, 200, "Data Not Found");
                }
                else
                {   
                    response = new Response<List<BPCheckListDetail>>(result, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<BPCheckListDetail>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<BPTransInstance>> GetOffBoardTrack(int BProcessID, int ReqInitID)
        {
            Response<List<BPTransInstance>> response;
            try
            {
                var result = _offBoardRepository.GetOffBoardTrack(BProcessID, ReqInitID);       
                if (result.Count() == 0)
                {      
                    response = new Response<List<BPTransInstance>>(result, 200, "Data Not Found");
                }
                else
                {   
                    response = new Response<List<BPTransInstance>>(result, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<BPTransInstance>>(ex.Message, 500);
            }
            return response;
        }
        public Response<OnBoardRequestModelMsg> DeleteoffboardingRequest(int ChkListInstanceID)
        {
            Response<OnBoardRequestModelMsg> response;
            try
            {
                var result = _offBoardRepository.DeleteoffboardingRequest(ChkListInstanceID);
                if(result.ReqID == 0)
                {
                response = new Response<OnBoardRequestModelMsg>(result,200,"Cannot Deleted");
                }
                else
                {
                    response = new Response<OnBoardRequestModelMsg>(result,200,"Deleted Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<OnBoardRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<ReqDetails> GetReqDetails(int ChkListInstanceID)
        {
            Response<ReqDetails> response;
            try
            {
                var result = _offBoardRepository.GetReqDetails(ChkListInstanceID);       
                if (result == null)
                {      
                    response = new Response<ReqDetails>(result, 200, "Data Not Found");
                }
                else
                {   
                    response = new Response<ReqDetails>(result, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<ReqDetails>(ex.Message, 500);
            }
            return response;
        }
        public Response<GenCheckListInstance> GetResignLetter(int ChkListInstanceID,int ProductID)
         {
            Response<GenCheckListInstance> responce;
            ReturnDocDetailBO result = new ReturnDocDetailBO();
            string path = string.Empty;
            string fileName = string.Empty;  
            //GenCheckListInstance objreturn = new GenCheckListInstance();
            AzureDocURLBO docURL = new AzureDocURLBO();
            try
            {
              var objreturn = _offBoardRepository.GetRequest(ChkListInstanceID,ProductID);
              if (objreturn.ChkListInstanceID == 0)
                {
                    responce = new Response<GenCheckListInstance>(objreturn, 200,"Data Not Found");
                }
                else
                {
                    objreturn.base64Images = "";
                    DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                    //DocumentDA objdocservices = new DocumentDA(_configuration.GetConnectionString("KOPRODADBConnection"), DBType.MySQL);
                    var docs = objdoc.GetDocument(0, objreturn.ChkListInstanceID, "", "ResignLetter", objreturn.TenantID);
                    if (docs.Count > 0)
                    {
                        //var objcont = objdoc.GetDocRepository(objreturn.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value), KoSoft.DocRepo.DocRepoName.ResignLetter);
                        var objcont = objdoc.GetDocContainer(objreturn.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                        //string TenantName = objcont[0].ContainerName.ToLower();
                        var doc = _storage.DownloadDocument(new SaveDocCloudBO
                        {
                            CloudType = _configuration.GetSection("CloudType").Value,
                            Container = objcont[0].ContainerName.ToLower(),
                            fileName = docs[0].GenDocName,
                            folderPath = docs[0].DirectionPath,
                            ProductCode = Convert.ToString(ProductID)
                        }).Result;
                        objreturn.base64Images = doc.DocumentURL;
                    }
                    // responce = new Response<GenCheckListInstance>(objreturn, null);
                    // objreturn.ChkListInstanceID =  ChkListInstanceID;
                    // objreturn.EmpID =  objreturn.RefEntityID;
                }
              
              // var split = objreturn.ResignFile.Split('/');
              // var mm =  split.SkipLast(1);
              // path = String.Join("/", mm);
              // fileName = split.Last();
              // StorageConnect storage = new StorageConnect(_configuration);
              // var retresult = storage.GetCloudURL(objreturn.TenantID.Value,path,fileName);
              // if(retresult != null)
              // {
              //   result.ChkListInstanceID =  ChkListInstanceID;
              //   result.EmpID =  objreturn.RefEntityID.Value;
              //   result.File =  retresult.URL;
                
              // }              
                responce = new Response<GenCheckListInstance>(objreturn, 200, "Data Retrived");
            }
            catch (Exception ex)
            {
                responce = new Response<GenCheckListInstance>(ex.Message);
            }
            return responce;
        }
        public Response<List<BPCheckListDetail>> GetOffBoardingRequest(int TenantID)
        {
            Response<List<BPCheckListDetail>> response;
            try
            {
                var result = _offBoardRepository.GetOffBoardingRequest(TenantID);       
                if (result.Count() == 0)
                {      
                    response = new Response<List<BPCheckListDetail>>(result, 200, "Data Not Found");
                }
                else
                {   
                    response = new Response<List<BPCheckListDetail>>(result, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<BPCheckListDetail>>(ex.Message, 500);
            }
            return response;
        }
        public Response<OnBoardRequestModelMsg> CreateCheckListInstance(GenCheckListInstance inputJson,DocDetailBO objfile)
        {
            Response<OnBoardRequestModelMsg> response = new Response<OnBoardRequestModelMsg>();
            try
            {
                int DocID = 0;
                string NewFileName;
                string ResignFile = string.Empty;
                string filExt;
                DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                var result = _offBoardRepository.CreateCheckListInstance(inputJson);
                if (result.ReqID == 0)
                {
                    response = new Response<OnBoardRequestModelMsg>(result,200,"Data Creation or Updation is Failed");
                }
                else
                {
                    filExt = Path.GetExtension(objfile.File.FileName.ToString());
                    if (inputJson.DocID > 0)
                    {
                        var results = objdoc.GetDocument(inputJson.DocID, 0, "", "", 0);
                        var name = Path.GetFileNameWithoutExtension(results[0].GenDocName);
                        NewFileName = name + filExt;
                        DocID = results[0].DocID;
                    }
                    else
                    {
                        NewFileName = "ResignLetter_" + (Guid.NewGuid()).ToString("N") + filExt;
                    }
                    SaveDocCloudBO docCloudins = new SaveDocCloudBO();
                    
                    DocumentDA objdocService = new DocumentDA(_configuration.GetConnectionString("KOPRODADBConnection"), DBType.MySQL);
                    var Repo = objdocService.GetDocRepository(inputJson.TenantID,inputJson.TenantID,"Tenant",Convert.ToInt32(_configuration.GetSection("ProductID").Value), LocationType.ResignLetter);
                    var docRepo = Repo;
                    if (docRepo[0].RepoType == "Storage")
                    {

                        var ms = new MemoryStream();
                        objfile.File.OpenReadStream().CopyTo(ms);
                        byte[] contents = ms.ToArray();
                        var fileFormat = Convert.ToBase64String(contents);

                                        switch (objfile.File.ContentType)
                                        {
                                            case "image/png":
                                                docCloudins.ContentType = "image/png";
                                                break;
                                            case "image/jpeg":
                                                docCloudins.ContentType = "image/jpeg";
                                                break;
                                            case "image/jpg":
                                                docCloudins.ContentType = "image/jpeg";
                                                break;
                                            case "application/pdf":
                                                docCloudins.ContentType = "application/pdf";
                                                break;
                                            case "application/msword":
                                                docCloudins.ContentType = "application/msword";
                                                break;
                                            default:
                                                docCloudins.ContentType = System.Net.Mime.MediaTypeNames.Application.Pdf;
                                                break;
                                        }
                                        //var objcont = objdoc.GetDocRepository(inputJson.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value), DocRepoName.ResignLetter);

                                        var objcont = objdoc.GetDocContainer(inputJson.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                                        //string TenantName = objcont[0].ContainerName;
                                        docCloudins.Container = objcont[0].ContainerName.ToLower();
                                        docCloudins.CloudType = _configuration.GetSection("CloudType").Value;
                                        docCloudins.folderPath = docRepo[0].RepoPath.ToLower() + "/" + Convert.ToString(result.ReqID);
                                        docCloudins.file = fileFormat;
                                        docCloudins.fileName = NewFileName;
                                        //docCloudins.Token = tokenchkdata.Result.ToString();
                                        docCloudins.ProductCode = _configuration.GetSection("ProductID").Value;

                                        _ = _storage.SaveBulkDocumentCloud(docCloudins);
                                        ResignFile = docCloudins.folderPath + "/" + docCloudins.fileName; 

                                        GenDocument d1 = new GenDocument();
                                        d1.DocID = DocID;
                                        d1.RepositoryID = docRepo[0].RepositoryID;
                                        d1.DocumentName = objfile.File.FileName;
                                        d1.DocType = docRepo[0].RepoName;
                                        d1.CreatedBy = inputJson.CreatedBy;
                                        d1.OrgDocName = NewFileName;
                                        d1.GenDocName = NewFileName;
                                        d1.DocKey = (Guid.NewGuid()).ToString("N");
                                        d1.DocSize = Convert.ToDecimal(objfile.File.Length);
                                        d1.Entity = docRepo[0].LocType;
                                        d1.EntityID = result.ReqID;
                                        d1.TenantID = inputJson.TenantID;
                                        //d1.LocationID = inputJson.LocationID;
                                        d1.DirectionPath = docCloudins.folderPath;
                                        var doc = objdoc.SaveDocument(d1);
                                        DocID = doc;
                                        _offBoardRepository.UploadOffboardLetter(result.ReqID,inputJson.TenantID,ResignFile);
                                    }
                    }
                    response = new Response<OnBoardRequestModelMsg>(result, 200, "Saved Successfully");
                }
            catch (Exception ex)
            {
                response = new Response<OnBoardRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<List<BPCheckListDetail>> GetOffBoardReqChkLists(int ChkListInstanceID,int TenantID)
        {
            Response<List<BPCheckListDetail>> response;
            try
            {
                var result = _offBoardRepository.GetOffBoardReqChkLists(ChkListInstanceID, TenantID);       
                if (result.Count() == 0)
                {      
                    response = new Response<List<BPCheckListDetail>>(result, 200, "Data Not Found");
                }
                else
                {   
                    response = new Response<List<BPCheckListDetail>>(result, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<BPCheckListDetail>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<BPchecklistDetBO>> GetOffBoardReqCheckList(int ChkListInstanceID)
        {
            Response<List<BPchecklistDetBO>> response;
            try
            {
                var result = _offBoardRepository.GetOffBoardReqCheckList(ChkListInstanceID);       
                if (result.Count() == 0)
                {      
                    response = new Response<List<BPchecklistDetBO>>(result, 200, "Data Not Found");
                }
                else
                {   
                    foreach(var item in result)
                    {
                        var group = _offBoardRepository.GetOffBoardReqChkList(item.ChkListInstanceID,item.ChkListGroup);
                        item.BPCheckList = group;
                    }
                    response = new Response<List<BPchecklistDetBO>>(result, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<BPchecklistDetBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<OnBoardRequestModelMsg> UpdateCheckListItem(List<UpdateCheckList> list)
        {
            Response<OnBoardRequestModelMsg> response;
            try
            {
                OnBoardRequestModelMsg result = new OnBoardRequestModelMsg();
                foreach(var item in list)
                {
                 result = _offBoardRepository.UpdateCheckListItem(item);
                }
                response = new Response<OnBoardRequestModelMsg>(result,200, "Updated Successfully");
            }
            catch (Exception ex)
            {
                response = new Response<OnBoardRequestModelMsg>(ex.Message, 500);
            }
            return response;
        }
        public Response<EmployeeInfoBO> GetEmpOffBoardInfo(int TenantID, int LocationID,int EmpID)
        {
            Response<EmployeeInfoBO> response;
            try
            {
                var result = _offBoardRepository.GetEmpOffBoardInfo(TenantID, LocationID, EmpID);       
                if (result == null)
                {      
                    response = new Response<EmployeeInfoBO>(result, 200, "Data Not Found");
                }
                else
                {   
                    response = new Response<EmployeeInfoBO>(result, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<EmployeeInfoBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<OnBoardRequestModelMsg> AddGenCheckListDetail(int EmpID,int LocationID,int TenantID)
        {
            Response<OnBoardRequestModelMsg> response;
            try
            {
                var result = _offBoardRepository.AddGenCheckListDetail(EmpID,LocationID,TenantID);       
                if (result.ReqID == 0)
                {      
                    response = new Response<OnBoardRequestModelMsg>(result, 200, "Data Not Found");
                }
                else
                {   
                    response = new Response<OnBoardRequestModelMsg>(result, 200, "Saved Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<OnBoardRequestModelMsg>(ex.Message, 500);
            }
            return response;
        }
        public Response<OffboardRequestSearchBO> GetOffboardRequestSearch(OffboardRequestSearchBO objresult)
        {
            Response<OffboardRequestSearchBO> response;
            try
            {
                var result = _offBoardRepository.GetOffboardRequestSearch(objresult);       
                if (result.objOffBoardRequestList.Count() == 0)
                {      
                    response = new Response<OffboardRequestSearchBO>(result, 200, "Data Not Found");
                }
                else
                {   
                    response = new Response<OffboardRequestSearchBO>(result, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<OffboardRequestSearchBO>(ex.Message, 500);
            }
            return response;
        }
    }
}