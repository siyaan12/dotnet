using MyDodos.Domain.BenefitManagement;
using MyDodos.ViewModel.ServerSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDodos.ViewModel.BenefitManagement
{
    public class HRBenefitPlansInputBO
    {
      public int TenantID { get; set;}
      public int LocationID { get; set;}
      public string PlanStatus { get; set;}
    }
    public class HRBenefitPlansSearchBO
    {
        public HRBenefitPlansInputBO objinput { get; set;}
        public ServerSearchable ServerSearchables { get; set; }
        public List<HRBenefitPlansOutputBO> objplanlist { get; set;}
    }
}