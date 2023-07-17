using System;

namespace MyDodos.Domain.AzureStorage
{
    public class SaveDocCloudBO
    {
        public string Container { get; set; }
        public string CloudType { get; set; }
        public string folderPath { get; set; }
        public string file { get; set; }
        public string fileName { get; set; }
        public string Token { get; set; }
        public string ProductCode { get; set; }
        public string ContentType { get; set; }
    }
    public class AzureDocURLBO
    {
        public string DocumentURL { get; set; }
        public string FileName { get; set; }
        public string ContainerName { get; set; }
        public string Message { get; set; }
        public int DocID { get; set; }
        public string docName { get; set; }
        public string docsFile { get; set; }
        public decimal docsSize { get; set; }
    }
}