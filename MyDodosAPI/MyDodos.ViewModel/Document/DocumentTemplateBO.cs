using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace MyDodos.ViewModel.Document
{
public class InputDocsBO
    {
        public int DocID { get; set; }
        public int TenantID { get; set; }
        public string docName { get; set; }
        public string docsFile { get; set; }
        public decimal? docsSize { get; set; }
    }
    public class GenDocumentBO
    {
        public int DocID { get; set; }
        public string DocumentName { get; set; }
        public int RepositoryID { get; set; }
        public string DocType { get; set; }
        public int Rentention { get; set; }
        public string DocNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string DocStatus { get; set; }
        public string OrgDocName { get; set; }
        public string GenDocName { get; set; }
        public string TokenID { get; set; }
        public DateTime ValidUpto { get; set; }
        public int TenantID { get; set; }
        public string Entity { get; set; }
        public int EntityID { get; set; }
        public int DocTypeID { get; set; }
        public int LocationID { get; set; }
        public string DocKey { get; set; }
        public decimal DocSize { get; set; }
        public string DirectionPath { get; set; }

    }
    public class DocRepositoryBO
    {
        public int RepositoryID { get; set; }
        public string RepoName { get; set; }
        public string RepoPath { get; set; }
        public int OwnerID { get; set; }
        public string LocType { get; set; }
        public bool IsAccessRequired { get; set; }
        public string LocAccess_UserName { get; set; }
        public string LocAccess_Password { get; set; }
        public string LocAccess_LocToken { get; set; }
        public string RepoStatus { get; set; }
        public string OwnerType { get; set; }
        public int TenantID { get; set; }
        public int ProductID { get; set; }
        public string RepoType { get; set; }
        public int TemplateTypeId { get; set; }

    }
    public class ConfigSettingBO
    {
        public int ConfigID { get; set; }   
        public string ServiceName { get; set; } 
        public int TenantID { get; set; } 
        public int InstitutionID { get; set; } 
        public string GenerateMode { get; set; } 
        public string PrefixValue { get; set; } 
        public int StartNo { get; set; } 
        public int IncrementNo { get; set; } 
        public int FixedWidth { get; set; } 
        public string IdentifierType { get; set; } 
        public string IdentifierColumnName { get; set; } 

    }
    public class TemplateMgmtResponceBO
    {
        public bool succeeded { get; set; }
        public string message { get; set; }
        public string errors { get; set; }
        public List<TemplateMgmt> data { get; set; }
    }
    public class TemplateMgmt
    {
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public int RepositoryID { get; set; }
        public string StorageFolder { get; set; }
        public string Description { get; set; }
        public string TemplateStatus { get; set; }
        public string FileName { get; set; }
        public int TenantId { get; set; }
    }
    public class TemplateTagResponceBO
    {
        public bool succeeded { get; set; }
        public string message { get; set; }
        public string errors { get; set; }
        //public List<TagDataQueryBO> data { get; set; }
    }
    public class TemplateTagBO
    {
        public int TagID { get; set; }
        public string TagName { get; set; }
        public string TagValue { get; set; }
        public string RepeatTagName { get; set; }
        public int RepeatTagRowNumber { get; set; }
        public int DataSourceID { get; set; }
        public string DataSourceType { get; set; }
        public string DataSource { get; set; }
        public string DataSourceKeyfield { get; set; }
        public string MapFieldName { get; set; }
        public int EntityID { get; set; }
        public string EntityType { get; set; }

    }
    // public class TagDataQueryBO
    // {
    //     public int QueryID { get; set; }
    //     public string DBQuery { get; set; }
    //     public string[] ConditionFields { get; set; }
    //     public string[] FieldList { get; set; }
    //     public string dbType { get; set; }
    //     public Dictionary<string, string> oQueryParamValue { get; set; }
    // }
    public class TemplatedetailBO
    {
        public int CompanyID { get; set; }
        public int TenantID { get; set; }
        public int TemplateID { get; set; }
        public string TemplateName { get; set; }
        public string TemplateCategory { get; set; }
        public string TemplateType { get; set; }
        public string TemplateFile { get; set; }
        public bool isDefault { get; set; }
        public string FileName { get; set; }
        public string FullPath { get; set; }
        public string RepoPath { get; set; }
        public string RepoType { get; set; }
        public string Description { get; set; }
        public string TemplateStatus { get; set; }
        public int RepositoryID { get; set; }
        public string Entity { get; set; }
        public int ProductID { get; set; }
        public string StoragePath { get; set; }
        public string StorageFolder { get; set; }
        public List<TemplateTagBO> Tags { get; set; }
    }
    public class MetaBO
    {
        public string MetaName { get; set; }
        public string MetaValue { get; set; }
    }
    public enum DocRepoNameBO
    {
        [DescriptionAttribute("Curriculum Records")]
        SyllabusRecords,

        [DescriptionAttribute("Onboard Records")]
        OnBoardRec,

        [DescriptionAttribute("Onboard IDCardImages")]
        OnBoardIDCard,

        [DescriptionAttribute("Upload Employee")]
        EmpUpload,

        [DescriptionAttribute("Upload Student")]
        StudentUpload,

        [DescriptionAttribute("Upload Holiday")]
        HolidayUpload,

        [DescriptionAttribute("Upload InstitutionLogo")]
        InstitutionLogo,

        [DescriptionAttribute("Upload UserProfileImage")]
        UserProfileImage,

        [DescriptionAttribute("Upload ProfileImage")]
        ProfileImage,

        [DescriptionAttribute("Upload Class Assignments")]
        ClassAssignment,

        [DescriptionAttribute("Upload Student Assignments")]
        StudentAssignment,

    }
    public class DocProofFileBo
    {
        public string InputJson { get; set; }    
        public IFormFile File { get; set; }
    }
    
    public class DocProofFileListBo
    {
        public string InputJson { get; set; }    
        public List<IFormFile> File { get; set; }
    }
    public class DocContainerBO
    {
        public int ContainerID { get; set; }
        public string ContainerName { get; set; }
        public string ContainerStatus { get; set; }
        public int TenantID { get; set; }
        public int ProductID { get; set; }
    }
    public class TemplateDatafieldBO
    {
        public int TemplateId { get; set; }
        public int ProductId { get; set; }
        public int TenantId { get; set; }
        public string TemplateName { get; set; }
        public string TemplateDescription { get; set; }
        public string TemplateType { get; set; }
        public string TemplateCategory { get; set; }
        public string AttributeLabel { get; set; }
        public string AttributeValue { get; set; }
        public string DataFieldIds { get; set; }
        public string DataFieldName { get; set; }
        public bool IsImport { get; set; }
        public bool IsExport { get; set; }
        public string DataValidateFieldIds { get; set; }
        public string DataValidateFieldName { get; set; }
        public string IS_Upper_Case { get; set; }
        public string IS_Lower_Case { get; set; }
        public string Begin_Upper_Case { get; set; }
        public string Begin_Lower_Case { get; set; }
        public string IsException { get; set; }
        public string DataFieldRightIds { get; set; }
        public string DataFieldRightName { get; set; }
        public string DataFieldDisplayName { get; set; }
        public string DataFieldNameValues { get; set; }
        public string DataHRFieldName { get; set; }
        public int RefTemplateID { get; set; }
    }
    public class TemplateFieldBO
    {
        public int TenantID { get; set; }
        public string FieldName { get; set; }
        public int? CategoryId { get; set; }
        public int ProductId { get; set; }
        public int EntityId { get; set; }
        public bool IsRequired { get; set; }
    }
    public class TemplatexlsxBO
    {
        public int TenantID { get; set; }
        public string[] FieldName { get; set; }
        public string[] Fieldvalue { get; set; }
    }
    public class DocFileInputBO
    {
        public string InputJson { get; set; }
        public string FileName { get; set; }
        public IFormFile docs { get; set; }                
    }
}