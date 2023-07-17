using Microsoft.Extensions.Configuration;
using MyDodos.Domain.LeaveBO;
using MyDodos.Domain.Wrapper;
using MyDodos.Domain.ProjectManagement;
using MyDodos.Repository.Administrative;
using MyDodos.Repository.ProjectManagement;
using MyDodos.ViewModel.ProjectManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyDodos.Domain.Mail;
using KoSoft.DocTemplate;
using KoSoft.DocRepo;
using MyDodos.Domain.AzureStorage;
using MyDodos.Repository.AzureStorage;
using MyDodos.Repository.TemplateManager;
using MyDodos.Repository.Mail;

namespace MyDodos.Service.ProjectManagement
{
    public class TimeSheetService : ITimeSheetService
    {
        private readonly IConfiguration _configuration;
        private readonly IProjectRepository _projectRepository;
        private readonly IAdministrativeRepository _administrativeRepository;
        private readonly ITimeSheetRepository _timeSheetRepository;
        private readonly IStorageConnect _storage;
        private readonly IDocRepository _docRepository;
        private readonly IMailRepository _mailrepository;
        public TimeSheetService(IConfiguration configuration, IProjectRepository projectRepository,IAdministrativeRepository administrativeRepository,ITimeSheetRepository timeSheetRepository, IStorageConnect storage, IDocRepository docRepository, IMailRepository mailrepository)
        {
            _configuration = configuration;
            _projectRepository =  projectRepository;
            _administrativeRepository = administrativeRepository;
            _timeSheetRepository = timeSheetRepository;
            _storage = storage;
            _docRepository = docRepository;
            _mailrepository = mailrepository;
        }
        public Response<LeaveRequestModelMsg> SaveTimeSheet(PPTimeSheetBO timeSheet)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                var result = _timeSheetRepository.SaveTimeSheet(timeSheet);
                if (result.RequestID == 0)
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"TimeSheet Data Creation or Updation is Failed");
                }
                else
                {
                    response = new Response<LeaveRequestModelMsg>(result,200,"Saved Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<TimeSheetTaskBO> GetTimeSheetTasks(TimesheetInputBO task)
        {
            Response<TimeSheetTaskBO> response;
            try
            {
                TimeSheetTaskBO objtask = new TimeSheetTaskBO();
                var result = _timeSheetRepository.GetTimeSheetTasks(task);
                if(result.Count()>0)
                {
                    objtask.TimeSheetID = result[0].TimeUnquieID;
                    objtask.Task = result;
                }
                if (result.Count() == 0)
                {
                    response = new Response<TimeSheetTaskBO>(objtask,200,"Data Not Found");
                }
                else
                {
                    response = new Response<TimeSheetTaskBO>(objtask,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<TimeSheetTaskBO>(ex.Message,500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> SaveWeekTSNonBillable(BillNonBillable taskbnb)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                if (taskbnb.Billable.Count > 0)
                {
                    foreach (var bill in taskbnb.Billable)
                    {
                        _timeSheetRepository.SaveWeekTimeSheet(bill);
                        if (bill.PTaskID != null)
                        {
                            _timeSheetRepository.SaveTimesheetOverAll(new vwStatusWeekTimeSheetBO
                            {
                                TimeSheetStatus = bill.WTimeSheetStatus,
                                TaskStatus = bill.WTimeSheetStatus,
                                TimeSheetID = bill.TimeSheetID,
                                EmpID = bill.EmpID,
                                YearID = bill.YearID,
                                WeekNo = bill.WeekNo,
                                PTaskID = bill.PTaskID,
                                TenantID = bill.TenantID,
                                LocationID = bill.LocationID,
                                CreatedBy = bill.CreatedBy
                            });
                        }
                    }
                }
                if (taskbnb.NonBillable.Count > 0)
                {
                    foreach (var nbill in taskbnb.NonBillable)
                    {
                        _timeSheetRepository.SaveWeekTSNonBillable(nbill);
                        if (nbill.TimeSheetNBStatus == "Awaiting")
                        {
                            var exception = _timeSheetRepository.SaveTimeSheetFlagged(new TimesheetInputBO
                            {
                                YearID = nbill.YearID,
                                EmpID = nbill.EmpID,
                                TimeSheetID = nbill.TimeSheetID,
                                TimeSheetStatus = nbill.TimeSheetNBStatus,
                                TenantID = nbill.TenantID,
                                LocationID = nbill.LocationID,
                                GetDateTime = nbill.GetDateTime
                            });
                            _timeSheetRepository.UpdateTimeSheetFlagRelease(nbill);
                        }
                    }
                }
                response = new Response<LeaveRequestModelMsg>(null,200,"Saved Successfully");
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<List<PPTimeSheetBO>> GetTimeSheetList(TimesheetInputBO list)
        {
            Response<List<PPTimeSheetBO>> response;
            try
            {
                var result = _timeSheetRepository.GetTimeSheetList(list);
                if (result.Count() == 0)
                {
                    response = new Response<List<PPTimeSheetBO>>(result,200,"Data Not Found");
                }
                else
                {
                    response = new Response<List<PPTimeSheetBO>>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<PPTimeSheetBO>>(ex.Message,500);
            }
            return response;
        }
        public Response<PPTimeSheetBO> TSBillableNonBillableList(TimesheetInputBO list)
        {
            Response<PPTimeSheetBO> response;
            try
            {
                PPTimeSheetBO lists = new PPTimeSheetBO();
                var result = _timeSheetRepository.GetTimeSheetDataList(list);
                if (result == null)
                {
                    response = new Response<PPTimeSheetBO>(null, 200, "Data Not Found");
                    var timesheet = _timeSheetRepository.SaveTimeSheet(new PPTimeSheetBO
                    {
                        EmpID = list.EmpID,
                        TimeSheetStatus = "Active",
                        TenantID = list.TenantID,
                        LocationID = list.LocationID,
                        YearID = list.YearID,
                        CreatedBy = list.EmpID
                    });
                    _timeSheetRepository.SaveMasterWeekTSNonBillable(new PPWeekTSNonBillableBO
                    {
                        EmpID = list.EmpID,
                        YearID = list.YearID,
                        TimeSheetID = timesheet.RequestID,
                        TimeSheetNBStatus = "Active",
                        EstStartDate = list.StartDate,
                        TenantID = list.TenantID,
                        LocationID = list.LocationID,
                        CreatedBy = list.EmpID
                    });
                    var results = _timeSheetRepository.GetTimeSheetDataList(list);
                    _timeSheetRepository.GetTimeSheetTasksNBData(new PPWeekTSNonBillableBO
                    {
                        EmpID = list.EmpID,
                        YearID = list.YearID,
                        WeekNo = results.WeekNo,
                        TenantID = list.TenantID
                    });
                    var nonbillable = _timeSheetRepository.GetTimeSheetNonBillableData(list.EmpID, timesheet.RequestID);
                    results.TimeSheetNonBillableData = nonbillable;
                    response = new Response<PPTimeSheetBO>(results, 200, "Data Retraived");
                }
                else
                {
                    /* Exception Data */
                    if(result.IsCurrException == false)
                    {
                        var exception = _timeSheetRepository.SaveTimeSheetFlagged(new TimesheetInputBO
                        {
                            YearID = result.YearID,
                            EmpID = result.EmpID,
                            TimeSheetID = 0,
                            TimeSheetStatus = result.TimeSheetStatus,
                            TenantID = result.TenantID,
                            LocationID = result.LocationID,
                            GetDateTime = list.GetDateTime
                        });
                        if (exception.Count() > 0)
                        {
                            result.IsCurrException = true;
                        }
                        else
                        {
                            result.IsCurrException = false;
                        }
                    }
                    /*Time sheet Get Call*/
                    var billable = _timeSheetRepository.GetTimeSheetBillableData(result.EmpID, result.TimeSheetID);
                    result.TimeSheetBillableData = billable;
                    _timeSheetRepository.GetTimeSheetTasksNBData(new PPWeekTSNonBillableBO
                    {
                        EmpID = list.EmpID,
                        YearID = list.YearID,
                        WeekNo = result.WeekNo,
                        TenantID = list.TenantID
                    });
                    var nonbillable = _timeSheetRepository.GetTimeSheetNonBillableData(result.EmpID, result.TimeSheetID);
                    result.TimeSheetNonBillableData = nonbillable;
                    if (result.TimeSheetNonBillableData.Count == 0)
                    {
                        _timeSheetRepository.SaveMasterWeekTSNonBillable(new PPWeekTSNonBillableBO
                        {
                            EmpID = list.EmpID,
                            YearID = list.YearID,
                            TimeSheetID = result.TimeSheetID,
                            TimeSheetNBStatus = "Active",
                            EstStartDate = list.StartDate,
                            TenantID = list.TenantID,
                            LocationID = list.LocationID,
                            CreatedBy = list.EmpID
                        });
                        _timeSheetRepository.GetTimeSheetTasksNBData(new PPWeekTSNonBillableBO
                        {
                            EmpID = list.EmpID,
                            YearID = list.YearID,
                            WeekNo = result.WeekNo,
                            TenantID = list.TenantID
                        });
                        var nonbillables = _timeSheetRepository.GetTimeSheetNonBillableData(list.EmpID, result.TimeSheetID);
                        result.TimeSheetNonBillableData = nonbillable;
                    }
                    response = new Response<PPTimeSheetBO>(result, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<PPTimeSheetBO>(ex.Message,500);
            }
            return response;
        }
        public Response<GetTimeSheetList> GetTimeSheetData(GetTimeSheetList timesheet)
        {
            Response<GetTimeSheetList> response;
            try
            {
                var result = _timeSheetRepository.GetTimeSheetData(timesheet);         
                if (result.objTimeSheetList.Count() == 0)
                {      
                    response = new Response<GetTimeSheetList>(result, 200, "Data Not Found");
                }
                else
                {      
                    foreach(var item in result.objTimeSheetList)
                    {
                        //var paid = _timeSheetRepository.GetTimeSheetPaidStatus(item.TenantID,item.YearID,item.WeekNo,item.EmpID);
                        //item.PaidStatus = paid;
                        var hours = _timeSheetRepository.GetTSBillNonBillHours(item);
                        item.TSHours = hours;
                    }
                    response = new Response<GetTimeSheetList>(result, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<GetTimeSheetList>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> GetWeekcount()
        {
            Response<int> response;
            try
            {
                var result = _timeSheetRepository.GetWeekcount();
                if (result == 0)
                {
                    response = new Response<int>(result,200,"Data Not Found");
                }
                else
                {
                    response = new Response<int>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message,500);
            }
            return response;
        }
        public Response<List<WeekDateRange>> GetWeekDateRange(int TenantID,int LocationID,DateTime AttendanceDate)
        {
            Response<List<WeekDateRange>> response;
            try
            {
                var result = _timeSheetRepository.GetWeekDateRange(TenantID,LocationID,AttendanceDate);
                if (result.Count() == 0)
                {
                    response = new Response<List<WeekDateRange>>(result,200,"Data Not Found");
                }
                else
                {
                    response = new Response<List<WeekDateRange>>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<WeekDateRange>>(ex.Message,500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> SaveTimeSheetTaskApply(PPWeekTimeSheetBO timeSheet)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                if(timeSheet.PTaskIDs!=null && timeSheet.PTaskIDs!="")
                {
                    string[] split = timeSheet.PTaskIDs.Split(',');
                    foreach (var str in split)
                    {
                        var result = _timeSheetRepository.SaveTimeSheetTaskApply(new PPWeekTimeSheetBO
                        {
                            TimeSheetID = timeSheet.TimeSheetID,
                            EmpID = timeSheet.EmpID,
                            YearID = timeSheet.YearID,
                            WeekNo = timeSheet.WeekNo,
                            WTimeSheetStatus = timeSheet.WTimeSheetStatus,
                            PTaskID = Convert.ToInt32(str),
                            TenantID = timeSheet.TenantID,
                            LocationID = timeSheet.LocationID,
                            CreatedBy = timeSheet.CreatedBy
                        });
                        _timeSheetRepository.SaveTimesheetOverAll(new vwStatusWeekTimeSheetBO
                        {
                            TimeSheetStatus = timeSheet.WTimeSheetStatus,
                            TaskStatus = timeSheet.WTimeSheetStatus,
                            TimeSheetID = timeSheet.TimeSheetID,
                            EmpID = timeSheet.EmpID,
                            YearID = timeSheet.YearID,
                            WeekNo = timeSheet.WeekNo,
                            PTaskID = Convert.ToInt32(str),
                            TenantID = timeSheet.TenantID,
                            LocationID = timeSheet.LocationID,
                            CreatedBy = timeSheet.CreatedBy
                        });
                    }
                }
                    response = new Response<LeaveRequestModelMsg>(null,200,"Saved Successfully");
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<TimeSheetFlaggedBO> SaveTimeSheetFlagged(TimesheetInputBO timeSheet)
        {
            Response<TimeSheetFlaggedBO> response;
            try
            {
                TimeSheetFlaggedBO objflag = new TimeSheetFlaggedBO();
                if (string.IsNullOrEmpty(timeSheet.TimeSheetStatus))
                {
                    timeSheet.TimeSheetStatus = "Flagged";
                }
                var result = _timeSheetRepository.SaveTimeSheetFlagged(timeSheet);                
                if(result.Count() > 0)
                {
                    objflag.TimeSheetCount = result.Count();
                }
                else
                {
                    objflag.TimeSheetCount = 0;
                }
                if (result.Count() == 0)
                {
                    response = new Response<TimeSheetFlaggedBO>(objflag,200,"Data Not Found");
                }
                else
                {
                    response = new Response<TimeSheetFlaggedBO>(objflag,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<TimeSheetFlaggedBO>(ex.Message,500);
            }
            return response;
        }
        public Response<TimeSheetEmpReportList> GetTimeSheetEmpReportData(TimeSheetEmpReportList timesheet)
        {
            Response<TimeSheetEmpReportList> response;
            try
            {
                var result = _timeSheetRepository.GetTimeSheetEmpReportData(timesheet);         
                if (result.objTSEmpReportList.Count() == 0)
                {      
                    response = new Response<TimeSheetEmpReportList>(result, 200, "Data Not Found");
                }
                else
                {      
                    response = new Response<TimeSheetEmpReportList>(result, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<TimeSheetEmpReportList>(ex.Message, 500);
            }
            return response;
        }
        public Response<TSExcReportResultList> GetTSExcReportResult(TSExcReportResultList timesheet)
        {
            Response<TSExcReportResultList> response;
            try
            {
                var result = _timeSheetRepository.GetTSExcReportResult(timesheet);         
                if (result.objTSReportResultList.Count() == 0)
                {      
                    response = new Response<TSExcReportResultList>(result, 200, "Data Not Found");
                }
                else
                {      
                    response = new Response<TSExcReportResultList>(result, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<TSExcReportResultList>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<TimeSheetWeek>> GetWeekDropdown()
        {
            Response<List<TimeSheetWeek>> response;
            try
            {
                var result = _timeSheetRepository.GetWeekDropdown();
                if (result.Count() == 0)
                {
                    response = new Response<List<TimeSheetWeek>>(result,200,"Data Not Found");
                }
                else
                {
                    response = new Response<List<TimeSheetWeek>>(result,200,"Data Retreived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<TimeSheetWeek>>(ex.Message,500);
            }
            return response;
        }
        public Response<List<TimeSheetException>> GetConsoleTimesheet(int TenantID, int LocationID)
        {
            Response<List<TimeSheetException>> response;
            try
            {
                MailInputModel objinput = new MailInputModel();
                var result = _timeSheetRepository.GetConsoleTimesheet(TenantID, LocationID);
                if (result.Count() > 0)
                {
                    foreach (var obj in result)
                    {
                        if (obj.EmpID > 0)
                        {
                            objinput.EntityID = obj.TimeSheetID;
                            objinput.TenantID = TenantID;
                            objinput.LocationID = LocationID;
                            objinput.EntityType = "Timesheet";
                            objinput.EntityName = Convert.ToString(EntityType.TimesheetExceptionApply);
                            objinput.EntityStatus = obj.TimeSheetStatus;
                            var objper = _mailrepository.GetMailReportName(obj.EmpID);
                            objinput.EntityID = SendLeave(objinput, objper);
                        }
                        else
                        {
                            response = new Response<List<TimeSheetException>>(result, 200, "Mail Not Send");
                        }
                    }
                    response = new Response<List<TimeSheetException>>(result, 200, "Mail Send");
                }
                else
                {
                    response = new Response<List<TimeSheetException>>(result, 200, "Data Not Found");
                }

            }
            catch (Exception ex)
            {
                response = new Response<List<TimeSheetException>>(ex.Message, 500);
            }
            return response;
        }
        public int SendLeave(MailInputModel objinput, List<MailNotifyBO> standarddata)
        {
            int mailID = 0;
            string emlContent = string.Empty;
            List<TemplateBO> TagS = new List<TemplateBO>();
            KoTemplate tpl = new KoSoft.DocTemplate.KoTemplate(_configuration.GetConnectionString("KOPRODADBConnection"), TempDBType.MySQL);
            DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("KOPRODADBConnection"), DBType.MySQL);
            List<KoSoft.DocTemplate.TemplateBO> oTpl = tpl.GetTemplateData(Convert.ToInt32(objinput.TenantID), TemplateType.EmailTemplate, Convert.ToInt32(_configuration.GetSection("ProductID").Value), true);
            List<TagDataQueryBO> oQry = new List<TagDataQueryBO>();
            TagS = oTpl.Where(s => s.Entity == objinput.EntityName).ToList();
            var tagsdet = tpl.GetTemplateTags(TagS[0].TemplateID);
            oQry = tpl.GetTagQuery(tagsdet);
            Dictionary<string, string> oParamValue = new Dictionary<string, string>();
            oParamValue.Add("requestId", Convert.ToString(objinput.EntityID));
            oParamValue.Add("leaveType", Convert.ToString(objinput.EntityType));
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
                        MailModel oNBO = new MailModel
                        {
                            SubscriberID = Convert.ToInt32(_configuration.GetSection("ProductID").Value),
                            NotifyFrom = item.EmployeeEmail,
                            NotifyTo = item.ReportingEmail,
                            NotifyCC = item.HREmail,
                            NotifySubject = objinput.EntityType + ": " + objinput.EntityStatus,
                            NotifyBody = emlContent,
                            Entity = objinput.EntityType + ": " + objinput.EntityStatus,
                            NotifyID = objinput.EntityID,
                            EntityId = objinput.EntityID,
                            TenantID = objinput.TenantID,
                        };
                        mailID = _mailrepository.SendMail(oNBO).Result;
                    }
                }
            }
            return mailID;
        }
        public Response<List<TimeSheetSummaryBO>> GetTimeSheetSummary(int TenantID, int LocationID, int EmpID,int ManagerID)
        {
            Response<List<TimeSheetSummaryBO>> response;
            try
            {
                var result = _timeSheetRepository.GetTimeSheetSummary(TenantID,LocationID,EmpID,ManagerID);
                if (result.Count() == 0)
                {
                    response = new Response<List<TimeSheetSummaryBO>>(result,200,"Data Not Found");
                }
                else
                {
                    response = new Response<List<TimeSheetSummaryBO>>(result,200,"Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<TimeSheetSummaryBO>>(ex.Message,500);
            }
            return response;
        }
        public Response<LeaveRequestModelMsg> UpdateTimeSheetPaidStatus(List<UpdateTimeSheetPaidStatusBO> project)
        {
            Response<LeaveRequestModelMsg> response;
            try
            {
                    foreach (var str in project)
                    {
                        var result = _timeSheetRepository.UpdateTimeSheetPaidStatus(str);
                    }
                    response = new Response<LeaveRequestModelMsg>(null,200,"Updated Successfully");
            }
            catch (Exception ex)
            {
                response = new Response<LeaveRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
    }
}