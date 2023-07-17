using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper.Configuration;
using ExcelMapper;
using MyDodos.Domain.Document;

namespace MyDodos.Service.Mapper
{
    public sealed class MapPatientData : CsvHelper.Configuration.ClassMap<StgDataEmployeeBO>
    {
        
        public MapPatientData()
        {
            
            //Map(x => x.EmpNumber).Name("EmpNumber");
            Map(x => x.Prefix).Name("Prefix");
            Map(x => x.FirstName).Name("FirstName");
            //Map(x => x.MiddleName).Name("MiddleName");
            Map(x => x.LastName).Name("LastName");
            Map(x => x.Department).Name("Department");
           // Map(x => x.RoleName).Name("Designation");
            //Map(x => x.EmpName).Name("EmpName");
            //Map(x => x.FathersName).Name("FathersName");
            //Map(x => x.MothersName).Name("MothersName");
            Map(x => x.Gender).Name("Gender");
            Map(x => x.DateofBirth).Name("DOB");
            //Map(x => x.MobileNo).Name("MobileNo");
            Map(x => x.MaritalStatus).Name("MaritalStatus");
            Map(x => x.Department).Name("Department");
            Map(x => x.Designation).Name("Designation");
            //Map(x => x.Email).Name("Email");     
            Map(x => x.BloodGroup).Name("BloodGroup");  
            Map(x => x.DateofOffer).Name("OfferDate");      
            Map(x => x.DateofJoining).Name("DateOfJoining");           
            //Map(x => x.Address).Name("Address");
            //Map(x => x.Address2).Name("CommunicationAddress");
            Map(x => x.Nationality).Name("Nationality");
            
            //Map(x => x.Country).Name("Country");
            //Map(x => x.City).Name("City");
            //Map(x => x.State).Name("State");
            Map(x => x.QualificationDegree).Name("EducationalQualification");
            //Map(x => x.Specializations).Name("Specializations");
            //Map(x => x.LastEmploymentDetails).Name("LastEmploymentDetails");           
           // Map(x => x.IsCompensate).Name("AllowCompensate");
            //Map(x => x.IsTimeSheet).Name("AllowTimeSheet");
            //Map(x => x.IsAttendance).Name("AllowAttendance");

        }
    }
    public class xlsxMapPatientData : ExcelMapper.ExcelClassMap<StgDataEmployeeBO>
    {
        
        public xlsxMapPatientData()
        {
            //Map(x => x.EmpNumber).WithColumnName("EmpNumber");
            Map(x => x.Prefix).WithColumnName("Prefix");
            Map(x => x.FirstName).WithColumnName("FirstName");
            //Map(x => x.MiddleName).WithColumnName("MiddleName");
            Map(x => x.LastName).WithColumnName("LastName");
            //Map(x => x.EmpName).WithColumnName("EmpName");
            //Map(x => x.FathersName).WithColumnName("FathersName");
            Map(x => x.Department).WithColumnName("Department");
            //Map(x => x.RoleName).WithColumnName("Designation");
            Map(x => x.Gender).WithColumnName("Gender");
            Map(x => x.DateofBirth).WithColumnName("DOB");
            Map(x => x.BloodGroup).WithColumnName("BloodGroup");
            Map(x => x.Nationality).WithColumnName("Nationality");
            Map(x => x.Department).WithColumnName("Department");
            Map(x => x.Designation).WithColumnName("Designation");
            //Map(x => x.MobileNo).WithColumnName("MobileNo");
            Map(x => x.MaritalStatus).WithColumnName("MaritalStatus");
            //Map(x => x.Email).WithColumnName("Email");
            Map(x => x.DateofOffer).WithColumnName("OfferDate");            
            Map(x => x.DateofJoining).WithColumnName("DateOfJoining");          
            //Map(x => x.Address).WithColumnName("Address");
            //Map(x => x.Address2).Name("CommunicationAddress");
            //Map(x => x.Country).WithColumnName("Country");
            //Map(x => x.City).WithColumnName("City");
            //Map(x => x.State).WithColumnName("State");
            Map(x => x.QualificationDegree).WithColumnName("EducationalQualification");
            //Map(x => x.Specializations).WithColumnName("Specializations");
            //Map(x => x.LastEmploymentDetails).WithColumnName("LastEmploymentDetails");

        }
    }
}