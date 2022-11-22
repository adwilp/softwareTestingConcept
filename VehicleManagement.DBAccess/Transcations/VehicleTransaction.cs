using VehicleManagement.DataContracts.DataModels;
using VehicleManagement.DBAccess.Factories;
using VehicleManagement.DBAccess.Repositories;

namespace VehicleManagement.DBAccess.Transcations
{
    public class VehicleTransaction : IVehicleTransaction
    {
        private readonly IVehicleFactory _vehicleFactory;
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleTransaction(IVehicleFactory vehicleFactory, IVehicleRepository vehicleRepository)
        {
            _vehicleFactory = vehicleFactory;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<IEnumerable<FlatVehicle>> GetAllAsync(CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleRepository.GetAllAsync(cancellationToken, asNoTracking: true, includedPaths: "Manufacturer");

            return vehicles.Select(v => _vehicleFactory.Create(v));
        }
    }
}
