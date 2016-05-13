using Vserv.Accounting.Core.Models;

namespace Vserv.Accounting.Core.Services
{
    public interface IEmailService
    {
        void SendEmail(string emailAddress, string title, string message);

        void SendEmail(SendEmailModel sendEmailModel, string templateName, object data);
    }
}
