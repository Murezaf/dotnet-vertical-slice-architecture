using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TravelInspiration.API.Features.Stops;
using TravelInspiration.API.IntegrationTests.Fixtures;
using TravelInspiration.API.Shared.Domain.Entities;

namespace TravelInspiration.API.IntegrationTests.Features.Stops;

public class CreateStopTests(SliceFixture sliceFixture) : IClassFixture<SliceFixture>
{
    private readonly SliceFixture _sliceFixture = sliceFixture;

    [Fact]
    public async Task WhenExecutingCreateStopSlice_WithValidInput_StopMustBeCreated()
    {
        using (var scope = _sliceFixture.ServiceScopeFactory.CreateScope())
        {
            //ARRANGE
            var context = _sliceFixture.CreateContext(scope);
            await context.Database.BeginTransactionAsync();

            var itinerary = new Itinerary("Test", "SomeUserId");
            context.Add(itinerary);
            await context.SaveChangesAsync();

            var cmd = new CreateStop.CreateStopCommand(itinerary.Id, "A Stop for Testing", null);

            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            //ACT
            await mediator.Send(cmd);

            //ASSERT
            context.ChangeTracker.Clear();
            var stop = await context.Stops.FirstOrDefaultAsync(s => s.ItineraryId == itinerary.Id);
            Assert.NotNull(stop);
            Assert.Equal(stop.ItineraryId, cmd.ItineraryId);
            Assert.Equal(stop.Name, cmd.Name);
            
            await context.Database.RollbackTransactionAsync();
        }
    }
}
