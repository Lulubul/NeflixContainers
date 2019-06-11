using System;
using System.Threading.Tasks;

namespace Marketing.API
{
    public interface IMailService
    {
        Task SendMail(string userId, string email);
    }

    public class MailService : IMailService
    {
        public Task SendMail(string userId, string email)
        {
            throw new NotImplementedException();
        }
    }
}
