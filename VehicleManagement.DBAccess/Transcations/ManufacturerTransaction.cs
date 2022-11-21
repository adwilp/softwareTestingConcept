using VehicleManagement.DataContracts.DataModels;
using VehicleManagement.DBAccess.Factories;
using VehicleManagement.DBAccess.Repositories;

namespace VehicleManagement.DBAccess.Transcations
{
    public class ManufacturerTransaction : IManufacturerTransaction
    {
        private readonly IVehicleFactory _vehicleFactory;
        private readonly IVehicleRepository _vehicleRepository;

        public ManufacturerTransaction(IVehicleFactory vehicleFactory, IVehicleRepository vehicleRepository)
        {
            _vehicleFactory = vehicleFactory;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<IEnumerable<FlatVehicle>> GetFlatVehiclesAsync(CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleRepository.GetAllAsync(cancellationToken, asNoTracking: true, includedPaths: "Manufacturer");

            return vehicles.Select(v => _vehicleFactory.Create(v));
        }
    }
}
