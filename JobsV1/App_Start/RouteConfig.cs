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


            #region wordpress links
            routes.MapRoute(
                name: "van-for-rent",
                url: "{controller}/van-for-rent",
                defaults: new { controller = "CarRental", action = "CarDetail", unitid = 1 }
            );
            routes.MapRoute(
                name: "NissanUrvanPremium-for-rent",
                url: "{controller}/NissanUrvanPremium-for-rent",
                defaults: new { controller = "CarRental", action = "CarDetail", unitid = 2 }
            );
            routes.MapRoute(
                name: "suvpickup4x4-rental-rates",
                url: "carrental/suvpickup4x4-rental-rates",
                defaults: new { controller = "CarRental", action = "CarDetail", unitid = 3 }
            );
            routes.MapRoute(
                name: "toyota-innova-for-rent",
                url: "{controller}/toyota-innova-for-rent",
                defaults: new { controller = "CarRental", action = "CarDetail", unitid = 4 }
            );
            routes.MapRoute(
                name: "Sedan-rental",
                url: "carrental/sedan-rental",
                defaults: new { controller = "CarRental", action = "CarDetail", unitid = 5 }
            );
            routes.MapRoute(
                name: "Pickup-rental",
                url: "carrental/pickup-rental",
                defaults: new { controller = "CarRental", action = "CarDetail", unitid = 6 }
            );

            #endregion


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


            routes.MapRoute(
                name: "Sitemapxml",
                url: "Sitemap.xml",
                defaults: new { controller = "Home", action = "SitemapXml" }
                );
        }
    }
}
