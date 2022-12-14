using System.Collections.Generic;
using System.Threading;

using FluentAssertions;

using VehicleManagement.Core.Services;
using VehicleManagement.Core.UnitTests.TestData;
using VehicleManagement.DataContracts.DataModels;

using VehicleManagement.DBAccess.Transcations;

namespace VehicleManagement.Core.UnitTests.Services
{
    public class BookingServiceTests
    {
        private readonly Mock<IBookingTransaction> _bookingTransactionMock;
        private readonly IBookingService _bookingService;

        public BookingServiceTests()
        {
            _bookingTransactionMock = new Mock<IBookingTransaction>();
            _bookingService = new BookingService(_bookingTransactionMock.Object);
        }

        [Theory]
        [MemberData(nameof(BookingTestData.GetBookingsTestData), MemberType = typeof(BookingTestData))]
        public async Task GetAllAsync_Should_Get_All(List<FlatBooking> vehicles)
        {
            // ARRANGE
            _bookingTransactionMock
                .Setup(vt => vt.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(vehicles);

            // ACT
            var result = await _bookingService.GetAllAsync(It.IsAny<CancellationToken>());

            // ASSERT
            result.Should().BeEquivalentTo(vehicles);
        }

        [Fact]
        public async Task GetAllAsync_Should_Call_Transaction_Once()
        {
            // ACT
            await _bookingService.GetAllAsync(It.IsAny<CancellationToken>());

            // ASSERT
            _bookingTransactionMock.Verify(vt => vt.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
