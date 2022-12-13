using VehicleManagement.DataContracts.DataModels;

namespace VehicleManagement.Core.Services
{
    public interface IVehicleService
    {
        Task<IEnumerable<FlatVehicle>> GetAllAsync(CancellationToken cancellationToken);
    }
}
