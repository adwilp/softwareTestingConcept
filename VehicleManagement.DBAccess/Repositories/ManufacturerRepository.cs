using VehicleManagement.DBAccess.Entities;

namespace VehicleManagement.DBAccess.Repositories
{
    public class ManufacturerRepository : BaseRepository<Manufacturer>, IManufacturerRepository
    {
        public ManufacturerRepository(IVehicleManagementContext context) : base(context) { }
    }
}
