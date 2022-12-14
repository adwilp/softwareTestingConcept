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

        public async Task<IEnumerable<FlatBooking>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _bookingService.GetAllAsync(cancellationToken);
        }
    }
}
