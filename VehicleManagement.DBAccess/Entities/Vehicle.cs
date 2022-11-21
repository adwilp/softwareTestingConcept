using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleManagement.DBAccess.Entities
{
    public class Vehicle : BaseEntity
    {
        [Key]
        public string FIN { get; set; }

        public string LicensePlate { get; set; }

        public string Color { get; set; }

        public double Mileage { get; set; }

        public string ManufacturerId { get; set; }

        [ForeignKey("ManufacturerId")]
        public virtual Manufacturer Manufacturer { get; set; }
    }
}
