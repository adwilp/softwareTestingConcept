using System;
using System.Linq.Expressions;
using System.Threading;

using FluentAssertions;

using Moq;

using VehicleManagement.DataContracts.DataModels;
using VehicleManagement.DBAccess.Factories;
using VehicleManagement.DBAccess.Repositories;
using VehicleManagement.DBAccess.Transcations;
using VehicleManagement.DBAccess.UnitTests.TestData;

using entities = VehicleManagement.DBAccess.Entities;

namespace VehicleManagement.DBAccess.UnitTests.Transactions
{
    public class ManufacturerTransactionTests
    {
        private readonly Mock<IManufacturerRepository> _manufacturerRepository;
        private readonly Mock<IManufacturerFactory> _manufacturerFactory;
        private readonly IManufacturerTransaction _manufacturerTransaction;

        public ManufacturerTransactionTests()
        {
            _manufacturerRepository = new Mock<IManufacturerRepository>();
            _manufacturerFactory = new Mock<IManufacturerFactory>();
            _manufacturerTransaction = new ManufacturerTransaction(_manufacturerFactory.Object, _manufacturerRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ManufacturerTestData.GetManufacturersTestData), MemberType = typeof(ManufacturerTestData))]
        public async Task GetAllAsync_Should_Get_All(List<entities.Manufacturer> entities, List<Manufacturer> models)
        {
            // ARRANGE
            _manufacturerRepository
                .Setup(mr =>
                    mr.GetAllAsync(
                        It.IsAny<CancellationToken>(),
                        It.IsAny<Expression<Func<entities.Manufacturer, bool>>>(),
                        It.IsAny<bool>(),
                        It.IsAny<string[]>()
                    )
                )
                .ReturnsAsync(entities);

            _manufacturerFactory
                .Setup(mf => mf.Create(entities))
                .Returns(models);

            // ACT
            var result = await _manufacturerTransaction.GetAllAsync(It.IsAny<CancellationToken>());

            // ASSERT
            result.Should().BeEquivalentTo(models);
        }
    }
}
