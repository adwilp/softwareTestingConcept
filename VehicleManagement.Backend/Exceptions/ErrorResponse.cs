using Newtonsoft.Json;

namespace VehicleManagement.Backend.Exceptions
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ErrorResponse
    {
        public string? Message { get; }

        public IEnumerable<object>? Data { get; }

        public ErrorResponse(string? message)
        {
            Message = message;
        }

        public ErrorResponse(string? message, IEnumerable<object>? data) : this(message)
        {
            Data = data;
        }
    }
}
