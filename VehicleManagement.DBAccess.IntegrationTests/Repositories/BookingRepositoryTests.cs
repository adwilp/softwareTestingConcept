using System.Linq;
using System.Threading;

using Moq;

using VehicleManagement.DBAccess.Entities;
using VehicleManagement.DBAccess.IntegrationTests.DBFixtures;
using VehicleManagement.DBAccess.IntegrationTests.TestData;
using VehicleManagement.DBAccess.Repositories;

namespace VehicleManagement.DBAccess.IntegrationTests.Repositories
{
    public class BookingRepositoryTests
    {
        private readonly IBookingRepository _repository;

        public BookingRepositoryTests()
        {
            var dbFixture = new BookingDBFixture();
            var context = dbFixture.CreateContext();

            _repository = new BookingRepository(context);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_All()
        {
            // ACT
            var result = await _repository.GetAllAsync(It.IsAny<CancellationToken>(), asNoTracking: It.IsAny<bool>(), includedPaths: "Vehicle");
            var resultList = result.ToList();

            // ASSERT
            Assert.Equal(4, resultList.Count);

            Assert.Equal(1, resultList[0].Id);
            Assert.Equal(new DateTime(2022, 12, 14, 10, 50, 12), resultList[0].Start);
            Assert.Equal(new DateTime(2022, 12, 16, 10, 0, 0), resultList[0].End);
            Assert.Equal("12345", resultList[0].EmployeeNumber);
            Assert.Equal("SB164ABN1PE082986", resultList[0].FIN);
            Assert.Equal("MI-XY-666", resultList[0].Vehicle.LicensePlate);

            Assert.Equal(2, resultList[1].Id);
            Assert.Equal(new DateTime(2022, 10, 14, 10, 50, 12), resultList[1].Start);
            Assert.Equal(new DateTime(2022, 10, 16, 10, 0, 0), resultList[1].End);
            Assert.Equal("12345", resultList[1].EmployeeNumber);
            Assert.Equal("SB164ABN1PE082986", resultList[1].FIN);
            Assert.Equal("MI-XY-666", resultList[1].Vehicle.LicensePlate);

            Assert.Equal(3, resultList[2].Id);
            Assert.Equal(new DateTime(2022, 12, 14, 10, 50, 12), resultList[2].Start);
            Assert.Equal(new DateTime(2022, 12, 16, 10, 0, 0), resultList[2].End);
            Assert.Equal("54321", resultList[2].EmployeeNumber);
            Assert.Equal("SB164ABN1PE082096", resultList[2].FIN);
            Assert.Equal("DH-IL-12", resultList[2].Vehicle.LicensePlate);

            Assert.Equal(4, resultList[3].Id);
            Assert.Equal(new DateTime(2022, 12, 14, 10, 50, 12), resultList[3].Start);
            Assert.Equal(new DateTime(2022, 12, 16, 10, 0, 0), resultList[3].End);
            Assert.Equal("654789", resultList[3].EmployeeNumber);
            Assert.Equal("SB189ABN1PE034986", resultList[3].FIN);
            Assert.Equal("VEC-KL-234", resultList[3].Vehicle.LicensePlate);
        }

        [Theory]
        [MemberData(nameof(BookingTestData.GetAddTestData), MemberType = typeof(BookingTestData))]
        public async Task AddAsync_Should_Add_And_Return(Booking booking, Booking newBooking)
        {
            // ACT
            var result = await _repository.AddAsync(booking, It.IsAny<CancellationToken>());
            await _repository.SaveAsync(It.IsAny<CancellationToken>());

            var resultList = await _repository.GetAllAsync(It.IsAny<CancellationToken>());
            var bookings = resultList.ToList();

            // ASSERT
            Assert.Equal(newBooking.Id, result.Id);
            Assert.Equal(newBooking.Start, result.Start);
            Assert.Equal(newBooking.End, result.End);
            Assert.Equal(newBooking.EmployeeNumber, result.EmployeeNumber);
            Assert.Equal(newBooking.FIN, result.FIN);

            Assert.Equal(5, bookings.Count);
            Assert.Equal(newBooking.Id, bookings[4].Id);
            Assert.Equal(newBooking.Start, bookings[4].Start);
            Assert.Equal(newBooking.End, bookings[4].End);
            Assert.Equal(newBooking.EmployeeNumber, bookings[4].EmployeeNumber);
            Assert.Equal(newBooking.FIN, bookings[4].FIN);
        }

        [Theory]
        [MemberData(nameof(BookingTestData.GetReloadReferencesTestData), MemberType = typeof(BookingTestData))]
        public async Task ReloadReferences_Should_Load_References(Booking booking)
        {
            // ACT
            var result = await _repository.GetAsync(It.IsAny<CancellationToken>(), b => b.Id == booking.Id);
            await _repository.ReloadReferences(result, "Vehicle");

            // ASSERT
            Assert.NotNull(result.Vehicle);

            Assert.Equal(booking.Vehicle.FIN, result.Vehicle.FIN);
            Assert.Equal(booking.Vehicle.LicensePlate, result.Vehicle.LicensePlate);
            Assert.Equal(booking.Vehicle.Color, result.Vehicle.Color);
            Assert.Equal(booking.Vehicle.Mileage, result.Vehicle.Mileage);
            Assert.Equal(booking.Vehicle.ManufacturerId, result.Vehicle.ManufacturerId);
        }
    }
}
