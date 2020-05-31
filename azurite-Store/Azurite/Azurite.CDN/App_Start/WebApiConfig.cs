using System.Web.Http;
using Azurite.CDN.Config.Handlers;
using log4net;

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

            var logger = LogManager.GetLogger("ApplicationLogger");
            config.MessageHandlers.Add(new LoggingHandler(logger));
        }
    }
}
