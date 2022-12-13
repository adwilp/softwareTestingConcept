using System.Net;

namespace VehicleManagement.Backend.IntegrationTests.Utilities
{
    public static class HttpAssertions
    {
        public static void AssertSuccess(HttpResponseMessage response)
        {
            Assert.True(response.IsSuccessStatusCode, ErrorMessage(response.StatusCode));
        }

        public static void AssertBadRequest(HttpResponseMessage response)
        {
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest, ErrorMessage(response.StatusCode));
        }

        private static string ErrorMessage(HttpStatusCode code)
        {
            return $"Actual status code: {code}";
        }
    }
}
