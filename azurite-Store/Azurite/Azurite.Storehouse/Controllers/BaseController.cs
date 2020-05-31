using System.Web.Mvc;
using log4net;

namespace Azurite.Storehouse.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ILog logger;

        public BaseController(ILog logger)
        {
            this.logger = logger;
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext != null && filterContext.Exception != null)
            {
                this.logger.Error("Unexpected exception: ", filterContext.Exception);
            }
        }
    }
}