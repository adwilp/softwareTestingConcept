using VehicleManagement.Core.Services;
using VehicleManagement.DataContracts.DataModels;

namespace VehicleManagement.Core.Domains
{
    public class ManufacturerDomain : IManufacturerDomain
    {
        private readonly IManufacturerService _manufacturerService;

        public ManufacturerDomain(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }

        public async Task<IEnumerable<Manufacturer>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _manufacturerService.GetAllAsync(cancellationToken);
        }
    }
}
