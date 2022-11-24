using System.Collections.Generic;
using System.Threading;

using FluentAssertions;

using VehicleManagement.Core.Services;
using VehicleManagement.Core.UnitTests.TestData;
using VehicleManagement.DataContracts.DataModels;
using VehicleManagement.DBAccess.Transcations;

namespace VehicleManagement.Core.UnitTests.Services
{
    public class VehicleServiceTests
    {
        private readonly Mock<IVehicleTransaction> _vehicleTransactionMock;
        private readonly IVehicleService _vehicleService;

        public VehicleServiceTests()
        {
            _vehicleTransactionMock = new Mock<IVehicleTransaction>();
            _vehicleService = new VehicleService(_vehicleTransactionMock.Object);
        }

        [Theory]
        [MemberData(nameof(VehicleTestData.GetVehiclesTestData), MemberType = typeof(VehicleTestData))]
        public async Task GetAllAsync_Should_Get_All(List<FlatVehicle> vehicles)
        {
            // ARRANGE
            _vehicleTransactionMock
                .Setup(vt => vt.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(vehicles);

            // ACT
            var result = await _vehicleService.GetAllAsync(It.IsAny<CancellationToken>());

            // ASSERT
            result.Should().BeEquivalentTo(vehicles);
        }

        [Fact]
        public async Task GetAllAsync_Should_Call_Transaction_Once()
        {
            // ACT
            await _vehicleService.GetAllAsync(It.IsAny<CancellationToken>());

            // ASSERT
            _vehicleTransactionMock.Verify(vt => vt.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
