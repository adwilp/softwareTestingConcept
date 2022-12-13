using VehicleManagement.DataContracts.DataModels;
using VehicleManagement.DataContracts.Exceptions;
using VehicleManagement.DBAccess.Entities;

namespace VehicleManagement.DBAccess.Factories
{
    public class VehicleFactory : IVehicleFactory
    {
        public FlatVehicle Create(Vehicle vehicle)
        {
            var flatVehicle = new FlatVehicle();

            if (vehicle == null)
            {
                throw new DataConversionException(Messages.NullObject, nameof(vehicle));
            }

            flatVehicle.FIN = vehicle.FIN;
            flatVehicle.LicensePlate = vehicle.LicensePlate;
            flatVehicle.Mileage = vehicle.Mileage;
            flatVehicle.Color = vehicle.Color;

            if (vehicle.Manufacturer != null)
            {
                flatVehicle.Manufacturer = vehicle.Manufacturer.Name;
            }

            return flatVehicle;
        }

        public IEnumerable<FlatVehicle> Create(IEnumerable<Vehicle> vehicles)
        {
            if (vehicles == null)
            {
                throw new DataConversionException(Messages.NullObject, nameof(vehicles));
            }

            return vehicles.Select(v => Create(v));
        }
    }
}
