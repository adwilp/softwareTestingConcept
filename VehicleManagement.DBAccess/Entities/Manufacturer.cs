using System.ComponentModel.DataAnnotations;

namespace VehicleManagement.DBAccess.Entities
{
    public class Manufacturer : BaseEntity
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
