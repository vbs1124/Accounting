using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Vserv.Accounting.Web.Code.Unity;

namespace Vserv.Accounting.Web
{
    public class MvcApplication : System.Web.HttpApplication, IUnityContainerAccessor
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

            Vserv.Common.Bootstrapper.Log4NetLoader.Initialize(Server.MapPath(Vserv.Common.Utils.ApplicationConfiguration.GetApplicationConfiguration(Vserv.Accounting.Common.Constants.CommonConstants.Log4NetConfigKeyName)));
        }
    }
}
