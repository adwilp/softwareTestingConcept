namespace VehicleManagement.Backend.Exceptions
{
    public class ErrorResponse
    {
        public string? Message { get; set; }

        public ErrorResponse(string? message)
        {
            Message = message;
        }
    }
}
