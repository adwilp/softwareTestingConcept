using Newtonsoft.Json;

namespace VehicleManagement.Backend.Exceptions
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ErrorResponse
    {
        public string? Message { get; }

        public object? Data { get; }

        public ErrorResponse(string? message)
        {
            Message = message;
        }

        [JsonConstructor]
        public ErrorResponse(string? message, object? data) : this(message)
        {
            Data = data;
        }
    }
}
