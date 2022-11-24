using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace VehicleManagement.DBAccess.IntegrationTests.DBFixtures
{
    public abstract class SharedDatabaseFixture : IDisposable
    {
        private readonly SqliteConnection _connection;

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

        protected void InsertData(string table, string valueCommand)
        {
            using var cmd = _connection.CreateCommand();

            cmd.CommandText = $"INSERT INTO {table} VALUES ({valueCommand})";

            cmd.ExecuteNonQuery();
        }
    }
}
