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
    }
}
