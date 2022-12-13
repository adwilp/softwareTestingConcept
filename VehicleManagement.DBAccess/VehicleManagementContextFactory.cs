using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VehicleManagement.DBAccess
{
    public class VehicleManagementContextFactory : IDesignTimeDbContextFactory<VehicleManagementContext>
    {
        public VehicleManagementContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<VehicleManagementContext>();
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=vehicle-management-db-dev;Trusted_Connection=True;TrustServerCertificate=True;");

            return new VehicleManagementContext(optionsBuilder.Options);
        }
    }
}
