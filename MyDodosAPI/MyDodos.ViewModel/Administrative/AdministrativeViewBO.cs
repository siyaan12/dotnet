using System;
using System.Collections.Generic;
using MyDodos.Domain.Administrative;
using MyDodos.ViewModel.ServerSearch;

namespace MyDodos.ViewModel.Administrative
{
    public class GenSeqNum
    {
        public string Id { get; set; }
        public bool? IsRequired { get; set; }
    }
    public class DepartmentList
    {
        public GetDepartmentInputs objDepartmentInput { get; set; }
        public ServerSearchable ServerSearchables { get; set; }
        public List<MasDepartmentBO> objDepartmentList { get; set; }
    }
    public class GetDepartmentInputs
    {
        public int TenantID { get; set; }
        public int LocationID { get; set; }
    }
}