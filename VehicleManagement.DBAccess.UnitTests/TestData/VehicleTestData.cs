using VehicleManagement.DataContracts.DataModels;
using VehicleManagement.DBAccess.Entities;

namespace VehicleManagement.DBAccess.UnitTests.TestData
{
    public static class VehicleTestData
    {
        public static IEnumerable<object[]> GetVehicleFactoryTestData()
        {
            yield return new object[]
            {
                new Vehicle()
                {
                    FIN = "SB164ABN1PE082986",
                    Color = "black",
                    LicensePlate = "VEC-EL-123",
                    Manufacturer = new Entities.Manufacturer()
                    {
                        Id = "WAU",
                        Name = "Audi"
                    },
                    ManufacturerId = "WAU",
                    Mileage = 1234.56
                },
                new FlatVehicle()
                {
                    FIN = "SB164ABN1PE082986",
                    Color = "black",
                    LicensePlate = "VEC-EL-123",
                    Manufacturer = "Audi",
                    Mileage = 1234.56
                }
            };

            yield return new object[]
            {
                new Vehicle()
                {
                    FIN = "SB164ABN1PE082986",
                    Color = "black",
                    LicensePlate = "VEC-EL-123",
                    Manufacturer = new Entities.Manufacturer()
                    {
                        Id = "WAU",
                        Name = "Audi"
                    },
                    Mileage = 1234.56
                },
                new FlatVehicle()
                {
                    FIN = "SB164ABN1PE082986",
                    Color = "black",
                    LicensePlate = "VEC-EL-123",
                    Manufacturer = "Audi",
                    Mileage = 1234.56
                }
            };

            yield return new object[]
            {
                new Vehicle()
                {
                    FIN = "SB164ABN1PE082986",
                    Color = "black",
                    LicensePlate = "VEC-EL-123",
                    ManufacturerId = "WAU",
                    Mileage = 1234.56
                },
                new FlatVehicle()
                {
                    FIN = "SB164ABN1PE082986",
                    Color = "black",
                    LicensePlate = "VEC-EL-123",
                    Mileage = 1234.56
                }
            };

            yield return new object[]
            {
                new Vehicle()
                {
                    FIN = "SB164ABN1PE082986",
                    Color = "black",
                    LicensePlate = "VEC-EL-123",
                    Mileage = 1234.56
                },
                new FlatVehicle()
                {
                    FIN = "SB164ABN1PE082986",
                    Color = "black",
                    LicensePlate = "VEC-EL-123",
                    Mileage = 1234.56
                }
            };
        }
    }
}
