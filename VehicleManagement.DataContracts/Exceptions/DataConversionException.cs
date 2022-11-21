namespace VehicleManagement.DataContracts.Exceptions
{
    public class DataConversionException : ArgumentException
    {
        public DataConversionException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
