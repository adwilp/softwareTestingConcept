using VehicleManagement.DataContracts.DataModels;
using VehicleManagement.DBAccess.Factories;
using VehicleManagement.DBAccess.Repositories;

namespace VehicleManagement.DBAccess.Transcations
{
    public class ManufacturerTransaction : IManufacturerTransaction
    {
        private readonly IManufacturerFactory _manufacturerFactory;
        private readonly IManufacturerRepository _manufacturerRepository;

        public ManufacturerTransaction(IManufacturerFactory manufacturerFactory, IManufacturerRepository manufacturerRepository)
        {
            _manufacturerFactory = manufacturerFactory;
            _manufacturerRepository = manufacturerRepository;
        }

        public async Task<IEnumerable<Manufacturer>> GetAllAsync(CancellationToken cancellation)
        {
            var manufacturers = await _manufacturerRepository.GetAllAsync(cancellation);

            return _manufacturerFactory.Create(manufacturers);
        }
    }
}