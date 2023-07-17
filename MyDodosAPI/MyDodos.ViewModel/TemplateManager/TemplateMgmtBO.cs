using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDodos.ViewModel.ServerSearch;

namespace MyDodos.ViewModel.TemplateManager
{
    public class AllTemplates
    {
        public ServerSearchable ServerSearchables { get; set; }
        public List<tblTemplateManagement> templates { get; set; }
    }
    public class tblTemplateManagement
    {
        [Key]
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string Description { get; set; }
        public string TemplateStatus { get; set; }
        public int TenantId { get; set; }
        public string TemplateCategory { get; set; }
        public string FileName { get; set; }
        public int ProductId { get; set; }
        public string TemplateType { get; set; }
        public string TplFileName { get; set; }
        public string Entity { get; set; }
        public bool IsTenantSpecific { get; set; }
        public int RepositoryID { get; set; }
        public int EntityID { get; set; }
        public string AttributeLabel { get; set; }
        public string AttributeValue { get; set; }
        [NotMapped]
        public string HtmlContent { get; set; }
    }
    public class TemplateMetaTag
    {
        [Key]
        public int TagID { get; set; }
        public int TemplateID { get; set; }
        public string TagName { get; set; }
        public string MapFieldName { get; set; }
        public int DataSourceID { get; set; }
        public string RepeatTagName { get; set; }
        [NotMapped]
        public string TagValue { get; set; }
        [NotMapped]
        public int RepeatTagRowNumber { get; set; }
    }
    public class GetCategoryList
    {
        public int ProductId { get; set; }
        public int TenantId { get; set; }
        public string TemplateType { get; set; }
    }
    public class TemplateCategory
    {
        [Key]
        public int RepositoryID { get; set; }
        public string RepoName { get; set; }
    }
    public class TemplateTypeVW
    {
        public int TemplateTypeID { get; set; }
        public string TemplateType { get; set; }
    }
    public class TemplatePath
    {
        public string RepoPath { get; set; }
        public string RepoName { get; set; }
        public string FileName { get; set; }
        public string HtmlContent { get; set; }
        public int TenantID { get; set; }
        public string storageURL { get; set; }
        public string Message { get; set; }
    }
    public class TemplateAttribute
    {
        public int AttriID { get; set; }
        public int ProductID { get; set; }
        public string AttriName { get; set; }
        public string AttriAPI { get; set; }
        public string APIParam { get; set; }
        public bool AttriValstatus { get; set; }
        public string APIUrl { get; set; }
        public string AttriListData { get; set; }
    }
    public class vwInputTemp
    {
        public string TemplateStatus { get; set; }
        public int TemplateId { get; set; }
    }
}
