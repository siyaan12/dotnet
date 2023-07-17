using System.Collections.Generic;
using MyDodos.Domain.Employee;
using MyDodos.Domain.HR;
using MyDodos.Domain.Master;
using MyDodos.Domain.Payroll;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.Common;
using MyDodos.ViewModel.Employee;
using MyDodos.ViewModel.HR;

namespace MyDodos.Service.Employee
{
    public interface IEmployeeService
    {
        Response<GetHRDirectoryList> GetHRDirectoryList(GetHRDirectoryList objresult);
        Response<UserViewBO> GetUserView(int EmpID, int LoginID,int TenantID);
        Response<HRDirectoryEmpView> GetEmployeeViewDetails(int EmpID, int LocationID,int TenantID);
        Response<OrgChartBO> GetIndividualEmpOrgzChart(int EmpID);
        Response<EmpReportingOrgBO> GetIndividualOrgzChart(int EmpID,int ManagerID, int LocationID, int TenantID);
        Response<int> SaveEmployeeProfile(EmployeeProfileBO profile);
        Response<EmployeeProfileInputBO> GetEmployeeProfile(int AppUserID,int ProductID);
        Response<int> SaveCompanyProfile(CompanyProfileBO profile);
        Response<TenantProfileImageBO> GetTenantProfile(int TenantID,int ProductID);
        Response<SaveOut> SaveLocation(LocationdetBO objlocation);
        Response<List<vwPayrollUser>> GetPayRollEmployees(int TenantID,int LocationID,int EmpID);
        //Response<List<HREmpSalaryStructure>> GetEmployeePayInfo(int EmpID, int BenefitGroupID, string paycycle);
        Response<OnboardingBenefitsBO> GetBenefitsByEmp(int EmpID,int TenantID,int LocationID);
        Response<MyTeamBO> GetMyTeam(int TenantID,int EmpID);
        Response<List<EntRoles>> GetDesignation(int TenantID);
        Response<SaveOut> UpdateEmployeeDesignation(EmployeeInfoBO employee);
        Response<List<EmpManagerDropdown>> GetManagers(int TenantID);
    }
}