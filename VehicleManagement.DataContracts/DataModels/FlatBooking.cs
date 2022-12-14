namespace VehicleManagement.DataContracts.DataModels
{
    public class FlatBooking
    {
        public int Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string EmployeeNumber { get; set; }

        public string FIN { get; set; }

        public string LicensePlate { get; set; }
    }
}
