using System.Collections.Generic;
using System.Threading;

using FluentAssertions;

using VehicleManagement.Core.Domains;
using VehicleManagement.Core.Services;
using VehicleManagement.Core.UnitTests.TestData;
using VehicleManagement.DataContracts.DataModels;

namespace VehicleManagement.Core.UnitTests.Domains
{
    public class ManufacturerDomainTests
    {
        private readonly Mock<IManufacturerService> _manufacturerServiceMock;
        private readonly IManufacturerDomain _manufacturerDomain;

        public ManufacturerDomainTests()
        {
            _manufacturerServiceMock = new Mock<IManufacturerService>();
            _manufacturerDomain = new ManufacturerDomain(_manufacturerServiceMock.Object);
        }

        [Theory]
        [MemberData(nameof(ManufacturerTestData.GetManufacturersTestData), MemberType = typeof(ManufacturerTestData))]
        public async Task GetAllAsync_Should_Return_All(List<Manufacturer> manufacturers)
        {
            // ARRANGE
            _manufacturerServiceMock
                .Setup(vs => vs.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(manufacturers);

            // ACT
            var result = await _manufacturerDomain.GetAllAsync(It.IsAny<CancellationToken>());

            // ASSERT
            result.Should().BeEquivalentTo(manufacturers);
        }

        [Fact]
        public async Task GetAllAsync_Should_Call_Service_Once()
        {
            // ACT
            await _manufacturerDomain.GetAllAsync(It.IsAny<CancellationToken>());

            // ASSERT
            _manufacturerServiceMock.Verify(vs => vs.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
