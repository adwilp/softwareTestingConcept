using System;
using System.Linq.Expressions;
using System.Threading;

using FluentAssertions;

using Moq;

using VehicleManagement.DataContracts.DataModels;
using VehicleManagement.DBAccess.Entities;
using VehicleManagement.DBAccess.Factories;
using VehicleManagement.DBAccess.Repositories;
using VehicleManagement.DBAccess.Transcations;
using VehicleManagement.DBAccess.UnitTests.TestData;

namespace VehicleManagement.DBAccess.UnitTests.Transactions
{
    public class BookingTransactionTests
    {
        private readonly Mock<IBookingRepository> _bookingRepository;
        private readonly Mock<IBookingFactory> _bookingFactory;
        private readonly IBookingTransaction _bookingTransaction;

        public BookingTransactionTests()
        {
            _bookingRepository = new Mock<IBookingRepository>();
            _bookingFactory = new Mock<IBookingFactory>();
            _bookingTransaction = new BookingTransaction(_bookingFactory.Object, _bookingRepository.Object);
        }

        [Theory]
        [MemberData(nameof(BookingTestData.GetBookingsTestData), MemberType = typeof(BookingTestData))]
        public async Task GetAllAsync_Should_Get_All(List<Booking> entities, List<FlatBooking> models)
        {
            // ARRANGE
            _bookingRepository
                .Setup(vr =>
                    vr.GetAllAsync(
                        It.IsAny<CancellationToken>(),
                        It.IsAny<Expression<Func<Booking, bool>>>(),
                        It.IsAny<bool>(),
                        It.IsAny<string[]>()
                    )
                )
                .ReturnsAsync(entities);

            _bookingFactory
                .Setup(vf => vf.Create(entities))
                .Returns(models);

            // ACT
            var result = await _bookingTransaction.GetAllAsync(It.IsAny<CancellationToken>());

            // ASSERT
            result.Should().BeEquivalentTo(models);
        }
    }
}
