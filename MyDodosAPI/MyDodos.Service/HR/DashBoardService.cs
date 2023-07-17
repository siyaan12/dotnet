using Microsoft.Extensions.Configuration;
using MyDodos.Domain.HR;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.HR;
using MyDodos.Repository.Employee;
using MyDodos.Repository.ProjectManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KoSoft.DocRepo;
using MyDodos.Repository.AzureStorage;
using MyDodos.Domain.AzureStorage;

namespace MyDodos.Service.HR
{
    public class DashBoardService : IDashBoardService
    {
        private readonly IConfiguration _configuration;
        private readonly IDashBoardRepository _dashBoardRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IStorageConnect _storage;
        public DashBoardService(IConfiguration configuration, IDashBoardRepository dashBoardRepository, IEmployeeRepository employeeRepository, IProjectRepository projectRepository,IStorageConnect storage)
        {
            _configuration = configuration;
            _dashBoardRepository = dashBoardRepository;
            _employeeRepository = employeeRepository;
            _projectRepository = projectRepository;
             _storage = storage;
        }
        public Response<DashBoard> GetDashBoardList(int TenantID, int LocationID, int YearID)
        {
            Response<DashBoard> response;
            try
            {
                DashBoard list = new DashBoard();
                list.EmployeeSummary = _dashBoardRepository.GetEmployeeDashBoardSummary(TenantID,LocationID,0);
                list.AttendanceSummary = _dashBoardRepository.GetAttendanceDashBoardSummary(TenantID,LocationID,0);
                list.TodayBirthday = _dashBoardRepository.GetUpcomingBirthday(TenantID,LocationID,true);
                foreach(var item in list.TodayBirthday)
                {
                    item.base64images = "";
                    DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                    var docs = objdoc.GetDocument(0, item.EmpID, "", "ProfileImage", TenantID);
                    if (docs.Count > 0)
                    {
                        var objcont = objdoc.GetDocContainer(TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                        var doc = _storage.DownloadDocument(new SaveDocCloudBO
                        {
                            CloudType = _configuration.GetSection("CloudType").Value,
                            Container = objcont[0].ContainerName.ToLower(),
                            fileName = docs[0].GenDocName,
                            folderPath = docs[0].DirectionPath,
                            ProductCode = _configuration.GetSection("ProductID").Value
                        }).Result;
                        item.base64images = doc.DocumentURL;
                    }
                }
                list.UpcomingBirthdays = _dashBoardRepository.GetUpcomingBirthday(TenantID,LocationID,false);
                foreach(var item in list.UpcomingBirthdays)
                {
                    item.base64images = "";
                    DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                    var docs = objdoc.GetDocument(0, item.EmpID, "", "ProfileImage", TenantID);
                    if (docs.Count > 0)
                    {
                        var objcont = objdoc.GetDocContainer(TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                        var doc = _storage.DownloadDocument(new SaveDocCloudBO
                        {
                            CloudType = _configuration.GetSection("CloudType").Value,
                            Container = objcont[0].ContainerName.ToLower(),
                            fileName = docs[0].GenDocName,
                            folderPath = docs[0].DirectionPath,
                            ProductCode = _configuration.GetSection("ProductID").Value
                        }).Result;
                        item.base64images = doc.DocumentURL;
                    }
                }
                list.Events = _dashBoardRepository.GetDashBoardEvents(TenantID,LocationID,YearID);
                list.EmployeeLists = _dashBoardRepository.GetEmployeeList(TenantID,LocationID,0);
                foreach(var item in list.EmployeeLists)
                {
                    item.base64images = "";
                    DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                    var docs = objdoc.GetDocument(0, item.EmpID, "", "ProfileImage", TenantID);
                    if (docs.Count > 0)
                    {
                        var objcont = objdoc.GetDocContainer(TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                        var doc = _storage.DownloadDocument(new SaveDocCloudBO
                        {
                            CloudType = _configuration.GetSection("CloudType").Value,
                            Container = objcont[0].ContainerName.ToLower(),
                            fileName = docs[0].GenDocName,
                            folderPath = docs[0].DirectionPath,
                            ProductCode = _configuration.GetSection("ProductID").Value
                        }).Result;
                        item.base64images = doc.DocumentURL;
                    }
                }
                list.ProjectSummary = _dashBoardRepository.GetProjectDashboardSummery(TenantID,LocationID);
                list.Project = _projectRepository.GetProjectLists(TenantID,LocationID,0);
                // foreach(var item in list.Project)
                // {
                //     var manager = _employeeRepository.GetProjectmanager(0,item.ProjectID,item.IsProjectManager = true);
                //     item.ProjectManager = manager;
                //     var colleagues = _employeeRepository.GetProjectmanager(0,item.ProjectID,item.IsProjectManager = false);
                //     item.TeamMembers = colleagues;
                // }
                response = new Response<DashBoard>(list, 200, "Data Retraived");
            }
            catch (Exception ex)
            {
                response = new Response<DashBoard>(ex.Message, 500);
            }
            return response;
        }
    }
}