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

            // todo: creating action for ex1 and ex4
            routes.MapRoute(
                name: "display14",
                url: "display/{IPOrFilename}/{portOrRate}",
                defaults: new { controller = "Display", action = "Index14" });

            // todo: creating action for ex2
            routes.MapRoute(
                name: "display2",
                url: "display/{IP}/{port}/{rate}",
                defaults: new { controller = "Display", action = "Index2" });

            // todo:creating new action for ex3
            routes.MapRoute(
                name: "save",
                url: "save/{IP}/{port}/{rate}/{length}/{filename}",
                defaults: new { controller = "Display", action = "Index3" });

            routes.MapRoute(
                name: "Default",
                url: "display/{IPOrFile}/{param2}/{param3}",
                defaults: new { controller = "Display", action = "Index", param3 = UrlParameter.Optional }
            );
        }
    }
}
