using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            var response = Assert.IsType<ObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.Created, response.StatusCode);

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

        [Theory]
        [MemberData(nameof(BookingTestData.GetUpdateBookingTestData), MemberType = typeof(BookingTestData))]
        public async Task Update_Should_Return_Updated_Successfull(UpdateableBooking booking, FlatBooking newBooking)
        {
            // ARRANGE
            _bookingDomainMock
                .Setup(bd => bd.UpdateAsync(booking, It.IsAny<CancellationToken>()))
                .ReturnsAsync(newBooking);

            // ACT
            var result = await _bookingsController.Update(booking, It.IsAny<CancellationToken>());

            // ASSERT
            var response = Assert.IsType<OkObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);

            var body = Assert.IsAssignableFrom<FlatBooking>(response.Value);

            Assert.Equal(newBooking.Id, body.Id);
            Assert.Equal(newBooking.Start, body.Start);
            Assert.Equal(newBooking.End, body.End);
            Assert.Equal(newBooking.EmployeeNumber, body.EmployeeNumber);
            Assert.Equal(newBooking.FIN, body.FIN);

            Assert.Equal(booking.Id, body.Id);
            Assert.Equal(booking.Start, body.Start);
            Assert.Equal(booking.End, body.End);
            Assert.Equal(booking.EmployeeNumber, body.EmployeeNumber);
            Assert.Equal(booking.FIN, body.FIN);
        }

        [Theory]
        [MemberData(nameof(BookingTestData.GetBookingTestData), MemberType = typeof(BookingTestData))]
        public async Task Get_Should_Return_Booking_Successfull(int id, UpdateableBooking booking)
        {
            // ARRANGE
            _bookingDomainMock
                .Setup(bd => bd.GetAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(booking);

            // ACT
            var result = await _bookingsController.Get(id, It.IsAny<CancellationToken>());

            // ASSERT
            var response = Assert.IsType<OkObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);

            var body = Assert.IsAssignableFrom<UpdateableBooking>(response.Value);

            Assert.Equal(booking.Id, body.Id);
            Assert.Equal(booking.Start, body.Start);
            Assert.Equal(booking.End, body.End);
            Assert.Equal(booking.EmployeeNumber, body.EmployeeNumber);
            Assert.Equal(booking.FIN, body.FIN);
        }

        [Theory]
        [MemberData(nameof(BookingTestData.GetDeleteTestData), MemberType = typeof(BookingTestData))]
        public async Task Delete_Should_Return_NoContent(int id)
        {
            // ARRANGE
            _bookingDomainMock
                .Setup(bd => bd.DeleteAsync(id, It.IsAny<CancellationToken>()));

            // ACT
            var result = await _bookingsController.Delete(id, It.IsAny<CancellationToken>());

            // ASSERT
            var response = Assert.IsType<NoContentResult>(result);

            Assert.Equal((int)HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
