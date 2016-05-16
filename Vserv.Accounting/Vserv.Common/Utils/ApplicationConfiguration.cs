using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Vserv.Common.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApplicationConfiguration
    {
        /// <summary>
        /// Gets the application configuration.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public static string GetApplicationConfiguration(string keyName)
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains(keyName))
                return ConfigurationManager.AppSettings[keyName];
            else
                throw new KeyNotFoundException(string.Format("Attempt to read the ConfigurationKey {0} failed because it does not exist", keyName));
        }
    }
}
