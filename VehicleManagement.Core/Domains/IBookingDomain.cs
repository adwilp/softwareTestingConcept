using VehicleManagement.DataContracts.DataModels;

namespace VehicleManagement.Core.Domains
{
    public interface IBookingDomain
    {
        Task<IEnumerable<FlatBooking>> GetAllAsync(CancellationToken cancellationToken);

        Task<FlatBooking> AddAsync(Booking booking, CancellationToken cancellationToken);

        Task<FlatBooking> UpdateAsync(UpdateableBooking booking, CancellationToken cancellationToken);
    }
}