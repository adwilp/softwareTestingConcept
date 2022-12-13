using VehicleManagement.Backend.Exceptions;

namespace VehicleManagement.Backend.UnitTests.Exceptions
{
    public class ErrorResponseTests
    {
        [Theory]
        [InlineData("I am a error message!")]
        [InlineData("")]
        [InlineData(null)]
        public void Constructor_Should_Map_Message(string message)
        {
            // ACT
            var result = new ErrorResponse(message);

            // ASSERT
            Assert.Equal(message, result.Message);
        }
    }
}
