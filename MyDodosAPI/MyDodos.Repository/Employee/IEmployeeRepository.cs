using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDodos.Domain.Employee;
using MyDodos.Domain.HR;
using MyDodos.Domain.Master;
using MyDodos.Domain.Payroll;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.Common;
using MyDodos.ViewModel.Employee;
using MyDodos.ViewModel.HR;

namespace MyDodos.Repository.Employee
{
    public interface IEmployeeRepository
    {
        GetHRDirectoryList GetHRDirectoryList(GetHRDirectoryList inputParam);
        List<HRDirectorySummeryBO> GetHRDirectorySummery(int TeantID,int LocationID,int DepartmentID);
        Response<List<ManagerBO>> GetManagerList(int EmpID, int TenantID,int LocationID);
        List<EmployeeView> GetUserView(int EmpID,int LoginID,int TenantID);
        HRDirectoryEmpView GetEmployeeViewDetails(int EmpID,int LocationID,int TenantID);
        HREmpPersonalInfo GetEmployeePersonalInfo(int EmpID,int LocationID,int TenantID);
        HREmpAddress GetEmployeeAddressInfo(int EmpID);
        List<BPTransNameBO> GetBPTransName(int EmpOnboardingID);
        List<OnboardPersonalDetail> GetEmpOnBoard(int EmpID);
        InCompleteCountBO GetHRDirInCompleteCount(int TenantID,int LocationID);
        List<EmpReportingBO> GetEmpReports(int EmpID);
        List<EmpDirectorBO> GetEmpDirects(int EmpID);
        List<EmpColleaguesBO> GetEmpColleagues(int EmpID);
        List<EmpReportingOrgBO> GetParentsOrgzChart(int EmpID, int ManagerID,  int LocationID,int TenantID);
        List<EmpReportingOrgBO> GetChaildsOrgzChart(int EmpID, int ManagerID,  int LocationID,int TenantID);
        // List<EmpDirectorOrgBO> GetIndividualOrgzCharts(int EmpID,int LocationID,int TenantID);
        // List<EmpColleaguesOrgBO> GetIndividualOrgzChartss(int EmpID,int LocationID,int TenantID);
        OnBoardRequestModelMsg SaveEmployeeProfile(EmployeeProfileInputBO profile);
        EmployeeProfileInputBO GetEmployeeProfile(int AppUserID,int ProductID);
        TenantProfiledataBO GetTenantProfile(int TenantID,int ProductID);
        OnBoardRequestModelMsg UpdateTenantProfile(TenantProfiledataBO profile);
        TenantProfileImageBO GetTenantProfileImage(int TenantID,int ProductID);
        SaveOut SaveLocation(LocationdetBO objlocation);
        SaveOut SaveAccountDetails(AccountDetailsBO objaccount);
        AccountDetailsBO GetAccountDetails(int EmpID);
        List<vwPayrollUser> EmpPayrollSalaryMonth(int TenantID,int LocationID,int EmpID);
        List<vwPayrollUser> GetPayHistory(int TenantID,int LocationID,int SalaryPeriodId);
        int GetBenefitGroupByEmp(int EmpID);
        List<EmpPersonalDetail> GetEmpDetails(int EmpID,int TenantID,int LocationID);
        List<vwProjectBO> GetEmpProjectList(int EmpID,int ProjectID,bool IsProjectManager);
        List<EmpPersonalDetail> GetProjectmanager(int EmpID,int ProjectID,bool IsProjectManager);
        List<EntRoles> GetDesignation(int TenantID);
        SaveOut UpdateEmployeeDesignation(EmployeeInfoBO employee);
        List<EmpManagerDropdown> GetManagers(int TenantID,int LocationID,int ProjectID);
    }
}