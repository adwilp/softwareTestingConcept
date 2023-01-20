using VehicleManagement.Core.Services;
using VehicleManagement.DataContracts.DataModels;

namespace VehicleManagement.Core.Domains
{
    public class BookingDomain : IBookingDomain
    {
        private readonly IBookingService _bookingService;

        public BookingDomain(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<FlatBooking> AddAsync(Booking booking, CancellationToken cancellationToken)
        {
            return await _bookingService.AddAsync(booking, cancellationToken);
        }

        public async Task<IEnumerable<FlatBooking>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _bookingService.GetAllAsync(cancellationToken);
        }

        public async Task<UpdateableBooking> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await _bookingService.GetAsync(id, cancellationToken);
        }

        public async Task<FlatBooking> UpdateAsync(UpdateableBooking booking, CancellationToken cancellationToken)
        {
            return await _bookingService.UpdateAsync(booking, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _bookingService.DeleteAsync(id, cancellationToken);
        }
    }
}
