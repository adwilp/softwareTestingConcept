using System.Collections.Generic;
using System.Threading;

using FluentAssertions;

using VehicleManagement.Core.Domains;
using VehicleManagement.Core.Services;
using VehicleManagement.Core.UnitTests.TestData;
using VehicleManagement.DataContracts.DataModels;

namespace VehicleManagement.Core.UnitTests.Domains
{
    public class VehicleDomainTests
    {
        private readonly Mock<IVehicleService> _vehicleServiceMock;
        private readonly IVehicleDomain _vehicleDomain;

        public VehicleDomainTests()
        {
            _vehicleServiceMock = new Mock<IVehicleService>();
            _vehicleDomain = new VehicleDomain(_vehicleServiceMock.Object);
        }

        [Theory]
        [MemberData(nameof(VehicleTestData.GetVehiclesTestData), MemberType = typeof(VehicleTestData))]
        public async Task GetAllAsync_Should_Return_All(List<FlatVehicle> vehicles)
        {
            // ARRANGE
            _vehicleServiceMock
                .Setup(vs => vs.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(vehicles);

            // ACT
            var result = await _vehicleDomain.GetAllAsync(It.IsAny<CancellationToken>());

            // ASSERT
            result.Should().BeEquivalentTo(vehicles);
        }

        [Fact]
        public async Task GetAllAsync_Should_Call_Service_Once()
        {
            // ACT
            await _vehicleDomain.GetAllAsync(It.IsAny<CancellationToken>());

            // ASSERT
            _vehicleServiceMock.Verify(vs => vs.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
