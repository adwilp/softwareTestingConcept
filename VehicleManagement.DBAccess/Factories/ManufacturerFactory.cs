using VehicleManagement.DataContracts.DataModels;
using VehicleManagement.DataContracts.Exceptions;

using entities = VehicleManagement.DBAccess.Entities;

namespace VehicleManagement.DBAccess.Factories
{
    public class ManufacturerFactory : IManufacturerFactory
    {
        public Manufacturer Create(entities.Manufacturer manufacturer)
        {
            if (manufacturer == null)
            {
                throw new DataConversionException(Messages.NullObject, nameof(manufacturer));
            }

            return new Manufacturer()
            {
                Id = manufacturer.Id,
                Name = manufacturer.Name,
            };
        }

        public IEnumerable<Manufacturer> Create(IEnumerable<entities.Manufacturer> manufacturers)
        {
            if (manufacturers == null)
            {
                throw new DataConversionException(Messages.NullObject, nameof(manufacturers));
            }

            return manufacturers.Select(m => Create(m));
        }
    }
}
