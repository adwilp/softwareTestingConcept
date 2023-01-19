using VehicleManagement.DBAccess.Entities;

using models = VehicleManagement.DataContracts.DataModels;

namespace VehicleManagement.DBAccess.Factories
{
    public interface IBookingFactory
    {
        models.UpdateableBooking CreateFull(Booking booking);

        models.FlatBooking Create(Booking booking);

        IEnumerable<models.FlatBooking> Create(IEnumerable<Booking> bookings);

        Booking Create(models.Booking booking);

        Booking Create(models.UpdateableBooking booking);
    }
}