using VehicleManagement.DataContracts.DataModels;

using entities = VehicleManagement.DBAccess.Entities;

namespace VehicleManagement.DBAccess.UnitTests.TestData
{
    public static class ManufacturerTestData
    {
        public static IEnumerable<object[]> GetManufacturerFactoryTestData()
        {
            yield return new object[]
            {
                new entities.Manufacturer()
                {
                    Id = "WAU",
                    Name = "Audi"
                },
                new Manufacturer()
                {
                    Id = "WAU",
                    Name = "Audi"
                }
            };

            yield return new object[]
            {
                new entities.Manufacturer()
                {
                    Id = "WAU",
                    Name = null
                },
                new Manufacturer()
                {
                    Id = "WAU",
                    Name = null
                }
            };
        }
    }
}
