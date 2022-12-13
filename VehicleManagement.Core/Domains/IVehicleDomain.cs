using VehicleManagement.DataContracts.DataModels;

namespace VehicleManagement.Core.Domains
{
    public interface IVehicleDomain
    {
        Task<IEnumerable<FlatVehicle>> GetAllAsync(CancellationToken cancellationToken);
    }
}
