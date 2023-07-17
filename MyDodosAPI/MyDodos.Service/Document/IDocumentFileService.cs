using System.Collections.Generic;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.Document;

namespace MyDodos.Service.Document
{
    public interface IDocumentFileService
    {
        Response<List<TemplatedetailBO>> GetDataTemplate(int ProductID, int TenantID, string datatemplate, string entity, int EntityID);
        Response<List<TemplatedetailBO>> GetDataTemplateCatogory(int ProductID, int TenantID, string TemplateCategory, string TemplateType);
        Response<List<TemplateDatafieldBO>> GetDatafield(int ProductID, int TenantID, int TemplateID, int LocationID);
        Response<TemplatexlsxBO> GetDataFileds(int ProductID, int TenantID, int TemplateID, int LocationID);
        Response<List<TemplateFieldBO>> GetDataDetails(int ProductID, int TenantID, int TemplateID);
        Response<DocFileReturnBO> SaveStagingDocument(DocFileInputBO docBO, StgreturnDataReferBO inputJson);
        Response<List<StgreturnDataReferBO>> GetStageData(StageInputReferBO input);
        Response<StageSearchBO> GetAllStageSerachData(StageSearchBO input);
        Response<int> SaveBulkEmployee(InputBulkBO objstage);
        Response<string> SaveStageCompleted(string Entity, string UniqueNO, int StgDataID);
    }
}