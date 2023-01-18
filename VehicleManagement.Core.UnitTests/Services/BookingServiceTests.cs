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
        public async Task GetAllAsync_Should_Get_All(List<FlatBooking> bookings)
        {
            // ARRANGE
            _bookingTransactionMock
                .Setup(bt => bt.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(bookings);

            // ACT
            var result = await _bookingService.GetAllAsync(It.IsAny<CancellationToken>());

            // ASSERT
            result.Should().BeEquivalentTo(bookings);
        }

        [Fact]
        public async Task GetAllAsync_Should_Call_Transaction_Once()
        {
            // ACT
            await _bookingService.GetAllAsync(It.IsAny<CancellationToken>());

            // ASSERT
            _bookingTransactionMock.Verify(bt => bt.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Theory]
        [MemberData(nameof(BookingTestData.GetAddBookingTestData), MemberType = typeof(BookingTestData))]
        public async Task AddAsync_Should_Get_New(Booking booking, FlatBooking newBooking)
        {
            // ARRANGE
            _bookingTransactionMock
                .Setup(bt => bt.AddAsync(booking, It.IsAny<CancellationToken>()))
                .ReturnsAsync(newBooking);

            // ACT
            var result = await _bookingService.AddAsync(booking, It.IsAny<CancellationToken>());

            // ASSERT
            Assert.Equal(newBooking.Start, result.Start);
            Assert.Equal(newBooking.End, result.End);
            Assert.Equal(newBooking.EmployeeNumber, result.EmployeeNumber);
            Assert.Equal(newBooking.FIN, result.FIN);

            Assert.Equal(booking.Start, result.Start);
            Assert.Equal(booking.End, result.End);
            Assert.Equal(booking.EmployeeNumber, result.EmployeeNumber);
            Assert.Equal(booking.FIN, result.FIN);
        }

        [Fact]
        public async Task AddAsync_Should_Call_Transaction_Once()
        {
            // ACT
            await _bookingService.AddAsync(It.IsAny<Booking>(), It.IsAny<CancellationToken>());

            // ASSERT
            _bookingTransactionMock.Verify(bt => bt.AddAsync(It.IsAny<Booking>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Theory]
        [MemberData(nameof(BookingTestData.GetUpdateBookingTestData), MemberType = typeof(BookingTestData))]
        public async Task UpdateAsync_Should_Get_New(UpdateableBooking booking, FlatBooking newBooking)
        {
            // ARRANGE
            _bookingTransactionMock
                .Setup(bt => bt.UpdateAsync(booking, It.IsAny<CancellationToken>()))
                .ReturnsAsync(newBooking);

            // ACT
            var result = await _bookingService.UpdateAsync(booking, It.IsAny<CancellationToken>());

            // ASSERT
            Assert.Equal(newBooking.Id, result.Id);
            Assert.Equal(newBooking.Start, result.Start);
            Assert.Equal(newBooking.End, result.End);
            Assert.Equal(newBooking.EmployeeNumber, result.EmployeeNumber);
            Assert.Equal(newBooking.FIN, result.FIN);

            Assert.Equal(booking.Id, result.Id);
            Assert.Equal(booking.Start, result.Start);
            Assert.Equal(booking.End, result.End);
            Assert.Equal(booking.EmployeeNumber, result.EmployeeNumber);
            Assert.Equal(booking.FIN, result.FIN);
        }

        [Fact]
        public async Task UpdateAsync_Should_Call_Transaction_Once()
        {
            // ACT
            await _bookingService.UpdateAsync(It.IsAny<UpdateableBooking>(), It.IsAny<CancellationToken>());

            // ASSERT
            _bookingTransactionMock.Verify(bt => bt.UpdateAsync(It.IsAny<UpdateableBooking>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Theory]
        [MemberData(nameof(BookingTestData.GetBookingTestData), MemberType = typeof(BookingTestData))]
        public async Task GetAsync_Should_Get_UpdateableBooking(int id, UpdateableBooking booking)
        {
            // ARRANGE
            _bookingTransactionMock
                .Setup(bt => bt.GetAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(booking);

            // ACT
            var result = await _bookingService.GetAsync(id, It.IsAny<CancellationToken>());

            // ASSERT
            Assert.Equal(id, result.Id);

            Assert.Equal(booking.Id, result.Id);
            Assert.Equal(booking.Start, result.Start);
            Assert.Equal(booking.End, result.End);
            Assert.Equal(booking.EmployeeNumber, result.EmployeeNumber);
            Assert.Equal(booking.FIN, result.FIN);
        }

        [Fact]
        public async Task GetAsync_Should_Call_Transaction_Once()
        {
            // ACT
            await _bookingService.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>());

            // ASSERT
            _bookingTransactionMock.Verify(bt => bt.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
