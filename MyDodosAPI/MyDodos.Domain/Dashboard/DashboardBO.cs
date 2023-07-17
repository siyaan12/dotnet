using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDodos.Domain.Dashboard
{
    public class DashboardCountBO
    {
        public int EmpCount { get; set; }
        public int LeaveCount { get; set; }
        public int AbsentCount { get; set; }
        public int RegularCount { get; set; }
        public int IrregularCount { get; set; }
        public int ExceptionCount { get; set; } 
    }
    public class DashboardInputBO
    {
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public string DateValue { get; set;}
    }
}