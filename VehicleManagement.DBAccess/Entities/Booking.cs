using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleManagement.DBAccess.Entities
{
    public class Booking : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string EmployeeNumber { get; set; }

        public string FIN { get; set; }

        [ForeignKey("FIN")]
        public virtual Vehicle Vehicle { get; set; }
    }
}
