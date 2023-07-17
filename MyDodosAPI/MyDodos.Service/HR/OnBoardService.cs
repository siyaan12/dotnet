using Microsoft.Extensions.Configuration;
using MyDodos.Domain.AzureStorage;
using MyDodos.ViewModel.HR;
using MyDodos.Domain.HR;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.AzureStorage;
using MyDodos.Repository.HR;
using MyDodos.Repository.Auth;
using MyDodos.Domain.AuthBO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyDodos.ViewModel.Business;
using MyDodos.ViewModel.Document;
using MyDodos.Repository.TemplateManager;
using MyDodos.Repository.Mail;
using System.Net.Mail;
using System.Net;
using MyDodos.Domain.Mail;
using KoSoft.DocRepo;
using KoSoft.DocTemplate;
using MyDodos.ViewModel.Entitlement;
using MyDodos.Repository.Payroll;
using MyDodos.Domain.Payroll;
using MyDodos.Domain.BenefitManagement;
using MyDodos.Repository.BenefitManagement;
using MyDodos.ViewModel.Common;
using MyDodos.ViewModel.Payroll;
using MyDodos.ViewModel.Employee;
using MyDodos.Repository.Employee;
using MyDodos.Repository.HRMS;

namespace MyDodos.Service.HR
{
    public class OnBoardService : IOnBoardService
    {
        private readonly IConfiguration _configuration;
        private readonly IOnBoardRepository _onBoardRepository;
        private readonly IAuthRepository _authRepository;
        private readonly IStorageConnect _storage;
        private readonly IMailRepository _mailRepository;
        private readonly IDocRepository _docRepository;
        private readonly IPayrollRepository _payrollRepository;
        private readonly ILeaveBenefitRepository _leaveBenefitRepository;
        private readonly IDisabilityBenefitRepository _disabilityBenefitRepository;
        private readonly IBenefitGroupRepository _benefitGroupRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public readonly IPayrollMasterRepository _payrollMasterRepository;
        public readonly ICommonRepository _commonRepository;
        public readonly IMedicalInsuranceRepository _medicalInsuranceRepository;
        private readonly IHrmsInstanceRepository _hrmsInstanceRepository;

