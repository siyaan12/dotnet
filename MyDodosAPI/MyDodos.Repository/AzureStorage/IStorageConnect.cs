using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyDodos.Domain.AzureStorage;

namespace MyDodos.Repository.AzureStorage
{
    public interface IStorageConnect
    {
        Task<AzureDocURLBO> SaveBulkDocumentCloud(SaveDocCloudBO docCloud);
        Task<AzureDocURLBO> DownloadDocument(SaveDocCloudBO getDoc);
        Task<AzureDocURLBO> DeleteDocument(SaveDocCloudBO getDoc);
        string ReadDataInUrl(string url);
    }
}
