using System;
using System.Collections.Generic;
using MyDodos.Domain.Document;
using MyDodos.ViewModel.Document;

namespace MyDodos.Repository.Document
{
    public interface IDocumentFileRepository
    {
        List<TemplatedetailBO> GetDataTemplate(int ProductID, int TenantID, string datatemplate, string entity, int EntityID);
        List<TemplatedetailBO> GetDataTemplateCatogory(int ProductID, int TenantID, string TemplateCategory, string TemplateType);
        List<TemplateDatafieldBO> GetDatafield(int ProductID, int TenantID,int Template_ID, int LocationID);
        List<TemplateFieldBO> GetDataDetails(int ProductID, int TenantID, int TemplateID);
        // Get call
        int GetStageSearchdata(StageCheckInputBO _check);
        List<StgreturnDataReferBO> GetHRDataRefer(int  TenantID,int LocationID, string Entity, string UniqueNO);
        List<StgreturnDataReferBO> GetHRDataReference(StageInputReferBO input);
        //Batch Status
        StageSearchBO GetStageEmployee(StageSearchBO input);
        StageSearchBO GetStageHoliday(StageSearchBO input);
        List<StgDataEmployeeBO> GetHRDataEmployee(string Entity,  string UniqueNO, int StgDataID);
        List<StageHolidayBO> GetHRDataHoliday(string Entity,  string UniqueNO, int StgDataID);
        // save call
        string SaveStageDataReference(StageInputReferBO data);
        string SaveStageEmployee(StgDataEmployeeBO data);
        string SaveStageHoliday(StageHolidayBO data);
        StageRequestModelMsg SaveStageOnbordMasterData(StgDataEmployeeBO onboarding);
        StageRequestModelMsg SaveStageEmployeeMasterData(StgDataEmployeeBO onboarding);
        string SaveStageCompleted(string Entity,  string UniqueNO, int StgDataID);
    }
}