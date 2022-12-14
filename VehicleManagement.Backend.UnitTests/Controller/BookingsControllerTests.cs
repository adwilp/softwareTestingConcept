using System.Collections.Generic;
using System.Linq;
using System.Threading;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using VehicleManagement.Backend.Controllers;
using VehicleManagement.Backend.UnitTests.TestData;
using VehicleManagement.Core.Domains;

using VehicleManagement.DataContracts.DataModels;

namespace VehicleManagement.Backend.UnitTests.Controller
{
    public class BookingsControllerTests
    {
        private readonly Mock<IBookingDomain> _bookingDomainMock;
        private readonly BookingsController _bookingsController;

        public BookingsControllerTests()
        {
            _bookingDomainMock = new Mock<IBookingDomain>();
            _bookingsController = new BookingsController(_bookingDomainMock.Object);
        }

        [Theory]
        [MemberData(nameof(BookingTestData.GetBookingsTestData), MemberType = typeof(BookingTestData))]
        public async Task GetAll_Should_Return_All_Successfull(List<FlatBooking> bookings)
        {
            // ARRANGE
            _bookingDomainMock
                .Setup(md => md.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(bookings);

            // ACT
            var result = await _bookingsController.GetAll(It.IsAny<CancellationToken>());

            // ASSERT
            var response = Assert.IsType<OkObjectResult>(result);
            var body = Assert.IsAssignableFrom<IEnumerable<FlatBooking>>(response.Value);

            Assert.NotNull(body);
            body.Should().BeEquivalentTo(bookings);
            Assert.Equal(bookings.Count, body.Count());
        }
    }
}
