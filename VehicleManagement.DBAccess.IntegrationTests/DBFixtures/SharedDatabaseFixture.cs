using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using VehicleManagement.DBAccess.Entities;

namespace VehicleManagement.DBAccess.IntegrationTests.DBFixtures
{
    public abstract class SharedDatabaseFixture : IDisposable
    {
        private readonly SqliteConnection _connection;

        protected const string _manufacturerColumns = $"{nameof(Manufacturer.Id)}, {nameof(Manufacturer.Name)}";
        protected const string _vehicleColumns = $"{nameof(Vehicle.FIN)}, {nameof(Vehicle.LicensePlate)}, {nameof(Vehicle.Color)}, {nameof(Vehicle.Mileage)}, {nameof(Vehicle.ManufacturerId)}";
        protected const string _bookingColumns = $"{nameof(Booking.Id)}, {nameof(Booking.Start)}, {nameof(Booking.End)}, {nameof(Booking.EmployeeNumber)}, {nameof(Booking.FIN)}";

        protected SharedDatabaseFixture()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
        }

        public void Dispose()
        {
            _connection.Close();
        }

        public IVehicleManagementContext CreateContext()
        {
            var context = new VehicleManagementContext(
                new DbContextOptionsBuilder<VehicleManagementContext>()
                    .UseSqlite(_connection)
                    .Options
                );

            context.Database.EnsureCreated();

            InitializeData();

            return context;
        }

        protected abstract void InitializeData();

        protected void InsertData(string table, string columnsCommand, string valueCommand)
        {
            using var cmd = _connection.CreateCommand();

            cmd.CommandText = $"INSERT INTO {table} ({columnsCommand}) VALUES ({valueCommand})";

            cmd.ExecuteNonQuery();
        }
    }
}
