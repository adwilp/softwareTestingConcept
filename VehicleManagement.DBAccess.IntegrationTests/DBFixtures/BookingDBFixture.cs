namespace VehicleManagement.DBAccess.IntegrationTests.DBFixtures
{
    public class BookingDBFixture : SharedDatabaseFixture
    {
        protected override void InitializeData()
        {
            // MANUFACTURER
            InsertData(
                nameof(VehicleManagementContext.Manufacturer),
                _manufacturerColumns,
                "'WMI', 'Audi'");
            InsertData(
                nameof(VehicleManagementContext.Manufacturer),
                _manufacturerColumns,
                "'W0L', 'Opel'");

            // VEHICLE
            InsertData(
                nameof(VehicleManagementContext.Vehicles),
                _vehicleColumns,
                "'SB164ABN1PE082986', 'MI-XY-666', 'black', 12345.89, 'WMI'");
            InsertData(
                nameof(VehicleManagementContext.Vehicles),
                _vehicleColumns,
                "'SB164ABN1PE082096', 'DH-IL-12', 'green', 125.40, 'W0L'");
            InsertData(
                nameof(VehicleManagementContext.Vehicles),
                _vehicleColumns,
                "'SB189ABN1PE034986', 'VEC-KL-234', 'red', 0.0, 'WMI'");

            // BOOKING
            InsertData(
                nameof(VehicleManagementContext.Bookings),
                _bookingColumns,
                "1, '2022-12-14 10:50:12', '2022-12-16 10:00:00', '12345', 'SB164ABN1PE082986'");

            InsertData(
                nameof(VehicleManagementContext.Bookings),
                _bookingColumns,
                "2, '2022-10-14 10:50:12', '2022-10-16 10:00:00', '12345', 'SB164ABN1PE082986'");

            InsertData(
                nameof(VehicleManagementContext.Bookings),
                _bookingColumns,
                "3, '2022-12-14 10:50:12', '2022-12-16 10:00:00', '54321', 'SB164ABN1PE082096'");

            InsertData(
                nameof(VehicleManagementContext.Bookings),
                _bookingColumns,
                "4, '2022-12-14 10:50:12', '2022-12-16 10:00:00', '654789', 'SB189ABN1PE034986'");
        }
    }
}
