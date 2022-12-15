using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using VehicleManagement.Core.Domains;
using VehicleManagement.Core.Services;

namespace VehicleManagement.Core
{
    public static class BuilderConfigurationExtension
    {
        public static void AddDomains(this WebApplicationBuilder builder)
        {
            builder.AddService();

            builder.Services.AddScoped<IVehicleDomain, VehicleDomain>();
            builder.Services.AddScoped<IManufacturerDomain, ManufacturerDomain>();
            builder.Services.AddScoped<IBookingDomain, BookingDomain>();
        }

        private static void AddService(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IVehicleService, VehicleService>();
            builder.Services.AddScoped<IManufacturerService, ManufacturerService>();
            builder.Services.AddScoped<IBookingService, BookingService>();
        }
    }
}
