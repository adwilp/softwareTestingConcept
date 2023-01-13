using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleManagement.DBAccess.Entities
{
    public class Booking : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        [Required]
        public string EmployeeNumber { get; set; }

        [Required]
        public string FIN { get; set; }

        [ForeignKey("FIN")]
        public virtual Vehicle Vehicle { get; set; }
    }
}
