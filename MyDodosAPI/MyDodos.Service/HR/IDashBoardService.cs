using System.Collections.Generic;
using MyDodos.Domain.HR;
using MyDodos.Domain.Wrapper;

namespace MyDodos.Service.HR
{
    public interface IDashBoardService
    {
        Response<DashBoard> GetDashBoardList(int TenantID, int LocationID, int YearID);
    }
}