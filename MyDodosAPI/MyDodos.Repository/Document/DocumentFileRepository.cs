using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Document;
using MyDodos.ViewModel.Document;
using MySql.Data.MySqlClient;

namespace MyDodos.Repository.Document
{
    public class DocumentFileRepository : IDocumentFileRepository
    {
        private readonly IConfiguration _configuration;
        public DocumentFileRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("DODOSADBConnection"));
            return conn;
        }
        public IDbConnection GetServiceConnection()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("KOPRODADBConnection"));
            return conn;
        }
        public List<TemplatedetailBO> GetDataTemplate(int ProductID, int TenantID, string datatemplate, string entity, int EntityID)
        {
           List<TemplatedetailBO> obj = new List<TemplatedetailBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Product_id", ProductID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Tenant_Id", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Institution_ID", EntityID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Template_Category", entity, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Template_Type", datatemplate, DbType.String, ParameterDirection.Input);
            var conn = this.GetServiceConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetAllTemplate";
                obj = SqlMapper.Query<TemplatedetailBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();                
            }
            conn.Close();
            conn.Dispose();
            return obj;
        }       
        public List<TemplatedetailBO> GetDataTemplateCatogory(int ProductID, int TenantID, string TemplateCategory, string TemplateType)
        {
           List<TemplatedetailBO> obj = new List<TemplatedetailBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Productid", ProductID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@TenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@TemplateCategory", TemplateCategory, DbType.String, ParameterDirection.Input);
            dyParam.Add("@TemplateType", TemplateType, DbType.String, ParameterDirection.Input);
            var conn = this.GetServiceConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "spGetAllTemplate";
                obj = SqlMapper.Query<TemplatedetailBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();                
            }
            conn.Close();
            conn.Dispose();
            return obj;
        }
        public List<TemplateDatafieldBO> GetDatafield(int ProductID, int TenantID,int Template_ID, int LocationID)
        {
            List<TemplateDatafieldBO> obj = new List<TemplateDatafieldBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@Product_ID", ProductID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Tenant_ID", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Template_ID", Template_ID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@Institution_ID", LocationID, DbType.String, ParameterDirection.Input);
            var conn = this.GetServiceConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetValidFieldIds";
                obj = SqlMapper.Query<TemplateDatafieldBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();                
            }
            conn.Close();
            conn.Dispose();
            return obj;
        }
        public List<TemplateFieldBO> GetDataDetails(int ProductID, int TenantID, int TemplateID)
        {
           
           List<TemplateFieldBO> obj = new List<TemplateFieldBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@ProductID", ProductID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@TenantID", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@TemplateID", TemplateID, DbType.String, ParameterDirection.Input);
            dyParam.Add("@Isspecifc", 0, DbType.Boolean, ParameterDirection.Input);
            var conn = this.GetServiceConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetDataFields";
                obj = SqlMapper.Query<TemplateFieldBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            conn.Close();
            conn.Dispose();
            return obj;
        }        
        
        //Get Call
        public int GetStageSearchdata(StageCheckInputBO _check)
        {
           
            int obj = 0;
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", _check.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@entityId", _check.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@commonId", _check.CommonID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@commonName", _check.CommonName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@entityName", _check.EntityName, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)

            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {

                var query = "sp_GetStageSearchData";
                obj = SqlMapper.Query<int>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();

            }
            conn.Close();
            conn.Dispose();
            return obj;
        }
        public string SaveStageDataReference(StageInputReferBO data)
        {
            string rtnval = string.Empty;
                DynamicParameters dyParam = new DynamicParameters();
                dyParam.Add("@stgDataReferId", data.StgDataReferID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@entityName", data.EntityName, DbType.String, ParameterDirection.Input);
                dyParam.Add("@tenantId", data.TenantID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@uniqueBatchNo", data.UniqueBatchNO, DbType.String, ParameterDirection.Input);
                dyParam.Add("@locationId", data.LocationID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@fileName", data.FileName, DbType.String, ParameterDirection.Input);
                dyParam.Add("@identityName", data.Identityname, DbType.String, ParameterDirection.Input);
                dyParam.Add("@templateId", data.TemplateID, DbType.Int32, ParameterDirection.Input);                
                dyParam.Add("@actionType", data.Actiontype, DbType.String, ParameterDirection.Input);
                dyParam.Add("@batchStatus", data.BatchStatus, DbType.String, ParameterDirection.Input);
                var conn = this.GetConnection();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (conn.State == ConnectionState.Open)
                {
                    var query = "sp_SaveStageDataReference";
                    rtnval = SqlMapper.Query<string>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
                conn.Close();
                conn.Dispose();
            return rtnval;
        }
        public List<StgreturnDataReferBO> GetHRDataRefer(int  TenantID,int LocationID, string Entity, string UniqueNO)
        {         
            List<StgreturnDataReferBO> employee = new List<StgreturnDataReferBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@entirtyName", Entity, DbType.String, ParameterDirection.Input);
            dyParam.Add("@uniqueNO", UniqueNO, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetStageBatchReference";
                employee = SqlMapper.Query<StgreturnDataReferBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                
            }
            conn.Close();
            conn.Dispose();
            return employee;
        }
        public List<StgreturnDataReferBO> GetHRDataReference(StageInputReferBO input)
        {         
            List<StgreturnDataReferBO> employee = new List<StgreturnDataReferBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", input.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", input.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@entirtyName", input.EntityName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@uniqueNO", input.UniqueBatchNO, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetStageBatchReference";
                employee = SqlMapper.Query<StgreturnDataReferBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                
            }
            conn.Close();
            conn.Dispose();
            return employee;
        }
        // Batch Data
        public StageSearchBO GetStageEmployee(StageSearchBO input)
        {         
            StageSearchBO employee = new StageSearchBO();
            StgreturnDataReferBO stage = new StgreturnDataReferBO();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", input.objStageInput.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", input.objStageInput.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@entityName", input.objStageInput.EntityName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@uniqueNO", input.objStageInput.UniqueBatchNO, DbType.String, ParameterDirection.Input);
            dyParam.Add("@search_data", input.ServerSearchables.search_data, DbType.String, ParameterDirection.Input);
            dyParam.Add("@page_No", input.ServerSearchables.page_No, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@page_Size", input.ServerSearchables.page_Size, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@orderBy_Column", input.ServerSearchables.orderBy_Column, DbType.String, ParameterDirection.Input);
            dyParam.Add("@order_By", input.ServerSearchables.order_By, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetStageAllData";
                var data = SqlMapper.Query<StgDataEmployeeBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                //stage.Employeedata = data;
                employee.StageRefList = input.StageRefList;
                employee.StageRefList[0].Employee = data;
                employee.objStageInput = input.objStageInput;
                employee.ServerSearchables = input.ServerSearchables;
                if(employee.StageRefList[0].Employee.Count > 0)
                {
                    employee.ServerSearchables.RecordsTotal = employee.StageRefList[0].Employee[0].SearchTotalCount;
                }
                
            }
            conn.Close();
            conn.Dispose();
            return employee;
        }
        public StageSearchBO GetStageHoliday(StageSearchBO input)
        {         
            StageSearchBO employee = new StageSearchBO();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@tenantId", input.objStageInput.TenantID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", input.objStageInput.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@entirtyName", input.objStageInput.EntityName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@uniqueNO", input.objStageInput.UniqueBatchNO, DbType.String, ParameterDirection.Input);
            dyParam.Add("@search_data", input.ServerSearchables.search_data, DbType.String, ParameterDirection.Input);
            dyParam.Add("@page_No", input.ServerSearchables.page_No, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@page_Size", input.ServerSearchables.page_Size, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@orderBy_Column", input.ServerSearchables.orderBy_Column, DbType.String, ParameterDirection.Input);
            dyParam.Add("@order_By", input.ServerSearchables.order_By, DbType.String, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetStageAllData";
                var data = SqlMapper.Query<StageHolidayBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                employee.StageRefList = input.StageRefList;
                employee.StageRefList[0].Holiday = data;
                //employee.objStageInput = input.objStageInput;
                employee.ServerSearchables = input.ServerSearchables;
                if(employee.StageRefList[0].Holiday.Count > 0)
                {
                    employee.ServerSearchables.RecordsTotal = employee.StageRefList[0].Holiday[0].SearchTotalCount;
                }
            }
            conn.Close();
            conn.Dispose();
            return employee;
        }
        public List<StgDataEmployeeBO> GetHRDataEmployee(string Entity,  string UniqueNO, int StgDataID)
        {         
            List<StgDataEmployeeBO> employee = new List<StgDataEmployeeBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@uniqueNo", UniqueNO, DbType.String, ParameterDirection.Input);
            dyParam.Add("@entityName", Entity, DbType.String, ParameterDirection.Input);
            dyParam.Add("@stgDataId", StgDataID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetStageAllBatchReference";
                employee = SqlMapper.Query<StgDataEmployeeBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                
            }
            conn.Close();
            conn.Dispose();
            return employee;
        }
        public List<StageHolidayBO> GetHRDataHoliday(string Entity,  string UniqueNO, int StgDataID)
        {         
            List<StageHolidayBO> employee = new List<StageHolidayBO>();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@uniqueNo", UniqueNO, DbType.String, ParameterDirection.Input);
            dyParam.Add("@entityName", Entity, DbType.String, ParameterDirection.Input);
            dyParam.Add("@stgDataId", StgDataID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_GetStageAllBatchReference";
                employee = SqlMapper.Query<StageHolidayBO>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).ToList();
                
            }
            conn.Close();
            conn.Dispose();
            return employee;
        }
       //Save call details
        public string SaveStageEmployee(StgDataEmployeeBO data)
        {
            string rtnval = string.Empty;
                DynamicParameters dyParam = new DynamicParameters();
                dyParam.Add("@stgDataId", data.StgDataID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@employeeId", data.EmployeeID, DbType.String, ParameterDirection.Input);
                dyParam.Add("@prefix", data.Prefix, DbType.String, ParameterDirection.Input);
                dyParam.Add("@firstName", data.FirstName, DbType.String, ParameterDirection.Input);
                dyParam.Add("@middleName", data.MiddleName, DbType.String, ParameterDirection.Input);
                dyParam.Add("@lastName", data.LastName, DbType.String, ParameterDirection.Input);
                dyParam.Add("@gender", data.Gender, DbType.String, ParameterDirection.Input);
                dyParam.Add("@maritalStatus", data.MaritalStatus, DbType.String, ParameterDirection.Input);
                dyParam.Add("@dateOfBirth", data.DateofBirth, DbType.Date, ParameterDirection.Input);
                dyParam.Add("@contactNo", data.ContactNo, DbType.String, ParameterDirection.Input);
                dyParam.Add("@email", data.Email, DbType.String, ParameterDirection.Input);
                dyParam.Add("@resident", data.Resident, DbType.String, ParameterDirection.Input);
                dyParam.Add("@nonResident", data.NonResident, DbType.String, ParameterDirection.Input);
                dyParam.Add("@bloodGroup", data.BloodGroup, DbType.String, ParameterDirection.Input);
                dyParam.Add("@designation", data.Designation, DbType.String, ParameterDirection.Input);
                dyParam.Add("@department", data.Department, DbType.String, ParameterDirection.Input);
                dyParam.Add("@nationality", data.Nationality, DbType.String, ParameterDirection.Input);                
                dyParam.Add("@city", data.City, DbType.String, ParameterDirection.Input);
                dyParam.Add("@state", data.State, DbType.String, ParameterDirection.Input);
                dyParam.Add("@country", data.Country, DbType.String, ParameterDirection.Input);
                dyParam.Add("@zipCode", data.ZipCode, DbType.String, ParameterDirection.Input);
                dyParam.Add("@dateOfAppoinment", data.DateofAppoinment, DbType.Date, ParameterDirection.Input);
                dyParam.Add("@dateofOffer", data.DateofOffer, DbType.Date, ParameterDirection.Input);
                dyParam.Add("@dateofJoining", data.DateofJoining, DbType.Date, ParameterDirection.Input);
                dyParam.Add("@dateofAcceptance", data.DateofAcceptance, DbType.Date, ParameterDirection.Input);
                dyParam.Add("@subject", data.Subject, DbType.String, ParameterDirection.Input);
                dyParam.Add("@govtID", data.GovtID, DbType.String, ParameterDirection.Input);
                dyParam.Add("@address1", data.Address1, DbType.String, ParameterDirection.Input);
                dyParam.Add("@address2", data.Address2, DbType.String, ParameterDirection.Input);
                dyParam.Add("@ethnicity", data.Ethnicity, DbType.String, ParameterDirection.Input);
                dyParam.Add("@salaryDetails", data.SalaryDetails, DbType.String, ParameterDirection.Input);
                dyParam.Add("@experineceCompanyName", data.ExperineceCompanyName, DbType.String, ParameterDirection.Input);
                dyParam.Add("@experineceDesignation", data.ExperineceDesignation, DbType.String, ParameterDirection.Input);
                dyParam.Add("@experineceDateOfJoining", data.ExperineceDateofJoining, DbType.Date, ParameterDirection.Input);
                dyParam.Add("@experineceDateOfRelieving", data.ExperineceDateofRelieving, DbType.Date, ParameterDirection.Input);
                dyParam.Add("@qualificationSchool", data.QualificationSchool, DbType.String, ParameterDirection.Input);
                dyParam.Add("@qualificationDiploma", data.QualificationDiploma, DbType.String, ParameterDirection.Input);
                dyParam.Add("@qualificationDegree", data.QualificationDegree, DbType.String, ParameterDirection.Input);
                dyParam.Add("@departmentId", data.DepartmentID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@designationId", data.DesignationID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@managerId", data.ManagerID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@tenantId", data.TenantID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@locationId", data.LocationID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@isExcepations", data.IsExcepations, DbType.Boolean, ParameterDirection.Input);
                dyParam.Add("@stageFieldName", data.StageFieldName, DbType.String, ParameterDirection.Input);
                dyParam.Add("@excepationFieldName", data.ExcepationFieldName, DbType.String, ParameterDirection.Input);
                dyParam.Add("@uniqueBatchNo", data.UniqueBatchNO, DbType.String, ParameterDirection.Input);
                dyParam.Add("@processStatus", data.ProcessStatus, DbType.String, ParameterDirection.Input);
                var conn = this.GetConnection();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (conn.State == ConnectionState.Open)
                {
                    var query = "sp_SaveStageEmployee";
                    rtnval = SqlMapper.Query<string>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
                conn.Close();
                conn.Dispose();
            return rtnval;
        }        
        public string SaveStageHoliday(StageHolidayBO data)
        {
            string rtnval = string.Empty;
                DynamicParameters dyParam = new DynamicParameters();
                dyParam.Add("@stgDataId", data.StgDataID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@holidayName", data.HolidayName, DbType.String, ParameterDirection.Input);
                dyParam.Add("@holidayDate", data.HolidayDate, DbType.Date, ParameterDirection.Input);
                dyParam.Add("@description", data.Description, DbType.String, ParameterDirection.Input);
                dyParam.Add("@holidayType", data.HolidayType, DbType.String, ParameterDirection.Input);
                dyParam.Add("@tenantId", data.TenantID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@locationId", data.LocationID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@uniqueBatchNo", data.UniqueBatchNO, DbType.String, ParameterDirection.Input);
                dyParam.Add("@IsExcepations", data.IsExcepations, DbType.Boolean, ParameterDirection.Input);
                dyParam.Add("@stageFieldName", data.StageFieldName, DbType.String, ParameterDirection.Input);
                var conn = this.GetConnection();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (conn.State == ConnectionState.Open)
                {
                    var query = "sp_SaveStageHoliday";
                    rtnval = SqlMapper.Query<string>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
                conn.Close();
                conn.Dispose();
            return rtnval;
        }
        /*Import to Main table Save data*/
        public StageRequestModelMsg SaveStageOnbordMasterData(StgDataEmployeeBO onboarding)
        {
            StageRequestModelMsg rtnval = new StageRequestModelMsg();
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@empOnboardingId", 0, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@requestNumber", onboarding.EmployeeID, DbType.String, ParameterDirection.Input);
            dyParam.Add("@requestSource", "", DbType.String, ParameterDirection.Input);
            dyParam.Add("@firstName", onboarding.FirstName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@middleName", onboarding.MiddleName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@lastName", onboarding.LastName, DbType.String, ParameterDirection.Input);
            dyParam.Add("@requestStatus", onboarding.ProcessStatus, DbType.String, ParameterDirection.Input);
            dyParam.Add("@requestDepId", onboarding.DepartmentID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@roleId", onboarding.DesignationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@tenantId", onboarding.TenantID, DbType.Int32, ParameterDirection.Input);            
            dyParam.Add("@offerDate", onboarding.DateofOffer, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@acceptanceDate", onboarding.DateofAcceptance, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@joiningDate", onboarding.DateofJoining, DbType.DateTime, ParameterDirection.Input);
            dyParam.Add("@reqEmpId", onboarding.ManagerID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@empId", 0, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@hrInchargeId", 0, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@locationId", onboarding.LocationID, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@countryId", 0, DbType.String, ParameterDirection.Input);
            dyParam.Add("@groupTypeId", 0, DbType.Int32, ParameterDirection.Input);
            dyParam.Add("@groupType", "", DbType.String, ParameterDirection.Input);
            dyParam.Add("@createdBy", onboarding.Createdby, DbType.Int32, ParameterDirection.Input);
            
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_SaveStageOnbordMasterData";
                
                rtnval = SqlMapper.Query<StageRequestModelMsg>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            conn.Close();
            conn.Dispose();            
            return rtnval;
        }
        public StageRequestModelMsg SaveStageEmployeeMasterData(StgDataEmployeeBO onboarding)
        {
            StageRequestModelMsg rtnval = new StageRequestModelMsg();
                DynamicParameters dyParam = new DynamicParameters();
                dyParam.Add("@empOnboardingId", onboarding.onboardID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@empId", 0, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@gender", onboarding.Gender, DbType.String, ParameterDirection.Input);
                dyParam.Add("@dOB", onboarding.DateofBirth, DbType.DateTime, ParameterDirection.Input);
                dyParam.Add("@officeEmail", "", DbType.String, ParameterDirection.Input);
                dyParam.Add("@personalMail", onboarding.Email, DbType.String, ParameterDirection.Input);
                dyParam.Add("@nationality", onboarding.Nationality, DbType.String, ParameterDirection.Input);
                dyParam.Add("@empType", "Full Time", DbType.String, ParameterDirection.Input);
                dyParam.Add("@maritalStatus", onboarding.MaritalStatus, DbType.String, ParameterDirection.Input);
                dyParam.Add("@officeContactNumber", "", DbType.String, ParameterDirection.Input);
                dyParam.Add("@emerContactNumber", "", DbType.String, ParameterDirection.Input);
                dyParam.Add("@emerRelationShip", "", DbType.String, ParameterDirection.Input);
                dyParam.Add("@bloodGroup", onboarding.BloodGroup, DbType.String, ParameterDirection.Input);
                dyParam.Add("@suffix", onboarding.Prefix, DbType.String, ParameterDirection.Input);
                dyParam.Add("@mobileNo", onboarding.ContactNo, DbType.String, ParameterDirection.Input);
                dyParam.Add("@empStatus", onboarding.ProcessStatus, DbType.String, ParameterDirection.Input);
                dyParam.Add("@stgDataId", onboarding.StgDataID, DbType.Int32, ParameterDirection.Input);
                dyParam.Add("@hrNotification", onboarding.DataHRFieldName, DbType.String, ParameterDirection.Input);
                dyParam.Add("@createdBy", onboarding.Createdby, DbType.Int32, ParameterDirection.Input);
                var conn = this.GetConnection();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (conn.State == ConnectionState.Open)
                {
                    var query = "sp_SaveStageEmployeeMasterData";
                    rtnval = SqlMapper.Query<StageRequestModelMsg>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
                conn.Close();
                conn.Dispose();
            return rtnval;
        }
        public string SaveStageCompleted(string Entity,  string UniqueNO, int StgDataID)
        {         
            string employee = string.Empty;
            DynamicParameters dyParam = new DynamicParameters();
            dyParam.Add("@uniqueNo", UniqueNO, DbType.String, ParameterDirection.Input);
            dyParam.Add("@entityName", Entity, DbType.String, ParameterDirection.Input);
            dyParam.Add("@stgDataId", StgDataID, DbType.Int32, ParameterDirection.Input);
            var conn = this.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "sp_StageCompleted";
                employee = SqlMapper.Query<string>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
                
            }
            conn.Close();
            conn.Dispose();
            return employee;
        }
    }
}