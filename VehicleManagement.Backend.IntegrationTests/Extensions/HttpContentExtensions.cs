using System.Text;

using Newtonsoft.Json;

namespace VehicleManagement.Backend.IntegrationTests.Extensions
{
    public static class HttpContentExtensions
    {
        public static StringContent ToJsonContent<T>(this T content)
        {
            return new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
        }
    }
}
