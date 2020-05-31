using System.IO;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Azurite.Storehouse.Config.NinjectConfig;
using log4net;
using log4net.Config;

namespace Azurite.Storehouse
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
