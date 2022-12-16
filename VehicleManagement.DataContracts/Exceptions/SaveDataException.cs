namespace VehicleManagement.DataContracts.Exceptions
{
    public class SaveDataException : Exception
    {
        public IEnumerable<object> InvalidData { get; }

        public SaveDataException(string message, IEnumerable<object> invalidData) : base(message)
        {
            InvalidData = invalidData;
        }
    }
}
