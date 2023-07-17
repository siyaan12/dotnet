using System;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.Employee;

namespace MyDodos.Service.Payroll
{
    public interface IPayrollRevisonService
    {
        Response<GetHRDirectoryList> GetPayrollEmployeeSearch(GetHRDirectoryList objresult);

    }
}