        public OnBoardService(IConfiguration configuration, ICommonRepository commonRepository, IOnBoardRepository onBoardRepository, IStorageConnect storage, IDocRepository docRepository,IAuthRepository authRepository,IMailRepository mailRepository,IPayrollRepository payrollRepository,ILeaveBenefitRepository leaveBenefitRepository,IDisabilityBenefitRepository disabilityBenefitRepository,IBenefitGroupRepository benefitGroupRepository,IEmployeeRepository employeeRepository,IPayrollMasterRepository payrollMasterRepository,IMedicalInsuranceRepository medicalInsuranceRepository, IHrmsInstanceRepository hrmsInstanceRepository)
        {
            _configuration = configuration;
            _commonRepository = commonRepository;
            _onBoardRepository = onBoardRepository;
            _storage = storage;
            _docRepository = docRepository;
            _authRepository = authRepository;
            _mailRepository = mailRepository;
            _payrollRepository = payrollRepository;
            _leaveBenefitRepository = leaveBenefitRepository;
            _disabilityBenefitRepository = disabilityBenefitRepository;
            _benefitGroupRepository = benefitGroupRepository;
            _employeeRepository = employeeRepository;
            _payrollMasterRepository = payrollMasterRepository;
            _medicalInsuranceRepository = medicalInsuranceRepository;
            _hrmsInstanceRepository = hrmsInstanceRepository;
        }
        public Response<int> UpdateOnBoardSetting(BPProcessBO bpBo)
        {
            Response<int> response;
            try
            {
                int objres = 0;
                var Updatesett = _onBoardRepository.UpdateOnBoardSetting(bpBo);
                var objdata = _onBoardRepository.GetOnBoardSetting(bpBo.TenantID, bpBo.LocationID, bpBo.TransOrder, "Onboarding");
                var obj = objdata.Data;
                if (obj.Count > 0)
                {
                    foreach (var item in obj)
                    {
                        bpBo.TransOrder = item.TransOrder;
                        bpBo.bpids = "";
                        objres = _onBoardRepository.UpdateOnBoardSetting(bpBo);
                    }
                }
                response = new Response<int>(Updatesett, 200, "Saved Successfully");
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<RoleBO>> GetEntRoles(int ProductID, string GroupType)
        {
            Response<List<RoleBO>> response;
            try
            {
                var objres = _authRepository.GetEntRoles(ProductID, GroupType);
                var rtnobj = objres.Data;
                response = new Response<List<RoleBO>>(rtnobj, 200, "Data Reterived");
            }
            catch (Exception ex)
            {
                response = new Response<List<RoleBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<EntRolesBO>> GetEntTenantRoles(InpurtEntRolesBO roles)
        {
            Response<List<EntRolesBO>> response;
            try
            {
                var objres = _authRepository.GetEntTenantRoles(roles);
                var rtnobj = objres.Data;
                response = new Response<List<EntRolesBO>>(rtnobj, 200, "Data Reterived");
            }
            catch (Exception ex)
            {
                response = new Response<List<EntRolesBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<RtnUserGroupBO>> GetAccountTypes(int ProductID, int TenantID)
        {
            Response<List<RtnUserGroupBO>> response;
            try
            {
                var objres = _authRepository.GetAccountTypes(ProductID, TenantID);
                var rtnobj = objres.Data;
                response = new Response<List<RtnUserGroupBO>>(rtnobj, 200, "Data Reterived");

            }
            catch (Exception ex)
            {
                response = new Response<List<RtnUserGroupBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> SaveOnboardEducation(HRInputEmpEducation Edu)
        {
            Response<int> response;
            try
            {
                int objres = 0;
                if (Edu.onEdu.Count > 0)
                {
                    foreach (var item in Edu.onEdu)
                    {
                        objres = _onBoardRepository.SaveOnboardEducation(item);
                    }
                }
                response = new Response<int>(objres, 200, "Saved Successfully");
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<OnBoardRequestModelMsg> AddOnBoardingForm(EmpOnboardingModBO onboarding)
        {
            Response<OnBoardRequestModelMsg> response;
            try
            {
                OnboardPersonalDetail obj = new OnboardPersonalDetail();
                //int result = 0;
                //onboarding.AppUser = new InputAppUserBO();
                onboarding.employee = new OnboardPersonalDetail();
                var result = _onBoardRepository.AddOnBoardingForm(onboarding);
                if (result.ReqID > 0)
                {
                    var trans = _onBoardRepository.SaveBPTransInstance(onboarding.TenantID,onboarding.LocationID,result.ReqID);
                    var onboard = _onBoardRepository.GetOnboardTrack(result.ReqID,onboarding.LocationID);
                    var empids = _onBoardRepository.GetEmployee(onboard.EmpID,onboarding.TenantID,onboarding.LocationID);
                    if (empids.Count > 0)
                    {
                        empids[0].EmpOnboardingID = result.ReqID;
                        empids[0].EmpStatus = onboarding.RequestStatus;
                        empids[0].FirstName = onboarding.FirstName;
                        empids[0].MiddleName = onboarding.MiddleName;
                        empids[0].LastName = onboarding.LastName;
                        empids[0].TenantID = onboarding.TenantID;
                        empids[0].LocationID = onboarding.LocationID;
                        //empids[0].Gender = onboarding.Gender;
                        //empids[0].Suffix = onboarding.Suffix;
                        //empids[0].DOB = onboarding.DOB;
                        //empids[0].MaritalStatus = onboarding.Maritalstatus;
                        //empids[0].Nationality = onboarding.Nationality;
                        //empids[0].BloodGroup = onboarding.BloodGroup;
                        if (!string.IsNullOrEmpty(empids[0].DOB.ToString()))
                        {
                            empids[0].DOB = Convert.ToDateTime(empids[0].DOB).Date;
                        }
                        onboarding.EmpID = _onBoardRepository.InsertOnBoardPersonal(empids[0]);
                    }
                    else
                    {
                        onboarding.employee.EmpOnboardingID = result.ReqID;
                        onboarding.employee.EmpStatus = onboarding.RequestStatus;
                        onboarding.employee.FirstName = onboarding.FirstName;
                        onboarding.employee.MiddleName = onboarding.MiddleName;
                        onboarding.employee.LastName = onboarding.LastName;
                        onboarding.employee.TenantID = onboarding.TenantID;
                        onboarding.employee.LocationID = onboarding.LocationID;
                        //onboarding.employee.Suffix = onboarding.Suffix;
                        //onboarding.employee.Gender = onboarding.Gender;
                        //onboarding.employee.MaritalStatus = onboarding.Maritalstatus;
                        //onboarding.employee.DOB = onboarding.DOB;
                        //onboarding.employee.Nationality = onboarding.Nationality;
                        //onboarding.employee.BloodGroup = onboarding.BloodGroup;
                        onboarding.EmpID = _onBoardRepository.InsertOnBoardPersonal(onboarding.employee);
                    }
                //     if (onboarding.Address.Count > 0)
                //     {
                //         if (onboarding.EmpID > 0)
                //         {
                //             onboarding.Address[0].EmpRefID = onboarding.EmpID;
                //             var mm = _onBoardRepository.SaveEmployeeAddress(onboarding.Address);
                //         }
                //     }
                }
                if (result.ReqID == 0)
                {
                    response = new Response<OnBoardRequestModelMsg>(result, 500, "Onboard Data Creation or Updation is Failed");
                }
                else
                {
                    response = new Response<OnBoardRequestModelMsg>(result, 200,result.Msg);
                }
            }
            catch (Exception ex)
            {
                response = new Response<OnBoardRequestModelMsg>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<OnBoardingResourceBO>> GetHROnboardResource(int productId, int TenantID, int LocationID)
        {
            Response<List<OnBoardingResourceBO>> response;
            try
            {
                var objres = _onBoardRepository.GetHROnboardResource(TenantID, LocationID);
                foreach (var Idcard in objres)
                {
                    Idcard.base64Images = "";
                    DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                   // DocumentDA objdocservices = new DocumentDA(_configuration.GetConnectionString("KOPRODADBConnection"), DBType.MySQL);
                    var result = objdoc.GetDocument(0, Idcard.EmpID, "", "IDCardImages", TenantID);
                    if (result.Count > 0)
                    {
                        var objcont = objdoc.GetDocContainer(TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                        //string TenantName = objcont[0].ContainerName.ToLower();
                        var doc = _storage.DownloadDocument(new SaveDocCloudBO
                        {
                            CloudType = _configuration.GetSection("CloudType").Value,
                            Container = objcont[0].ContainerName.ToLower(),
                            fileName = result[0].GenDocName,
                            folderPath = result[0].DirectionPath,
                            ProductCode = Convert.ToString(productId)

                        }).Result;
                        Idcard.base64Images = doc.DocumentURL;
                    }

                }
                response = new Response<List<OnBoardingResourceBO>>(objres, 200, "Data Reterived");
            }
            catch (Exception ex)
            {
                response = new Response<List<OnBoardingResourceBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<Tuple<OnBoardingGenralBO, AppUserDetailsBO, OnBoardRequestModelMsg>> SaveHROnboardGenral(OnBoardingGenralBO genral)
        {
            Response<Tuple<OnBoardingGenralBO, AppUserDetailsBO, OnBoardRequestModelMsg>> response;
            try
            {
                //string emlContent = string.Empty;
                //string tomail = string.Empty;
                int mailID = 0;
                string Objresult = string.Empty;
                string RootPath = string.Empty;
                string fileContent = string.Empty;
                AppUserDetailsBO  appuserdet = new AppUserDetailsBO ();
                // MailNotifyBO objper = new MailNotifyBO();
                // AzureDocURLBO Azuredoc = new AzureDocURLBO();
                var objres = _onBoardRepository.SaveHROnboardGenral(genral);
                if (genral.ReqStatus == "Completed")
                {
                    if (objres.ReqID > 0)
                    {
                        var emp = _onBoardRepository.GetEmployee(genral.EmpID,genral.TenantID,genral.LocationID);
                        if (genral.EmpID > 0)
                        {
                            InputAppUserBO app = new InputAppUserBO();
                            if (emp[0].AppUserID > 0)
                            app.AppUserID = emp[0].AppUserID;
                            app.AppUserID = 0;
                            app.UserName = emp[0].OfficeEmail;
                            app.Password = "pass";
                            app.FirstName = emp[0].FirstName;
                            app.MiddleName = emp[0].MiddleName;
                            app.LastName = emp[0].LastName;
                            app.CreatedBy = genral.CreatedBy;
                            app.TenantID = genral.TenantID;
                            app.ProductID = genral.ProductID;
                            app.RoleID = emp[0].FuncRoleID;
                            app.UserAccessType = "";
                            app.DefaultUserGroupID = 0;
                            app.PrimaryEmail = emp[0].PersonalMail;
                            app.CreatedBy = emp[0].CreatedBy;
                            app.AppUserStatus = "Active";
                            appuserdet = _authRepository.AddProfile(app).Data;
                            if (appuserdet != null)
                            {
                                //var appuserdet = appdet.appUser;
                                //if (appuserdet != null)
                                //{
                                    app.AppUserID = Convert.ToInt32(appuserdet.AppUser.AppUserID);
                                    app.EmpID = genral.EmpID;
                                    app.UserName = appuserdet.AppUser.AppUserName;
                                    app.Password = appuserdet.AppUser.AppUserPassword;
                                    app.AppUserStatus = emp[0].EmpStatus;
                                    app.PrimaryEmail = appuserdet.AppUser.AppUserName;
                                    app.UserAccessType = appuserdet.AppUser.UserType;
                                    app.AppUserID = _onBoardRepository.SaveEmployeeAppuser(app);
                                    genral.EntityType  = "Onboard";
                                    if(objres.Msg == "Saved Successfully")
                                    {
                                        genral.EntityName = Convert.ToString(EntityType.OnboardApply);
                                    }
                                    var objper = _mailRepository.GetMailReportName(genral.EmpID);
                                    mailID = SendLeave(genral, objper);
                                //}
                    //             var temp = _docRepository.GetTemplatesByTenant(genral.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value), "Email Template", "Content Template");
                    //             var objtemp = temp.Data;
                    //             if (objtemp.Count > 0)
                    //             {
                    //                 foreach (var item in objtemp)
                    //                 {
                    //                     if (item.TemplateName == "Onboard Apply Template")
                    //                     {
                    //                         var det = _docRepository.GetDocRepository(genral.TenantID, genral.TenantID, "Tenant", Convert.ToInt32(_configuration.GetSection("ProductID").Value), "Email Template");
                    //                         var objdocpath = det.Data;
                    //                         if (objdocpath.Count > 0)
                    //                         {
                    //                             TempName.RepoType = objdocpath[0].RepoType;
                    //                             TempName.RepoPath = objdocpath[0].RepoPath;
                    //                             TempName.TemplateID = item.TemplateId;
                    //                             TempName.TemplateName = item.TemplateName;
                    //                             TempName.FileName = item.FileName;
                    //                             TempName.StorageFolder = item.StorageFolder;
                    //                         }
                    //                     }
                    //                 }
                    //             }
                    //             objper = _mailRepository.GetMailReportName(genral.EmpID);
                    //             if (objper != null)
                    //             {
                    //                 if (TempName.RepoType == "Storage")
                    //                 {
                    //                     var tag = _docRepository.GetMetatag(genral.ProductID,TempName.TemplateID, genral.EmpID, "Onboard");
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
                    //                     var tag = _docRepository.GetMetatag(genral.ProductID,TempName.TemplateID, genral.EmpID, "Onboard");
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
                    //                         NotifyFrom = objper.ReportingEmail,
                    //                         NotifyTo = objper.EmployeeEmail,
                    //                         NotifyCC = objper.HREmail,
                    //                         NotifySubject = "Welcome on-board "+emp.FullName,
                    //                         NotifyBody = fileContent,
                    //                         NotifyID = genral.EmpID,
                    //                         EntityId = genral.EmpID,
                    //                         TenantID = genral.TenantID,
                    //                     };
                    //                     mailID = _mailRepository.SendMail(oNBO).Result;
                    //                 }
                    //             }
                    //             objres.Msg = objres.Msg + ",Mail ID:" + Convert.ToString(mailID);
                            }
                         }
                     }
                }
                // if (genral.ReqStatus == "Completed")
                // {
                // _hrmsInstanceRepository.GetonbordToHRMS(genral, appuserdet);
                // }
                response = new Response<Tuple<OnBoardingGenralBO, AppUserDetailsBO, OnBoardRequestModelMsg>>(Tuple.Create(genral,appuserdet, objres), 200, "Saved Successfully");                 
            }
            catch (Exception ex)
            {
                response = new Response<Tuple<OnBoardingGenralBO, AppUserDetailsBO, OnBoardRequestModelMsg>>(ex.Message, 500);
            }
            return response;
        }
        public int SendLeave(OnBoardingGenralBO genral, List<MailNotifyBO> standarddata)
        {
            int mailID = 0;
            string emlContent = string.Empty;
            List<TemplateBO> TagS = new List<TemplateBO>();
            KoTemplate tpl = new KoSoft.DocTemplate.KoTemplate(_configuration.GetConnectionString("KOPRODADBConnection"), TempDBType.MySQL);
            DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("KOPRODADBConnection"), DBType.MySQL);
            List<KoSoft.DocTemplate.TemplateBO> oTpl = tpl.GetTemplateData(Convert.ToInt32(genral.TenantID), TemplateType.EmailTemplate, Convert.ToInt32(_configuration.GetSection("ProductID").Value), true);
            List<TagDataQueryBO> oQry = new List<TagDataQueryBO>();
                TagS  = oTpl.Where(s => s.Entity == genral.EntityName).ToList();
                var tagsdet = tpl.GetTemplateTags(TagS[0].TemplateID);            
            oQry = tpl.GetTagQuery(tagsdet);
            Dictionary<string, string> oParamValue = new Dictionary<string, string>();
            oParamValue.Add("requestId", Convert.ToString(genral.EmpID));
            oParamValue.Add("leaveType", Convert.ToString(genral.EntityType));
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
                    var emp = _onBoardRepository.GetEmployee(genral.EmpID,genral.TenantID,genral.LocationID);
                    foreach (var item in standarddata)
                    {
                        if(item.ReportingEmail != null)
                        {
                                MailModel oNBO = new MailModel
                                {
                                    SubscriberID = Convert.ToInt32(_configuration.GetSection("ProductID").Value),
                                    NotifyFrom = item.EmployeeEmail,
                                    NotifyTo = item.ReportingEmail,
                                    NotifyCC = item.HREmail,
                                    NotifySubject = "Welcome on-board "+emp[0].FullName,
                                    NotifyBody = emlContent,
                                    NotifyID = genral.EmpID,
                                    EntityId = genral.EmpID,
                                    TenantID = genral.TenantID,
                                };
                               mailID = _mailRepository.SendMail(oNBO).Result;
                        }
                    }
                }
            }
            return mailID;
        }
        public Response<OnBoardingRequestBO> GetOnboard(int EmpOnboardingID)
        {
            Response<OnBoardingRequestBO> response;
            try
            {
                OnBoardingRequestBO rtndata = new OnBoardingRequestBO();
                if (EmpOnboardingID > 0)
                {
                    rtndata = _onBoardRepository.GetOnboardIndividual(EmpOnboardingID, 0);
                    if (rtndata.EmpID > 0)
                    {
                        var emp = _onBoardRepository.GetEmployee(rtndata.EmpID,rtndata.TenantID,rtndata.LocationID);
                        var onboard = _onBoardRepository.GetAddress(rtndata.EmpID);
                        rtndata.employee = emp[0];
                        rtndata.employee.Address = onboard;
                    }
                }
                response = new Response<OnBoardingRequestBO>(rtndata, 200, "Data Reterived");
            }
            catch (Exception ex)
            {
                response = new Response<OnBoardingRequestBO>("Error : " + ex.Message, 500);
            }
            return response;
        }
        public Response<OnBoardingRequest> GetOnboardTrack(int EmpOnboardingID, int LocationID)
        {
            Response<OnBoardingRequest> response;
            try
            {
                OnBoardingRequest obj = new OnBoardingRequest();
                ChecklistBO chkList = new ChecklistBO();
                obj = _onBoardRepository.GetOnboardTrack(EmpOnboardingID, LocationID);
                if (obj != null && obj.EmpOnboardingID > 0)
                {
                    var track = _onBoardRepository.GetTracking(obj.TenantID, obj.LocationID, obj.EmpOnboardingID);
                    obj.OnboardTrack = track;
                    if (obj.EmpID > 0)
                    {
                        if(obj.BenefitGroupID != null && obj.BenefitGroupID > 0){
                            var mm = _payrollMasterRepository.getpayrollEmployeeBenefits(obj.EmpID, obj.BenefitGroupID.Value);
                            if(mm.Count > 0)
                                obj.StructureID = mm[0].PayrollStructID;
                        }
                        chkList.TenantID = obj.TenantID;
                        chkList.LocationID = obj.LocationID;
                        chkList.EmpID = obj.EmpID;
                        chkList.ProcessCategory = "Onboarding";
                        chkList.EntityName = "Employee";
                        chkList.CreatedBy = obj.ReqEmpID;
                        var trans = _onBoardRepository.SaveBPTransInstance(obj.TenantID,obj.LocationID,obj.EmpOnboardingID);
                        var det = _onBoardRepository.SaveHRCraeteCheckList(chkList);
                    }
                }
                response = new Response<OnBoardingRequest>(obj, 200, "Data Reterived");
            }
            catch (Exception ex)
            {
                response = new Response<OnBoardingRequest>("Error : " + ex.Message, 500);
            }
            return response;
        }
        public Response<int> SaveOnBoardEmployee(OnboardPersonalDetail onboarding)
        {
            Response<int> response;
            try
            {
                int empid = 0;
                //onboarding.Address = new List<EmployeeAddress>();
                if (onboarding.EmpOnboardingID > 0)
                {
                    empid = _onBoardRepository.InsertOnBoardPersonal(onboarding);
                    if (empid > 0)
                    {
                        if (onboarding.Address.Count > 0)
                        {
                            onboarding.Address[0].ReferenceID = empid;
                            var mm = _onBoardRepository.SaveEmployeeAddress(onboarding.Address);
                        }
                    }
                }
                if (empid == 0)
                {
                    response = new Response<int>(empid, 500, "Onboard Data Creation or Updation is Failed");
                }
                else
                {
                    response = new Response<int>(empid, 200, "Saved Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> SaveAzureDocuments(DocProofFileBo _objdoc, DocProofInputBo _obj)
        {
            Response<int> response;
            try
            {
                int DocID = 0;
                string NewFileName;
                string filExt;
                DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                DocumentDA objdocservice = new DocumentDA(_configuration.GetConnectionString("KOPRODADBConnection"), DBType.MySQL);
                filExt = Path.GetExtension(_objdoc.File.FileName.ToString());
                if (_obj.DocID > 0)
                {                    
                    var result = objdoc.GetDocument(_obj.DocID, 0, "", "", 0);
                    var name = Path.GetFileNameWithoutExtension(result[0].GenDocName);
                    NewFileName = name + filExt;
                    DocID = result[0].DocID;
                }
                else
                {
                    NewFileName = "IDproof_" + (Guid.NewGuid()).ToString("N") + filExt;
                }
                SaveDocCloudBO docCloudins = new SaveDocCloudBO();
                var Repo = objdocservice.GetDocRepository(_obj.TenantID,_obj.TenantID,"Tenant",Convert.ToInt32(_configuration.GetSection("ProductID").Value), LocationType.IDProof);
                var docRepo = Repo;
                if (docRepo[0].RepoType == "Storage")
                {

                    var ms = new MemoryStream();
                    _objdoc.File.OpenReadStream().CopyTo(ms);
                    byte[] contents = ms.ToArray();
                    var fileFormat = Convert.ToBase64String(contents);
                    switch (_objdoc.File.ContentType)
                    {
                        case "image/png":
                            docCloudins.ContentType = "image/png";
                            break;
                        case "image/jpeg":
                            docCloudins.ContentType = "image/jpeg";
                            break;
                        case "application/vnd.ms-excel":
                            docCloudins.ContentType = "application/vnd.ms-excel";
                            break;
                        case "application/msword":
                            docCloudins.ContentType = "application/msword";
                            break;
                        default:
                            docCloudins.ContentType = System.Net.Mime.MediaTypeNames.Application.Pdf;
                            break;
                    }
                    var objcont1 = objdoc.GetDocContainer(_obj.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                    docCloudins.Container = objcont1[0].ContainerName.ToLower();

                    docCloudins.CloudType = _configuration.GetSection("CloudType").Value;
                    docCloudins.folderPath = docRepo[0].RepoPath.ToLower() + "/" + Convert.ToString(_obj.EmpID);
                    docCloudins.file = fileFormat;
                    docCloudins.fileName = NewFileName;
                    docCloudins.ProductCode = _configuration.GetSection("ProductID").Value;

                    _ = _storage.SaveBulkDocumentCloud(docCloudins);

                    GenDocument d1 = new GenDocument();
                    d1.DocID = DocID;
                    d1.RepositoryID = docRepo[0].RepositoryID;
                    d1.DocumentName = _objdoc.File.FileName;
                    d1.DocType = docRepo[0].RepoName;
                    d1.CreatedBy = _obj.CreatedBy;
                    d1.OrgDocName = NewFileName;
                    d1.GenDocName = NewFileName;
                    d1.DocKey = (Guid.NewGuid()).ToString("N");
                    d1.DocSize = Convert.ToDecimal(_objdoc.File.Length);
                    d1.Entity = docRepo[0].LocType;
                    d1.EntityID = _obj.EmpID;
                    d1.TenantID = _obj.TenantID;
                    d1.DocTypeID = _obj.DocTypeID;
                    d1.DirectionPath = docCloudins.folderPath;
                    var doc = objdoc.SaveDocument(d1);
                    DocID = doc;

                }
                if (DocID == 0)
                {
                    response = new Response<int>(DocID, 400, "please upload Incomplete file");
                }
                else
                {
                    response = new Response<int>(DocID, 200, "Saved Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> SaveIDcardDocuments(InputDocIDCardInputBo _objdoc)
        {
            Response<int> response;
            try
            {
                int DocID = 0;
                string NewFileName;
                string NewFileNames;
                string filExt;
                DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                DocumentDA objdocservice = new DocumentDA(_configuration.GetConnectionString("KOPRODADBConnection"), DBType.MySQL);
                var idcard = _onBoardRepository.SaveHRIDCard(_objdoc.IDcard);
                if (idcard > 0)
                {
                    _objdoc.IDcard.EmpIDCardID = idcard;
                    filExt = Path.GetExtension(_objdoc.inputDocs.docName.ToString());

                    NewFileName = "IDCard_" + (Guid.NewGuid()).ToString("N") + filExt;
                    if (_objdoc.inputDocs.DocID > 0)
                    {
                        var result = objdoc.GetDocument(_objdoc.inputDocs.DocID, 0, "", "", 0);
                        if(result.Count > 0)
                        {
                        var name = Path.GetFileNameWithoutExtension(result[0].GenDocName);
                        NewFileName = name + filExt;
                        DocID = result[0].DocID;
                        }
                    }                    

                    SaveDocCloudBO docCloudins = new SaveDocCloudBO();
                    var Repo = objdocservice.GetDocRepository(_objdoc.IDcard.TenantID,_objdoc.IDcard.TenantID,"Tenant",Convert.ToInt32(_configuration.GetSection("ProductID").Value), LocationType.IDCardImages);
                    var docRepo = Repo;
                    if (docRepo[0].RepoType == "Storage")
                    {
                        var Base64PDF = _objdoc.inputDocs.docsFile;
                        if (filExt == ".png")
                        {
                            docCloudins.ContentType = "image/png";
                        }
                        if (filExt == ".jpeg")
                        {
                            docCloudins.ContentType = "image/jpeg";
                        }
                        if (filExt == ".jpg")
                        {
                            docCloudins.ContentType = "image/jpeg";
                        }
                        if (filExt == ".jpeg" || filExt == ".png" || filExt == ".jpg")
                        {
                            var objcont = objdoc.GetDocContainer(_objdoc.IDcard.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                            docCloudins.Container = objcont[0].ContainerName.ToLower();
                            docCloudins.CloudType = _configuration.GetSection("CloudType").Value;
                            docCloudins.folderPath = docRepo[0].RepoPath.ToLower() + "/" + Convert.ToString(_objdoc.IDcard.EmpID);
                            docCloudins.file = Base64PDF;
                            docCloudins.fileName = NewFileName;
                            docCloudins.ProductCode = _configuration.GetSection("ProductID").Value;

                            _ = _storage.SaveBulkDocumentCloud(docCloudins);

                            GenDocument d1 = new GenDocument();
                            d1.DocID = DocID;
                            d1.RepositoryID = docRepo[0].RepositoryID;
                            d1.DocumentName = _objdoc.inputDocs.docName;
                            d1.DocType = docRepo[0].RepoName;
                            d1.CreatedBy = _objdoc.IDcard.ActivatedBy;
                            d1.OrgDocName = NewFileName;
                            d1.GenDocName = NewFileName;
                            d1.DocKey = (Guid.NewGuid()).ToString("N");
                            d1.DocSize = Convert.ToDecimal(_objdoc.inputDocs.docsSize);
                            d1.Entity = docRepo[0].LocType;
                            d1.EntityID = _objdoc.IDcard.EmpID;
                            d1.TenantID = _objdoc.IDcard.TenantID;
                            d1.DirectionPath = docCloudins.folderPath;
                            var doc = objdoc.SaveDocument(d1);
                            DocID = doc;

                            /* Profile Image Saved */
                            NewFileNames = "EmpProfile_" + (Guid.NewGuid()).ToString("N") + filExt;
                            var Repos = objdocservice.GetDocRepository(_objdoc.IDcard.TenantID,_objdoc.IDcard.TenantID,"Tenant",Convert.ToInt32(_configuration.GetSection("ProductID").Value), LocationType.ProfileImage);
                            var docRepos = Repos;
                            if (docRepos[0].RepoType == "Storage")
                            {
                                var Base64Image = _objdoc.inputDocs.docsFile;
                                if (filExt == ".png")
                                {
                                    docCloudins.ContentType = "image/png";
                                }
                                if (filExt == ".jpeg")
                                {
                                    docCloudins.ContentType = "image/jpeg";
                                }
                                if (filExt == ".jpg")
                                {
                                    docCloudins.ContentType = "image/jpeg";
                                }
                                if (filExt == ".jpeg" || filExt == ".png" || filExt == ".jpg")
                                {
                                    var objconta = objdoc.GetDocContainer(_objdoc.IDcard.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                                    docCloudins.Container = objconta[0].ContainerName.ToLower();
                                    docCloudins.CloudType = _configuration.GetSection("CloudType").Value;
                                    docCloudins.folderPath = docRepos[0].RepoPath.ToLower() + "/" + Convert.ToString(_objdoc.IDcard.EmpID);
                                    docCloudins.file = Base64Image;
                                    docCloudins.fileName = NewFileNames;
                                    docCloudins.ProductCode = _configuration.GetSection("ProductID").Value;

                                    _ = _storage.SaveBulkDocumentCloud(docCloudins);

                                    GenDocument d2 = new GenDocument();
                                    d2.DocID = DocID;
                                    d2.RepositoryID = docRepos[0].RepositoryID;
                                    d2.DocumentName = _objdoc.inputDocs.docName;
                                    d2.DocType = docRepos[0].RepoName;
                                    d2.CreatedBy = _objdoc.IDcard.ActivatedBy;
                                    d2.OrgDocName = NewFileNames;
                                    d2.GenDocName = NewFileNames;
                                    d2.DocKey = (Guid.NewGuid()).ToString("N");
                                    d2.DocSize = Convert.ToDecimal(_objdoc.inputDocs.docsSize);
                                    d2.Entity = docRepos[0].LocType;
                                    d2.EntityID = _objdoc.IDcard.EmpID;
                                    d2.TenantID = _objdoc.IDcard.TenantID;
                                    d2.DirectionPath = docCloudins.folderPath;
                                    var docs = objdoc.SaveDocument(d2);
                                    DocID = docs;
                                }
                            }
                            // var emp = _onBoardRepository.GetEmployee(_objdoc.IDcard.EmpID,_objdoc.IDcard.TenantID,_objdoc.IDcard.LocationID);
                            // if (!string.IsNullOrEmpty(emp[0].EmpNumber))
                            // {
                            //     var docRep = objdocservice.GetDocRepository(_objdoc.IDcard.TenantID,_objdoc.IDcard.TenantID,"Tenant",Convert.ToInt32(_configuration.GetSection("ProductID").Value),LocationType.ProfileImage);
                            //     var docRepo1 = docRep;
                            //     if (docRepo1[0].RepoType == "Storage")
                            //     {
                            //         var result = objdoc.GetDocument(0, _objdoc.IDcard.EmpID, "", "ProfileImage", _objdoc.IDcard.TenantID);
                            //         if (result.Count > 0)
                            //         {
                            //             DocID = result[0].DocID;
                            //         }
                            //         else
                            //         {
                            //             DocID = 0;
                            //         }
                            //         docCloudins.Container = objcont[0].ContainerName.ToLower();
                            //         docCloudins.CloudType = _configuration.GetSection("CloudType").Value;
                            //         docCloudins.folderPath = docRepo1[0].RepoPath.ToLower() + "/" + Convert.ToString(emp[0].EmpNumber);
                            //         docCloudins.file = Base64PDF;
                            //         docCloudins.fileName = NewFileName;
                            //         docCloudins.ProductCode = _configuration.GetSection("ProductID").Value;
                            //         _ = _storage.SaveBulkDocumentCloud(docCloudins);

                            //         d1.DocID = DocID;
                            //         d1.RepositoryID = docRepo1[0].RepositoryID;
                            //         d1.DocumentName = _objdoc.inputDocs.docName;
                            //         d1.DocType = docRepo1[0].LocType;
                            //         d1.CreatedBy = _objdoc.IDcard.ActivatedBy;
                            //         d1.OrgDocName = NewFileName;
                            //         d1.GenDocName = NewFileName;
                            //         d1.DocKey = (Guid.NewGuid()).ToString("N");
                            //         d1.DocSize = Convert.ToDecimal(_objdoc.inputDocs.docsSize);
                            //         d1.Entity = docRepo1[0].RepoName;
                            //         d1.EntityID = _objdoc.IDcard.EmpID;
                            //         d1.TenantID = _objdoc.IDcard.TenantID;
                            //         d1.DirectionPath = docCloudins.folderPath;
                            //         var doc1 = objdoc.SaveDocument(d1);
                            //         DocID = doc;
                            //     }
                            // }
                        }
                    }
                }
                if (DocID == 0)
                {
                    response = new Response<int>(DocID, 400, "please upload Incomplete file");
                }
                else
                {
                    response = new Response<int>(DocID, 200, "Saved Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<GenDocument>> GetDocInfo(int TenantID, int LocationID, int EmpID)
        {
            Response<List<GenDocument>> response;
            try
            {
                DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                var newList = objdoc.GetDocument(0, EmpID, "", "IDProof", TenantID);
                var docs = newList.OrderBy(x => x.DocID).ToList();
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
        public Response<List<DocIDCardInputBo>> GetIDCardInfo(int EmpID, int LocationID, int tenantID, int EmpIDCardID)
        {
            Response<List<DocIDCardInputBo>> response;
            try
            {
                DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                var result = _onBoardRepository.GetIDCardInfo(EmpID, LocationID, EmpIDCardID);
                if (result.Count() == 0)
                {
                    response = new Response<List<DocIDCardInputBo>>(result, 200, "Data Not Found");
                }
                else
                {
                    foreach (var s in result)
                    {
                        var docs = objdoc.GetDocument(0, s.EmpID, "", "IDCardImages", tenantID);
                        s.docsList = docs;
                    }
                    response = new Response<List<DocIDCardInputBo>>(result, 200, "Data Reterived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<DocIDCardInputBo>>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> SaveHRCraeteCheckList(ChecklistBO chkList)
        {
            Response<int> response;
            try
            {
                int objres = 0;
                chkList.ProcessCategory = "Onboarding";
                chkList.EntityName = "Employee";
                objres = _onBoardRepository.SaveHRCraeteCheckList(chkList);
                response = new Response<int>(objres, 200, "Saved Successfully");

            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<BPchecklist>> GetHRCheckList(BPcheckInputBO chkList)
        {
            Response<List<BPchecklist>> response;
            try
            {
                var objres = _onBoardRepository.GetHRCheckList(chkList);
                response = new Response<List<BPchecklist>>(objres, 200, "Data Reterived");

            }
            catch (Exception ex)
            {
                response = new Response<List<BPchecklist>>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> SaveWorkplace(WorkPlaceBO _work)
        {
            Response<int> response;
            try
            {
                var objres = _onBoardRepository.SaveWorkplace(_work);
                if (_work.checkList.Count > 0)
                {
                    foreach (var _rt in _work.checkList)
                    {
                        var mm = _onBoardRepository.SaveHRCheckList(_rt);
                    }
                }
                response = new Response<int>(objres, 200, "Saved Successfully");

            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> SaveNetworkSetUp(NetworkSetupBO _network)
        {
            Response<int> response;
            try
            {
                var objres = _onBoardRepository.SaveNetworkSetUp(_network);
                if (_network.checkList.Count > 0)
                {
                    foreach (var _rt in _network.checkList)
                    {
                        var mm = _onBoardRepository.SaveHRCheckList(_rt);
                    }
                }
                response = new Response<int>(objres, 200, "Saved Successfully");

            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<NetworkSetupBO> GetNetworkDetails(NetworkSetupBO _network)
        {
            Response<NetworkSetupBO> response;
            try
            {
                NetworkSetupBO objrtn = new NetworkSetupBO();
                var objres = _onBoardRepository.GetEmployee(_network.EmpID, _network.TenantID, _network.LocationID);
                if (objres != null)
                {
                    objrtn.EmpID = objres[0].EmpID;
                    objrtn.LocationID = _network.LocationID;
                    objrtn.NetworkDomain = objres[0].NetworkDomain;
                    objrtn.NetworkUserName = objres[0].NetworkUserName;
                    //objrtn.CreatedBy = objres.CreatedBy.Value;                    
                    /* Check list Save Call */
                    BPcheckInputBO obj = new BPcheckInputBO();
                    //_network.checkList
                    obj.LocationID = _network.LocationID;
                    obj.RefEntityID = _network.EmpID;
                    obj.ChkListGroup = _network.ProcessName;
                    obj.RefEntity = "Employee";
                    var chklist = _onBoardRepository.GetHRCheckList(obj);
                    objrtn.checkList = chklist;
                }
                response = new Response<NetworkSetupBO>(objrtn, 200, "Data Reterived");

            }
            catch (Exception ex)
            {
                response = new Response<NetworkSetupBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<WorkPlaceBO> GetWorkPlace(WorkPlaceBO _work)
        {
            Response<WorkPlaceBO> response;
            try
            {
                WorkPlaceBO objrtn = new WorkPlaceBO();
                var objres = _onBoardRepository.GetEmployee(_work.EmpID,_work.TenantID,_work.LocationID);
                if (objres != null)
                {
                    objrtn.EmpID = objres[0].EmpID;
                    objrtn.LocationID = _work.LocationID;
                    objrtn.CubicleNo = objres[0].CubicleNo;
                    //objrtn.CreatedBy = objres.CreatedBy.Value;                    
                    /* Check list Save Call */
                    BPcheckInputBO obj = new BPcheckInputBO();
                    //_network.checkList
                    obj.LocationID = _work.LocationID;
                    obj.RefEntityID = _work.EmpID;
                    obj.ChkListGroup = _work.ProcessName;
                    obj.RefEntity = "Employee";
                    var chklist = _onBoardRepository.GetHRCheckList(obj);
                    objrtn.checkList = chklist;
                }
                response = new Response<WorkPlaceBO>(objrtn, 200, "Data Reterived");

            }
            catch (Exception ex)
            {
                response = new Response<WorkPlaceBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<OrientationBO> GetOrientation(OrientationBO _work)
        {
            Response<OrientationBO> response;
            try
            {
                OrientationBO objrtn = new OrientationBO();
                var objres = _onBoardRepository.GetEmployee(_work.EmpID,_work.TenantID,_work.LocationID);
                if (objres != null)
                {
                    objrtn.EmpID = objres[0].EmpID;
                    objrtn.LocationID = _work.LocationID;
                    /* Check list Save Call */
                    BPcheckInputBO obj = new BPcheckInputBO();
                    //_network.checkList
                    obj.LocationID = _work.LocationID;
                    obj.RefEntityID = _work.EmpID;
                    obj.ChkListGroup = _work.ProcessName;
                    obj.RefEntity = "Employee";
                    var chklist = _onBoardRepository.GetHRCheckList(obj);
                    objrtn.checkList = chklist;
                }
                response = new Response<OrientationBO>(objrtn, 200, "Data Reterived");

            }
            catch (Exception ex)
            {
                response = new Response<OrientationBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> SaveHRCheckList(OrientationBO chkList)
        {
            Response<int> response;
            try
            {
                int objres = 0;
                if (chkList.checkList.Count > 0)
                {
                    foreach (var _rt in chkList.checkList)
                    {
                        objres = _onBoardRepository.SaveHRCheckList(_rt);
                    }
                }
                // if(_network.checkList.Count > 0)
                // {
                //     /* Check list Save Call */
                // }
                response = new Response<int>(objres, 200, "Saved Successfully");

            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> SaveHRAttendnceConfig(AttendanceConfigBO attend)
        {
            Response<int> response;
            try
            {
                var objres = _onBoardRepository.SaveHRAttendnceConfig(attend);
                if(objres.ReqID == 0)
                {
                    response = new Response<int>(objres.ReqID, 200, "Data Creation or Updation is Failed");
                }
                else
                {
                    response = new Response<int>(objres.ReqID, 200,objres.Msg);
                }
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<AttendanceConfigBO>> GetHRAttendnceConfig(int EmpID, int LocationID, int AttendConfigID)
        {
            Response<List<AttendanceConfigBO>> response;
            try
            {
                //int objres = 0;              
                var objres = _onBoardRepository.GetHRAttendnceConfig(EmpID, LocationID, AttendConfigID);
                response = new Response<List<AttendanceConfigBO>>(objres, 200, "Data Reterived");

            }
            catch (Exception ex)
            {
                response = new Response<List<AttendanceConfigBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<OnBoardRequestModelMsg> SaveOnboardExperience(HRInputEmpExperience onInpExp)
        {
            Response<OnBoardRequestModelMsg> response;
            try
            {
                OnBoardRequestModelMsg result = new OnBoardRequestModelMsg();
                if (onInpExp.onExp.Count > 0)
                {
                    foreach (var item in onInpExp.onExp)
                    {
                        result = _onBoardRepository.SaveOnboardExperience(item);
                    }
                }
                response = new Response<OnBoardRequestModelMsg>(result,200,result.Msg);
            }
            catch (Exception ex)
            {
                response = new Response<OnBoardRequestModelMsg>(ex.Message,500);
            }
            return response;
        }
        public Response<List<HREmpEducation>> GetOnboardEducation(int EmpID)
        {
            Response<List<HREmpEducation>> response;
            try
            {
                var result = _onBoardRepository.GetOnboardEducation(EmpID);
                if (result.Count() == 0)
                {
                    response = new Response<List<HREmpEducation>>(result, 200, "Data Not Found");
                }
                else
                {
                    response = new Response<List<HREmpEducation>>(result, 200, "Data Reterived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<HREmpEducation>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<HREmpExperience>> GetOnboardEmpExperience(int EmpID)
        {
            Response<List<HREmpExperience>> response;
            try
            {
                var result = _onBoardRepository.GetOnboardEmpExperience(EmpID);
                if (result.Count() == 0)
                {
                    response = new Response<List<HREmpExperience>>(result, 200, "Data Not Found");
                }
                else
                {
                    response = new Response<List<HREmpExperience>>(result, 200, "Data Reterived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<HREmpExperience>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<BenefitGroupBO>> GetBenefitGroupByGroupType(int TenantID,int GroupTypeID)
        {
            Response<List<BenefitGroupBO>> response;
            try
            {
                var result = _payrollRepository.GetBenefitGroupByGroupType(TenantID,GroupTypeID);
                if (result.Count() == 0)
                {
                    response = new Response<List<BenefitGroupBO>>(result, 200, "Data Not Found");
                }
                else
                {
                    response = new Response<List<BenefitGroupBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<BenefitGroupBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<EmployeeBenefitsBO>> GetEmployeeBenefits(int EmpID,int TenantID,int LocationID)
        {
            Response<List<EmployeeBenefitsBO>> response;
            try
            {
                var result = _payrollRepository.GetEmployeeBenefits(EmpID,TenantID,LocationID);
                if (result.Count() == 0)
                {
                    response = new Response<List<EmployeeBenefitsBO>>(result, 200, "Data Not Found");
                }
                else
                {
                    response = new Response<List<EmployeeBenefitsBO>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<EmployeeBenefitsBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<OnboardingBenefitsBO> GetBenefits(int BenefitGroupID,int TenantID,int LocationID)
        {
            Response<OnboardingBenefitsBO> response;
            OnboardingBenefitsBO objoutput = new OnboardingBenefitsBO();
            List<MasLeaveGroupBO> objmasleave = new List<MasLeaveGroupBO>();
            List<LeaveAllocReferenceBO> objref = new List<LeaveAllocReferenceBO>();
            PlanTypeCategoryBO lstobjplan = new PlanTypeCategoryBO();
            List<HRBenefitPlansBO> objplan = new List<HRBenefitPlansBO>();
            List<PlanTypeCategoryBO> objplanwise = new List<PlanTypeCategoryBO>();
            try
            {
                var result = _benefitGroupRepository.GetBenefitGroup(BenefitGroupID,TenantID,LocationID);
                var medben = _benefitGroupRepository.GetBenefitMedPlan(BenefitGroupID,TenantID,LocationID);

                foreach (var val in medben)
                {
                    var plan =_medicalInsuranceRepository.GetPlanTypeCategory(val.MedBenePlanID,TenantID,LocationID);
                    foreach (var itemval in plan)
                    {
                        lstobjplan = itemval;
                        objplan = _medicalInsuranceRepository.GetHRBenefitPlansByEmp(itemval.PlanTypeID,TenantID,LocationID);
                        lstobjplan.objmedplan = objplan;
                        objplan = new List<HRBenefitPlansBO>();
                        objplanwise.Add(lstobjplan);
                        lstobjplan = new PlanTypeCategoryBO();
                    }
                }
                if(result.Count == 0)
                {
                    response = new Response<OnboardingBenefitsBO>(objoutput,200, "Data Not Found");
                }
                else
                {
                foreach (var item in result)
                {
                    objmasleave = _leaveBenefitRepository.GetMasLeaveGroup(item.LeaveGroupID,TenantID,LocationID);
                    foreach (var items in objmasleave)
                    {
                    objref = _leaveBenefitRepository.GetLeaveAllocReference(item.LeaveGroupID, item.TenantID, item.LocationID);
                    items.objalloc = new List<LeaveAllocReferenceBO>(objref);
                    objref = new List<LeaveAllocReferenceBO>();
                    }
                }
                    var disability = _disabilityBenefitRepository.GetMasDisabilityBenefitByGroupType(result[0].GroupTypeID,TenantID,LocationID);
                    var medical = _payrollRepository.GetMasMedicalBenefit(TenantID,LocationID);
                    var other = _payrollRepository.GetOtherBenefits(BenefitGroupID,TenantID,LocationID);
                    objoutput.objbenefit = result;
                    objoutput.objleave = objmasleave;
                    objoutput.objdisbenefit = disability;
                    objoutput.objmedical = medical;
                    objoutput.objother = other;
                    objoutput.objwise = objplanwise;
                    
                    response = new Response<OnboardingBenefitsBO>(objoutput,200, "Data Retrieved");
                    }
                    
            }
            catch (Exception ex)
            {
                response = new Response<OnboardingBenefitsBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> SaveOnBoardBenefits(OnBoardBenefitGroupBO objgroup)
        {
            Response<int> response;
            try
            {
                var result = _payrollRepository.SaveOnBoardBenefits(objgroup);
                if (result.Id == 0)
                {
                    response = new Response<int>(result.Id, 200, result.Msg);
                }
                else
                {
                     if(objgroup.BenefitGroupID == 0)
                      objgroup.BenefitGroupID = result.Id;
                    var objstrucdata = _payrollMasterRepository.GetPayrollMasterStructure(objgroup.TenantID, objgroup.LocationID,0);
                    var objstruc = objstrucdata.Where(s => s.GroupTypeID == objgroup.GroupTypeID);
                    foreach (var item in objstruc)
                    {
                        if(item.GradeID > 0 && item.GradeID != null)
                        {
                        objgroup.StructureID = item.StructureID;
                        var result2 = _payrollRepository.SaveOnBoardStruct(objgroup);
                        }
                        else{
                            objgroup.StructureID = item.StructureID;
                            var result2 = _payrollRepository.SaveOnBoardStruct(objgroup);
                            }
                    }
                    response = new Response<int>(result.Id, 200, result.Msg);
                }
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<OnboardingEmpBenefitsBO> GetEmployeePayrollCycle(SalaryonboardPaycycle objPaycycle)
        {
            Response<OnboardingEmpBenefitsBO> response;
            try
            {
                OnboardingEmpBenefitsBO result = new OnboardingEmpBenefitsBO();
                result.objbenefit = _payrollRepository.GetEmployeePayrollBenefit(objPaycycle.EmpID, objPaycycle.BenefitGroupID, objPaycycle.Paycycle, objPaycycle.StructureID);                
                if (result.objbenefit.Count() == 0)
                {
                    response = new Response<OnboardingEmpBenefitsBO>(result, 200, "Data Not Found");
                }
                else
                {
                    result.Structure = _payrollRepository.GetEmployeePayrollStructure(objPaycycle.EmpID, objPaycycle.BenefitGroupID, objPaycycle.Paycycle, objPaycycle.StructureID);
                    response = new Response<OnboardingEmpBenefitsBO>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<OnboardingEmpBenefitsBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> SaveEmployeeCTC(PayrollCTCBO objctc)
        {
            Response<int> response;            
            try
            {
                SaveOut output = new SaveOut();
                var result = _payrollRepository.GetEmployeePayrollStructure(objctc.EmpID, objctc.BenefitGroupID, string.Empty, objctc.StructureID);
                foreach(var struc in result)
                {
                objctc.EmpSalStrucID = struc.EmpSalStrucID;
                objctc.LineItemKey = struc.LineItemKey;
                objctc.StructureID = struc.StructureID;
                var data = _payrollRepository.SaveEmployeeCTC(objctc);
                if (data.Id == 0)
                {
                    output = data;
                }
                else
                {
                   output = data;;
                }
                }
                response = new Response<int>(output.Id, 200, output.Msg);
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> SaveEmployeePayRollBenfits(PayrollCTCBO objctc)
        {
            Response<int> response;            
            try
            {                
                var data = _payrollRepository.SaveEmployeePayRollBenfits(objctc);
                if (data.Id == 0)
                {
                    response = new Response<int>(data.Id, 200, data.Msg);
                }
                else
                {
                    response = new Response<int>(data.Id, 200, data.Msg);
                }
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> SaveEmpPayrollCTC(EmpSalaryStructureCTC objctc)
        {
            Response<SaveOut> response;
            try
            {
                SaveOut output = new SaveOut();
                PayrollIncomeandProfissonalBO objPFtax = new PayrollIncomeandProfissonalBO();
                /*PF & ESI Calculations*/
                /*Payroll stratr Get call tblHRPayrollMasCalculation*/
                var payrollcalc = _payrollRepository.GetPayrollTypecalc(objctc.TenantID, objctc.LocationID);
                if (payrollcalc.Count > 0)
                {
                    foreach (var item in payrollcalc)
                    {
                        item.GrossSalary = objctc.GrossSalary;
                        var result1 = _payrollRepository.GetPayrollPTaxandITax(item);
                        if (result1.Count > 0)
                        {
                            if (result1[0].ProfessionalTaxValue > 0)
                            {
                                objctc.ProfessionalTax = result1[0].ProfessionalTaxValue;
                            }
                            if (result1[0].IncomeTaxValue > 0)
                            {
                                objctc.IncomeTax = result1[0].IncomeTaxValue;
                            }
                        }
                    }
                }
                var objgenral = _payrollRepository.GetEmpPayrolBycycle(objctc.EmpID, objctc.payrollStructID, "General");
                foreach (var struc in objgenral)
                {
                    objctc.Ratevalues = decimal.Zero;
                    string objstructureRate = string.Empty;                    
                    var result2 = _payrollMasterRepository.GetPayrollRules(objctc.TenantID, objctc.LocationID, struc.EmpSalItemID);
                    if (result2.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(result2[0].PayrollRuleForm) && !string.IsNullOrEmpty(result2[0].PayrollRuleComponent))
                        {
                            objctc.PayrollLineCalcFormat = "SELECT " + result2[0].PayrollRuleForm;                        
                        string[] a1_values = result2[0].PayrollRuleComponent.Split(',');
                        foreach (var item in a1_values)
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                if (!string.IsNullOrEmpty(item))
                                {
                                    objctc.PayrollCode = item;
                                }
                                objstructureRate = _payrollMasterRepository.GetRuleSalaryValues(objctc);
                            }
                            if (!string.IsNullOrEmpty(objstructureRate) && !string.IsNullOrEmpty(objctc.PayrollLineCalcFormat))
                            {
                                objctc.PayrollLineCalcFormat = objctc.PayrollLineCalcFormat.Replace(objctc.PayrollCode, objstructureRate);
                            }
                        }
                        if (!string.IsNullOrEmpty(objstructureRate) && !string.IsNullOrEmpty(objctc.PayrollLineCalcFormat))
                        {
                            var objquery = _commonRepository.GetExecutiveScript(objctc.PayrollLineCalcFormat);
                            if (!string.IsNullOrEmpty(objstructureRate))
                                objctc.Ratevalues = Convert.ToDecimal(objquery);
                        }
                        }
                    }
                    //*Save Grocess salary to save Data Calculations*/
                    objctc.EmpSalStrucID = struc.EmpSalStrucID;
                    objctc.LineItemKey = struc.LineItemKey;
                    objctc.payrollStructID = struc.StructureID;
                    //objctc.PayrollLineCalcFormat = struc.PayrollLineCalcFormat;
                    objctc.SalaryRangeMin =  struc.SalaryRangesMin;
                    objctc.SalaryRangeMax =  struc.SalaryRangesMax;
                    output = _payrollRepository.SavePayrollSetCTC(objctc);
                }
                var objEarning = _payrollRepository.GetEmpPayrolBycycle(objctc.EmpID, objctc.payrollStructID, "Earnings");
                foreach (var struc in objEarning)
                {
                    objctc.Ratevalues = decimal.Zero;
                    string objstructureRate = string.Empty;
                    var result2 = _payrollMasterRepository.GetPayrollRules(objctc.TenantID, objctc.LocationID, struc.EmpSalItemID);
                    if (result2.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(result2[0].PayrollRuleForm) && !string.IsNullOrEmpty(result2[0].PayrollRuleComponent))
                        {
                            objctc.PayrollLineCalcFormat = "SELECT " + result2[0].PayrollRuleForm;
                        
                        string[] a1_values = result2[0].PayrollRuleComponent.Split(',');
                        foreach (var item in a1_values)
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                if (!string.IsNullOrEmpty(item))
                                {
                                    objctc.PayrollCode = item;
                                }
                                objstructureRate = _payrollMasterRepository.GetRuleSalaryValues(objctc);
                            }
                            if (!string.IsNullOrEmpty(objstructureRate) && !string.IsNullOrEmpty(objctc.PayrollLineCalcFormat))
                            {
                                objctc.PayrollLineCalcFormat = objctc.PayrollLineCalcFormat.Replace(objctc.PayrollCode, objstructureRate);
                            }
                        }
                        if (!string.IsNullOrEmpty(objstructureRate) && !string.IsNullOrEmpty(objctc.PayrollLineCalcFormat))
                        {
                            var objquery = _commonRepository.GetExecutiveScript(objctc.PayrollLineCalcFormat);
                            if (!string.IsNullOrEmpty(objstructureRate))
                                objctc.Ratevalues = Convert.ToDecimal(objquery);
                        }
                        }
                    }
                    //*Save Grocess salary to save Data Calculations*/
                    objctc.EmpSalStrucID = struc.EmpSalStrucID;
                    objctc.LineItemKey = struc.LineItemKey;
                    objctc.payrollStructID = struc.StructureID;
                    //objctc.PayrollLineCalcFormat = struc.PayrollLineCalcFormat;
                    objctc.SalaryRangeMin =  struc.SalaryRangesMin;
                    objctc.SalaryRangeMax =  struc.SalaryRangesMax;
                    output = _payrollRepository.SavePayrollSetCTC(objctc);
                }
                var objDectuion = _payrollRepository.GetEmpPayrolBycycle(objctc.EmpID, objctc.payrollStructID, "Deductions");
                foreach (var struc in objDectuion)
                {
                    objctc.Ratevalues = decimal.Zero;
                    string objstructureRate = string.Empty;
                    var result2 = _payrollMasterRepository.GetPayrollRules(objctc.TenantID, objctc.LocationID, struc.EmpSalItemID);
                    if (result2.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(result2[0].PayrollRuleForm) && !string.IsNullOrEmpty(result2[0].PayrollRuleComponent))
                        {
                            objctc.PayrollLineCalcFormat = "SELECT " + result2[0].PayrollRuleForm;
                        
                        string[] a1_values = result2[0].PayrollRuleComponent.Split(',');
                        foreach (var item in a1_values)
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                if (!string.IsNullOrEmpty(item))
                                {
                                    objctc.PayrollCode = item;
                                }
                                objstructureRate = _payrollMasterRepository.GetRuleSalaryValues(objctc);
                            }
                            if (!string.IsNullOrEmpty(objstructureRate) && !string.IsNullOrEmpty(objctc.PayrollLineCalcFormat))
                            {
                                objctc.PayrollLineCalcFormat = objctc.PayrollLineCalcFormat.Replace(objctc.PayrollCode, objstructureRate);
                            }
                        }
                        if (!string.IsNullOrEmpty(objstructureRate) && !string.IsNullOrEmpty(objctc.PayrollLineCalcFormat))
                        {
                            var objquery = _commonRepository.GetExecutiveScript(objctc.PayrollLineCalcFormat);
                            if (!string.IsNullOrEmpty(objstructureRate))
                                objctc.Ratevalues = Convert.ToDecimal(objquery);
                        }
                        }
                    }
                    //*Save Grocess salary to save Data Calculations*/
                    objctc.EmpSalStrucID = struc.EmpSalStrucID;
                    objctc.LineItemKey = struc.LineItemKey;
                    objctc.payrollStructID = struc.StructureID;
                    //objctc.PayrollLineCalcFormat = struc.PayrollLineCalcFormat;
                    objctc.SalaryRangeMin =  struc.SalaryRangesMin;
                    objctc.SalaryRangeMax =  struc.SalaryRangesMax;
                    output = _payrollRepository.SavePayrollSetCTC(objctc);
                }
                var objothers = _payrollRepository.GetEmpPayrolBycycle(objctc.EmpID, objctc.payrollStructID, "");
                foreach (var struc in objothers)
                {
                    objctc.Ratevalues = decimal.Zero;
                    string objstructureRate = string.Empty;
                    var result2 = _payrollMasterRepository.GetPayrollRules(objctc.TenantID, objctc.LocationID, struc.EmpSalItemID);
                    if (result2.Count > 0)
                    {
                        if ((!string.IsNullOrEmpty(result2[0].PayrollRuleForm)) && (!string.IsNullOrEmpty(result2[0].PayrollRuleComponent)))
                        {
                            objctc.PayrollLineCalcFormat = "SELECT " + result2[0].PayrollRuleForm;
                        
                        string[] a1_values = result2[0].PayrollRuleComponent.Split(',');
                        foreach (var item in a1_values)
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                if (!string.IsNullOrEmpty(item))
                                {
                                    objctc.PayrollCode = item;
                                }
                                objstructureRate = _payrollMasterRepository.GetRuleSalaryValues(objctc);
                            }
                            if (!string.IsNullOrEmpty(objstructureRate) && !string.IsNullOrEmpty(objctc.PayrollLineCalcFormat))
                            {
                                objctc.PayrollLineCalcFormat = objctc.PayrollLineCalcFormat.Replace(objctc.PayrollCode, objstructureRate);
                            }
                        }
                        if (!string.IsNullOrEmpty(objstructureRate) && !string.IsNullOrEmpty(objctc.PayrollLineCalcFormat))
                        {
                            var objquery = _commonRepository.GetExecutiveScript(objctc.PayrollLineCalcFormat);
                            if (!string.IsNullOrEmpty(objstructureRate))
                                objctc.Ratevalues = Convert.ToDecimal(objquery);
                        }
                        }
                    }
                    //*Save Grocess salary to save Data Calculations*/
                    objctc.EmpSalStrucID = struc.EmpSalStrucID;
                    objctc.LineItemKey = struc.LineItemKey;
                    objctc.payrollStructID = struc.StructureID;
                    //objctc.PayrollLineCalcFormat = struc.PayrollLineCalcFormat;
                    objctc.SalaryRangeMin =  struc.SalaryRangesMin;
                    objctc.SalaryRangeMax =  struc.SalaryRangesMax;
                    output = _payrollRepository.SavePayrollSetCTC(objctc);
                }
                //var mm =  SaveEmpDynamicPayrollCTC(objctc);
                //code loop                
                response = new Response<SaveOut>(output, 200, output.Msg);
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> SaveAccountDetails(AccountDetailsBO objaccount)
        {
            Response<int> response;            
            try
            {                
                var result = _employeeRepository.SaveAccountDetails(objaccount);
                if (result.Id == 0)
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
        public Response<AccountDetailsBO> GetAccountDetails(int EmpID)
        {
            Response<AccountDetailsBO> response;            
            try
            {                
                var result = _employeeRepository.GetAccountDetails(EmpID);
                if (result == null)
                {
                    response = new Response<AccountDetailsBO>(result, 200, "Data Not Found");
                }
                else
                {
                    response = new Response<AccountDetailsBO>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<AccountDetailsBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> SaveOnBoardSettingDragDrop(List<BPTransaction> process)
        {
            Response<SaveOut> response;
            try
            {
                SaveOut result = new SaveOut();
                int i = 1;
                foreach (var item in process)
                {
                    item.TransOrder = i;
                    result = _onBoardRepository.SaveOnBoardSettingDragDrop(item);
                    i++;      
                }                
                response = new Response<SaveOut>(result, 200, "Data Retrieved");
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> SaveEmpDynamicPayrollCTC(EmpSalaryStructureCTC objctc)
        {
            Response<SaveOut> response;
            try
            {
                SaveOut output = new SaveOut();                
                var objothers = _payrollRepository.GetEmpPayrolBycycle(objctc.EmpID, objctc.payrollStructID, "General");
                foreach (var struc in objothers)
                {
                    objctc.Ratevalues = decimal.Zero;
                    var result2 = _payrollMasterRepository.GetPayrollRules(objctc.TenantID,objctc.LocationID,struc.EmpSalItemID);

                            if (result2.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(result2[0].PayrollRuleComponent))
                                {
                                    objctc.PayrollCode = result2[0].PayrollRuleComponent;
                                }
                                if (!string.IsNullOrEmpty(result2[0].PayrollRuleForm))
                                {
                                    objctc.PayrollLineCalcFormat = "SELECT " + result2[0].PayrollRuleForm;
                                }
                            }
                    var objstructureRate = _payrollMasterRepository.GetRuleSalaryValues(objctc);
                    if(!string.IsNullOrEmpty(objstructureRate) && !string.IsNullOrEmpty(objctc.PayrollLineCalcFormat))
                    {
                        objctc.PayrollLineCalcFormat = objctc.PayrollLineCalcFormat.Replace(objctc.PayrollCode, objstructureRate);
                        var objquery = _commonRepository.GetExecutiveScript(objctc.PayrollLineCalcFormat);
                        if(!string.IsNullOrEmpty(objstructureRate))
                           objctc.Ratevalues = Convert.ToDecimal(objquery);
                    }
                    
                    //*Save Grocess salary to save Data Calculations*/
                    objctc.EmpSalStrucID = struc.EmpSalStrucID;
                    objctc.LineItemKey = struc.LineItemKey;
                    objctc.payrollStructID = struc.StructureID;
                    output = _payrollRepository.SavePayrollSetCTC(objctc);
                }
                var objEarning = _payrollRepository.GetEmpPayrolBycycle(objctc.EmpID, objctc.payrollStructID, "Earnings");
                foreach (var struc in objEarning)
                {
                    objctc.Ratevalues = decimal.Zero;
                    var result2 = _payrollMasterRepository.GetPayrollRules(objctc.TenantID,objctc.LocationID,struc.EmpSalItemID);

                            if (result2.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(result2[0].PayrollRuleComponent))
                                {
                                    objctc.PayrollCode = result2[0].PayrollRuleComponent;
                                }
                                if (!string.IsNullOrEmpty(result2[0].PayrollRuleForm))
                                {
                                    objctc.PayrollLineCalcFormat = "SELECT " + result2[0].PayrollRuleForm;
                                }
                            }
                    var objstructureRate = _payrollMasterRepository.GetRuleSalaryValues(objctc);
                    if(!string.IsNullOrEmpty(objstructureRate) && !string.IsNullOrEmpty(objctc.PayrollLineCalcFormat))
                    {
                        objctc.PayrollLineCalcFormat = objctc.PayrollLineCalcFormat.Replace(objctc.PayrollCode, objstructureRate);
                        var objquery = _commonRepository.GetExecutiveScript(objctc.PayrollLineCalcFormat);
                        if(!string.IsNullOrEmpty(objstructureRate))
                           objctc.Ratevalues = Convert.ToDecimal(objquery);
                    }
                    
                    //*Save Grocess salary to save Data Calculations*/
                    objctc.EmpSalStrucID = struc.EmpSalStrucID;
                    objctc.LineItemKey = struc.LineItemKey;
                    objctc.payrollStructID = struc.StructureID;
                    output = _payrollRepository.SavePayrollSetCTC(objctc);
                }
                var objDectuion = _payrollRepository.GetEmpPayrolBycycle(objctc.EmpID, objctc.payrollStructID, "Deductions");
                foreach (var struc in objDectuion)
                {
                    objctc.Ratevalues = decimal.Zero;
                    var result2 = _payrollMasterRepository.GetPayrollRules(objctc.TenantID,objctc.LocationID,struc.EmpSalItemID);

                            if (result2.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(result2[0].PayrollRuleComponent))
                                {
                                    objctc.PayrollCode = result2[0].PayrollRuleComponent;
                                }
                                if (!string.IsNullOrEmpty(result2[0].PayrollRuleForm))
                                {
                                    objctc.PayrollLineCalcFormat = "SELECT " + result2[0].PayrollRuleForm;
                                }
                            }
                    var objstructureRate = _payrollMasterRepository.GetRuleSalaryValues(objctc);
                    if(!string.IsNullOrEmpty(objstructureRate) && !string.IsNullOrEmpty(objctc.PayrollLineCalcFormat))
                    {
                        objctc.PayrollLineCalcFormat = objctc.PayrollLineCalcFormat.Replace(objctc.PayrollCode, objstructureRate);
                        var objquery = _commonRepository.GetExecutiveScript(objctc.PayrollLineCalcFormat);
                        if(!string.IsNullOrEmpty(objstructureRate))
                           objctc.Ratevalues = Convert.ToDecimal(objquery);
                    }                    
                    //*Save Grocess salary to save Data Calculations*/
                    objctc.EmpSalStrucID = struc.EmpSalStrucID;
                    objctc.LineItemKey = struc.LineItemKey;
                    objctc.payrollStructID = struc.StructureID;
                    objctc.PayrollLineCalcFormat = struc.PayrollLineCalcFormat;
                    output = _payrollRepository.SavePayrollSetCTC(objctc);
                }
                //code loop                
                response = new Response<SaveOut>(output, 200, output.Msg);
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> UpdateHRDirEmployee(OnboardPersonalDetail onboarding)
        {
            Response<int> response;
            try
            {
                int empid = 0;
                //onboarding.Address = new List<EmployeeAddress>();
                if (onboarding.EmpID > 0)
                {
                    empid = _onBoardRepository.HRDirEmpPersonal(onboarding);
                    if (empid > 0)
                    {
                        if (onboarding.Address.Count > 0)
                        {
                            onboarding.Address[0].ReferenceID = empid;
                            var mm = _onBoardRepository.SaveEmployeeAddress(onboarding.Address);
                        }
                    }
                }
                if (empid == 0)
                {
                    response = new Response<int>(empid, 500, "Employee Details Updation is Failed");
                }
                else
                {
                    response = new Response<int>(empid, 200, "Updated Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }        
    }
}