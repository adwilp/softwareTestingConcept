using VehicleManagement.DBAccess.Entities;

namespace VehicleManagement.DBAccess.Repositories
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        public BookingRepository(IVehicleManagementContext context) : base(context) { }
    }
}
