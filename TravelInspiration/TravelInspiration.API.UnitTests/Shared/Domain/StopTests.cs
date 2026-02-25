using TravelInspiration.API.Shared.Domain.Entities;
using TravelInspiration.API.Shared.Domain.Events;
using static TravelInspiration.API.Features.Stops.CreateStop;

namespace TravelInspiration.API.UnitTests.Shared.Domain;

public class StopTests : IDisposable
{
    private readonly Stop _stopEntity;
    private readonly CreateStopCommand _createStopCommand;

    public StopTests()
    {
        _stopEntity = new Stop("StopForTesting");
        _createStopCommand = new CreateStopCommand(42, "A Name", null);
    }

    [Fact]
    public void WhenExecutingHandleCreateCommand_WithItineraryId_StopItineraryIdMustMatch()
    {
        //ARRANGE

        //ACT
        _stopEntity.HandleCreateCommand(_createStopCommand);

        //ASSERT
        Assert.Equal(_createStopCommand.ItineraryId, _stopEntity.ItineraryId);
    }

    [Fact]
    public void WhenExecutingCreateCommand_WithValidInput_OneStopCreatedEventMustBeAdded()
    {
        //ARRANGE

        //ACT
        _stopEntity.HandleCreateCommand(_createStopCommand);

        //ASSERT
        Assert.Single(_stopEntity.DomainEvents);
        Assert.IsType<StopCreatedEvent>(_stopEntity.DomainEvents[0]);
    }

    public void Dispose()
    {
    }

    //[Fact]
    //public void WhenExecutingHandleCreateCommand_WithItineraryId_StopItineraryIdMustMatch()
    //{
    //    //ARRANGE
    //    var stopEntity = new Stop("StopForTesting");
    //    var createStopCommand = new CreateStopCommand(42, "A Name", null);

    //    //ACT
    //    stopEntity.HandleCreateCommand(createStopCommand);

    //    //ASSERT
    //    Assert.Equal(createStopCommand.ItineraryId, stopEntity.ItineraryId);
    //}

    //[Fact]
    //public void WhenExecutingCreateCommand_WithValidInput_OneStopCreatedEventMustBeAdded()
    //{
    //    //ARRANGE
    //    var stopEntity = new Stop("StopForTesting");
    //    var createStopCommand = new CreateStopCommand(42, "A Name", null);

    //    //ACT
    //    stopEntity.HandleCreateCommand(createStopCommand);

    //    //ASSERT
    //    Assert.Single(stopEntity.DomainEvents);
    //    Assert.IsType<StopCreatedEvent>(stopEntity.DomainEvents[0]);
    //}
}
