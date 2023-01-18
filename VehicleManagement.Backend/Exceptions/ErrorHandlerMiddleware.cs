using System.Net;

using Newtonsoft.Json;

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
                object? data = null;
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
                    case EntityNotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        data = ((EntityNotFoundException)error).RequestedData;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonConvert.SerializeObject(
                    new ErrorResponse(error.Message, data),
                    new JsonSerializerSettings()
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore,
                    });
                await response.WriteAsync(result);
            }
        }
    }
}
