using TravelInspiration.API.Features.Stops;
using TravelInspiration.API.Shared.Domain.Entities;
using TravelInspiration.API.Shared.Domain.Events;

namespace TravelInspiration.API.UnitTests.Shared.Domain;

public class StopTests
{
    [Fact]
    public void WhenExecutingHandleCreateCommand_WithItineraryId_StopItineraryIdMustMatch()
    {
        //ARRANGE
        var stopEntity = new Stop("StopForTesting");
        var createStopCommand = new CreateStopCommand(42, "A Name", null);

        //ACT
        stopEntity.HandleCreateCommand(createStopCommand);

        //ASSERT
        Assert.Equal(createStopCommand.ItineraryId, stopEntity.ItineraryId);
    }

    [Fact]
    public void WhenExecutingCreateCommand_WithValidInput_OneStopCreatedEventMustBeAdded()
    {
        //ARRANGE
        var stopEntity = new Stop("StopForTesting");
        var createStopCommand = new CreateStopCommand(42, "A Name", null);

        //ACT
        stopEntity.HandleCreateCommand(createStopCommand);

        //ASSERT
        Assert.Single(stopEntity.DomainEvents);
        Assert.IsType<StopCreatedEvent>(stopEntity.DomainEvents[0]);
    }
}
