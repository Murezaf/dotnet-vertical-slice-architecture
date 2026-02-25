using Microsoft.Extensions.DependencyInjection;
using TravelInspiration.API.IntegrationTests.Factories;
using TravelInspiration.API.Shared.Persistence.Migrations;

namespace TravelInspiration.API.IntegrationTests.Fixtures;

public sealed class SliceFixture
{
    private readonly TravelInspirationWebApplicationFactory _factory;
    public IServiceScopeFactory ServiceScopeFactory { get; }
    private static readonly object _lock = new object();
    private static bool _databaseInitialized; 

    public SliceFixture()
    {
        _factory = new TravelInspirationWebApplicationFactory();
        ServiceScopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();

        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                using (var scope = ServiceScopeFactory.CreateScope())
                {
                    using(var context = CreateContext(scope))
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();

                        context.Stops.RemoveRange(context.Stops.ToList());
                        context.Itineraries.RemoveRange(context.Itineraries.ToList());
                        context.SaveChanges();
                    }
                }
                _databaseInitialized = true;
            }
        }
    }

    public TravelInspirationDbContext CreateContext(IServiceScope scope)
    {
        var context = scope.ServiceProvider.GetRequiredService<TravelInspirationDbContext>();
        return context;
    }
}
