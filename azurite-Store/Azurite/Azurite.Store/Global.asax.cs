using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Azurite.Infrastructure.Config;
using Azurite.Store.Config.NinjectConfig;
using log4net;
using log4net.Config;

namespace Azurite.Store
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            InitializeLogger();
            DependencyResolver.SetResolver(new NinjectDependencyResolver());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutoMapperConfig.RegisterMappings();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var cookie = HttpContext.Current.Request.Cookies["Language"];
            if(cookie != null && cookie.Value != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(cookie.Value);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(cookie.Value);
            } 
            else
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("bg");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("bg");
            }
        }

        private void InitializeLogger()
        {
            var logFilePath = HostingEnvironment.MapPath(@"~/Content/logs");
            var logFileName = "errorLog"; // without the extension
            var fullPath = Path.Combine(logFilePath, logFileName);
            GlobalContext.Properties["LogFileName"] = fullPath;

            // Initialize log4net
            XmlConfigurator.Configure();
        }
    }
}
