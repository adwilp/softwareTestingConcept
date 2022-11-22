using VehicleManagement.DataContracts.DataModels;
using VehicleManagement.DBAccess.Transcations;

namespace VehicleManagement.Core.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IManufacturerTransaction _manufacturerTransaction;

        public ManufacturerService(IManufacturerTransaction manufacturerTransaction)
        {
            _manufacturerTransaction = manufacturerTransaction;
        }

        public async Task<IEnumerable<Manufacturer>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _manufacturerTransaction.GetAllAsync(cancellationToken);
        }
    }
}
