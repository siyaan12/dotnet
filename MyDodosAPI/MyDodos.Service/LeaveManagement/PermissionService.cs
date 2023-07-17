using Microsoft.Extensions.Configuration;
using MyDodos.Domain.PermissionBO;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.Wrapper;
using MyDodos.Domain.Mail;
using MyDodos.Domain.AzureStorage;
using MyDodos.Repository.LeaveManagement;
using MyDodos.ViewModel.LeaveManagement;
using MyDodos.Domain.Document;
using MyDodos.Repository.AzureStorage;
using MyDodos.Repository.Document;
using MyDodos.Repository.Holiday;
using MyDodos.Repository.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyDodos.ViewModel.Document;
using MyDodos.Repository.TemplateManager;
using KoSoft.DocTemplate;
using KoSoft.DocRepo;
using System.Diagnostics;

namespace MyDodos.Service.LeaveManagement
{
    public class PermissionService : IPermissionService
    {
        private readonly IConfiguration _configuration;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IStorageConnect _storage;
        private readonly IDocRepository _docRepository;
        private readonly IMailRepository _mailrepository; 
        public PermissionService(IConfiguration configuration, IPermissionRepository permissionRepository, ILeaveRepository leaveRepository, IHolidayRepository holidayrepository, IStorageConnect storage, IDocRepository docRepository, IMailRepository mailrepository)
        {
            _configuration = configuration;
            _permissionRepository = permissionRepository;
            _storage = storage;
            _docRepository = docRepository;
            _mailrepository = mailrepository;
        }
        public Response<PermissionRequestModelMsg> AddNewPermissionRequest(PermissionModel permission)
        {
            Response<PermissionRequestModelMsg> response;
            int mailID  = 0;
            string Objresult  = string.Empty;
            string RootPath = string.Empty;
            string fileContent = string.Empty;
            // TemplatedetailBO TempName = new TemplatedetailBO();
            // MailNotifyBO objper = new MailNotifyBO();
            // AzureDocURLBO Azuredoc = new AzureDocURLBO();            
            try
            {
            if(string.IsNullOrEmpty(permission.PermComments))
            {
                TimeSpan result;
                result = permission.PermStartTime + permission.PermDuration;
                permission.PermEndTime = result;
            }
                var Msg = _permissionRepository.AddNewPermissionRequest(permission);
                //Msg.ErrorMsg = "Save Data";
                if(Msg.RequestID > 0)
                {
                var Permissiondetails = _permissionRepository.GetPermission(permission.TenantID, permission.LocationID,Msg.RequestID);
                permission.PermID = Msg.RequestID;
                permission.EntityType  = "Permission";
                var objper = _mailrepository.GetMailReportName(permission.EmpID);
                    if(Msg.Msg == "Submitted Successfully")
                    {
                        permission.EntityName = Convert.ToString(EntityType.PermissionApply);
                    }
                    if(Msg.Msg == "Updated Successfully")
                    {
                        if(permission.PermStatus  == "Approved")
                        {
                            var empmail = objper[0].EmployeeEmail;
                            objper[0].EmployeeEmail = objper[0].ReportingEmail;                            
                            objper[0].ReportingEmail = empmail;
                            permission.EntityName = Convert.ToString(EntityType.PermissionApproved);
                        }
                        else
                        {
                            var empmail = objper[0].EmployeeEmail;
                            objper[0].EmployeeEmail = objper[0].ReportingEmail;                            
                            objper[0].ReportingEmail = empmail;
                            permission.EntityName = Convert.ToString(EntityType.PermissionRejected);
                        }
                    }
                    mailID = SendLeave(permission, objper);
                    if(Msg.Msg == "Submitted Successfully")
                    {
                        permission.EntityName = Convert.ToString(EntityType.PermissionSumbit);
                        SendSumbit(permission, objper);
                    }
                }
                // if (Msg.RequestID > 0 && Msg.Msg == "Submitted Successfully")
                // {
                //     permission.PermID = Msg.RequestID;
                //     var temp = _docRepository.GetTemplatesByTenant(permission.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value), "Email Template", "Content Template");
                //     var objtemp = temp.Data;
                //     if (objtemp.Count > 0)
                //     {
                //         foreach (var item in objtemp)
                //         {
                //             if (item.TemplateName == "Permission Apply Template")
                //             {
                //                 var det = _docRepository.GetDocRepository(permission.TenantID, permission.TenantID, "Tenant", Convert.ToInt32(_configuration.GetSection("ProductID").Value), "Email Template");
                //                 var objdocpath = det.Data;
                //                 if (objdocpath.Count > 0)
                //                 {
                //                     TempName.RepoType = objdocpath[0].RepoType;
                //                     TempName.RepoPath = objdocpath[0].RepoPath;
                //                     TempName.TemplateID = item.TemplateId;
                //                     TempName.TemplateName = item.TemplateName;
                //                     TempName.FileName = item.FileName;
                //                     TempName.StorageFolder = item.StorageFolder;
                //                 }
                //             }
                //         }
                //         objper = _mailrepository.GetMailReportName(permission.EmpID);
                //         if (objper != null)
                //         {
                //             if (TempName.RepoType == "Storage")
                //             {
                //                 var tag = _docRepository.GetMetatag(permission.ProductID, TempName.TemplateID, permission.PermID, "Permission");
                //                 var tagvalues = tag.Data;
                //                 if (tagvalues.Count > 0)
                //                 {
                //                     var mTag1 = _docRepository.GetRapidMetaTagValues(tagvalues);
                //                     var mTag = mTag1.Data;
                //                     if (mTag.Count > 0)
                //                     {
                //                         var docsdet = _storage.DownloadDocument(new SaveDocCloudBO
                //                         {
                //                             CloudType = _configuration.GetSection("CloudType").Value,
                //                             Container = "template",
                //                             fileName = TempName.FileName,
                //                             folderPath = "DO00101/" + TempName.StorageFolder,
                //                             ProductCode = Convert.ToString(_configuration.GetSection("ProductID").Value)

                //                         }).Result;
                //                         Azuredoc = docsdet;
                //                         if (!string.IsNullOrEmpty(Azuredoc.DocumentURL))
                //                         {
                //                             fileContent = _docRepository.GetTemplateWithContent(Azuredoc.DocumentURL, mTag);
                //                         }
                //                         else
                //                         {
                //                             fileContent = "Sorry!!! Mail Template Problem contact network team";
                //                         }
                //                     }
                //                 }
                //             }
                //             else if (TempName.RepoType == "File System")
                //             {
                //                 var tag = _docRepository.GetMetatag(permission.ProductID, TempName.TemplateID, permission.PermID, "Permission");
                //                 var tagvalues = tag.Data;
                //                 var mTag1 = _docRepository.GetRapidMetaTagValues(tagvalues);
                //                 var mTag = mTag1.Data;
                //                 RootPath = Path.Combine(TempName.RepoPath, TempName.TemplateName);
                //                 if (!string.IsNullOrEmpty(RootPath))
                //                 {
                //                     fileContent = _docRepository.GetTemplateWithContent(Azuredoc.DocumentURL, mTag);
                //                 }
                //                 else
                //                 {
                //                     fileContent = "Sorry!!! Mail Template Problem contact network team";
                //                 }
                //             }
                //             if (!string.IsNullOrEmpty(fileContent))
                //             {
                //                 MailModel oNBO = new MailModel
                //                 {
                //                     SubscriberID = Convert.ToInt32(_configuration.GetSection("ProductID").Value),
                //                     NotifyFrom = objper.EmployeeEmail,
                //                     NotifyTo = objper.ReportingEmail,
                //                     NotifyCC = objper.HREmail,
                //                     NotifySubject = "Permission: Testing",
                //                     //NotifySubject = "Permission: " + permission.PermStatus,
                //                     NotifyBody = fileContent,
                //                     Entity = "Permission: " + permission.PermStatus,
                //                     NotifyID = permission.EmpID,
                //                     TenantID = permission.TenantID,
                //                 };
                //                 mailID = _mailrepository.SendMail(oNBO).Result;
                //             }
                //         }
                //     }
                //     var sumbitdeatails = SumbitMailPermission(permission);
                //     Msg.Msg = Msg.Msg + ",Mail ID:" + Convert.ToString(mailID);
                // }
                // else if (Msg.RequestID > 0 && Msg.Msg == "Updated Successfully")
                // {
                //     var temp = _docRepository.GetTemplatesByTenant(permission.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value), "Email Template", "Content Template");
                //     var objtemp = temp.Data;
                //     if (objtemp.Count > 0)
                //     {
                //         foreach (var item in objtemp)
                //         {
                //             if (permission.PermStatus == "Approved")
                //             {
                //                 if (item.TemplateName == "Permission Approved Template")
                //                 {
                //                     var det = _docRepository.GetDocRepository(permission.TenantID, permission.TenantID, "Tenant", Convert.ToInt32(_configuration.GetSection("ProductID").Value), "Email Template");
                //                     var objdocpath = det.Data;
                //                     if (objdocpath.Count > 0)
                //                     {
                //                         TempName.RepoType = objdocpath[0].RepoType;
                //                         TempName.RepoPath = objdocpath[0].RepoPath;
                //                         TempName.TemplateID = item.TemplateId;
                //                         TempName.TemplateName = item.TemplateName;
                //                         TempName.FileName = item.FileName;
                //                         TempName.StorageFolder = item.StorageFolder;
                //                     }
                //                 }
                //             }
                //             else
                //             {
                //                 if (item.TemplateName == "Permission Reject Template")
                //                 {
                //                     var det = _docRepository.GetDocRepository(permission.TenantID, permission.TenantID, "Tenant", Convert.ToInt32(_configuration.GetSection("ProductID").Value), "Email Template");
                //                     var objdocpath = det.Data;
                //                     if (objdocpath.Count > 0)
                //                     {
                //                         TempName.RepoType = objdocpath[0].RepoType;
                //                         TempName.RepoPath = objdocpath[0].RepoPath;
                //                         TempName.TemplateID = item.TemplateId;
                //                         TempName.TemplateName = item.TemplateName;
                //                         TempName.FileName = item.FileName;
                //                         TempName.StorageFolder = item.StorageFolder;
                //                     }
                //                 }
                //             }
                //         }
                //         objper = _mailrepository.GetMailReportName(permission.EmpID);
                //         if (objper != null)
                //         {
                //             if (TempName.RepoType == "Storage")
                //             {
                //                 var tag = _docRepository.GetMetatag(permission.ProductID, TempName.TemplateID, permission.PermID, "Permission");
                //                 var tagvalues = tag.Data;
                //                 if (tagvalues.Count > 0)
                //                 {
                //                     var mTag1 = _docRepository.GetRapidMetaTagValues(tagvalues);
                //                     var mTag = mTag1.Data;
                //                     if (mTag.Count > 0)
                //                     {
                //                         var docsdet = _storage.DownloadDocument(new SaveDocCloudBO
                //                         {
                //                             CloudType = _configuration.GetSection("CloudType").Value,
                //                             Container = "template",
                //                             fileName = TempName.FileName,
                //                             folderPath = "DO00101/" + TempName.StorageFolder,
                //                             ProductCode = Convert.ToString(_configuration.GetSection("ProductID").Value)

                //                         }).Result;
                //                         Azuredoc = docsdet;
                //                         if (!string.IsNullOrEmpty(Azuredoc.DocumentURL))
                //                         {
                //                             fileContent = _docRepository.GetTemplateWithContent(Azuredoc.DocumentURL, mTag);
                //                         }
                //                         else
                //                         {
                //                             fileContent = "Sorry!!! Mail Template Problem contact network team";
                //                         }
                //                     }
                //                 }
                //             }
                //             else if (TempName.RepoType == "File System")
                //             {
                //                 var tag = _docRepository.GetMetatag(permission.ProductID, TempName.TemplateID, permission.PermID, "Permission");
                //                 var tagvalues = tag.Data;
                //                 var mTag1 = _docRepository.GetRapidMetaTagValues(tagvalues);
                //                 var mTag = mTag1.Data;
                //                 //File Type Is Here
                //                 RootPath = Path.Combine(TempName.RepoPath, TempName.TemplateName);
                //                 if (!string.IsNullOrEmpty(RootPath))
                //                 {
                //                     fileContent = _docRepository.GetTemplateWithContent(Azuredoc.DocumentURL, mTag);
                //                 }
                //                 else
                //                 {
                //                     fileContent = "Sorry!!! Mail Template Problem contact network team";
                //                 }
                //             }
                //             if (!string.IsNullOrEmpty(fileContent))
                //             {
                //                 MailModel oNBO = new MailModel
                //                 {
                //                     SubscriberID = Convert.ToInt32(_configuration.GetSection("ProductID").Value),
                //                     NotifyFrom = objper.ReportingEmail,
                //                     NotifyTo = objper.EmployeeEmail,
                //                     NotifyCC = objper.HREmail,
                //                     NotifySubject = "Permission: Testing",
                //                     //NotifySubject = "Permission: " + permission.PermStatus,
                //                     NotifyBody = fileContent,
                //                     Entity = "Permission: " + permission.PermStatus,
                //                     NotifyID = permission.EmpID,
                //                     TenantID = permission.TenantID,
                //                 };
                //                 mailID = _mailrepository.SendMail(oNBO).Result;
                            //  }                            
                        // }
                  //  }
            //         Msg.Msg  =  Msg.Msg + ",Mail ID:" + Convert.ToString(mailID);
            // }
            // else{
            //     Msg.Msg = Msg.Msg + ",Mail ID:" + Convert.ToString(mailID);
            // }
                response = new Response<PermissionRequestModelMsg>(Msg, 200, "Saved Successfully");
            }
            catch (Exception ex)
            {
                //StackFrame callStack = new StackFrame(1, true);
                response = new Response<PermissionRequestModelMsg>(ex.Message, 500);
            }
            
            return response;
        }
        public int SendLeave(PermissionModel permission, List<MailNotifyBO> standarddata)
        {
            int mailID = 0;
            string emlContent = string.Empty;
            List<TemplateBO> TagS = new List<TemplateBO>();
            KoTemplate tpl = new KoSoft.DocTemplate.KoTemplate(_configuration.GetConnectionString("KOPRODADBConnection"), TempDBType.MySQL);
            DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("KOPRODADBConnection"), DBType.MySQL);
            List<KoSoft.DocTemplate.TemplateBO> oTpl = tpl.GetTemplateData(Convert.ToInt32(permission.TenantID), TemplateType.EmailTemplate, Convert.ToInt32(_configuration.GetSection("ProductID").Value), true);
            List<TagDataQueryBO> oQry = new List<TagDataQueryBO>();
                TagS  = oTpl.Where(s => s.Entity == permission.EntityName).ToList();
                var tagsdet = tpl.GetTemplateTags(TagS[0].TemplateID);            
            oQry = tpl.GetTagQuery(tagsdet);
            Dictionary<string, string> oParamValue = new Dictionary<string, string>();
            oParamValue.Add("requestId", Convert.ToString(permission.PermID));
            oParamValue.Add("leaveType", Convert.ToString(permission.EntityType));
            foreach (TagDataQueryBO qry in oQry)
            {
                qry.oQueryParamValue = oParamValue;
            }
            var mTag = _docRepository.GetTemplateTagValues(oQry);

