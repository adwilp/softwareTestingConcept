using VehicleManagement.DBAccess.Entities;

using models = VehicleManagement.DataContracts.DataModels;

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
                new models.FlatBooking()
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
                new models.FlatBooking()
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
                new models.FlatBooking()
            };

            yield return new object[]
            {
                new Booking()
                {
                    Id = 1
                },
                new models.FlatBooking()
                {
                    Id = 1
                }
            };
        }
        public static IEnumerable<object[]> GetSingleBookingModelTestData()
        {
            yield return new object[]
            {
                new models.Booking()
                {
                    Start = new System.DateTime(2022, 12, 15, 10, 43, 50),
                    End = new System.DateTime(2022, 12, 16, 10, 0, 0),
                    EmployeeNumber = "12345",
                    FIN = "WO1234567890"
                },
                new Booking()
                {
                    Id = 1,
                    Start = new System.DateTime(2022, 12, 15, 10, 43, 50),
                    End = new System.DateTime(2022, 12, 16, 10, 0, 0),
                    EmployeeNumber = "12345",
                    FIN = "WO1234567890"
                }
            };

            yield return new object[]
            {
                new models.Booking(),
                new Booking()
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
                new List<models.FlatBooking>()
                {
                    new models.FlatBooking()
                    {
                        Id = 1,
                        Start = new System.DateTime(2022, 12, 14, 10, 43, 50),
                        End = new System.DateTime(2022, 12, 16, 11, 00, 00),
                        EmployeeNumber = "28178",
                        FIN = "WAU1234567890",
                        LicensePlate = "VEC-GR-123"
                    },
                    new models.FlatBooking()
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
                new List<models.FlatBooking>()
                {
                    new models.FlatBooking()
                    {
                        Id = 1
                    }
                }
            };

            yield return new object[]
            {
                new List<Booking>(),
                new List<models.FlatBooking>()
            };
        }

        public static IEnumerable<object[]> GetAddBookingTestData()
        {
            yield return new object[]
            {
                new models.Booking()
                {
                    Start = new System.DateTime(2022, 12, 15, 10, 43, 50),
                    End = new System.DateTime(2022, 12, 16, 10, 0, 0),
                    EmployeeNumber = "12345",
                    FIN = "WO1234567890"
                },
                new Booking()
                {
                    Start = new System.DateTime(2022, 12, 15, 10, 43, 50),
                    End = new System.DateTime(2022, 12, 16, 10, 0, 0),
                    EmployeeNumber = "12345",
                    FIN = "WO1234567890"
                },
                new Booking()
                {
                    Id = 1,
                    Start = new System.DateTime(2022, 12, 15, 10, 43, 50),
                    End = new System.DateTime(2022, 12, 16, 10, 0, 0),
                    EmployeeNumber = "12345",
                    FIN = "WO1234567890",
                    Vehicle = new Vehicle()
                    {
                        LicensePlate = "VEC-GR-123"
                    }
                },
                new models.FlatBooking()
                {
                    Id = 1,
                    Start = new System.DateTime(2022, 12, 15, 10, 43, 50),
                    End = new System.DateTime(2022, 12, 16, 10, 0, 0),
                    EmployeeNumber = "12345",
                    FIN = "WO1234567890",
                    LicensePlate = "VEC-GR-123"
                }
            };
        }
    }
}
