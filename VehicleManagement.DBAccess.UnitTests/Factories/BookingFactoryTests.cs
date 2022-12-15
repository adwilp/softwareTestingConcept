using FluentAssertions;

using VehicleManagement.DataContracts.DataModels;
using VehicleManagement.DataContracts.Exceptions;
using VehicleManagement.DBAccess.Entities;
using VehicleManagement.DBAccess.Factories;
using VehicleManagement.DBAccess.UnitTests.TestData;

namespace VehicleManagement.DBAccess.UnitTests.Factories
{
    public class BookingFactoryTests
    {
        private readonly IBookingFactory _bookingFactory;

        public BookingFactoryTests()
        {
            _bookingFactory = new BookingFactory();
        }

        [Theory]
        [MemberData(nameof(BookingTestData.GetSingleBookingTestData), MemberType = typeof(BookingTestData))]
        public void Create_Should_Create_FlatBooking(Booking entity, FlatBooking model)
        {
            // ACT
            var result = _bookingFactory.Create(entity);

            // ASSERT
            Assert.Equal(model.Id, result.Id);
            Assert.Equal(model.Start, result.Start);
            Assert.Equal(model.End, result.End);
            Assert.Equal(model.EmployeeNumber, result.EmployeeNumber);
            Assert.Equal(model.FIN, result.FIN);
            Assert.Equal(model.LicensePlate, result.LicensePlate);
        }

        [Fact]
        public void Create_Should_Throw_Exception_For_Null()
        {
            // ARRANGE
            Booking? entity = null;

            // ASSERT & ACT
            Assert.Throws<DataConversionException>(() => _bookingFactory.Create(entity));
        }

        [Theory]
        [MemberData(nameof(BookingTestData.GetBookingsTestData), MemberType = typeof(BookingTestData))]
        public void Create_Should_Create_FlatVehicle_Enumerable(List<Booking> entity, List<FlatBooking> model)
        {
            // ACT
            var result = _bookingFactory.Create(entity);

            // ASSERT
            result.Should().BeEquivalentTo(model);
        }

        [Fact]
        public void Create_Should_Throw_Exception_For_Null_Enumerable()
        {
            // ARRANGE
            List<Booking>? entities = null;

            // ASSERT & ACT
            Assert.Throws<DataConversionException>(() => _bookingFactory.Create(entities));
        }
    }
}
