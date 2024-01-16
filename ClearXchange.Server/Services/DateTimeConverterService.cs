using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClearXchange.Server.Services
{
    public class DateTimeConverterService : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Implement the logic to convert a JSON representation to a DateTime object
            if (reader.TokenType == JsonTokenType.String && DateTime.TryParse(reader.GetString(), out var dateTime))
            {
                return dateTime;
            }

            throw new JsonException($"Unable to convert {reader.TokenType} to DateTime.");
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            // Implement the logic to convert a DateTime object to a JSON representation
            writer.WriteStringValue(value.ToUniversalTime().ToString("yyyy-MM-dd"));
        }
    }
}
