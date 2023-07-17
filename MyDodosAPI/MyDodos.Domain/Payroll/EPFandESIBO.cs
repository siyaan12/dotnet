using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyDodos.Domain.Payroll
{
    public class EPFandESIBO
    {
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public int SalaryPeriodID { get; set; }
        public string Comments { get; set; }
        public bool ischeck { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DownloadOn { get; set; }        
    }
    public class RtnEPFandESIBO
    {
        public int DocID { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public int SalaryPeriodID { get; set; }
        public int CreatedBy { get; set; }
        public string DocURL { get; set; }
    }
}