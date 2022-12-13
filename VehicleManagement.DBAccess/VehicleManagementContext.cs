using Microsoft.EntityFrameworkCore;

using VehicleManagement.DBAccess.Entities;

namespace VehicleManagement.DBAccess
{
    public class VehicleManagementContext : DbContext, IVehicleManagementContext
    {
        public DbSet<Manufacturer> Manufacturer { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public VehicleManagementContext(DbContextOptions options) : base(options) { }
    }
}
