using VehicleManagement.DataContracts.DataModels;
using VehicleManagement.DBAccess.Transcations;

namespace VehicleManagement.Core.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleTransaction _vehicleTransaction;

        public VehicleService(IVehicleTransaction vehicleTransaction)
        {
            _vehicleTransaction = vehicleTransaction;
        }

        public async Task<IEnumerable<FlatVehicle>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _vehicleTransaction.GetAllAsync(cancellationToken);
        }
    }
}
