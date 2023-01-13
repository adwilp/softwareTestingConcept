using VehicleManagement.DBAccess.Entities;

using models = VehicleManagement.DataContracts.DataModels;

namespace VehicleManagement.DBAccess.Factories
{
    public interface IBookingFactory
    {
        models.FlatBooking Create(Booking booking);

        IEnumerable<models.FlatBooking> Create(IEnumerable<Booking> bookings);

        Booking Create(models.Booking booking);
    }
}