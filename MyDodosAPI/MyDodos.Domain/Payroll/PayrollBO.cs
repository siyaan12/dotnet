using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyDodos.Domain.Payroll
{
    public class PayrollCountBO
    {
        public int EmpCount { get; set; }
        public string AttendanceClosingDate { get; set; }
        public string PayDate { get; set; }
    }
    public class MasMedicalBenefitBO
    {
        public int MedicalBenefitID { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public string CoverageType { get; set; }
        public bool IsMandatory { get; set; }
        public decimal MandatoryCTCLimit { get; set; }
        public decimal EmpContribution { get; set; }
        public bool IsContributionPercent { get; set; }
        public string BenefitStatus { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
    public class OnBoardBenefitGroupBO
    {
        public int EmpID { get; set; }
        public int BenefitGroupID { get; set; }
        public int GroupTypeID { get; set; }
        public int LeaveGroupID { get; set; }
        public int StructureID { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
    public class OtherBenefitsBO
    {
        public bool IsBusinessCard { get; set; }
        public bool IsCorpCard { get; set; }
        public bool IsGuestHouse { get; set; }
        public bool isPerks { get; set; }
    }
    public class EmployeeBenefitsBO
    {
        public int EmpID { get; set; }
        public string EmpNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int RoleID { get; set; }
        public int ManagerID { get; set; }
        public int DepartmentID { get; set; }
        public int BenefitGroupID { get; set; }
        public string Gender { get; set; }
        public DateTime? DOJ { get; set; }
        public DateTime? DOB { get; set; }
        public string OfficeEmail { get; set; }
        public string PersonalMail { get; set; }
        public string MobileNo { get; set; }
        public string EmpType { get; set; }
        public string BloodGroup { get; set; }
        public string Nationality { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public bool IsCompensate { get; set; }
        public bool IsTimeSheet { get; set; }
        public bool IsAttendance { get; set; }
        public int HRInchargeID { get; set; }
        public string EmpStatus { get; set; }
        public string ProfileImage { get; set; }
        public int AppUserID { get; set; }
        public bool IsCompleted { get; set; }
        public string base64images { get; set; }
        public string HRNotification { get; set; }
        public string PaymentMode { get; set; }
        public string EmpName { get; set; }
    }
    public class HREmpBenefitsModel
    {
        public int EmpBeneID { get; set; }
        public int EmpID { get; set; }
        public int LeaveGroupID { get; set; }
        public int BenefitGroupID { get; set; }
        public string GroupName { get; set; }
        public int PayrollStructID { get; set; }
        public decimal MinCTC { get; set; }
        public decimal MaxCTC { get; set; }
        public decimal SetCTC { get; set; }
        public decimal GrossByAnnual { get; set; }
        public decimal GrossByMonth { get; set; }
        public decimal TakeHomeSalary { get; set; }
        public decimal TotalDeducations { get; set; }
        public decimal Costtocompany { get; set; }
        public decimal TotalEarnings { get; set; }
        public decimal NetPay { get; set; }
        public string ExceptionNotes { get; set; }
        public string BenefitStatus { get; set; }
    }
    public class HREmpSalaryStructure
    {
        public int EmpSalStrucID { get; set; }
        public int EmpID { get; set; }
        public int StructureID { get; set; }
        public int ItemID { get; set; }
        public int Rate { get; set; }
        public int ItemOrder { get; set; }
        public bool IsItemActive { get; set; }
        public decimal CalcType { get; set; }
        public string LineItemKey { get; set; }
        public string LineItem { get; set; }
        public string SalaryRangesMin { get; set; }
        public string SalaryRangesMax { get; set; }
        public string PayrollLineCalcFormat { get; set; }
        public string ItemType { get; set; }
        public int EmpSalItemID { get; set; }
    }
    public class OnboardingEmpBenefitsBO
    {
        public List<HREmpBenefitsModel> objbenefit { get; set; }
        public List<HREmpSalaryStructure> Structure { get; set; }
    }
    public class PayrollCTCBO
    {
        public int EmpID { get; set; }
        public int BenefitGroupID { get; set; }
        public int TenantID { get; set; }
        public int YearID { get; set; }
        public int LocationID { get; set; }
        public int StructureID { get; set; }
        public int EmpSalStrucID { get; set; }
        public string LineItemKey { get; set; }
        public decimal MinCTC { get; set; }
        public decimal MaxCTC { get; set; }
        public decimal SetCTC { get; set; }
        public decimal GrossSalary { get; set; }
        public string ExceptionNotes { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
    public class YearSpanValue
    {
        public int YearID { get; set; }
        public string YearSpan { get; set; }
        public List<SalaryDetailsMonth> detailsByMonth { get; set; }
    }
    public class SalaryDetailsMonth
    {
        public int SalaryDetails { get; set; }
        public DateTime AttendanceClosingDate { get; set; }
        public string DatePeriod { get; set; }
        public int EmpTotals { get; set; }
        public decimal IncomeTaxAmount { get; set; }
        public decimal NetPay { get; set; }
        public DateTime PayDate { get; set; }
        public decimal PayrollCost { get; set; }
        public decimal ProfessionTaxAmount { get; set; }
        public string Status { get; set; }
        public decimal TotalDeductionAmount { get; set; }
        public decimal TotalTax { get; set; }
        public int SalaryPeriodID { get; set; }
        public DateTime PeriodFrom { get; set; }
        public DateTime PeriodTo { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public decimal AdjustmentsAmount { get; set; }
        public string YearSpan { get; set; }
    }
    public class SalaryPeriodBO
    {
        public int SalaryPeriodID { get; set; }
        public string PeriodType { get; set; }
        public int YearID { get; set; }
        public DateTime PeriodFrom { get; set; }
        public DateTime PeriodTo { get; set; }
        public string PeriodStatus { get; set; }
        public string ProcessStatus { get; set; }
        public bool IsBaseSalarySet { get; set; }
        public bool IsLeaveCalc { get; set; }
        public bool IsOtherDeduction { get; set; }
        public bool IsBankInstruction { get; set; }
        public bool IsSalaryCredited { get; set; }
        public string Comments { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string YearSpan { get; set; }
    }
    public class RegPayRollBO
    {
        public int EmpTotals { get; set; }
        public DateTime AttendanceClosingDate { get; set; }
        public DateTime PayDate { get; set; }
        public decimal TotalAdjustments { get; set; }
        public string DatePeriod { get; set; }
        public decimal PayrollCost { get; set; }
        public decimal ProfessionTaxAmount { get; set; }
        public decimal NetPay { get; set; }
        public decimal IncomeTaxAmount { get; set; }
        public decimal TotalDeductionAmount { get; set; }
        public decimal TotalTax { get; set; }
        public string Status { get; set; }
        public int SalaryPeriodID { get; set; }
    }
    public class vwPayrollUser
    {
        public int EmpID { get; set; }
        public string EmpNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string OfficeEmail { get; set; }
        public string EmpType { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int EmpBeneID { get; set; }
        public int BenefitGroupID { get; set; }
        public int LeaveGroupID { get; set; }
        public decimal MinCTC { get; set; }
        public decimal MaxCTC { get; set; }
        public decimal SetCTC { get; set; }
        public decimal CTCByMonth { get; set; }
        public decimal GrossByAnnual { get; set; }
        public decimal GrossByMonth { get; set; }
        public decimal TakeHomeSalary { get; set; }
        public decimal NetPayAmount { get; set; }
        public int EmpSalStrucID { get; set; }
        public int StructureID { get; set; }
        public int ItemID { get; set; }
        public decimal Rate { get; set; }
        public decimal RateByMonth { get; set; }
        public int ItemOrder { get; set; }
        public bool IsItemActive { get; set; }
        public string CalcType { get; set; }
        public string LineItemKey { get; set; }
        public string LineItem { get; set; }
        public string ItemType { get; set; }
        public string EmpStatus { get; set; }
        public int ManagerID { get; set; }
        public int PayrollStructID { get; set; }
        public decimal TotalDays { get; set; }
        public decimal LOPDays { get; set; }
        public decimal LeaveDays { get; set; }
        public decimal TimeoffDays { get; set; }
        public decimal Holidays { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal PaidDays { get; set; }
        public int AppUserId { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public DateTime DOJ { get; set; }
        public string Designation { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal TotalEarnings { get; set; }
        public DateTime PeriodFrom { get; set; }
        public DateTime PeriodTo { get; set; }
        public decimal Basic { get; set; }
        public decimal HRA { get; set; }
        public decimal Conveyance { get; set; }
        public decimal Special { get; set; }
        public decimal EmployerPF { get; set; }
        public decimal EmployeePF { get; set; }
        public decimal EmployerESI { get; set; }
        public decimal EmployeeESI { get; set; }
        public decimal ProfessionalTax { get; set; }
        public decimal CTC { get; set; }
        public decimal Others { get; set; }
        public decimal LOPDeductions { get; set; }
        public decimal OtherDeductions { get; set; }
        public decimal PFESI { get; set; }
        public decimal IncomeTax { get; set; }
        public bool IsAdjust { get; set; }
        public decimal TotalAdjustments { get; set; }
        public int SalaryPeriodId { get; set; }
        public bool IsExcel { get; set; }
    }
    public class SalaryAdjustmentsBO
    {
        public int AdjustmentID { get; set; }
        public int AdjustmentLineItemID { get; set; }
        public string AdjustmentType { get; set; }
        public string AdjustmentLineItem { get; set; }
        public decimal Amount { get; set; }
        public string AdjustmentMode { get; set; }
        public decimal ExistingAmount { get; set; }
        public string AdjustmentComments { get; set; }
        public int EmpID { get; set; }
        public int SalaryPeriodID { get; set; }
        public decimal ChangedAmount { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
    public class YearPFSpanValue
    {
        public int YearID { get; set; }
        public string YearSpan { get; set; }
        public decimal AmountPaid { get; set; }
        public string EPFDate { get; set; }
        public string ESIDate { get; set; }
        public int TotalEmployee { get; set; }
        public List<PayrollPFContributionSummary> detailsByMonth { get; set; }
    }
    public class PayrollPFContributionSummary
    {
        public int SalaryPeriodID { get; set; }
        public string Month { get; set; }
        public string EPFDate { get; set; }
        public string ESIDate { get; set; }
        public int EmpTotals { get; set; }
        public decimal TotalEmployeeShare { get; set; }
        public decimal TotalEPS { get; set; }
        public decimal TotalEPF { get; set; }
        public decimal TotalAdmin { get; set; }
        public decimal TotalEDLI { get; set; }
        public decimal TotalEFCont { get; set; }
        public decimal AmountPaid { get; set; }
    }
    public class PayrollPFContribution
    {
        public int PFContributionID { get; set; }
        public int SalaryPeriodID { get; set; }
        public int EmpTotals { get; set; }
        public int EmpID { get; set; }
        public string EmpNumber { get; set; }
        public string FullName { get; set; }
        public string PFNumber { get; set; }
        public string UANNumber { get; set; }
        public decimal Basic { get; set; }
        public decimal PayableBasic { get; set; }
        public decimal EmpShareDue { get; set; }
        public decimal VPF { get; set; }
        public decimal TotalEmpPF { get; set; }
        public decimal EPSScheme { get; set; }
        public decimal EPF { get; set; }
        public decimal PFAdminCharges { get; set; }
        public decimal EDLI { get; set; }
        public decimal TotalPF { get; set; }
        public decimal NCPandLOPday { get; set; }
        public decimal AmountPaid { get; set; }
        public string EPFDate { get; set; }
        public string ESIDate { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
        public decimal NoOfDay { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal ESIEesCont { get; set; }
        public decimal ESIErsCont { get; set; }
        // public DateTime PFDate { get; set; }
        // public DateTime ESDate { get; set; }

    }
    public class EmpSalaryStructureCTC
    {
        public int EmpSalStrucID { get; set; }
        public int EmpID { get; set; }
        public string LineItemKey { get; set; }
        public string PayrollLineCalcFormat { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal ProfessionalTax { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal SetCTC { get; set; }
        public decimal PayrollMin { get; set; }
        public decimal PayrollMax { get; set; }
        public int BenefitGroupID { get; set; }
        public int payrollStructID { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public string ExceptionNotes { get; set; }
        public int CreatedBy { get; set; }
        public string PayrollCode { get; set; }
        public string PayrollValues { get; set; }
        public decimal Ratevalues { get; set; }
        public string SalaryRangeMin { get; set; }
        public string SalaryRangeMax { get; set; }
    }
    public class YearESISpanValue
    {
        public int YearID { get; set; }
        public string YearSpan { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime ESIDate { get; set; }
        public int TotalEmployee { get; set; }
        public List<PayrollESIContributionSummary> detailsByMonth { get; set; }
    }
    public class PayrollESIContributionSummary
    {
        public int SalaryPeriodID { get; set; }
        public string Month { get; set; }
        public string EPFMonth { get; set; }
        public string ESIMonth { get; set; }
        public int EmpTotals { get; set; }
        public decimal ESIEesCont { get; set; }
        public DateTime ESIDate { get; set; }
        public decimal ESIErsCont { get; set; }
        public decimal AmountPaid { get; set; }
    }
    public class PayrollESIContribution
    {
        public int PFContributionID { get; set; }
        public int SalaryPeriodID { get; set; }
        public int EmpTotals { get; set; }
        public int EmpID { get; set; }
        public string EmpNumber { get; set; }
        public string FullName { get; set; }
        public string PFNumber { get; set; }
        public string UANNumber { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime ESIDate { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
        public decimal NoOfDay { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal ESIEesCont { get; set; }
        public decimal ESIErsCont { get; set; }
    }

    public class GetLOPBO
    {
        public decimal TotalDays { get; set; }
        public decimal LOPDays { get; set; }
        public decimal TimeoffDays { get; set; }
        public decimal Holidays { get; set; }
        public decimal PaidDays { get; set; }
        public decimal NoofDays { get; set; }
    }
    public class PayrollEntAppUserBO
    {
        public int AppUserID { get; set; }
        public string AppUserName { get; set; }
        public string AppUserPassword { get; set; }
        public string AppUserStatus { get; set; }
        public int PrimaryCompanyID { get; set; }
        public string ProfilePhoto { get; set; }
        public string Prefix { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string Department { get; set; }
        public string ActivationKey { get; set; }
        public bool IsActivated { get; set; }
        public string ActivatedOn { get; set; }
        public int ProfileImageID { get; set; }
        public int ReportingToID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int FailedAttempts { get; set; }
        public DateTime CurrentSigninAt { get; set; }
        public string CurrentSigninIP { get; set; }
        public DateTime LastSigninAt { get; set; }
        public string LastSigninIP { get; set; }
        public string DefaultPage { get; set; }
        public string PrimaryEmail { get; set; }
        public string SecondaryEmail { get; set; }
        public string ContactPhone { get; set; }
        public string UserType { get; set; }
        public bool ChangePassword { get; set; }
        public int LoginPIN { get; set; }
        public bool IsSSoUser { get; set; }
        public string AuthPIN { get; set; }
    }
    public class DatesLOP
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class BankExcelInputBO
    {
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int SalaryPeriodID { get; set; }
        public string Purpose { get; set; }
        public int EmpID { get; set; }
    }
    public class BankExcelValuesBO
    {
        public string AccNumber { get; set; }
        public string EmpName { get; set; }
        public decimal NetPayAmount { get; set; }
    }
    public class EmployeeSalaryExcelBO
    {
        public string EmpNumber { get; set; }
        public string EmpName { get; set; }
        public DateTime DOJ { get; set; }
        public decimal CurrentMonthGross { get; set; }
        public decimal Basic { get; set; }
        public decimal HRA { get; set; }
        public decimal ConveyanceAllowance { get; set; }
        public decimal EmployerPF { get; set; }
        public decimal EmployeePF { get; set; }
        public decimal EmployerESI { get; set; }
        public decimal EmployeeESI { get; set; }
        public decimal ProfessionalTax { get; set; }
        public decimal OtherAllowance { get; set; }
        public decimal NoOfDays { get; set; }
        public decimal LOPDays { get; set; }
        public decimal LOPDeductions { get; set; }
        public decimal OtherDeductions { get; set; }
        public decimal TakeHomeSalary { get; set; }
        public decimal YearlyCTC { get; set; }
        public decimal IncomeTax { get; set; }
    }
    public class SalaryPeriodStatusBO
    {
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int SalaryPeriodID { get; set; }
        public string ProcessStatus { get; set; }
    }
    public class MasYearDetailsBO
    {
        public int YearID { get; set; }
        public int StartMonth { get; set; }
        public int StartYear { get; set; }
        public int EndMonth { get; set; }
        public int EndYear { get; set; }
        public string YearStatus { get; set; }
        public string YearName { get; set; }
        public bool YearEndProcessDone { get; set; }
        public bool sHolidayLock { get; set; }
        public int LocationID { get; set; }
        public int TenantID { get; set; }
        public bool isReview { get; set; }
        public string YearSpan { get; set; }
        public int OptionalHoliday { get; set; }
        public bool IsEmployee { get; set; }
        public string DueDate { get; set; }
        public bool IsNotify { get; set; }
        public string NotifyContent { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
    public class SalaryonboardPaycycle
    {
        public int EmpID { get; set; }
        public int BenefitGroupID { get; set; }
        public int StructureID { get; set; }
        public string Paycycle { get; set; }
    }
}