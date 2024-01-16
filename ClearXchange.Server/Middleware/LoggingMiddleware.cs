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
        private async Task LogRequest(HttpRequest request)
        {
            var requestDetails = new StringBuilder();
            requestDetails.AppendLine($"Request Method: {request.Method}");
            requestDetails.AppendLine($"Request Path: {request.Path}");
            requestDetails.AppendLine($"Request Query String: {request.QueryString}");

            // Log headers
            foreach (var header in request.Headers)
            {
                requestDetails.AppendLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }

            // Log body
            if (request.Body.CanSeek)
            {
                request.Body.Seek(0, SeekOrigin.Begin);
                using var reader = new StreamReader(request.Body);
                var body = await reader.ReadToEndAsync();
                requestDetails.AppendLine($"Request Body: {body}");
                request.Body.Seek(0, SeekOrigin.Begin); // Reset the stream so the next middleware can read it
            }

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
