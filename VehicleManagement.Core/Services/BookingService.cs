using VehicleManagement.DataContracts.DataModels;
using VehicleManagement.DBAccess.Transcations;

namespace VehicleManagement.Core.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingTransaction _bookingTransaction;

        public BookingService(IBookingTransaction bookingTransaction)
        {
            _bookingTransaction = bookingTransaction;
        }

        public async Task<FlatBooking> AddAsync(Booking booking, CancellationToken cancellationToken)
        {
            return await _bookingTransaction.AddAsync(booking, cancellationToken);
        }

        public async Task<IEnumerable<FlatBooking>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _bookingTransaction.GetAllAsync(cancellationToken);
        }
    }
}
