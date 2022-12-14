using VehicleManagement.DataContracts.DataModels;
using VehicleManagement.DBAccess.Factories;
using VehicleManagement.DBAccess.Repositories;

namespace VehicleManagement.DBAccess.Transcations
{
    public class BookingTransaction : IBookingTransaction
    {
        private readonly IBookingFactory _bookingFactory;
        private readonly IBookingRepository _bookingRepository;

        public BookingTransaction(IBookingFactory bookingFactory, IBookingRepository bookingRepository)
        {
            _bookingFactory = bookingFactory;
            _bookingRepository = bookingRepository;
        }

        public async Task<IEnumerable<FlatBooking>> GetAllAsync(CancellationToken cancellationToken)
        {
            var bookings = await _bookingRepository.GetAllAsync(cancellationToken, asNoTracking: true, includedPaths: "Vehicle");

            return _bookingFactory.Create(bookings);
        }
    }
}
