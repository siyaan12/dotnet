using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.Employee;
using MyDodos.ViewModel.Employee;
using MyDodos.Repository.AzureStorage;
using MyDodos.Repository.HR;
using MyDodos.Domain.AzureStorage;
using MyDodos.Repository.Auth;
using System;
using System.IO;
using System.Linq;
using MyDodos.Repository.TemplateManager;
using MyDodos.Domain.AuthBO;
using System.Collections.Generic;
using KoSoft.DocRepo;
using MyDodos.Domain.Employee;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using MyDodos.Domain.Master;
using MyDodos.ViewModel.Common;
using MyDodos.Domain.Payroll;
using MyDodos.Domain.HR;
using MyDodos.Domain.BenefitManagement;
using MyDodos.Repository.BenefitManagement;
using MyDodos.Repository.Payroll;
using MyDodos.ViewModel.HR;

namespace MyDodos.Service.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IConfiguration _configuration;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAuthRepository _authRepository;
        private readonly IOnBoardRepository _onBoardRepository;
        private readonly IDocRepository _docRepository;
        private readonly IStorageConnect _storage;
        private readonly IPayrollRepository _ipayrollRepository;
        private readonly ILeaveBenefitRepository _leaveBenefitRepository;
        private readonly IDisabilityBenefitRepository _disabilityBenefitRepository;
        private readonly IBenefitGroupRepository _benefitGroupRepository;
        private readonly IPayrollRepository _payrollRepository;
        private readonly IMedicalInsuranceRepository _medicalInsuranceRepository;
        public readonly IPayrollMasterRepository _payrollMasterRepository;
        public EmployeeService(IConfiguration configuration, IEmployeeRepository employeeRepository, IDocRepository docRepository, IStorageConnect storage, IOnBoardRepository onBoardRepository, IAuthRepository authRepository,ILeaveBenefitRepository leaveBenefitRepository,IDisabilityBenefitRepository disabilityBenefitRepository,IBenefitGroupRepository benefitGroupRepository,IPayrollRepository payrollRepository, IPayrollRepository ipayrollRepository,IMedicalInsuranceRepository medicalInsuranceRepository,IPayrollMasterRepository payrollMasterRepository)
        {
            _configuration = configuration;
            _employeeRepository = employeeRepository;
            _docRepository = docRepository;
            _storage = storage;
            _onBoardRepository = onBoardRepository;
            _authRepository = authRepository;
            _ipayrollRepository = ipayrollRepository;
            _leaveBenefitRepository = leaveBenefitRepository;
            _disabilityBenefitRepository = disabilityBenefitRepository;
            _benefitGroupRepository = benefitGroupRepository;
            _payrollRepository = payrollRepository;
            _medicalInsuranceRepository = medicalInsuranceRepository;
            _payrollMasterRepository = payrollMasterRepository;
        }
        public Response<GetHRDirectoryList> GetHRDirectoryList(GetHRDirectoryList objresult)
        {
            Response<GetHRDirectoryList> response;
            try
            {
                var result = _employeeRepository.GetHRDirectoryList(objresult);
                foreach (var list in result.objHRDirectoryList)
                {
                    list.base64Images = "";
                    List<string> obj = new List<string>();
                    DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                    //DocumentDA objdocservices = new DocumentDA(_configuration.GetConnectionString("KOPRODADBConnection"), KoSoft.DocRepo.DBType.MySQL);
                    var results = objdoc.GetDocument(0, list.EmpID, "", "ProfileImage", list.TenantID);
                    if (results.Count > 0)
                    {
                        var objcont = objdoc.GetDocContainer(list.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                        //string TenantName = objcont[0].ContainerName.ToLower();
                        var doc = _storage.DownloadDocument(new SaveDocCloudBO
                        {
                            CloudType = _configuration.GetSection("CloudType").Value,
                            Container = objcont[0].ContainerName.ToLower(),
                            fileName = results[0].GenDocName,
                            folderPath = results[0].DirectionPath,
                            ProductCode = Convert.ToString(objresult.objHRDirectoryInput.productId)
                        }).Result;
                        list.base64Images = doc.DocumentURL;
                    }
                    if (!string.IsNullOrEmpty(list.HRNotification))
                    {
                        var fieldname = list.HRNotification.Split(", ");
                        obj = fieldname.ToList();
                        list.ListHRNotification = obj;
                    }
                }
                if (result.objHRDirectoryList.Count() == 0)
                {
                    response = new Response<GetHRDirectoryList>(result, 200, "Data Not Found");
                }
                else
                {
                    foreach (var list in result.objHRDirectoryList)
                    {
                        var emp = _employeeRepository.GetEmpOnBoard(list.EmpID);
                        foreach (var item in emp)
                        {
                            var trans = _employeeRepository.GetBPTransName(item.EmpOnboardingID);
                            list.OnBoardTrackStatus = trans;
                        }
                        if(list.BenefitGroupID != null && list.BenefitGroupID > 0)
                        {
                            var mm = _payrollMasterRepository.getpayrollEmployeeBenefits(list.EmpID, list.BenefitGroupID.Value);
                            if(mm.Count > 0)
                            list.StructureID = mm[0].PayrollStructID;
                        }
                    }
                    var summary = _employeeRepository.GetHRDirectorySummery(objresult.objHRDirectoryInput.TenantID, objresult.objHRDirectoryInput.LocationID, objresult.objHRDirectoryInput.DepartmentID);
                    result.Summary = summary;
                    UserViewBO user = new UserViewBO();
                    var view = GetUserView(objresult.objHRDirectoryInput.EmpID, objresult.objHRDirectoryInput.EmpID, objresult.objHRDirectoryInput.TenantID);
                    result.UserView = view.Data;
                    var comp = _employeeRepository.GetHRDirInCompleteCount(objresult.objHRDirectoryInput.TenantID, objresult.objHRDirectoryInput.LocationID);
                    result.InCompletedCounts = comp;
                    response = new Response<GetHRDirectoryList>(result, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<GetHRDirectoryList>(ex.Message, 500);
            }
            return response;
        }
        public Response<UserViewBO> GetUserView(int EmpID, int LoginID, int TenantID)
        {
            Response<UserViewBO> response;
            try
            {
                UserViewBO user = new UserViewBO();
                var result = _employeeRepository.GetUserView(EmpID, LoginID, TenantID);
                if (result.Count() > 0)
                {
                    user.CommonName = result[0].CommonName;
                    //user.EmployeeViewList = result;
                }
                if (result.Count() == 0)
                {
                    response = new Response<UserViewBO>(user, 200, "Data Not Found");
                }
                else
                {
                    response = new Response<UserViewBO>(user, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<UserViewBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<HRDirectoryEmpView> GetEmployeeViewDetails(int EmpID, int LocationID, int TenantID)
        {
            Response<HRDirectoryEmpView> response;
            InputAppUserBO app = new InputAppUserBO();
            Employment employee = new Employment();
            try
            {
                var result = _employeeRepository.GetEmployeeViewDetails(EmpID, LocationID, TenantID);
                result.base64images = "";
                DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                //DocumentDA objdocservices = new DocumentDA(_configuration.GetConnectionString("KOPRODADBConnection"), KoSoft.DocRepo.DBType.MySQL);
                var results = objdoc.GetDocument(0, result.EmpID, "", "ProfileImage", result.TenantID);
                //var results = _docRepository.GetDocument(0, result.EmpID, "", "ProfileImage", result.TenantID);
                if (results.Count > 0)
                {
                    var objcont = objdoc.GetDocContainer(result.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                    string TenantName = objcont[0].ContainerName.ToLower();
                    var doc = _storage.DownloadDocument(new SaveDocCloudBO
                    {
                        CloudType = _configuration.GetSection("CloudType").Value,
                        Container = TenantName,
                        fileName = results[0].GenDocName,
                        folderPath = results[0].DirectionPath,
                        ProductCode = Convert.ToString(result.ProductID)
                    }).Result;
                    result.base64images = _storage.ReadDataInUrl(doc.DocumentURL);
                }
                if (result == null)
                {
                    response = new Response<HRDirectoryEmpView>(result, 200, "Data Not Found");
                }
                else
                {
                    var info = _employeeRepository.GetEmployeePersonalInfo(result.EmpID, result.LocationID, result.TenantID);
                    result.PersonalInformation = info;
                    var address = _onBoardRepository.GetAddress(info.EmpID);
                    info.Address = address;
                    var education = _onBoardRepository.GetOnboardEducation(info.EmpID);
                    employee.Education = education;
                    var experience = _onBoardRepository.GetOnboardEmpExperience(info.EmpID);
                    employee.Experience = experience;
                    result.Employment = employee;
                    var appuserdet = _authRepository.AddProfile(app).Data;
                    if (appuserdet != null)
                    {
                        //var appuserdet = appdet.appUser;
                        //if (appuserdet != null)
                        //{
                        app.AppUserID = Convert.ToInt32(appuserdet.AppUser.AppUserID);
                        app.EmpID = result.EmpID;
                        app.UserName = appuserdet.AppUser.AppUserName;
                        app.Password = appuserdet.AppUser.AppUserPassword;
                        app.AppUserStatus = result.EmpStatus;
                        app.PrimaryEmail = appuserdet.AppUser.AppUserName;
                        app.UserAccessType = appuserdet.AppUser.UserType;
                        app.AppUserID = _onBoardRepository.SaveEmployeeAppuser(app);
                        //}
                    }
                    response = new Response<HRDirectoryEmpView>(result, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<HRDirectoryEmpView>(ex.Message, 500);
            }
            return response;
        }
        public Response<OrgChartBO> GetIndividualEmpOrgzChart(int EmpID)
        {
            Response<OrgChartBO> response;
            try
            {
                OrgChartBO charts = new OrgChartBO();
                List<EmpReportingBO> report = new List<EmpReportingBO>();
                List<EmpDirectorBO> diret = new List<EmpDirectorBO>();
                List<EmpColleaguesBO> colle = new List<EmpColleaguesBO>();
                var reports = _employeeRepository.GetEmpReports(EmpID);
                charts.Reporting = reports;
                charts.Colleagues = _employeeRepository.GetEmpColleagues(EmpID);

                foreach (var item in reports)
                {
                    var direct = _employeeRepository.GetEmpDirects(item.EmpID);
                    diret.AddRange(direct);
                    charts.Directors = diret;
                }
                response = new Response<OrgChartBO>(charts, 200, "Data Retraived");
                //}
            }
            catch (Exception ex)
            {
                response = new Response<OrgChartBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<EmpReportingOrgBO> GetIndividualOrgzChart(int EmpID,int ManagerID, int LocationID, int TenantID)
        {
            Response<EmpReportingOrgBO> response;
            try
            {
                EmpReportingOrgBO orgchart = new EmpReportingOrgBO();
                List<EmpReportingOrgBO> orgReport = new List<EmpReportingOrgBO>();

                orgchart = _employeeRepository.GetParentsOrgzChart(EmpID, 0, LocationID, TenantID).FirstOrDefault();          
                
                if (orgchart == null)
                {
                    response = new Response<EmpReportingOrgBO>(orgchart, 200, "Data Not Found");
                }
                else
                {
                    // foreach (var user in orgchart)
                    // {
                        orgchart.AddChild(new EmpReportingOrgBO
                        {
                            EmpID = orgchart.EmpID,
                            EmployeeName = orgchart.EmployeeName,
                            base64images = orgchart.base64images,
                            FuncRoleID = orgchart.FuncRoleID,
                            RoleName = orgchart.RoleName,
                            Gender = orgchart.Gender,
                            ManagerID = orgchart.ManagerID,
                            Children = _employeeRepository.GetParentsOrgzChart(orgchart.EmpID, orgchart.EmpID, LocationID, TenantID).ToList()
                        });
                        
                        if (orgchart.Children.Count != 0)
                        {
                            foreach (var det in orgchart.Children)
                            {
                               orgchart.AddChild(new EmpReportingOrgBO
                                {
                                    EmpID = det.EmpID,
                                    EmployeeName = det.EmployeeName,
                                    base64images = det.base64images,
                                    FuncRoleID = det.FuncRoleID,
                                    RoleName = det.RoleName,
                                    Gender = det.Gender,
                                    ManagerID = det.ManagerID,
                                    Children = _employeeRepository.GetParentsOrgzChart(det.EmpID, det.EmpID, LocationID, TenantID).ToList()
                                });
                            }
                        // }
                            
                        // }
                    }
                //     // foreach (var item in orgReport)
                //     // {
                //     //     if (item.Childs.Count != 0)
                //     //     {
                //     //         foreach (var det in item.Childs)
                //     //         {
                //     //            orgchart.Add(new EmpReportingOrgBO
                //     //             {
                //     //                 EmpID = det.EmpID,
                //     //                 EmployeeName = det.EmployeeName,
                //     //                 base64images = det.base64images,
                //     //                 FuncRoleID = det.FuncRoleID,
                //     //                 RoleName = det.RoleName,
                //     //                 Gender = det.Gender,
                //     //                 ManagerID = det.ManagerID,
                //     //                 Childs = _employeeRepository.GetParentsOrgzChart(det.EmpID, det.EmpID, LocationID, TenantID).ToList()
                //     //             });
                //     //         }
                //     //     }
                //     // }
                     //orgReport = orgchart;

                    //     foreach (var sub in user.sub)

                    //     {

                    //         userSubordinatesViews.Add(new UserSubordinatesView

                    //         {

                    //             UserId = sub.UserId,

                    //             FirstName = sub.FirstName,

                    //             LastName = sub.LastName,

                    //         });


                    //     }

                    //     userDirectsView.Add(new UserDirectsView

                    //     {

                    //         UserId = user.UserId,

                    //         FirstName = user.FirstName,

                    //         LastName = user.LastName,

                    //         directs = userSubordinatesViews


                    //     });

                    // }

                // // var reporys = orgReport;
                // // //chailds
                // // orgReport.ForEach(s=> s.Childs = reporys.Where(d => d.EmpID == EmpID).ToList());
                //     foreach (var orgdet in orgReport)
                //     {
                //     //var det = 

                //     orgReport.Add(new EmpReportingOrgBO{
                //         EmpID = orgdet.EmpID,
                //         EmployeeName = orgdet.EmployeeName,
                //         base64images = orgdet.base64images,
                //         FuncRoleID = orgdet.FuncRoleID,
                //         RoleName = orgdet.RoleName,
                //         Gender = orgdet.Gender,
                //         ManagerID = orgdet.ManagerID,
                //         Childs = orgchart.Where(d => d.EmpID == orgdet.EmpID).ToList()
                //         });
                        
                //     }
                //     
                    //response = new Response<List<EmpReportingOrgBO>>(orgReport, 200, "Data Retraived");
                    // foreach (var item in orgchart)
                    // {
                    //     var emp = _employeeRepository.GetChaildsOrgzChart(item.EmpID, item.ManagerID, LocationID, TenantID);
                    //     item.Childs = emp;                        
                    // }
                    response = new Response<EmpReportingOrgBO>(orgchart, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<EmpReportingOrgBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> SaveEmployeeProfile(EmployeeProfileBO profile)
        {
            Response<int> response;
            try
            {
                var idcard = _employeeRepository.SaveEmployeeProfile(profile.EmpProfile);
                if (profile.inputDocs != null)
                {
                    int DocID = 0;
                    string NewFileName;
                    string filExt;
                    DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                    DocumentDA objdocService = new DocumentDA(_configuration.GetConnectionString("KOPRODADBConnection"), DBType.MySQL);

                    var appuser = _authRepository.UpdateCenEntAppUser(profile.EmpProfile);
                    if (idcard.ReqID > 0)
                    {
                        filExt = Path.GetExtension(profile.inputDocs.docName.ToString());
                        NewFileName = "EmpProfile_" + (Guid.NewGuid()).ToString("N") + filExt;
                        if (profile.inputDocs.DocID > 0)
                        {
                            var result = objdoc.GetDocument(profile.inputDocs.DocID, 0, "", "", 0);
                            if (result.Count > 0)
                            {
                                var name = Path.GetFileNameWithoutExtension(result[0].GenDocName);
                                NewFileName = name + filExt;
                                DocID = result[0].DocID;
                            }
                        }
                        SaveDocCloudBO docCloudins = new SaveDocCloudBO();
                        var Repo = objdocService.GetDocRepository(profile.EmpProfile.PrimaryCompanyID, profile.EmpProfile.PrimaryCompanyID, "Tenant", Convert.ToInt32(_configuration.GetSection("ProductID").Value), LocationType.ProfileImage);
                        var docRepo = Repo;
                        if (docRepo[0].RepoType == "Storage")
                        {
                            var Base64PDF = profile.inputDocs.docsFile;
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
                                var objcont = objdoc.GetDocContainer(profile.EmpProfile.PrimaryCompanyID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                                docCloudins.Container = objcont[0].ContainerName.ToLower();
                                docCloudins.CloudType = _configuration.GetSection("CloudType").Value;
                                docCloudins.folderPath = docRepo[0].RepoPath.ToLower() + "/" + Convert.ToString(profile.EmpProfile.AppUserID);
                                docCloudins.file = Base64PDF;
                                docCloudins.fileName = NewFileName;
                                docCloudins.ProductCode = _configuration.GetSection("ProductID").Value;

                                _ = _storage.SaveBulkDocumentCloud(docCloudins);

                                GenDocument d1 = new GenDocument();
                                d1.DocID = DocID;
                                d1.RepositoryID = docRepo[0].RepositoryID;
                                d1.DocumentName = profile.inputDocs.docName;
                                d1.DocType = docRepo[0].RepoName;
                                d1.CreatedBy = profile.EmpProfile.ModifiedBy;
                                d1.OrgDocName = NewFileName;
                                d1.GenDocName = NewFileName;
                                d1.DocKey = (Guid.NewGuid()).ToString("N");
                                d1.DocSize = Convert.ToDecimal(profile.inputDocs.docsSize);
                                d1.Entity = docRepo[0].LocType;
                                d1.EntityID = profile.EmpProfile.EmpID;
                                d1.TenantID = profile.EmpProfile.PrimaryCompanyID;
                                d1.DirectionPath = docCloudins.folderPath;
                                var doc = objdoc.SaveDocument(d1);
                                DocID = doc;
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
                else
                {
                    response = new Response<int>(idcard.ReqID, 200, "Saved Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<EmployeeProfileInputBO> GetEmployeeProfile(int AppUserID, int ProductID)
        {
            Response<EmployeeProfileInputBO> responce;
            string path = string.Empty;
            string fileName = string.Empty;
            AzureDocURLBO docURL = new AzureDocURLBO();
            try
            {
                var objreturn = _employeeRepository.GetEmployeeProfile(AppUserID, ProductID);
                if (objreturn.AppUserID == 0)
                {
                    responce = new Response<EmployeeProfileInputBO>("Data Valid Not Found", 500);
                }
                else
                {
                    objreturn.base64Images = "";
                    DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                    var docs = objdoc.GetDocument(0, objreturn.EmpID, "", "ProfileImage", objreturn.PrimaryCompanyID);
                    if (docs.Count > 0)
                    {
                        var objcont = objdoc.GetDocContainer(objreturn.PrimaryCompanyID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));

                        var doc = _storage.DownloadDocument(new SaveDocCloudBO
                        {
                            CloudType = _configuration.GetSection("CloudType").Value,
                            Container = objcont[0].ContainerName.ToLower(),
                            fileName = docs[0].GenDocName,
                            folderPath = docs[0].DirectionPath,
                            ProductCode = Convert.ToString(ProductID)
                        }).Result;
                        objreturn.base64Images = _storage.ReadDataInUrl(doc.DocumentURL);
                    }
                }
                responce = new Response<EmployeeProfileInputBO>(objreturn, 200, "Data Retraived");
            }
            catch (Exception ex)
            {
                responce = new Response<EmployeeProfileInputBO>(ex.Message, 500);
            }
            return responce;
        }
        public Response<int> SaveCompanyProfile(CompanyProfileBO profile)
        {
            Response<int> response;
            try
            {
                var result = _employeeRepository.GetTenantProfile(profile.TenantProfile.TenantID, profile.TenantProfile.ProductID);
                if (result.TenantID == 0)
                {
                    response = new Response<int>(0, 500, "TenantProfile Data Creation or Updation is Failed");
                }
                else
                {
                    if (profile.inputDocs.RegularLogodocsFile != "" && profile.inputDocs.RegularLogodocsFile != null)
                    {
                        int DocID = 0;
                        string NewFileName;
                        string filExt;
                        DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                        DocumentDA objdocService = new DocumentDA(_configuration.GetConnectionString("KOPRODADBConnection"), DBType.MySQL);
                        filExt = Path.GetExtension(profile.inputDocs.RegularLogodocName.ToString());
                        NewFileName = "TenantProfile_" + (Guid.NewGuid()).ToString("N") + filExt;
                        if (profile.inputDocs.RegularDocID > 0)
                        {
                            var results = objdoc.GetDocument(profile.inputDocs.RegularDocID, 0, "", "", 0);
                            if (results.Count > 0)
                            {
                                var name = Path.GetFileNameWithoutExtension(results[0].GenDocName);
                                NewFileName = name + filExt;
                                DocID = results[0].DocID;
                            }
                        }
                        //else
                        //{
                        //NewFileName = "TenantProfile_" + (Guid.NewGuid()).ToString("N") + filExt;
                        //}

                        SaveDocCloudBO docCloudins = new SaveDocCloudBO();
                        var Repo = objdocService.GetDocRepository(profile.TenantProfile.TenantID, profile.TenantProfile.TenantID, "Tenant", Convert.ToInt32(_configuration.GetSection("ProductID").Value), LocationType.TenantImage);
                        var docRepo = Repo;
                        if (docRepo[0].RepoType == "Storage")
                        {
                            var Base64PDF = profile.inputDocs.RegularLogodocsFile;
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
                                //var objcont = objdoc.GetDocRepository(profile.IDcard.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value), DocRepoName.OnBoardIDCard);
                                var objcont = objdoc.GetDocContainer(profile.TenantProfile.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                                docCloudins.Container = objcont[0].ContainerName.ToLower();
                                docCloudins.CloudType = _configuration.GetSection("CloudType").Value;
                                docCloudins.folderPath = docRepo[0].RepoPath.ToLower() + "/" + Convert.ToString(result.TenantCode);
                                docCloudins.file = Base64PDF;
                                docCloudins.fileName = NewFileName;
                                docCloudins.ProductCode = _configuration.GetSection("ProductID").Value;

                                _ = _storage.SaveBulkDocumentCloud(docCloudins);

                                GenDocument d1 = new GenDocument();
                                d1.DocID = DocID;
                                d1.RepositoryID = docRepo[0].RepositoryID;
                                d1.DocumentName = profile.inputDocs.RegularLogodocName;
                                d1.DocType = docRepo[0].RepoName;
                                //d1.CreatedBy = profile.EmpProfile.ModifiedBy;
                                d1.OrgDocName = NewFileName;
                                d1.GenDocName = NewFileName;
                                d1.DocKey = (Guid.NewGuid()).ToString("N");
                                d1.DocSize = Convert.ToDecimal(profile.inputDocs.RegulardocsSize);
                                d1.Entity = docRepo[0].LocType;
                                d1.EntityID = profile.TenantProfile.TenantID;
                                d1.TenantID = profile.TenantProfile.TenantID;
                                //d1.LocationID = profile.EmpProfile.LocationID;
                                d1.DirectionPath = docCloudins.folderPath;
                                var doc = objdoc.SaveDocument(d1);
                                var update = _employeeRepository.UpdateTenantProfile(profile.TenantProfile);
                                TenantProfiledataBO tenant = new TenantProfiledataBO();
                                if (result.TenantID > 0)
                                {
                                    tenant.TenantID = result.TenantID;
                                    tenant.TenantCode = result.TenantCode;
                                    tenant.TenantName = result.TenantName;
                                    tenant.TenantType = result.TenantType;
                                    tenant.ParentTenantID = result.ParentTenantID;
                                    tenant.TaxID = result.TaxID;
                                    tenant.InCorpState = result.InCorpState;
                                    tenant.Address1 = result.Address1;
                                    tenant.Address2 = result.Address2;
                                    tenant.City = result.City;
                                    tenant.State = result.State;
                                    tenant.Zip = result.Zip;
                                    tenant.Country = result.Country;
                                    tenant.Website = result.Website;
                                    tenant.AlternatePhone = result.AlternatePhone;
                                    tenant.Fax = result.Fax;
                                    tenant.TenantStatus = result.TenantStatus;
                                    tenant.CreatedFromIP = result.CreatedFromIP;
                                    tenant.GeoRegion = result.GeoRegion;
                                    tenant.BillAddress1 = result.BillAddress1;
                                    tenant.BillAddress2 = result.BillAddress2;
                                    tenant.BillCity = result.BillCity;
                                    tenant.BillZipCode = result.BillZipCode;
                                    tenant.BillState = result.BillState;
                                    tenant.BillCountry = result.BillCountry;
                                    tenant.TenantRegNo = result.TenantRegNo;
                                    tenant.ContactUsEmailID = result.ContactUsEmailID;
                                    tenant.ContactUsTelephone = result.ContactUsTelephone;
                                    tenant.ShortName = result.ShortName;
                                    tenant.TenantAccountNo = result.TenantAccountNo;
                                    tenant.CarrierType = result.CarrierType;
                                    tenant.AdministratorName = result.AdministratorName;
                                    tenant.AdministratorEmail = result.AdministratorEmail;
                                    tenant.AdministratorPhone = result.AdministratorPhone;
                                    tenant.PrimaryPhone = profile.TenantProfile.PrimaryPhone;
                                    tenant.Email = profile.TenantProfile.Email;
                                    var ten = _authRepository.SaveTenantProfile(tenant);
                                }
                            }
                        }
                    }
                    if (profile.inputDocs.SmallLogodocsFile != "" && profile.inputDocs.SmallLogodocsFile != null)
                    {
                        int DocID = 0;
                        string NewFileName;
                        string filExt;
                        DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                        DocumentDA objdocService = new DocumentDA(_configuration.GetConnectionString("KOPRODADBConnection"), DBType.MySQL);
                        filExt = Path.GetExtension(profile.inputDocs.SmallLogodocName.ToString());
                        NewFileName = "TenantLogo_" + (Guid.NewGuid()).ToString("N") + filExt;
                        if (profile.inputDocs.SmallDocID > 0)
                        {
                            var results = objdoc.GetDocument(profile.inputDocs.SmallDocID, 0, "", "", 0);
                            if (results.Count > 0)
                            {
                                var name = Path.GetFileNameWithoutExtension(results[0].GenDocName);
                                NewFileName = name + filExt;
                                DocID = results[0].DocID;
                            }
                        }
                        //else
                        //{
                        //NewFileName = "TenantProfile_" + (Guid.NewGuid()).ToString("N") + filExt;
                        //}

                        SaveDocCloudBO docCloudins = new SaveDocCloudBO();
                        var Repo = objdocService.GetDocRepository(profile.TenantProfile.TenantID, profile.TenantProfile.TenantID, "Tenant", Convert.ToInt32(_configuration.GetSection("ProductID").Value), LocationType.TenantLogo);
                        var docRepo = Repo;
                        if (docRepo[0].RepoType == "Storage")
                        {
                            var Base64PDF = profile.inputDocs.SmallLogodocsFile;
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
                                //var objcont = objdoc.GetDocRepository(profile.IDcard.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value), DocRepoName.OnBoardIDCard);
                                var objcont = objdoc.GetDocContainer(profile.TenantProfile.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                                docCloudins.Container = objcont[0].ContainerName.ToLower();
                                docCloudins.CloudType = _configuration.GetSection("CloudType").Value;
                                docCloudins.folderPath = docRepo[0].RepoPath.ToLower() + "/" + Convert.ToString(result.TenantCode);
                                docCloudins.file = Base64PDF;
                                docCloudins.fileName = NewFileName;
                                docCloudins.ProductCode = _configuration.GetSection("ProductID").Value;

                                _ = _storage.SaveBulkDocumentCloud(docCloudins);

                                GenDocument d1 = new GenDocument();
                                d1.DocID = DocID;
                                d1.RepositoryID = docRepo[0].RepositoryID;
                                d1.DocumentName = profile.inputDocs.SmallLogodocName;
                                d1.DocType = docRepo[0].RepoName;
                                //d1.CreatedBy = profile.EmpProfile.ModifiedBy;
                                d1.OrgDocName = NewFileName;
                                d1.GenDocName = NewFileName;
                                d1.DocKey = (Guid.NewGuid()).ToString("N");
                                d1.DocSize = Convert.ToDecimal(profile.inputDocs.SmalldocsSize);
                                d1.Entity = docRepo[0].LocType;
                                d1.EntityID = profile.TenantProfile.TenantID;
                                d1.TenantID = profile.TenantProfile.TenantID;
                                //d1.LocationID = profile.EmpProfile.LocationID;
                                d1.DirectionPath = docCloudins.folderPath;
                                var doc = objdoc.SaveDocument(d1);
                                var update = _employeeRepository.UpdateTenantProfile(profile.TenantProfile);
                                TenantProfiledataBO tenant = new TenantProfiledataBO();
                                if (result.TenantID > 0)
                                {
                                    tenant.TenantID = result.TenantID;
                                    tenant.TenantCode = result.TenantCode;
                                    tenant.TenantName = result.TenantName;
                                    tenant.TenantType = result.TenantType;
                                    tenant.ParentTenantID = result.ParentTenantID;
                                    tenant.TaxID = result.TaxID;
                                    tenant.InCorpState = result.InCorpState;
                                    tenant.Address1 = result.Address1;
                                    tenant.Address2 = result.Address2;
                                    tenant.City = result.City;
                                    tenant.State = result.State;
                                    tenant.Zip = result.Zip;
                                    tenant.Country = result.Country;
                                    tenant.Website = result.Website;
                                    tenant.AlternatePhone = result.AlternatePhone;
                                    tenant.Fax = result.Fax;
                                    tenant.TenantStatus = result.TenantStatus;
                                    tenant.CreatedFromIP = result.CreatedFromIP;
                                    tenant.GeoRegion = result.GeoRegion;
                                    tenant.BillAddress1 = result.BillAddress1;
                                    tenant.BillAddress2 = result.BillAddress2;
                                    tenant.BillCity = result.BillCity;
                                    tenant.BillZipCode = result.BillZipCode;
                                    tenant.BillState = result.BillState;
                                    tenant.BillCountry = result.BillCountry;
                                    tenant.TenantRegNo = result.TenantRegNo;
                                    tenant.ContactUsEmailID = result.ContactUsEmailID;
                                    tenant.ContactUsTelephone = result.ContactUsTelephone;
                                    tenant.ShortName = result.ShortName;
                                    tenant.TenantAccountNo = result.TenantAccountNo;
                                    tenant.CarrierType = result.CarrierType;
                                    tenant.AdministratorName = result.AdministratorName;
                                    tenant.AdministratorEmail = result.AdministratorEmail;
                                    tenant.AdministratorPhone = result.AdministratorPhone;
                                    tenant.PrimaryPhone = profile.TenantProfile.PrimaryPhone;
                                    tenant.Email = profile.TenantProfile.Email;
                                    var ten = _authRepository.SaveTenantProfile(tenant);
                                }
                            }
                        }
                    }
                }
                response = new Response<int>(profile.TenantProfile.TenantID, 200, "Saved Successfully");

            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        public Response<TenantProfileImageBO> GetTenantProfile(int TenantID, int ProductID)
        {
            Response<TenantProfileImageBO> responce;
            string path = string.Empty;
            string fileName = string.Empty;
            AzureDocURLBO docURL = new AzureDocURLBO();
            try
            {
                var objreturn = _employeeRepository.GetTenantProfileImage(TenantID, ProductID);
                if (objreturn.TenantID == 0)
                {
                    responce = new Response<TenantProfileImageBO>("Data Valid Not Found", 500);
                }
                else
                {
                    objreturn.RegularLogobase64Images = "";
                    objreturn.SmallLogobase64Images = "";
                    DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                    var docs = objdoc.GetDocument(0, objreturn.TenantID, "", "TenantImage", objreturn.TenantID);
                    if (docs.Count > 0)
                    {
                        var objcont = objdoc.GetDocContainer(objreturn.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));

                        var doc = _storage.DownloadDocument(new SaveDocCloudBO
                        {
                            CloudType = _configuration.GetSection("CloudType").Value,
                            Container = objcont[0].ContainerName.ToLower(),
                            fileName = docs[0].GenDocName,
                            folderPath = docs[0].DirectionPath,
                            ProductCode = Convert.ToString(ProductID)
                        }).Result;
                        objreturn.RegularLogobase64Images = _storage.ReadDataInUrl(doc.DocumentURL);
                    }
                    var documents = objdoc.GetDocument(0, objreturn.TenantID, "", "TenantLogo", objreturn.TenantID);
                    if (documents.Count > 0)
                    {
                        var objconta = objdoc.GetDocContainer(objreturn.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));

                        var docu = _storage.DownloadDocument(new SaveDocCloudBO
                        {
                            CloudType = _configuration.GetSection("CloudType").Value,
                            Container = objconta[0].ContainerName.ToLower(),
                            fileName = documents[0].GenDocName,
                            folderPath = documents[0].DirectionPath,
                            ProductCode = Convert.ToString(ProductID)
                        }).Result;
                        objreturn.SmallLogobase64Images = _storage.ReadDataInUrl(docu.DocumentURL);
                    }
                }
                responce = new Response<TenantProfileImageBO>(objreturn, 200, "Data Retraived");
            }
            catch (Exception ex)
            {
                responce = new Response<TenantProfileImageBO>(ex.Message, 500);
            }
            return responce;
        }
        public Response<SaveOut> SaveLocation(LocationdetBO objlocation)
        {
           Response<SaveOut> response;
            try
            {
                SaveOut result = new SaveOut();
                result = _employeeRepository.SaveLocation(objlocation);
                
                if (result.Id == 0)
                {
                    response = new Response<SaveOut>(result, 200, "Data not Found");
                }
                else
                {
                    response = new Response<SaveOut>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<SaveOut>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<vwPayrollUser>> GetPayRollEmployees(int TenantID,int LocationID,int EmpID)
        {
            Response<List<vwPayrollUser>> response;
            List<vwPayrollUser> list = new List<vwPayrollUser>();
            try
            {
                var result = _employeeRepository.EmpPayrollSalaryMonth(TenantID,LocationID,EmpID);
                if (result.Count == 0)
                {
                    response = new Response<List<vwPayrollUser>>(result, 200, "Data not Found");
                }
                else
                {
                    foreach(var item in result)
                    {
                        var payhistory = _employeeRepository.GetPayHistory(item.TenantID,item.LocationID,item.SalaryPeriodId);
                        list.AddRange(payhistory);
                    }
                    response = new Response<List<vwPayrollUser>>(list, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<vwPayrollUser>>(ex.Message, 500);
            }
            return response;
        }
        // public Response<OnboardingEmpBenefitsBO> GetEmployeePayInfo(int EmpID, int BenefitGroupID, string paycycle)
        // {
        //     Response<OnboardingEmpBenefitsBO> response;
        //     try
        //     {
        //         var result = _ipayrollRepository.GetEmployeePayrollCycle(SalaryonboardPaycycle objPaycycle);                
        //         if (result.Count() == 0)
        //         {
        //             response = new Response<OnboardingEmpBenefitsBO>(result, 200, "Data Not Found");
        //         }
        //         else
        //         {
        //             response = new Response<OnboardingEmpBenefitsBO>(result, 200, "Data Retrieved");
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         response = new Response<OnboardingEmpBenefitsBO>(ex.Message, 500);
        //     }
        //     return response;
        // }
        public Response<OnboardingBenefitsBO> GetBenefitsByEmp(int EmpID,int TenantID,int LocationID)
        {
            Response<OnboardingBenefitsBO> response;
            OnboardingBenefitsBO objoutput = new OnboardingBenefitsBO();
            List<MasLeaveGroupBO> objmasleave = new List<MasLeaveGroupBO>();
            List<LeaveAllocReferenceBO> objref = new List<LeaveAllocReferenceBO>();
            List<PlanTypeCategoryBO> objplanwise = new List<PlanTypeCategoryBO>();
            PlanTypeCategoryBO lstobjplan = new PlanTypeCategoryBO();
            List<HRBenefitPlansBO> objplan = new List<HRBenefitPlansBO>();

            try
            {
                var emp = _employeeRepository.GetBenefitGroupByEmp(EmpID);
                var result = _benefitGroupRepository.GetBenefitGroup(emp,TenantID,LocationID);
                var medben = _benefitGroupRepository.GetBenefitMedPlan(emp,TenantID,LocationID);

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
                    var other = _payrollRepository.GetOtherBenefits(emp,TenantID,LocationID);
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
        public Response<MyTeamBO> GetMyTeam(int TenantID,int EmpID)
        {
            Response<MyTeamBO> response;
            try
            {
                MyTeamBO charts = new MyTeamBO();
                var emp = _employeeRepository.GetEmpDetails(EmpID,TenantID,0);
                charts.EmpDetails = emp;
                //base64image
                foreach(var item in charts.EmpDetails)
                {
                    item.base64images= "";
                    DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                    var results = objdoc.GetDocument(0, EmpID, "", "ProfileImage", TenantID);
                    if (results.Count > 0)
                    {
                        var objcont = objdoc.GetDocContainer(TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                        var doc = _storage.DownloadDocument(new SaveDocCloudBO
                        {
                            CloudType = _configuration.GetSection("CloudType").Value,
                            Container = objcont[0].ContainerName.ToLower(),
                            fileName = results[0].GenDocName,
                            folderPath = results[0].DirectionPath,
                            ProductCode = Convert.ToString(_configuration.GetSection("ProductID").Value)
                        }).Result;
                        item.base64images = doc.DocumentURL;
                    }
                }
                var reports = _employeeRepository.GetEmpReports(EmpID);
                charts.ReportingTo = reports;
                var project = _employeeRepository.GetEmpProjectList(EmpID,0,false);
                charts.ProjectList = project;
                foreach(var item in charts.ProjectList)
                {
                    var manager = _employeeRepository.GetProjectmanager(0,item.ProjectID,item.IsProjectManager = true);
                    item.ManagerDetails = manager;
                    var colleagues = _employeeRepository.GetProjectmanager(0,item.ProjectID,item.IsProjectManager = false);
                    item.Colleagues = colleagues;
                }
                response = new Response<MyTeamBO>(charts, 200, "Data Retraived");
            }
            catch (Exception ex)
            {
                response = new Response<MyTeamBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<EntRoles>> GetDesignation(int TenantID)
        {
            Response<List<EntRoles>> response;
            try
            {
                var result = _employeeRepository.GetDesignation(TenantID);                
                if (result.Count() == 0)
                {
                    response = new Response<List<EntRoles>>(result, 200, "Data Not Found");
                }
                else
                {
                    response = new Response<List<EntRoles>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<EntRoles>>(ex.Message, 500);
            }
            return response;
        }
        public Response<SaveOut> UpdateEmployeeDesignation(EmployeeInfoBO employee)
        {
            Response<SaveOut> response;
            try
            {
                var result = _employeeRepository.UpdateEmployeeDesignation(employee);
                if (result.Id == 0)
                {
                    response = new Response<SaveOut>(result,200,"EmployeeDesignation Data Creation or Updation is Failed");
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
        public Response<List<EmpManagerDropdown>> GetManagers(int TenantID)
        {
            Response<List<EmpManagerDropdown>> response;
            try
            {
                var result = _employeeRepository.GetManagers(TenantID,0,0);                
                if (result.Count() == 0)
                {
                    response = new Response<List<EmpManagerDropdown>>(result, 200, "Data Not Found");
                }
                else
                {
                    response = new Response<List<EmpManagerDropdown>>(result, 200, "Data Retrieved");
                }
            }
            catch (Exception ex)
            {
                response = new Response<List<EmpManagerDropdown>>(ex.Message, 500);
            }
            return response;
        }
    }
}