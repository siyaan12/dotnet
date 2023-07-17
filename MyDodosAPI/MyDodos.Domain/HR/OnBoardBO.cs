using System;
using System.Collections.Generic;
using MyDodos.Domain.AuthBO;
using MyDodos.Domain.BenefitManagement;
using MyDodos.Domain.Payroll;

namespace MyDodos.Domain.HR
{
    public class BPProcessBO
    {
        public int TransOrder { get; set; }
        public string ProcessCategory { get; set; }
        public string bpids { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int BProcessID { get; set; }
    }
    public class BPTransaction
    {
        public int BPTransID { get; set; }
        public int BProcessID { get; set; }
        public string BPTransName { get; set; }
        public string BeginWhen { get; set; }
        public string EndsWhen { get; set; }
        public string PreCondition { get; set; }
        public string PostCondition { get; set; }
        public string BusinessUnit { get; set; }
        public string TransStatus { get; set; }
        public int TransOrder { get; set; }
        public bool IsMandatory { get; set; }
        public string BusinessURL { get; set; }
        public string PrevURL { get; set; }
        public string NxtURL { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public string ProcessCategory { get; set; }
    }
    public class BPTransInstance
    {
        public int BPTransInstanceID { get; set; }
        public int BProcessID { get; set; }
        public int ReqInitID { get; set; }
        public string BPTransName { get; set; }
        public string BeginWhen { get; set; }
        public string EndsWhen { get; set; }
        public string PreCondition { get; set; }
        public string PostCondition { get; set; }
        public string BusinessUnit { get; set; }
        public string TransStatus { get; set; }
        public string BusinessURL { get; set; }
        public string PrevURL { get; set; }
        public string NxtURL { get; set; }
        public int TransOrder { get; set; }
        public bool IsMandatory { get; set; }
        public string BPTransInsID { get; set; }
        public string ReqStatus { get; set; }
    }
    public class OnBoardRequestModelMsg {
        public string Msg { get; set; }
        public int ReqID { get; set; }
    }
    // public class EntRolesBO
    // {
    //     public int RoleId { get; set; }
    //     public string RoleName { get; set; }
    //     public string RoleDescription { get; set; }
    //     public int ReportToRoleId { get; set; }
    //     public string RoleCategory { get; set; }
    //     public int KproductId { get; set; }
    //     public string DefaultPage { get; set; }
    //     public string DefaultPageV2 { get; set; }
    //     public bool IsSeedRole { get; set; }
    //     public bool IsSignatory { get; set; }
    // }
    public class InpurtEntRolesBO
    {
        public int ProductId { get; set; }
        public int TenantID { get; set; }
        public string GroupType { get; set; }
    }
    public class IDProofDocumnent
    {
        public int DocTypeId { get; set; }
        public string DocType { get; set; }
    }
    public class HRInputEmpEducation
    {
        public List<HREmpEducation> onEdu { get; set; }
    }
    public class HREmpEducation
    {
        public int EmpEduID { get; set; }
        public int EmpID { get; set; }        
        public string LevelName { get; set; }
        public string CourseName { get; set; }
        public string InstituteName { get; set; }
        public string DurationFrom { get; set; }
        public string DurationTo { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
    public class EmpOnboardingModBO
    {
        public int EmpOnboardingID { get; set; }
        public string RequestNumber { get; set; }
        public string RequestSource { get; set; }
        public string SourceRefNumber { get; set; }
        public string RequestDesc { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string RequestStatus { get; set; }
        public int LocationID { get; set; }
        public DateTime? InitiationDate { get; set; }
        public int RequestDepID { get; set; }
        public int RoleID { get; set; }
        public int ProductID { get; set; }
        public int TenantID { get; set; }
        public int CountryID { get; set; }
        public int GroupTypeID { get; set; }
        public string GroupType { get; set; }
        public DateTime? OfferDate { get; set; }
        public DateTime? AcceptanceDate { get; set; }
        public DateTime? JoiningDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ReqEmpID { get; set; }
        public int HRInchargeID { get; set; }
        public string RoleType { get; set; }
        //public string DepIDs { get; set; }
        public int EmpID { get; set; }
        public int AppUserID { get; set; }
        public string Suffix { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Nationality { get; set; }
        public string Maritalstatus { get; set; }
        public string BloodGroup { get; set; }
        //public InputAppUserBO AppUser { get; set; }
        public OnboardPersonalDetail employee { get; set; }        
        //public List<EmployeeAddress> Address { get; set; }
    }
    public class EmployeeAddress
    {
        public int AddressID { get; set; }
        public int RellAddID { get; set; }
        public int EmpRefID { get; set; }
        public int ReferenceID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Fax { get; set; }
        public string Phone { get; set; }
        public string AddressType { get; set; }
        public string AddressPurpose { get; set; }
        public string AddressStatus { get; set; }
        public string CountryCode { get; set; }
        public bool IsSame { get; set; }       
    }
    public class OnboardPersonalDetail
    {
        public int EmpID { get; set; }
        public int EmpOnboardingID { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public string EmpNumber { get; set; }
        public string EmpStatus { get; set; }        
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Suffix { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string Nationality { get; set; }
        public string MaritalStatus { get; set; }
        public string MobileNo { get; set; }
        public string AlternatePhoneNo { get; set; }
        public string BloodGroup { get; set; }
        public string PersonalMail { get; set; }
        public string OfficeEmail { get; set; }
        public string EmerContactNumber { get; set; }
        public string EmerRelationShip { get; set; }
        public string OfficeContactNumber { get; set; }
        public string PhoneSuffix { get; set; }
        public string AlternatePhoneSuffix { get; set; }
        public string CubicleNo { get; set; }
        public string NetworkDomain { get; set; }
        public string NetworkUserName { get; set; }
        public bool IsCompensate { get; set; }
        public bool IsTimeSheet { get; set; }
        public bool IsAttendance { get; set; }
        public bool IsCompleted { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int AppUserID { get; set; }
        public int FuncRoleID { get; set; }
        public int CountryID { get; set; }
        public List<EmployeeAddress> Address { get; set; }
    }  
    public class AttendanceConfigBO
    {
        public int AttendConfigID { get; set; }        
        public int EmpID { get; set; }
        public int LocationID { get; set; }
        public string ClockingMode { get; set; }
        public string AttendCardNo { get; set; }
        public bool IsRoster { get; set; }
        public bool IsIDCardLinked { get; set; }
        public int CreatedBy { get; set; }
    }
    public class HRInputEmpExperience
    {
        public List<HREmpExperience> onExp { get; set; }
    }
    public class HREmpExperience
    {
        public int EmpExpID { get; set; }
        public int EmpID { get; set; }        
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public DateTime? DurationFrom { get; set; }
        public DateTime? DurationTo { get; set; }
        public int YearOfExp { get; set; }
        public decimal CTC { get; set; }
        public string WorkLocation { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
    public class OnboardingBenefitsBO
    {
        public List<BenefitGroupBO> objbenefit { get; set;}
        public List<MasLeaveGroupBO> objleave { get; set; }
        public List<MasDisabilityBenefitBO> objdisbenefit { get; set;}
        public List<MasMedicalBenefitBO> objmedical { get; set;}
        public List<OtherBenefitsBO> objother { get; set; }
        public List<PlanTypeCategoryBO> objwise { get; set; }
    }
    public class OnboardingBenefitInputBO
    {
        public int EmpID { get; set; }
        public int BenefitGroupID { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
    }     
}