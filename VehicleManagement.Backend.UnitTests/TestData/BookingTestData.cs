using System.Collections.Generic;

using VehicleManagement.DataContracts.DataModels;

namespace VehicleManagement.Backend.UnitTests.TestData
{
    public static class BookingTestData
    {
        public static IEnumerable<object[]> GetBookingsTestData()
        {
            yield return new object[]
            {
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
                    },
                    new FlatBooking()
                    {
                        Id = 1
                    }
                }
            };

            yield return new object[]
            {
                new List<FlatBooking>()
            };
        }

        public static IEnumerable<object[]> GetAddBookingTestData()
        {
            yield return new object[]
            {
                new Booking()
                {
                    Start = new System.DateTime(2022, 12, 14, 10, 43, 50),
                    End = new System.DateTime(2022, 12, 16, 11, 00, 00),
                    EmployeeNumber = "98732",
                    FIN = "WAU1234567890"
                },
                new FlatBooking()
                {
                    Id = 1,
                    Start = new System.DateTime(2022, 12, 14, 10, 43, 50),
                    End = new System.DateTime(2022, 12, 16, 11, 00, 00),
                    EmployeeNumber = "98732",
                    FIN = "WAU1234567890",
                    LicensePlate = "VEC-GR-123"
                },
            };

            yield return new object[]
            {
                new Booking()
                {
                    Start = new System.DateTime(2022, 12, 14, 10, 43, 50),
                    EmployeeNumber = "98732",
                    FIN = "WAU1234567890"
                },
                new FlatBooking()
                {
                    Id = 1,
                    Start = new System.DateTime(2022, 12, 14, 10, 43, 50),
                    EmployeeNumber = "98732",
                    FIN = "WAU1234567890",
                    LicensePlate = "VEC-GR-123"
                },
            };
        }
    }
}
