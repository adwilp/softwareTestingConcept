using VehicleManagement.DataContracts.DataModels;
using VehicleManagement.DBAccess.Entities;

namespace VehicleManagement.DBAccess.Factories
{
    public interface IVehicleFactory
    {
        FlatVehicle Create(Vehicle vehicle);
        IEnumerable<FlatVehicle> Create(IEnumerable<Vehicle> vehicles);
    }
}
