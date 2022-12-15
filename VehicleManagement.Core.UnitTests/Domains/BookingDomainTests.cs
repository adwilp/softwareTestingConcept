using System.Collections.Generic;
using System.Threading;

using FluentAssertions;

using VehicleManagement.Core.Domains;
using VehicleManagement.Core.Services;
using VehicleManagement.Core.UnitTests.TestData;
using VehicleManagement.DataContracts.DataModels;

namespace VehicleManagement.Core.UnitTests.Domains
{
    public class BookingDomainTests
    {
        private readonly Mock<IBookingService> _bookingServiceMock;
        private readonly IBookingDomain _bookingDomain;

        public BookingDomainTests()
        {
            _bookingServiceMock = new Mock<IBookingService>();
            _bookingDomain = new BookingDomain(_bookingServiceMock.Object);
        }

        [Theory]
        [MemberData(nameof(BookingTestData.GetBookingsTestData), MemberType = typeof(BookingTestData))]
        public async Task GetAllAsync_Should_Return_All(List<FlatBooking> bookings)
        {
            // ARRANGE
            _bookingServiceMock
                .Setup(vs => vs.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(bookings);

            // ACT
            var result = await _bookingDomain.GetAllAsync(It.IsAny<CancellationToken>());

            // ASSERT
            result.Should().BeEquivalentTo(bookings);
        }

        [Fact]
        public async Task GetAllAsync_Should_Call_Service_Once()
        {
            // ACT
            await _bookingDomain.GetAllAsync(It.IsAny<CancellationToken>());

            // ASSERT
            _bookingServiceMock.Verify(vs => vs.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
