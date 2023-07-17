using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDodos.Domain.Report
{
    public class LeaveReportsBO
    {
        public int LeaveReportID { get; set;}
        public string LeaveFrom { get; set;}
        public string LeaveTo { get; set;}
        public string LeaveCategory { get; set;}
        public string LeaveType { get; set;}
        public decimal NoOfDays { get; set;}
        public string LeaveReason { get; set;}
        public string LeaveStatus { get; set;}
        public string LeaveSession { get; set;}
        public string LeaveComments { get; set;}
        public DateTime RequestDate { get; set;}
        public int IsLOP { get; set;}
        public int EmpID { get; set;}
        public string EmpName { get; set;}
        public int LocationID { get; set;}
        public int TenantID { get; set;}
        public int YearID { get; set;}
        public int LeaveCategoryID { get; set;}
        public DateTime CreatedOn { get; set;}
        public DateTime ModifiedOn { get; set;}
        public int CreatedBy { get; set;}
        public string ModifiedBy { get; set;}
    }
    public class ReportTypeBO
    {
        public int ReportTypeID { get; set; }
        public string ReportTypeName { get; set; }
        public string ReportTypeDescription { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int ReportID { get; set;}
        public string ReportName { get; set; }
        public string ReportDescription { get; set; }
    }
    public class ReportTypesBO
    {
        public int ReportTypeID { get; set; }
        public string ReportTypeName { get; set; }
        public string ReportTypeDescription { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public List<DynReportsBO> objreport { get; set; }
    }
    public class DynReportsBO 
    {
        public int ReportID { get; set;}
        public string ReportName { get; set; }
        public string ReportDescription { get; set; }
    }
    public class ReportDetailsBO
    {
        public int TimesheetReportID { get; set; }
        public int TimesheetID { get; set; }
        public string WeekNo { get; set; }
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ResourceName { get; set; }
        public int HoursReported { get; set; }
        public int BilledHours { get; set; }
        public string Symbol { get; set; }
        public string Rate { get; set; }
        public string PaidAmount { get; set; }
        public string BilledAmount { get; set; }
        public string BalanceAmount { get; set; }
        public DateTime AssignStart { get; set; }
        public DateTime AssignEnd { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int LeaveReportID { get; set;}
        public string LeaveFrom { get; set;}
        public string LeaveTo { get; set;}
        public string LeaveCategory { get; set;}
        public string LeaveType { get; set;}
        public decimal NoOfDays { get; set;}
        public string LeaveReason { get; set;}
        public string LeaveStatus { get; set;}
        public string LeaveSession { get; set;}
        public string LeaveComments { get; set;}
        public DateTime RequestDate { get; set;}
        public int IsLOP { get; set;}
        public decimal LOPCount { get; set; }
        public int EmpID { get; set;}
        public string EmpNumber { get; set; }
        public string EmpName { get; set;}
        public int YearID { get; set;}
        public int LeaveCategoryID { get; set;}
        public string ApprovedBy { get; set; }
        public int LOPID { get; set; }
        public string ManagerName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int SalaryPeriodID { get; set; }
        public decimal NoOfLeave { get; set; }
        public decimal Accrued { get; set; }
        public decimal Availed { get; set; }
        public decimal LeaveBalance { get; set; }
        public string EmpAllocStatus { get; set; }
        public int RowNum { get; set; }
        public int TotalCount { get; set; }
    }
    public class MapValues
    {
        public int MapID {get; set;}
        public string FieldName { get; set; }
        public string MapValue { get ; set; }
        public bool IsDefaultField { get; set; }
    }
    public class ReportInputBO
    {
        public int ReportTypeID { get; set; }
        public int ReportID { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int SalaryPeriodID { get; set; }
    }
    public class MasReportBO
    {
        public int ReportID { get; set; }
        public string ReportName { get; set; }
        public string ReportDescription { get; set; }
        public int ReportTypeID { get; set; }
        public string ReportTableName { get; set; }
        public string ReportTableQuery { get; set; }
        public string TableColumnNames {get; set; }
        public string DefaultFieldNames { get; set; }
        public string DynamicFieldNames { get; set; }
        public string DisplayColumns { get; set; }
        public string QueryConditions { get; set; }
        public int TenantID {get; set; }
        public int LocationID { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ColumnName { get; set; }
        public string ProcedureName { get; set; }
        public string ProcedureInputs { get; set; }
    }
}
