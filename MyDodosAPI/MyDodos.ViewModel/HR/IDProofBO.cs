using System;
using System.Collections.Generic;
using KoSoft.DocRepo;

namespace MyDodos.ViewModel.HR
{
    public class InputDocIDCardInputBo
    {
        public DocIDCardInputBo IDcard { get; set; }  
        public InputDocsBO inputDocs { get; set; }
    }
    public class InputDocsBO
    {
        public int DocID { get; set; }
        public int TenantID { get; set; }
        public string docName { get; set; }
        public string docsFile { get; set; }
        public decimal docsSize { get; set; }
    }
    public class DocIDCardInputBo
    {
        public int EmpIDCardID { get; set; }
        public int EmpID { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public string CardNumber { get; set; }
        public int ActivatedBy { get; set; }
        public string ActivatedName { get; set; }
        public DateTime ActivatedOn { get; set; }
        public string IDCardStatus { get; set; }
        public string IDCardImage { get; set; }
        public List<GenDocument> docsList { get; set; }        
    }
    public class DocProofInputBo
    {
        public int DocID { get; set; }
        public string FileName { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int EmpID { get; set; }
        public string EmpNumber { get; set; }
        public int DocTypeID { get; set; }
        public int CreatedBy { get; set; }        
    }
}