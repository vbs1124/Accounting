
namespace Vserv.Accounting.Core.Models
{
    public class SendEmailModel
    {
        /// <summary>
        /// EmailAddress
        /// </summary>
        public string EmailAddress
        {
            get;
            set;
        }

        /// <summary>
        /// Subject
        /// </summary> 
        public string Subject
        {
            get;
            set;
        }

        /// <summary>
        /// WebsiteURL
        /// </summary> 
        public string WebsiteURL { get; set; }

        /// <summary>
        /// WebsiteTitle
        /// </summary>
        public string WebsiteTitle
        {
            get;
            set;
        }

        /// <summary>
        /// WebsiteUrlName
        /// </summary> 
        public string WebsiteUrlName
        {
            get;
            set;
        }
    }
}
