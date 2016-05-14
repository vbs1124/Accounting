using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vserv.Common.Bootstrapper
{
    public static class Log4NetLoader
    {
        public static void Initialize()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static void Initialize(string xmlConfigPath)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(xmlConfigPath));
        }
    }
}
