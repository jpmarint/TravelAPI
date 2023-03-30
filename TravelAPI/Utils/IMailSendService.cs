using TravelAPI.Models;

namespace TravelAPI.Utils
{
    public interface IMailSendService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
