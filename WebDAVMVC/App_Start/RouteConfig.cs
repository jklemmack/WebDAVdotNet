using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebDAVMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            MyEngineFactory myFactory = new MyEngineFactory();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // This route accepts all 
            routes.MapRoute(
                name: "WebDAV",
                url: "{*resource}",
                defaults: new { 
                    controller = "WebDAV", 
                    action = "WebDAVMethod", 
                    resource = UrlParameter.Optional, 
                    factory = myFactory 
                }
                ,namespaces: new[] { "WebDAVdotNet.WebDAVController" }
                ,constraints: null
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}