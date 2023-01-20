using VehicleManagement.Core;
using VehicleManagement.DBAccess;

namespace VehicleManagement.Backend
{
    /// <summary>
    /// Extension method to configure webserver.
    /// </summary>
    public static class BuilderConfigurationExtension
    {
        /// <summary>
        /// Configures the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void Configure(this WebApplicationBuilder builder)
        {
            builder.AddVehicleManagementDB("VehicleManagementDb");
            builder.AddDomains();
        }
    }
}
