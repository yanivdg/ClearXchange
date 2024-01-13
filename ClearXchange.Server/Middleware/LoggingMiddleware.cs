using System.Text;

namespace ClearXchange.Server.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // Log request details
            LogRequest(context.Request);
           
            // Call the next middleware in the pipeline
            await _next(context);

            // Log response details
            LogResponse(context.Response);
        }

        private void LogRequest(HttpRequest request)
        {
            var requestDetails = new StringBuilder();
            requestDetails.AppendLine($"Request Method: {request.Method}");
            requestDetails.AppendLine($"Request Path: {request.Path}");
            requestDetails.AppendLine($"Request Query String: {request.QueryString}");

            _logger.LogInformation(requestDetails.ToString());
        }

        private void LogResponse(HttpResponse response)
        {
            var responseDetails = new StringBuilder();
            responseDetails.AppendLine($"Response Status Code: {response.StatusCode}");

            _logger.LogInformation(responseDetails.ToString());
        }
    }
}
