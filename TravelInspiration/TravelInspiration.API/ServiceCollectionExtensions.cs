using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TravelInspiration.API.Shared.Networking;
using TravelInspiration.API.Shared.Persistence.Migrations;
using TravelInspiration.API.Shared.Slices;

namespace TravelInspiration.API;


public static class ServiceCollectionExtensions
{

    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IDestinationSearchApiClient, DestinationSearchApiClient>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });
        services.RegisterSlices();

        return services;
    }

    public static IServiceCollection RegisterPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TravelInspirationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("TravelInspirationDbConnection"));
        });
        return services;
    }
}
