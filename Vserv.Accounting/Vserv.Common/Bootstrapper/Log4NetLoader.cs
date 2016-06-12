using System.IO;
using log4net.Config;

namespace Vserv.Common.Bootstrapper
{
    /// <summary>
    /// 
    /// </summary>
    public static class Log4NetLoader
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public static void Initialize()
        {
            XmlConfigurator.Configure();
        }

        /// <summary>
        /// Initializes the specified XML configuration path.
        /// </summary>
        /// <param name="xmlConfigPath">The XML configuration path.</param>
        public static void Initialize(string xmlConfigPath)
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo(xmlConfigPath));
        }
    }
}
