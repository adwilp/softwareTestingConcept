using VehicleManagement.DataContracts.Exceptions;
using VehicleManagement.DBAccess.Entities;

using models = VehicleManagement.DataContracts.DataModels;

namespace VehicleManagement.DBAccess.Factories
{
    public class BookingFactory : IBookingFactory
    {
        public models.FlatBooking Create(Booking booking)
        {
            if (booking == null)
            {
                throw new DataConversionException(Messages.NullObject, nameof(booking));
            }

            var flatBooking = new models.FlatBooking()
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

        public IEnumerable<models.FlatBooking> Create(IEnumerable<Booking> bookings)
        {
            if (bookings == null)
            {
                throw new DataConversionException(Messages.NullObject, nameof(bookings));
            }

            return bookings.Select(booking => Create(booking));
        }

        public Booking Create(models.UpdateableBooking booking)
        {
            var entity = Create((models.Booking)booking);

            entity.Id = booking.Id;

            return entity;
        }

        public Booking Create(models.Booking booking)
        {
            if (booking == null)
            {
                throw new DataConversionException(Messages.NullObject, nameof(booking));
            }

            return new Booking()
            {
                Start = booking.Start,
                End = booking.End,
                EmployeeNumber = booking.EmployeeNumber,
                FIN = booking.FIN
            };
        }

    }
}
