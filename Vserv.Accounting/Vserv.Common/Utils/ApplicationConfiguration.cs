using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Vserv.Common.Utils
{
    public static class ApplicationConfiguration
    {
        public static string GetApplicationConfiguration(string keyName)
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains(keyName))
                return ConfigurationManager.AppSettings[keyName];
            else
                throw new KeyNotFoundException(string.Format("Attempt to read the ConfigurationKey {0} failed because it does not exist", keyName));
        }
    }
}
