namespace ClearXchange.Server.Middleware
{
    using ClearXchange.Server.Model;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using System.IO;
    using System.Threading.Tasks;

    public class ResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Call the next middleware in the pipeline to handle the request
            await _next(context);

            // Get the response model from the response's items (assuming 'Member' model)
            if (context.Items.TryGetValue("ResponseModel", out object responseModelObj) && responseModelObj is Member responseModel)
            {
                // Convert the response model to JSON
                string responseBody = JsonConvert.SerializeObject(responseModel);

                // Write the converted JSON to the response body
                using (StreamWriter writer = new StreamWriter(context.Response.Body))
                {
                    await writer.WriteAsync(responseBody);
                }
            }
        }
    }

}
