using System;
using MyDodos.ViewModel.Employee;

namespace MyDodos.Repository.Payroll
{
    public interface IPayrollRevisonRepository
    {
        GetHRDirectoryList GetPayrollEmployeeSearch(GetHRDirectoryList inputParam);

    }
}