using Newtonsoft.Json;

namespace VehicleManagement.Backend.Exceptions
{
    /// <summary>
    /// CLR object to store an error response.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ErrorResponse
    {
        public string? Message { get; }

        public object? Data { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResponse"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ErrorResponse(string? message)
        {
            Message = message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResponse"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="data">The data.</param>
        [JsonConstructor]
        public ErrorResponse(string? message, object? data) : this(message)
        {
            Data = data;
        }
    }
}
