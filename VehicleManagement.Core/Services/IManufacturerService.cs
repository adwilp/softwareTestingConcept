using VehicleManagement.DataContracts.DataModels;

namespace VehicleManagement.Core.Services
{
    public interface IManufacturerService
    {
        Task<IEnumerable<Manufacturer>> GetAllAsync(CancellationToken cancellationToken);
    }
}
