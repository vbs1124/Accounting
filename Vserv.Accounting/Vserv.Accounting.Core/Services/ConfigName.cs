using System;

namespace Vserv.Accounting.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ConfigName
    {
        /// <summary>
        /// The name
        /// </summary>
        private readonly String name;

        /// <summary>
        /// The website URL name
        /// </summary>
        public static readonly ConfigName WebsiteUrlName = new ConfigName("WebsiteUrlName");
        /// <summary>
        /// The website title
        /// </summary>
        public static readonly ConfigName WebsiteTitle = new ConfigName("WebsiteTitle");
        /// <summary>
        /// The website URL
        /// </summary>
        public static readonly ConfigName WebsiteUrl = new ConfigName("WebsiteUrl");

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigName"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        private ConfigName(String name)
        {
            this.name = name;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override String ToString()
        {
            return name;
        }
    }
}
