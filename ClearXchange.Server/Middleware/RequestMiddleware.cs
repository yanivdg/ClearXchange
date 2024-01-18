
using ClearXchange.Server.Model;
using Newtonsoft.Json;

namespace ClearXchange.Server.Middleware
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Read the request body
            using (StreamReader reader = new StreamReader(context.Request.Body))
            {
                string requestBody = await reader.ReadToEndAsync();

                // Convert JSON data to your model (assuming 'Member' model)
                Member requestModel = JsonConvert.DeserializeObject<Member>(requestBody, new JsonSerializerSettings
                {
                    // Set the datetime format to ISO 8601
                    DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ",
                });

                // Set the converted model in the request's items for further processing
                context.Items["RequestModel"] = requestModel;
            }

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
