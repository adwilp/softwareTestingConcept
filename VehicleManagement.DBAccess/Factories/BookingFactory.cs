using VehicleManagement.DataContracts.DataModels;
using VehicleManagement.DataContracts.Exceptions;
using VehicleManagement.DBAccess.Entities;

namespace VehicleManagement.DBAccess.Factories
{
    public class BookingFactory : IBookingFactory
    {
        public FlatBooking Create(Booking booking)
        {
            if (booking == null)
            {
                throw new DataConversionException(Messages.NullObject, nameof(booking));
            }

            var flatBooking = new FlatBooking()
            {
                Id = booking.Id,
                Start = booking.Start,
                End = booking.End,
                EmployeeNumber = booking.EmployeeNumber,
                FIN = booking.FIN,
            };

            if (booking.Vehicle != null)
            {
                flatBooking.LicensePlate = booking.Vehicle.LicensePlate;
            }

            return flatBooking;
        }

        public IEnumerable<FlatBooking> Create(IEnumerable<Booking> bookings)
        {
            if (bookings == null)
            {
                throw new DataConversionException(Messages.NullObject, nameof(bookings));
            }

            return bookings.Select(booking => Create(booking));
        }
    }
}
