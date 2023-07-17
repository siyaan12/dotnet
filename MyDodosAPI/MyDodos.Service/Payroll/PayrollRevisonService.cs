using System;
using Microsoft.Extensions.Configuration;
using MyDodos.Domain.Wrapper;
using MyDodos.Repository.Payroll;
using MyDodos.ViewModel.Employee;

namespace MyDodos.Service.Payroll
{
    public class PayrollRevisonService : IPayrollRevisonService
    {
        private readonly IConfiguration _configuration;
        private readonly IPayrollRevisonRepository _payrollRevisonRepository;
        public PayrollRevisonService(IConfiguration configuration, IPayrollRevisonRepository payrollRevisonRepository)
        {
            _configuration = configuration;
            _payrollRevisonRepository =  payrollRevisonRepository;
        }
        public Response<GetHRDirectoryList> GetPayrollEmployeeSearch(GetHRDirectoryList objresult)
        {
            Response<GetHRDirectoryList> response;
            try
            {
                var result = _payrollRevisonRepository.GetPayrollEmployeeSearch(objresult);
                
                if (result.objHRDirectoryList.Count == 0)
                {
                    response = new Response<GetHRDirectoryList>(result, 200, "Data Not Found");
                }
                else
                {
                    response = new Response<GetHRDirectoryList>(result, 200, "Data Retraived");
                }
            }
            catch (Exception ex)
            {
                response = new Response<GetHRDirectoryList>(ex.Message, 500);
            }
            return response;
        }
    }
}