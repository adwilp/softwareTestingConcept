using VehicleManagement.DataContracts.DataModels;

namespace VehicleManagement.DBAccess.Transcations
{
    public interface IVehicleTransaction
    {
        Task<IEnumerable<FlatVehicle>> GetAllAsync(CancellationToken cancellationToken);
    }
}
