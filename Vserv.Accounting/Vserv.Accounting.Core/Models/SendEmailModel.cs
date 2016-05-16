
namespace Vserv.Accounting.Core.Models
{
    public class SendEmailModel
    {
        /// <summary>
        /// EmailAddress
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        public string EmailAddress
        {
            get;
            set;
        }

        /// <summary>
        /// Subject
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject
        {
            get;
            set;
        }

        /// <summary>
        /// WebsiteURL
        /// </summary>
        /// <value>
        /// The website URL.
        /// </value>
        public string WebsiteURL { get; set; }

        /// <summary>
        /// WebsiteTitle
        /// </summary>
        /// <value>
        /// The website title.
        /// </value>
        public string WebsiteTitle
        {
            get;
            set;
        }

        /// <summary>
        /// WebsiteUrlName
        /// </summary>
        /// <value>
        /// The name of the website URL.
        /// </value>
        public string WebsiteUrlName
        {
            get;
            set;
        }
    }
}
