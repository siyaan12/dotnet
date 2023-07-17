using MyDodos.Domain.BenefitManagement;
using MyDodos.ViewModel.ServerSearch;
using System;
using System.Collections.Generic;

namespace MyDodos.ViewModel.Business
{
    public class ChecklistBO
    {
        public int EmpID { get; set; }        
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public string ProcessCategory { get; set; }
        public string EntityName { get; set; }
        public string RequestDescription { get; set; }
        public int CreatedBy { get; set; }
    }
    public class BPcheckInputBO
    {
        public int RefEntityID { get; set; }
        public int LocationID { get; set; }
        public string RefEntity { get; set; }
        public string ChkListGroup { get; set; }
    }
    public class BPchecklist
    {
        public int ChkListInsID { get; set; }
        public string ChkListGroup { get; set; }
        public string LineItemName { get; set; }
        public bool IsMandatory { get; set; }
        public string ItemComments { get; set; }
        public int ListOrder { get; set; }
        public bool IsCompleted { get; set; }
        public int ChkListInstanceID { get; set; }
        public int RefEntityID { get; set; }
        public int CreatedBy { get; set; }
    }
}