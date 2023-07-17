using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyDodos.Domain.AuthBO;
using MyDodos.Domain.Wrapper;
using MyDodos.ViewModel.TicketingSystem;

namespace MyDodos.Service.TicketingSystem
{
    public interface ITicketingSystemService
    {
        Response<SaveTicket> RiseTicket(RiseTicket inputobj);
        Response<List<Ticket>> TicketsList(TicketingInputBO inputobj);
    }
}
