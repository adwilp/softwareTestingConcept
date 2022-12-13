using VehicleManagement.Core.Services;
using VehicleManagement.DataContracts.DataModels;

namespace VehicleManagement.Core.Domains
{
    public class VehicleDomain : IVehicleDomain
    {
        private readonly IVehicleService _vehicleService;

        public VehicleDomain(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<IEnumerable<FlatVehicle>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _vehicleService.GetAllAsync(cancellationToken);
        }
    }
}
