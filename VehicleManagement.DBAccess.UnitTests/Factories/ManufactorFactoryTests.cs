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
        [MemberData(nameof(ManufacturerTestData.GetManufacturerFactoryTestData), MemberType = typeof(ManufacturerTestData))]
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
            // ASSERT
            Assert.Throws<DataConversionException>(() => _manufacturerFactory.Create(null));
        }
    }
}
