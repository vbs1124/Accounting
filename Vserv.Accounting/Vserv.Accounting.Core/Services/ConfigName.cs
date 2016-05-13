using System;

namespace Vserv.Accounting.Core.Services
{
    public sealed class ConfigName
    {
        private readonly String name;

        public static readonly ConfigName WebsiteUrlName = new ConfigName("WebsiteUrlName");
        public static readonly ConfigName WebsiteTitle = new ConfigName("WebsiteTitle");
        public static readonly ConfigName WebsiteUrl = new ConfigName("WebsiteUrl");

        private ConfigName(String name)
        {
            this.name = name;
        }

        public override String ToString()
        {
            return name;
        }
    }
}
