using VehicleManagement.DataContracts.DataModels;

namespace VehicleManagement.DBAccess.Transcations
{
    public interface IManufacturerTransaction
    {
        Task<IEnumerable<Manufacturer>> GetAllAsync(CancellationToken cancellation);
    }
}