            if (TagS.Count > 0)
            {
                
                var docRepo = objdoc.GetDocRepository(0, 0, DocRepoName.NONE, oTpl[0].RepositoryID);
                if (docRepo.Count > 0)
                {
                    SaveDocCloudBO cloud = new SaveDocCloudBO();
                    cloud.CloudType = _configuration.GetSection("CloudType").Value;
                    cloud.Container = docRepo[0].ContainerName.ToLower();
                    cloud.fileName = TagS[0].FileName;
                    cloud.folderPath = docRepo[0].RepoPath;
                    cloud.ProductCode = _configuration.GetSection("ProductID").Value;
                    var docURL = _storage.DownloadDocument(cloud);
                    emlContent = tpl.GetTemplateWithContentData(docURL.Result.DocumentURL, mTag.Data);

                    foreach (var item in standarddata)
                    {
                        //emlContent = "Your Verification OTP is : " + otp;
                                MailModel oNBO = new MailModel
                                {
                                    SubscriberID = Convert.ToInt32(_configuration.GetSection("ProductID").Value),
                                    NotifyFrom = item.EmployeeEmail,
                                    NotifyTo = item.ReportingEmail,
                                    NotifyCC = item.HREmail,
                                    NotifySubject = "Permission: " + permission.PermStatus,
                                    // NotifySubject = "Leave: " + leave.LeaveStatus,
                                    NotifyBody = emlContent,
                                    Entity = "Permission: " + permission.PermStatus,
                                    NotifyID = permission.PermID,
                                    EntityId = permission.PermID,
                                    TenantID = permission.TenantID,
                                };
                               mailID = _mailrepository.SendMail(oNBO).Result;
                              //  Msg.ErrorMsg = "Mail INprogress";
                        
                    }
                }
            }
            return mailID;
        }
        public void SendSumbit(PermissionModel permission, List<MailNotifyBO> standarddata)
        {
            int mailID = 0;
            string emlContent = string.Empty;
            List<TemplateBO> TagS = new List<TemplateBO>();
            KoTemplate tpl = new KoTemplate(_configuration.GetConnectionString("KOPRODADBConnection"), TempDBType.MySQL);
            DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("KOPRODADBConnection"), DBType.MySQL);
            List<KoSoft.DocTemplate.TemplateBO> oTpl = tpl.GetTemplateData(Convert.ToInt32(permission.TenantID), TemplateType.EmailTemplate, Convert.ToInt32(_configuration.GetSection("ProductID").Value), true);
            List<TagDataQueryBO> oQry = new List<TagDataQueryBO>();
            
