using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Azurite.CDN.Attributes;
using log4net;

namespace Azurite.CDN.Config.Handlers
{
    public class LoggingHandler : DelegatingHandler
    {
        private readonly ILog logger;

        public LoggingHandler(ILog logger)
        {
            this.logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var requestContent = await request.Content.ReadAsStringAsync();
                var logMessage = $"Request {request.Method.Method} {request.RequestUri}; Headers: {SerializeHeaders(request)}; Body: {requestContent};";
                this.logger.Info(logMessage);

                var response = await base.SendAsync(request, cancellationToken);

                if (ShouldLogResponse(request) && response.Content != null)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();   
                        this.logger.Info($"Response: {responseContent};");
                    }
                    else
                    {
                        var httpError = await response.Content.ReadAsAsync<HttpError>();

                        if (httpError != null && (httpError.Count > 0 || httpError.ExceptionMessage != null))
                        {
                            this.logger.Error($"Model State: {httpError.ModelState?.Message}; Exception: {httpError.ExceptionType}; Message: {httpError.ExceptionMessage}; StackTrace: {httpError.StackTrace}");
                        }
                        else
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            this.logger.Error(responseContent);
                        }
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                this.logger.Error("Application error:", ex);

                var errorResponse = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"There was an unexpected error: {ex.Message}")
                };

                return errorResponse;
            };
        }

        private string SerializeHeaders(HttpRequestMessage request)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var header in request.Headers)
            {
                builder.Append($"[{header.Key} - {string.Join(";", header.Value)}]");
            }

            return builder.ToString();
        }

        private bool ShouldLogResponse(HttpRequestMessage request)
        {
            var actionDescriptor = request.GetActionDescriptor();

            // response can be returned before routing is resolved
            if (actionDescriptor != null)
            {
                var attributes = actionDescriptor.GetCustomAttributes<DisableResponseLoggingAttribute>();

                if (attributes != null && attributes.Count > 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}