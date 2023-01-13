using FluentAssertions;

using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

using VehicleManagement.Backend.Exceptions;
using VehicleManagement.Backend.IntegrationTests.Extensions;
using VehicleManagement.Backend.IntegrationTests.TestData;
using VehicleManagement.Backend.IntegrationTests.Utilities;
using VehicleManagement.Core.Domains;
using VehicleManagement.DataContracts.DataModels;
using VehicleManagement.DataContracts.Exceptions;

namespace VehicleManagement.Backend.IntegrationTests.Controller
{
    public class BookingsControllerTests : IClassFixture<VehicleManagementTestServer>
    {
        private const string _baseUrl = "api/Bookings";

        private readonly Mock<IBookingDomain> _bookingDomainMock;
        private readonly HttpClient _httpClient;

        public BookingsControllerTests(VehicleManagementTestServer factory)
        {
            _bookingDomainMock = new Mock<IBookingDomain>();
            _httpClient = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped(provider => _bookingDomainMock.Object);
                });
            })
            .CreateClient();
        }

        [Theory]
        [MemberData(nameof(BookingTestData.GetBookingsTestData), MemberType = typeof(BookingTestData))]
        public async Task GetAll_Should_Return_Ok_Result(List<FlatBooking> bookings)
        {
            // ARRANGE
            _bookingDomainMock
                .Setup(bd => bd.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(bookings);

            // ACT
            var response = await _httpClient.GetAsync(_baseUrl);

            // ASSERT
            HttpAssertions.AssertSuccess(response);

            var body = await response.GetBodyAs<IEnumerable<FlatBooking>>();

            Assert.NotNull(body);
            body.Should().BeEquivalentTo(bookings);
            Assert.Equal(bookings.Count, body.Count());
        }

        [Fact]
        public async Task GetAll_With_DataConversionException_Should_Return_BadRequest()
        {
            // ARRANGE
            _bookingDomainMock
                .Setup(bd => bd.GetAllAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new DataConversionException(It.IsAny<string>(), It.IsAny<string>()));

            // ACT
            var response = await _httpClient.GetAsync(_baseUrl);

            // ASSERT
            HttpAssertions.AssertBadRequest(response);

            var body = await response.GetBodyAs<ErrorResponse>();

            Assert.NotNull(body);
        }

        [Theory]
        [MemberData(nameof(BookingTestData.GetAddBookingTestData), MemberType = typeof(BookingTestData))]
        public async Task Add_Should_Return_Ok_Result(Booking booking, FlatBooking newBooking)
        {
            // ARRANGE
            _bookingDomainMock
                .Setup(vd => vd.AddAsync(
                    It.Is<Booking>(b =>
                        b.Start == booking.Start &&
                        b.End == booking.End &&
                        b.EmployeeNumber == booking.EmployeeNumber &&
                        b.FIN == booking.FIN
                    ),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(newBooking);

            // ACT
            var response = await _httpClient.PostAsync(_baseUrl, booking.ToJsonContent());

            // ASSERT
            HttpAssertions.AssertSuccess(response);

            var body = await response.GetBodyAs<FlatBooking>();

            Assert.NotNull(body);

            Assert.Equal(newBooking.Start, body.Start);
            Assert.Equal(newBooking.End, body.End);
            Assert.Equal(newBooking.EmployeeNumber, body.EmployeeNumber);
            Assert.Equal(newBooking.FIN, body.FIN);

            Assert.Equal(booking.Start, body.Start);
            Assert.Equal(booking.End, body.End);
            Assert.Equal(booking.EmployeeNumber, body.EmployeeNumber);
            Assert.Equal(booking.FIN, body.FIN);
        }

        [Fact]
        public async Task Add_With_DataConversionException_Should_Return_BadRequest()
        {
            // ARRANGE
            _bookingDomainMock
                .Setup(bd => bd.AddAsync(It.IsAny<Booking>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new DataConversionException(It.IsAny<string>(), It.IsAny<string>()));

            // ACT
            var response = await _httpClient.PostAsync(_baseUrl, new Booking().ToJsonContent());

            // ASSERT
            HttpAssertions.AssertBadRequest(response);

            var body = await response.GetBodyAs<ErrorResponse>();

            Assert.NotNull(body);
        }
    }
}
