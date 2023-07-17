using System.Collections.Generic;
using System.Threading.Tasks;
using MyDodos.Domain.Mail;

namespace MyDodos.Repository.Mail
{
    public interface IMailRepository
    {
        Task<int> SendMail(MailModel notification);
        List<MailNotifyBO> GetMailReportName(int EmpID);
        
    }
}