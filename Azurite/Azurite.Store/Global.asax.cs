using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using Azurite.Infrastructure.Config;
using Azurite.Store.Config.NinjectConfig;
using System.Web.Routing;

namespace Azurite.Store
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
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
    }
}
