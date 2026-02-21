using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TravelInspiration.API.Shared.Behaviors;
using TravelInspiration.API.Shared.Metrics;
using TravelInspiration.API.Shared.Networking;
using TravelInspiration.API.Shared.Persistence.Migrations;
using TravelInspiration.API.Shared.Security;
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
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())
            .RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())
            .AddOpenRequestPreProcessor(typeof(LoggingBehavior<>))
            .AddOpenBehavior(typeof(ModelValidationBehavior<,>))
            .AddOpenBehavior(typeof(HandlerPerformanceMetricBehavior<,>));
        });
        services.RegisterSlices();
        services.AddSingleton<HandlerPerformanceMetric>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped<ICurrentUserService, CurrentUserService>();

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
