using VehicleManagement.DataContracts.DataModels;

using entities = VehicleManagement.DBAccess.Entities;

namespace VehicleManagement.DBAccess.Factories
{
    public interface IManufacturerFactory
    {
        Manufacturer Create(entities.Manufacturer manufacturer);
        IEnumerable<Manufacturer> Create(IEnumerable<entities.Manufacturer> manufacturers);
    }
}
