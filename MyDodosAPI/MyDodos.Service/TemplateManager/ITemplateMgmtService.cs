using System.Collections.Generic;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.TemplateManager;

namespace MyDodos.Service.TemplateManager
{
    public interface ITemplateMgmtService
    {
        Response<AllTemplates> GetAllTemplate(AllTemplates allTemplates);
        Response<tblTemplateManagement> CreateTemplates(tblTemplateManagement template);
        Response<List<TemplateMetaTag>> GetMetaTagByTemplateId(int RepoId);
        Response<List<TemplateCategory>> GetCategoryList(GetCategoryList categoryList);
        Response<List<TemplateTypeVW>> GetTemplateType(int ProductId, int TenantId);
        Response<TemplatePath> GetTemplateFile(int TemplateId, int ProductId);
        Response<List<TemplateAttribute>> GetAttributeList(int ProductId);
        Response<string> DeleteTemplate(int TemplateId);
        Response<string> UpdateTemplateStatus(vwInputTemp objtemp);
    }
}