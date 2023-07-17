using System;
using System.Collections.Generic;
using MyDodos.Domain.Payroll;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.Payroll;

namespace MyDodos.Service.Payroll
{
    public interface IPayrollSlipService
    {
        Response<RtnEPFandESIBO> DowloadECRFile(EPFandESIBO _objpayroll);
        Response<RtnEPFandESIBO> PayrollDowloadESIFile(EPFandESIBO _objpayroll);
        Response<RtnEPFandESIBO> DowloadECRtxtFile(EPFandESIBO _objpayroll);
    }
}