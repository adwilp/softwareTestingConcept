using VehicleManagement.DataContracts.DataModels;

using entities = VehicleManagement.DBAccess.Entities;

namespace VehicleManagement.DBAccess.UnitTests.TestData
{
    public static class ManufacturerTestData
    {
        public static IEnumerable<object[]> GetSingleManufacturerTestData()
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

        public static IEnumerable<object[]> GetManufacturersTestData()
        {
            yield return new object[]
            {
                new List<entities.Manufacturer>()
                {
                    new entities.Manufacturer()
                    {
                        Id = "WAU",
                        Name = "Audi"
                    },
                    new entities.Manufacturer()
                    {
                        Id = "WAU",
                        Name = null
                    },
                },
                new List<Manufacturer>()
                {
                    new Manufacturer()
                    {
                        Id = "WAU",
                        Name = "Audi"
                    },
                    new Manufacturer()
                    {
                        Id = "WAU",
                        Name = null
                    }
                }
            };

            yield return new object[]
            {
                new List<entities.Manufacturer>(),
                new List<Manufacturer>()
            };
        }
    }
}
