using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web_Rest_DvirAriel
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // todo: creating action for ex1
            routes.MapRoute(
                name: "display1",
                url: "display/{IP1}.{IP2}.{IP3}.{IP4}/{port}",
                defaults: new { controller = "Display", action = "Index1" });

            // todo: creating action for ex2
            routes.MapRoute(
                name: "display2",
                url: "display/{IP}/{port}/{rate}",
                defaults: new { controller = "Display", action = "Index2" });
            
            routes.MapRoute(
                name: "save",
                url: "save/{IP}/{port}/{rate}/{length}/{filename}",
                defaults: new { controller = "Display", action = "Index3" });

            routes.MapRoute(
                name: "display4",
                url: "display/{fileName}/{rate}",
                defaults: new { controller = "Display", action = "Index4" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Display", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
