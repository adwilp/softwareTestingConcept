namespace VehicleManagement.DataContracts.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public object RequestedData { get; }

        public EntityNotFoundException(string message, object requestedData) : base(message)
        {
            RequestedData = requestedData;
        }
    }
}
