//using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using ClearXchange.Server.Services;
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
            try
            {
                // Log request details
                LogRequest(context.Request);

                // Call the next middleware in the pipeline
                await _next(context);

                // Log response details
                LogResponse(context.Response);
            }
            catch (Exception ex)
            {
                // Log the exception details
                _logger.LogError($"Exception: {ex}");

                // Check if it's a validation exception
                if (ex is ValidationException validationException)
                {
                    // Log additional details specific to validation failure
                    _logger.LogError("Validation failed. Details:");
                    foreach (var failure in validationException.Failures)
                    {
                        _logger.LogError($"{failure.Key}: {failure.Value}");
                    }

                    // Return a custom error response to the client
                    context.Response.StatusCode = 400; // Bad Request
                    context.Response.ContentType = "application/json";

                    var errorResponse = new
                    {
                        Message = "Validation failed",
                        Errors = validationException.Failures
                    };

                    var jsonResponse = JsonSerializer.Serialize(errorResponse);
                    await context.Response.WriteAsync(jsonResponse);

                    return; // End the middleware execution
                }

                // Re-throw the exception to allow it to propagate further if needed
                throw;
            }
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
