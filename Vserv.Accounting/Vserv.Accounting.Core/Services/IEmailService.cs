using Vserv.Accounting.Core.Models;

namespace Vserv.Accounting.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        void SendEmail(string emailAddress, string title, string message);

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="sendEmailModel">The send email model.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="data">The data.</param>
        void SendEmail(SendEmailModel sendEmailModel, string templateName, object data);
    }
}
