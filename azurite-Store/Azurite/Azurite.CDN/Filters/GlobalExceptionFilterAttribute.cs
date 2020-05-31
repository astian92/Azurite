using System.Web.Http.Filters;
using log4net;

namespace Azurite.CDN.Filters
{
    public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILog logger;

        public GlobalExceptionFilterAttribute()
        {
            this.logger = LogManager.GetLogger("GlobalHandler");
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception != null)
            {
                this.logger.Error("An unexpected exception ocurred: ", actionExecutedContext.Exception);
            }
        }
    }
}