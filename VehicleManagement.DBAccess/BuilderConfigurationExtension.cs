using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using VehicleManagement.DBAccess.Factories;
using VehicleManagement.DBAccess.Repositories;
using VehicleManagement.DBAccess.Transcations;

namespace VehicleManagement.DBAccess
{
    public static class BuilderConfigurationExtension
    {
        public static void AddVehicleManagementDB(this WebApplicationBuilder builder, string connectionName)
        {
            builder.AddDBContext(connectionName);
            builder.AddRepositories();
            builder.AddFactories();
            builder.AddTransactions();
        }

        private static void AddTransactions(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IManufacturerTransaction, ManufacturerTransaction>();
            builder.Services.AddScoped<IVehicleTransaction, VehicleTransaction>();
        }

        private static void AddRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
            builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
        }

        private static void AddFactories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IVehicleFactory, VehicleFactory>();
            builder.Services.AddScoped<IManufacturerFactory, ManufacturerFactory>();
        }

        private static void AddDBContext(this WebApplicationBuilder builder, string connectionName)
        {
            builder.Services.AddDbContext<IVehicleManagementContext, VehicleManagementContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString(connectionName));
            });
        }
    }
}
