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
    public class ManufacturerControllerTests
    {
        private readonly Mock<IManufacturerDomain> _manufacturerDomainMock;
        private readonly ManufacturersController _manufacturersController;

        public ManufacturerControllerTests()
        {
            _manufacturerDomainMock = new Mock<IManufacturerDomain>();
            _manufacturersController = new ManufacturersController(_manufacturerDomainMock.Object);
        }

        [Theory]
        [MemberData(nameof(ManufacturerTestData.GetManufacturersTestData), MemberType = typeof(ManufacturerTestData))]
        public async Task GetAll_Should_Return_All_Successfull(List<Manufacturer> manufacturers)
        {
            // ARRANGE
            _manufacturerDomainMock
                .Setup(md => md.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(manufacturers);

            // ACT
            var result = await _manufacturersController.GetAll(It.IsAny<CancellationToken>());

            // ASSERT
            var response = Assert.IsType<OkObjectResult>(result);
            var body = Assert.IsAssignableFrom<IEnumerable<Manufacturer>>(response.Value);

            Assert.NotNull(body);
            body.Should().BeEquivalentTo(manufacturers);
            Assert.Equal(manufacturers.Count, body.Count());
        }
    }
}
