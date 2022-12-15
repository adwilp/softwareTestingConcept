using VehicleManagement.DataContracts.DataModels;
using VehicleManagement.DBAccess.Entities;

namespace VehicleManagement.DBAccess.Factories
{
    public interface IBookingFactory
    {
        FlatBooking Create(Booking booking);

        IEnumerable<FlatBooking> Create(IEnumerable<Booking> bookings);
    }
}