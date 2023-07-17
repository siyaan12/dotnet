using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using ClosedXML.Excel;
using KoSoft.DocRepo;
using KoSoft.DocTemplate;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.AzureStorage;
using MyDodos.Domain.HR;
using MyDodos.Domain.Payroll;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.AzureStorage;
using MyDodos.Repository.HR;
using MyDodos.Repository.Payroll;

namespace MyDodos.Service.Payroll
{
    public class PayrollSlipService : IPayrollSlipService
    {
        private readonly IConfiguration _configuration;
        private readonly IStorageConnect _storage;
        private readonly IPayrollSlipRepository _payrollSlipRepositoryc;
        private readonly IPayrollMasterRepository _payrollMasterRepository;
        private readonly IPayrollRepository _payrollRepository;
        private readonly ICommonRepository _commonRepository;
        public PayrollSlipService(IConfiguration configuration, IStorageConnect storage,ICommonRepository commonRepository, IPayrollSlipRepository payrollSlipRepositoryc, IPayrollRepository payrollRepository, IPayrollMasterRepository payrollMasterRepository)
        {
            _configuration = configuration;
            _storage = storage;
            _commonRepository = commonRepository;
            _payrollSlipRepositoryc = payrollSlipRepositoryc;
            _payrollRepository = payrollRepository;
            _payrollMasterRepository = payrollMasterRepository;
        }
        public Response<RtnEPFandESIBO> DowloadECRFile(EPFandESIBO _objpayroll)
        {
            Response<RtnEPFandESIBO> response;
            try
            {
                RtnEPFandESIBO objrtn = new RtnEPFandESIBO();
                int DocID = 0;
                string objpath = string.Empty;
                SaveDocCloudBO cloud = new SaveDocCloudBO();
                var result = _payrollMasterRepository.GetPayrollEmpEPFSummary(_objpayroll.SalaryPeriodID);
                var FileName = Convert.ToString(EntityType.PayrollPFECRFile);
                DocumentDA objcontanier = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                KoTemplate tpl = new KoSoft.DocTemplate.KoTemplate(_configuration.GetConnectionString("KOPRODADBConnection"), TempDBType.MySQL);
                DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("KOPRODADBConnection"), DBType.MySQL);
                List<KoSoft.DocTemplate.TemplateBO> oTpl = tpl.GetTemplateData(Convert.ToInt32(_objpayroll.TenantID), TemplateType.PayrollTemplate, Convert.ToInt32(_configuration.GetSection("ProductID").Value), true);
                var TagS = oTpl.Where(s => s.Entity == FileName).ToList();
                var docRepo = objdoc.GetDocRepository(0, 0, DocRepoName.NONE, oTpl[0].RepositoryID);
                if (docRepo.Count > 0)
                {
                    cloud.CloudType = _configuration.GetSection("CloudType").Value;
                    cloud.Container = docRepo[0].ContainerName.ToLower();
                    cloud.fileName = TagS[0].FileName;
                    cloud.folderPath = docRepo[0].RepoPath;
                    cloud.ProductCode = _configuration.GetSection("ProductID").Value;
                    var objdata = _storage.DownloadDocument(cloud);
                    objpath = objdata.Result.DocumentURL;
                }
                WebClient wc = new WebClient();
                using (MemoryStream stream = new MemoryStream(wc.DownloadData(objpath)))
                {
                    using (var workbook = new XLWorkbook(stream))
                    {
                        if (workbook.Worksheets.Count > 0)
                        {
                            IXLWorksheet worksheet = workbook.Worksheet(1);
                            int row = 1;
                            int col = 1;

                        Found:
                            var cellV = worksheet.Cell(row, col).Value;

                            if (cellV.ToString() == "")
                            {
                                row++;
                                var cellV1 = worksheet.Cell(row, col).Value;
                                {
                                    row--;
                                    if (cellV1.ToString() == "")
                                    {
                                        foreach (var dat in result)
                                        {
                                            worksheet.Cell(row, col++).Value = dat.UANNumber;
                                            worksheet.Cell(row, col++).Value = dat.FullName;
                                            worksheet.Cell(row, col++).Value = dat.PayableBasic;
                                            worksheet.Cell(row, col++).Value = dat.PayableBasic;
                                            worksheet.Cell(row, col++).Value = dat.PayableBasic;
                                            worksheet.Cell(row, col++).Value = dat.PayableBasic;
                                            worksheet.Cell(row, col++).Value = dat.EmpShareDue;
                                            worksheet.Cell(row, col++).Value = dat.EPSScheme;
                                            worksheet.Cell(row, col++).Value = dat.EPF;
                                            worksheet.Cell(row, col++).Value = dat.NCPandLOPday;
                                            worksheet.Cell(row, col++).Value = dat.NCPandLOPday;
                                            row++;
                                            col = 1;
                                        }
                                    }
                                    else
                                    {
                                        row++;
                                        goto Found;
                                    }
                                }
                            }
                            else
                            {
                                row++;
                                goto Found;
                            }
                        }
                        using (var ms = new MemoryStream())
                        {
                            var filename = "Payroll_" + (Guid.NewGuid()).ToString("N") + ".xlsx";
                            var Repo = objdoc.GetDocRepository(_objpayroll.TenantID, _objpayroll.TenantID, "Tenant", Convert.ToInt32(_configuration.GetSection("ProductID").Value), LocationType.PayrollECR);
                            workbook.SaveAs(ms);
                            var bytes = ms.ToArray();
                            byte[] contents = ms.ToArray();
                            var fileFormat = Convert.ToBase64String(contents);

                            var objcont = objcontanier.GetDocContainer(_objpayroll.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                            
                            cloud.Container = objcont[0].ContainerName.ToLower();
                            cloud.CloudType = _configuration.GetSection("CloudType").Value;
                            cloud.ContentType = "application/vnd.ms-excel";
                            cloud.folderPath = Repo[0].RepoPath.ToLower() + "/" + DateTime.Now.Year + "_" + DateTime.Now.Month;
                            cloud.file = fileFormat;
                            cloud.fileName = filename;
                            cloud.ProductCode = _configuration.GetSection("ProductID").Value;

                            var docURLBO = _storage.SaveBulkDocumentCloud(cloud);
                            if (docURLBO.Id > 0)
                            {
                                GenDocument d1 = new GenDocument();
                                d1.DocID = DocID;
                                d1.RepositoryID = docRepo[0].RepositoryID;
                                d1.DocumentName = TagS[0].FileName;
                                d1.DocType = docRepo[0].RepoName;
                                d1.CreatedBy = _objpayroll.CreatedBy;
                                d1.OrgDocName = filename;
                                d1.GenDocName = filename;
                                d1.DocKey = (Guid.NewGuid()).ToString("N");
                                d1.DocStatus = "Completed";
                                d1.DocSize = Convert.ToDecimal(contents.Length);
                                d1.Entity = TagS[0].Entity;
                                d1.EntityID = _objpayroll.SalaryPeriodID;
                                d1.TenantID = _objpayroll.TenantID;
                                //d1.c = _objpayroll.Comments;
                                //d1.LocationID = inputJson.LocationID;
                                d1.DirectionPath = cloud.folderPath;
                                var doc = objcontanier.SaveDocument(d1);
                                objrtn.DocID = doc;
                                objrtn.DocURL = docURLBO.Result.DocumentURL;
                                objrtn.SalaryPeriodID = _objpayroll.SalaryPeriodID;
                            if (objrtn.DocID > 0)
                            {
                                StgDownloadDocBO d2 = new StgDownloadDocBO();
                                d2.DownloadDocID = 0;
                                d2.DownloadDocName = TagS[0].FileName;
                                d2.DocID = objrtn.DocID;
                                d2.RepositoryID = docRepo[0].RepositoryID;
                                d2.DocNumber = _objpayroll.Comments;
                                d2.IsDownload = _objpayroll.ischeck;
                                d2.DownloadBy = _objpayroll.CreatedBy;
                                d2.DownloadOn = _objpayroll.DownloadOn;
                                d2.DownloadDocStatus = "Completed";
                                d2.GenDocName = TagS[0].FileName;
                                d2.Entity = TagS[0].Entity;
                                d2.EntityID = _objpayroll.SalaryPeriodID;
                                d2.TenantID = _objpayroll.TenantID;
                                d2.DownloadComments = _objpayroll.Comments;
                                d2.LocationID = _objpayroll.LocationID;                                
                                var doc2 = _commonRepository.SaveStgDownloadDoc(d2);
                            }
                            }
                        }
                    }

                }
                if (objrtn.DocID == 0)
                {
                    response = new Response<RtnEPFandESIBO>(objrtn, 200, "Sponsor Data Creation or Updation is Failed");
                }
                else
                {
                    response = new Response<RtnEPFandESIBO>(objrtn, 200, "Saved Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<RtnEPFandESIBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<RtnEPFandESIBO> PayrollDowloadESIFile(EPFandESIBO _objpayroll)
        {
            Response<RtnEPFandESIBO> response;
            try
            {
                RtnEPFandESIBO objrtn = new RtnEPFandESIBO();
                int DocID = 0;
                string objpath = string.Empty;
                SaveDocCloudBO cloud = new SaveDocCloudBO();
                var result = _payrollMasterRepository.GetPayrollEmpEPFSummary(_objpayroll.SalaryPeriodID);
                var FileName = Convert.ToString(EntityType.PayrollESIFile);
                DocumentDA objcontanier = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                KoTemplate tpl = new KoSoft.DocTemplate.KoTemplate(_configuration.GetConnectionString("KOPRODADBConnection"), TempDBType.MySQL);
                DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("KOPRODADBConnection"), DBType.MySQL);
                List<KoSoft.DocTemplate.TemplateBO> oTpl = tpl.GetTemplateData(Convert.ToInt32(_objpayroll.TenantID), TemplateType.PayrollTemplate, Convert.ToInt32(_configuration.GetSection("ProductID").Value), true);
                var TagS = oTpl.Where(s => s.Entity == FileName).ToList();
                var docRepo = objdoc.GetDocRepository(0, 0, DocRepoName.NONE, oTpl[0].RepositoryID);
                if (docRepo.Count > 0)
                {
                    cloud.CloudType = _configuration.GetSection("CloudType").Value;
                    cloud.Container = docRepo[0].ContainerName.ToLower();
                    cloud.fileName = TagS[0].FileName;
                    cloud.folderPath = docRepo[0].RepoPath;
                    cloud.ProductCode = _configuration.GetSection("ProductID").Value;
                    var objdata = _storage.DownloadDocument(cloud);
                    objpath = objdata.Result.DocumentURL;
                }
                WebClient wc = new WebClient();
                using (MemoryStream stream = new MemoryStream(wc.DownloadData(objpath)))
                {
                    using (var workbook = new XLWorkbook(stream))
                    {
                        if (workbook.Worksheets.Count > 0)
                        {
                            IXLWorksheet worksheet = workbook.Worksheet(1);
                            int row = 1;
                            int col = 1;

                        Found:
                            var cellV = worksheet.Cell(row, col).Value;

                            if (cellV.ToString() == "")
                            {
                                row++;
                                var cellV1 = worksheet.Cell(row, col).Value;
                                {
                                    row--;
                                    if (cellV1.ToString() == "")
                                    {
                                        foreach (var dat in result)
                                        {
                                            worksheet.Cell(row, col++).Value = dat.UANNumber;
                                            worksheet.Cell(row, col++).Value = dat.FullName;
                                            worksheet.Cell(row, col++).Value = dat.NoOfDay;
                                            worksheet.Cell(row, col++).Value = dat.ESIEesCont + dat.ESIErsCont;
                                            worksheet.Cell(row, col++).Value = 0;
                                            worksheet.Cell(row, col++).Value = null;
                                            row++;
                                            col = 1;
                                        }
                                    }
                                    else
                                    {
                                        row++;
                                        goto Found;
                                    }
                                }
                            }
                            else
                            {
                                row++;
                                goto Found;
                            }
                            // worksheet.Row(1).Delete();
                        }
                        using (var ms = new MemoryStream())
                        {
                            var filename = "Payroll_" + (Guid.NewGuid()).ToString("N") + ".xlsx";
                            //DocumentDA objdocService = new DocumentDA(_configuration.GetConnectionString("KOPRODADBConnection"), DBType.MySQL);
                            var Repo = objdoc.GetDocRepository(_objpayroll.TenantID, _objpayroll.TenantID, "Tenant", Convert.ToInt32(_configuration.GetSection("ProductID").Value), LocationType.PayrollESI);
                            workbook.SaveAs(ms);
                            var bytes = ms.ToArray();
                            byte[] contents = ms.ToArray();
                            var fileFormat = Convert.ToBase64String(contents);

                            var objcont = objcontanier.GetDocContainer(_objpayroll.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));
                            //string TenantName = objcont[0].ContainerName;
                            cloud.Container = objcont[0].ContainerName.ToLower();
                            cloud.CloudType = _configuration.GetSection("CloudType").Value;
                            cloud.ContentType = "application/vnd.ms-excel";
                            cloud.folderPath = Repo[0].RepoPath.ToLower() + "/" + DateTime.Now.Year + "_" + DateTime.Now.Month;
                            cloud.file = fileFormat;
                            cloud.fileName = filename;
                            cloud.ProductCode = _configuration.GetSection("ProductID").Value;

                            var docURLBO = _storage.SaveBulkDocumentCloud(cloud);
                            if (docURLBO.Id > 0)
                            {
                                GenDocument d1 = new GenDocument();
                                d1.DocID = DocID;
                                d1.RepositoryID = docRepo[0].RepositoryID;
                                d1.DocumentName = TagS[0].FileName;
                                d1.DocType = docRepo[0].RepoName;
                                d1.CreatedBy = _objpayroll.CreatedBy;
                                d1.OrgDocName = filename;
                                d1.GenDocName = filename;
                                d1.DocKey = (Guid.NewGuid()).ToString("N");
                                d1.DocStatus = "Completed";
                                d1.DocSize = Convert.ToDecimal(contents.Length);
                                d1.Entity = TagS[0].Entity;
                                d1.EntityID = _objpayroll.SalaryPeriodID;
                                d1.TenantID = _objpayroll.TenantID;
                                //d1.c = _objpayroll.Comments;
                                //d1.LocationID = inputJson.LocationID;
                                d1.DirectionPath = cloud.folderPath;
                                var doc = objcontanier.SaveDocument(d1);
                                objrtn.DocID = doc;
                                objrtn.DocURL = docURLBO.Result.DocumentURL;
                                objrtn.SalaryPeriodID = _objpayroll.SalaryPeriodID;
                                if (objrtn.DocID > 0)
                            {
                                StgDownloadDocBO d2 = new StgDownloadDocBO();
                                d2.DownloadDocID = 0;
                                d2.DownloadDocName = TagS[0].FileName;
                                d2.DocID = objrtn.DocID;
                                d2.RepositoryID = docRepo[0].RepositoryID;
                                d2.DocNumber = _objpayroll.Comments;
                                d2.IsDownload = _objpayroll.ischeck;
                                d2.DownloadBy = _objpayroll.CreatedBy;
                                d2.DownloadOn = _objpayroll.DownloadOn;
                                d2.DownloadDocStatus = "Completed";
                                d2.GenDocName = TagS[0].FileName;
                                d2.Entity = TagS[0].Entity;
                                d2.EntityID = _objpayroll.SalaryPeriodID;
                                d2.TenantID = _objpayroll.TenantID;
                                d2.DownloadComments = _objpayroll.Comments;
                                d2.LocationID = _objpayroll.LocationID;                                
                                var doc2 = _commonRepository.SaveStgDownloadDoc(d2);
                            }
                            }
                        }
                    }

                }
                if (objrtn.DocID == 0)
                {
                    response = new Response<RtnEPFandESIBO>(objrtn, 200, "Sponsor Data Creation or Updation is Failed");
                }
                else
                {
                    response = new Response<RtnEPFandESIBO>(objrtn, 200, "Saved Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<RtnEPFandESIBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<RtnEPFandESIBO> DowloadECRtxtFile(EPFandESIBO _objpayroll)
        {
            Response<RtnEPFandESIBO> response;
            try
            {
                RtnEPFandESIBO objrtn = new RtnEPFandESIBO();
                int DocID = 0;
                string objpath = string.Empty;
                string objrepdata = "#~#";
                SaveDocCloudBO cloud = new SaveDocCloudBO();
                StringBuilder builder = new StringBuilder();
                var result = _payrollMasterRepository.GetPayrollEmpEPFSummary(_objpayroll.SalaryPeriodID);
                DocumentDA objcontanier = new DocumentDA(_configuration.GetConnectionString("DODOSADBConnection"), DBType.MySQL);
                DocumentDA objdoc = new DocumentDA(_configuration.GetConnectionString("KOPRODADBConnection"), DBType.MySQL);
                var docRepo = objdoc.GetDocRepository(_objpayroll.TenantID, _objpayroll.TenantID, "Tenant", Convert.ToInt32(_configuration.GetSection("ProductID").Value), LocationType.PayrollECR);

                if (result.Count > 0)
                {
                    foreach (var item in result)
                    {

                        builder.Append(item.UANNumber + objrepdata);
                        builder.Append(item.FullName + objrepdata);
                        builder.Append(Decimal.Round(item.PayableBasic) + objrepdata);
                        builder.Append(Decimal.Round(item.PayableBasic) + objrepdata);
                        builder.Append(Decimal.Round(item.PayableBasic) + objrepdata);
                        builder.Append(Decimal.Round(item.EmpShareDue) + objrepdata);
                        builder.Append(Decimal.Round(item.EPSScheme) + objrepdata);
                        builder.Append(Decimal.Round(item.EPF) + objrepdata);
                        builder.Append(Decimal.Round(item.NCPandLOPday) + objrepdata);
                        builder.Append(Decimal.Round(item.NCPandLOPday));
                        builder.Append("\n");
                    }
                    var myString = builder.ToString();
                    var myByteArray = System.Text.Encoding.UTF8.GetBytes(myString);

                    using (var ms = new MemoryStream(myByteArray))
                    {
                        var filename = "Payroll_" + (Guid.NewGuid()).ToString("N") + ".txt";
                        var Repo = objdoc.GetDocRepository(_objpayroll.TenantID, _objpayroll.TenantID, "Tenant", Convert.ToInt32(_configuration.GetSection("ProductID").Value), LocationType.PayrollECR);

                        byte[] contents = ms.ToArray();
                        var fileFormat = Convert.ToBase64String(contents);

                        var objcont = objcontanier.GetDocContainer(_objpayroll.TenantID, Convert.ToInt32(_configuration.GetSection("ProductID").Value));

                        cloud.Container = objcont[0].ContainerName.ToLower();
                        cloud.CloudType = _configuration.GetSection("CloudType").Value;
                        cloud.ContentType = "text/plain";
                        cloud.folderPath = Repo[0].RepoPath.ToLower() + "/" + DateTime.Now.Year + "_" + DateTime.Now.Month;
                        cloud.file = fileFormat;
                        cloud.fileName = filename;
                        cloud.ProductCode = _configuration.GetSection("ProductID").Value;

                        var docURLBO = _storage.SaveBulkDocumentCloud(cloud);
                        if (docURLBO.Id > 0)
                        {
                            GenDocument d1 = new GenDocument();
                            d1.DocID = DocID;
                            d1.RepositoryID = docRepo[0].RepositoryID;
                            d1.DocumentName = filename;
                            d1.DocType = docRepo[0].RepoName;
                            d1.CreatedBy = _objpayroll.CreatedBy;
                            d1.OrgDocName = filename;
                            d1.GenDocName = filename;
                            d1.DocKey = (Guid.NewGuid()).ToString("N");
                            d1.DocStatus = "Completed";
                            d1.DocSize = Convert.ToDecimal(contents.Length);
                            d1.Entity = docRepo[0].LocType;
                            d1.EntityID = _objpayroll.SalaryPeriodID;
                            d1.TenantID = _objpayroll.TenantID;
                            //d1.c = _objpayroll.Comments;
                            //d1.LocationID = inputJson.LocationID;
                            d1.DirectionPath = cloud.folderPath;
                            var doc = objcontanier.SaveDocument(d1);
                            objrtn.DocID = doc;
                            objrtn.DocURL = _storage.ReadDataInUrl(docURLBO.Result.DocumentURL);
                            objrtn.SalaryPeriodID = _objpayroll.SalaryPeriodID;
                        }
                    }
                }
                if (objrtn.DocID == 0)
                {
                    response = new Response<RtnEPFandESIBO>(objrtn, 200, "Sponsor Data Creation or Updation is Failed");
                }
                else
                {
                    response = new Response<RtnEPFandESIBO>(objrtn, 200, "Saved Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<RtnEPFandESIBO>(ex.Message, 500);
            }
            return response;
        }
    }
}