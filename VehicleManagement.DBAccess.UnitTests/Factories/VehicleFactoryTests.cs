using VehicleManagement.DataContracts.DataModels;
using VehicleManagement.DataContracts.Exceptions;
using VehicleManagement.DBAccess.Entities;
using VehicleManagement.DBAccess.Factories;
using VehicleManagement.DBAccess.UnitTests.TestData;

namespace VehicleManagement.DBAccess.UnitTests.Factories
{
    public class VehicleFactoryTests
    {
        private readonly IVehicleFactory _vehicleFactory;

        public VehicleFactoryTests()
        {
            _vehicleFactory = new VehicleFactory();
        }

        [Theory]
        [MemberData(nameof(VehicleTestData.GetVehicleFactoryTestData), MemberType = typeof(VehicleTestData))]
        public void Create_Should_Create_FlatVehicle(Vehicle entity, FlatVehicle model)
        {
            // ACT
            var result = _vehicleFactory.Create(entity);

            // ASSERT
            Assert.Equal(model.FIN, result.FIN);
            Assert.Equal(model.Color, result.Color);
            Assert.Equal(model.LicensePlate, result.LicensePlate);
            Assert.Equal(model.Mileage, result.Mileage);
            Assert.Equal(model.Manufacturer, result.Manufacturer);
        }

        [Fact]
        public void Create_Should_Throw_Exception_For_Null()
        {
            // ASSERT
            Assert.Throws<DataConversionException>(() => _vehicleFactory.Create(null));
        }
    }
}
