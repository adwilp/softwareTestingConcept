namespace VehicleManagement.DBAccess.IntegrationTests.DBFixtures
{
    public class ManufacturerDBFixture : SharedDatabaseFixture
    {
        protected override void InitializeData()
        {
            InsertData(
                nameof(VehicleManagementContext.Manufacturer),
                _manufacturerColumns,
                "'WMI', 'Audi'");
            InsertData(
                nameof(VehicleManagementContext.Manufacturer),
                _manufacturerColumns,
                "'W0L', 'Opel'");
        }
    }
}
