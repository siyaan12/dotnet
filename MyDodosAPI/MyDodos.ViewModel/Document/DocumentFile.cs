using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using MyDodos.Domain.Document;
using MyDodos.ViewModel.ServerSearch;

namespace MyDodos.ViewModel.Document
{
public class StageCheckInputBO
    {
        public int LocationID { get; set; }
        public int TenantID { get; set; }        
        public int CommonID { get; set; }
        public string CommonName { get; set; }
        public string EntityName { get; set; }
    }
    public class StageInputReferBO
    {
        public int StgDataReferID { get; set; }
        public string EntityName { get; set; }
        public int TenantID { get; set; }
        public int ProductID { get; set; }
        public int LocationID { get; set; }
        public int TemplateID { get; set; }
        public string UniqueBatchNO { get; set; }
        public string Actiontype { get; set; }
        public string Identityname { get; set; }
        public string FileName { get; set; }
        public string BatchStatus { get; set; }
        public string Msg { get; set; }    
    }
    public class DocFileReturnBO
    {
        public string FileName { get; set; }
        public string UniqueBatchNO { get; set; }
        public string Msg { get; set; }
        public string MissingFieldname { get; set; }
        public bool isMissingField { get; set; }    
    }
    public class StageSearchBO
    {
        public StageInputReferBO objStageInput { get; set; }
        public ServerSearchable ServerSearchables { get; set; }
        public List<StageRetunfieldBO> DisplayRecords { get; set; }
        public List<StgreturnDataReferBO> StageRefList { get; set; }
    }
    public class StgreturnDataReferBO
    {
        public int StgDataReferID { get; set; }
        public string EntityName { get; set; }        
        public int TenantID { get; set; }
        public int ProductID { get; set; }
        public int LocationID { get; set; }
        public string Actiontype { get; set; }
        public string Identityname { get; set; }
        public string UniqueBatchNO { get; set; }
        public int TotalCount { get; set; }
        public int Validdata { get; set; }
        public int Excepations { get; set; }
        public int TemplateID { get; set; }
        public int CourseID { get; set; }
        public int BatchID { get; set; }
        public int TermID { get; set; }
        public List<StgDataEmployeeBO> Employee { get; set; }
        public List<StageHolidayBO> Holiday { get; set; }
    }
    public class InputBulkBO
    {
        public int StgDataID { get; set; }
        public int TenantID { get; set; }
        public int ProductID { get; set; }
        public int LocationID { get; set; }
        public int TemplateID { get; set; }
        public int CreatedBy { get; set; }
        public string UniqueBatchNO { get; set; }
        public bool isMissingField { get; set; }
        public string EntityName { get; set; }        
        public string StgDataIDs { get; set; }
    }
    public class RtnStageExceptionBO
    {
        public bool IsExcepations { get; set; }
        public string ExcepationFieldName { get; set; }
    }
}