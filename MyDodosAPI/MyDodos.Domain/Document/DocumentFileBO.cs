using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace MyDodos.Domain.Document
{
    public class StageRetunfieldBO
    {
        public string FiledName { get; set; }
        public string MapValue  { get; set; }       
    }
    
    public class StgDataEmployeeBO
    {
        public int StgDataID { get; set; }
        /* Excel Data */
        # region Excel Data
        public string EmployeeID { get; set; }
        public string Prefix { get; set; }        
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public DateTime DateofBirth { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Resident { get; set; }
        public string NonResident { get; set; }
        public string BloodGroup { get; set; }
        public string Designation { get; set; }
        public string ManagerName { get; set; }
        public string Department { get; set; }
        public string Nationality { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public DateTime DateofJoining { get; set; }
        public DateTime DateofAppoinment { get; set; }
        public DateTime DateofOffer { get; set; }
        public DateTime DateofAcceptance { get; set; }        
        public string Subject { get; set; }        
        public string GovtID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Ethnicity { get; set; }
        public string SalaryDetails { get; set; }
        /* Experience details */
        public string ExperineceCompanyName { get; set; }   
        public string ExperineceDesignation { get; set; }
        public DateTime ExperineceDateofJoining { get; set; }
        public DateTime ExperineceDateofRelieving { get; set; }
        public string TotalExperinece { get; set; }
        /* Education Details */
        public string QualificationSchool { get; set; }
        public string QualificationDiploma { get; set; }
        public string QualificationDegree { get; set; }
        #endregion
        /* Back End Data */
        public int DepartmentID { get; set; }
        public int onboardID { get; set; }
        public int DesignationID { get; set; }
        public int ManagerID { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public string StageFieldName { get; set; }
        public string UniqueBatchNO { get; set; }
        public string ProcessStatus { get; set; }
        public int Createdby { get; set; }
        public int TotalCount { get; set; }
        public int Validdata { get; set; }
        public bool IsExcepations { get; set; }
        public int Excepations { get; set; }
        public int SearchTotalCount { get; set; }
        public string ExcepationFieldName { get; set; }
        public string DataHRFieldName { get; set; }
    }
    public class StageHolidayBO
    {
        public int StgDataID { get; set; }
        public string HolidayName { get; set; }
        public DateOnly HolidayDate { get; set; }
        public string Description { get; set; }
        public string HolidayType { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public string StageFieldName { get; set; }
        public string UniqueBatchNO { get; set; }
        public string InsertDate { get; set; }
        public string ProcessStatus { get; set;}
        public int TotalCount { get; set; }
        public int Validdata { get; set; }
        public bool IsExcepations { get; set; }
        public int Excepations { get; set; }
        public int SearchTotalCount { get; set; }
        public string ExcepationFieldName { get; set; } 
    }
    public class StageRequestModelMsg {
        public string Msg { get; set; }
        public int RequestID { get; set; }
    }
}