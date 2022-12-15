using VehicleManagement.DataContracts.DataModels;

namespace VehicleManagement.DBAccess.Transcations
{
    public interface IBookingTransaction
    {
        Task<IEnumerable<FlatBooking>> GetAllAsync(CancellationToken cancellationToken);

        Task<FlatBooking> AddAsync(Booking booking, CancellationToken cancellationToken);
    }
}