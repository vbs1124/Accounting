﻿using System.Web.Mvc;
using System.Web.Routing;

namespace Vserv.Accounting.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //enabling attribute routing
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Templates",
                url: "{feature}/Template/{name}",
                defaults: new { controller = "Template", action = "Render" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
