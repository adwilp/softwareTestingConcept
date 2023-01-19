using System.Linq;
using System.Threading;

using FluentAssertions;

using Moq;

using VehicleManagement.DataContracts.Exceptions;
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
        [MemberData(nameof(BookingTestData.GetFailAddTestData), MemberType = typeof(BookingTestData))]
        public async Task AddAsync_Should_Throw_SaveDataException_On_Save_Error(Booking booking)
        {
            // ACT
            await _repository.AddAsync(booking, It.IsAny<CancellationToken>());

            // ACT & ASSERT
            var exception = await Assert.ThrowsAsync<SaveDataException>(() => _repository.SaveAsync(It.IsAny<CancellationToken>()));

            Booking invalidBooking = (Booking)exception.InvalidData.First();

            Assert.NotNull(invalidBooking);
            Assert.Equal(booking.Start, invalidBooking.Start);
            Assert.Equal(booking.End, invalidBooking.End);
            Assert.Equal(booking.EmployeeNumber, invalidBooking.EmployeeNumber);
            Assert.Equal(booking.FIN, invalidBooking.FIN);
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

        [Theory]
        [MemberData(nameof(BookingTestData.GetUpdateTestData), MemberType = typeof(BookingTestData))]
        public async Task Update_Should_Add_And_Return(Booking booking, Booking newBooking)
        {
            // ACT
            var result = _repository.Update(booking);
            await _repository.SaveAsync(It.IsAny<CancellationToken>());

            var queryResult = await _repository.GetAsync(It.IsAny<CancellationToken>(), b => b.Id == newBooking.Id);

            // ASSERT
            Assert.Equal(newBooking.Id, result.Id);
            Assert.Equal(newBooking.Start, result.Start);
            Assert.Equal(newBooking.End, result.End);
            Assert.Equal(newBooking.EmployeeNumber, result.EmployeeNumber);
            Assert.Equal(newBooking.FIN, result.FIN);

            Assert.Equal(newBooking.Id, queryResult.Id);
            Assert.Equal(newBooking.Start, queryResult.Start);
            Assert.Equal(newBooking.End, queryResult.End);
            Assert.Equal(newBooking.EmployeeNumber, queryResult.EmployeeNumber);
            Assert.Equal(newBooking.FIN, queryResult.FIN);
        }

        [Theory]
        [MemberData(nameof(BookingTestData.GetFailUpdateTestData), MemberType = typeof(BookingTestData))]
        public async Task Update_Should_Throw_SaveDataException_On_Save_Error(Booking booking)
        {
            // ACT
            _repository.Update(booking);

            // ACT & ASSERT
            var exception = await Assert.ThrowsAsync<SaveDataException>(() => _repository.SaveAsync(It.IsAny<CancellationToken>()));

            Booking invalidBooking = (Booking)exception.InvalidData.First();

            Assert.NotNull(invalidBooking);
            Assert.Equal(booking.Id, invalidBooking.Id);
            Assert.Equal(booking.Start, invalidBooking.Start);
            Assert.Equal(booking.End, invalidBooking.End);
            Assert.Equal(booking.EmployeeNumber, invalidBooking.EmployeeNumber);
            Assert.Equal(booking.FIN, invalidBooking.FIN);
        }

        [Theory]
        [MemberData(nameof(BookingTestData.GetTestData), MemberType = typeof(BookingTestData))]
        public async Task GetASync_Should_Get_Correct_Booking(int id, Booking? booking)
        {
            // ACT
            var entity = await _repository.GetAsync(It.IsAny<CancellationToken>(), b => b.Id == id, true);

            // ASSERT
            entity.Should().BeEquivalentTo(booking);
        }
    }
}
