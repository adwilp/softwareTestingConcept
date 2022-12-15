using VehicleManagement.DataContracts.DataModels;

namespace VehicleManagement.Core.Domains
{
    public interface IBookingDomain
    {
        Task<IEnumerable<FlatBooking>> GetAllAsync(CancellationToken cancellationToken);
    }
}