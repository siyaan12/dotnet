using Microsoft.Extensions.Configuration;
using MyDodos.Domain.AzureStorage;
using MyDodos.Domain.Holiday;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.Mail;
using MyDodos.ViewModel.Document;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.AzureStorage;
using MyDodos.Repository.Holiday;
using MyDodos.Repository.LeaveManagement;
using MyDodos.Repository.Mail;
using MyDodos.ViewModel.LeaveManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyDodos.Repository.TemplateManager;
using KoSoft.DocTemplate;
using KoSoft.DocRepo;
using System.Diagnostics;

namespace MyDodos.Service.LeaveManagement
{
    public class LeaveService : ILeaveService
    {
        private readonly IConfiguration _configuration;
        private readonly ILeaveRepository _leaveRepository;
        private readonly IHolidayRepository _holidayrepository;
        private readonly IStorageConnect _storage;
        private readonly IDocRepository _docRepository;
        private readonly IMailRepository _mailrepository;
        public LeaveService(IConfiguration configuration, ILeaveRepository leaveRepository, IHolidayRepository holidayrepository, IStorageConnect storage, IDocRepository docRepository, IMailRepository mailrepository)
        {
            _configuration = configuration;
            _leaveRepository = leaveRepository;
            _holidayrepository = holidayrepository;
            _storage = storage;
            _docRepository = docRepository;
            _mailrepository = mailrepository;
        }
        public Response<LeaveRequestModelMsg> AddNewLeaveRequest(LeaveRequestModel leave)
        {
            Response<LeaveRequestModelMsg> response;
            int mailID = 0;
            string Objresult = string.Empty;
            string RootPath = string.Empty;
            string fileContent = string.Empty;
           // TemplatedetailBO TempName = new TemplatedetailBO();
           // MailNotifyBO objper = new MailNotifyBO();
            //AzureDocURLBO Azuredoc = new AzureDocURLBO();
            try
            {
                var Msg = _leaveRepository.AddNewLeaveRequest(leave);
                if (Msg.RequestID > 0)
                {
                    var Leavedetails = _leaveRepository.GetLeave(Msg.RequestID, leave.TenantID, leave.LocationID);
                    if (Leavedetails.Count > 0)
                    {
                        if (Leavedetails[0].LeaveStatus == "Approved")
                        {
                            var mm = _leaveRepository.SaveLeaveAlloc(Leavedetails[0]);
                        }
                    }
                    leave.LeaveID = Msg.RequestID;
                    leave.EntityType  = "Leave";
                  var objper = _mailrepository.GetMailReportName(leave.EmpID);
                  if(Msg.Msg == "Submitted Successfully")
                    {
                        leave.EntityName = Convert.ToString(EntityType.LeaveApply);
                    }
                    if(Msg.Msg == "Updated Successfully")
                    {
                        if(leave.LeaveStatus  == "Approved")
                        {
                            var empmail = objper[0].EmployeeEmail;
                            objper[0].EmployeeEmail = objper[0].ReportingEmail;                            
                            objper[0].ReportingEmail = empmail;
                            leave.EntityName = Convert.ToString(EntityType.LeaveApproved);                            
                        }
                        else
                        {
                            var empmail = objper[0].EmployeeEmail;
                            objper[0].EmployeeEmail = objper[0].ReportingEmail;                            
                            objper[0].ReportingEmail = empmail;
                            leave.EntityName = Convert.ToString(EntityType.LeaveReject);
                        }
                    }
                 mailID = SendLeave(leave, objper);
                 if(Msg.Msg == "Submitted Successfully")
                    {
                        leave.EntityName = Convert.ToString(EntityType.LeaveSumbit);
                        SendSumbit(leave, objper);
                    }
                }
                // if (Msg.RequestID > 0 && Msg.Msg == "Submitted Successfully")
                // {
                //     leave.LeaveID = Msg.RequestID;
                //     var temp = _docRepository.GetTemplatesByTenant(leave.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value), "Email Template", "Content Template");
                //     var objtemp = temp.Data;
                //     if (objtemp.Count > 0)
                //     {
                //         foreach (var item in objtemp)
                //         {
                //             if (item.TemplateName == "Leave Apply Template")
                //             {
                //                 var det = _docRepository.GetDocRepository(leave.TenantID, leave.TenantID, "Tenant", Convert.ToInt32(_configuration.GetSection("ProductID").Value), "Email Template");
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
                       
                //         Msg.ErrorMsg = "Template Data";
                //         if (objper != null)
                //         {
                //             if (TempName.RepoType == "Storage")
                //             {
                //                 var tag = _docRepository.GetMetatag(leave.ProductID,TempName.TemplateID, leave.LeaveID, "Leave");
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
                //                             fileName =  TempName.FileName,
                //                             folderPath = "DO00101/" + TempName.StorageFolder,
                //                             ProductCode = Convert.ToString(_configuration.GetSection("ProductID").Value)

                //                         }).Result;
                //                         Azuredoc = docsdet;
                //                         if (!string.IsNullOrEmpty(Azuredoc.DocumentURL))
                //                         {
                //                             fileContent = _docRepository.GetTemplateWithContent(Azuredoc.DocumentURL, mTag);
                //                         Msg.ErrorMsg = "Content Template";
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
                //                 var tag = _docRepository.GetMetatag(leave.ProductID,TempName.TemplateID, leave.LeaveID, "Leave");
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
                //              Msg.ErrorMsg = "Mail INprogress";
                //             if (!string.IsNullOrEmpty(fileContent))
                //             {                               
                //                 MailModel oNBO = new MailModel
                //                 {
                //                     SubscriberID = Convert.ToInt32(_configuration.GetSection("ProductID").Value),
                //                     NotifyFrom = objper.EmployeeEmail,
                //                     NotifyTo = objper.ReportingEmail,
                //                     NotifyCC = objper.HREmail,
                //                     NotifySubject = "Leave: Testing",
                //                     // NotifySubject = "Leave: " + leave.LeaveStatus,
                //                     NotifyBody = fileContent,
                //                     Entity = "Leave: " + leave.LeaveStatus,
                //                     NotifyID = leave.LeaveID,
                //                     EntityId = leave.LeaveID,
                //                     TenantID = leave.TenantID,
                //                 };
                //                 mailID = _mailrepository.SendMail(oNBO).Result;
                //                 Msg.ErrorMsg = "Mail INprogress";                                
                //             }
                //         }
                //     }
                //     var sumbitdeatails = SumbitMailLeave(leave);
                //     Msg.Msg = Msg.Msg + ",Mail ID:" + Convert.ToString(mailID);
                // Msg.ErrorMsg = "Mail Send";
                // }
                // else if (Msg.RequestID > 0 && Msg.Msg == "Updated Successfully")
                // {
                //     var temp = _docRepository.GetTemplatesByTenant(leave.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value), "Email Template", "Content Template");
                //     var objtemp = temp.Data;
                //     if (objtemp.Count > 0)
                //     {
                //         foreach (var item in objtemp)
                //         {
                //             if (leave.LeaveStatus == "Approved")
                //             {
                //                 if (item.TemplateName == "Leave Approved Template")
                //                 {
                //                     var det = _docRepository.GetDocRepository(leave.TenantID, leave.TenantID, "Tenant", Convert.ToInt32(_configuration.GetSection("ProductID").Value), "Email Template");
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
                //                 if (item.TemplateName == "Leave Reject Template")
                //                 {
                //                     var det = _docRepository.GetDocRepository(leave.TenantID, leave.TenantID, "Tenant", Convert.ToInt32(_configuration.GetSection("ProductID").Value), "Email Template");
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
                //         objper = _mailrepository.GetMailReportName(leave.EmpID);
                //         if (objper != null)
                //         {
                //             if (TempName.RepoType == "Storage")
                //             {
                //                 var tag = _docRepository.GetMetatag(leave.ProductID,TempName.TemplateID, leave.LeaveID, "Leave");
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
                //                             fileName =  TempName.FileName,
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
                //                 var tag = _docRepository.GetMetatag(leave.ProductID,TempName.TemplateID, leave.LeaveID, "Leave");
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
                //                     NotifySubject = "Leave: Testing",
                //                     // NotifySubject = "Leave: " + leave.LeaveStatus,
                //                     NotifyBody = fileContent,
                //                     Entity = "Leave: " + leave.LeaveStatus,
                //                     NotifyID = leave.LeaveID,
                //                     EntityId = leave.LeaveID,
                //                     TenantID = leave.TenantID,
                //                 };
                //                 mailID = _mailrepository.SendMail(oNBO).Result;                                
                //             }
                //         }
                //     }
                //     Msg.Msg = Msg.Msg + ",Mail ID:" + Convert.ToString(mailID);
                // }
                // else
                // {
                //     Msg.Msg = Msg.Msg + ",Mail ID:" + Convert.ToString(mailID);
                // }
                response = new Response<LeaveRequestModelMsg>(Msg, 200, "Saved Successfully");
            }
            catch (Exception ex)
            {
                StackFrame callStack = new StackFrame(1, true);
                response = new Response<LeaveRequestModelMsg>(ex.Message + "  "  + ex.InnerException 
                + ","  + callStack.GetFileName() +","+ callStack.GetFileLineNumber(), 500);
            }

            return response;
        }
        public Response<GetMyLeaveBO> GetEmpLeaveList(int teantID, int YearId, int LocationID, int EmpId)
        {
            Response<GetMyLeaveBO> response;
            try
            {
                GetMyLeaveBO objleave = new GetMyLeaveBO();
                LeaveRequestModel leav = new LeaveRequestModel();
                List<HRGetMyLeaveList> objteam = new List<HRGetMyLeaveList>();
                leav.EmpID = EmpId;
                leav.YearID = YearId;
                leav.TenantID = teantID;
                leav.LocationID = LocationID;                
                if (leav.EmpID > 0)
                {
                    leav.LeaveStatus = "Approved";
                    objleave.OutStandLeave = _leaveRepository.GetLeaveStatus(leav);
                }
                if (leav.EmpID > 0)
                {
                    leav.LeaveStatus = "Submitted";
                    objleave.OutStandRequest = _leaveRepository.GetLeaveStatus(leav);
                    var robj = _leaveRepository.GetReportandEmpList(leav);

                    foreach (var obj in robj)
                    {
                        obj.LeaveStatus = "Submitted";
                        obj.YearID = YearId;
                        var team = _leaveRepository.GetLeaveStatus(obj);
                        objteam.AddRange(team);
                    }
                    objleave.TeamMember = objteam;
                }
                if (leav.EmpID > 0)
                {
                    objleave.LOPLeave = _leaveRepository.GetLeaveLOPList(leav);
                }
                objleave.YearDetails = _holidayrepository.GetYearDetails(YearId, teantID, LocationID);
                objleave.EmpHoliday = _holidayrepository.GetEmployeeHoliday(EmpId, YearId);
                objleave.EmpOptinalHoliday = objleave.EmpHoliday.Where(s => s.isOptional == true && s.HStatus == "Approved").ToList();
                // if (objleave.EmpHoliday.Count > 0)
                // {
                    objleave.LeaveJourney = _leaveRepository.GetEmployeeCategory(leav);
                // }
                objleave.Holiday = _holidayrepository.GetHolidayList(teantID, YearId);
                objleave.UpcomingHoliday = objleave.Holiday.Where(s => Convert.ToDateTime(s.HDate).Date > DateTime.Now.Date && s.HType == "Mandatory").FirstOrDefault();
                response = new Response<GetMyLeaveBO>(objleave, 200, "Data Retraived");
            }
            catch (Exception ex)
            {
                response = new Response<GetMyLeaveBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> SaveEmployeeHoliday(EmployeeHolidayBO _holiday)
        {
            Response<int> response;
            try
            {
                int result = 0;
                result = _holidayrepository.SaveEmployeeHoliday(_holiday);
                response = new Response<int>(result, 200, "Saved Successfully");
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<MasLeaveCategoryBO>> GetCategoryList(LeaveRequestModel leave)
        {
            Response<List<MasLeaveCategoryBO>> response;
            try
            {
                HRGetMyLeaveList _objdata = new HRGetMyLeaveList();
                //int result = 0;               
                var result = _leaveRepository.GetCategoryList(leave);
                if (result.Count > 0)
                {
                    foreach (var item in result)
                    {
                        _objdata.EmpID = leave.EmpID;
                        _objdata.LeaveCategoryID = item.CategoryID;
                        _objdata.YearID = leave.YearID;
                        _objdata.TenantID = leave.TenantID;
                        _objdata.LocationID = leave.LocationID;
                        _ = _leaveRepository.SaveCategoryList(_objdata);
                    }
                
                }
                 var result1 = _leaveRepository.GetCategoryList(leave);
                response = new Response<List<MasLeaveCategoryBO>>(result1, 200, "Data Retraived");
            }
            catch (Exception ex)
            {
                response = new Response<List<MasLeaveCategoryBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> SaveLeaveCategoryMaster(HRVwBeneftisLeave_BO category)
        {
            Response<int> response;
            try
            {
                var result = _leaveRepository.SaveLeaveCategoryMaster(category);
                if(result.Id == 0)
                {
                    response = new Response<int>(result.Id, 200, result.Msg);
                }
                else
                {
                    response = new Response<int>(result.Id, 200, result.Msg);
                }
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<HRVwBeneftisLeave_BO>> GetLeaveCategoryMaster(int TenantID, int LocationID)
        {
            Response<List<HRVwBeneftisLeave_BO>> response;
            try
            {
                var result = _leaveRepository.GetLeaveCategoryMaster(TenantID, LocationID);
                response = new Response<List<HRVwBeneftisLeave_BO>>(result, 200, "Data Retraived");
            }
            catch (Exception ex)
            {
                response = new Response<List<HRVwBeneftisLeave_BO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<GetMyLeaveList> GetMyLeaveList(GetMyLeaveList objresult)
        {
            Response<GetMyLeaveList> response;
            try
            {
                var result = _leaveRepository.GetMyLeaveList(objresult);
                if (result.objMyLeaveList.Count() == 0)
                {
                    response = new Response<GetMyLeaveList>(result, 200, "Data Not Found");
                }
                else
                {
                    response = new Response<GetMyLeaveList>(result, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<GetMyLeaveList>(ex.Message, 500);
            }
            return response;
        }
        // public Response<string> SumbitMailLeave(LeaveRequestModel leave)
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
        //         var temp = _docRepository.GetTemplatesByTenant(leave.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value), "Email Template", "Content Template");
        //         var objtemp = temp.Data;
        //         if (objtemp.Count > 0)
        //         {
        //             foreach (var item in objtemp)
        //             {
        //                 if (item.TemplateName == "Leave Sumbit Template")
        //                 {
        //                     var det = _docRepository.GetDocRepository(leave.TenantID, leave.TenantID, "Tenant", Convert.ToInt32(_configuration.GetSection("ProductID").Value), "Email Template");
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
        //             objper = _mailrepository.GetMailReportName(leave.EmpID);
        //             if (objper != null)
        //             {
        //                 if (TempName.RepoType == "Storage")
        //                 {
        //                     var tag = _docRepository.GetMetatag(leave.ProductID,TempName.TemplateID, leave.LeaveID, "Leave");
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
        //                     var tag = _docRepository.GetMetatag(leave.ProductID,TempName.TemplateID, leave.LeaveID, "Leave");
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
        //                         NotifyTo = objper.EmployeeEmail,
        //                         NotifyCC = objper.HREmail,
        //                         NotifySubject = "Leave: Testing",
        //                         // NotifySubject = "Leave: " + leave.LeaveStatus,
        //                         NotifyBody = fileContent,
        //                         Entity = "Leave: " + leave.LeaveStatus,
        //                         NotifyID = leave.LeaveID,
        //                         EntityId = leave.LeaveID,
        //                         TenantID = leave.TenantID,
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
        public Response<GetMyLeaveBO> GetManagerLeaveList(GetMyLeaveListInputs _objleave)
        {
            Response<GetMyLeaveBO> response;
            try
            {
                GetMyLeaveBO objleave = new GetMyLeaveBO();
                LeaveRequestModel leav = new LeaveRequestModel();
                // int result = 0;               
                //    result = _leaveRepository.GetBenefitsByEmpBeneId(EmpId);
                //    if(result > 0)
                //    {
                //         objleave.LeaveJournal = _leaveRepository.GetBenefitsByEmpBeneId(EmpId, result, YearId, teantID);
                //    }
                leav.EmpID = _objleave.EmpID;
                leav.YearID = _objleave.YearID;
                leav.TenantID = _objleave.TenantID;
                leav.LocationID = _objleave.LocationID;
                if (leav.EmpID > 0)
                {
                    leav.LeaveStatus = "Approved";
                    objleave.OutStandLeave = _leaveRepository.GetLeaveStatus(leav);
                }
                if (leav.EmpID > 0)
                {
                    leav.LeaveStatus = "Submitted";
                    objleave.OutStandRequest = _leaveRepository.GetLeaveStatus(leav);
                }
                objleave.YearDetails = _holidayrepository.GetYearDetails(_objleave.YearID, _objleave.TenantID, _objleave.LocationID);
                objleave.EmpHoliday = _holidayrepository.GetEmployeeHoliday(_objleave.EmpID, _objleave.YearID);
                if (objleave.EmpHoliday.Count > 0)
                {
                    objleave.LeaveJourney = _leaveRepository.GetEmployeeCategory(leav);
                }
                objleave.Holiday = _holidayrepository.GetHolidayList(_objleave.TenantID, _objleave.YearID);
                objleave.UpcomingHoliday = objleave.Holiday.Where(s => Convert.ToDateTime(s.HDate).Date > DateTime.Now.Date && s.HType == "Mandatory").FirstOrDefault();
                response = new Response<GetMyLeaveBO>(objleave, 200, "Data Retraived");
            }
            catch (Exception ex)
            {
                response = new Response<GetMyLeaveBO>(ex.Message, 500);
            }
            return response;
        }
        public int SendLeave(LeaveRequestModel leave, List<MailNotifyBO> standarddata)
        {
            int mailID = 0;
            string emlContent = string.Empty;
            List<TemplateBO> TagS = new List<TemplateBO>();
            KoTemplate tpl = new KoSoft.DocTemplate.KoTemplate(_configuration.GetConnectionString("KOPRODADBConnection"), TempDBType.MySQL);
            DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("KOPRODADBConnection"), DBType.MySQL);
            List<KoSoft.DocTemplate.TemplateBO> oTpl = tpl.GetTemplateData(Convert.ToInt32(leave.TenantID), TemplateType.EmailTemplate, Convert.ToInt32(_configuration.GetSection("ProductID").Value), true);
            List<TagDataQueryBO> oQry = new List<TagDataQueryBO>();
                TagS  = oTpl.Where(s => s.Entity == leave.EntityName).ToList();
                var tagsdet = tpl.GetTemplateTags(TagS[0].TemplateID);            
            oQry = tpl.GetTagQuery(tagsdet);
            Dictionary<string, string> oParamValue = new Dictionary<string, string>();
            oParamValue.Add("requestId", Convert.ToString(leave.LeaveID));
            oParamValue.Add("leaveType", Convert.ToString(leave.EntityType));
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
                                    NotifySubject = "Leave: " + leave.LeaveStatus,
                                    // NotifySubject = "Leave: " + leave.LeaveStatus,
                                    NotifyBody = emlContent,
                                    Entity = "Leave: " + leave.LeaveStatus,
                                    NotifyID = leave.LeaveID,
                                    EntityId = leave.LeaveID,
                                    TenantID = leave.TenantID,
                                };
                               mailID = _mailrepository.SendMail(oNBO).Result;
                              //  Msg.ErrorMsg = "Mail INprogress";
                        
                    }
                }
            }
            return mailID;
        }
        public void SendSumbit(LeaveRequestModel leave, List<MailNotifyBO> standarddata)
        {
            int mailID = 0;
            string emlContent = string.Empty;
            List<TemplateBO> TagS = new List<TemplateBO>();
            KoTemplate tpl = new KoTemplate(_configuration.GetConnectionString("KOPRODADBConnection"), TempDBType.MySQL);
            DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("KOPRODADBConnection"), DBType.MySQL);
            List<KoSoft.DocTemplate.TemplateBO> oTpl = tpl.GetTemplateData(Convert.ToInt32(leave.TenantID), TemplateType.EmailTemplate, Convert.ToInt32(_configuration.GetSection("ProductID").Value), true);
            List<TagDataQueryBO> oQry = new List<TagDataQueryBO>();
            
            TagS  = oTpl.Where(s => s.Entity == leave.EntityName).ToList();
                var tagsdet = tpl.GetTemplateTags(TagS[0].TemplateID);            
            oQry = tpl.GetTagQuery(tagsdet);
            Dictionary<string, string> oParamValue = new Dictionary<string, string>();
            oParamValue.Add("requestId", Convert.ToString(leave.LeaveID));
            oParamValue.Add("leaveType", Convert.ToString(leave.EntityType));
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
                                    NotifySubject = "Leave: " + leave.LeaveStatus,
                                    // NotifySubject = "Leave: " + leave.LeaveStatus,
                                    NotifyBody = emlContent,
                                    Entity = "Leave: " + leave.LeaveStatus,
                                    NotifyID = leave.LeaveID,
                                    EntityId = leave.LeaveID,
                                    TenantID = leave.TenantID,
                                };
                               mailID = _mailrepository.SendMail(oNBO).Result;
                              //  Msg.ErrorMsg = "Mail INprogress";
                        
                    }
                }
            }
           // return mailID;
        }
        public Response<LeaveRequestModelMsg> MAddNewLeaveRequest(LeaveRequestModel leave)
        {
            Response<LeaveRequestModelMsg> response;
            int mailID = 0;
            string Objresult = string.Empty;
            string RootPath = string.Empty;
            string fileContent = string.Empty;
           // TemplatedetailBO TempName = new TemplatedetailBO();
           // MailNotifyBO objper = new MailNotifyBO();
            //AzureDocURLBO Azuredoc = new AzureDocURLBO();
            try
            {
                var Msg = _leaveRepository.MAddNewLeaveRequest(leave);
                // if (Msg.RequestID > 0)
                // {
                    //var Leavedetails = _leaveRepository.MGetLeave(Msg.RequestID, leave.TenantID, leave.LocationID);
                    // if (Leavedetails.Count > 0)
                    // {
                    //     if (Leavedetails[0].LeaveStatus == "Approved")
                    //     {
                    //         var mm = _leaveRepository.MSaveLeaveAlloc(Leavedetails[0]);
                    //     }
                    // }
                //     leave.LeaveID = Msg.RequestID;
                //     leave.EntityType  = "Leave";
                //   var objper = _mailrepository.GetMailReportName(leave.EmpID);
                //   if(Msg.Msg == "Submitted Successfully")
                //     {
                //         leave.EntityName = Convert.ToString(EntityType.LeaveApply);
                //     }
                //     if(Msg.Msg == "Updated Successfully")
                //     {
                //         if(leave.LeaveStatus  == "Approved")
                //         {
                //             var empmail = objper[0].EmployeeEmail;
                //             objper[0].EmployeeEmail = objper[0].ReportingEmail;                            
                //             objper[0].ReportingEmail = empmail;
                //             leave.EntityName = Convert.ToString(EntityType.LeaveApproved);                            
                //         }
                //         else
                //         {
                //             var empmail = objper[0].EmployeeEmail;
                //             objper[0].EmployeeEmail = objper[0].ReportingEmail;                            
                //             objper[0].ReportingEmail = empmail;
                //             leave.EntityName = Convert.ToString(EntityType.LeaveReject);
                //         }
                //     }
                //  mailID = SendLeave(leave, objper);
                //  if(Msg.Msg == "Submitted Successfully")
                //     {
                //         leave.EntityName = Convert.ToString(EntityType.LeaveSumbit);
                //         SendSumbit(leave, objper);
                //     }
                //}
                response = new Response<LeaveRequestModelMsg>(Msg, 200,Msg.Msg);
            }
            catch (Exception ex)
            {
                StackFrame callStack = new StackFrame(1, true);
                response = new Response<LeaveRequestModelMsg>(ex.Message + "  "  + ex.InnerException 
                + ","  + callStack.GetFileName() +","+ callStack.GetFileLineNumber(), 500);
            }
            return response;
        }
        public Response<List<HRGetMyLeaveList>> MGetMyLeaveList(GetMyLeaveListInputs objresult)
        {
            Response<List<HRGetMyLeaveList>> response;
            try
            {
                var result = _leaveRepository.MGetMyLeaveList(objresult);
                if (result.Count() == 0)
                {
                    response = new Response<List<HRGetMyLeaveList>>(result, 200, "Data Not Found");
                }
                else
                {
                    response = new Response<List<HRGetMyLeaveList>>(result, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<HRGetMyLeaveList>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<MobileLeaveCategoryBO>> MGetCategoryList(LeaveRequestModel leave)
        {
            Response<List<MobileLeaveCategoryBO>> response;
            try
            {
                HRGetMyLeaveList _objdata = new HRGetMyLeaveList();
                //int result = 0;               
                var result = _leaveRepository.MGetCategoryList(leave);
                if (result.Count > 0)
                {
                    foreach (var item in result)
                    {
                        _objdata.EmpID = leave.EmpID;
                        _objdata.LeaveCategoryID = item.CategoryID;
                        _objdata.YearID = leave.YearID;
                        _objdata.TenantID = leave.TenantID;
                        _objdata.LocationID = leave.LocationID;
                        _ = _leaveRepository.MSaveCategoryList(_objdata);
                    }
                
                }
                 var result1 = _leaveRepository.MGetCategoryList(leave);
                response = new Response<List<MobileLeaveCategoryBO>>(result1, 200, "Data Retraived");
            }
            catch (Exception ex)
            {
                response = new Response<List<MobileLeaveCategoryBO>>(ex.Message, 500);
            }
            return response;
        }
    }
}