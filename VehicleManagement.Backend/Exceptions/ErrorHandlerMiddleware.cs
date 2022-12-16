using System.Net;
using System.Text.Json;

using VehicleManagement.DataContracts.Exceptions;

namespace VehicleManagement.Backend.Exceptions
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                IEnumerable<object>? data = null;
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case DataConversionException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case SaveDataException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        data = ((SaveDataException)error).InvalidData;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new ErrorResponse(error.Message, data));
                await response.WriteAsync(result);
            }
        }
    }
}
