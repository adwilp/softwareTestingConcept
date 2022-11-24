using System.Collections.Generic;

using VehicleManagement.DataContracts.DataModels;

namespace VehicleManagement.Backend.UnitTests.TestData
{
    public static class VehicleTestData
    {
        public static IEnumerable<object[]> GetVehiclesTestData()
        {
            yield return new object[]
            {
                new List<FlatVehicle>()
                {
                    new FlatVehicle()
                    {
                        FIN = "SB164ABN1PE082986",
                        Color = "black",
                        LicensePlate = "VEC-EL-123",
                        Manufacturer = "Audi",
                        Mileage = 1234.56
                    },
                    new FlatVehicle()
                    {
                        FIN = "SB164ABN1PE082986",
                        Color = "black",
                        LicensePlate = "VEC-EL-123",
                        Manufacturer = "Audi",
                    },
                    new FlatVehicle()
                    {
                        FIN = "SB164ABN1PE082986",
                    }
                }
            };

            yield return new object[]
            {
                new List<FlatVehicle>()
            };
        }
    }
}
