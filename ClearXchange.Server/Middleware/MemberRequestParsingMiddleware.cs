using ClearXchange.Server.Model;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

public class MemberParsingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<MemberParsingMiddleware> _logger;

    public MemberParsingMiddleware(RequestDelegate next, ILogger<MemberParsingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Method == HttpMethods.Post)
        {
            await ParseMemberRequest(context);
        }

        await _next(context);
    }

    private async Task ParseMemberRequest(HttpContext context)
    {
        try
        {
            using (var reader = new StreamReader(context.Request.Body))
            {
                var requestBody = await reader.ReadToEndAsync();

                // Deserialize the request body into your Member data model
                var member = JsonConvert.DeserializeObject<Member>(requestBody);

                //*********************
                //using (StreamReader reader = new StreamReader(context.Request.Body))
                //{
                    //string requestBody = await reader.ReadToEndAsync();

                    // Convert JSON data to your model (assuming 'Member' model)
                    Member requestModel = JsonConvert.DeserializeObject<Member>(requestBody, new JsonSerializerSettings
                    {
                        // Set the datetime format to ISO 8601
                        DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ",
                    });

                    // Set the converted model in the request's items for further processing
                    context.Items["RequestModel"] = requestModel;
                //}
                //*********************

                /*
                // Validate the Member object
                var validationContext = new ValidationContext(member);
                var validationResults = new List<ValidationResult>();
                var isValid = Validator.TryValidateObject(member, validationContext, validationResults, validateAllProperties: true);

                if (isValid)
                {
                    // Now you have your Member object populated with the data from the request
                    _logger.LogInformation($"Parsed Member request: ID = {member.Id}, Name = {member.Name}, " +
                                           $"Email = {member.Email}, DateOfBirth = {member.DateOfBirth}, " +
                                           $"Gender = {member.Gender}, Phone = {member.Phone}");
                }
                else
                {
                    // Log validation errors
                    foreach (var validationResult in validationResults)
                    {
                        foreach (var memberName in validationResult.MemberNames)
                        {
                            var errorMessage = validationResult.ErrorMessage;

                            // Log a detailed error message including what was received and what was expected
                            _logger.LogError($"Validation error for property {memberName} in the received request. " +
                                             $"Received value: {member.GetType().GetProperty(memberName)?.GetValue(member)}, " +
                                             $"Expected: {string.Join(", ", validationResult.MemberNames)} - {errorMessage}");
                        }
                    }

                    // Handle the validation errors or return an appropriate response to the client
                }
                */
            }
                
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error parsing Member request: {ex.Message}");
            // Handle the exception or return an appropriate response to the client
        }
    }

}
