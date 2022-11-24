using System.Collections.Generic;
using System.Threading;

using FluentAssertions;

using VehicleManagement.Core.Services;
using VehicleManagement.Core.UnitTests.TestData;
using VehicleManagement.DataContracts.DataModels;
using VehicleManagement.DBAccess.Transcations;

namespace VehicleManagement.Core.UnitTests.Services
{
    public class ManufacturerServiceTests
    {
        private readonly Mock<IManufacturerTransaction> _manufacturerTransactionMock;
        private readonly IManufacturerService _manufacturerService;

        public ManufacturerServiceTests()
        {
            _manufacturerTransactionMock = new Mock<IManufacturerTransaction>();
            _manufacturerService = new ManufacturerService(_manufacturerTransactionMock.Object);
        }

        [Theory]
        [MemberData(nameof(ManufacturerTestData.GetManufacturersTestData), MemberType = typeof(ManufacturerTestData))]
        public async Task GetAllAsync_Should_Get_All(List<Manufacturer> manufacturers)
        {
            // ARRANGE
            _manufacturerTransactionMock
                .Setup(mt => mt.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(manufacturers);

            // ACT
            var result = await _manufacturerService.GetAllAsync(It.IsAny<CancellationToken>());

            // ASSERT
            result.Should().BeEquivalentTo(manufacturers);
        }

        [Fact]
        public async Task GetAllAsync_Should_Call_Transaction_Once()
        {
            // ACT
            await _manufacturerService.GetAllAsync(It.IsAny<CancellationToken>());

            // ASSERT
            _manufacturerTransactionMock.Verify(mt => mt.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
