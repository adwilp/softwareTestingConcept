using VehicleManagement.Core;
using VehicleManagement.DBAccess;

namespace VehicleManagement.Backend
{
    public static class BuilderConfigurationExtension
    {
        public static void Configure(this WebApplicationBuilder builder)
        {
            builder.AddVehicleManagementDB("VehicleManagementDb");
            builder.AddDomains();
        }
    }
}
