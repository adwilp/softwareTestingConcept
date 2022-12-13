using FluentAssertions;

using VehicleManagement.DataContracts.DataModels;
using VehicleManagement.DataContracts.Exceptions;
using VehicleManagement.DBAccess.Factories;
using VehicleManagement.DBAccess.UnitTests.TestData;

using entities = VehicleManagement.DBAccess.Entities;

namespace VehicleManagement.DBAccess.UnitTests.Factories
{
    public class ManufactorFactoryTests
    {
        private readonly IManufacturerFactory _manufacturerFactory;

        public ManufactorFactoryTests()
        {
            _manufacturerFactory = new ManufacturerFactory();
        }

        [Theory]
        [MemberData(nameof(ManufacturerTestData.GetSingleManufacturerTestData), MemberType = typeof(ManufacturerTestData))]
        public void Create_Should_Create_Manufacturer(entities.Manufacturer entity, Manufacturer model)
        {
            // ACT
            var result = _manufacturerFactory.Create(entity);

            // ASSERT
            Assert.Equal(model.Id, result.Id);
            Assert.Equal(model.Name, result.Name);
        }

        [Fact]
        public void Create_Should_Throw_Exception_For_Null()
        {
            // ARRANGE
            entities.Manufacturer? entity = null;

            // ASSERT
            Assert.Throws<DataConversionException>(() => _manufacturerFactory.Create(entity));
        }

        [Theory]
        [MemberData(nameof(ManufacturerTestData.GetManufacturersTestData), MemberType = typeof(ManufacturerTestData))]
        public void Create_Should_Create_Manufacturer_Enumerable(List<entities.Manufacturer> entities, List<Manufacturer> models)
        {
            // ACT
            var result = _manufacturerFactory.Create(entities);

            // ASSERT
            result.Should().BeEquivalentTo(models);
        }

        [Fact]
        public void Create_Should_Throw_Exception_For_Null_Enumerable()
        {
            // ARRANGE
            List<entities.Manufacturer>? entities = null;

            // ASSERT
            Assert.Throws<DataConversionException>(() => _manufacturerFactory.Create(entities));
        }
    }
}
