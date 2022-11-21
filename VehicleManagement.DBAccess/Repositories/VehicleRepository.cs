using VehicleManagement.DBAccess.Entities;

namespace VehicleManagement.DBAccess.Repositories
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(IVehicleManagementContext context) : base(context) { }
    }
}
