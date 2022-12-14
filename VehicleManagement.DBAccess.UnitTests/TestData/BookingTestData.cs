using VehicleManagement.DataContracts.DataModels;
using VehicleManagement.DBAccess.Entities;

namespace VehicleManagement.DBAccess.UnitTests.TestData
{
    public static class BookingTestData
    {
        public static IEnumerable<object[]> GetSingleBookingTestData()
        {
            yield return new object[]
            {
                new Booking()
                {
                    Id = 1,
                    Start = new System.DateTime(2022, 12, 14, 10, 43, 50),
                    End = new System.DateTime(2022, 12, 16, 11, 00, 00),
                    EmployeeNumber = "28178",
                    FIN = "WAU1234567890",
                    Vehicle = new Vehicle()
                    {
                        FIN = "WAU1234567890",
                        LicensePlate = "VEC-GR-123",
                        Color = "black",
                        Mileage = 12.3
                    }
                },
                new FlatBooking()
                {
                    Id = 1,
                    Start = new System.DateTime(2022, 12, 14, 10, 43, 50),
                    End = new System.DateTime(2022, 12, 16, 11, 00, 00),
                    EmployeeNumber = "28178",
                    FIN = "WAU1234567890",
                    LicensePlate = "VEC-GR-123"
                }
            };

            yield return new object[]
            {
                new Booking()
                {
                    Id = 1,
                    Start = new System.DateTime(2022, 12, 14, 10, 43, 50),
                    EmployeeNumber = "28178",
                    FIN = "WAU1234567890",
                },
                new FlatBooking()
                {
                    Id = 1,
                    Start = new System.DateTime(2022, 12, 14, 10, 43, 50),
                    EmployeeNumber = "28178",
                    FIN = "WAU1234567890",
                }
            };

            yield return new object[]
            {
                new Booking(),
                new FlatBooking()
            };

            yield return new object[]
            {
                new Booking()
                {
                    Id = 1
                },
                new FlatBooking()
                {
                    Id = 1
                }
            };
        }

        public static IEnumerable<object[]> GetBookingsTestData()
        {
            yield return new object[]
            {
                new List<Booking>()
                {
                    new Booking()
                    {
                        Id = 1,
                        Start = new System.DateTime(2022, 12, 14, 10, 43, 50),
                        End = new System.DateTime(2022, 12, 16, 11, 00, 00),
                        EmployeeNumber = "28178",
                        FIN = "WAU1234567890",
                        Vehicle = new Vehicle()
                        {
                            FIN = "WAU1234567890",
                            LicensePlate = "VEC-GR-123",
                            Color = "black",
                            Mileage = 12.3
                        }
                    },
                    new Booking()
                    {
                        Id = 1,
                        Start = new System.DateTime(2022, 12, 14, 10, 43, 50),
                        EmployeeNumber = "28178",
                        FIN = "WAU1234567890",
                    },
                },
                new List<FlatBooking>()
                {
                    new FlatBooking()
                    {
                        Id = 1,
                        Start = new System.DateTime(2022, 12, 14, 10, 43, 50),
                        End = new System.DateTime(2022, 12, 16, 11, 00, 00),
                        EmployeeNumber = "28178",
                        FIN = "WAU1234567890",
                        LicensePlate = "VEC-GR-123"
                    },
                    new FlatBooking()
                    {
                        Id = 1,
                        Start = new System.DateTime(2022, 12, 14, 10, 43, 50),
                        EmployeeNumber = "28178",
                        FIN = "WAU1234567890",
                    }
                }
            };

            yield return new object[]
            {
                new List<Booking>()
                {
                    new Booking()
                    {
                        Id = 1
                    }
                },
                new List<FlatBooking>()
                {
                    new FlatBooking()
                    {
                        Id = 1
                    }
                }
            };

            yield return new object[]
            {
                new List<Booking>(),
                new List<FlatBooking>()
            };
        }
    }
}
