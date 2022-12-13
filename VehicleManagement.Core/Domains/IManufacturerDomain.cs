using VehicleManagement.DataContracts.DataModels;

namespace VehicleManagement.Core.Domains
{
    public interface IManufacturerDomain
    {
        Task<IEnumerable<Manufacturer>> GetAllAsync(CancellationToken cancellationToken);
    }
}
