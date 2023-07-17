using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using MyDodos.Domain.Wrapper;
using MyDodos.Domain.Document;
using MyDodos.ViewModel.Document;
using MyDodos.Repository.Document;
using System.IO;
using System.Linq;
using MyDodos.Repository.Auth;
using CsvHelper.Configuration;
using MyDodos.Service.Mapper;
using System.Globalization;
using CsvHelper;
using DocumentFormat.OpenXml.Packaging;
using ExcelMapper;
using System.Text;
using Microsoft.AspNetCore.Http;
using MyDodos.Domain.AuthBO;
using MyDodos.Repository.HR;

namespace MyDodos.Service.Document
{
    public class DocumentFileService : IDocumentFileService
    {
        private readonly IDocumentFileRepository _documentFileRepository;
        private readonly IAuthRepository _authRepository;
        private readonly IOnBoardRepository _onBoardRepository;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DocumentFileService(IDocumentFileRepository documentFileRepository, IAuthRepository authRepository, IConfiguration configuration, IHttpContextAccessor httpContextAccessor,
        IOnBoardRepository onBoardRepository)
        {
            _documentFileRepository = documentFileRepository;
            _authRepository = authRepository;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _onBoardRepository = onBoardRepository;
        }
        public Response<List<TemplatedetailBO>> GetDataTemplate(int ProductID, int TenantID, string datatemplate, string entity, int EntityID)
        {
            Response<List<TemplatedetailBO>> response;
            try
            {
                var rtndata = _documentFileRepository.GetDataTemplate(ProductID, TenantID, "Data Template", entity, EntityID);

                response = new Response<List<TemplatedetailBO>>(rtndata, 200, "Data Reterived");
            }
            catch (Exception ex)
            {
                response = new Response<List<TemplatedetailBO>>("Error : " + ex.Message, 500);
            }
            return response;
        }
        public Response<List<TemplatedetailBO>> GetDataTemplateCatogory(int ProductID, int TenantID, string TemplateCategory, string TemplateType)
        {
            Response<List<TemplatedetailBO>> response;
            try
            {
                var rtndata = _documentFileRepository.GetDataTemplateCatogory(ProductID, TenantID, TemplateCategory,TemplateType);

                response = new Response<List<TemplatedetailBO>>(rtndata, 200, "Data Reterived");
            }
            catch (Exception ex)
            {
                response = new Response<List<TemplatedetailBO>>("Error : " + ex.Message, 500);
            }
            return response;
        }
        public Response<List<TemplateDatafieldBO>> GetDatafield(int ProductID, int TenantID, int TemplateID, int LocationID)
        {
            Response<List<TemplateDatafieldBO>> response;
            try
            {
                var rtndata = _documentFileRepository.GetDatafield(ProductID, TenantID, TemplateID, LocationID);

                response = new Response<List<TemplateDatafieldBO>>(rtndata, 200, "Data Reterived");
            }
            catch (Exception ex)
            {
                response = new Response<List<TemplateDatafieldBO>>("Error : " + ex.Message, 500);
            }
            return response;
        }
        public Response<TemplatexlsxBO> GetDataFileds(int ProductID, int TenantID, int TemplateID, int LocationID)
        {
            Response<TemplatexlsxBO> response;
            try
            {
                TemplatexlsxBO rtnobj = new TemplatexlsxBO();
                var fieldlist = _documentFileRepository.GetDatafield(ProductID, TenantID, TemplateID, LocationID);
                if (fieldlist.Count > 0)
                {
                    List<string> obj = new List<string>();
                    List<string> obj1 = new List<string>();
                    var fieldname = fieldlist[0].DataFieldName.Split(",");
                    var fieldnamevalues = fieldlist[0].DataFieldNameValues.Split(",");
                    //sb.Clear();
                    if (fieldname.Count() == fieldnamevalues.Count())
                    {
                        foreach (var rtnname in fieldname)
                        {
                            obj.Add(rtnname);
                        }
                        foreach (var rtnname in fieldnamevalues)
                        {
                            obj1.Add(rtnname);
                        }
                        rtnobj.FieldName = obj.ToArray();
                        rtnobj.Fieldvalue = obj1.ToArray();
                    }
                    response = new Response<TemplatexlsxBO>(rtnobj, 400, "Please Verify DataFieldName And DataFieldNameValues");
                }

                response = new Response<TemplatexlsxBO>(rtnobj, 200, "Data Reterived");
            }
            catch (Exception ex)
            {
                response = new Response<TemplatexlsxBO>("Error : " + ex.Message, 500);
            }
            return response;
        }
        public Response<List<TemplateFieldBO>> GetDataDetails(int ProductID, int TenantID, int TemplateID)
        {
            Response<List<TemplateFieldBO>> response;
            try
            {
                var rtndata = _documentFileRepository.GetDataDetails(ProductID, TenantID, TemplateID);

                response = new Response<List<TemplateFieldBO>>(rtndata, 200, "Data Reterived");
            }
            catch (Exception ex)
            {
                response = new Response<List<TemplateFieldBO>>("Error : " + ex.Message, 500);
            }
            return response;
        }
        # region excel Upload
        public Response<DocFileReturnBO> SaveStagingDocument(DocFileInputBO docBO, StgreturnDataReferBO inputJson)
        {
            Response<DocFileReturnBO> response = new Response<DocFileReturnBO>();
            List<StgDataEmployeeBO> objrecords = new List<StgDataEmployeeBO>();
            StgreturnDataReferBO docB = new StgreturnDataReferBO();
            DocFileReturnBO rtn = new DocFileReturnBO();
            var httpRequest = _httpContextAccessor.HttpContext;
            try
            {
                foreach (var file in httpRequest.Request.Form.Files)
                {
                    if (file.Length > 0)
                    {

                        //var inputJson = JsonConvert.DeserializeObject<StageInputReferBO>(Request.Form["InputJson"]);

                        //List<TemplatedetailBO> tempobj = new List<TemplatedetailBO>();
                        var xlsFromat = Path.GetExtension(docBO.docs.FileName);
                        if (xlsFromat == ".csv")
                        {

                            var postedFile = httpRequest.Request.Form.Files[0];
                            docBO.FileName = Convert.ToString(postedFile.FileName);
                            if (string.IsNullOrEmpty(inputJson.UniqueBatchNO))
                                inputJson.UniqueBatchNO = Guid.NewGuid().ToString();

                            // tempobj = _dataservice.GetTemplate(inputJson.TenantID, inputJson.TemplateID).Data;
                            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                            {
                                HasHeaderRecord = true,
                                IgnoreBlankLines = true,
                            };

                            using (var reader = new StreamReader(postedFile.OpenReadStream()))
                            {
                                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                                {
                                    if (inputJson.TemplateID > 0)
                                    {
                                        string str = string.Empty;
                                        var dtafield = _documentFileRepository.GetDatafield(inputJson.ProductID, inputJson.TenantID, inputJson.TemplateID, inputJson.LocationID);
                                        if (dtafield.Count > 0)
                                        {
                                            var filelds_names = dtafield[0].DataFieldName;
                                            var machfileds = dtafield[0].DataHRFieldName;
                                            csv.Read();
                                            csv.ReadHeader();
                                            List<string> headers = csv.Context.Reader.HeaderRecord.ToList();
                                            str = String.Join(",", headers);
                                            if (filelds_names == str)
                                            {

                                                //var Columndata = _dataservice.GetDataDetails(docdetails.ProductID, docdetails.TenantID, docdetails.TemplateID);
                                                rtn.Msg = "Success";
                                                StringBuilder sb = new StringBuilder();
                                                var fieldname = filelds_names.Split(",");
                                                var dataxlsx = machfileds.Split(",");
                                                var replacedate = string.Empty;
                                                rtn.isMissingField = true;
                                                foreach (var field in fieldname)
                                                {

                                                    foreach (var detxlsxmap in dataxlsx)
                                                    {
                                                        if (detxlsxmap != field)
                                                        {
                                                            sb.Append(detxlsxmap);
                                                            sb.Append(", ");
                                                        }
                                                        var repstring = field.ToString();
                                                        replacedate = sb.ToString().Replace(repstring, "");
                                                    }
                                                    rtn.MissingFieldname = replacedate;
                                                }
                                            }
                                            else
                                            {
                                                response = new Response<DocFileReturnBO>(response.Data, 400, "Mismatched Column Header");
                                            }
                                        }
                                        else
                                        {
                                            response = new Response<DocFileReturnBO>(response.Data, 400, "Please Added Template Data");
                                        }
                                        if (rtn.Msg == "Success")
                                        {
                                            if (inputJson.EntityName == "Employee")
                                            {
                                                var mm = getmappingdata(inputJson, str);

                                                if (mm.Count > 0)
                                                {
                                                    var fooMap = new DefaultClassMap<StgDataEmployeeBO>();
                                                    fooMap.Map(mm);
                                                    csv.Context.RegisterClassMap(fooMap);
                                                    var records = csv.GetRecords<StgDataEmployeeBO>().ToList();

                                                    // csv.Context.RegisterClassMap<MapPatientData>();
                                                    // var records = csv.GetRecords<StgDataEmployeeBO>().ToList();
                                                    if (records.Count > 0)
                                                    {
                                                        // docB.StgDataReferID = inputJson.StgDataReferID;
                                                        //     docB.FileName = inputJson.FileName;
                                                        //     docB.Actiontype = inputJson.Actiontype;
                                                        //     docB.Identityname = inputJson.Identityname;
                                                        //     docB.TenantID = inputJson.TenantID;
                                                        //     docB.CreatedBy = inputJson.CreatedBy;
                                                        //     docB.InstitutionID = inputJson.InstitutionID;
                                                        //     docB.EntityName = inputJson.EntityName;
                                                        //     docB.TemplateID = inputJson.TemplateID;
                                                        //     docB.UniqueBatchNO = inputJson.UniqueBatchNO;
                                                        docB = inputJson;
                                                        docB.Employee = records;
                                                        if (records.Count < 100)
                                                        {
                                                            response = SaveEmployeeDoc(docB);
                                                            response.Data.MissingFieldname = rtn.MissingFieldname;
                                                            response.Data.isMissingField = rtn.isMissingField;
                                                        }
                                                        else
                                                        {
                                                            // response = _onBoardDocumentService.SaveGenDetails(docB, docBO);
                                                        }
                                                    }
                                                }

                                            }
                                            else if (inputJson.EntityName == "Holiday")
                                            {
                                               var mm = getmappingdata(inputJson, str);
                                                 if (mm.Count > 0)
                                                {
                                                    var fooMap = new DefaultClassMap<StageHolidayBO>();
                                                    fooMap.Map(mm);
                                                    csv.Context.RegisterClassMap(fooMap);
                                                    var records = csv.GetRecords<StageHolidayBO>().ToList();
                                                // csv.Context.RegisterClassMap<MapEventData>();
                                                // var records = csv.GetRecords<StageHolidayBO>().ToList();
                                                if (records.Count > 0)
                                                {
                                                    // docB.StgDataReferID = inputJson.StgDataReferID;
                                                    //     docB.FileName = inputJson.FileName;
                                                    //     docB.Actiontype = inputJson.Actiontype;
                                                    //     docB.Identityname = inputJson.Identityname;
                                                    //     docB.TenantID = inputJson.TenantID;
                                                    //     docB.CreatedBy = inputJson.CreatedBy;
                                                    //     docB.InstitutionID = inputJson.InstitutionID;
                                                    //     docB.EntityName = inputJson.EntityName;
                                                    //     docB.TemplateID = inputJson.TemplateID;
                                                    //     docB.UniqueBatchNO = inputJson.UniqueBatchNO;
                                                    docB = inputJson;
                                                    docB.Holiday = records;
                                                    if (records.Count < 50)
                                                    {
                                                        response = SaveHolidayDoc(docB);
                                                    }
                                                    else
                                                    {
                                                        // response = _onBoardDocumentService.SaveGenDetails(docB, docBO);
                                                    }
                                                }
                                                }
                                                // response = new Response<DocFileReturnBO>(response.Data, 400, "Implemented Holiday List");
                                            }
                                            else
                                            {
                                                response = new Response<DocFileReturnBO>(response.Data, 400, "Invalid Template");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (xlsFromat == ".xlsx")
                        {
                            if (docBO.docs.Length > 0)
                            {
                                var postedFile = httpRequest.Request.Form.Files[0];
                                docBO.FileName = Convert.ToString(postedFile.FileName);
                                if (string.IsNullOrEmpty(inputJson.UniqueBatchNO))
                                    inputJson.UniqueBatchNO = Guid.NewGuid().ToString();
                                // tempobj = _dataservice.GetTemplate(inputJson.TenantID, inputJson.TemplateID).Data;
                                var dtafield = _documentFileRepository.GetDatafield(inputJson.ProductID, inputJson.TenantID, inputJson.TemplateID, inputJson.LocationID);
                                var filelds_names = dtafield[0].DataFieldName;
                                using var memoryStream = new MemoryStream(new byte[docBO.docs.Length]);
                                docBO.docs.CopyToAsync(memoryStream);
                                memoryStream.Position = 0;
                                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(memoryStream, true);
                                using (var importer = new ExcelImporter(memoryStream))
                                {
                                    WorksheetPart newWorksheetPart = spreadsheetDocument.WorkbookPart.AddNewPart<WorksheetPart>();
                                    importer.Configuration.SkipBlankLines = true;
                                    ExcelSheet sheet = importer.ReadSheet();
                                    var readdata = sheet.ReadHeading();
                                    List<string> headers = sheet.Heading.ColumnNames.ToList();
                                    var str = String.Join(",", headers);
                                    if (filelds_names == str)
                                    {
                                        rtn.Msg = "Success";
                                    }
                                    else
                                    {
                                        response = new Response<DocFileReturnBO>(response.Data, 400, "Mismatched Column Header");
                                    }
                                    if (rtn.Msg == "Success")
                                    {
                                        if (inputJson.EntityName == "Employee")
                                        {
                                            // var mm = getmappingdata(inputJson, str);

                                            // if(mm.Count > 0)
                                            // {
                                            //         var fooMap = new DefaultClassMap<StgDataEmployeeBO>();
                                            //         fooMap.Map(mm);
                                            //         //importer.Configuration.RegisterClassMap(fooMap);
                                            //         var records = sheet.ReadRows<StgDataEmployeeBO>().ToList();
                                            // }
                                            importer.Configuration.RegisterClassMap<xlsxMapPatientData>();
                                            var president = sheet.ReadRows<StgDataEmployeeBO>().ToList();
                                            if (president.Count > 0)
                                            {
                                                // docB.StgDataReferID = inputJson.StgDataReferID;
                                                //     docB.FileName = inputJson.FileName;
                                                //     docB.Actiontype = inputJson.Actiontype;
                                                //     docB.Identityname = inputJson.Identityname;
                                                //     docB.TenantID = inputJson.TenantID;
                                                //     docB.CreatedBy = inputJson.CreatedBy;
                                                //     docB.InstitutionID = inputJson.InstitutionID;
                                                //     docB.EntityName = inputJson.EntityName;
                                                //     docB.TemplateID = inputJson.TemplateID;
                                                //     docB.UniqueBatchNO = inputJson.UniqueBatchNO;
                                                docB = inputJson;
                                                docB.Employee = president;
                                                if (president.Count < 100)
                                                {
                                                    response = SaveEmployeeDoc(docB);
                                                }
                                                else
                                                {
                                                    // response = _onBoardDocumentService.SaveGenDetails(docB, docBO);
                                                }
                                            }
                                        }
                                        else if (inputJson.EntityName == "Holiday")
                                        {
                                            importer.Configuration.RegisterClassMap<xlsxMapEventData>();
                                            var president = sheet.ReadRows<StageHolidayBO>().ToList();
                                            if (president.Count > 0)
                                            {
                                                docB = inputJson;
                                                docB.Holiday = president;
                                                if (president.Count < 50)
                                                {
                                                    response = SaveHolidayDoc(docB);
                                                }
                                                else
                                                {
                                                    // response = _onBoardDocumentService.SaveGenDetails(docB, docBO);
                                                }
                                            }
                                            //response = new Response<DocFileReturnBO>(response.Data, 400, "Implemented Holiday List");
                                        }
                                        else
                                        {
                                            response = new Response<DocFileReturnBO>(response.Data, 400, "Invalid Template");
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            response = new Response<DocFileReturnBO>(response.Data, 400, "Please Choose Current fromat for xlsx and xls file");
                        }
                    }
                }
                //return Ok(response);

                //response = new Response<List<TemplateFieldBO>>(rtndata, 200, "Data Reterived");
            }
            catch (Exception ex)
            {
                response = new Response<DocFileReturnBO>("Error : " + ex.Message, 500);
            }
            return response;
        }
        public Dictionary<string, string> getmappingdata(StgreturnDataReferBO data, string xlsxmap)
        {
            var dtafield = _documentFileRepository.GetDatafield(data.ProductID, data.TenantID, data.TemplateID, data.LocationID);
            var filelds_names = dtafield[0].DataFieldName;
            var fieldname = filelds_names.Split(",");
            var dataxlsx = xlsxmap.Split(",");
            Dictionary<string, string> AuthorList = new Dictionary<string, string>();
            foreach (var detxlsxmap in dataxlsx)
            {
                foreach (var field in fieldname)
                {
                    if (detxlsxmap == field)
                    {
                        AuthorList.Add(detxlsxmap, field);
                    }
                }
            }
            return AuthorList;
        }

        # endregion
        public Response<DocFileReturnBO> SaveEmployeeDoc(StgreturnDataReferBO data)
        {
            Response<DocFileReturnBO> response;
            try
            {
                DocFileReturnBO objres = new DocFileReturnBO();
                List<TemplateFieldBO> datatemp = new List<TemplateFieldBO>();
                StageCheckInputBO InputDet = new StageCheckInputBO();
                StageInputReferBO inputdeta = new StageInputReferBO();
                //inputdeta.Msg = data.Msg;
                string result = string.Empty;
                inputdeta.EntityName = data.EntityName;
                inputdeta.TenantID = data.TenantID;
                inputdeta.ProductID = data.ProductID;
                inputdeta.LocationID = data.LocationID;
                inputdeta.TemplateID = data.TemplateID;
                inputdeta.UniqueBatchNO = data.UniqueBatchNO;
                inputdeta.Identityname = data.Identityname;
                inputdeta.BatchStatus = "Active";
                result = _documentFileRepository.SaveStageDataReference(inputdeta);
                objres.UniqueBatchNO = result;
                objres.UniqueBatchNO = result;
                StringBuilder sb = new StringBuilder();
                if (!string.IsNullOrEmpty(result))
                {
                    if (data.Employee.Count > 0)
                    {
                        foreach (var Result in data.Employee)
                        {

                            if (Result.FirstName != "Test" && Result.LastName != "Test")
                            {
                                Result.TenantID = data.TenantID;
                                Result.LocationID = data.LocationID;
                                Result.UniqueBatchNO = objres.UniqueBatchNO;
                                Result.StgDataID = data.StgDataReferID;
                                Result.ProcessStatus = "Active";
                                Result.DesignationID = 0;
                                Result.ManagerID = 0;
                                Result.DepartmentID = 0;
                                Result.IsExcepations = false;
                                if (!string.IsNullOrEmpty(Result.Department))
                                {
                                    string example = Result.Department.ToLower();
                                    string trimmed = String.Concat(example.Where(c => !Char.IsWhiteSpace(c)));
                                    InputDet.EntityName = "DepartmentName";
                                    InputDet.CommonName = trimmed;
                                    InputDet.CommonID = 0;
                                    InputDet.TenantID = data.TenantID;
                                    InputDet.LocationID = data.LocationID;
                                    Result.DepartmentID = _documentFileRepository.GetStageSearchdata(InputDet);
                                }
                                // if (Result.DepartmentID > 0)
                                // {
                                if (!string.IsNullOrEmpty(Result.Designation))
                                {
                                    string example = Result.Designation.ToLower();
                                    string trimmed = String.Concat(example.Where(c => !Char.IsWhiteSpace(c)));
                                    InputDet.EntityName = "RoleName";
                                    InputDet.CommonName = trimmed;
                                    //InputDet.CommonID = Result.DepartmentID;
                                    InputDet.TenantID = data.TenantID;
                                    InputDet.LocationID = data.LocationID;
                                    Result.DesignationID = _documentFileRepository.GetStageSearchdata(InputDet);
                                }
                                if (!string.IsNullOrEmpty(Result.ManagerName))
                                {
                                    string example = Result.ManagerName.ToLower();
                                    string trimmed = String.Concat(example.Where(c => !Char.IsWhiteSpace(c)));
                                    InputDet.EntityName = "ManagerName";
                                    InputDet.CommonName = trimmed;
                                    //InputDet.CommonID = Result.DepartmentID;
                                    InputDet.TenantID = data.TenantID;
                                    InputDet.LocationID = data.LocationID;
                                    Result.ManagerID = _documentFileRepository.GetStageSearchdata(InputDet);
                                }
                                // }
                                var fieldlist = _documentFileRepository.GetDatafield(data.ProductID, data.TenantID, data.TemplateID, data.LocationID);
                                if (fieldlist.Count > 0)
                                {
                                    var fieldname = fieldlist[0].DataValidateFieldName.Split(",");
                                    sb.Clear();
                                    foreach (var rtnname in fieldname)
                                    {
                                        var prop = Result.GetType().GetProperties().ToList();
                                        foreach (var pro in prop)
                                        {
                                            if (rtnname == pro.Name)
                                            {
                                                var proval = pro.GetValue(Result);
                                                var val = Convert.ToString(proval);
                                                if (rtnname == "Department" || rtnname == "Designation") //|| rtnname == "ManagerName"
                                                {
                                                    if (Result.DepartmentID == 0 || Result.DesignationID == 0) //|| Result.ManagerID == 0
                                                    {
                                                        sb.Append(rtnname);
                                                        sb.Append(", ");
                                                    }
                                                }
                                                else if (string.IsNullOrEmpty(val))
                                                {
                                                    sb.Append(rtnname);
                                                    sb.Append(", ");
                                                }
                                            }
                                        }
                                    }
                                    if (sb.Length > 0)
                                    {
                                        var expname = sb.ToString();
                                        var name = expname.Remove(expname.Length - 1);
                                        Result.ExcepationFieldName = name;
                                        Result.IsExcepations = true;
                                    }
                                }
                                result = _documentFileRepository.SaveStageEmployee(Result);
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(result))
                    {
                        response = new Response<DocFileReturnBO>(objres, 200, "Saved Successfully");
                    }
                    else
                    {
                        response = new Response<DocFileReturnBO>(objres, 400, "Save Not Successfully");
                    }
                }
                else
                {
                    response = new Response<DocFileReturnBO>(objres, 400, "Save Not Successfully");

                }
            }
            catch (Exception ex)
            {
                response = new Response<DocFileReturnBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<DocFileReturnBO> SaveHolidayDoc(StgreturnDataReferBO data)
        {
            Response<DocFileReturnBO> response;
            try
            {
                DocFileReturnBO objres = new DocFileReturnBO();
                List<TemplateFieldBO> datatemp = new List<TemplateFieldBO>();
                StageInputReferBO inputdeta = new StageInputReferBO();
                string result = string.Empty;
                inputdeta.BatchStatus = "Active";
                result = _documentFileRepository.SaveStageDataReference(inputdeta);
                StringBuilder sb = new StringBuilder();
                if (!string.IsNullOrEmpty(result))
                {
                    if (data.Holiday.Count > 0)
                    {
                        foreach (var Result in data.Holiday)
                        {
                            Result.IsExcepations = false;
                            Result.UniqueBatchNO = result;
                            Result.LocationID = data.LocationID;
                            Result.TenantID = data.TenantID;
                            Result.StgDataID = data.StgDataReferID;
                            var fieldlist = _documentFileRepository.GetDatafield(data.ProductID, data.TenantID, data.TemplateID, data.LocationID);
                            if (fieldlist.Count > 0)
                            {
                                var fieldname = fieldlist[0].DataValidateFieldName.Split(",");
                                sb.Clear();
                                foreach (var rtnname in fieldname)
                                {
                                    var prop = Result.GetType().GetProperties().ToList();
                                    foreach (var pro in prop)
                                    {
                                        if (rtnname == pro.Name)
                                        {
                                            var proval = pro.GetValue(Result);
                                            var val = Convert.ToString(proval);

                                            if (string.IsNullOrEmpty(val))
                                            {
                                                sb.Append(rtnname);
                                                sb.Append(", ");
                                            }
                                        }
                                    }
                                }
                                if (sb.Length > 0)
                                {
                                    var expname = sb.ToString();
                                    var name = expname.Remove(expname.Length - 1);
                                    Result.StageFieldName = name;
                                    Result.IsExcepations = true;
                                }
                            }
                            result = _documentFileRepository.SaveStageHoliday(Result);
                        }
                        objres.UniqueBatchNO = result;
                    }
                    if (!string.IsNullOrEmpty(result))
                    {
                        response = new Response<DocFileReturnBO>(objres, 200, "Saved Successfully");
                    }
                    else
                    {
                        response = new Response<DocFileReturnBO>(objres, 200, "Saved Not Successfully");

                    }
                }
                else
                {
                    response = new Response<DocFileReturnBO>(objres, 200, "Saved Not Successfully");

                }
            }
            catch (Exception ex)
            {
                response = new Response<DocFileReturnBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<List<StgreturnDataReferBO>> GetStageData(StageInputReferBO input)
        {
            Response<List<StgreturnDataReferBO>> response;
            try
            {
                List<StgreturnDataReferBO> objres = new List<StgreturnDataReferBO>();
                objres = _documentFileRepository.GetHRDataReference(input);

                response = new Response<List<StgreturnDataReferBO>>(objres, 200, "Data Reterived");
            }
            catch (Exception ex)
            {
                response = new Response<List<StgreturnDataReferBO>>(ex.Message, 500);
            }
            return response;
        }
        public Response<StageSearchBO> GetAllStageSerachData(StageSearchBO input)
        {
            Response<StageSearchBO> response;
            try
            {
                StageSearchBO objres = new StageSearchBO();
                objres.StageRefList = _documentFileRepository.GetHRDataRefer(input.objStageInput.TenantID, input.objStageInput.LocationID, input.objStageInput.EntityName, input.objStageInput.UniqueBatchNO).ToList();
                if (objres.StageRefList.Count > 0)
                {
                    if (objres.StageRefList != null)
                    {
                        input.StageRefList = objres.StageRefList;
                        objres.DisplayRecords = new List<StageRetunfieldBO>();
                        if (input.objStageInput.EntityName == "Employee")
                        {
                            // foreach (var item in objres.Stage)
                            // {
                            var obj = _documentFileRepository.GetStageEmployee(input);
                            if (obj.StageRefList[0].Employee.Count > 0)
                            {
                                var fieldlist = _documentFileRepository.GetDatafield(input.objStageInput.ProductID, input.objStageInput.TenantID, input.objStageInput.TemplateID, input.objStageInput.LocationID);
                                if (fieldlist.Count > 0)
                                {
                                    var fieldname = fieldlist[0].DataFieldDisplayName.Split(",");
                                    foreach (var rtnname in fieldname)
                                    {
                                        string value = char.ToLower(rtnname[0]) + rtnname.Substring(1);
                                        objres.DisplayRecords.Add(new StageRetunfieldBO
                                        {
                                            FiledName = rtnname,
                                            MapValue = value
                                        });
                                    }
                                }
                                objres.StageRefList[0].TotalCount = obj.StageRefList[0].Employee[0].TotalCount;
                                objres.StageRefList[0].Excepations = obj.StageRefList[0].Employee[0].Excepations;
                                objres.StageRefList[0].Validdata = obj.StageRefList[0].Employee[0].Validdata;
                                objres.ServerSearchables = obj.ServerSearchables;
                                objres.StageRefList[0].Employee = obj.StageRefList[0].Employee;
                            }
                            // }
                        }
                        else if (input.objStageInput.EntityName == "Holiday")
                        {
                            var obj = _documentFileRepository.GetStageHoliday(input);
                            if (obj.StageRefList[0].Holiday.Count > 0)
                            {
                                var fieldlist = _documentFileRepository.GetDatafield(input.objStageInput.ProductID, input.objStageInput.TenantID, input.objStageInput.TemplateID, input.objStageInput.LocationID);
                                if (fieldlist.Count > 0)
                                {
                                    var fieldname = fieldlist[0].DataFieldDisplayName.Split(",");
                                    foreach (var rtnname in fieldname)
                                    {
                                        string value = char.ToLower(rtnname[0]) + rtnname.Substring(1);
                                        objres.DisplayRecords.Add(new StageRetunfieldBO
                                        {
                                            FiledName = rtnname,
                                            MapValue = value
                                        });
                                    }
                                }
                                objres.StageRefList[0].TotalCount = obj.StageRefList[0].Holiday[0].TotalCount;
                                objres.StageRefList[0].Excepations = obj.StageRefList[0].Holiday[0].Excepations;
                                objres.StageRefList[0].Validdata = obj.StageRefList[0].Holiday[0].Validdata;
                                objres.ServerSearchables = obj.ServerSearchables;
                                objres.StageRefList[0].Holiday = obj.StageRefList[0].Holiday;
                            }
                        }
                    }
                }
                response = new Response<StageSearchBO>(objres, 200, "Data Reterived");
            }
            catch (Exception ex)
            {
                response = new Response<StageSearchBO>(ex.Message, 500);
            }
            return response;
        }
        public Response<int> SaveBulkEmployee(InputBulkBO objstage)
        {
            Response<int> response;
            try
            {
                StageRequestModelMsg result = new StageRequestModelMsg();
                if (objstage.isMissingField)
                {
                var objreturn = _documentFileRepository.GetHRDataEmployee(objstage.EntityName, objstage.UniqueBatchNO, 0);

                    foreach (var objdata in objreturn)
                    {
                        objdata.ProcessStatus = "Stagging Data";                        

                        result = _documentFileRepository.SaveStageOnbordMasterData(objdata);
                        if (result.RequestID > 0)
                        {
                            
                            if (objstage.isMissingField)
                            {
                                objdata.ProcessStatus = "Active";
                            }
                                /*Error throw list to Show details*/
                                var chkdata = CheckDataTemplate(objstage, objdata);
                                if(!string.IsNullOrEmpty(chkdata.Data.ExcepationFieldName))
                                {
                                    objdata.DataHRFieldName = chkdata.Data.ExcepationFieldName;
                                    objdata.IsExcepations = chkdata.Data.IsExcepations;
                                }
                            objdata.onboardID = result.RequestID;
                            var objdetails = _documentFileRepository.SaveStageEmployeeMasterData(objdata);
                            InputAppUserBO app = new InputAppUserBO();
                            if(string.IsNullOrEmpty(objdata.Email) && objdata.DesignationID >0)
                            {
                                app.AppUserID = 0;
                                app.UserName = objdata.Email;
                                app.Password = "pass";
                                app.FirstName = objdata.FirstName;
                                app.MiddleName = objdata.MiddleName;
                                app.LastName = objdata.LastName;
                                app.CreatedBy = objdata.Createdby;
                                app.TenantID = objstage.TenantID;
                                app.ProductID = objstage.ProductID;
                                app.RoleID = objdata.DesignationID;
                                app.UserAccessType = "";
                                app.DefaultUserGroupID = 0;
                                app.PrimaryEmail = objdata.Email;
                                app.AppUserStatus = objdata.ProcessStatus;
                                var appuserdet = _authRepository.AddProfile(app);
                                var apppuserview = appuserdet.Data;
                                if (apppuserview != null)
                                {
                                    app.AppUserID = Convert.ToInt32(apppuserview.AppUser.AppUserID);
                                    app.EmpID = objdetails.RequestID;
                                    app.UserName = apppuserview.AppUser.AppUserName;
                                    app.Password = apppuserview.AppUser.AppUserPassword;
                                    app.AppUserStatus = objdata.ProcessStatus;
                                    app.PrimaryEmail = apppuserview.AppUser.AppUserName;
                                    app.UserAccessType = apppuserview.AppUser.UserType;
                                    app.AppUserID = _onBoardRepository.SaveEmployeeAppuser(app);
                                }
                            }
                            var mm2 = _documentFileRepository.SaveStageCompleted(objstage.EntityName, objstage.UniqueBatchNO, objdata.StgDataID);
                        }                        
                    }
                } 
                else
                {
                    var stage = objstage.StgDataIDs.Split(",");
                    foreach (var item in stage)
                    {
                        var objreturn = _documentFileRepository.GetHRDataEmployee(objstage.EntityName, objstage.UniqueBatchNO, Convert.ToInt32(item));
                        foreach (var objdata in objreturn)
                        {
                            objdata.ProcessStatus = "Initiated";
                            result = _documentFileRepository.SaveStageOnbordMasterData(objdata);
                            if (result.RequestID > 0)
                            {                               
                                objdata.onboardID = result.RequestID;
                                var objdetails = _documentFileRepository.SaveStageEmployeeMasterData(objdata);
                            }
                            var mm2 = _documentFileRepository.SaveStageCompleted(objstage.EntityName, objstage.UniqueBatchNO, objdata.StgDataID);
                        }
                    }
                    
                }
                if (result.RequestID == 0)
                {
                    response = new Response<int>(result.RequestID, 500, "Bulk Data Creation or Updation is Failed");
                }
                else
                {
                    response = new Response<int>(result.RequestID, 200, "Saved Successfully");
                }
            }
            catch (Exception ex)
            {
                response = new Response<int>(ex.Message, 500);
            }
            return response;
        }
        // public Response<int> SaveBulkEmployee(InputBulkBO objstage)
        // {
        //     Response<int> response;
        //     try
        //     {
        //         StageRequestModelMsg result = new StageRequestModelMsg();
                
        //         var objdetails = _documentFileRepository.SaveStageEmployeeMasterData(objdata);
                            
        //         var mm2 = _documentFileRepository.SaveStageCompleted(objstage.EntityName, objstage.UniqueBatchNO, objdata.StgDataID);                        
                    
                
        //     }
        //     catch (Exception ex)
        //     {
        //         response = new Response<int>(ex.Message, 500);
        //     }
        //     return response;
        // }
        public Response<string> SaveStageCompleted(string Entity, string UniqueNO, int StgDataID)
        {
            Response<string> response;
            try
            {
                var rtndata = _documentFileRepository.SaveStageCompleted(Entity, UniqueNO, 0);

                response = new Response<string>(rtndata, 200, "Saved Successfully");
            }
            catch (Exception ex)
            {
                response = new Response<string>("Error : " + ex.Message, 500);
            }
            return response;
        }
        //Data check for Check Missing Column details:

        public Response<RtnStageExceptionBO> CheckDataTemplate(InputBulkBO objstage, StgDataEmployeeBO objdata)
        {
            Response<RtnStageExceptionBO> response;
            try
            {
                RtnStageExceptionBO rtn = new RtnStageExceptionBO();
                        StringBuilder sb = new StringBuilder();
                        var fieldlist = _documentFileRepository.GetDatafield(objstage.ProductID, objstage.TenantID, objstage.TemplateID, objstage.LocationID);
                                if (fieldlist.Count > 0)
                                {
                                    var fieldname = fieldlist[0].DataHRFieldName.Split(",");
                                    sb.Clear();
                                    foreach (var rtnname in fieldname)
                                    {
                                        var prop = objdata.GetType().GetProperties().ToList();
                                        foreach (var pro in prop)
                                        {
                                            if (rtnname == pro.Name)
                                            {
                                                var proval = pro.GetValue(objdata);
                                                var val = Convert.ToString(proval);
                                                if (rtnname == "Department") //|| rtnname == "ManagerName"
                                                {
                                                    if (objdata.DepartmentID == 0) //|| Result.ManagerID == 0
                                                    {
                                                        sb.Append(rtnname);
                                                        sb.Append(", ");
                                                    }
                                                }
                                                if (rtnname == "Designation") //|| rtnname == "ManagerName"
                                                {
                                                    if (objdata.DesignationID == 0) //|| Result.ManagerID == 0
                                                    {
                                                        sb.Append(rtnname);
                                                        sb.Append(", ");
                                                    }
                                                }
                                                if (rtnname == "ManagerName") //|| rtnname == "ManagerName"
                                                {
                                                    if (objdata.ManagerID == 0) //|| Result.ManagerID == 0
                                                    {
                                                        sb.Append(rtnname);
                                                        sb.Append(", ");
                                                    }
                                                }
                                                else if (string.IsNullOrEmpty(val))
                                                {
                                                    sb.Append(rtnname);
                                                    sb.Append(", ");
                                                }
                                            }
                                        }
                                    }
                                    if (sb.Length > 0)
                                    {
                                        var expname = sb.ToString();
                                        var name = expname.Remove(expname.Length - 2);
                                        rtn.ExcepationFieldName = name;
                                        rtn.IsExcepations = true;
                                    }
                                }
                                response = new Response<RtnStageExceptionBO>(rtn, 200, "Saved Successfully");
            }
            catch (Exception ex)
            {
                response = new Response<RtnStageExceptionBO>("Error : " + ex.Message, 500);
            }
            return response;
        }
    }
}