using System;
using System.Collections.Generic;
using MyDodos.Domain.Holiday;
using MyDodos.Domain.PermissionBO;
using MyDodos.ViewModel.ServerSearch;

namespace MyDodos.ViewModel.LeaveManagement
{
    public class GetMyPermissionList
    {
        public GetMyPermissionListInputs objMyPermissionListInput { get; set; }
        public ServerSearchable ServerSearchables { get; set; }
        public List<PermissionModel> objMyPermissionList { get; set; }
    }
    public class GetMyPermissionListInputs
    {
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int YearID { get; set; }
        public int EmpID { get; set; }
        public int ManagerID { get; set; }
        public string PermStatus { get; set; }
    }
}