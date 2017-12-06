using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Azurite.CDN
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Files with Id",
                routeTemplate: "cdn/files/{productId}",
                defaults: new { controller = "Files" }
            );

            config.Routes.MapHttpRoute(
                name: "Files with name",
                routeTemplate: "cdn/files/{*filename}",
                defaults: new { controller = "Files" }
            );

            config.Routes.MapHttpRoute(
                name: "Radianna Gold CDN",
                routeTemplate: "cdn/{controller}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
