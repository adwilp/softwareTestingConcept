namespace VehicleManagement.DBAccess.IntegrationTests.DBFixtures
{
    public class ManufacturerDBFixture : SharedDatabaseFixture
    {
        protected override void InitializeData()
        {
            InsertData(nameof(VehicleManagementContext.Manufacturer), "'WMI', 'Audi'");
            InsertData(nameof(VehicleManagementContext.Manufacturer), "'W0L', 'Opel'");
        }
    }
}
