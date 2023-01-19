using System.Collections.Generic;

using VehicleManagement.DBAccess.Entities;

namespace VehicleManagement.DBAccess.IntegrationTests.TestData
{
    public static class BookingTestData
    {
        public static IEnumerable<object[]> GetAddTestData()
        {
            yield return new object[]
            {
                new Booking()
                {
                    Start = new System.DateTime(2022, 12, 15, 10, 43, 50),
                    End = new System.DateTime(2022, 12, 16, 10, 0, 0),
                    EmployeeNumber = "12345",
                    FIN = "SB164ABN1PE082986"
                },
                new Booking()
                {
                    Id = 5,
                    Start = new System.DateTime(2022, 12, 15, 10, 43, 50),
                    End = new System.DateTime(2022, 12, 16, 10, 0, 0),
                    EmployeeNumber = "12345",
                    FIN = "SB164ABN1PE082986",
                },
            };
        }

        public static IEnumerable<object[]> GetFailAddTestData()
        {
            yield return new object[]
            {
                new Booking()
                {
                    Start = new System.DateTime(2022, 12, 15, 10, 43, 50),
                    End = new System.DateTime(2022, 12, 16, 10, 0, 0),
                    EmployeeNumber = "12345",
                    FIN = "ABC"
                }
            };

            yield return new object[]
            {
                new Booking()
                {
                    Start = new System.DateTime(2022, 12, 15, 10, 43, 50),
                    End = new System.DateTime(2022, 12, 16, 10, 0, 0),
                    EmployeeNumber = "12345",
                    FIN = "DEF",
                },
            };
        }

        public static IEnumerable<object[]> GetFailUpdateTestData()
        {
            yield return new object[]
            {
                new Booking()
                {
                    Id = 5,
                    Start = new System.DateTime(2022, 12, 15, 10, 43, 50),
                    End = new System.DateTime(2022, 12, 16, 10, 0, 0),
                    EmployeeNumber = "12345",
                    FIN = "SB164ABN1PE082096"
                }
            };

            yield return new object[]
            {
                new Booking()
                {
                    Id = 1,
                    Start = new System.DateTime(2022, 12, 15, 10, 43, 50),
                    End = new System.DateTime(2022, 12, 16, 10, 0, 0),
                    EmployeeNumber = "12345",
                    FIN = "DEF",
                },
            };
        }

        public static IEnumerable<object[]> GetReloadReferencesTestData()
        {
            yield return new object[]
            {
                new Booking()
                {
                    Id = 1,
                    Start = new System.DateTime(2022, 12, 14, 10, 50, 12),
                    End = new System.DateTime(2022, 12, 16, 10, 0, 0),
                    EmployeeNumber = "12345",
                    FIN = "SB164ABN1PE082986",
                    Vehicle = new Vehicle()
                    {
                        FIN = "SB164ABN1PE082986",
                        LicensePlate = "MI-XY-666",
                        Color = "black",
                        Mileage = 12345.89,
                        ManufacturerId = "WMI"
                    }
                },
            };
        }

        public static IEnumerable<object[]> GetUpdateTestData()
        {
            yield return new object[]
            {
                new Booking()
                {
                    Id = 1,
                    Start = new System.DateTime(2022, 12, 15, 10, 43, 50),
                    End = new System.DateTime(2022, 12, 16, 10, 0, 0),
                    EmployeeNumber = "12345",
                    FIN = "SB164ABN1PE082986"
                },
                new Booking()
                {
                    Id = 1,
                    Start = new System.DateTime(2022, 12, 15, 10, 43, 50),
                    End = new System.DateTime(2022, 12, 16, 10, 0, 0),
                    EmployeeNumber = "12345",
                    FIN = "SB164ABN1PE082986",
                },
            };

            yield return new object[]
            {
                new Booking()
                {
                    Id = 1,
                    Start = new System.DateTime(2022, 12, 15, 10, 43, 50),
                    End = new System.DateTime(2022, 12, 16, 10, 0, 0),
                    EmployeeNumber = "12345",
                    FIN = "SB164ABN1PE082096"
                },
                new Booking()
                {
                    Id = 1,
                    Start = new System.DateTime(2022, 12, 15, 10, 43, 50),
                    End = new System.DateTime(2022, 12, 16, 10, 0, 0),
                    EmployeeNumber = "12345",
                    FIN = "SB164ABN1PE082096",
                },
            };
        }

        public static IEnumerable<object[]> GetTestData()
        {
            yield return new object[]
            {
                1,
                new Booking()
                {
                    Id = 1,
                    Start = new DateTime(2022, 12, 14, 10, 50, 12),
                    End = new DateTime(2022, 12, 16, 10, 00, 00),
                    EmployeeNumber = "12345",
                    FIN = "SB164ABN1PE082986"
                }
            };

            yield return new object[]
            {
                3,
                new Booking()
                {
                    Id = 3,
                    Start = new DateTime(2022, 12, 14, 10, 50, 12),
                    End = new DateTime(2022, 12, 16, 10, 00, 00),
                    EmployeeNumber = "54321",
                    FIN = "SB164ABN1PE082096"
                }
            };

            yield return new object[]
            {
                5,
                null
            };
        }

        public static IEnumerable<object[]> GetDeleteData()
        {
            yield return new object[]
            {
                new Booking()
                {
                    Id = 1
                }
            };

            yield return new object[]
            {
                new Booking()
                {
                    Id = 3
                }
            };
        }
    }
}
