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
                .Setup(bd => bd.GetAllAsync(It.IsAny<CancellationToken>()))
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

        [Theory]
        [MemberData(nameof(BookingTestData.GetAddBookingTestData), MemberType = typeof(BookingTestData))]
        public async Task Add_Should_Return_New_Successfull(Booking booking, FlatBooking newBooking)
        {
            // ARRANGE
            _bookingDomainMock
                .Setup(bd => bd.AddAsync(booking, It.IsAny<CancellationToken>()))
                .ReturnsAsync(newBooking);

            // ACT
            var result = await _bookingsController.Add(booking, It.IsAny<CancellationToken>());

            // ASSERT
            var response = Assert.IsType<OkObjectResult>(result);
            var body = Assert.IsAssignableFrom<FlatBooking>(response.Value);

            Assert.Equal(newBooking.Start, body.Start);
            Assert.Equal(newBooking.End, body.End);
            Assert.Equal(newBooking.EmployeeNumber, body.EmployeeNumber);
            Assert.Equal(newBooking.FIN, body.FIN);

            Assert.Equal(booking.Start, body.Start);
            Assert.Equal(booking.End, body.End);
            Assert.Equal(booking.EmployeeNumber, body.EmployeeNumber);
            Assert.Equal(booking.FIN, body.FIN);
        }
    }
}
