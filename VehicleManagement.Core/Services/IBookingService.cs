using VehicleManagement.DataContracts.DataModels;

namespace VehicleManagement.Core.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<FlatBooking>> GetAllAsync(CancellationToken cancellationToken);
    }
}