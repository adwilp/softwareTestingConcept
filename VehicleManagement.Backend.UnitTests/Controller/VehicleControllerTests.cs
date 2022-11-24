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
    public class VehicleControllerTests
    {
        private readonly Mock<IVehicleDomain> _vehicleDomainMock;
        private readonly VehiclesController _vehiclesController;

        public VehicleControllerTests()
        {
            _vehicleDomainMock = new Mock<IVehicleDomain>();
            _vehiclesController = new VehiclesController(_vehicleDomainMock.Object);
        }

        [Theory]
        [MemberData(nameof(VehicleTestData.GetVehiclesTestData), MemberType = typeof(VehicleTestData))]
        public async Task GetAll_Should_Return_All_Successfull(List<FlatVehicle> vehicles)
        {
            // ARRANGE
            _vehicleDomainMock
                .Setup(md => md.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(vehicles);

            // ACT
            var result = await _vehiclesController.GetAll(It.IsAny<CancellationToken>());

            // ASSERT
            var response = Assert.IsType<OkObjectResult>(result);
            var body = Assert.IsAssignableFrom<IEnumerable<FlatVehicle>>(response.Value);

            Assert.NotNull(body);
            body.Should().BeEquivalentTo(vehicles);
            Assert.Equal(vehicles.Count, body.Count());
        }
    }
}
