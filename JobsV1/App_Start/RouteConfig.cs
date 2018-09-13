using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JobsV1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("{*x}", new { x = @".*\.asmx(/.*)?" });


            /********************************
            * landing/home page
            ********************************/
            //routes.MapRoute(
            //    name: "myHome",
            //    url: "MainGeneric/Index/{id}",
            //    defaults: new { controller = "MainGeneric", action = "Index", id = UrlParameter.Optional }
            //);

            /********************************
            * Generic Default page
            ********************************/
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "MainGeneric", action = "Index", id = UrlParameter.Optional }
            //);

            /********************************
            * landing/home page
            ********************************/
            routes.MapRoute(
                name: "myHome",
                url: "CarRental/Index/{id}",
                defaults: new { controller = "CarRental", action = "Index", id = UrlParameter.Optional }
            );
            /********************************
            *AJ88 car rental default
            ********************************/
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "CarRental", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}
