using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Vserv.Accounting.Common;
using Vserv.Accounting.Web.Code.Unity;
using Vserv.Common.Bootstrapper;
using Vserv.Common.Utils;

namespace Vserv.Accounting.Web
{
    public class MvcApplication : HttpApplication, IUnityContainerAccessor
    {
        #region IUnityContainerAccessor

        private static UnityContainer container;

        public static IUnityContainer Container
        {
            get { return container; }
        }

        IUnityContainer IUnityContainerAccessor.Container
        {
            get { return Container; }
        }

        #endregion

        protected void Application_Start()
        {
            if (container == null)
            {
                container = new UnityContainer();
                ContainerConfig.RegisterTypes(container);
            }

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Log4NetLoader.Initialize(Server.MapPath(ApplicationConfiguration.GetApplicationConfiguration(CommonConstants.LOG4_NET_CONFIG_KEY_NAME)));
        }
    }
}
