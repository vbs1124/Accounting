using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Vserv.Accounting.Core.Models;
using Vserv.Accounting.Core.Resources;

namespace Vserv.Accounting.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Vserv.Accounting.Core.Services.IEmailService" />
    public class EmailService : IEmailService
    {
        /// <summary>
        /// The _SMTP client
        /// </summary>
        private readonly SmtpClient _smtpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService"/> class.
        /// </summary>
        /// <param name="smtpClient">The SMTP client.</param>
        public EmailService(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        /// <summary>
        /// Send plain text email message
        /// </summary>
        /// <param name="emailAddress">Recipient email address</param>
        /// <param name="subject">Email subject</param>
        /// <param name="message">Email message</param>
        /// <exception cref="ArgumentException">
        /// emailAddress
        /// or
        /// subject
        /// or
        /// message
        /// </exception>
        void IEmailService.SendEmail(string emailAddress, string subject, string message)
        {
            #region Validation

            if (String.IsNullOrWhiteSpace(emailAddress))
            {
                throw new ArgumentException(String.Format(Global.CannotBeNullOrEmpy, "emailAddress"), "emailAddress");
            }

            if (String.IsNullOrWhiteSpace(subject))
            {
                throw new ArgumentException(String.Format(Global.CannotBeNullOrEmpy, "subject"), "subject");
            }

            if (String.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException(String.Format(Global.CannotBeNullOrEmpy, "message"), "message");
            }

            #endregion

            // emailAddress may contain a list of email addresses. For example: "user1@mail.com, user2@mail.com"
            // so.. let's split them into an array
            var emailAddresses = emailAddress.Split(',', ';')
                .Where(x => !String.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim()).ToArray();

            using (var mailMessage = new MailMessage())
            {
                foreach (var email in emailAddresses)
                {
                    mailMessage.To.Add(email);
                }

                // Set subject
                mailMessage.Subject = subject;

                // Is it HTML message?
                if (message.Contains("<p>")) // TODO: add more advanced logic here
                {
                    mailMessage.IsBodyHtml = true;
                }

                mailMessage.Body = message;

                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.SubjectEncoding = Encoding.UTF8;
                mailMessage.HeadersEncoding = Encoding.UTF8;

                _smtpClient.Send(mailMessage);
            }
        }

        /// <summary>
        /// Send email message with a template
        /// </summary>
        /// <param name="sendEmailModel">The send email model.</param>
        /// <param name="templateName">Template name. Ex.: "Contact"</param>
        /// <param name="data">Data object for the template. Ex.: new { Name = "John" }</param>
        /// <exception cref="ArgumentException">templateName</exception>
        /// <exception cref="ApplicationException">Missing Website URL property.</exception>
        void IEmailService.SendEmail(SendEmailModel sendEmailModel, string templateName, object data)
        {
            if (String.IsNullOrWhiteSpace(templateName))
            {
                throw new ArgumentException(String.Format(Global.CannotBeNullOrEmpy, "templateName"), "templateName");
            }

            var viewData = data as ViewDataDictionary ?? new ViewDataDictionary { Model = data };
            viewData["SendEmailModel"] = sendEmailModel;

            var tempData = new TempDataDictionary();

            if (String.IsNullOrWhiteSpace(sendEmailModel.WebsiteURL))
            {
                throw new ApplicationException("Missing Website URL property.");
            }

            using (var stringWriter = new StringWriter())
            {
                var httpResponse = new HttpResponse(stringWriter);
                var newHttpContext = new HttpContext(new HttpRequest("/", sendEmailModel.WebsiteURL, ""), httpResponse) { User = new GenericPrincipal(new GenericIdentity(""), null) };
                var controllerContext = new ControllerContext
                {
                    HttpContext = new HttpContextWrapper(newHttpContext),
                    RequestContext = new RequestContext(new HttpContextWrapper(newHttpContext),
                        new RouteData())
                };
                controllerContext.RouteData.Values.Add("controller", "foo");

                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controllerContext,
                                                                                  String.Format(
                                                                                      "~/Views/EmailTemplates/{0}.cshtml",
                                                                                      templateName));
                ViewContext viewContext = new ViewContext(controllerContext, viewResult.View, viewData, tempData,
                                                          httpResponse.Output);
                viewResult.View.Render(viewContext, httpResponse.Output);
                httpResponse.Flush();

                var message = httpResponse.Output.ToString().Trim();

                (this as IEmailService).SendEmail(sendEmailModel.EmailAddress, sendEmailModel.Subject, message);
            }
        }

        /*
        public Task SendEmailAsync(string emailAddress, string title, string template, object data)
        {
            var task = new Task(() => this.SendEmail(emailAddress, title, template, data), TaskCreationOptions.None);
            task.Start();

            // for testing purposes
            return task;
        }
        */
    }
}
