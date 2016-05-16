using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// Initializes the specified XML configuration path.
        /// </summary>
        /// <param name="xmlConfigPath">The XML configuration path.</param>
        public static void Initialize(string xmlConfigPath)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(xmlConfigPath));
        }
    }
}
