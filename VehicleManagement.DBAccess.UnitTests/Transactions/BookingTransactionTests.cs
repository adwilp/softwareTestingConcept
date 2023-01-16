using System;
using System.Linq.Expressions;
using System.Threading;

using FluentAssertions;

using Moq;

using VehicleManagement.DBAccess.Entities;
using VehicleManagement.DBAccess.Factories;
using VehicleManagement.DBAccess.Repositories;
using VehicleManagement.DBAccess.Transcations;
using VehicleManagement.DBAccess.UnitTests.TestData;

using models = VehicleManagement.DataContracts.DataModels;

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
        public async Task GetAllAsync_Should_Get_All(List<Booking> entities, List<models.FlatBooking> models)
        {
            // ARRANGE
            _bookingRepository
                .Setup(br =>
                    br.GetAllAsync(
                        It.IsAny<CancellationToken>(),
                        It.IsAny<Expression<Func<Booking, bool>>>(),
                        It.IsAny<bool>(),
                        It.IsAny<string[]>()
                    )
                )
                .ReturnsAsync(entities);

            _bookingFactory
                .Setup(bf => bf.Create(entities))
                .Returns(models);

            // ACT
            var result = await _bookingTransaction.GetAllAsync(It.IsAny<CancellationToken>());

            // ASSERT
            result.Should().BeEquivalentTo(models);
        }

        [Theory]
        [MemberData(nameof(BookingTestData.GetAddBookingTestData), MemberType = typeof(BookingTestData))]
        public async Task AddAsync_Should_Add_And_Get_FlatBooking(models.Booking model, Booking entity, Booking newEntity, models.FlatBooking newModel)
        {
            // ARRANGE
            _bookingFactory
                .Setup(bf => bf.Create(model))
                .Returns(entity);

            _bookingRepository
                .Setup(br =>
                    br.AddAsync(entity, It.IsAny<CancellationToken>())
                )
                .ReturnsAsync(newEntity);

            _bookingFactory
                .Setup(vf => vf.Create(newEntity))
                .Returns(newModel);

            // ACT
            var result = await _bookingTransaction.AddAsync(model, It.IsAny<CancellationToken>());

            // ASSERT
            result.Should().BeEquivalentTo(newModel);
        }

        [Fact]
        public async Task AddAsync_Should_Call_Add_From_Repo_Once()
        {
            // ACT
            await _bookingTransaction.AddAsync(It.IsAny<models.Booking>(), It.IsAny<CancellationToken>());

            // ASSERT
            _bookingRepository.Verify(br => br.AddAsync(It.IsAny<Booking>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task AddAsync_Should_Call_ReloadReferences_From_Repo()
        {
            // ACT
            await _bookingTransaction.AddAsync(It.IsAny<models.Booking>(), It.IsAny<CancellationToken>());

            // ASSERT
            _bookingRepository.Verify(br => br.ReloadReferences(It.IsAny<Booking>(), "Vehicle"), Times.Once());
        }


        [Theory]
        [MemberData(nameof(BookingTestData.GetUpdateBookingTestData), MemberType = typeof(BookingTestData))]
        public async Task UpdateAsync_Should_Update_And_Get_FlatBooking(models.UpdateableBooking model, Booking entity, Booking newEntity, models.FlatBooking newModel)
        {
            // ARRANGE
            _bookingFactory
                .Setup(bf => bf.Create(model))
                .Returns(entity);

            _bookingRepository
                .Setup(br =>
                    br.Update(entity)
                )
                .Returns(newEntity);

            _bookingFactory
                .Setup(vf => vf.Create(newEntity))
                .Returns(newModel);

            // ACT
            var result = await _bookingTransaction.UpdateAsync(model, It.IsAny<CancellationToken>());

            // ASSERT
            result.Should().BeEquivalentTo(newModel);
        }

        [Fact]
        public async Task UpdateAsync_Should_Call_Update_From_Repo_Once()
        {
            // ACT
            await _bookingTransaction.UpdateAsync(It.IsAny<models.UpdateableBooking>(), It.IsAny<CancellationToken>());

            // ASSERT
            _bookingRepository.Verify(br => br.Update(It.IsAny<Booking>()), Times.Once());
        }

        [Fact]
        public async Task UpdateAsync_Should_Call_ReloadReferences_From_Repo()
        {
            // ACT
            await _bookingTransaction.UpdateAsync(It.IsAny<models.UpdateableBooking>(), It.IsAny<CancellationToken>());

            // ASSERT
            _bookingRepository.Verify(br => br.ReloadReferences(It.IsAny<Booking>(), "Vehicle"), Times.Once());
        }
    }
}
