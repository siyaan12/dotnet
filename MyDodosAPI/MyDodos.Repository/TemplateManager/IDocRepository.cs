using System;
using System.Collections.Generic;
using KoSoft.DocTemplate;
using MyDodos.Domain.Wrapper;

namespace MyDodos.Repository.TemplateManager
{
    public interface IDocRepository
    {
        // Response<List<DocRepositoryBO>> GetDocRepository(int TenantID, int OwnerID, string OwnerType, int ProductID, string LocType);
        // Response<List<TemplateMgmt>> GetTemplatesByTenant(int TenantID, int ProductID, string RepoName, string RepoType);
        // Response<List<TagDataQueryBO>> GetMetatag(int ProductID,int TemplateID, int RequestID, string LeaveType);
        // int SaveDocument(GenDocumentBO d1);
        // List<GenDocumentBO> GetDocument(int docId, int entityId, string docKey, string entity, int tenantId);
        // int DeleteDocument(int docId);
        // //Response<List<TemplateTagBO>> GetRapidMetaTagValues(List<TagDataQueryBO> oTagQuery);
        // string GetTemplateWithContent(string TplFileFullPath, List<KoSoft.DocTemplate.TemplateTagBO> oTagValues);
        // //string GetTemplateWithContent(string TplFileFullPath, List<TemplateTagBO> oTagValues);
        // List<DocContainerBO> GetDocContainer(int tenantId,int productId);
        Response<List<TemplateTagBO>> GetTemplateTagValues(List<TagDataQueryBO> oTagQuery);
    }
}
