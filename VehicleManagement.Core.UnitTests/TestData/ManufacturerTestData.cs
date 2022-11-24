using System.Collections.Generic;

using VehicleManagement.DataContracts.DataModels;

namespace VehicleManagement.Core.UnitTests.TestData
{
    public static class ManufacturerTestData
    {
        public static IEnumerable<object[]> GetManufacturersTestData()
        {
            yield return new object[]
            {
                new List<Manufacturer>()
                {
                    new Manufacturer()
                    {
                        Id = "WAU",
                        Name = "Audi"
                    },
                    new Manufacturer()
                    {
                        Id = "W0L",
                        Name = "Opel"
                    },
                    new Manufacturer()
                    {
                        Id = "S1B"
                    }
                }
            };

            yield return new object[]
            {
                new List<Manufacturer>()
            };
        }
    }
}