            TagS  = oTpl.Where(s => s.Entity == permission.EntityName).ToList();
                var tagsdet = tpl.GetTemplateTags(TagS[0].TemplateID);            
            oQry = tpl.GetTagQuery(tagsdet);
            Dictionary<string, string> oParamValue = new Dictionary<string, string>();
            oParamValue.Add("requestId", Convert.ToString(permission.PermID));
            oParamValue.Add("leaveType", Convert.ToString(permission.EntityType));
            foreach (TagDataQueryBO qry in oQry)
            {
                qry.oQueryParamValue = oParamValue;
            }
            var mTag = _docRepository.GetTemplateTagValues(oQry);

            if (TagS.Count > 0)
            {
               
                var docRepo = objdoc.GetDocRepository(0, 0, DocRepoName.NONE, oTpl[0].RepositoryID);

                if (docRepo.Count > 0)
                {
                    SaveDocCloudBO cloud = new SaveDocCloudBO();
                    cloud.CloudType = _configuration.GetSection("CloudType").Value;
                    cloud.Container = docRepo[0].ContainerName.ToLower();
                    cloud.fileName = TagS[0].FileName;
                    cloud.folderPath = docRepo[0].RepoPath;
                    cloud.ProductCode = _configuration.GetSection("ProductID").Value;
                    var docURL = _storage.DownloadDocument(cloud);
                    emlContent = tpl.GetTemplateWithContentData(docURL.Result.DocumentURL, mTag.Data);

                    foreach (var item in standarddata)
                    {
                        //emlContent = "Your Verification OTP is : " + otp;
                                MailModel oNBO = new MailModel
                                {
                                    SubscriberID = Convert.ToInt32(_configuration.GetSection("ProductID").Value),
                                   NotifyFrom = _configuration.GetSection("EmailFrom").Value,
                                   NotifyTo = item.EmployeeEmail,
                                   NotifyCC = item.HREmail,
                                    NotifySubject = "Permission: " + permission.PermStatus,
                                    // NotifySubject = "Leave: " + leave.LeaveStatus,
                                    NotifyBody = emlContent,
                                    Entity = "Permission: " + permission.PermStatus,
                                    NotifyID = permission.PermID,
                                    EntityId = permission.PermID,
                                    TenantID = permission.TenantID,
                                };
                               mailID = _mailrepository.SendMail(oNBO).Result;
                              //  Msg.ErrorMsg = "Mail INprogress";
                        
                    }
                }
            }
           // return mailID;
        }
        // public Response<string> SumbitMailPermission(PermissionModel permission)
        // {
        //     Response<string> response;
        //     int mailID = 0;
        //     string Objresult = string.Empty;
        //     string RootPath = string.Empty;
        //     string fileContent = string.Empty;
        //     TemplatedetailBO TempName = new TemplatedetailBO();
        //     MailNotifyBO objper = new MailNotifyBO();
        //     AzureDocURLBO Azuredoc = new AzureDocURLBO();
        //     try
        //     {
        //         var temp = _docRepository.GetTemplatesByTenant(permission.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value), "Email Template", "Content Template");
        //         var objtemp = temp.Data;
        //         if (objtemp.Count > 0)
        //         {
        //             foreach (var item in objtemp)
        //             {
        //                 if (item.TemplateName == "Permission Sumbit Template")
        //                 {
        //                     var det = _docRepository.GetDocRepository(permission.TenantID, permission.TenantID, "Tenant", Convert.ToInt32(_configuration.GetSection("ProductID").Value), "Email Template");
        //                     var objdocpath = det.Data;
        //                     if (objdocpath.Count > 0)
        //                     {
        //                         TempName.RepoType = objdocpath[0].RepoType;
        //                         TempName.RepoPath = objdocpath[0].RepoPath;
        //                         TempName.TemplateID = item.TemplateId;
        //                         TempName.TemplateName = item.TemplateName;
        //                         TempName.FileName = item.FileName;
        //                         TempName.StorageFolder = item.StorageFolder;
        //                     }
        //                 }
        //             }
        //             objper = _mailrepository.GetMailReportName(permission.EmpID);
        //             if (objper != null)
        //             {
        //                 if (TempName.RepoType == "Storage")
        //                 {
        //                     var tag = _docRepository.GetMetatag(permission.ProductID,TempName.TemplateID, permission.PermID, "Permission");
        //                     var tagvalues = tag.Data;
        //                     if (tagvalues.Count > 0)
        //                     {
        //                         var mTag1 = _docRepository.GetRapidMetaTagValues(tagvalues);
        //                         var mTag = mTag1.Data;
        //                         if (mTag.Count > 0)
        //                         {
        //                             var docsdet = _storage.DownloadDocument(new SaveDocCloudBO
        //                             {
        //                                 CloudType = _configuration.GetSection("CloudType").Value,
        //                                 Container = "template",
        //                                 fileName =  TempName.FileName,
        //                                 folderPath = "DO00101/" + TempName.StorageFolder,
        //                                 ProductCode = Convert.ToString(_configuration.GetSection("ProductID").Value)
        //                             }).Result;
        //                             Azuredoc = docsdet;
        //                             if (!string.IsNullOrEmpty(Azuredoc.DocumentURL))
        //                             {
        //                                 fileContent = _docRepository.GetTemplateWithContent(Azuredoc.DocumentURL, mTag);
        //                             }
        //                             else
        //                             {
        //                                 fileContent = "Sorry!!! Mail Template Problem contact network team";
        //                             }
        //                         }
        //                     }
        //                 }
        //                 else if (TempName.RepoType == "File System")
        //                 {
        //                     var tag = _docRepository.GetMetatag(permission.ProductID,TempName.TemplateID, permission.PermID, "Permission");
        //                     var tagvalues = tag.Data;
        //                     var mTag1 = _docRepository.GetRapidMetaTagValues(tagvalues);
        //                     var mTag = mTag1.Data;
        //                     RootPath = Path.Combine(TempName.RepoPath, TempName.TemplateName);
        //                     if (!string.IsNullOrEmpty(RootPath))
        //                     {
        //                         fileContent = _docRepository.GetTemplateWithContent(Azuredoc.DocumentURL, mTag);
        //                     }
        //                     else
        //                     {
        //                         fileContent = "Sorry!!! Mail Template Problem contact network team";
        //                     }
        //                 }
        //                 if (!string.IsNullOrEmpty(fileContent))
        //                 {
        //                     MailModel oNBO = new MailModel
        //                     {
        //                         SubscriberID = Convert.ToInt32(_configuration.GetSection("ProductID").Value),
        //                         NotifyFrom = "support@kosoft.co",
        //                         NotifyTo = objper.EmployeeEmail.Trim(),
        //                         NotifyCC = objper.HREmail,
        //                         NotifySubject = "Permission: Testing",
        //                         // NotifySubject = "Permission: " + permission.PermStatus,
        //                         NotifyBody = fileContent,
        //                         Entity = "Permission: " + permission.PermStatus,
        //                         NotifyID = permission.PermID,
        //                         EntityId = permission.PermID,
        //                         TenantID = permission.TenantID,
        //                     };
        //                     mailID = _mailrepository.SendMail(oNBO).Result;
        //                 }
        //             }
        //         }
        //         response = new Response<string>("Mail Send Details", 200, "Saved Successfully");
        //     }
        //     catch (Exception ex)
        //     {
        //         response = new Response<string>(ex.Message);
        //     }
        //     return response;
        // }
        public Response<GetMyPermissionList> GetMyPermissionList(GetMyPermissionList objresult)
        {
            Response<GetMyPermissionList> response;
            try
            {
                var result = _permissionRepository.GetMyPermissionList(objresult);         
                if (result.objMyPermissionList.Count() == 0)
                {      
                    response = new Response<GetMyPermissionList>(result, 200, "Data Not Found");
                }
                else
                {      
                    response = new Response<GetMyPermissionList>(result, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<GetMyPermissionList>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<PermissionModel>> GetMyPermission(int TenantID,int LocationID,int PermID)
        {
            Response<List<PermissionModel>> response;
            try
            {
                var result = _permissionRepository.GetPermission(TenantID,LocationID,PermID);
                if (result.Count() == 0)
                {
                    response = new Response<List<PermissionModel>>(result,200,"Data Not Found");
                }
                else
                {
                    response = new Response<List<PermissionModel>>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<PermissionModel>>(ex.Message,500);
            }
            return response;
        }
        public Response<PermissionRequestModelMsg> MAddNewPermissionRequest(PermissionModel permission)
        {
            Response<PermissionRequestModelMsg> response;
            int mailID  = 0;
            string Objresult  = string.Empty;
            string RootPath = string.Empty;
            string fileContent = string.Empty;
            // TemplatedetailBO TempName = new TemplatedetailBO();
            // MailNotifyBO objper = new MailNotifyBO();
            // AzureDocURLBO Azuredoc = new AzureDocURLBO();            
            try
            {
            if(string.IsNullOrEmpty(permission.PermComments))
            {
                TimeSpan result;
                result = permission.PermStartTime + permission.PermDuration;
                permission.PermEndTime = result;
            }
                var Msg = _permissionRepository.MAddNewPermissionRequest(permission);
                //if(Msg.RequestID > 0)
                //{
                //var Permissiondetails = _permissionRepository.MGetPermission(permission.TenantID, permission.LocationID,Msg.RequestID);
                //permission.PermID = Msg.RequestID;
                //permission.EntityType  = "Permission";
                //var objper = _mailrepository.GetMailReportName(permission.EmpID);
                    //if(Msg.Msg == "Submitted Successfully")
                    //{
                        //permission.EntityName = Convert.ToString(EntityType.PermissionApply);
                    //}
                    // if(Msg.Msg == "Updated Successfully")
                    // {
                    //     if(permission.PermStatus  == "Approved")
                    //     {
                    //         var empmail = objper[0].EmployeeEmail;
                    //         objper[0].EmployeeEmail = objper[0].ReportingEmail;                            
                    //         objper[0].ReportingEmail = empmail;
                    //         permission.EntityName = Convert.ToString(EntityType.PermissionApproved);
                    //     }
                    //     else
                    //     {
                    //         var empmail = objper[0].EmployeeEmail;
                    //         objper[0].EmployeeEmail = objper[0].ReportingEmail;                            
                    //         objper[0].ReportingEmail = empmail;
                    //         permission.EntityName = Convert.ToString(EntityType.PermissionRejected);
                    //     }
                    // }
                    // mailID = SendLeave(permission, objper);
                    // if(Msg.Msg == "Submitted Successfully")
                    // {
                    //     permission.EntityName = Convert.ToString(EntityType.PermissionSumbit);
                    //     SendSumbit(permission, objper);
                    // }
                //}
                response = new Response<PermissionRequestModelMsg>(Msg, 200,Msg.Msg);
            }
            catch (Exception ex)
            {
                response = new Response<PermissionRequestModelMsg>(ex.Message, 500);
            }
            
            return response;
        }
        public Response<List<PermissionModel>> MGetMyPermission(int TenantID,int LocationID,int PermID)
        {
            Response<List<PermissionModel>> response;
            try
            {
                var result = _permissionRepository.MGetPermission(TenantID,LocationID,PermID);
                if (result.Count() == 0)
                {
                    response = new Response<List<PermissionModel>>(result,200,"Data Not Found");
                }
                else
                {
                    response = new Response<List<PermissionModel>>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<PermissionModel>>(ex.Message,500);
            }
            return response;
        }
        public Response<List<PermissionModel>> MGetMyPermissionList(GetMyPermissionListInputs objresult)
        {
            Response<List<PermissionModel>> response;
            try
            {
                var result = _permissionRepository.MGetMyPermissionList(objresult);         
                if (result.Count() == 0)
                {      
                    response = new Response<List<PermissionModel>>(result, 200, "Data Not Found");
                }
                else
                {      
                    response = new Response<List<PermissionModel>>(result, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<PermissionModel>>(ex.Message, 500);
            }
            return response;
        }
    }
}